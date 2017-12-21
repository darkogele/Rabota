// Decompiled with JetBrains decompiler
// Type: interop.UserPanel.indexu
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace interop.UserPanel
{
  public class indexu : UserControl
  {
    protected Image Image1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu3"] = (object) "Help/korisnik/Help_pocetna_obicen.aspx";
    }
  }
}
