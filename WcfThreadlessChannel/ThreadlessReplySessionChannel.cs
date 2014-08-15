using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessReplySessionChannel : ThreadlessReplyChannel, IReplySessionChannel
    {
        public ThreadlessReplySessionChannel(ThreadlessReplySessionChannelListener listener, EndpointAddress localAddress)
            : base(listener, listener, localAddress)
        {
        }

        public IInputSession Session
        {
            get { return BindingElement.Binding.InputSessionProvider(this); }
        }
    }
}
