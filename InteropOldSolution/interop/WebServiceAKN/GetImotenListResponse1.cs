// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.GetImotenListResponse1
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceAKN
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  [MessageContract(IsWrapped = false)]
  public class GetImotenListResponse1
  {
    [MessageBodyMember(Namespace = "http://wcfwebserviceakn.vista.local/WCFServiceAKN.svc", Order = 0)]
    public GetImotenListResponse GetImotenListResponse;

    public GetImotenListResponse1()
    {
    }

    public GetImotenListResponse1(GetImotenListResponse GetImotenListResponse)
    {
      this.GetImotenListResponse = GetImotenListResponse;
    }
  }
}
