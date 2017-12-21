// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceAKN
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  public class AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient : ClientBase<AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP>, AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP
  {
    public AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient()
    {
    }

    public AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetImotenListResponse1 AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP.GetImotenList(GetImotenListRequest request)
    {
      return this.Channel.GetImotenList(request);
    }

    public GetImotenListResponse GetImotenList(ImotenListSchema ImotenListSchema)
    {
      return ((AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP) this).GetImotenList(new GetImotenListRequest()
      {
        ImotenListSchema = ImotenListSchema
      }).GetImotenListResponse;
    }
  }
}
