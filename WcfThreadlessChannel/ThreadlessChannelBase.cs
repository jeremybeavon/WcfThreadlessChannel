using System;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public abstract class ThreadlessChannelBase : ChannelBase, IHasBindingElement
    {
        public ThreadlessChannelBase(ChannelManagerBase channelManager, IHasBindingElement bindingElement)
            : base(channelManager)
        {
            BindingElement = bindingElement.BindingElement;
        }

        public ThreadlessBindingElement BindingElement { get; private set; }

        protected override void OnAbort()
        {
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
        }

        protected override void OnEndClose(IAsyncResult result)
        {
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
        }

        protected override void OnOpen(TimeSpan timeout)
        {
        }
    }
}
