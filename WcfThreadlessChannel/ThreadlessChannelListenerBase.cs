using System;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public abstract class ThreadlessChannelListenerBase<TChannel> : ChannelListenerBase<TChannel>, IHasBindingElement
        where TChannel : class, IChannel
    {
        private readonly Uri uri;
        private bool acceptChannel;

        public ThreadlessChannelListenerBase(ThreadlessBindingElement bindingElement, Uri uri)
        {
            BindingElement = bindingElement;
            this.uri = uri;
            acceptChannel = true;
        }

        public ThreadlessBindingElement BindingElement { get; private set; }

        public override Uri Uri
        {
            get { return uri; }
        }

        protected override void OnAbort()
        {
        }

        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
        }

        protected override TChannel OnEndAcceptChannel(IAsyncResult result)
        {
            return AcceptChannel();
        }

        protected override void OnEndClose(IAsyncResult result)
        {
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
        }

        protected override bool OnEndWaitForChannel(IAsyncResult result)
        {
            return false;
        }

        protected override void OnOpen(TimeSpan timeout)
        {
        }

        protected override bool OnWaitForChannel(TimeSpan timeout)
        {
            return false;
        }

        protected override TChannel OnAcceptChannel(TimeSpan timeout)
        {
            if (!acceptChannel)
            {
                return null;
            }

            acceptChannel = false;
            return OnAcceptChannel();
        }

        protected abstract TChannel OnAcceptChannel();
    }
}
