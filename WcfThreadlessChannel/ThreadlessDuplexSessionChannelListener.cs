using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessDuplexSessionChannelListener : ThreadlessChannelListenerBase<IDuplexSessionChannel>
    {
        public ThreadlessDuplexSessionChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IDuplexSessionChannel OnAcceptChannel()
        {
            return new ThreadlessDuplexSessionChannel(this, new EndpointAddress(Uri));
        }
    }
}
