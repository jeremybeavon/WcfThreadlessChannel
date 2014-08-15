using System;
using System.ServiceModel.Channels;
using System.Threading;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessRequestContext : RequestContext, IAsyncResult
    {
        private readonly AsyncCallback asyncCallback;
        private Message request;
        private Message reply;

        public ThreadlessRequestContext(AsyncCallback callback, object state)
        {
            asyncCallback = callback;
            AsyncState = state;
        }

        public override Message RequestMessage
        {
            get { return request; }
        }

        public Message ResponseMessage
        {
            get { return reply; }
        }

        public bool IsCompleted { get; private set; }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotSupportedException(); }
        }

        public object AsyncState { get; private set; }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public Message ExecuteRequest(Message requestMessage)
        {
            request = requestMessage;
            asyncCallback(this);
            return reply;
        }

        public override void Abort()
        {
        }

        public override void Close()
        {
        }

        public override void Close(TimeSpan timeout)
        {
        }

        public override void Reply(Message message)
        {
            reply = message.CreateBufferedCopy(int.MaxValue).CreateMessage();
            IsCompleted = true;
        }

        public override void Reply(Message message, TimeSpan timeout)
        {
            Reply(message);
        }

        public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
        {
            Reply(message);
            return new CompletedAsyncResult(callback, state);
        }

        public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            return BeginReply(message, callback, state);
        }

        public override void EndReply(IAsyncResult result)
        {
        }
    }
}
