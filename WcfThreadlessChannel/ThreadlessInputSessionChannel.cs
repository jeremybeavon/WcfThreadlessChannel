using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessInputSessionChannel : ThreadlessInputChannel, IInputSessionChannel
    {
        public ThreadlessInputSessionChannel(ThreadlessInputSessionChannelListener listener, EndpointAddress localAddress)
            : base(listener, listener, localAddress)
        {
        }

        public IInputSession Session
        {
            get { return BindingElement.Binding.InputSessionProvider(this); }
        }
    }
}
