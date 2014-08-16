using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessInputChannel : ThreadlessChannelBase, IInputChannel
    {
        public ThreadlessInputChannel(ThreadlessInputChannelListener listener, EndpointAddress localAddress)
            : this(listener, listener, localAddress)
        {
        }

        protected ThreadlessInputChannel(
            ChannelManagerBase channelManager,
            IHasBindingElement bindingElement,
            EndpointAddress localAddress)
            : base(channelManager, bindingElement)
        {
            LocalAddress = localAddress;
        }

        public EndpointAddress LocalAddress { get; private set; }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            return BindingElement.CreateAndRegisterRequestContext(this, LocalAddress, callback, state);
        }

        public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReceive(callback, state);
        }

        public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReceive(callback, state);
        }

        public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        public Message EndReceive(IAsyncResult result)
        {
            return ((ThreadlessRequestContext)result).ResponseMessage;
        }

        public bool EndTryReceive(IAsyncResult result, out Message message)
        {
            message = ((ThreadlessRequestContext)result).ResponseMessage;
            return true;
        }

        public bool EndWaitForMessage(IAsyncResult result)
        {
            return false;
        }

        public Message Receive()
        {
            throw new NotSupportedException();
        }

        public Message Receive(TimeSpan timeout)
        {
            return Receive();
        }

        public bool TryReceive(TimeSpan timeout, out Message message)
        {
            message = null;
            return false;
        }

        public bool WaitForMessage(TimeSpan timeout)
        {
            return false;
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            BindingElement.CloseUrl(LocalAddress.Uri);
        }
    }
}
