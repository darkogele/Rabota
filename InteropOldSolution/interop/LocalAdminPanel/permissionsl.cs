// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.permissionsl
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

namespace interop.LocalAdminPanel
{
  public class permissionsl : UserControl
  {
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView IGridView;
    protected LinkButton btnUsage;
    protected Label Label3;
    protected Label lblinfo;
    protected GridView Gridview1;
    protected CheckBoxList CheckBoxListUser;
    protected Panel PanelInfoInst;
    protected Label Label2;
    protected Label lblDescr;
    protected Label lbl1;
    protected Label lblName;
    protected Label lblURL;
    protected Label Label11;
    protected Label lblstatus;
    protected LinkButton LinkButtonPromeni;

    private void DisableTxtBox()
    {
    }

    private void ClearTxtBox()
    {
      this.lblName.Text = "";
      this.lblDescr.Text = "";
      this.lblURL.Text = "";
      this.Label11.Text = "";
    }

    private void ClearStyleSubmeni()
    {
    }

    private void ClearStylePermbmeni()
    {
      this.btnUsage.CssClass = "none";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/HelpAdminPrivilegii.aspx";
      this.IGridView.DataSource = (object) (List<HelpClassUsers>) this.Application["ListUsersPermissionsPerm"];
      this.IGridView.DataBind();
      if (this.Session["ListWSAll"] != null)
      {
        if (((List<WEBSERVICE>) this.Session["ListWSAll"]).Count == 0)
        {
          this.lblinfo.Text = "Nema podatoci";
          this.ClearStylePermbmeni();
        }
        else
          this.lblinfo.Text = "";
      }
      else
      {
        this.lblinfo.Text = "Nema podatoci";
        this.ClearStylePermbmeni();
      }
      this.btnUsage.CssClass = "default-tab current";
      this.Gridview1.DataSource = (object) (List<WEBSERVICE>) this.Session["ListWSAll"];
      this.Gridview1.DataBind();
      if (this.Session["LocalPermisions"] == null)
        return;
      this.Session["LocalPermisions"] = (object) null;
    }

