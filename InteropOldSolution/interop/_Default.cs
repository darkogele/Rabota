// Decompiled with JetBrains decompiler
// Type: interop._Default
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using WebApplicationInterop;

namespace interop
{
  public class _Default : Page
  {
    private Control kontrola = new Control();
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected ToolkitScriptManager ToolkitScriptManager1;
    protected LinkButton LinkButton8;
    protected Image logo;
    protected LinkButton LinkButton2;
    protected Label lbluserinfo;
    protected Label lblInstIfoUs;
    protected LinkButton LinkButton1;
    protected TextBox TextBoxSearch;
    protected Button ButtonSearch;
    protected Panel ControlInstitutions;
    protected Panel ControlServices;
    protected HtmlGenericControl SysSettings;
    protected LinkButton LinkButton4;
    protected Label Label18;
    protected FileUpload FileUploadValidate;
    protected Button ButtonValidate;
    protected Panel AdminPanelMenu;
    protected LinkButton btn_institutions;
    protected Label Label2;
    protected LinkButton btn_users;
    protected Label Label3;
    protected LinkButton btn_basis;
    protected Label lblbasis;
    protected LinkButton btn_wsbasis;
    protected Label lblwsbasis;
    protected LinkButton btn_ws;
    protected Label Label4;
    protected LinkButton btn_permissions;
    protected Label Label5;
    protected LinkButton btn_log;
    protected Label Label6;
    protected LinkButton btn_ServiceLog;
    protected Label Label7;
    protected LinkButton btn_Help;
    protected Label Label14;
    protected Panel LocalAdminPanelMenu;
    protected LinkButton btn_usersl;
    protected Label Label9;
    protected LinkButton btn_wsl;
    protected Label Label8;
    protected LinkButton btn_permissionsl;
    protected Label Label10;
    protected LinkButton btn_logl;
    protected Label Label11;
    protected LinkButton btn_ServiceLogLocalAdmin;
    protected Label Label17;
    protected LinkButton btn_Help1;
    protected Label Label15;
    protected Panel UserPanelMenu;
    protected LinkButton btn_wsus;
    protected Label Label12;
    protected LinkButton LinkButton3;
    protected Label Label13;
    protected LinkButton btn_Help2;
    protected Label Label16;
    protected Label ctrID;
    protected Label ctr1ID;
    protected UpdatePanel MainUpdatePanel;
    protected UpdateProgress updateProgres;
    protected PlaceHolder mainPlaceholder;

    public void CallClientScript(string Poraka)
    {
      this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('" + Poraka + "')</script>");
    }

    private void fillControl(string ctrl_path)
    {
      if (this.mainPlaceholder.FindControl(this.ctrID.Text) != null)
        this.mainPlaceholder.Controls.Remove(this.mainPlaceholder.FindControl(this.ctrID.Text));
      this.kontrola = this.LoadControl(ctrl_path);
      this.kontrola.ID = ctrl_path;
      this.ctrID.Text = this.kontrola.ID;
      this.mainPlaceholder.Controls.Add(this.kontrola);
    }

