using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace GettingStartedLib
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IFridge
    {

        [OperationContract]
        int GetFruitTotal();

        [OperationContract]
        int AddFruit(string fruit);

        [OperationContract]
        int TakeFruit(string fruit);

        [OperationContract]
        List<string> FridgeContents();
    }
}