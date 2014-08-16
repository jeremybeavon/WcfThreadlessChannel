using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessDuplexSessionChannel : ThreadlessDuplexChannel, IDuplexSessionChannel
    {
        public ThreadlessDuplexSessionChannel(
            ThreadlessDuplexSessionChannelFactory channelFactory,
            EndpointAddress address,
            Uri via)
            : base(channelFactory, channelFactory, BuildCallbackAddress(address), address, via)
        {
        }

        public ThreadlessDuplexSessionChannel(
            ThreadlessDuplexSessionChannelListener listener,
            EndpointAddress address)
            : base(listener, listener, address, BuildCallbackAddress(address), address.Uri)
        {
        }

        public IDuplexSession Session
        {
            get { return BindingElement.Binding.DuplexSessionProvider(this); }
        }
    }
}
