// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.searchresulta
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.AdminPanel
{
  public class searchresulta : UserControl
  {
    protected Image Image1;
    protected Panel PanelSearch;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/Help_pocetna_super.aspx";
    }

    public void FillList(List<SearchObjects> ListSearches)
    {
      foreach (SearchObjects searchObjects in ListSearches)
      {
        LinkButton linkButton = new LinkButton();
        linkButton.Width = (Unit) 800;
        linkButton.ID = searchObjects.RezultID.ToString() + ";" + searchObjects.ResultSession.ToString();
        linkButton.Text = searchObjects.Rezult;
        linkButton.Click += new EventHandler(this.LinkSearch_Click);
        this.PanelSearch.Controls.Add((Control) linkButton);
        Label label1 = new Label();
        label1.Width = (Unit) 800;
        label1.Height = (Unit) 30;
        Label label2 = label1;
        label2.Text = searchObjects.Description;
        this.PanelSearch.Controls.Add((Control) label2);
      }
    }

    protected void PanelSearch_Load(object sender, EventArgs e)
    {
      this.FillList((List<SearchObjects>) this.Session["SelectedSearchList"]);
    }

    protected void LinkSearch_Click(object sender, EventArgs e)
    {
      string[] parametars = ((Control) sender).ID.Split(';');
      object obj = Enumerable.First<object>(Enumerable.Select<SearchObjects, object>(Enumerable.Where<SearchObjects>((IEnumerable<SearchObjects>) this.Session["SelectedSearchList"], (Func<SearchObjects, bool>) (p => p.RezultID == Convert.ToInt64(parametars[0]))), (Func<SearchObjects, object>) (p => p.Obj)));
      USER user = (USER) this.Session["user"];
      if (user == null || user.Type != 1)
        return;
      this.Session[parametars[1]] = obj;
      this.Response.Redirect("Default.aspx");
    }
  }
}
