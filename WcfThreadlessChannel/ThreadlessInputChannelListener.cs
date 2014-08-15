using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessInputChannelListener : ThreadlessChannelListenerBase<IInputChannel>
    {
        public ThreadlessInputChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IInputChannel OnAcceptChannel()
        {
            return new ThreadlessInputChannel(this, new EndpointAddress(Uri));
        }
    }
}
