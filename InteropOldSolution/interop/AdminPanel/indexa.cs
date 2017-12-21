// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.indexa
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace interop.AdminPanel
{
  public class indexa : UserControl
  {
    protected Image Image1;
    protected Image Image2;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton2;
    protected LinkButton LinkButton3;
    protected LinkButton LinkButton4;
    protected LinkButton LinkButton5;
    protected Image Image3;
    protected LinkButton LinkButton6;
    protected LinkButton LinkButton7;
    protected LinkButton LinkButton8;
    protected LinkButton LinkButton9;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/Help_pocetna_super.aspx";
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
      this.Session["AdminInstitution"] = (object) " ";
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
      this.Session["AdminSelectedWS"] = (object) null;
      this.Session["AdminWS"] = (object) " ";
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
      this.Session["AdminUser"] = (object) "";
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkButton9_Click(object sender, EventArgs e)
    {
      this.Session["AdminPermisions"] = (object) "";
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("http://www.mvr.gov.mk/DesktopDefault.aspx");
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("http://www.ujp.gov.mk/");
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("http://www.crm.com.mk/");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("http://www.customs.gov.mk/DesktopDefault.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("http://www.uslugi.gov.mk/ListaUslugi.aspx");
    }
  }
}
