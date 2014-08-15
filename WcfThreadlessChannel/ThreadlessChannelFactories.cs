using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WcfThreadlessChannel
{
    internal sealed class ThreadlessChannelFactories : ConcurrentDictionary<Type, ThreadlessChannelFactoryProvider>
    {
        public void Add(Type type, ThreadlessChannelFactoryProvider provider)
        {
            ((IDictionary<Type, ThreadlessChannelFactoryProvider>)this).Add(type, provider);
        }
    }
}
