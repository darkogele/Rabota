// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.usersl
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

namespace interop.LocalAdminPanel
{
  public class usersl : UserControl
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
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected Label Label2;
    protected TextBox txtSurname;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected Label Label3;
    protected TextBox txtemail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected Label Label5;
    protected TextBox txtIP;
    protected Label Label8;
    protected DropDownList DropDownInst;
    protected Label Label13;
    protected DropDownList DropDownListCert;
    protected Label Label6;
    protected TextBox txtUsername;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected Label lblUserExist;
    protected Label Label7;
    protected TextBox txtPassword;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected RegularExpressionValidator RegularExpressionValidator6;
    protected Label lblConfirmPassword;
    protected TextBox txtConfirmPassword;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected CompareValidator CompareValidator1;
    protected CheckBoxList CheckBoxListWS;
    protected GridView Gridview1;
    protected LinkButton LinkButton2;
    protected Panel PanelEditUser;
    protected Image Image2;
    protected Label Label4;
    protected TextBox txtPName;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected Label lb5;
    protected TextBox txtPSurname;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected Label Label9;
    protected TextBox txtPmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected Label Label15;
    protected TextBox txtPip;
    protected Label Label10;
    protected TextBox txtPusername;
    protected RequiredFieldValidator RequiredFieldValidator7;
    protected Label Label11;
    protected TextBox txtPpassword;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected Label lblConfirmPPAssword;
    protected TextBox txtConfirmPPassword;
    protected CompareValidator CompareValidator2;
    protected Label Label12;
    protected TextBox txtInstP;
    protected Label Label14;
    protected DropDownList DropDownListPCert;
    protected CheckBoxList CheckBoxListWSP;
    protected TreeView TreeView2;
    protected CheckBox CheckBoxPTrue;
    protected CheckBox CheckBoxPFalse;
    protected LinkButton LinkButton3;

    private void EnableTxtBox()
    {
      this.txtName.Enabled = true;
      this.txtSurname.Enabled = true;
      this.txtemail.Enabled = true;
      this.txtIP.Enabled = true;
      this.txtUsername.Enabled = true;
      this.txtPassword.Enabled = true;
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
      for (int index = 0; index < this.CheckBoxListWS.Items.Count; ++index)
        this.CheckBoxListWS.Items[index].Selected = false;
    }

    private void ClearStyleSubmeni()
    {
      this.LinkButton3.CssClass = "none";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/HelpKorisnici.aspx";
      this.PageLoadStaf();
      if (this.Session["LocalUser"] == null)
        return;
      this.Session["LocalUser"] = (object) null;
      this.PanelNewUser.CssClass = "content-box column-left";
    }

    public void PageLoadStaf()
    {
      List<HelpClassUsers> list1 = (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
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
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
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
      this.DropDownInst.DataSource = (object) Enumerable.Distinct<interop.INSTITUTION>((IEnumerable<interop.INSTITUTION>) list3);
      this.DropDownInst.DataBind();
      this.DropDownInst.SelectedIndex = 0;
      this.DropDownListCert.Items.Add(new ListItem()
      {
        Value = ((interop.USER) this.Session["user"]).ID_CERT.ToString(),
        Text = new CertificatesDAL().GetByUser((interop.USER) this.Session["user"]).Subject
      });
      if ((HelpClassUsers) this.Session["LocalSelectedUser"] != null)
      {
        HelpClassUsers TempUser = (HelpClassUsers) this.Session["LocalSelectedUser"];
        this.Application["TempUser"] = (object) TempUser;
        this.Session["LocalSelectedUser"] = (object) null;
        this.FillUser(TempUser);
      }
      this.Gridview1.DataSource = (object) (List<interop.WEBSERVICE>) this.Session["ListWSAllInst"];
      this.Gridview1.DataBind();
      this.CheckBoxListWS.Visible = false;
    }

    protected void CustomersGridView_SelectedIndexChanged1(object sender, EventArgs e)
    {
      Guid row = (Guid) this.CustomersGridView.SelectedDataKey.Value;
      HelpClassUsers TempUser = Enumerable.Single<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Application["ListUsersPermissions"], (Func<HelpClassUsers, bool>) (p => p.ID == row));
      this.Application["TempUser"] = (object) TempUser;
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
      this.FillUser(TempUser);
    }

