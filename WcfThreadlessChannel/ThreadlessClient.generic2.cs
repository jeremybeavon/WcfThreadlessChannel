using System;
using System.ServiceModel;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessClient<TService, TServiceInterface> : ThreadlessClient<TServiceInterface>
        where TService : TServiceInterface
        where TServiceInterface : class
    {
        public ThreadlessClient(EndpointAddress address)
            : base(address, typeof(TService))
        {
        }

        public ThreadlessClient(EndpointAddress address, ThreadlessBinding binding)
            : base(address, typeof(TService), binding)
        {
        }
    }
}
