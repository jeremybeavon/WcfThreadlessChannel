using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessReplySessionChannelListener : ThreadlessChannelListenerBase<IReplySessionChannel>
    {
        public ThreadlessReplySessionChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IReplySessionChannel OnAcceptChannel()
        {
            return new ThreadlessReplySessionChannel(this, new EndpointAddress(Uri));
        }
    }
}
