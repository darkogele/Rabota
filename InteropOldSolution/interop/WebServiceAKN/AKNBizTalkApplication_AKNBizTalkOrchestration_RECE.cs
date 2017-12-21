// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceAKN
{
  [ServiceContract(ConfigurationName = "WebServiceAKN.AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP")]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public interface AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP
  {
    [XmlSerializerFormat]
    [OperationContract(Action = "GetImotenList", ReplyAction = "*")]
    GetImotenListResponse1 GetImotenList(GetImotenListRequest request);
  }
}
