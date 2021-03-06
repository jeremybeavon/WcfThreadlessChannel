﻿using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessBindingElement : TransportBindingElement
    {
        private static readonly ThreadlessChannelFactories ChannelFactories = new ThreadlessChannelFactories()
        {
            { typeof(IRequestChannel), bindingElement => new ThreadlessRequestChannelFactory(bindingElement) },
            { typeof(IRequestSessionChannel), bindingElement => new ThreadlessRequestSessionChannelFactory(bindingElement) },
            { typeof(IOutputChannel), bindingElement => new ThreadlessOutputChannelFactory(bindingElement) },
            { typeof(IOutputSessionChannel), bindingElement => new ThreadlessOutputSessionChannelFactory(bindingElement) },
            { typeof(IDuplexChannel), bindingElement => new ThreadlessDuplexChannelFactory(bindingElement) },
            { typeof(IDuplexSessionChannel), bindingElement => new ThreadlessDuplexSessionChannelFactory(bindingElement) }
        };

        private static readonly ThreadlessChannelListeners ChannelListeners = new ThreadlessChannelListeners()
        {
            { typeof(IReplyChannel), (bindingElement, uri) => new ThreadlessReplyChannelListener(bindingElement, uri) },
            { typeof(IReplySessionChannel), (bindingElement, uri) => new ThreadlessReplySessionChannelListener(bindingElement, uri) },
            { typeof(IInputChannel), (bindingElement, uri) => new ThreadlessInputChannelListener(bindingElement, uri) },
            { typeof(IInputSessionChannel), (bindingElement, uri) => new ThreadlessInputSessionChannelListener(bindingElement, uri) },
            { typeof(IDuplexChannel), (bindingElement, uri) => new ThreadlessDuplexChannelListener(bindingElement, uri) },
            { typeof(IDuplexSessionChannel), (bindingElement, uri) => new ThreadlessDuplexSessionChannelListener(bindingElement, uri) },
        };

        private readonly ConcurrentDictionary<Uri, ThreadlessRequestContextQueue> requestsByUri;

        public ThreadlessBindingElement()
        {
            requestsByUri = new ConcurrentDictionary<Uri, ThreadlessRequestContextQueue>();
        }

        private ThreadlessBindingElement(ThreadlessBindingElement elementToBeClone)
            : base(elementToBeClone)
        {
            requestsByUri = elementToBeClone.requestsByUri;
            Binding = elementToBeClone.Binding;
        }

        public ThreadlessBinding Binding { get; internal set; }

        public override string Scheme
        {
            get { return "net.threadless"; }
        }

        public override BindingElement Clone()
        {
            return new ThreadlessBindingElement(this);
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            return ChannelFactories.Keys.Contains(typeof(TChannel));
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            return ChannelListeners.Keys.Contains(typeof(TChannel));
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!CanBuildChannelFactory<TChannel>(context))
            {
                throw new InvalidOperationException(string.Format("ChannelTypeNotSupported - {0}", typeof(TChannel).Name));
            }

            if (ManualAddressing)
            {
                throw new InvalidOperationException("ManualAddressingNotSupported");
            }

            return (IChannelFactory<TChannel>)ChannelFactories[typeof(TChannel)](this);
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!CanBuildChannelListener<TChannel>(context))
            {
                throw new InvalidOperationException(string.Format("ChannelTypeNotSupported - {0}", typeof(TChannel).Name));
            }

            Uri uri = new Uri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress);
            return (IChannelListener<TChannel>)ChannelListeners[typeof(TChannel)](this, uri);
        }

        public ThreadlessRequestContext CreateAndRegisterRequestContext(
            ICommunicationObject channel,
            EndpointAddress address,
            AsyncCallback callback,
            object state)
        {
            if (channel.State == CommunicationState.Closing || channel.State == CommunicationState.Closed)
            {
                return null;
            }

            ThreadlessRequestContext context = new ThreadlessRequestContext(callback, state);
            RegisterRequestContext(address.Uri, context);
            return context;
        }

        public void RegisterRequestContext(Uri uri, ThreadlessRequestContext context)
        {
            Func<Uri, ThreadlessRequestContextQueue> addAction = key =>
            {
                ThreadlessRequestContextQueue queue = new ThreadlessRequestContextQueue();
                queue.Enqueue(context);
                return queue;
            };
            ThreadlessRequestContextQueue requestQueue = null;
            Func<Uri, ThreadlessRequestContextQueue, ThreadlessRequestContextQueue> updateAction = (key, queue) =>
            {
                queue.Enqueue(context);
                requestQueue = queue; 
                return queue;
            };
            requestsByUri.AddOrUpdate(uri, addAction, updateAction);
            if (requestQueue != null && requestQueue.UseWaitHandle)
            {
                requestQueue.WaitHandle.Set();
            }
        }

        public Message ExecuteRequest(Uri uri, Message message)
        {
            ThreadlessRequestContext context;
            bool useWaitHandle = false;
            Func<Uri, ThreadlessRequestContextQueue> addAction = key =>
            {
                useWaitHandle = true;
                return new ThreadlessRequestContextQueue()
                {
                    UseWaitHandle = true
                };
            };
            ThreadlessRequestContextQueue queue = requestsByUri.GetOrAdd(uri, addAction);
            if (useWaitHandle)
            {
                bool hasRequest = queue.WaitHandle.WaitOne(TimeSpan.FromMinutes(1));
                queue.UseWaitHandle = false;
                if (!hasRequest)
                {
                    throw new CommunicationObjectFaultedException();
                }
            }
                
            if (queue.TryDequeue(out context))
            {
                message.Headers.To = uri;
                return context.ExecuteRequest(message);
            }
            
            throw new CommunicationObjectFaultedException();
        }

        public void CloseUrl(Uri uri)
        {
            ThreadlessRequestContextQueue queue;
            if (requestsByUri.TryRemove(uri, out queue))
            {
                ThreadlessRequestContext context;
                while (queue.TryDequeue(out context))
                {
                    context.Close();
                }
            }
        }
    }
}
