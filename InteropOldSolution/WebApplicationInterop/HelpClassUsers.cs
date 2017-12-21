// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.HelpClassUsers
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;

namespace WebApplicationInterop
{
  public class HelpClassUsers
  {
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public long ID_Cert { get; set; }

    public string NameSurname { get; set; }

    public string Email { get; set; }

    public string ip { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedOn { get; set; }

    public string user { get; set; }

    public string pass { get; set; }

    public int Type { get; set; }

    public USER UserObj { get; set; }

    public long IDInstitution { get; set; }

    public string InstitutionName { get; set; }

    public List<PERMISSION> PermissionList { get; set; }
  }
}
