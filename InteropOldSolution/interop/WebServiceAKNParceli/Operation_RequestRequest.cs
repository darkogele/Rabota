// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.Operation_RequestRequest
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceAKNParceli
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class Operation_RequestRequest
  {
    [MessageBodyMember(Namespace = "http://AKNParceliBizTalkApplication.ParceliSchema", Order = 0)]
    public ParceliSchema ParceliSchema;

    public Operation_RequestRequest()
    {
    }

    public Operation_RequestRequest(ParceliSchema ParceliSchema)
    {
      this.ParceliSchema = ParceliSchema;
    }
  }
}
