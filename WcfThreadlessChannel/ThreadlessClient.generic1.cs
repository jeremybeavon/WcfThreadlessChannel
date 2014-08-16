using System;
using System.ServiceModel;

namespace WcfThreadlessChannel
{
    public class ThreadlessClient<TServiceInterface> : IDisposable
        where TServiceInterface : class
    {
        private readonly ServiceHost serviceHost;
        private readonly ChannelFactory<TServiceInterface> channelFactory;

        public ThreadlessClient(EndpointAddress address, Type serviceType)
            : this(address, serviceType, new ThreadlessBinding())
        {
        }

        public ThreadlessClient(EndpointAddress address, Type serviceType, ThreadlessBinding binding)
            : this(address, serviceType, binding, new ChannelFactory<TServiceInterface>(binding, address))
        {
        }

        protected ThreadlessClient(
            EndpointAddress address,
            Type serviceType,
            ThreadlessBinding binding,
            ChannelFactory<TServiceInterface> channelFactory)
        {
            serviceHost = new ServiceHost(serviceType);
            serviceHost.AddServiceEndpoint(typeof(TServiceInterface), binding, address.Uri);
            serviceHost.Open();
            this.channelFactory = channelFactory;
            Client = channelFactory.CreateChannel();
        }

        public TServiceInterface Client { get; private set; }

        public void Dispose()
        {
            Client = null;
            channelFactory.Close();
            serviceHost.Close();
        }
    }
}
