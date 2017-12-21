// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.Operation_RequestResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceAKNParceli
{
  [MessageContract(IsWrapped = false)]
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class Operation_RequestResponse
  {
    [MessageBodyMember(Namespace = "http://wcfwebserviceakn.vista.local/WCFServeiceAKNParcela.svc", Order = 0)]
    public GetPodatociStrukturaParcelaResponse GetPodatociStrukturaParcelaResponse;

    public Operation_RequestResponse()
    {
    }

    public Operation_RequestResponse(GetPodatociStrukturaParcelaResponse GetPodatociStrukturaParcelaResponse)
    {
      this.GetPodatociStrukturaParcelaResponse = GetPodatociStrukturaParcelaResponse;
    }
  }
}
