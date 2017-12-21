// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_UJP.Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceCR_UJP
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient : ClientBase<Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT>, Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT
  {
    public Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient()
    {
    }

    public Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetDataByEMBSResponse Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT.GetDataByEMBS(GetDataByEMBSRequest request)
    {
      return this.Channel.GetDataByEMBS(request);
    }

    public VratiCRMRezultatiUJPResponse GetDataByEMBS(EMBS EMBS)
    {
      return ((Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT) this).GetDataByEMBS(new GetDataByEMBSRequest()
      {
        EMBS = EMBS
      }).VratiCRMRezultatiUJPResponse;
    }
  }
}
