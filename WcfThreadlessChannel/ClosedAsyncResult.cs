using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WcfThreadlessChannel
{
    public sealed class ClosedAsyncResult : IAsyncResult
    {
        public ClosedAsyncResult(object state)
        {
            AsyncState = state;
        }

        public object AsyncState { get; private set; }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotSupportedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return false; }
        }
    }
}
