using System;
using System.Threading;

namespace WcfThreadlessChannel
{
    public sealed class CompletedAsyncResult<T> : IAsyncResult
    {
        public CompletedAsyncResult(T result, AsyncCallback callback, object state)
        {
            Result = result;
            AsyncState = state;
            if (callback != null)
            {
                callback(this);
            }
        }

        public T Result { get; private set; }

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
