// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.institutions
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.AdminPanel
{
  public class institutions : UserControl
  {
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView InstitutionsGridView;
    protected Panel PanelNewUser;
    protected Image Image3;
    protected Label Label1;
    protected TextBox txtName;
    protected Label Label2;
    protected TextBox txtDescription;
    protected CheckBox CheckBoxTrue;
    protected CheckBox CheckBoxFalse;
    protected LinkButton LinkButton2;
    protected Panel PanelEditUser;
    protected Image Image2;
    protected Label Label3;
    protected TextBox txtPName;
    protected Label Label4;
    protected TextBox txtPDescription;
    protected CheckBox CheckBoxPTrue;
    protected CheckBox CheckBoxPFalse;
    protected LinkButton LinkButton3;
    protected HtmlGenericControl diverror;
    protected Image Image4;
    protected Literal Literal1;

    private void EnableTxtBox()
    {
      this.txtName.Enabled = true;
      this.txtDescription.Enabled = true;
      this.CheckBoxFalse.Enabled = true;
      this.CheckBoxTrue.Enabled = true;
    }

    private void DisableTxtBox()
    {
      this.txtName.Enabled = false;
      this.txtDescription.Enabled = false;
      this.CheckBoxFalse.Enabled = false;
      this.CheckBoxTrue.Enabled = false;
    }

    private void ClearTxtBox()
    {
      this.txtName.Text = "";
      this.txtDescription.Text = "";
      this.CheckBoxFalse.Checked = false;
      this.CheckBoxTrue.Checked = false;
      this.txtPName.Text = "";
      this.txtPDescription.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearTxtBoxChange()
    {
      this.txtPName.Text = "";
      this.txtPDescription.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearStyleSubmeni()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpInstitucii.aspx";
      this.InstitutionsGridView.DataSource = (object) (List<INSTITUTION>) this.Application["ListInstitutions"];
      this.InstitutionsGridView.DataBind();
      if (this.Session["AdminInstitution"] == null)
        return;
      this.Session["AdminInstitution"] = (object) null;
      this.PanelNewUser.CssClass = "content-box column-left";
    }

    protected void InstitutionsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ClearTxtBoxChange();
      int row = Convert.ToInt32(this.InstitutionsGridView.SelectedDataKey.Value);
      INSTITUTION institution = Enumerable.Single<INSTITUTION>((IEnumerable<INSTITUTION>) this.Application["ListInstitutions"], (Func<INSTITUTION, bool>) (p => p.ID == (long) row));
      this.Application["Institution"] = (object) institution;
      this.txtPName.Text = institution.Tittle;
      this.txtPDescription.Text = institution.Description;
      if (institution.Active)
      {
        this.CheckBoxPTrue.Checked = true;
        this.CheckBoxPFalse.Checked = false;
      }
      else
      {
        this.CheckBoxPFalse.Checked = true;
        this.CheckBoxPTrue.Checked = false;
      }
      this.LinkButton3.Enabled = true;
      this.PanelEditUser.CssClass = "content-box column-right";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    protected void btn_promeni_Click(object sender, EventArgs e)
    {
      INSTITUTION inst = (INSTITUTION) this.Application["Institution"];
      bool flag1 = false;
      if (this.CheckBoxPTrue.Checked)
        flag1 = true;
      bool flag2 = false;
      if (!flag1)
      {
        List<HelpClassUsers> usersByInstitution = new UsersDAL().GetUsersByInstitution(inst);
        List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(inst);
        if (usersByInstitution.Count != 0 || servicesByIstitution.Count != 0)
        {
          string str = "Не е дозволено бришење! Зависност од следниве објекти<br />";
          if (usersByInstitution.Count != 0)
          {
            str += "<br />Корисници:<br />";
            foreach (HelpClassUsers helpClassUsers in usersByInstitution)
              str = str + "   " + helpClassUsers.NameSurname + "<br />";
          }
          if (servicesByIstitution.Count != 0)
          {
            str += "<br />Сервиси:<br />";
            foreach (WEBSERVICE webservice in servicesByIstitution)
              str = str + "   " + webservice.Tittle + "<br />";
          }
          this.diverror.Visible = true;
          this.Literal1.Text = str;
        }
        else
          flag2 = true;
      }
      else
        flag2 = true;
      if (flag2)
      {
        new InstitutionsDAL().Update(this.txtPName.Text, this.txtPDescription.Text, new bool?(flag1), new DateTime?(), inst.ID);
        string old = inst.Tittle + ";" + inst.Description + ";" + inst.Active.ToString();
        string newone = this.txtPName.Text + ";" + this.txtPDescription.Text + ";" + flag1.ToString();
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "INSTITUTIONS")).Key, (USER) this.Session["user"], inst.ID.ToString(), 2, DateTime.Now, old, newone);
        this.Application["ListInstitutions"] = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
        this.InstitutionsGridView.DataSource = (object) (List<INSTITUTION>) this.Application["ListInstitutions"];
        this.InstitutionsGridView.DataBind();
      }
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
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

    protected void btn_vnesi_Click(object sender, EventArgs e)
    {
      long num = new InstitutionsDAL().Insert(this.txtName.Text, this.txtDescription.Text, true, DateTime.Now);
      string old = this.txtName.Text + ";" + this.txtDescription.Text + ";" + true.ToString();
      string newone = "";
      new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "INSTITUTIONS")).Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
      this.ClearTxtBox();
      this.Application["ListInstitutions"] = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
      this.InstitutionsGridView.DataSource = (object) (List<INSTITUTION>) this.Application["ListInstitutions"];
      this.InstitutionsGridView.DataBind();
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      List<INSTITUTION> list = new List<INSTITUTION>();
      IEnumerable<INSTITUTION> source = Enumerable.Where<INSTITUTION>((IEnumerable<INSTITUTION>) allActiveDeleted, (Func<INSTITUTION, bool>) (p => p.Tittle.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
      if (Enumerable.Count<INSTITUTION>(source) != 0)
      {
        list.Clear();
        foreach (INSTITUTION institution in source)
          list.Add(institution);
      }
      if (list.Count == 0)
        list.AddRange((IEnumerable<INSTITUTION>) allActiveDeleted);
      this.Application["ListInstitutions"] = (object) list;
      this.InstitutionsGridView.DataSource = (object) list;
      this.InstitutionsGridView.DataBind();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      allActiveDeleted.AddRange((IEnumerable<INSTITUTION>) new InstitutionsDAL().GetAllActiveDeleted(false));
      this.InstitutionsGridView.DataSource = (object) allActiveDeleted;
      this.InstitutionsGridView.DataBind();
      this.Application["ListInstitutions"] = (object) allActiveDeleted;
      this.ClearStyleSubmeni();
    }

    public void Funkcija()
    {
      List<INSTITUTION> list1 = (List<INSTITUTION>) this.Application["ListInstitutions"];
      List<INSTITUTION> list2 = new List<INSTITUTION>();
      list2.AddRange((IEnumerable<INSTITUTION>) list1);
      IEnumerable<INSTITUTION> source = Enumerable.Where<INSTITUTION>((IEnumerable<INSTITUTION>) list1, (Func<INSTITUTION, bool>) (p => p.Tittle.StartsWith(this.TextBox1.Text, true, CultureInfo.CurrentUICulture)));
      if (Enumerable.Count<INSTITUTION>(source) != 0)
      {
        list2.Clear();
        foreach (INSTITUTION institution in source)
          list2.Add(institution);
      }
      this.InstitutionsGridView.DataSource = (object) list2;
      this.InstitutionsGridView.DataBind();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      this.InstitutionsGridView.DataSource = (object) allActiveDeleted;
      this.InstitutionsGridView.DataBind();
      this.Application["ListInstitutions"] = (object) allActiveDeleted;
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(false);
      this.InstitutionsGridView.DataSource = (object) allActiveDeleted;
      this.InstitutionsGridView.DataBind();
      this.Application["ListInstitutions"] = (object) allActiveDeleted;
      this.ClearStyleSubmeni();
    }

    protected void CheckBoxTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (!this.CheckBoxTrue.Checked)
        return;
      this.CheckBoxFalse.Checked = false;
    }

    protected void CheckBoxFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (!this.CheckBoxFalse.Checked)
        return;
      this.CheckBoxTrue.Checked = false;
    }

    protected void CheckBoxPTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (!this.CheckBoxPTrue.Checked)
        return;
      this.CheckBoxPFalse.Checked = false;
    }

    protected void CheckBoxPFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (!this.CheckBoxPFalse.Checked)
        return;
      this.CheckBoxPTrue.Checked = false;
    }

    protected void WSGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      string str = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Active"));
      if (str.ToString() == "True")
      {
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
      else if (str.ToString() == "False")
      {
        foreach (TableCell tableCell in e.Row.Cells)
        {
          if (tableCell.Text == "False")
          {
            Image image = new Image();
            image.ID = "trueicon";
            image.ImageUrl = "../rerources/images/Cancel.png";
            tableCell.Wrap = false;
            tableCell.Controls.Add((Control) image);
          }
        }
      }
    }

    protected void LinkButton4_Click1(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(false);
      this.InstitutionsGridView.DataSource = (object) allActiveDeleted;
      this.InstitutionsGridView.DataBind();
      this.Application["ListInstitutions"] = (object) allActiveDeleted;
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      this.InstitutionsGridView.DataSource = (object) allActiveDeleted;
      this.InstitutionsGridView.DataBind();
      this.Application["ListInstitutions"] = (object) allActiveDeleted;
    }

    protected void InstitutionsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.InstitutionsGridView.PageIndex = e.NewPageIndex;
      this.InstitutionsGridView.DataBind();
    }
  }
}
