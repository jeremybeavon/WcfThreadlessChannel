using System.ServiceModel;

namespace WcfThreadlessChannel.Tests.Services
{
    public sealed class DuplexService : IDuplexService
    {
        public void Ping(string text)
        {
            OperationContext.Current.GetCallbackChannel<ICallbackService>().Callback("Ping: " + text);
        }
    }
}
