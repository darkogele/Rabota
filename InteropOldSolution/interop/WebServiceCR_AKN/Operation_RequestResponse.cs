// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_AKN.Operation_RequestResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceCR_AKN
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class Operation_RequestResponse
  {
    [MessageBodyMember(Namespace = "http://wcfwebservicecr.vista.local/WCFServiceAKN.svc", Order = 0)]
    public VratiCRMRezultatiAKNResponse VratiCRMRezultatiAKNResponse;

    public Operation_RequestResponse()
    {
    }

    public Operation_RequestResponse(VratiCRMRezultatiAKNResponse VratiCRMRezultatiAKNResponse)
    {
      this.VratiCRMRezultatiAKNResponse = VratiCRMRezultatiAKNResponse;
    }
  }
}
