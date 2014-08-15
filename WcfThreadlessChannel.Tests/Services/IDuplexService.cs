using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfThreadlessChannel.Tests.Services
{
    [ServiceContract(CallbackContract = typeof(ICallbackService))]
    public interface IDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(string text);
    }
}
