using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessOutputSessionChannelFactory : ThreadlessChannelFactoryBase<IOutputSessionChannel>
    {
        public ThreadlessOutputSessionChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }

        protected override IOutputSessionChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            return new ThreadlessOutputSessionChannel(this, address, via);
        }
    }
}
