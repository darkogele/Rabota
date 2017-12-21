// Decompiled with JetBrains decompiler
// Type: interop.Login
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop.WSSendEmail;
using InteropClassLibrary;
using StringProtection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop
{
  public class Login : Page
  {
    private IRepository classobj = (IRepository) new InteropClassLibrary.InteropDAL(0);
    private Service1 wsEmail = new Service1();
    protected HtmlForm form1;
    protected Image Image1;
    protected Label Label1;
    protected TextBox TextBox1;
    protected Label Label2;
    protected TextBox TextBox2;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Session["times"] == null)
        this.Session["times"] = (object) 0;
      string ip = this.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (ip == null)
        ip = this.Request.ServerVariables["REMOTE_ADDR"];
      this.Session["ip"] = (object) ip;
      try
      {
        LOGIN_ATTEMPT loginAttempt = Queryable.Single<LOGIN_ATTEMPT>(this.classobj.Login_attempts, (System.Linq.Expressions.Expression<Func<LOGIN_ATTEMPT, bool>>) (p => p.IP == ip));
        if (loginAttempt.DateTime.AddMinutes((double) loginAttempt.Time_rest) > DateTime.Now)
          this.Response.Redirect("WorningPageInterop.aspx");
      }
      catch
      {
      }
      HttpClientCertificate clientCertificate = this.Request.ClientCertificate;
      X509Certificate2 x509Certificate2 = new X509Certificate2(this.Context.Request.ClientCertificate.Certificate);
      string contents;
      if (clientCertificate.IsPresent)
      {
        contents = clientCertificate.Subject.Split(',')[2].Trim();
        this.Session["CertificateName"] = (object) contents;
      }
      else
      {
        contents = "No certificate was found.";
        this.Session["CertificateName"] = (object) null;
      }
      try
      {
        File.WriteAllText("C:\\test.txt", contents);
      }
      catch
      {
      }
      if (!new WebApplicationInterop.InteropDAL().GetSetting().CanCopyPrintScreen.Value)
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "setClipBoardData", "" + "function setClipBoardData(){" + "setInterval(\"window.clipboardData.setData('text','')\",20);" + "}" + "function blockError(){" + "window.location.reload(true);" + "return true;}", true);
      else
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "setClipBoardData", "" + "function setClipBoardData(){" + "}", true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        new WebApplicationInterop.InteropDAL().Set();
        if (this.TextBox1.Text == string.Empty || this.TextBox2.Text == string.Empty)
        {
          this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалидно логирање. Погрешно корисничко име или лозинка!')</script>");
        }
        else
        {
          USER User = new UsersDAL().CheckUser(this.TextBox1.Text, new Crypto().EncryptStringAES(this.TextBox2.Text, ConfigurationManager.AppSettings["PssCrypto"]));
          if (User != null)
          {
            if (new WebApplicationInterop.InteropDAL().GetSetting().IPLimit.Value)
            {
              if (User.IpAdress != null)
              {
                string[] strArray = User.IpAdress.Split(';');
                if (!Enumerable.Contains<string>((IEnumerable<string>) strArray, this.Session["ip"].ToString()))
                {
                  if (strArray.Length > 1)
                  {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Адресата од која се најавувате е потребно да се регистрира во системот!')</script>");
                    return;
                  }
                  new UsersDAL().UpdateIp(User.IpAdress + ";" + this.Session["ip"].ToString(), User.ID);
                }
              }
              else
                new UsersDAL().UpdateIp(this.Session["ip"].ToString(), User.ID);
            }
            CERTIFICATE byUser = new CertificatesDAL().GetByUser(User);
            int num1;
            if (User != null && User.username.Equals(this.TextBox1.Text))
            {
              if (byUser.Subject.Trim() == this.Request.ClientCertificate.Subject.Split(',')[2].Trim())
              {
                num1 = !(byUser.SerialNumber == this.Request.ClientCertificate.SerialNumber.Replace("-", "")) ? 1 : 0;
                goto label_16;
              }
            }
            num1 = 1;
label_16:
            if (num1 == 0)
            {
              User.password = this.TextBox2.Text;
              this.Session["user"] = (object) User;
              if (this.Session["Login_" + (object) User.ID] == null || this.Application.Get("Login_" + (object) User.ID) == null)
              {
                string sessionId = this.Session.SessionID;
                this.Session["Login_" + (object) User.ID] = (object) sessionId;
                if (this.Application.Get("Login_" + (object) User.ID) == null)
                  this.Application.Add("Login_" + (object) User.ID, (object) sessionId);
                else
                  this.Application.Set("Login_" + (object) User.ID, (object) sessionId);
              }
              else if (this.Session["Login_" + (object) User.ID] != this.Application.Get("Login_" + (object) User.ID))
              {
                string sessionId = this.Session.SessionID;
                this.Session["Login_" + (object) User.ID] = (object) sessionId;
                this.Application.Set("Login_" + (object) User.ID, (object) sessionId);
              }
              try
              {
                if (User.ModifiedAt.Value.AddMonths(Queryable.First<PASSWORD_SETTING>(this.classobj.Password_settings).PasswordTimeValid) < DateTime.Now)
                  this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Препорачуваме да си ја промените лозинката!'); window.location.href = 'Default.aspx'; </script>");
                else
                  this.Response.Redirect("Default.aspx");
              }
              catch
              {
                this.Response.Redirect("Default.aspx");
              }
            }
            else
            {
              int num2;
              if (User != null && User.username.Equals(this.TextBox1.Text))
              {
                if (byUser.Subject.Trim() == this.Request.ClientCertificate.Subject.Split(',')[1].Trim())
                {
                  num2 = !(byUser.SerialNumber == this.Request.ClientCertificate.SerialNumber.Replace("-", "")) ? 1 : 0;
                  goto label_31;
                }
              }
              num2 = 1;
label_31:
              if (num2 == 0)
              {
                User.password = this.TextBox2.Text;
                this.Session["user"] = (object) User;
                if (this.Session["Login_" + (object) User.ID] == null || this.Application.Get("Login_" + (object) User.ID) == null)
                {
                  string sessionId = this.Session.SessionID;
                  this.Session["Login_" + (object) User.ID] = (object) sessionId;
                  if (this.Application.Get("Login_" + (object) User.ID) == null)
                    this.Application.Add("Login_" + (object) User.ID, (object) sessionId);
                  else
                    this.Application.Set("Login_" + (object) User.ID, (object) sessionId);
                }
                else if (this.Session["Login_" + (object) User.ID] != this.Application.Get("Login_" + (object) User.ID))
                {
                  string sessionId = this.Session.SessionID;
                  this.Session["Login_" + (object) User.ID] = (object) sessionId;
                  this.Application.Set("Login_" + (object) User.ID, (object) sessionId);
                }
                try
                {
                  if (User.ModifiedAt.Value.AddMonths(Queryable.First<PASSWORD_SETTING>(this.classobj.Password_settings).PasswordTimeValid) < DateTime.Now)
                    this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Препорачуваме да си ја промените лозинката!'); window.location.href = 'Default.aspx'; </script>");
                  else
                    this.Response.Redirect("Default.aspx");
                }
                catch
                {
                  this.Response.Redirect("Default.aspx");
                }
              }
              else if (byUser.SerialNumber != this.Request.ClientCertificate.SerialNumber.Replace("-", "").ToUpper())
              {
                if (this.Request.ClientCertificate.ValidUntil < DateTime.Now)
                {
                  this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Корисникот се најавува со застарен сертификат!')</script>");
                  this.SendMail("Невалодно логирање. Корисникот: " + User.Name + User.Surname + " се најавува со стар сертификат: " + byUser.Subject);
                }
                else
                {
                  this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Корисникот што се најавува не се совпаѓа со приложениот сертификат!')</script>");
                  this.SendMail("Невалодно логирање. Корисникот: " + User.Name + User.Surname + " се најавува со сертификат: " + byUser.Subject + " кој не е во негова сопственост");
                }
              }
              else
              {
                this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Корисникот што се најавува не се совпаѓа со приложениот сертификат!')</script>");
                this.SendMail("Невалодно логирање. Корисникот: " + User.Name + User.Surname + " се најавува со сертификат: " + byUser.Subject + " кој не е во негова сопственост");
              }
            }
          }
          else if (this.Session["ip"] != null)
          {
            try
            {
              new UsersDAL().InsertTemp(this.TextBox1.Text, this.TextBox2.Text, this.Session["ip"].ToString(), this.Request.ClientCertificate.Subject.Split(',')[2].Trim(), DateTime.Now);
            }
            catch
            {
            }
            try
            {
                int num = 3;
                string ip = this.Session["ip"].ToString();
                LOGIN_ATTEMPT atemptObj = null;
                LOGIN_SETTING lOGIN_SETTING = null;
                try
                {
                    LOGIN_ATTEMPT atempt = this.classobj.Login_attempts.Single((LOGIN_ATTEMPT p) => p.IP == ip);
                    atemptObj = atempt;
                    if (atemptObj.DateTime.AddMinutes((double)atemptObj.Time_rest) > DateTime.Now)
                    {
                        base.Response.Redirect("WorningPageInterop.aspx");
                    }
                    else
                    {
                        if (atemptObj.Attempt == 1)
                        {
                            LOGIN_SETTING lOGIN_SETTING2 = this.classobj.Login_settings.Single((LOGIN_SETTING p) => p.Times == atempt.Attempt + 1);
                            lOGIN_SETTING = lOGIN_SETTING2;
                            num = lOGIN_SETTING2.Time_missed;
                        }
                        else
                        {
                            LOGIN_SETTING lOGIN_SETTING2 = this.classobj.Login_settings.Single((LOGIN_SETTING p) => p.Times == 2);
                            lOGIN_SETTING = lOGIN_SETTING2;
                            num = lOGIN_SETTING2.Time_missed;
                        }
                    }
              }
              catch
              {
                LOGIN_SETTING loginSetting2 = Queryable.Single<LOGIN_SETTING>(this.classobj.Login_settings, (System.Linq.Expressions.Expression<Func<LOGIN_SETTING, bool>>) (p => p.Times == 1));
                lOGIN_SETTING = loginSetting2;
                num = loginSetting2.Time_missed;
              }
              int num2 = Convert.ToInt32(this.Session["times"]) + 1;
              this.Session["times"] = (object) num2;
              if (num2 > num)
              {
                this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Погрешно корисничко име или лозинка!')</script>");
                // ISSUE: reference to a compiler-generated field
                if (atemptObj != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  LOGIN_ATTEMPT login_attempt = Queryable.Single<LOGIN_ATTEMPT>(this.classobj.Login_attempts, (System.Linq.Expressions.Expression<Func<LOGIN_ATTEMPT, bool>>) (p => p.ID == atemptObj.ID));
                  login_attempt.DateTime = DateTime.Now;
                  login_attempt.Attempt = 2;
                  login_attempt.Time_rest = lOGIN_SETTING.Time_blocked;
                  this.classobj.Save(login_attempt);
                  this.SendMail("Блокиран е корисникот со ip адреса: " + login_attempt.IP);
                }
                else
                {
                  LOGIN_ATTEMPT login_attempt = new LOGIN_ATTEMPT();
                  // ISSUE: reference to a compiler-generated field
                  login_attempt.IP = ip;
                  login_attempt.DateTime = DateTime.Now;
                  login_attempt.Attempt = 1;
                  login_attempt.Time_rest = lOGIN_SETTING.Time_blocked;
                  this.classobj.Save(login_attempt);
                  this.SendMail("Блокиран е корисникот со ip адреса: " + login_attempt.IP);
                }
                this.Response.Redirect("WorningPageInterop.aspx");
              }
              else
                this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Погрешно корисничко име или лозинка!')</script>");
            }
            catch
            {
              this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Погрешно корисничко име или лозинка!')</script>");
            }
          }
          else
            this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Невалодно логирање. Погрешно корисничко име или лозинка!')</script>");
        }
      }
      catch
      {
        this.ClientScript.RegisterStartupScript(this.GetType(), "ExceptionScript", "<script language = 'javascript'> alert('Системска грешка. Обидетесе повторно!')</script>");
      }
    }

    protected void SendMail(string messagetosend)
    {
      try
      {
        string Subject = "Порака за тревога во системот за Интероперабилност.";
        string Body = "Порака за тревога во системот за Интероперабилност. \n \n" + messagetosend;
        bool flag = false;
        IQueryable<InteropClassLibrary.USER> users = this.classobj.Users;
        System.Linq.Expressions.Expression<Func<InteropClassLibrary.USER, bool>> predicate = (System.Linq.Expressions.Expression<Func<InteropClassLibrary.USER, bool>>) (p => p.Type == 1);
        foreach (InteropClassLibrary.USER user in Enumerable.ToList<InteropClassLibrary.USER>((IEnumerable<InteropClassLibrary.USER>) Queryable.Where<InteropClassLibrary.USER>(users, predicate)))
          flag = this.wsEmail.SendMail(new Crypto().EncryptStringAES(Subject, "SecRet@admiN$"), Subject, Body, user.email);
        this.classobj.Save(new NOTIFIKACII()
        {
          Body = Body,
          EmailTo = "",
          IsReaded = false,
          IsSend = flag,
          Tittle = Subject,
          USER_ID = new Guid("b85e62c3-dc56-40c0-852a-49f759ac68fb"),
          DateCreated = DateTime.Now
        });
      }
      catch
      {
      }
    }
  }
}
