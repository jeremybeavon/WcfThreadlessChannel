using System;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public abstract class ThreadlessChannelFactoryBase<TChannel> : ChannelFactoryBase<TChannel>, IHasBindingElement
    {
        protected ThreadlessChannelFactoryBase(ThreadlessBindingElement bindingElement)
        {
            BindingElement = bindingElement;
        }

        public ThreadlessBindingElement BindingElement { get; private set; }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return new CompletedAsyncResult(callback, state);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
        }

        protected override void OnOpen(TimeSpan timeout)
        {
        }
    }
}
