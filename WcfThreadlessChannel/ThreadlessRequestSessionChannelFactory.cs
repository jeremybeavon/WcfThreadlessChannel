using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessRequestSessionChannelFactory : ThreadlessChannelFactoryBase<IRequestSessionChannel>
    {
        public ThreadlessRequestSessionChannelFactory(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
        }

        protected override IRequestSessionChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            throw new NotImplementedException();
        }
    }
}
