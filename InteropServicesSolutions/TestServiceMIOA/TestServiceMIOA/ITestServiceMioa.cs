using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestServiceMIOA
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ITestServiceMioa
    {
        [OperationContract]
        string GetFullName(string firstName, string lastName);

        [OperationContract]
        int SumOfNums(int first, int second);

        // TODO: Add your service operations here
    }
}
