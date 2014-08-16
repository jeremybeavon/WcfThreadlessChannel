using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public delegate ChannelFactory<TServiceInterface> ThreadlessChannelFactoryProvider<TServiceInterface>();
}
