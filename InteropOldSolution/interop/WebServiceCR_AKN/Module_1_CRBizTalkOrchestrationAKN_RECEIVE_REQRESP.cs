// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_AKN.Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORT
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceCR_AKN
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [ServiceContract(ConfigurationName = "WebServiceCR_AKN.Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORT")]
  public interface Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORT
  {
    [XmlSerializerFormat]
    [OperationContract(Action = "Operation_Request", ReplyAction = "*")]
    Operation_RequestResponse Operation_Request(Operation_RequestRequest request);
  }
}
