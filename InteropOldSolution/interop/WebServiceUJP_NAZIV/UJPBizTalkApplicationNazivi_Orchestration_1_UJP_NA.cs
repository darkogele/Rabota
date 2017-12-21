// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceUJP_NAZIV
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [ServiceContract(ConfigurationName = "WebServiceUJP_NAZIV.UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT")]
  public interface UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT
  {
    [OperationContract(Action = "GetFirmiNaziv", ReplyAction = "*")]
    [XmlSerializerFormat]
    GetFirmiNazivResponse GetFirmiNaziv(GetFirmiNazivRequest request);
  }
}
