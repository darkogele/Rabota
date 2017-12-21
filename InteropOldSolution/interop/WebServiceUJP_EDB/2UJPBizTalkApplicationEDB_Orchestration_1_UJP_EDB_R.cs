// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceUJP_EDB
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient : ClientBase<UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT>, UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT
  {
    public UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient()
    {
    }

    public UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetDataByEDBResponse UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.GetDataByEDB(GetDataByEDBRequest request)
    {
      return this.Channel.GetDataByEDB(request);
    }

    public GetPodatociByEDBResponse GetDataByEDB(EDB EDB)
    {
      return ((UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT) this).GetDataByEDB(new GetDataByEDBRequest()
      {
        EDB = EDB
      }).GetPodatociByEDBResponse;
    }
  }
}
