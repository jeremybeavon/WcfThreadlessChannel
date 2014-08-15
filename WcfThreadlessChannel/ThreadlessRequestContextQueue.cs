using System.Collections.Concurrent;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessRequestContextQueue : ConcurrentQueue<ThreadlessRequestContext>
    {
    }
}