    protected void IGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      Guid row = (Guid) this.IGridView.SelectedDataKey.Value;
      HelpClassUsers helpClassUsers = Enumerable.Single<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissionsPerm"], (Func<HelpClassUsers, bool>) (p => p.ID == row));
      this.Application["UserPermission"] = (object) helpClassUsers;
      this.Application["SelectedUser"] = (object) row;
      List<WEBSERVICE> list1 = new List<WEBSERVICE>();
      new InstitutionsDAL().GetByID(helpClassUsers.IDInstitution);
      List<WEBSERVICE> list2 = (List<WEBSERVICE>) this.Application["ListWebServices"];
      this.CheckBoxListUser.DataSource = (object) list2;
      this.CheckBoxListUser.DataBind();
      this.Session["ListWSAll"] = (object) list2;
      foreach (PERMISSION permission in helpClassUsers.PermissionList)
      {
        for (int index = 0; index < this.CheckBoxListUser.Items.Count; ++index)
        {
          if (this.CheckBoxListUser.Items[index].Value == permission.WEBSERVICE.ID.ToString())
          {
            this.CheckBoxListUser.Items[index].Selected = true;
            break;
          }
        }
      }
      foreach (PERMISSION permission in new PermissionsDAL().GetPermisionsByUser(new UsersDAL().GetByID(helpClassUsers.ID)))
      {
        if (permission.Active)
          list1.Add(permission.WEBSERVICE);
      }
      this.Session["ListWSUser"] = (object) list1;
      this.lblName.Text = helpClassUsers.NameSurname;
      this.lblDescr.Text = helpClassUsers.InstitutionName;
      this.lblstatus.Text = !helpClassUsers.Active ? "Неактивен" : "Активен";
      this.DisableTxtBox();
      this.ClearStylePermbmeni();
      this.lblinfo.Text = "";
      if (this.CheckBoxListUser.Visible)
        this.ClearStylePermbmeni();
      this.btnUsage.CssClass = "default-tab current";
      this.PanelInfoInst.Visible = true;
      this.Gridview1.DataSource = (object) list2;
      this.Gridview1.DataBind();
      this.Gridview1.Visible = true;
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<INSTITUTION> list = (List<INSTITUTION>) this.Application["DropDownInstitutions"];
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, list[0]);
      permissionsByInstitution.AddRange((IEnumerable<HelpClassUsers>) new UsersDAL().GetUsersPermissionsByInstitution(false, list[0]));
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.IGridView.DataSource = (object) permissionsByInstitution;
      this.IGridView.DataBind();
      this.IGridView.SelectedIndex = -1;
      this.CheckBoxListUser.Items.Clear();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, ((List<INSTITUTION>) this.Application["DropDownInstitutions"])[0]);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.IGridView.DataSource = (object) permissionsByInstitution;
      this.IGridView.DataBind();
      this.IGridView.SelectedIndex = -1;
      this.CheckBoxListUser.Items.Clear();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(false, ((List<INSTITUTION>) this.Application["DropDownInstitutions"])[0]);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.IGridView.DataSource = (object) permissionsByInstitution;
      this.IGridView.DataBind();
      this.IGridView.SelectedIndex = -1;
      this.CheckBoxListUser.Items.Clear();
      this.ClearStyleSubmeni();
    }

    protected void btnUsage_Click(object sender, EventArgs e)
    {
      this.CheckBoxListUser.Visible = true;
    }

    protected void btnOwner_Click(object sender, EventArgs e)
    {
      this.CheckBoxListUser.Visible = false;
    }

    protected void LinkButtonPromeni_Click(object sender, EventArgs e)
    {
      bool flag = false;
      for (int index = 0; index < this.CheckBoxListUser.Items.Count; ++index)
      {
        if (this.CheckBoxListUser.Items[index].Selected)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      List<WEBSERVICE> list = (List<WEBSERVICE>) this.Session["ListWSUser"];
      HelpClassUsers helpClassUsers = (HelpClassUsers) this.Application["UserPermission"];
      INSTITUTION byId1 = new InstitutionsDAL().GetByID(helpClassUsers.IDInstitution);
      USER byId2 = new UsersDAL().GetByID(helpClassUsers.ID);
      KeyValuePair<int, string> keyValuePair;
      for (int i = 0; i < this.CheckBoxListUser.Items.Count; ++i)
      {
        if (this.CheckBoxListUser.Items[i].Selected)
        {
          try
          {
            Enumerable.Single<WEBSERVICE>((IEnumerable<WEBSERVICE>) list, (Func<WEBSERVICE, bool>) (p => p.ID == Convert.ToInt64(this.CheckBoxListUser.Items[i].Value)));
          }
          catch
          {
            WEBSERVICE byId3 = new WebservicesDAL().GetByID(Convert.ToInt64(this.CheckBoxListUser.Items[i].Value));
            long num = new PermissionsDAL().Insert(byId1, byId2, byId3, 2, true, DateTime.Now);
            string old = "";
            string newone = "";
            keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS"));
            new LOGDAL().Insert(keyValuePair.Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
          }
        }
        else
        {
          try
          {
            WEBSERVICE ws = Enumerable.Single<WEBSERVICE>((IEnumerable<WEBSERVICE>) list, (Func<WEBSERVICE, bool>) (p => p.ID == Convert.ToInt64(this.CheckBoxListUser.Items[i].Value)));
            PERMISSION permission = Enumerable.Single<PERMISSION>((IEnumerable<PERMISSION>) helpClassUsers.PermissionList, (Func<PERMISSION, bool>) (p => p.ID_WS == ws.ID && p.Usage == 2));
            new PermissionsDAL().Update((INSTITUTION) null, (USER) null, (WEBSERVICE) null, new int?(), new bool?(false), new DateTime?(), permission.ID);
            string old = "";
            string newone = "";
            keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS"));
            new LOGDAL().Insert(keyValuePair.Key, (USER) this.Session["user"], permission.ID.ToString(), 2, DateTime.Now, old, newone);
          }
          catch
          {
          }
        }
      }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<HelpClassUsers> list1 = (List<HelpClassUsers>) this.Application["ListUsersPermissionsPerm"];
      List<HelpClassUsers> list2 = new List<HelpClassUsers>();
      string[] ImePrezime = this.TextBox1.Text.Split(' ');
      if (Enumerable.Count<string>((IEnumerable<string>) ImePrezime) == 2)
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(ImePrezime[0].ToUpper()) && p.Surname.ToUpper().Contains(ImePrezime[1].ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list2.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list2.Add(helpClassUsers);
        }
        this.IGridView.DataSource = (object) list2;
        this.IGridView.DataBind();
      }
      else
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list2.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list2.Add(helpClassUsers);
        }
        this.IGridView.DataSource = (object) list2;
        this.IGridView.DataBind();
      }
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "DeleteWS")
      {
        long num = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
        List<WEBSERVICE> list = (List<WEBSERVICE>) this.Session["ListWSUser"];
        using (List<WEBSERVICE>.Enumerator enumerator = list.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            WEBSERVICE c = enumerator.Current;
            if (c.ID == num)
            {
              HelpClassUsers helpClassUsers = (HelpClassUsers) this.Application["UserPermission"];
              if (helpClassUsers.PermissionList.Count > 1)
              {
                list.Remove(c);
                PERMISSION permission = Enumerable.First<PERMISSION>((IEnumerable<PERMISSION>) helpClassUsers.PermissionList, (Func<PERMISSION, bool>) (p => p.ID_WS == c.ID && p.Usage == 2 && p.Active));
                new PermissionsDAL().Update((INSTITUTION) null, (USER) null, (WEBSERVICE) null, new int?(), new bool?(false), new DateTime?(), permission.ID);
                string old = "";
                string newone = "";
                new LOGDAL().Insert(Enumerable.First<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS")).Key, (USER) this.Session["user"], permission.ID.ToString(), 2, DateTime.Now, old, newone);
                this.Application["ListUsersPermissionsPerm"] = (object) new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((USER) this.Session["user"])[0].INSTITUTION);
                this.Application["UserPermission"] = (object) Enumerable.First<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissionsPerm"], (Func<HelpClassUsers, bool>) (p => p.ID == (Guid) this.Application["SelectedUser"]));
                break;
              }
              break;
            }
          }
        }
        this.Session["ListWSUser"] = (object) list;
        this.Gridview1.DataBind();
      }
      else
      {
        if (!(e.CommandName == "InsertWS"))
          return;
        WEBSERVICE byId = new WebservicesDAL().GetByID(Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]));
        List<WEBSERVICE> list = (List<WEBSERVICE>) this.Session["ListWSUser"];
        list.Add(byId);
        HelpClassUsers helpClassUsers = (HelpClassUsers) this.Application["UserPermission"];
        long num = new PermissionsDAL().Insert(new InstitutionsDAL().GetByID(helpClassUsers.IDInstitution), new UsersDAL().GetByID(helpClassUsers.ID), byId, 2, true, DateTime.Now);
        string old = "";
        string newone = "";
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS")).Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
        this.Application["ListUsersPermissionsPerm"] = (object) new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((USER) this.Session["user"])[0].INSTITUTION);
        this.Application["UserPermission"] = (object) Enumerable.First<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissionsPerm"], (Func<HelpClassUsers, bool>) (p => p.ID == (Guid) this.Application["SelectedUser"]));
        this.Session["ListWSUser"] = (object) list;
        this.Gridview1.DataBind();
      }
    }

    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      WEBSERVICE c = (WEBSERVICE) e.Row.DataItem;
      if (c != null)
      {
        imageButton.Attributes.Add("1", c.ID.ToString());
        List<WEBSERVICE> list = (List<WEBSERVICE>) this.Session["ListWSUser"];
        try
        {
          Enumerable.First<WEBSERVICE>((IEnumerable<WEBSERVICE>) list, (Func<WEBSERVICE, bool>) (p => p.ID == c.ID));
          imageButton.ImageUrl = "../rerources/images/add.png";
          imageButton.CommandName = "DeleteWS";
        }
        catch
        {
          imageButton.CommandName = "InsertWS";
        }
      }
    }

    protected void WSGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow || !(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Active")).ToString() == "True"))
        return;
      foreach (TableCell tableCell in e.Row.Cells)
      {
        if (tableCell.Text == "True")
        {
          Image image = new Image();
          image.ID = "trueicon";
          image.ImageUrl = "../rerources/images/Ok.png";
          tableCell.Wrap = false;
          tableCell.Controls.Add((Control) image);
        }
      }
    }

    protected void IGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.IGridView.PageIndex = e.NewPageIndex;
      this.IGridView.DataBind();
    }
  }
}
