// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.indexl
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace interop.LocalAdminPanel
{
  public class indexl : UserControl
  {
    protected Image Image1;
    protected Image Image2;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton2;
    protected LinkButton LinkButton3;
    protected LinkButton LinkButton4;
    protected LinkButton LinkButton5;
    protected Image Image3;
    protected LinkButton LinkButton8;
    protected LinkButton LinkButton9;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/Help_Pocetna_Admin.aspx";
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
      this.Session["LocalUser"] = (object) "";
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkButton9_Click(object sender, EventArgs e)
    {
      this.Session["LocalPermisions"] = (object) "";
      this.Response.Redirect("Default.aspx");
    }
  }
}
