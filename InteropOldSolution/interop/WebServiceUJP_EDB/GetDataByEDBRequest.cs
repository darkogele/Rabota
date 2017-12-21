// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.GetDataByEDBRequest
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
  public class GetDataByEDBRequest
  {
    [MessageBodyMember(Namespace = "http://UJPBizTalkApplicationEDB.EDB", Order = 0)]
    public EDB EDB;

    public GetDataByEDBRequest()
    {
    }

    public GetDataByEDBRequest(EDB EDB)
    {
      this.EDB = EDB;
    }
  }
}
