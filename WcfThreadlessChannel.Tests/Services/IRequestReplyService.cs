using System.ServiceModel;

namespace WcfThreadlessChannel.Tests.Services
{
    [ServiceContract]
    public interface IRequestReplyService
    {
        [OperationContract]
        string Ping(string text);
    }
}
