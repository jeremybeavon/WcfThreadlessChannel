using System.Collections.Concurrent;
using System.Threading;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessRequestContextQueue : ConcurrentQueue<ThreadlessRequestContext>
    {
        private bool useWaitHandle;

        public ManualResetEvent WaitHandle { get; private set; }

        public bool UseWaitHandle
        {
            get
            {
                return useWaitHandle;
            }

            set
            {
                useWaitHandle = value;
                if (useWaitHandle)
                {
                    if (WaitHandle == null)
                    {
                        WaitHandle = new ManualResetEvent(false);
                    }
                }
                else if (WaitHandle != null)
                {
                    WaitHandle.Dispose();
                    WaitHandle = null;
                }
            }
        }
    }
}
