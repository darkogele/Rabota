// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.users
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using InteropClassLibrary;
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
  public class users : UserControl
  {
    private IRepository classobj = (IRepository) new InteropClassLibrary.InteropDAL(0);
    protected Image Image1;
    protected TextBox TextBox1;
    protected LinkButton LinkButton1;
    protected LinkButton LinkButton4;
    protected DropDownList DropDownList3;
    protected GridView CustomersGridView;
    protected Panel PanelNewUser;
    protected Image Image3;
    protected Label Label1;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected Label Label2;
    protected TextBox txtSurname;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected Label Label3;
    protected TextBox txtemail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected Label Label14;
    protected TextBox txtIP;
    protected Label Label8;
    protected DropDownList DropDownInst;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected Label Label5;
    protected DropDownList DropDownListCert;
    protected RequiredFieldValidator RequiredFieldValidator11;
    protected Label Label6;
    protected TextBox txtUsername;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected Label lblUserExist;
    protected CheckBoxList CheckBoxListWS;
    protected GridView Gridview1;
    protected GridView GridViewOtherServices;
    protected Label Label7;
    protected TextBox txtPassword;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected RegularExpressionValidator RegularExpressionValidator6;
    protected Label lblConfirmPassword;
    protected TextBox txtConfirmPassword;
    protected RequiredFieldValidator RequiredFieldValidator10;
    protected CompareValidator CompareValidator1;
    protected LinkButton LinkButton2;
    protected Panel PanelEditUser;
    protected Image Image2;
    protected Label Label4;
    protected TextBox txtPName;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected Label lb5;
    protected TextBox txtPSurname;
    protected RequiredFieldValidator RequiredFieldValidator7;
    protected Label Label9;
    protected TextBox txtPmail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected Label Label15;
    protected TextBox txtPip;
    protected Label Label10;
    protected TextBox txtPusername;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected Label Label11;
    protected TextBox txtPpassword;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected RegularExpressionValidator RegularExpressionValidator7;
    protected Label lblConfirmPPassword;
    protected TextBox txtConfirmPPassword;
    protected CompareValidator CompareValidator2;
    protected Label Label12;
    protected TextBox txtInstP;
    protected Label Label13;
    protected DropDownList DropDownListPCert;
    protected RequiredFieldValidator RequiredFieldValidator12;
    protected CheckBoxList CheckBoxListWSP;
    protected CheckBox CheckBoxPTrue;
    protected CheckBox CheckBoxPFalse;
    protected LinkButton LinkButton3;
    private bool _InstStatus;

    private bool InstStatus
    {
      get
      {
        return this._InstStatus;
      }
      set
      {
        this._InstStatus = value;
      }
    }

    private void EnableTxtBox()
    {
      this.txtName.Enabled = true;
      this.txtSurname.Enabled = true;
      this.txtemail.Enabled = true;
      this.txtIP.Enabled = true;
      this.txtUsername.Enabled = true;
      this.txtPassword.Enabled = true;
    }

    private void DisableTxtBox()
    {
      this.txtName.Enabled = false;
      this.txtSurname.Enabled = false;
      this.txtemail.Enabled = false;
      this.txtIP.Enabled = false;
      this.txtUsername.Enabled = false;
      this.txtPassword.Enabled = false;
      this.DropDownInst.Enabled = false;
    }

    private void ClearTxtBox()
    {
      this.txtName.Text = "";
      this.txtSurname.Text = "";
      this.txtemail.Text = "";
      this.txtIP.Text = "";
      this.txtUsername.Text = "";
      this.txtPassword.Text = "";
    }

    private void ClearTxtBoxChange()
    {
      this.txtPName.Text = "";
      this.txtPSurname.Text = "";
      this.txtPmail.Text = "";
      this.txtPip.Text = "";
      this.txtPusername.Text = "";
      this.txtPpassword.Text = "";
      this.txtInstP.Text = "";
      this.CheckBoxPFalse.Checked = false;
      this.CheckBoxPTrue.Checked = false;
    }

    private void ClearStyleSubmeni()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpAdministratori.aspx";
      this.PageLoadStaf();
      if (this.Session["AdminUser"] == null)
        return;
      this.Session["AdminUser"] = (object) null;
      this.PanelNewUser.CssClass = "content-box column-left";
    }

    public void PageLoadStaf()
    {
      string selectedValue1 = this.DropDownInst.SelectedValue;
      string selectedValue2 = this.DropDownListCert.SelectedValue;
      this.DropDownInst.Items.Clear();
      this.DropDownInst.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Листај по институција --"
      });
      this.DropDownListCert.Items.Clear();
      this.DropDownListCert.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Листај сертификати --"
      });
      List<HelpClassUsers> list1 = Enumerable.ToList<HelpClassUsers>(Enumerable.Distinct<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissions"]));
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
        if (list2.Count == 0)
          list2.AddRange((IEnumerable<HelpClassUsers>) list1);
        this.Application["ListUsersPermissions"] = (object) list2;
        this.CustomersGridView.DataSource = (object) list2;
        this.CustomersGridView.DataBind();
      }
      else
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.user.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list2.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list2.Add(helpClassUsers);
        }
        if (list2.Count == 0)
          list2.AddRange((IEnumerable<HelpClassUsers>) list1);
        this.Application["ListUsersPermissions"] = (object) list2;
        this.CustomersGridView.DataSource = (object) list2;
        this.CustomersGridView.DataBind();
      }
      List<interop.INSTITUTION> list3 = new List<interop.INSTITUTION>();
      foreach (interop.INSTITUTION institution in (List<interop.INSTITUTION>) this.Application["DropDownInstitutions"])
      {
        if (institution.Active)
          list3.Add(institution);
      }
      this.DropDownInst.DataSource = (object) list3;
      this.DropDownInst.DataBind();
      this.DropDownInst.SelectedValue = selectedValue1;
      this.DropDownListCert.DataSource = (object) new CertificatesDAL().GetAllCertificates();
      this.DropDownListCert.DataBind();
      this.DropDownListCert.SelectedValue = selectedValue2;
      if ((HelpClassUsers) this.Session["AdminSelectedUser"] != null)
      {
        HelpClassUsers TempUser = (HelpClassUsers) this.Session["AdminSelectedUser"];
        this.Application["TempUser"] = (object) TempUser;
        this.Session["AdminSelectedUser"] = (object) null;
        this.FillUser(TempUser);
      }
      this.Gridview1.DataSource = (object) (List<interop.WEBSERVICE>) this.Session["ListWSAllInst"];
      this.Gridview1.DataBind();
      this.GridViewOtherServices.DataSource = (object) (List<interop.WEBSERVICE>) this.Session["ListWSCombinedHelp"];
      this.GridViewOtherServices.DataBind();
      this.CheckBoxListWS.Visible = false;
      this.lblUserExist.Visible = false;
    }

    protected void CustomersGridView_SelectedIndexChanged1(object sender, EventArgs e)
    {
      this.ClearTxtBoxChange();
      Guid row = (Guid) this.CustomersGridView.SelectedDataKey.Value;
      HelpClassUsers TempUser = Enumerable.Single<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissions"], (Func<HelpClassUsers, bool>) (p => p.ID == row));
      this.Application["TempUser"] = (object) TempUser;
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.FillUser(TempUser);
    }

    public void FillUser(HelpClassUsers TempUser)
    {
      this.DropDownListPCert.Items.Clear();
      this.DropDownListPCert.DataSource = (object) new CertificatesDAL().GetAllCertificates();
      this.DropDownListPCert.DataBind();
      try
      {
        this.DropDownListPCert.SelectedValue = TempUser.ID_Cert.ToString();
      }
      catch
      {
      }
      this.txtPName.Text = TempUser.Name;
      this.txtPSurname.Text = TempUser.Surname;
      this.txtPmail.Text = TempUser.Email;
      this.txtPip.Text = TempUser.ip;
      this.txtPusername.Text = TempUser.user;
      string str = new Crypto().DecryptStringAES(TempUser.pass, ConfigurationManager.AppSettings["PssCrypto"]);
      this.txtPpassword.Attributes["value"] = str;
      this.txtConfirmPPassword.Attributes["value"] = str;
      if (TempUser.InstitutionName != null)
        this.txtInstP.Text = TempUser.InstitutionName.ToString();
      if (TempUser.Active)
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
    }

    protected void btn_promeni_Click(object sender, EventArgs e)
    {
      if (this.txtPpassword.Text == string.Empty)
      {
        this.CompareValidator2.Text = "Внесете лозинка!";
      }
      else
      {
        HelpClassUsers helpClassUsers = (HelpClassUsers) this.Application["TempUser"];
        bool flag = false;
        if (this.CheckBoxPTrue.Checked)
          flag = true;
        string pass = !(this.txtConfirmPPassword.Text != string.Empty) ? this.txtPpassword.Text : new Crypto().EncryptStringAES(this.txtPpassword.Text, ConfigurationManager.AppSettings["PssCrypto"]);
        new UsersDAL().Update(this.txtPName.Text, this.txtPSurname.Text, new long?(Convert.ToInt64(this.DropDownListPCert.SelectedValue)), this.txtPmail.Text, new bool?(flag), new DateTime?(), this.txtPusername.Text, pass, new int?(), this.txtPip.Text, helpClassUsers.ID);
        string old = helpClassUsers.Name + ";" + helpClassUsers.Surname + ";" + helpClassUsers.Email + ";" + helpClassUsers.user + ";" + helpClassUsers.pass + ";" + helpClassUsers.Active.ToString();
        string newone = this.txtPName.Text + ";" + this.txtPSurname.Text + ";" + this.txtPmail.Text + ";" + this.txtPusername.Text + ";" + this.txtPpassword.Text + ";" + flag.ToString();
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "USERS")).Key, (interop.USER) this.Session["user"], helpClassUsers.ID.ToString(), 2, DateTime.Now, old, newone);
        this.Application["ListUsersPermissions"] = (object) new UsersDAL().GetUsersInstitutionsPermissions(2, true);
        this.CustomersGridView.DataSource = (object) (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
        this.CustomersGridView.DataBind();
        this.DropDownList3.SelectedIndex = -1;
        this.PanelNewUser.CssClass = "content-box column-left closed-box";
        this.PanelEditUser.CssClass = "content-box column-left closed-box";
      }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
      this.ClearTxtBox();
      this.DropDownInst.Enabled = true;
      this.DropDownInst.SelectedValue = "0";
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.DropDownInst.Enabled = false;
      this.EnableTxtBox();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
      institutionsPermissions.AddRange((IEnumerable<HelpClassUsers>) new UsersDAL().GetUsersInstitutionsPermissions(2, false));
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) institutionsPermissions;
      this.CustomersGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) institutionsPermissions;
      this.CustomersGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, false);
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) institutionsPermissions;
      this.CustomersGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void btn_vnesi_Click(object sender, EventArgs e)
    {
      if (this.lblUserExist.Visible)
        return;
      bool flag = false;
      if (((List<interop.WEBSERVICE>) this.Session["ListWSInst"]).Count != 0)
        flag = true;
      if (flag)
      {
        bool active = true;
        string pass = new Crypto().EncryptStringAES(this.txtPassword.Text, ConfigurationManager.AppSettings["PssCrypto"]);
        Guid id = new UsersDAL().Insert(this.txtName.Text, this.txtSurname.Text, new long?(Convert.ToInt64(this.DropDownListCert.SelectedValue)), this.txtemail.Text, active, DateTime.Now, this.txtUsername.Text, pass, 2, this.txtIP.Text);
        string old1 = this.txtName.Text + ";" + this.txtSurname.Text + ";" + this.txtemail.Text + ";" + this.txtUsername.Text + ";" + this.txtPassword.Text + ";" + active.ToString();
        string newone1 = "";
        KeyValuePair<int, string> keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "USERS"));
        new LOGDAL().Insert(keyValuePair.Key, (interop.USER) this.Session["user"], id.ToString(), 1, DateTime.Now, old1, newone1);
        interop.USER byId1 = new UsersDAL().GetByID(id);
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(this.DropDownInst.SelectedValue));
        foreach (interop.WEBSERVICE ws in (List<interop.WEBSERVICE>) this.Session["ListWSInst"])
        {
          long num = !((List<interop.WEBSERVICE>) this.Session["ListWSAllInst"]).Contains(ws) ? new PermissionsDAL().Insert(byId2, byId1, ws, 2, true, DateTime.Now) : new PermissionsDAL().Insert(byId2, byId1, ws, 1, true, DateTime.Now);
          string old2 = "";
          string newone2 = "";
          keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS"));
          new LOGDAL().Insert(keyValuePair.Key, (interop.USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old2, newone2);
        }
        this.ClearTxtBox();
      }
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.Application["ListUsersPermissionsPerm"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
      this.CustomersGridView.DataBind();
      this.DropDownList3.SelectedIndex = -1;
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    protected void CheckBoxTrue_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.txtPassword.Text = this.txtPassword.Text;
    }

    protected void CheckBoxFalse_CheckedChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
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

    protected void DropDownInst_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Session["UN"] = (object) this.txtUsername.Text;
      this.Session["UNPass"] = (object) this.txtPassword.Text;
      this.Session["InstitutionTag"] = (object) false;
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (this.DropDownInst.SelectedValue != "0")
      {
        this.CheckBoxListWS.Items.Clear();
        List<interop.WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(new InstitutionsDAL().GetByID((long) Convert.ToInt32(this.DropDownInst.SelectedValue)));
        this.CheckBoxListWS.DataSource = (object) servicesByIstitution;
        this.CheckBoxListWS.DataBind();
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution);
        this.Session["ListWSAllInst"] = (object) servicesByIstitution;
        this.Session["ListWSInst"] = (object) list1;
        for (int index = 0; index < this.CheckBoxListWS.Items.Count; ++index)
          this.CheckBoxListWS.Items[index].Selected = true;
        this.Gridview1.DataSource = (object) servicesByIstitution;
        this.Gridview1.DataBind();
        this.Gridview1.Visible = true;
        List<interop.WEBSERVICE> allActiveDeleted = new WebservicesDAL().GetAllActiveDeleted(true);
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) allActiveDeleted);
        foreach (interop.WEBSERVICE webservice1 in (List<interop.WEBSERVICE>) this.Session["ListWSAllInst"])
        {
          foreach (interop.WEBSERVICE webservice2 in allActiveDeleted)
          {
            if (webservice1.ID == webservice2.ID)
              list2.Remove(webservice2);
          }
        }
        new List<interop.WEBSERVICE>().AddRange((IEnumerable<interop.WEBSERVICE>) allActiveDeleted);
        this.Session["ListWSCombinedHelp"] = (object) list2;
        this.GridViewOtherServices.DataSource = (object) list2;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
        this.Session["InstitutionTag"] = (object) true;
        this.InstStatus = true;
      }
      else
        this.CheckBoxListWS.Items.Clear();
      this.CheckBoxListWS.Visible = false;
    }

    private void Institution(interop.INSTITUTION inst, List<interop.WEBSERVICE> ListWSHelp)
    {
      if (inst.Tittle == "Агенција за катастар на недвижности")
      {
        interop.INSTITUTION byId1 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["CRID"])));
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["MVRID"])));
        List<interop.WEBSERVICE> servicesByIstitution1 = new WebservicesDAL().GetServicesByIstitution(byId1);
        List<interop.WEBSERVICE> servicesByIstitution2 = new WebservicesDAL().GetServicesByIstitution(byId2);
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        foreach (interop.WEBSERVICE webservice in servicesByIstitution1)
        {
          if (webservice.Tittle == "Podatoci za MBPL za potrebite na AKN")
            list1.Add(webservice);
        }
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution2);
        List<interop.WEBSERVICE> list3 = new List<interop.WEBSERVICE>();
        list3.AddRange((IEnumerable<interop.WEBSERVICE>) list2);
        ListWSHelp.AddRange((IEnumerable<interop.WEBSERVICE>) list2);
        this.Session["ListWSInst"] = (object) ListWSHelp;
        this.Session["ListWSCombinedHelp"] = (object) list3;
        this.GridViewOtherServices.DataSource = (object) list3;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
      }
      else if (inst.Tittle == "Управа за јавни приходи")
      {
        interop.INSTITUTION byId1 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["CRID"])));
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["MVRID"])));
        interop.INSTITUTION byId3 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["AKNID"])));
        List<interop.WEBSERVICE> servicesByIstitution1 = new WebservicesDAL().GetServicesByIstitution(byId1);
        List<interop.WEBSERVICE> servicesByIstitution2 = new WebservicesDAL().GetServicesByIstitution(byId2);
        List<interop.WEBSERVICE> servicesByIstitution3 = new WebservicesDAL().GetServicesByIstitution(byId3);
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution1);
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution2);
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution3);
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        ListWSHelp.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        this.Session["ListWSInst"] = (object) ListWSHelp;
        this.Session["ListWSCombinedHelp"] = (object) list2;
        this.GridViewOtherServices.DataSource = (object) list2;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
      }
      else if (inst.Tittle == "Централен Регистар")
      {
        interop.INSTITUTION byId1 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["MVRID"])));
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["AKNID"])));
        List<interop.WEBSERVICE> servicesByIstitution1 = new WebservicesDAL().GetServicesByIstitution(byId1);
        List<interop.WEBSERVICE> servicesByIstitution2 = new WebservicesDAL().GetServicesByIstitution(byId2);
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution1);
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution2);
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        ListWSHelp.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        this.Session["ListWSInst"] = (object) ListWSHelp;
        this.Session["ListWSCombinedHelp"] = (object) list2;
        this.GridViewOtherServices.DataSource = (object) list2;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
      }
      else if (inst.Tittle == "Царинска управа")
      {
        interop.INSTITUTION byId1 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["MVRID"])));
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["CRID"])));
        List<interop.WEBSERVICE> servicesByIstitution1 = new WebservicesDAL().GetServicesByIstitution(byId1);
        List<interop.WEBSERVICE> servicesByIstitution2 = new WebservicesDAL().GetServicesByIstitution(byId2);
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution1);
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution2);
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        ListWSHelp.AddRange((IEnumerable<interop.WEBSERVICE>) list1);
        this.Session["ListWSInst"] = (object) ListWSHelp;
        this.Session["ListWSCombinedHelp"] = (object) list2;
        this.GridViewOtherServices.DataSource = (object) list2;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
      }
      else
      {
        if (!(inst.Tittle == "Министерство за Внатрешни Работи"))
          return;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = false;
      }
    }

    public void PopolniTreeView(List<HelpClassUsers> lista)
    {
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      string[] ImePrezime = this.TextBox1.Text.Split(' ');
      if (Enumerable.Count<string>((IEnumerable<string>) ImePrezime) == 2)
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) institutionsPermissions, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(ImePrezime[0].ToUpper()) && p.Surname.ToUpper().Contains(ImePrezime[1].ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list.Add(helpClassUsers);
        }
        if (list.Count == 0)
          list.AddRange((IEnumerable<HelpClassUsers>) institutionsPermissions);
        this.Application["ListUsersPermissions"] = (object) list;
        this.CustomersGridView.DataSource = (object) list;
        this.CustomersGridView.DataBind();
      }
      else
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) institutionsPermissions, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.user.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list.Add(helpClassUsers);
        }
        if (list.Count == 0)
          list.AddRange((IEnumerable<HelpClassUsers>) institutionsPermissions);
        this.Application["ListUsersPermissions"] = (object) list;
        this.CustomersGridView.DataSource = (object) list;
        this.CustomersGridView.DataBind();
      }
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "DeleteWS")
      {
        long num = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
        List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
        if (list.Count > 1)
        {
          foreach (interop.WEBSERVICE webservice in list)
          {
            if (webservice.ID == num)
            {
              list.Remove(webservice);
              break;
            }
          }
        }
        this.Session["ListWSInst"] = (object) list;
        this.Gridview1.DataBind();
      }
      else
      {
        if (!(e.CommandName == "InsertWS"))
          return;
        interop.WEBSERVICE byId = new WebservicesDAL().GetByID(Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]));
        List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
        list.Add(byId);
        this.Session["ListWSInst"] = (object) list;
        this.Gridview1.DataBind();
      }
    }

    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      interop.WEBSERVICE c = (interop.WEBSERVICE) e.Row.DataItem;
      if (c != null)
      {
        imageButton.Attributes.Add("1", c.ID.ToString());
        List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
        try
        {
          Enumerable.First<interop.WEBSERVICE>((IEnumerable<interop.WEBSERVICE>) list, (Func<interop.WEBSERVICE, bool>) (p => p.ID == c.ID));
          imageButton.ImageUrl = "../rerources/images/add.png";
          imageButton.CommandName = "DeleteWS";
        }
        catch
        {
          imageButton.CommandName = "InsertWS";
        }
      }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.DropDownList3.SelectedValue == "1")
      {
        List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, false);
        this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
        this.CustomersGridView.DataSource = (object) institutionsPermissions;
        this.CustomersGridView.DataBind();
      }
      else
      {
        List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
        this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
        this.CustomersGridView.DataSource = (object) institutionsPermissions;
        this.CustomersGridView.DataBind();
      }
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
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, false);
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) institutionsPermissions;
      this.CustomersGridView.DataBind();
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
      List<HelpClassUsers> institutionsPermissions = new UsersDAL().GetUsersInstitutionsPermissions(2, true);
      this.Application["ListUsersPermissions"] = (object) institutionsPermissions;
      this.CustomersGridView.DataSource = (object) institutionsPermissions;
      this.CustomersGridView.DataBind();
    }

    protected void CustomersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.CustomersGridView.PageIndex = e.NewPageIndex;
      this.CustomersGridView.DataBind();
      this.CustomersGridView.SelectedRowStyle.Reset();
      this.CustomersGridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.CustomersGridView.RowStyle.CssClass = "RowStyle";
      this.PanelEditUser.CssClass = "content-box column-left closed-box";
      this.ClearTxtBoxChange();
    }

    protected void GridViewOtherServices_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "DeleteWS")
      {
        long num = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
        List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
        if (list.Count > 1)
        {
          foreach (interop.WEBSERVICE webservice in list)
          {
            if (webservice.ID == num)
            {
              list.Remove(webservice);
              break;
            }
          }
        }
        this.Session["ListWSInst"] = (object) list;
        this.GridViewOtherServices.DataBind();
      }
      else
      {
        if (!(e.CommandName == "InsertWS"))
          return;
        interop.WEBSERVICE byId = new WebservicesDAL().GetByID(Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]));
        List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
        list.Add(byId);
        this.Session["ListWSInst"] = (object) list;
        this.GridViewOtherServices.DataBind();
      }
    }

    protected void GridViewOtherServices_RowCreated1(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType != DataControlRowType.DataRow)
          return;
        ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
        interop.WEBSERVICE c = (interop.WEBSERVICE) e.Row.DataItem;
        if (c != null)
        {
          if ((bool) this.Session["InstitutionTag"])
          {
            imageButton.Attributes.Add("1", c.ID.ToString());
            List<interop.WEBSERVICE> list = (List<interop.WEBSERVICE>) this.Session["ListWSInst"];
            try
            {
              Enumerable.Single<interop.WEBSERVICE>((IEnumerable<interop.WEBSERVICE>) list, (Func<interop.WEBSERVICE, bool>) (p => p.ID == c.ID));
              imageButton.ImageUrl = "../rerources/images/add.png";
              imageButton.CommandName = "DeleteWS";
            }
            catch
            {
              imageButton.CommandName = "InsertWS";
            }
          }
          else
            imageButton.CommandName = "InsertWS";
        }
      }
      catch
      {
      }
    }

    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
      this.Session["newpass"] = (object) this.txtPassword.Text;
    }

    protected void txtUsername_TextChanged(object sender, EventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      bool? nullable1 = new UsersDAL().CheckUserNameExists(this.txtUsername.Text);
      bool? nullable2;
      int num1;
      if (nullable1.HasValue)
      {
        nullable2 = nullable1;
        num1 = (!nullable2.GetValueOrDefault() ? 0 : (nullable2.HasValue ? 1 : 0)) == 0 ? 1 : 0;
      }
      else
        num1 = 1;
      if (num1 == 0)
      {
        this.lblUserExist.Visible = true;
      }
      else
      {
        int num2;
        if (nullable1.HasValue)
        {
          nullable2 = nullable1;
          num2 = (nullable2.GetValueOrDefault() ? 0 : (nullable2.HasValue ? 1 : 0)) == 0 ? 1 : 0;
        }
        else
          num2 = 1;
        if (num2 == 0)
        {
          this.lblUserExist.Visible = false;
        }
        else
        {
          if (nullable1.HasValue)
            return;
          this.lblUserExist.Visible = true;
        }
      }
    }

    protected void CustomersGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
      List<HelpClassUsers> list1 = (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
      List<HelpClassUsers> list2 = new List<HelpClassUsers>();
      if (e.SortExpression == "FirstName")
      {
        if (this.Session["sort"] == "ascending" || this.Session["sort"] == null)
        {
          foreach (HelpClassUsers helpClassUsers in (IEnumerable<HelpClassUsers>) Enumerable.OrderBy<HelpClassUsers, string>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, string>) (p => p.Name)))
            list2.Add(helpClassUsers);
          this.Application["ListUsersPermissions"] = (object) list2;
          this.CustomersGridView.DataSource = (object) list2;
          this.CustomersGridView.DataBind();
          this.Session["sort"] = (object) "descending";
        }
        else
        {
          foreach (HelpClassUsers helpClassUsers in (IEnumerable<HelpClassUsers>) Enumerable.OrderByDescending<HelpClassUsers, string>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, string>) (p => p.Name)))
            list2.Add(helpClassUsers);
          this.Application["ListUsersPermissions"] = (object) list2;
          this.CustomersGridView.DataSource = (object) list2;
          this.CustomersGridView.DataBind();
          this.Session["sort"] = (object) "ascending";
        }
      }
      else
      {
        if (!(e.SortExpression == "MiddleName"))
          return;
        if (this.Session["sort"] == "ascending" || this.Session["sort"] == null)
        {
          foreach (HelpClassUsers helpClassUsers in (IEnumerable<HelpClassUsers>) Enumerable.OrderBy<HelpClassUsers, string>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, string>) (p => p.Surname)))
            list2.Add(helpClassUsers);
          this.Application["ListUsersPermissions"] = (object) list2;
          this.CustomersGridView.DataSource = (object) list2;
          this.CustomersGridView.DataBind();
          this.Session["sort"] = (object) "descending";
        }
        else
        {
          foreach (HelpClassUsers helpClassUsers in (IEnumerable<HelpClassUsers>) Enumerable.OrderByDescending<HelpClassUsers, string>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, string>) (p => p.Surname)))
            list2.Add(helpClassUsers);
          this.Application["ListUsersPermissions"] = (object) list2;
          this.CustomersGridView.DataSource = (object) list2;
          this.CustomersGridView.DataBind();
          this.Session["sort"] = (object) "ascending";
        }
      }
    }

    protected void DropDownListCert_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Session["UN"] = (object) this.txtUsername.Text;
      this.Session["UNPass"] = (object) this.txtPassword.Text;
      this.Session["InstitutionTag"] = (object) false;
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      if (this.DropDownInst.SelectedValue != "0")
      {
        this.CheckBoxListWS.Items.Clear();
        List<interop.WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(new InstitutionsDAL().GetByID((long) Convert.ToInt32(this.DropDownInst.SelectedValue)));
        this.CheckBoxListWS.DataSource = (object) servicesByIstitution;
        this.CheckBoxListWS.DataBind();
        List<interop.WEBSERVICE> list1 = new List<interop.WEBSERVICE>();
        list1.AddRange((IEnumerable<interop.WEBSERVICE>) servicesByIstitution);
        this.Session["ListWSAllInst"] = (object) servicesByIstitution;
        this.Session["ListWSInst"] = (object) list1;
        for (int index = 0; index < this.CheckBoxListWS.Items.Count; ++index)
          this.CheckBoxListWS.Items[index].Selected = true;
        this.Gridview1.DataSource = (object) servicesByIstitution;
        this.Gridview1.DataBind();
        this.Gridview1.Visible = true;
        List<interop.WEBSERVICE> allActiveDeleted = new WebservicesDAL().GetAllActiveDeleted(true);
        List<interop.WEBSERVICE> list2 = new List<interop.WEBSERVICE>();
        list2.AddRange((IEnumerable<interop.WEBSERVICE>) allActiveDeleted);
        foreach (interop.WEBSERVICE webservice1 in (List<interop.WEBSERVICE>) this.Session["ListWSAllInst"])
        {
          foreach (interop.WEBSERVICE webservice2 in allActiveDeleted)
          {
            if (webservice1.ID == webservice2.ID)
              list2.Remove(webservice2);
          }
        }
        new List<interop.WEBSERVICE>().AddRange((IEnumerable<interop.WEBSERVICE>) allActiveDeleted);
        this.Session["ListWSCombinedHelp"] = (object) list2;
        this.GridViewOtherServices.DataSource = (object) list2;
        this.GridViewOtherServices.DataBind();
        this.GridViewOtherServices.Visible = true;
        this.Session["InstitutionTag"] = (object) true;
        this.InstStatus = true;
      }
      else
        this.CheckBoxListWS.Items.Clear();
      this.CheckBoxListWS.Visible = false;
    }

    protected void GetRegularValidator(RegularExpressionValidator req1, RegularExpressionValidator req2)
    {
      this.RegularExpressionValidator6.ValidationExpression = "(?=^[^\\s]{6,11}$)(?=.*\\d.*)(?=.*[a-zA-Z].*)(?=.*[@#$%^&*/.]{1,}).*";
      this.RegularExpressionValidator6.ErrorMessage = "Не се исполнува соодветниот формат";
      try
      {
        PASSWORD_SETTING passwordSetting = Queryable.First<PASSWORD_SETTING>(this.classobj.Password_settings);
        req1.ErrorMessage = passwordSetting.ErrorMessage;
        req2.ErrorMessage = passwordSetting.ErrorMessage;
        string str = "(?=^[^\\s]{" + (object) passwordSetting.CharactersNumberMin + "," + (string) (object) passwordSetting.CharactersNumberMax + "}$";
        if (passwordSetting.AlphaNumeric)
          str += "(?=.*\\d.*)(?=.*[a-zA-Z].*)";
        if (passwordSetting.SpecialCharactersNumber != 0)
          str = string.Concat(new object[4]
          {
            (object) str,
            (object) "(?=.*[@#$%^&*/.]{",
            (object) passwordSetting.SpecialCharactersNumber,
            (object) ",})"
          });
        req1.ValidationExpression = str;
        req2.ValidationExpression = str;
      }
      catch
      {
      }
    }
  }
}
