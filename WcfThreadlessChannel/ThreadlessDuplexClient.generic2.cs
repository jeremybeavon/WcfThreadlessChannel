using System;
using System.ServiceModel;

namespace WcfThreadlessChannel
{
    public class ThreadlessDuplexClient<TService, TServiceInterface> : ThreadlessDuplexClient<TServiceInterface>
        where TServiceInterface : class
    {
        public ThreadlessDuplexClient(EndpointAddress address, object callback)
            : base(address, typeof(TService), callback)
        {
        }

        public ThreadlessDuplexClient(EndpointAddress address, object callback, ThreadlessBinding binding)
            : base(address, typeof(TService), callback, binding)
        {
        }
    }
}
