// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_UJP.GetDataByEMBSResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceCR_UJP
{
  [DebuggerStepThrough]
  [MessageContract(IsWrapped = false)]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class GetDataByEMBSResponse
  {
    [MessageBodyMember(Namespace = "http://wcfwebservicecr.vista.local/WCFServiceUJP.svc", Order = 0)]
    public VratiCRMRezultatiUJPResponse VratiCRMRezultatiUJPResponse;

    public GetDataByEMBSResponse()
    {
    }

    public GetDataByEMBSResponse(VratiCRMRezultatiUJPResponse VratiCRMRezultatiUJPResponse)
    {
      this.VratiCRMRezultatiUJPResponse = VratiCRMRezultatiUJPResponse;
    }
  }
}
