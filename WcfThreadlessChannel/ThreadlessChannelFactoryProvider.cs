using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public delegate ChannelFactoryBase ThreadlessChannelFactoryProvider(ThreadlessBindingElement bindingElement);
}
