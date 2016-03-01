using System;
using System.ServiceModel;

namespace MobileWCF.Contracts
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetSum(int a, int b, AsyncCallback callback, object state);
        string EndGetSum(IAsyncResult asyncResult);
    }
}
