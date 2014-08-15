namespace WcfThreadlessChannel.Tests.Services
{
    public sealed class RequestReplyService : IRequestReplyService
    {
        public string Ping(string text)
        {
            return "Ping: " + text;
        }
    }
}
