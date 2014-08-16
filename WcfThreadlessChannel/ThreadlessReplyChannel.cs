using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessReplyChannel : ThreadlessChannelBase, IReplyChannel
    {
        public ThreadlessReplyChannel(ThreadlessReplyChannelListener listener, EndpointAddress localAddress)
            : this(listener, listener, localAddress)
        {
        }

        protected ThreadlessReplyChannel(
            ChannelManagerBase channelManager,
            IHasBindingElement bindingElement,
            EndpointAddress localAddress)
            : base(channelManager, bindingElement)
        {
            LocalAddress = localAddress;
        }

        public EndpointAddress LocalAddress { get; private set; }

        public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
        {
            return BindingElement.CreateAndRegisterRequestContext(this, LocalAddress, callback, state);
        }

        public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReceiveRequest(callback, state);
        }

        public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReceiveRequest(callback, state);
        }

        public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        public RequestContext EndReceiveRequest(IAsyncResult result)
        {
            return (RequestContext)result;
        }

        public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
        {
            context = result as RequestContext;
            return true;
        }

        public bool EndWaitForRequest(IAsyncResult result)
        {
            return false;
        }

        public RequestContext ReceiveRequest()
        {
            throw new NotSupportedException();
        }

        public RequestContext ReceiveRequest(TimeSpan timeout)
        {
            return ReceiveRequest();
        }

        public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
        {
            context = null;
            return false;
        }

        public bool WaitForRequest(TimeSpan timeout)
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
