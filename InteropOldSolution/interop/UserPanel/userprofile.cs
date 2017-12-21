// Decompiled with JetBrains decompiler
// Type: interop.UserPanel.userprofile
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

namespace interop.UserPanel
{
  public class userprofile : UserControl
  {
    private IRepository classobj = (IRepository) new InteropClassLibrary.InteropDAL(0);
    protected Image Image1;
    protected Label Label1;
    protected TextBox txtName;
    protected RequiredFieldValidator Req1;
    protected Label Label2;
    protected TextBox txtSurname;
    protected RequiredFieldValidator Req2;
    protected Label Label3;
    protected TextBox txtemail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected Label Label6;
    protected TextBox txtUsername;
    protected RequiredFieldValidator Req4;
    protected Label lblUserExist;
    protected Label Label7;
    protected TextBox txtPassword;
    protected RequiredFieldValidator Req5;
    protected Label lblConfirmPassword;
    protected TextBox txtPasswordConfirm;
    protected RequiredFieldValidator RequiredFieldValidator10;
    protected CompareValidator CompareValidator1;
    protected Label Label4;
    protected Button Button1;
    protected Panel PanelInfoInstOK;
    protected Panel PanelInfoInstNO;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu3"] = (object) "Help/korisnik/Help_user_profile.aspx";
      this.FillUser((interop.USER) this.Session["user"]);
    }

    public void FillUser(interop.USER TempUser)
    {
      this.txtName.Text = TempUser.Name;
      this.txtSurname.Text = TempUser.Surname;
      this.txtemail.Text = TempUser.email;
      this.txtUsername.Text = TempUser.username;
      this.txtPassword.Attributes["value"] = TempUser.password;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      if (this.lblUserExist.Visible)
        return;
      interop.USER user = (interop.USER) this.Session["user"];
      try
      {
        string pass = new Crypto().EncryptStringAES(this.txtPassword.Text, ConfigurationManager.AppSettings["PssCrypto"]);
        new UsersDAL().Update(this.txtName.Text, this.txtSurname.Text, new long?(), this.txtemail.Text, new bool?(user.Active), new DateTime?(), this.txtUsername.Text, pass, new int?(), (string) null, user.ID);
        this.PanelInfoInstOK.Visible = true;
        string old = user.Name + ";" + user.Surname + ";" + user.email + ";" + user.username + ";" + user.password + ";" + user.Active.ToString();
        string newone = this.txtName.Text + ";" + this.txtSurname.Text + ";" + this.txtemail.Text + ";" + this.txtUsername.Text + ";" + this.txtPassword.Text;
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) WebApplicationInterop.InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "USERS")).Key, (interop.USER) this.Session["user"], user.ID.ToString(), 2, DateTime.Now, old, newone);
      }
      catch
      {
        this.PanelInfoInstNO.Visible = true;
      }
    }

    protected void txtUsername_TextChanged(object sender, EventArgs e)
    {
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

    protected void GetRegularValidator(RegularExpressionValidator req2)
    {
    }
  }
}
