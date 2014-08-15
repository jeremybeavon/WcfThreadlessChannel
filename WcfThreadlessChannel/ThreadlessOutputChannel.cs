using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessOutputChannel : ThreadlessChannelBase, IOutputChannel
    {
        public ThreadlessOutputChannel(
            ThreadlessOutputChannelFactory channelFactory,
            EndpointAddress remoteAddress,
            Uri via)
            : this(channelFactory, channelFactory, remoteAddress, via)
        {
        }

        protected ThreadlessOutputChannel(
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

        public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
        {
            Send(message);
            return new CompletedAsyncResult(callback, state);
        }

        public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginSend(message, callback, state);
        }

        public void EndSend(IAsyncResult result)
        {
        }

        public void Send(Message message)
        {
            BindingElement.ExecuteRequest(Via, message);
        }

        public void Send(Message message, TimeSpan timeout)
        {
            Send(message);
        }
    }
}
