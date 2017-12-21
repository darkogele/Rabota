// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceAKNParceli
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient : ClientBase<Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT>, Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT
  {
    public Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient()
    {
    }

    public Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    Operation_RequestResponse Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT.Operation_Request(Operation_RequestRequest request)
    {
      return this.Channel.Operation_Request(request);
    }

    public GetPodatociStrukturaParcelaResponse Operation_Request(ParceliSchema ParceliSchema)
    {
      return ((Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT) this).Operation_Request(new Operation_RequestRequest()
      {
        ParceliSchema = ParceliSchema
      }).GetPodatociStrukturaParcelaResponse;
    }
  }
}
