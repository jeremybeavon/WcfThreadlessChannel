using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessRequestChannelFactory : ThreadlessChannelFactoryBase<IRequestChannel>
    {
        public ThreadlessRequestChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }
      
        protected override IRequestChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            return new ThreadlessRequestChannel(this, address, via);
        }
    }
}
