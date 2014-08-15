using System;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public delegate ChannelListenerBase ThreadlessChannelListenerProvider(ThreadlessBindingElement bindingElement, Uri uri);
}
