// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.ws
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using StringProtection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.AdminPanel
{
  public class ws : UserControl
  {
    protected Panel PanelMainContent;
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton3;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView WSGridView;
    protected Panel PanelServiceDeactive;
    protected Image Image4;
    protected Label Label11;
    protected TextBox txtAdminUserName;
    protected Label Label14;
    protected TextBox txtAdminPass;
    protected Label Label12;
    protected TextBox txtLadminUserName;
    protected Label Label13;
    protected TextBox txtLadminPass;
    protected Button btnCancel;
    protected Button btnPotvrdi;
    protected Panel PanelErrorInfo;
    protected Panel PanelNewUser;
    protected Image Image3;
    protected Label Label4;
    protected TextBox txtPName;
    protected Label Label7;
    protected TextBox txtPDescription;
    protected Label Label8;
    protected TextBox txtPURL;
    protected Label Label9;
    protected TextBox txtPNote;
    protected Label Label10;
    protected DropDownList DropDownList2;
    protected GridView Gridview2;
    protected CheckBox CheckBoxPTrue;
    protected CheckBox CheckBoxPFalse;
    protected LinkButton LinkButton2;
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
    protected TextBox txtInst;
    protected GridView Gridview1;
    protected CheckBox CheckBoxTrue;
    protected CheckBox CheckBoxFalse;
    protected LinkButton LinkButton1;
    protected GridView GridviewParamInfo;

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

    private void ClearTxtBoxChange()
    {
      this.txtPName.Text = "";
      this.txtPDescription.Text = "";
      this.txtPURL.Text = "";
      this.txtPNote.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearStyleSubmeni()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpServisiSuper.aspx";
      if (!this.IsPostBack)
        return;
      this.PageLoadStaf();
      if (this.Session["AdminWS"] != null)
      {
        this.Session["AdminWS"] = (object) null;
        this.PanelNewUser.CssClass = "content-box column-left";
        this.PanelNewUser.Focus();
      }
    }

    public void PageLoadStaf()
    {
      string selectedValue1 = this.DropDownList2.SelectedValue;
      string selectedValue2 = this.DropDownList3.SelectedValue;
      this.DropDownList2.Items.Clear();
      ListItem listItem = new ListItem()
      {
        Value = "0",
        Text = "-- Листај по институција --"
      };
      this.DropDownList2.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Листај по институција --"
      });
      this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Application["ListWebServices"];
      this.Session["ListOsnoviAll"] = (object) new BasisDAL().GetAllByStatus(true);
      if (this.Session["ListOsnoviSelected"] == null)
        this.Session["ListOsnoviSelected"] = (object) new List<BASIS>();
      this.WSGridView.DataBind();
      this.DropDownList2.DataSource = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
      this.DropDownList2.DataBind();
      this.DropDownList2.SelectedValue = selectedValue1;
      if ((List<PARAM>) this.Application["Params"] == null)
        this.Gridview2.DataSource = (object) new List<PARAM>();
      else
        this.Gridview2.DataSource = (object) (List<PARAM>) this.Application["Params"];
      this.Gridview2.DataBind();
      if ((HelpClassWebServices) this.Session["AdminSelectedWS"] != null)
      {
        HelpClassWebServices tempWS = (HelpClassWebServices) this.Session["AdminSelectedWS"];
        this.Application["TempUser"] = (object) tempWS;
        this.Session["AdminSelectedWS"] = (object) null;
        this.FillWS(tempWS);
      }
      this.DropDownList3.SelectedValue = selectedValue2;
    }

    protected void WSGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ClearTxtBox();
      int row = Convert.ToInt32(this.WSGridView.SelectedDataKey.Value);
      HelpClassWebServices tempWS = Enumerable.Single<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) this.Application["ListWebServices"], (Func<HelpClassWebServices, bool>) (p => p.ID == (long) row));
      this.Application["TempWebService"] = (object) tempWS;
      this.FillWS(tempWS);
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    public void FillWS(HelpClassWebServices tempWS)
    {
      this.txtName.Text = tempWS.Tittle;
      this.txtDescription.Text = tempWS.Description;
      this.txtURL.Text = tempWS.URL;
      this.txtNote.Text = tempWS.Note;
      this.txtInst.Text = "";
      if (tempWS.IDInstitution != 0L)
        this.txtInst.Text = tempWS.InstitutionName;
      if (tempWS.Active)
      {
        this.CheckBoxTrue.Checked = true;
        this.CheckBoxFalse.Checked = false;
      }
      else
      {
        this.CheckBoxFalse.Checked = true;
        this.CheckBoxTrue.Checked = false;
      }
      this.LinkButton1.Enabled = true;
      this.Gridview1.Visible = false;
      List<PARAM> paramsForWebservice = new ParamsDAL().GetParamsForWebservice(tempWS.WSObj);
      this.Application["WS"] = (object) tempWS.WSObj;
      this.Gridview1.DataSource = (object) paramsForWebservice;
      this.Gridview1.DataBind();
      this.GridviewParamInfo.DataSource = (object) new ParamsDAL().GetParamsForWebservice(tempWS.WSObj);
      this.GridviewParamInfo.DataBind();
      this.PanelEditUser.CssClass = "content-box column-right";
      this.EnableTxtBox();
      this.Gridview1.Visible = true;
      this.GridviewParamInfo.Visible = false;
    }

    protected void btn_promeni_Click(object sender, EventArgs e)
    {
      HelpClassWebServices classWebServices = (HelpClassWebServices) this.Application["TempWebService"];
      bool flag = false;
      if (this.CheckBoxTrue.Checked)
        flag = true;
      if (!flag && classWebServices.Active)
      {
        this.PanelServiceDeactive.Visible = true;
        this.PanelServiceDeactive.Focus();
        USER user = (USER) this.Session["user"];
        this.txtAdminUserName.Text = user.username;
        this.txtAdminPass.Text = user.password;
      }
      else
      {
        new WebservicesDAL().Update(this.txtName.Text, this.txtDescription.Text, this.txtNote.Text, this.txtURL.Text, new bool?(flag), new DateTime?(), classWebServices.ID);
        string old = classWebServices.Tittle + ";" + classWebServices.Description + ";" + classWebServices.Note + ";" + classWebServices.URL + ";" + classWebServices.Active.ToString();
        string newone = this.txtName.Text + ";" + this.txtDescription.Text + ";" + this.txtNote.Text + ";" + this.txtURL.Text + ";" + flag.ToString();
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICES")).Key, (USER) this.Session["user"], classWebServices.ID.ToString(), 2, DateTime.Now, old, newone);
        this.Application["ListWebServices"] = (object) new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
        this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Application["ListWebServices"];
        this.WSGridView.DataBind();
        this.DropDownList3.SelectedIndex = -1;
      }
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
      this.ClearTxtBox();
      this.Gridview1.DataSource = (object) new List<PARAM>();
      this.Gridview1.DataBind();
      this.Gridview1.Visible = true;
      this.GridviewParamInfo.Visible = false;
      this.Application["WS"] = (object) null;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
      this.Gridview1.Visible = true;
      this.GridviewParamInfo.Visible = false;
    }

    protected void CheckBoxTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (!this.CheckBoxTrue.Checked)
        return;
      this.CheckBoxFalse.Checked = false;
    }

    protected void CheckBoxFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
      if (!this.CheckBoxFalse.Checked)
        return;
      this.CheckBoxTrue.Checked = false;
    }

    protected void CheckBoxPTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (!this.CheckBoxPTrue.Checked)
        return;
      this.CheckBoxPFalse.Checked = false;
    }

    protected void CheckBoxPFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (!this.CheckBoxPFalse.Checked)
        return;
      this.CheckBoxPTrue.Checked = false;
    }

    protected void btn_vnesi_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.DropDownList2.SelectedValue != "0")
        {
          bool active = true;
          long id = new WebservicesDAL().Insert(this.txtPName.Text, this.txtPDescription.Text, this.txtPNote.Text, this.txtPURL.Text, active, DateTime.Now);
          string old1 = this.txtPName.Text + ";" + this.txtPDescription.Text + ";" + this.txtPNote.Text + ";" + this.txtPURL.Text + ";" + active.ToString();
          string newone1 = "";
          new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICES")).Key, (USER) this.Session["user"], id.ToString(), 1, DateTime.Now, old1, newone1);
          USER user = (USER) this.Session["user"];
          INSTITUTION byId1 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(this.DropDownList2.SelectedValue));
          WEBSERVICE byId2 = new WebservicesDAL().GetByID(id);
          long num1 = new PermissionsDAL().Insert(byId1, user, byId2, 1, true, DateTime.Now);
          string old2 = "";
          string newone2 = "";
          new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS")).Key, (USER) this.Session["user"], num1.ToString(), 1, DateTime.Now, old2, newone2);
          List<PARAM> list = (List<PARAM>) this.Application["Params"];
          KeyValuePair<int, string> keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PARAMS"));
          foreach (PARAM obj in list)
          {
            long num2 = new ParamsDAL().Insert(obj.Tittle, obj.Description, obj.MaxLength, obj.Type, byId2, true);
            string old3 = obj.Tittle + (object) ";" + obj.Description + ";" + (string) (object) obj.MaxLength + ";" + (string) (object) obj.Type + ";" + true.ToString();
            string newone3 = "";
            new LOGDAL().Insert(keyValuePair.Key, (USER) this.Session["user"], num2.ToString(), 1, DateTime.Now, old3, newone3);
          }
          foreach (BASIS basis in (List<BASIS>) this.Session["ListOsnoviSelected"])
            Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICESBASIS"));
          this.ClearTxtBoxChange();
          this.Application["Params"] = (object) null;
          this.Application["ListWebServices"] = (object) new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
          this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Application["ListWebServices"];
          this.WSGridView.DataBind();
          this.DropDownList3.SelectedIndex = -1;
        }
        this.PanelNewUser.CssClass = "content-box column-left closed-box";
        this.PanelEditUser.CssClass = "content-box column-right closed-box";
      }
      catch
      {
      }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
      institutionsPermissions.AddRange((IEnumerable<HelpClassWebServices>) new WebservicesDAL().GetWebServicesInstitutionsPermissions(false));
      this.Application["ListWebServices"] = (object) institutionsPermissions;
      this.WSGridView.DataSource = (object) institutionsPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
      this.Application["ListWebServices"] = (object) institutionsPermissions;
      this.WSGridView.DataSource = (object) institutionsPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(false);
      this.Application["ListWebServices"] = (object) institutionsPermissions;
      this.WSGridView.DataSource = (object) institutionsPermissions;
      this.WSGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "NoDataInsert")
      {
        TextBox textBox1 = (TextBox) this.Gridview1.Controls[0].Controls[0].FindControl("NoDataTittle");
        TextBox textBox2 = (TextBox) this.Gridview1.Controls[0].Controls[0].FindControl("NoDataDescription");
        WEBSERVICE ws = (WEBSERVICE) this.Application["WS"];
        if (ws == null)
          return;
        long num = new ParamsDAL().Insert(textBox1.Text, textBox2.Text, 50, 1, ws, true);
        string old = textBox1.Text + ";" + textBox2.Text + ";50;1;" + true.ToString();
        string newone = "";
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PARAMS")).Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
        this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(ws);
        this.Gridview1.DataBind();
      }
      else
      {
        if (!(e.CommandName == "InsertNew"))
          return;
        TextBox textBox1 = (TextBox) this.Gridview1.FooterRow.FindControl("InsertTittle");
        TextBox textBox2 = (TextBox) this.Gridview1.FooterRow.FindControl("InsertDescription");
        WEBSERVICE ws = (WEBSERVICE) this.Application["WS"];
        if (ws != null)
        {
          long num = new ParamsDAL().Insert(textBox1.Text, textBox2.Text, 50, 1, ws, true);
          string old = textBox1.Text + ";" + textBox2.Text + ";50;1;" + true.ToString();
          string newone = "";
          new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PARAMS")).Key, (USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old, newone);
          this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(ws);
          this.Gridview1.DataBind();
        }
      }
    }

    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
      TextBox textBox1 = (TextBox) this.Gridview1.SelectedRow.FindControl("EditTittle");
      TextBox textBox2 = (TextBox) this.Gridview1.SelectedRow.FindControl("EditDescription");
      long id = Convert.ToInt64(this.Gridview1.SelectedDataKey.Value);
      WEBSERVICE ws = (WEBSERVICE) this.Application["WS"];
      if (ws == null)
        return;
      PARAM byId = new ParamsDAL().GetByID(id);
      new ParamsDAL().Update(textBox1.Text, textBox2.Text, new int?(), new int?(), (WEBSERVICE) null, new bool?(), id);
      string old = byId.Tittle + ";" + byId.Description;
      string newone = textBox1.Text + ";" + textBox2.Text;
      new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PARAMS")).Key, (USER) this.Session["user"], id.ToString(), 2, DateTime.Now, old, newone);
      this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(ws);
      this.Gridview1.DataBind();
    }

    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      WEBSERVICE ws = (WEBSERVICE) this.Application["WS"];
      string TittleText = ((TextBox) this.Gridview1.Rows[e.RowIndex].FindControl("EditTittle")).Text;
      string DescText = ((TextBox) this.Gridview1.Rows[e.RowIndex].FindControl("EditDescription")).Text;
      if (ws == null)
        return;
      List<PARAM> paramsForWebservice = new ParamsDAL().GetParamsForWebservice(ws);
      try
      {
        PARAM obj = Enumerable.Single<PARAM>((IEnumerable<PARAM>) paramsForWebservice, (Func<PARAM, bool>) (p => p.Description == DescText && p.Tittle == TittleText));
        new ParamsDAL().Delete(obj.ID);
        string old = "";
        string newone = "";
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PARAMS")).Key, (USER) this.Session["user"], obj.ID.ToString(), 3, DateTime.Now, old, newone);
        this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(ws);
        this.Gridview1.DataBind();
      }
      catch
      {
        this.Gridview1.DataSource = (object) new ParamsDAL().GetParamsForWebservice(ws);
        this.Gridview1.DataBind();
      }
    }

    protected void Gridview2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      if (e.CommandName == "NoDataInsert")
      {
        TextBox textBox1 = (TextBox) this.Gridview2.Controls[0].Controls[0].FindControl("NoDataTittle");
        TextBox textBox2 = (TextBox) this.Gridview2.Controls[0].Controls[0].FindControl("NoDataDescription");
        List<PARAM> list = new List<PARAM>();
        list.Add(new PARAM()
        {
          Tittle = textBox1.Text,
          Description = textBox2.Text,
          MaxLength = 50,
          Type = 1,
          ID_WS = 0L,
          Acitve = true,
          ID = 1L
        });
        this.Gridview2.DataSource = (object) list;
        this.Gridview2.DataBind();
        this.Application["Params"] = (object) list;
      }
      else
      {
        if (!(e.CommandName == "InsertNew"))
          return;
        TextBox textBox1 = (TextBox) this.Gridview2.FooterRow.FindControl("InsertTittle");
        TextBox textBox2 = (TextBox) this.Gridview2.FooterRow.FindControl("InsertDescription");
        List<PARAM> list = (List<PARAM>) this.Application["Params"];
        list.Add(new PARAM()
        {
          Tittle = textBox1.Text,
          Description = textBox2.Text,
          MaxLength = 50,
          Type = 1,
          ID_WS = 0L,
          Acitve = true,
          ID = list[list.Count - 1].ID + 1L
        });
        this.Gridview2.DataSource = (object) list;
        this.Gridview2.DataBind();
        this.Application["Params"] = (object) list;
      }
    }

    protected void Gridview2_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      TextBox textBox1 = (TextBox) this.Gridview2.SelectedRow.FindControl("EditTittle");
      TextBox textBox2 = (TextBox) this.Gridview2.SelectedRow.FindControl("EditDescription");
      long num = Convert.ToInt64(this.Gridview2.SelectedDataKey.Value);
      List<PARAM> list = (List<PARAM>) this.Application["Params"];
      foreach (PARAM obj in list)
      {
        if (obj.ID == num)
        {
          obj.Tittle = textBox1.Text;
          obj.Description = textBox2.Text;
          break;
        }
      }
      this.Gridview2.DataSource = (object) list;
      this.Gridview2.DataBind();
      this.Application["Params"] = (object) list;
    }

    protected void Gridview2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      try
      {
        string TittleText = ((TextBox) this.Gridview2.Rows[e.RowIndex].FindControl("EditTittle")).Text;
        string DescText = ((TextBox) this.Gridview2.Rows[e.RowIndex].FindControl("EditDescription")).Text;
        List<PARAM> list = (List<PARAM>) this.Application["Params"];
        PARAM obj = Enumerable.Single<PARAM>((IEnumerable<PARAM>) list, (Func<PARAM, bool>) (p => p.Description == DescText && p.Tittle == TittleText));
        list.Remove(obj);
        this.Gridview2.DataSource = (object) list;
        this.Gridview2.DataBind();
        this.Application["Params"] = (object) list;
      }
      catch
      {
        this.Gridview2.DataSource = (object) (List<PARAM>) this.Application["Params"];
        this.Gridview2.DataBind();
      }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
      List<HelpClassWebServices> list = new List<HelpClassWebServices>();
      IEnumerable<HelpClassWebServices> source = Enumerable.Where<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) institutionsPermissions, (Func<HelpClassWebServices, bool>) (p => p.Tittle.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
      if (Enumerable.Count<HelpClassWebServices>(source) != 0)
      {
        list.Clear();
        foreach (HelpClassWebServices classWebServices in source)
          list.Add(classWebServices);
      }
      if (list.Count == 0)
        list.AddRange((IEnumerable<HelpClassWebServices>) institutionsPermissions);
      this.Application["ListWebServices"] = (object) list;
      this.WSGridView.DataSource = (object) list;
      this.WSGridView.DataBind();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.DropDownList3.SelectedValue == "1")
      {
        List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(false);
        this.Application["ListWebServices"] = (object) institutionsPermissions;
        this.WSGridView.DataSource = (object) institutionsPermissions;
        this.WSGridView.DataBind();
      }
      else
      {
        List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
        this.Application["ListWebServices"] = (object) institutionsPermissions;
        this.WSGridView.DataSource = (object) institutionsPermissions;
        this.WSGridView.DataBind();
      }
    }

    protected void btnPotvrdi_Click(object sender, EventArgs e)
    {
      HelpClassWebServices classWebServices = (HelpClassWebServices) this.Application["TempWebService"];
      List<HelpClassUsers> webServiceAndUsage = new UsersDAL().GetUsersByWebServiceAndUsage(classWebServices.ID, 1);
      bool flag1 = false;
      if (webServiceAndUsage.Count != 0)
      {
        foreach (HelpClassUsers helpClassUsers in webServiceAndUsage)
        {
          USER user = new UsersDAL().CheckUser(this.txtLadminUserName.Text, new Crypto().EncryptStringAES(this.txtLadminPass.Text, ConfigurationManager.AppSettings["PssCrypto"]));
          List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
          bool flag2 = false;
          foreach (PERMISSION permission in permisionsByUser)
          {
            if (permission.ID_WS == classWebServices.ID && permission.Usage == 1)
              flag2 = true;
          }
          string str = new Crypto().EncryptStringAES(this.txtAdminPass.Text, ConfigurationManager.AppSettings["PssCrypto"]);
          if (helpClassUsers.user == this.txtAdminUserName.Text && helpClassUsers.pass == str && (helpClassUsers.Type == 1 && user != null) && (user.Type == 2 && flag2) && this.txtAdminUserName.Text != this.txtLadminUserName.Text)
          {
            flag1 = true;
            break;
          }
        }
      }
      if (flag1)
      {
        bool flag2 = false;
        new WebservicesDAL().Update(this.txtName.Text, this.txtDescription.Text, this.txtNote.Text, this.txtURL.Text, new bool?(flag2), new DateTime?(), classWebServices.ID);
        string old = classWebServices.Tittle + ";" + classWebServices.Description + ";" + classWebServices.Note + ";" + classWebServices.URL + ";" + classWebServices.Active.ToString();
        string newone = this.txtName.Text + ";" + this.txtDescription.Text + ";" + this.txtNote.Text + ";" + this.txtURL.Text + ";" + flag2.ToString();
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICES")).Key, (USER) this.Session["user"], classWebServices.ID.ToString(), 2, DateTime.Now, old, newone);
        this.Application["ListWebServices"] = (object) new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
        this.WSGridView.DataSource = (object) (List<HelpClassWebServices>) this.Application["ListWebServices"];
        this.WSGridView.DataBind();
        this.DropDownList3.SelectedIndex = -1;
        this.PanelServiceDeactive.Visible = false;
      }
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.PanelEditUser.CssClass = "content-box column-right";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.PanelServiceDeactive.Visible = false;
      this.CheckBoxTrue.Checked = true;
      this.CheckBoxFalse.Checked = false;
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
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(false);
      this.Application["ListWebServices"] = (object) institutionsPermissions;
      this.WSGridView.DataSource = (object) institutionsPermissions;
      this.WSGridView.DataBind();
    }

    protected void LinkButton3_Click1(object sender, EventArgs e)
    {
      List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
      this.Application["ListWebServices"] = (object) institutionsPermissions;
      this.WSGridView.DataSource = (object) institutionsPermissions;
      this.WSGridView.DataBind();
    }

    protected void WSGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.WSGridView.PageIndex = e.NewPageIndex;
      this.WSGridView.DataBind();
    }

    protected void GridviewOsnoviNaBaranje_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      BASIS c = (BASIS) e.Row.DataItem;
      if (c != null)
      {
        imageButton.Attributes.Add("1", c.ID.ToString());
        List<BASIS> list = (List<BASIS>) this.Session["ListOsnoviSelected"];
        try
        {
          Enumerable.First<BASIS>((IEnumerable<BASIS>) list, (Func<BASIS, bool>) (p => p.ID == c.ID));
          imageButton.ImageUrl = "../rerources/images/add.png";
          imageButton.CommandName = "DeleteWS";
        }
        catch
        {
          imageButton.CommandName = "InsertWS";
        }
      }
    }

    protected void GridviewOsnoviNaBaranje_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "DeleteWS")
      {
        this.PanelNewUser.CssClass = "content-box column-left";
        this.PanelEditUser.CssClass = "content-box column-right closed-box";
        long num = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
        List<BASIS> list = (List<BASIS>) this.Session["ListOsnoviAll"];
        foreach (BASIS basis in list)
        {
          if (basis.ID == num)
          {
            list.Remove(basis);
            break;
          }
        }
        this.Session["ListOsnoviSelected"] = (object) list;
      }
      else
      {
        if (!(e.CommandName == "InsertWS"))
          return;
        this.PanelNewUser.CssClass = "content-box column-left";
        this.PanelEditUser.CssClass = "content-box column-right closed-box";
        BASIS byId = new BasisDAL().GetByID(Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]));
        List<BASIS> list = (List<BASIS>) this.Session["ListOsnoviSelected"];
        list.Add(byId);
        this.Session["ListOsnoviSelected"] = (object) list;
      }
    }

    protected void GridviewOsnoviNaBaranjeChange_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      BASIS c = (BASIS) e.Row.DataItem;
      if (c != null)
      {
        imageButton.Attributes.Add("1", c.ID.ToString());
        List<WEBSERVICESBASIS> list = (List<WEBSERVICESBASIS>) this.Session["SelectedWSBase"];
        try
        {
          Enumerable.First<WEBSERVICESBASIS>((IEnumerable<WEBSERVICESBASIS>) list, (Func<WEBSERVICESBASIS, bool>) (p => p.ID_Basis == c.ID));
          imageButton.ImageUrl = "../rerources/images/add.png";
          imageButton.CommandName = "DeleteWS";
        }
        catch
        {
          imageButton.CommandName = "InsertWS";
        }
      }
    }
  }
}
