// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.wsl
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
  public class wsl : UserControl
  {
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton3;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView WSGridView;
    protected Panel PanelEditUser;
    protected Image Image2;
    protected Label Label1;
    protected TextBox txtName;
    protected Label Label2;
    protected TextBox txtDescription;
    protected Label Label3;
    protected TextBox txtURL;
    protected Label Label5;
    protected TextBox txtNote;
    protected Label Label6;
    protected TextBox TextBoxInst;
    protected GridView Gridview1;
    protected CheckBox CheckBoxTrue;
    protected CheckBox CheckBoxFalse;
    protected LinkButton LinkButton1;

    private void EnableTxtBox()
    {
      this.txtName.Enabled = true;
      this.txtDescription.Enabled = true;
      this.txtURL.Enabled = true;
      this.txtNote.Enabled = true;
      this.CheckBoxFalse.Enabled = true;
      this.CheckBoxTrue.Enabled = true;
    }

    private void DisableTxtBox()
    {
      this.txtName.Enabled = false;
      this.txtDescription.Enabled = false;
      this.txtURL.Enabled = false;
      this.txtNote.Enabled = false;
      this.CheckBoxFalse.Enabled = false;
      this.CheckBoxTrue.Enabled = false;
    }

    private void ClearTxtBox()
    {
      this.txtName.Text = "";
      this.txtDescription.Text = "";
      this.txtURL.Text = "";
      this.txtNote.Text = "";
      this.CheckBoxFalse.Checked = false;
      this.CheckBoxTrue.Checked = false;
    }

    private void ClearStyleSubmeni()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiPocetna.aspx";
      this.PageLoadStaf();
    }

    public void PageLoadStaf()
    {
      this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Application["ListUserWebServices"];
      this.WSGridView.DataBind();
    }

    protected void WSGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      int row = Convert.ToInt32(this.WSGridView.SelectedDataKey.Value);
      HelpClassWebServices classWebServices = Enumerable.Single<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) this.Application["ListUserWebServices"], (Func<HelpClassWebServices, bool>) (p => p.ID == (long) row));
      this.Application["TempWebService"] = (object) classWebServices;
      this.txtName.Text = classWebServices.Tittle;
      this.txtDescription.Text = classWebServices.Description;
      this.txtURL.Text = classWebServices.URL;
      this.txtNote.Text = classWebServices.Note;
      this.TextBoxInst.Text = classWebServices.InstitutionName;
      if (classWebServices.Active)
      {
        this.CheckBoxTrue.Checked = true;
        this.CheckBoxFalse.Checked = false;
      }
      else
      {
        this.CheckBoxFalse.Checked = true;
        this.CheckBoxTrue.Checked = false;
      }
      this.DisableTxtBox();
      this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(classWebServices.WSObj);
      this.Gridview1.DataBind();
      this.PanelEditUser.CssClass = "content-box";
      this.PanelEditUser.Visible = true;
    }

    protected void btn_promeni_Click(object sender, EventArgs e)
    {
      HelpClassWebServices classWebServices = (HelpClassWebServices) this.Application["TempWebService"];
      bool flag = false;
      if (this.CheckBoxTrue.Checked)
        flag = true;
      new WebservicesDAL().Update(this.txtName.Text, this.txtDescription.Text, this.txtNote.Text, this.txtURL.Text, new bool?(flag), new DateTime?(), classWebServices.ID);
      string old = classWebServices.Tittle + ";" + classWebServices.Description + ";" + classWebServices.Note + ";" + classWebServices.URL + ";" + classWebServices.Active.ToString();
      string newone = this.txtName.Text + ";" + this.txtDescription.Text + ";" + this.txtNote.Text + ";" + this.txtURL.Text + ";" + flag.ToString();
      new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICES")).Key, (USER) this.Session["user"], classWebServices.ID.ToString(), 2, DateTime.Now, old, newone);
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
      this.ClearTxtBox();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
    }

    protected void CheckBoxTrue_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.CheckBoxTrue.Checked)
        return;
      this.CheckBoxFalse.Checked = false;
    }

    protected void CheckBoxFalse_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.CheckBoxFalse.Checked)
        return;
      this.CheckBoxTrue.Checked = false;
    }

    protected void btn_vnesi_Click(object sender, EventArgs e)
    {
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      servicesPermissions.AddRange((IEnumerable<HelpClassWebServices>) new WebservicesDAL().GetUsersWebServicesPermissions(false, (USER) this.Session["user"]));
      this.Application["ListUserWebServices"] = (object) servicesPermissions;
      this.WSGridView.DataSource = (object) servicesPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      this.Application["ListUserWebServices"] = (object) servicesPermissions;
      this.WSGridView.DataSource = (object) servicesPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(false, (USER) this.Session["user"]);
      this.Application["ListUserWebServices"] = (object) servicesPermissions;
      this.WSGridView.DataSource = (object) servicesPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      USER user = (USER) this.Session["user"];
      List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, user, permisionsByUser[0].INSTITUTION);
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
      this.Application["ListUserWebServices"] = (object) list;
      this.WSGridView.DataSource = (object) list;
      this.WSGridView.DataBind();
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

    protected void LinkButton4_Click1(object sender, EventArgs e)
    {
    }

    protected void LinkButton3_Click1(object sender, EventArgs e)
    {
    }

    protected void WSGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.WSGridView.PageIndex = e.NewPageIndex;
      this.WSGridView.DataBind();
    }
  }
}
