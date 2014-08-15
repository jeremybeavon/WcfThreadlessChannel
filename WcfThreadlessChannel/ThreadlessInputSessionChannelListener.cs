using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessInputSessionChannelListener : ThreadlessChannelListenerBase<IInputSessionChannel>
    {
        public ThreadlessInputSessionChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IInputSessionChannel OnAcceptChannel()
        {
            return new ThreadlessInputSessionChannel(this, new EndpointAddress(Uri));
        }
    }
}
