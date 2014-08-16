using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessDuplexChannelListener : ThreadlessChannelListenerBase<IDuplexChannel>
    {
        public ThreadlessDuplexChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IDuplexChannel OnAcceptChannel()
        {
            return new ThreadlessDuplexChannel(this, new EndpointAddress(Uri));
        }
    }
}
