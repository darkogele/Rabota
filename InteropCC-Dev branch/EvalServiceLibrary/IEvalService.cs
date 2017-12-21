using System.Collections.Generic;
using System.ServiceModel;

namespace EvalServiceLibrary
{
    [ServiceContract]
    public interface IEvalService
    {
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        //[XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        List<Eval> GetEvals();
        [OperationContract]
        //[XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Literal)]
        void RemoveEval(string id);
    }
}
