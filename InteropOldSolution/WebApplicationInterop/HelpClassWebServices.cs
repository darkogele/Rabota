// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.HelpClassWebServices
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;

namespace WebApplicationInterop
{
  public class HelpClassWebServices
  {
    public long ID { get; set; }

    public string Tittle { get; set; }

    public string Description { get; set; }

    public string Note { get; set; }

    public string URL { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedOn { get; set; }

    public WEBSERVICE WSObj { get; set; }

    public long IDInstitution { get; set; }

    public string InstitutionName { get; set; }

    public List<PERMISSION> PermissionList { get; set; }
  }
}
