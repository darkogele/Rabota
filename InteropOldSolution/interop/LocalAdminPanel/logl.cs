// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.logl
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

namespace interop.LocalAdminPanel
{
  public class logl : UserControl
  {
    protected Image Image1;
    protected Label Label2;
    protected TextBox txtDateFrom;
    protected Label Label3;
    protected TextBox txtDateTo;
    protected CalendarExtender CalendarExtender1;
    protected ImageButton ImageButton1;
    protected CalendarExtender calendarButtonExtender;
    protected GridView LOGGridView;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/HelpAdminLogovi.aspx";
      this.LOGGridView.DataSource = (object) (List<HelpClassLogs>) this.Session["Log"];
      this.LOGGridView.DataBind();
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

    protected void ButtonDate_Click(object sender, EventArgs e)
    {
      USER user = (USER) this.Session["user"];
      List<HelpClassLogs> list = new LOGDAL().GetTop50ByAdminInstitution(user);
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
        DateTime dateTime = Convert.ToDateTime(this.txtDateTo.Text);
        dateTime = dateTime.AddHours(24.0);
        nullable2 = new DateTime?(dateTime.AddMilliseconds(-1.0));
      }
      catch
      {
      }
      if (nullable1.HasValue && nullable2.HasValue)
        list = new LOGDAL().GetAllByAdminInstitutionForDate(user, nullable1.Value, nullable2.Value);
      this.Session["Log"] = (object) list;
      this.LOGGridView.DataSource = (object) list;
      this.LOGGridView.DataBind();
    }

    protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
  }
}
