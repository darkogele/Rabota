// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.GetFirmiNazivRequest
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceUJP_NAZIV
{
  [MessageContract(IsWrapped = false)]
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class GetFirmiNazivRequest
  {
    [MessageBodyMember(Namespace = "http://UJPBizTalkApplicationNazivi.FirmiNaziv", Order = 0)]
    public FirmiNaziv FirmiNaziv;

    public GetFirmiNazivRequest()
    {
    }

    public GetFirmiNazivRequest(FirmiNaziv FirmiNaziv)
    {
      this.FirmiNaziv = FirmiNaziv;
    }
  }
}
