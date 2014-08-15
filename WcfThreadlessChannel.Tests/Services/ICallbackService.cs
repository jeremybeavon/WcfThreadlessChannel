using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfThreadlessChannel.Tests.Services
{
    [ServiceContract]
    public interface ICallbackService
    {
        [OperationContract(IsOneWay = true)]
        void Callback(string text);
    }
}
