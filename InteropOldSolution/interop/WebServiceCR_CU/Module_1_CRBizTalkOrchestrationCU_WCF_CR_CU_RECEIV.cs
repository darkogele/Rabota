// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_CU.Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceCR_CU
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [ServiceContract(ConfigurationName = "WebServiceCR_CU.Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP")]
  public interface Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP
  {
    [OperationContract(Action = "Operation_Request", ReplyAction = "*")]
    [XmlSerializerFormat]
    Operation_RequestResponse Operation_Request(Operation_RequestRequest request);
  }
}
