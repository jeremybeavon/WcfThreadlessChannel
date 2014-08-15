using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public class ThreadlessOutputSessionChannel : ThreadlessOutputChannel, IOutputSessionChannel
    {
        public ThreadlessOutputSessionChannel(
            ThreadlessOutputSessionChannelFactory channelFactory,
            EndpointAddress remoteAddress,
            Uri via)
            : base(channelFactory, channelFactory, remoteAddress, via)
        {
        }

        public IOutputSession Session
        {
            get { return BindingElement.Binding.OutputSessionProvider(this); }
        }
    }
}
