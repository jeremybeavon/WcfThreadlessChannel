using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessDuplexChannel : ThreadlessChannelBase, IDuplexChannel
    {
        public ThreadlessDuplexChannel(
            ThreadlessDuplexChannelFactory channelFactory, EndpointAddress address, Uri via)
            : this(channelFactory, channelFactory, BuildCallbackAddress(address), address, via)
        {
        }

        public ThreadlessDuplexChannel(ThreadlessDuplexChannelListener listener, EndpointAddress address)
            : this(listener, listener, address, BuildCallbackAddress(address), address.Uri)
        {
        }

        protected ThreadlessDuplexChannel(
            ChannelManagerBase manager,
            IHasBindingElement bindingElement,
            EndpointAddress localAddress,
            EndpointAddress remoteAddress,
            Uri via)
            : base(manager, bindingElement)
        {
            LocalAddress = localAddress;
            RemoteAddress = remoteAddress;
            Via = via;
        }

        public EndpointAddress LocalAddress { get; private set; }

        public EndpointAddress RemoteAddress { get; private set; }

        public Uri Via { get; private set; }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            return BindingElement.CreateAndRegisterRequestContext(this, LocalAddress, callback, state);
        }

        public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReceive(callback, state);
        }

        public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
        {
            Send(message);
            return new CompletedAsyncResult(callback, state);
        }

        public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginSend(message, callback, state);
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

        public void EndSend(IAsyncResult result)
        {
        }

        public bool EndTryReceive(IAsyncResult result, out Message message)
        {
            ThreadlessRequestContext context = result as ThreadlessRequestContext;
            message = context == null ? null : context.RequestMessage;
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

        public void Send(Message message)
        {
            BindingElement.ExecuteRequest(RemoteAddress.Uri, message);
        }

        public void Send(Message message, TimeSpan timeout)
        {
            Send(message);
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

        protected static EndpointAddress BuildCallbackAddress(EndpointAddress address)
        {
            return new EndpointAddress(address.Uri.ToString() + "/callback");
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            BindingElement.CloseUrl(LocalAddress.Uri);
        }
    }
}
