using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessReplyChannelListener : ThreadlessChannelListenerBase<IReplyChannel>
    {
        public ThreadlessReplyChannelListener(ThreadlessBindingElement bindingElement, Uri uri)
            : base(bindingElement, uri)
        {
        }

        protected override IReplyChannel OnAcceptChannel()
        {
            return new ThreadlessReplyChannel(this, new EndpointAddress(Uri));
        }
    }
}
