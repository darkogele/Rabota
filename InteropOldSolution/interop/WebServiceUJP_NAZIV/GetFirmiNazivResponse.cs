﻿// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.GetFirmiNazivResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace interop.WebServiceUJP_NAZIV
{
  [DebuggerStepThrough]
  [MessageContract(IsWrapped = false)]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public class GetFirmiNazivResponse
  {
    [MessageBodyMember(Namespace = "http://ujpwcfserviceapplication.interop.local/GetNaziviFirmiByNaziv.svc", Order = 0)]
    public GetPodatociFirmiNaziviByNazivResponse GetPodatociFirmiNaziviByNazivResponse;

    public GetFirmiNazivResponse()
    {
    }

    public GetFirmiNazivResponse(GetPodatociFirmiNaziviByNazivResponse GetPodatociFirmiNaziviByNazivResponse)
    {
      this.GetPodatociFirmiNaziviByNazivResponse = GetPodatociFirmiNaziviByNazivResponse;
    }
  }
}
