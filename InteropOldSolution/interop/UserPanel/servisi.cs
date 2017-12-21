// Decompiled with JetBrains decompiler
// Type: interop.UserPanel.servisi
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

namespace interop.UserPanel
{
  public class servisi : UserControl
  {
    protected Image Image1;
    protected TextBox TextBox1;
    protected DropDownList DropDownList3;
    protected GridView WSGridView;
    protected Label Label11;
    protected Panel PanelInfoParams;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu3"] = (object) "Help/korisnik/Help_servisi_obicen.aspx";
      List<HelpClassWebServices> list = (List<HelpClassWebServices>) this.Session["PanelWSHelp"];
      this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Session["PanelWSHelp"];
      this.WSGridView.DataBind();
      string selectedValue = this.DropDownList3.SelectedValue;
      this.DropDownList3.Items.Clear();
      this.DropDownList3.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Листај по институција --"
      });
      this.DropDownList3.DataSource = (object) (List<INSTITUTION>) this.Session["PanelInstitutions"];
      this.DropDownList3.DataBind();
      this.DropDownList3.SelectedValue = selectedValue;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      this.DropDownList3.SelectedIndex = 0;
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      List<HelpClassWebServices> list = new List<HelpClassWebServices>();
      IEnumerable<HelpClassWebServices> source = Enumerable.Where<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, bool>) (p => p.Tittle.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
      if (Enumerable.Count<HelpClassWebServices>(source) != 0)
      {
        list.Clear();
        foreach (HelpClassWebServices classWebServices in source)
          list.Add(classWebServices);
      }
      if (list.Count == 0)
        list.AddRange((IEnumerable<HelpClassWebServices>) servicesPermissions);
      this.Session["PanelWSHelp"] = (object) list;
      this.WSGridView.DataSource = (object) list;
      this.WSGridView.DataBind();
    }

    protected void WSGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.WSGridView.PageIndex = e.NewPageIndex;
      this.WSGridView.DataBind();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TextBox1.Text = "";
      if (this.DropDownList3.SelectedValue != "0")
      {
        long num = Convert.ToInt64(this.DropDownList3.SelectedValue);
        List<HelpClassWebServices> list1 = (List<HelpClassWebServices>) this.Session["PanelWSHelp"];
        List<HelpClassWebServices> list2 = new List<HelpClassWebServices>();
        foreach (HelpClassWebServices classWebServices in list1)
        {
          if (classWebServices.IDInstitution == num)
            list2.Add(classWebServices);
        }
        this.Session["PanelWSHelp"] = (object) list2;
        this.WSGridView.DataSource = (object) list2;
        this.WSGridView.DataBind();
      }
      else
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
        this.Session["PanelWSHelp"] = (object) servicesPermissions;
        this.WSGridView.DataSource = (object) servicesPermissions;
        this.WSGridView.DataBind();
      }
    }

    protected void WSGridView_SelectedIndexChanged1(object sender, EventArgs e)
    {
      long row = Convert.ToInt64(this.WSGridView.SelectedDataKey.Value);
      this.Session["SelectedWS"] = (object) Enumerable.Single<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) this.Session["PanelWSHelp"], (Func<HelpClassWebServices, bool>) (p => p.ID == row));
      this.Response.Redirect("Default.aspx");
    }

    protected void WSGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      HelpClassWebServices classWebServices = (HelpClassWebServices) e.Row.DataItem;
      if (classWebServices != null)
      {
        imageButton.Attributes.Add("1", classWebServices.ID.ToString());
        imageButton.CommandName = "InfoWS";
      }
    }

    protected void WSGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "InfoWS"))
        return;
      long selectedidWS = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
      HelpClassWebServices classWebServices = Enumerable.Single<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) this.Session["PanelWSHelp"], (Func<HelpClassWebServices, bool>) (p => p.ID == selectedidWS));
      List<PARAM> paramsForWebservice = new ParamsDAL().GetParamsForWebservice(classWebServices.WSObj);
      this.Label11.Text = classWebServices.Tittle + ": " + classWebServices.Description + Environment.NewLine;
      if (paramsForWebservice.Count != 0)
      {
        foreach (PARAM obj in paramsForWebservice)
        {
          Label label1 = new Label();
          label1.Text = obj.Tittle + " -- ";
          label1.Font.Bold = true;
          Label label2 = new Label();
          label2.Text = obj.Description;
          LiteralControl literalControl1 = new LiteralControl("<p>");
          LiteralControl literalControl2 = new LiteralControl("</p>");
          LiteralControl literalControl3 = new LiteralControl("<br />");
          this.PanelInfoParams.Controls.Add((Control) literalControl1);
          this.PanelInfoParams.Controls.Add((Control) label1);
          this.PanelInfoParams.Controls.Add((Control) label2);
          this.PanelInfoParams.Controls.Add((Control) literalControl2);
          this.PanelInfoParams.Controls.Add((Control) literalControl3);
        }
      }
    }

    protected void WSGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
      this.TextBox1.Text = "";
      if (this.DropDownList3.SelectedValue != "0")
      {
        long num = Convert.ToInt64(this.DropDownList3.SelectedValue);
        List<HelpClassWebServices> list1 = (List<HelpClassWebServices>) this.Session["PanelWSHelp"];
        List<HelpClassWebServices> list2 = new List<HelpClassWebServices>();
        foreach (HelpClassWebServices classWebServices in list1)
        {
          if (classWebServices.IDInstitution == num)
            list2.Add(classWebServices);
        }
        this.Session["PanelWSHelp"] = (object) list2;
      }
      List<HelpClassWebServices> list3 = (List<HelpClassWebServices>) this.Session["PanelWSHelp"];
      List<HelpClassWebServices> list4 = new List<HelpClassWebServices>();
      if (!(e.SortExpression == "FirstName"))
        return;
      if (this.Session["sort"] == "ascending" || this.Session["sort"] == null)
      {
        foreach (HelpClassWebServices classWebServices in (IEnumerable<HelpClassWebServices>) Enumerable.OrderBy<HelpClassWebServices, string>((IEnumerable<HelpClassWebServices>) list3, (Func<HelpClassWebServices, string>) (p => p.Tittle)))
          list4.Add(classWebServices);
        this.Session["PanelWSHelp"] = (object) list4;
        this.WSGridView.DataSource = (object) list4;
        this.WSGridView.DataBind();
        this.Session["sort"] = (object) "descending";
      }
      else
      {
        foreach (HelpClassWebServices classWebServices in (IEnumerable<HelpClassWebServices>) Enumerable.OrderByDescending<HelpClassWebServices, string>((IEnumerable<HelpClassWebServices>) list3, (Func<HelpClassWebServices, string>) (p => p.Tittle)))
          list4.Add(classWebServices);
        this.Session["PanelWSHelp"] = (object) list4;
        this.WSGridView.DataSource = (object) list4;
        this.WSGridView.DataBind();
        this.Session["sort"] = (object) "ascending";
      }
    }
  }
}
