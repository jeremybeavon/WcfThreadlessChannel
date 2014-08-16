using System;
using System.ServiceModel;

namespace WcfThreadlessChannel
{
    public class ThreadlessDuplexClient<TServiceInterface> : ThreadlessClient<TServiceInterface>
        where TServiceInterface : class
    {
        public ThreadlessDuplexClient(EndpointAddress address, Type serviceType, object callback)
            : this(address, serviceType, callback, new ThreadlessBinding())
        {
        }

        public ThreadlessDuplexClient(EndpointAddress address, Type serviceType, object callback, ThreadlessBinding binding)
            : base(
                  address,
                  serviceType,
                  binding,
                  new DuplexChannelFactory<TServiceInterface>(new InstanceContext(callback), binding, address))
        {
        }
    }
}
