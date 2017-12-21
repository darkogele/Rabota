// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.log
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using AjaxControlToolkit;
using interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.AdminPanel
{
  public class log : UserControl
  {
    protected Image Image1;
    protected Label Label1;
    protected DropDownList DropDownListUsers;
    protected Label Label2;
    protected TextBox txtDateFrom;
    protected CalendarExtender calendarButtonExtender;
    protected Label Label3;
    protected TextBox txtDateTo;
    protected CalendarExtender CalendarExtender1;
    protected ImageButton ImageButton1;
    protected GridView LOGGridView;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpSuperLogovi.aspx";
      this.LOGGridView.DataSource = (object) (List<HelpClassLogs>) this.Session["Log"];
      this.LOGGridView.DataBind();
      this.DropDownListUsers.Items.Clear();
      this.DropDownListUsers.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "- - Корисник - -"
      });
      this.DropDownListUsers.DataSource = (object) new UsersDAL().GetAllActiveDeleted(true);
      this.DropDownListUsers.DataBind();
    }

    protected void LOGGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.LOGGridView.PageIndex = e.NewPageIndex;
      this.LOGGridView.DataBind();
    }

    protected void LOGGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
      if (!(e.SortExpression == "Date"))
        return;
      List<HelpClassLogs> list1 = (List<HelpClassLogs>) this.Session["Log"];
      List<HelpClassLogs> list2 = new List<HelpClassLogs>();
      foreach (HelpClassLogs helpClassLogs in (IEnumerable<HelpClassLogs>) Enumerable.OrderByDescending<HelpClassLogs, DateTime>((IEnumerable<HelpClassLogs>) list1, (Func<HelpClassLogs, DateTime>) (p => p.Date)))
        list2.Add(helpClassLogs);
      if (list2[0].ID == list1[0].ID && list2[list2.Count - 1].ID == list1[list1.Count - 1].ID)
      {
        list2.Clear();
        foreach (HelpClassLogs helpClassLogs in (IEnumerable<HelpClassLogs>) Enumerable.OrderBy<HelpClassLogs, DateTime>((IEnumerable<HelpClassLogs>) list1, (Func<HelpClassLogs, DateTime>) (p => p.Date)))
          list2.Add(helpClassLogs);
      }
      this.Session["Log"] = (object) list2;
      this.LOGGridView.DataSource = (object) list2;
      this.LOGGridView.DataBind();
    }

    protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      List<HelpClassLogs> list = new List<HelpClassLogs>();
      if (this.DropDownListUsers.SelectedValue != "0")
        list = new LOGDAL().GetTop50ByUser(new UsersDAL().GetByID(new Guid(this.DropDownListUsers.SelectedValue.ToString())));
      this.Session["Log"] = (object) list;
      this.LOGGridView.DataSource = (object) list;
      this.LOGGridView.DataBind();
    }

    protected void ButtonDate_Click(object sender, EventArgs e)
    {
      List<HelpClassLogs> list1;
      if (this.DropDownListUsers.SelectedItem.Text == "- - Корисник - -")
      {
        List<HelpClassLogs> list2 = new LOGDAL().GetTop50();
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        try
        {
          nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
        }
        catch
        {
        }
        try
        {
          nullable2 = new DateTime?(Convert.ToDateTime(this.txtDateTo.Text).AddHours(24.0));
        }
        catch
        {
        }
        if (nullable1.HasValue && nullable2.HasValue)
        {
          list1 = (List<HelpClassLogs>) null;
          list2 = new LOGDAL().GetByDate(nullable1.Value, nullable2.Value);
        }
        this.Session["Log"] = (object) list2;
        this.LOGGridView.DataSource = (object) list2;
        this.LOGGridView.DataBind();
      }
      else
      {
        USER byId = new UsersDAL().GetByID(new Guid(this.DropDownListUsers.SelectedValue.ToString()));
        List<HelpClassLogs> list2 = new LOGDAL().GetTop50ByUser(byId);
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        try
        {
          nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
        }
        catch
        {
        }
        try
        {
          nullable2 = new DateTime?(Convert.ToDateTime(this.txtDateTo.Text));
        }
        catch
        {
        }
        if (nullable1.HasValue && nullable2.HasValue)
        {
          list1 = (List<HelpClassLogs>) null;
          list2 = new LOGDAL().GetByUserForDate(byId, nullable1.Value, nullable2.Value);
        }
        this.Session["Log"] = (object) list2;
        this.LOGGridView.DataSource = (object) list2;
        this.LOGGridView.DataBind();
      }
    }
  }
}