    public void FillUser(HelpClassUsers TempUser)
    {
      this.txtPName.Text = TempUser.Name;
      this.txtPSurname.Text = TempUser.Surname;
      this.txtPmail.Text = TempUser.Email;
      this.txtIP.Text = TempUser.ip;
      this.txtPusername.Text = TempUser.user;
      this.DropDownListPCert.Items.Add(new ListItem()
      {
        Value = TempUser.ID_Cert.ToString(),
        Text = new CertificatesDAL().GetByUser(new UsersDAL().GetByID(TempUser.ID)).Subject
      });
      try
      {
        string str = new Crypto().DecryptStringAES(TempUser.pass, ConfigurationManager.AppSettings["PssCrypto"]);
        this.txtPpassword.Attributes["value"] = str;
        this.txtConfirmPPassword.Attributes["value"] = str;
      }
      catch
      {
      }
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
      if (TempUser.Type != 2)
      {
        if (new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION.ID == TempUser.IDInstitution)
          this.LinkButton3.Enabled = true;
      }
      else
        this.LinkButton3.Enabled = false;
      this.CheckBoxListWS.Visible = false;
      this.PanelEditUser.CssClass = "content-box column-right";
    }

    protected void btn_promeni_Click(object sender, EventArgs e)
    {
      HelpClassUsers helpClassUsers1 = (HelpClassUsers) this.Application["TempUser"];
      bool flag = false;
      if (this.CheckBoxPTrue.Checked)
        flag = true;
      string text = this.txtConfirmPPassword.Text;
      string pass = !(this.txtConfirmPPassword.Text != string.Empty) ? this.txtPpassword.Text : new Crypto().EncryptStringAES(this.txtPpassword.Text, ConfigurationManager.AppSettings["PssCrypto"]);
      new UsersDAL().Update(this.txtPName.Text, this.txtPSurname.Text, new long?(), this.txtPmail.Text, new bool?(flag), new DateTime?(), this.txtPusername.Text, pass, new int?(), this.txtPip.Text, helpClassUsers1.ID);
      string old = helpClassUsers1.Name + ";" + helpClassUsers1.Surname + ";" + helpClassUsers1.Email + ";" + helpClassUsers1.user + ";" + helpClassUsers1.pass + ";" + helpClassUsers1.Active.ToString();
      string newone = this.txtPName.Text + ";" + this.txtPSurname.Text + ";" + this.txtPmail.Text + ";" + this.txtPusername.Text + ";" + this.txtPpassword.Text + ";" + flag.ToString();
      new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "USERS")).Key, (interop.USER) this.Session["user"], helpClassUsers1.ID.ToString(), 2, DateTime.Now, old, newone);
      this.Application["ListUsersPermissions"] = (object) new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      List<HelpClassUsers> list1 = (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
      List<HelpClassUsers> list2 = new List<HelpClassUsers>();
      string[] ImePrezime = this.TextBox1.Text.Split(' ');
      if (Enumerable.Count<string>((IEnumerable<string>) ImePrezime) == 2)
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(ImePrezime[0].ToUpper()) && p.Surname.ToUpper().Contains(ImePrezime[1].ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list2.Clear();
          foreach (HelpClassUsers helpClassUsers2 in source)
            list2.Add(helpClassUsers2);
        }
        if (list2.Count == 0)
          list2.AddRange((IEnumerable<HelpClassUsers>) list1);
        this.Application["ListUsersPermissions"] = (object) list2;
        this.CustomersGridView.DataSource = (object) list2;
        this.CustomersGridView.DataBind();
      }
      else
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) list1, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list2.Clear();
          foreach (HelpClassUsers helpClassUsers2 in source)
            list2.Add(helpClassUsers2);
        }
        if (list2.Count == 0)
          list2.AddRange((IEnumerable<HelpClassUsers>) list1);
        this.Application["ListUsersPermissions"] = (object) list2;
        this.CustomersGridView.DataSource = (object) list2;
        this.CustomersGridView.DataBind();
      }
      this.DropDownList3.SelectedIndex = -1;
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
      this.PanelNewUser.CssClass = "content-box column-left closed-box";
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      this.EnableTxtBox();
      this.ClearTxtBox();
      this.DropDownInst.Enabled = true;
      this.CheckBoxListWS.DataSource = (object) (List<interop.WEBSERVICE>) this.Application["ListWebServices"];
      this.CheckBoxListWS.DataBind();
      this.CheckBoxListWS.Visible = true;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.DropDownInst.Enabled = false;
      this.EnableTxtBox();
      this.CheckBoxListWS.Visible = false;
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      List<interop.PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"]);
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, permisionsByUser[0].INSTITUTION);
      permissionsByInstitution.AddRange((IEnumerable<HelpClassUsers>) new UsersDAL().GetUsersPermissionsByInstitution(false, permisionsByUser[0].INSTITUTION));
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.CustomersGridView.DataSource = (object) permissionsByInstitution;
      this.CustomersGridView.DataBind();
      this.ClearStyleSubmeni();
      this.LinkButton3.CssClass = "subselected";
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.CustomersGridView.DataSource = (object) permissionsByInstitution;
      this.CustomersGridView.DataBind();
      this.ClearStyleSubmeni();
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(false, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.CustomersGridView.DataSource = (object) permissionsByInstitution;
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
        Guid id = new UsersDAL().Insert(this.txtName.Text, this.txtSurname.Text, ((interop.USER) this.Session["user"]).ID_CERT, this.txtemail.Text, active, DateTime.Now, this.txtUsername.Text, pass, 3, this.txtIP.Text);
        string old1 = this.txtName.Text + ";" + this.txtSurname.Text + ";" + this.txtemail.Text + ";" + this.txtUsername.Text + ";" + this.txtPassword.Text + ";" + active.ToString();
        string newone1 = "";
        KeyValuePair<int, string> keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "USERS"));
        new LOGDAL().Insert(keyValuePair.Key, (interop.USER) this.Session["user"], id.ToString(), 1, DateTime.Now, old1, newone1);
        interop.USER byId1 = new UsersDAL().GetByID(id);
        interop.INSTITUTION byId2 = new InstitutionsDAL().GetByID((long) Convert.ToInt32(this.DropDownInst.SelectedValue));
        foreach (interop.WEBSERVICE ws in (List<interop.WEBSERVICE>) this.Session["ListWSInst"])
        {
          long num = new PermissionsDAL().Insert(byId2, byId1, ws, 2, true, DateTime.Now);
          string old2 = "";
          string newone2 = "";
          keyValuePair = Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "PERMISSIONS"));
          new LOGDAL().Insert(keyValuePair.Key, (interop.USER) this.Session["user"], num.ToString(), 1, DateTime.Now, old2, newone2);
        }
        this.ClearTxtBox();
        List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
        this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
        this.Application["ListUsersPermissionsPerm"] = (object) permissionsByInstitution;
        this.CustomersGridView.DataSource = (object) (List<HelpClassUsers>) this.Application["ListUsersPermissions"];
        this.CustomersGridView.DataBind();
        this.DropDownList3.SelectedIndex = -1;
        this.PanelEditUser.CssClass = "content-box column-right closed-box";
        this.PanelNewUser.CssClass = "content-box column-left closed-box";
      }
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
      this.PanelNewUser.CssClass = "content-box column-left";
      this.PanelEditUser.CssClass = "content-box column-right closed-box";
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      string[] ImePrezime = this.TextBox1.Text.Split(' ');
      if (Enumerable.Count<string>((IEnumerable<string>) ImePrezime) == 2)
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) permissionsByInstitution, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(ImePrezime[0].ToUpper()) && p.Surname.ToUpper().Contains(ImePrezime[1].ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list.Add(helpClassUsers);
        }
        if (list.Count == 0)
          list.AddRange((IEnumerable<HelpClassUsers>) permissionsByInstitution);
        this.Application["ListUsersPermissions"] = (object) list;
        this.CustomersGridView.DataSource = (object) list;
        this.CustomersGridView.DataBind();
      }
      else
      {
        IEnumerable<HelpClassUsers> source = Enumerable.Where<HelpClassUsers>((IEnumerable<HelpClassUsers>) permissionsByInstitution, (Func<HelpClassUsers, bool>) (p => p.Name.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Surname.ToUpper().Contains(this.TextBox1.Text.ToUpper()) || p.Email.ToUpper().Contains(this.TextBox1.Text.ToUpper())));
        if (Enumerable.Count<HelpClassUsers>(source) != 0)
        {
          list.Clear();
          foreach (HelpClassUsers helpClassUsers in source)
            list.Add(helpClassUsers);
        }
        if (list.Count == 0)
          list.AddRange((IEnumerable<HelpClassUsers>) permissionsByInstitution);
        this.Application["ListUsersPermissions"] = (object) list;
        this.CustomersGridView.DataSource = (object) list;
        this.CustomersGridView.DataBind();
      }
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      this.PanelNewUser.CssClass = "content-box column-left";
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
          Enumerable.Single<interop.WEBSERVICE>((IEnumerable<interop.WEBSERVICE>) list, (Func<interop.WEBSERVICE, bool>) (p => p.ID == c.ID));
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
        List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(false, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
        this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
        this.CustomersGridView.DataSource = (object) permissionsByInstitution;
        this.CustomersGridView.DataBind();
      }
      else
      {
        List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
        this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
        this.CustomersGridView.DataSource = (object) permissionsByInstitution;
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
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(false, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.CustomersGridView.DataSource = (object) permissionsByInstitution;
      this.CustomersGridView.DataBind();
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
      List<HelpClassUsers> permissionsByInstitution = new UsersDAL().GetUsersPermissionsByInstitution(true, new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"])[0].INSTITUTION);
      this.Application["ListUsersPermissions"] = (object) permissionsByInstitution;
      this.CustomersGridView.DataSource = (object) permissionsByInstitution;
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

    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtPassword_DataBinding(object sender, EventArgs e)
    {
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

    protected void GetRegularValidator(RegularExpressionValidator req1, RegularExpressionValidator req2)
    {
    }
  }
}