    private void fillControl1(string ctrl_path, LinkButton btn)
    {
      if (this.mainPlaceholder.FindControl(this.ctr1ID.Text) != null)
        this.mainPlaceholder.Controls.Remove(this.mainPlaceholder.FindControl(this.ctr1ID.Text));
      this.kontrola = this.LoadControl(ctrl_path);
      this.kontrola.ID = ctrl_path.Split('/')[1].Split('.')[0];
      this.ctr1ID.Text = this.kontrola.ID;
      this.mainPlaceholder.Controls.Add(this.kontrola);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu3"] = (object) "Help/korisnik/Help_pocetna_obicen.aspx";
      this.Session["HelpMenu2"] = (object) "Help/admin/Help_Pocetna_Admin.aspx";
      this.Session["HelpMenu1"] = (object) "Help/superadmin/Help_pocetna_super.aspx";
      HttpBrowserCapabilities browser = this.Request.Browser;
      if (browser.Browser == "IE" && browser.MajorVersion <= 6)
      {
        HtmlHead header = this.Page.Header;
        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("href", this.Page.ResolveClientUrl("~/rerources/css/IE6Style.css"));
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Attributes.Add("rel", "stylesheet");
        header.Controls.Add((Control) htmlLink);
      }
      if (browser.Browser == "IE" && browser.MajorVersion == 7)
      {
        HtmlHead header = this.Page.Header;
        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("href", this.Page.ResolveClientUrl("~/rerources/css/IEStyle.css"));
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Attributes.Add("rel", "stylesheet");
        header.Controls.Add((Control) htmlLink);
      }
      if (browser.Browser == "IE" && browser.MajorVersion == 9)
      {
        HtmlHead header = this.Page.Header;
        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("href", this.Page.ResolveClientUrl("~/rerources/css/IEStyle.css"));
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Attributes.Add("rel", "stylesheet");
        header.Controls.Add((Control) htmlLink);
      }
      if (browser.Browser == "IE" && browser.MajorVersion == 8)
      {
        this.ControlInstitutions.Width = (Unit) 150;
        this.ControlServices.Width = (Unit) 150;
        HtmlHead header = this.Page.Header;
        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("href", this.Page.ResolveClientUrl("~/rerources/css/IE8Style.css"));
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Attributes.Add("rel", "stylesheet");
        header.Controls.Add((Control) htmlLink);
      }
      else if (browser.Browser != "IE")
      {
        this.ControlInstitutions.Width = (Unit) 150;
        this.ControlServices.Width = (Unit) 150;
        HtmlHead header = this.Page.Header;
        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("href", this.Page.ResolveClientUrl("~/rerources/css/Style.css"));
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Attributes.Add("rel", "stylesheet");
        header.Controls.Add((Control) htmlLink);
      }
      List<Opstini> list1 = new List<Opstini>();
      List<KatOpstini> list2 = new List<KatOpstini>();
      List<KatOpstini> list3 = new List<KatOpstini>();
      InteropDAL interopDal = new InteropDAL();
      this.Session["Grad"] = (object) list1;
      this.Session["Opstini"] = (object) list2;
      list3.AddRange((IEnumerable<KatOpstini>) list2);
      this.Session["OpstiniFilter"] = (object) list3;
      if (this.Session["user"] != null && this.Session["CertificateName"] != null)
      {
        USER user = (USER) this.Session["user"];
        if (this.Session["Login_" + (object) user.ID] == this.Application.Get("Login_" + (object) user.ID))
        {
          if (user.Type == 1)
          {
            this.SysSettings.Visible = true;
            if (!this.IsPostBack)
            {
              List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
              allActiveDeleted.AddRange((IEnumerable<INSTITUTION>) new InstitutionsDAL().GetAllActiveDeleted(false));
              this.Application["DropDownInstitutions"] = (object) allActiveDeleted;
              if (user.Type == 1)
              {
                this.Application["ListInstitutions"] = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
                this.Application["ListWebServices"] = (object) new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
                this.Application["ListUsersPermissions"] = (object) new UsersDAL().GetUsersInstitutionsPermissions(2, true);
                this.Application["ListUsersPermissionsPerm"] = (object) new UsersDAL().GetUsersInstitutionsPermissions(2, true);
                this.Application["ListBasis"] = (object) new BasisDAL().GetAllByStatus(true);
              }
            }
            this.AdminPanelMenu.Visible = true;
            if (this.ctr1ID.Text != "")
              this.fillControl1("AdminPanel/" + this.ctr1ID.Text + ".ascx", (LinkButton) this.FindControl("btn_" + this.ctr1ID.Text));
            else if (this.Session["AdminInstitution"] != null)
              this.fillControl1("AdminPanel/institutions.ascx", this.btn_ws);
            else if (this.Session["AdminWS"] != null)
              this.fillControl1("AdminPanel/ws.ascx", this.btn_ws);
            else if (this.Session["AdminUser"] != null)
              this.fillControl1("AdminPanel/users.ascx", this.btn_ws);
            else if (this.Session["AdminPermisions"] != null)
              this.fillControl1("AdminPanel/permissions.ascx", this.btn_ws);
            else if (this.Session["AdminValidate"] != null)
              this.fillControl1("AdminPanel/validatea.ascx", this.btn_ws);
            else if ((INSTITUTION) this.Session["AdminSelectedInstitution"] != null)
              this.fillControl1("AdminPanel/instinfoa.ascx", this.btn_ws);
            else if ((HelpClassWebServices) this.Session["AdminSelectedWS"] != null)
              this.fillControl1("AdminPanel/wsusagea.ascx", this.btn_ws);
            else if ((HelpClassUsers) this.Session["AdminSelectedUser"] != null)
              this.fillControl1("AdminPanel/users.ascx", this.btn_users);
            else
              this.fillControl1("AdminPanel/indexa.ascx", this.btn_users);
          }
          if (user.Type == 2)
          {
            if (!this.IsPostBack && user.Type == 2)
            {
              List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
              this.Application["DropDownInstitutions"] = (object) new List<INSTITUTION>()
              {
                permisionsByUser[0].INSTITUTION
              };
              List<WEBSERVICE> list4 = new List<WEBSERVICE>();
              foreach (PERMISSION permission in permisionsByUser)
              {
                if (permission.Active)
                  list4.Add(permission.WEBSERVICE);
              }
              this.Application["ListWebServices"] = (object) list4;
              this.Application["ListUserWebServices"] = (object) new WebservicesDAL().GetUsersWebServicesPermissions(true, user, permisionsByUser[0].INSTITUTION);
              this.Application["ListUsersPermissions"] = (object) new UsersDAL().GetUsersPermissionsByInstitution(true, permisionsByUser[0].INSTITUTION);
              this.Application["ListUsersPermissionsPerm"] = (object) new UsersDAL().GetUsersPermissionsByInstitution(true, permisionsByUser[0].INSTITUTION);
            }
            this.LocalAdminPanelMenu.Visible = true;
            this.lblInstIfoUs.Text = user.PERMISSIONs[0].INSTITUTION.Tittle;
            this.Session["UserInstitution"] = (object) user.PERMISSIONs[0].INSTITUTION;
            if (this.ctr1ID.Text != "")
              this.fillControl1("LocalAdminPanel/" + this.ctr1ID.Text + ".ascx", (LinkButton) this.FindControl("btn_" + this.ctr1ID.Text));
            else if (this.Session["LocalUser"] != null)
            {
              List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
              List<WEBSERVICE> list4 = new List<WEBSERVICE>();
              foreach (HelpClassWebServices classWebServices in servicesPermissions)
                list4.Add(classWebServices.WSObj);
              List<WEBSERVICE> list5 = new List<WEBSERVICE>();
              list5.AddRange((IEnumerable<WEBSERVICE>) list4);
              this.Session["ListWSAllInst"] = (object) list4;
              this.Session["ListWSInst"] = (object) list5;
              this.fillControl1("LocalAdminPanel/usersl.ascx", this.btn_ws);
            }
            else if (this.Session["LocalPermisions"] != null)
              this.fillControl1("LocalAdminPanel/permissionsl.ascx", this.btn_ws);
            else if ((INSTITUTION) this.Session["LocalSelectedInstitution"] != null)
              this.fillControl1("LocalAdminPanel/instinfol.ascx", this.btn_ws);
            else if ((HelpClassWebServices) this.Session["LocalSelectedWS"] != null)
              this.fillControl1("LocalAdminPanel/wsusagel.ascx", this.btn_ws);
            else if ((HelpClassUsers) this.Session["LocalSelectedUser"] != null)
              this.fillControl1("LocalAdminPanel/usersl.ascx", this.btn_usersl);
            else
              this.fillControl1("LocalAdminPanel/indexl.ascx", this.btn_usersl);
          }
          if (user.Type == 3)
          {
            this.UserPanelMenu.Visible = true;
            if (user.Type == 3)
            {
              List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
              List<WEBSERVICE> list4 = new List<WEBSERVICE>();
              foreach (PERMISSION permission in permisionsByUser)
              {
                if (permission.Active)
                  list4.Add(permission.WEBSERVICE);
              }
              this.Application["ListWebServices"] = (object) list4;
              this.Application["ListUserWebServices"] = (object) new WebservicesDAL().GetUsersWebServicesPermissions(true, user);
            }
            this.lblInstIfoUs.Text = user.PERMISSIONs[0].INSTITUTION.Tittle;
            this.Session["UserInstitution"] = (object) user.PERMISSIONs[0].INSTITUTION;
            if (this.ctr1ID.Text != "")
              this.fillControl1("UserPanel/" + this.ctr1ID.Text + ".ascx", (LinkButton) this.FindControl("btn_" + this.ctr1ID.Text));
            else if ((HelpClassWebServices) this.Session["SelectedWS"] != null)
              this.fillControl1("UserPanel/wsusage.ascx", this.btn_ws);
            else if ((INSTITUTION) this.Session["SelectedInstitution"] != null)
              this.fillControl1("UserPanel/instinfo.ascx", this.btn_ws);
            else
              this.fillControl1("UserPanel/indexu.ascx", this.btn_wsus);
          }
          this.lbluserinfo.Text = user.Name + " " + user.Surname;
          this.LinkButton1.Visible = true;
        }
        else
        {
          this.Session["user"] = (object) null;
          this.Response.Redirect("Login.aspx");
        }
      }
      else
      {
        this.Session["user"] = (object) null;
        this.Response.Redirect("Login.aspx");
      }
      if (!new InteropDAL().GetSetting().CanCopyPrintScreen.Value)
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "setClipBoardData", "" + "function setClipBoardData(){" + "setInterval(\"window.clipboardData.setData('text','')\",20);" + "}" + "function blockError(){" + "window.location.reload(true);" + "return true;}", true);
      else
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "setClipBoardData", "" + "function setClipBoardData(){" + "}", true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      this.Session.Clear();
      this.Session.Abandon();
      this.Session["user"] = (object) null;
      this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
      this.Response.Cache.SetExpires(DateTime.Now);
      this.ctrID.Text = string.Empty;
      this.Response.Redirect("Login.aspx");
      this.LinkButton1.Visible = false;
    }

