using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessRequestChannel : ThreadlessChannelBase, IRequestChannel
    {
        public ThreadlessRequestChannel(
            ThreadlessRequestChannelFactory channelFactory,
            EndpointAddress remoteAddress,
            Uri via)
            : this(channelFactory, channelFactory, remoteAddress, via)
        {
        }

        protected ThreadlessRequestChannel(
            ChannelManagerBase channelManager,
            IHasBindingElement bindingElement,
            EndpointAddress remoteAddress,
            Uri via)
            : base(channelManager, bindingElement)
        {
            RemoteAddress = remoteAddress;
            Via = via;
        }

        public EndpointAddress RemoteAddress { get; private set; }

        public Uri Via { get; private set; }

        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult<Message>(Request(message), callback, state);
        }

        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginRequest(message, callback, state);
        }

        public Message EndRequest(IAsyncResult result)
        {
            return ((CompletedAsyncResult<Message>)result).Result;
        }

        public Message Request(Message message)
        {
            return BindingElement.ExecuteRequest(Via, message);
        }

        public Message Request(Message message, TimeSpan timeout)
        {
            return Request(message);
        }
    }
}
