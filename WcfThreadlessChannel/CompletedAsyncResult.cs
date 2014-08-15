using System;
using System.Threading;

namespace WcfThreadlessChannel
{
    public sealed class CompletedAsyncResult : IAsyncResult
    {
        public CompletedAsyncResult(AsyncCallback callback, object state)
        {
            AsyncState = state;
            if (callback != null)
            {
                callback(this);
            }
        }

        public bool IsCompleted
        {
            get { return true; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public object AsyncState { get; private set; }

        public bool CompletedSynchronously
        {
            get { return true; }
        }
    }
}
