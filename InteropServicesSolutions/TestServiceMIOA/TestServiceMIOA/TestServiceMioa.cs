using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestServiceMIOA
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class TestServiceMioa : ITestServiceMioa
    {
        public string GetFullName(string firstName, string lastName)
        {
            if (firstName.Length>15)
                throw new Exception("Должината на името мора да биде помалку 15 карактери");
            return firstName + " " + lastName;
        }

        public int SumOfNums(int first, int second)
        {
            if (first == 0)
                throw new WebException("Ве молиме внесете број поголем од нула");

            if (second == 0)
                throw new FaultException("Ве молиме внесете број поголем од 0");

            return first + second;  
        }
    }
}
