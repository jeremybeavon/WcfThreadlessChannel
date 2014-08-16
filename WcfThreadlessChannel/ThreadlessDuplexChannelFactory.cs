using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessDuplexChannelFactory : ThreadlessChannelFactoryBase<IDuplexChannel>
    {
        public ThreadlessDuplexChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }

        protected override IDuplexChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            return new ThreadlessDuplexChannel(this, address, via);
        }
    }
}
