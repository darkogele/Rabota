// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceUJP_NAZIV
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  public class UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient : ClientBase<UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT>, UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT
  {
    public UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient()
    {
    }

    public UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetFirmiNazivResponse UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT.GetFirmiNaziv(GetFirmiNazivRequest request)
    {
      return this.Channel.GetFirmiNaziv(request);
    }

    public GetPodatociFirmiNaziviByNazivResponse GetFirmiNaziv(FirmiNaziv FirmiNaziv)
    {
      return ((UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT) this).GetFirmiNaziv(new GetFirmiNazivRequest()
      {
        FirmiNaziv = FirmiNaziv
      }).GetPodatociFirmiNaziviByNazivResponse;
    }
  }
}
