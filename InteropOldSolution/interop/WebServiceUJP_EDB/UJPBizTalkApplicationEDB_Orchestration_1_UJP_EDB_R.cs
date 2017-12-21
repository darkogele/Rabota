// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceUJP_EDB
{
  [ServiceContract(ConfigurationName = "WebServiceUJP_EDB.UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT")]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public interface UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT
  {
    [OperationContract(Action = "GetDataByEDB", ReplyAction = "*")]
    [XmlSerializerFormat]
    GetDataByEDBResponse GetDataByEDB(GetDataByEDBRequest request);
  }
}
