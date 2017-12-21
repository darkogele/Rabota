// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.GetImotenListRequest
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceAKN
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class GetImotenListRequest
  {
    [MessageBodyMember(Namespace = "http://AKNBizTalkApplication.ImotenListSchema", Order = 0)]
    public ImotenListSchema ImotenListSchema;

    public GetImotenListRequest()
    {
    }

    public GetImotenListRequest(ImotenListSchema ImotenListSchema)
    {
      this.ImotenListSchema = ImotenListSchema;
    }
  }
}