    protected void btn_wsbasis_Click(object sender, EventArgs e)
    {
      this.fillControl1("AdminPanel/permissionsbasis.ascx", this.btn_wsbasis);
    }

    protected void btn_users_Click(object sender, EventArgs e)
    {
      this.fillControl1("AdminPanel/users.ascx", this.btn_users);
    }

    protected void btn_basis_Click(object sender, EventArgs e)
    {
      this.fillControl1("AdminPanel/wsbasis.ascx", this.btn_basis);
    }

    protected void btn_inst_Click(object sender, EventArgs e)
    {
      this.fillControl1("AdminPanel/institutions.ascx", this.btn_institutions);
    }

    protected void btn_ws_Click(object sender, EventArgs e)
    {
      this.Application["Params"] = (object) null;
      this.fillControl1("AdminPanel/ws.ascx", this.btn_ws);
    }

    protected void btn_permissions_Click(object sender, EventArgs e)
    {
      this.Session["ListAllWSInst"] = (object) new List<WEBSERVICE>();
      this.Session["ListAllWS"] = (object) new List<WEBSERVICE>();
      this.fillControl1("AdminPanel/permissions.ascx", this.btn_permissions);
    }

    protected void btn_logs_Click(object sender, EventArgs e)
    {
        DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        this.Session["Log"] = new LOGDAL().GetTop50();
        this.fillControl1("AdminPanel/log.ascx", this.btn_log);
    }

