using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WcfThreadlessChannel
{
    internal sealed class ThreadlessChannelListeners : ConcurrentDictionary<Type, ThreadlessChannelListenerProvider>
    {
        public void Add(Type type, ThreadlessChannelListenerProvider provider)
        {
            ((IDictionary<Type, ThreadlessChannelListenerProvider>)this).Add(type, provider);
        }
    }
}
