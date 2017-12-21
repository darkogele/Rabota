// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.GradComparerByName
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.Collections.Generic;

namespace interop.AdminPanel
{
  internal class GradComparerByName : IComparer<KatOpstini>
  {
    public int Compare(KatOpstini Grad1, KatOpstini Grad2)
    {
      return string.Compare(Grad1.Naziv, Grad2.Naziv);
    }
  }
}