    protected void btn_ServiceLog_Click(object sender, EventArgs e)
    {
      List<SearchLog> top50SearchLog = new InteropDAL().GetTop50SearchLog();
      this.Session["BigList"] = (object) top50SearchLog;
      List<SearchLog> list1 = new List<SearchLog>();
      list1.AddRange((IEnumerable<SearchLog>) top50SearchLog);
      this.Session["BigListFilter"] = (object) list1;
      this.Session["ListLogU"] = (object) new UsersDAL().GetUsersByWSLog();
      this.Session["ListLogI"] = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
      List<WEBSERVICE> list2 = new List<WEBSERVICE>();
      foreach (string tittle in Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) top50SearchLog, (Func<SearchLog, string>) (p => p.WSTitle))))
      {
        if (tittle != null)
        {
          WEBSERVICE byTittle = new WebservicesDAL().GetByTittle(tittle);
          list2.Add(byTittle);
        }
      }
      this.Session["ListLogWS"] = (object) list2;
      this.fillControl1("AdminPanel/WebServicesLog.ascx", this.btn_ServiceLog);
    }

    protected void btn_wsus_Click(object sender, EventArgs e)
    {
      this.fillControl1("UserPanel/wsusage.ascx", this.btn_ws);
    }

    protected void btn_usersl_Click(object sender, EventArgs e)
    {
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      List<WEBSERVICE> list1 = new List<WEBSERVICE>();
      foreach (HelpClassWebServices classWebServices in servicesPermissions)
        list1.Add(classWebServices.WSObj);
      List<WEBSERVICE> list2 = new List<WEBSERVICE>();
      list2.AddRange((IEnumerable<WEBSERVICE>) list1);
      this.Session["ListWSAllInst"] = (object) list1;
      this.Session["ListWSInst"] = (object) list2;
      this.fillControl1("LocalAdminPanel/usersl.ascx", this.btn_usersl);
    }

    protected void btn_wsl_Click(object sender, EventArgs e)
    {
      this.fillControl1("LocalAdminPanel/wsl.ascx", this.btn_wsl);
    }

    protected void btn_permissionsl_Click(object sender, EventArgs e)
    {
      this.Session["ListWSAll"] = (object) new List<WEBSERVICE>();
      this.fillControl1("LocalAdminPanel/permissionsl.ascx", this.btn_permissionsl);
    }

    protected void btn_logsl_Click(object sender, EventArgs e)
    {
      this.Session["Log"] = (object) new LOGDAL().GetTop50ByAdminInstitution((USER) this.Session["user"]);
      this.fillControl1("LocalAdminPanel/logl.ascx", this.btn_logl);
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
      if (this.Session["user"] != null)
      {
        USER user = (USER) this.Session["user"];
        if (user.Type == 1)
          this.fillControl1("AdminPanel/indexa.ascx", this.btn_usersl);
        if (user.Type == 2)
          this.fillControl1("LocalAdminPanel/indexl.ascx", this.btn_usersl);
        if (user.Type == 3)
          this.fillControl1("UserPanel/indexu.ascx", this.btn_usersl);
        this.lbluserinfo.Text = user.Name + " " + user.Surname;
        this.LinkButton1.Visible = true;
      }
      else
        this.Response.Redirect("Login.aspx");
    }

    protected void ControlServices_Load(object sender, EventArgs e)
    {
      USER user = (USER) this.Session["user"];
      if (user == null)
        return;
      if (user.Type == 1)
      {
        List<HelpClassWebServices> institutionsPermissions = new WebservicesDAL().GetWebServicesInstitutionsPermissions(true);
        try
        {
          institutionsPermissions.Remove(Enumerable.First<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) institutionsPermissions, (Func<HelpClassWebServices, bool>) (p => p.Tittle == "Назив На Субјектот")));
        }
        catch
        {
        }
        this.Session["PanelWS"] = (object) institutionsPermissions;
        this.FillControlWS(institutionsPermissions);
      }
      else if (user.Type == 2)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, user);
        try
        {
          servicesPermissions.Remove(Enumerable.First<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, bool>) (p => p.Tittle == "Назив На Субјектот")));
        }
        catch
        {
        }
        this.Session["PanelWS"] = (object) servicesPermissions;
        this.FillControlWS(servicesPermissions);
      }
      else if (user.Type == 3)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, user);
        List<HelpClassWebServices> list = new List<HelpClassWebServices>();
        try
        {
          servicesPermissions.Remove(Enumerable.First<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, bool>) (p => p.Tittle == "Назив На Субјектот")));
        }
        catch
        {
        }
        this.Session["PanelWS"] = (object) servicesPermissions;
        list.AddRange((IEnumerable<HelpClassWebServices>) servicesPermissions);
        this.Session["PanelWSHelp"] = (object) list;
        this.FillControlWS(servicesPermissions);
      }
    }

    protected void ControlInstitutions_Load(object sender, EventArgs e)
    {
      USER user = (USER) this.Session["user"];
      if (user == null)
        return;
      if (user.Type == 1)
      {
        List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
        this.Session["PanelInstitutions"] = (object) allActiveDeleted;
        this.FillControlInstitutions(allActiveDeleted);
      }
      else if (user.Type == 2)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, user);
        List<INSTITUTION> List = new List<INSTITUTION>();
        foreach (long id in Enumerable.Distinct<long>(Enumerable.Select<HelpClassWebServices, long>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, long>) (p => p.IDInstitution))))
        {
          INSTITUTION byId = new InstitutionsDAL().GetByID(id);
          if (byId != null)
            List.Add(byId);
        }
        this.Session["PanelInstitutions"] = (object) List;
        this.FillControlInstitutions(List);
      }
      else if (user.Type == 3)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, user);
        List<INSTITUTION> List = new List<INSTITUTION>();
        foreach (long id in Enumerable.Distinct<long>(Enumerable.Select<HelpClassWebServices, long>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, long>) (p => p.IDInstitution))))
        {
          INSTITUTION byId = new InstitutionsDAL().GetByID(id);
          if (byId != null)
            List.Add(byId);
        }
        this.Session["PanelInstitutions"] = (object) List;
        this.FillControlInstitutions(List);
      }
    }

    public void FillControlInstitutions(List<INSTITUTION> List)
    {
      this.ControlInstitutions.Controls.Clear();
      foreach (INSTITUTION institution in List)
      {
        LinkButton linkButton = new LinkButton();
        linkButton.Width = (Unit) 180;
        linkButton.Text = institution.Tittle;
        linkButton.Click += new EventHandler(this.LinkB_Click);
        this.ControlInstitutions.Controls.Add((Control) linkButton);
      }
    }

    public void FillControlWS(List<HelpClassWebServices> List)
    {
      this.ControlServices.Controls.Clear();
      foreach (HelpClassWebServices classWebServices in List)
      {
        LinkButton linkButton = new LinkButton();
        linkButton.Width = (Unit) 180;
        string tittle = classWebServices.Tittle;
        linkButton.Text = tittle;
        linkButton.Click += new EventHandler(this.LinkWS_Click);
        this.ControlServices.Controls.Add((Control) linkButton);
      }
    }

    protected void LinkB_Click(object sender, EventArgs e)
    {
      LinkButton Temp = (LinkButton) sender;
      INSTITUTION institution = Enumerable.Single<INSTITUTION>((IEnumerable<INSTITUTION>) this.Session["PanelInstitutions"], (Func<INSTITUTION, bool>) (p => p.Tittle == Temp.Text));
      USER user = (USER) this.Session["user"];
      if (user == null)
        return;
      if (user.Type == 1)
      {
        this.Session["AdminSelectedInstitution"] = (object) institution;
        this.fillControl1("AdminPanel/instinfoa.ascx", this.btn_wsus);
      }
      else if (user.Type == 2)
      {
        this.Session["LocalSelectedInstitution"] = (object) institution;
        this.fillControl1("LocalAdminPanel/instinfol.ascx", this.btn_wsus);
      }
      else if (user.Type == 3)
      {
        this.Session["SelectedInstitution"] = (object) institution;
        this.fillControl1("UserPanel/instinfo.ascx", this.btn_wsus);
      }
    }

    protected void LinkWS_Click(object sender, EventArgs e)
    {
      LinkButton linkButton = (LinkButton) sender;
      List<HelpClassWebServices> list = (List<HelpClassWebServices>) this.Session["PanelWS"];
      string NazivServis = linkButton.Text;
      HelpClassWebServices classWebServices = Enumerable.Single<HelpClassWebServices>((IEnumerable<HelpClassWebServices>) list, (Func<HelpClassWebServices, bool>) (p => p.Tittle == NazivServis));
      USER user = (USER) this.Session["user"];
      if (user == null)
        return;
      if (user.Type == 1)
      {
        this.Session["AdminSelectedWS"] = (object) classWebServices;
        this.fillControl1("AdminPanel/wsusagea.ascx", this.btn_wsus);
      }
      else if (user.Type == 2)
      {
        this.Session["LocalSelectedWS"] = (object) classWebServices;
        this.fillControl1("LocalAdminPanel/wsusagel.ascx", this.btn_wsus);
      }
      else if (user.Type == 3)
      {
        this.Session["SelectedWS"] = (object) classWebServices;
        this.fillControl1("UserPanel/wsusage.ascx", this.btn_wsus);
      }
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
      USER userLogIn = (USER) this.Session["user"];
      this.Session["SelectedSearchList"] = (object) new InteropDAL().Search(this.TextBoxSearch.Text, userLogIn);
      if (userLogIn == null)
        return;
      if (userLogIn.Type == 1)
        this.fillControl1("AdminPanel/searchresulta.ascx", this.btn_wsus);
      else if (userLogIn.Type == 2)
        this.fillControl1("LocalAdminPanel/searchresultl.ascx", this.btn_wsus);
      else if (userLogIn.Type == 3)
        this.fillControl1("UserPanel/searchresultu.ascx", this.btn_wsus);
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      USER user = (USER) this.Session["user"];
      if (user == null)
        return;
      if (user.Type == 1)
        this.fillControl1("AdminPanel/adminprofile.ascx", this.btn_wsus);
      else if (user.Type == 2)
        this.fillControl1("LocalAdminPanel/localuserprofile.ascx", this.btn_wsus);
      else if (user.Type == 3)
        this.fillControl1("UserPanel/userprofile.ascx", this.btn_wsus);
    }

    protected void btn_wsus_Click1(object sender, EventArgs e)
    {
      this.fillControl1("UserPanel/servisi.ascx", this.btn_wsus);
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
      this.fillControl1("UserPanel/facs.ascx", this.btn_wsus);
    }

    protected void btn_Help_Click_SuperAdmin(object sender, EventArgs e)
    {
      this.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + this.Session["HelpMenu1"].ToString() + "','Graph','height=620,width=600');", true);
    }

    protected void btn_Help_Click_Admin(object sender, EventArgs e)
    {
      this.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + this.Session["HelpMenu2"].ToString() + "','Graph','height=600,width=600');", true);
    }

    protected void btn_Help_Click_Korisnik(object sender, EventArgs e)
    {
      this.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + this.Session["HelpMenu3"].ToString() + "','Graph','height=600,width=600');", true);
    }

    protected void btn_ServiceLogLocalAdmin_Click(object sender, EventArgs e)
    {
      List<SearchLog> logForLocalAdmin = new InteropDAL().GetTop50SearchLogForLocalAdmin((INSTITUTION) this.Session["UserInstitution"]);
      this.Session["BigList"] = (object) logForLocalAdmin;
      List<SearchLog> list1 = new List<SearchLog>();
      list1.AddRange((IEnumerable<SearchLog>) logForLocalAdmin);
      this.Session["BigListFilter"] = (object) list1;
      this.Session["ListLogU"] = (object) new UsersDAL().GetUsersByWSLogForLocalAdmin((INSTITUTION) this.Session["UserInstitution"]);
      this.Session["ListLogI"] = (object) new InstitutionsDAL().GetAllActiveDeleted(true);
      List<WEBSERVICE> list2 = new List<WEBSERVICE>();
      foreach (string tittle in Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) logForLocalAdmin, (Func<SearchLog, string>) (p => p.WSTitle))))
      {
        if (tittle != null)
        {
          WEBSERVICE byTittle = new WebservicesDAL().GetByTittle(tittle);
          list2.Add(byTittle);
        }
      }
      this.Session["ListLogWS"] = (object) list2;
      this.fillControl1("LocalAdminPanel/WebServicesLog.ascx", this.btn_ServiceLog);
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("https://misapplication.interop.local/admin/login.aspx");
    }

    protected void ButtonValidate_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.FileUploadValidate.HasFile)
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load(this.FileUploadValidate.FileContent);
          SignedXml signedXml = new SignedXml();
          XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Signature");
          signedXml.LoadXml((XmlElement) elementsByTagName[0]);
          if (signedXml.CheckSignature())
            this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Валидацијата е успешна!')</script>");
          else
            this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Неуспешна валидација!')</script>");
        }
        else
          this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Не прикачивте порака!!')</script>");
      }
      catch
      {
        this.ClientScript.RegisterStartupScript(this.GetType(), "InvalidLogInScript", "<script language = 'javascript'> alert('Не прикачивте валидна XML датотека!')</script>");
      }
    }
  }
}
