// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.GetDataByEDBResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceUJP_EDB
{
  [MessageContract(IsWrapped = false)]
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class GetDataByEDBResponse
  {
    [MessageBodyMember(Namespace = "http://ujpwcfserviceapplication.interop.local/GetOsnovniPodatociByEDB.svc", Order = 0)]
    public GetPodatociByEDBResponse GetPodatociByEDBResponse;

    public GetDataByEDBResponse()
    {
    }

    public GetDataByEDBResponse(GetPodatociByEDBResponse GetPodatociByEDBResponse)
    {
      this.GetPodatociByEDBResponse = GetPodatociByEDBResponse;
    }
  }
}
