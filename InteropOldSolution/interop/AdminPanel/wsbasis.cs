// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.wsbasis
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
  public class wsbasis : UserControl
  {
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView WSBasisGridView;
    protected Panel PanelBasisDeactive;
    protected Image Image5;
    protected Button btnCancel;
    protected Panel PanelNewUser;
    protected Image Image3;
    protected Label Label1;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected CheckBox CheckBoxTrue;
    protected CheckBox CheckBoxFalse;
    protected LinkButton LinkButton2;
    protected Panel PanelEditUser;
    protected Image Image2;
    protected Label Label3;
    protected TextBox txtPName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected CheckBox CheckBoxPTrue;
    protected CheckBox CheckBoxPFalse;
    protected LinkButton LinkButton3;
    protected HtmlGenericControl diverror;
    protected Image Image4;
    protected Literal Literal1;

    private void EnableTxtBox()
    {
      this.txtName.Enabled = true;
      this.CheckBoxFalse.Enabled = true;
      this.CheckBoxTrue.Enabled = true;
    }

    private void DisableTxtBox()
    {
      this.txtName.Enabled = false;
      this.CheckBoxFalse.Enabled = false;
      this.CheckBoxTrue.Enabled = false;
    }

    private void ClearTxtBox()
    {
      this.txtName.Text = "";
      this.CheckBoxFalse.Checked = false;
      this.CheckBoxTrue.Checked = false;
      this.txtPName.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearTxtBoxChange()
    {
      this.txtPName.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearStyleSubmeni()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) null;
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpOsnovBaranje.aspx";
      this.diverror.Visible = false;
      this.WSBasisGridView.DataSource = (object) (List<BASIS>) this.Application["ListBasis"];
      this.WSBasisGridView.DataBind();
      if (this.Session["AdminInstitution"] != null)
      {
        this.Session["AdminInstitution"] = (object) null;
        this.PanelNewUser.CssClass = "content-box column-left";
      }
      if (this.Session["naponema"] != null)
        Alert.Show(this.Session["naponema"].ToString());
      this.Session["naponema"] = (object) null;
    }

    protected void WSBasisGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ClearTxtBoxChange();
      int row = Convert.ToInt32(this.WSBasisGridView.SelectedDataKey.Value);
      BASIS basis = Enumerable.Single<BASIS>((IEnumerable<BASIS>) this.Application["ListBasis"], (Func<BASIS, bool>) (p => p.ID == (long) row));
      this.Application["Basis"] = (object) basis;
      this.txtPName.Text = basis.Tittle;
      if (basis.Active)
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
      bool @checked = this.CheckBoxPTrue.Checked;
      BASIS basis = (BASIS) this.Application["Basis"];
      List<WEBSERVICESBASIS> activeByBasisId = new WSBasisDAL().GetActiveByBasisID(basis.ID);
      if (activeByBasisId != null && activeByBasisId.Count == 0)
      {
        new BasisDAL().Update(this.txtPName.Text, @checked, new DateTime?(), basis.ID);
        string old = this.txtName.Text + ";" + true.ToString();
        string newone = "";
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "BASIS")).Key, (USER) this.Session["user"], basis.ID.ToString(), 1, DateTime.Now, old, newone);
        this.ClearTxtBoxChange();
        this.Application["ListBasis"] = (object) new BasisDAL().GetAllByStatus(true);
        this.WSBasisGridView.DataSource = (object) (List<BASIS>) this.Application["ListBasis"];
        this.WSBasisGridView.DataBind();
        this.PanelEditUser.CssClass = "content-box column-right closed-box";
        this.PanelNewUser.CssClass = "content-box column-left closed-box";
        this.CheckBoxTrue.Checked = true;
      }
      else
        this.PanelBasisDeactive.Visible = true;
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
      long num = new BasisDAL().Insert(this.txtName.Text, this.CheckBoxTrue.Checked, DateTime.Now);
      string old = this.txtName.Text + ";" + true.ToString();
      string newone = "";
      new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "BASIS")).Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
      this.ClearTxtBox();
      this.Application["ListBasis"] = (object) new BasisDAL().GetAllByStatus(true);
      this.WSBasisGridView.DataSource = (object) (List<BASIS>) this.Application["ListBasis"];
      this.WSBasisGridView.DataBind();
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.CheckBoxTrue.Checked = true;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(true);
      List<BASIS> list = new List<BASIS>();
      IEnumerable<BASIS> source = Enumerable.Where<BASIS>((IEnumerable<BASIS>) allByStatus, (Func<BASIS, bool>) (p => p.Tittle.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
      if (Enumerable.Count<BASIS>(source) != 0)
      {
        list.Clear();
        foreach (BASIS basis in source)
          list.Add(basis);
      }
      if (list.Count == 0)
        list.AddRange((IEnumerable<BASIS>) allByStatus);
      this.Application["ListBasis"] = (object) list;
      this.WSBasisGridView.DataSource = (object) list;
      this.WSBasisGridView.DataBind();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(true);
      allByStatus.AddRange((IEnumerable<BASIS>) new BasisDAL().GetAllByStatus(false));
      this.WSBasisGridView.DataSource = (object) allByStatus;
      this.WSBasisGridView.DataBind();
      this.Application["ListBasis"] = (object) allByStatus;
      this.ClearStyleSubmeni();
    }

    public void Funkcija()
    {
      List<BASIS> list = (List<BASIS>) this.Application["ListBasis"];
      new List<BASIS>().AddRange((IEnumerable<BASIS>) list);
      IEnumerable<BASIS> source = Enumerable.Where<BASIS>((IEnumerable<BASIS>) list, (Func<BASIS, bool>) (p => p.Tittle.StartsWith(this.TextBox1.Text, true, CultureInfo.CurrentUICulture)));
      if (Enumerable.Count<BASIS>(source) != 0)
      {
        list.Clear();
        foreach (BASIS basis in source)
          list.Add(basis);
      }
      this.WSBasisGridView.DataSource = (object) list;
      this.WSBasisGridView.DataBind();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(true);
      this.WSBasisGridView.DataSource = (object) allByStatus;
      this.WSBasisGridView.DataBind();
      this.Application["ListBasis"] = (object) allByStatus;
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(false);
      this.WSBasisGridView.DataSource = (object) allByStatus;
      this.WSBasisGridView.DataBind();
      this.Application["ListBasis"] = (object) allByStatus;
      this.ClearStyleSubmeni();
    }

    protected void CheckBoxTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (this.CheckBoxTrue.Checked)
        this.CheckBoxFalse.Checked = false;
      else
        this.CheckBoxFalse.Checked = true;
    }

    protected void CheckBoxFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (this.CheckBoxFalse.Checked)
        this.CheckBoxTrue.Checked = false;
      else
        this.CheckBoxTrue.Checked = true;
    }

    protected void CheckBoxPTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (this.CheckBoxPTrue.Checked)
        this.CheckBoxPFalse.Checked = false;
      else
        this.CheckBoxPFalse.Checked = true;
    }

    protected void CheckBoxPFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (this.CheckBoxPFalse.Checked)
        this.CheckBoxPTrue.Checked = false;
      else
        this.CheckBoxPTrue.Checked = true;
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
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(false);
      this.WSBasisGridView.DataSource = (object) allByStatus;
      this.WSBasisGridView.DataBind();
      this.Application["ListBasis"] = (object) allByStatus;
      this.WSBasisGridView.SelectedRowStyle.Reset();
      this.WSBasisGridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.WSBasisGridView.RowStyle.CssClass = "RowStyle";
      this.PanelEditUser.CssClass = "content-box column-left closed-box";
      this.ClearTxtBoxChange();
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(true);
      this.WSBasisGridView.DataSource = (object) allByStatus;
      this.WSBasisGridView.DataBind();
      this.Application["ListBasis"] = (object) allByStatus;
      this.WSBasisGridView.SelectedRowStyle.Reset();
      this.WSBasisGridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.WSBasisGridView.RowStyle.CssClass = "RowStyle";
      this.PanelEditUser.CssClass = "content-box column-left closed-box";
      this.ClearTxtBoxChange();
    }

    protected void WSBasisGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.WSBasisGridView.PageIndex = e.NewPageIndex;
      this.WSBasisGridView.DataBind();
      this.WSBasisGridView.SelectedRowStyle.Reset();
      this.WSBasisGridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.WSBasisGridView.RowStyle.CssClass = "RowStyle";
      this.PanelEditUser.CssClass = "content-box column-left closed-box";
      this.ClearTxtBoxChange();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.PanelBasisDeactive.Visible = false;
      this.CheckBoxPTrue.Checked = true;
      this.CheckBoxPFalse.Checked = false;
    }
  }
}
