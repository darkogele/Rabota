// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_CU.Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceCR_CU
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  public class Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient : ClientBase<Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP>, Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP
  {
    public Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient()
    {
    }

    public Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    Operation_RequestResponse Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP.Operation_Request(Operation_RequestRequest request)
    {
      return this.Channel.Operation_Request(request);
    }

    public VratiCRMRezultatiCUResponse Operation_Request(EMBS EMBS)
    {
      return ((Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP) this).Operation_Request(new Operation_RequestRequest()
      {
        EMBS = EMBS
      }).VratiCRMRezultatiCUResponse;
    }
  }
}
