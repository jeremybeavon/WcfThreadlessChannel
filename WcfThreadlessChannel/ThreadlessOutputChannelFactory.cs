using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessOutputChannelFactory : ThreadlessChannelFactoryBase<IOutputChannel>
    {
        public ThreadlessOutputChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }

        protected override IOutputChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            return new ThreadlessOutputChannel(this, address, via);
        }
    }
}
