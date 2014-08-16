using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessDuplexSessionChannelFactory : ThreadlessChannelFactoryBase<IDuplexSessionChannel>
    {
        public ThreadlessDuplexSessionChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }

        protected override IDuplexSessionChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            return new ThreadlessDuplexSessionChannel(this, address, via);
        }
    }
}
