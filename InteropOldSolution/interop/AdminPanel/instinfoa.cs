// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.instinfoa
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.AdminPanel
{
  public class instinfoa : UserControl
  {
    protected Image Image1;
    protected Panel PanelInstitucii;
    protected Image Image2;
    protected Panel PanelUsers;
    protected Image Image3;
    protected Panel PanelServices;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpInstituciiOpsto.aspx";
    }

    protected void PanelUsers_Load(object sender, EventArgs e)
    {
      this.FillPanelUsers((INSTITUTION) this.Session["AdminSelectedInstitution"]);
    }

    public void FillPanelUsers(INSTITUTION objectInstitution)
    {
      List<HelpClassUsers> byInstitutionNew = new UsersDAL().GetUsersPermissionsByInstitutionNew(true, objectInstitution);
      this.PanelUsers.Controls.Clear();
      foreach (HelpClassUsers helpClassUsers in byInstitutionNew)
      {
        if (helpClassUsers.Type == 2)
        {
          Label label = new Label();
          this.PanelUsers.Controls.Add((Control) new LiteralControl("<p>"));
          label.ID = "LabelU" + (object) helpClassUsers.ID;
          label.Text = helpClassUsers.NameSurname;
          label.CssClass = "lblpanelsinfo";
          this.PanelUsers.Controls.Add((Control) label);
          LinkButton linkButton = new LinkButton();
          linkButton.ID = "LinkU" + (object) helpClassUsers.ID;
          linkButton.Text = " Повеќе...";
          linkButton.CssClass = "linkpanelsinfo";
          linkButton.Click += new EventHandler(this.LinkU_Click);
          this.PanelUsers.Controls.Add((Control) linkButton);
          this.PanelUsers.Controls.Add((Control) new LiteralControl("</p>"));
        }
      }
    }

    protected void PanelServices_Load(object sender, EventArgs e)
    {
      this.FillPanelServices((INSTITUTION) this.Session["AdminSelectedInstitution"]);
    }

    public void FillPanelServices(INSTITUTION objectInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(objectInstitution);
      this.PanelServices.Controls.Clear();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        Label label = new Label();
        this.PanelServices.Controls.Add((Control) new LiteralControl("<p>"));
        label.ID = "LabelWS" + (object) webservice.ID;
        string str = webservice.Tittle;
        if (str == "Imoten List")
          str = "Имотен Лист";
        else if (str == "Drzavjanstvo")
          str = "Државјанство";
        else if (str == "Edinstven Danocen Broj Na Subjektot")
          str = "Единствен Даночен Број На Субјектот";
        else if (str == "Podatoci za MBPL za potrebite na AKN")
          str = "Податоци за ЕМБС за потребите на АКН";
        else if (str == "Podatoci za MBPL za potrebite na CU")
          str = "Податоци за ЕМБС за потребите на ЦУ";
        else if (str == "Podatoci za parceli")
          str = "Податоци за Парцели";
        label.Text = str;
        label.CssClass = "lblpanelsinfo";
        this.PanelServices.Controls.Add((Control) label);
        LinkButton linkButton = new LinkButton();
        linkButton.ID = "LinkWS" + (object) webservice.ID;
        linkButton.Text = "Пребарај...";
        linkButton.CssClass = "linkpanelsinfo";
        linkButton.Click += new EventHandler(this.LinkWS_Click);
        this.PanelServices.Controls.Add((Control) linkButton);
        this.PanelServices.Controls.Add((Control) new LiteralControl("</p>"));
      }
    }

    protected void PanelInstitucii_Load(object sender, EventArgs e)
    {
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      this.PanelInstitucii.Controls.Clear();
      foreach (INSTITUTION institution in allActiveDeleted)
      {
        Label label = new Label();
        this.PanelInstitucii.Controls.Add((Control) new LiteralControl("<p>"));
        label.ID = "LabelInst" + (object) institution.ID;
        label.Text = institution.Tittle;
        label.CssClass = "lblpanelsinfo";
        this.PanelInstitucii.Controls.Add((Control) label);
        LinkButton linkButton = new LinkButton();
        linkButton.ID = "LinkInst" + (object) institution.ID;
        linkButton.Text = "Повеќе...";
        linkButton.CssClass = "linkpanelsinfo";
        linkButton.Click += new EventHandler(this.LinkInst_Click);
        this.PanelInstitucii.Controls.Add((Control) linkButton);
        this.PanelInstitucii.Controls.Add((Control) new LiteralControl("</p>"));
      }
    }

    protected void LinkWS_Click(object sender, EventArgs e)
    {
      HelpClassWebServices byIdPermisions = new WebservicesDAL().GetByIDPermisions(Convert.ToInt64(((Control) sender).ID.Remove(0, 6)));
      USER user = (USER) this.Session["user"];
      if (user == null || user.Type != 1)
        return;
      this.Session["AdminSelectedInstitution"] = (object) null;
      this.Session["AdminSelectedWS"] = (object) byIdPermisions;
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkU_Click(object sender, EventArgs e)
    {
      HelpClassUsers byIdPermisions = new UsersDAL().GetByIDPermisions(new Guid(Convert.ToString(((Control) sender).ID.Remove(0, 5))));
      USER user = (USER) this.Session["user"];
      if (user == null || user.Type != 1)
        return;
      this.Session["AdminSelectedInstitution"] = (object) null;
      this.Session["AdminSelectedWS"] = (object) null;
      this.Session["AdminSelectedUser"] = (object) byIdPermisions;
      this.Response.Redirect("Default.aspx");
    }

    protected void LinkInst_Click(object sender, EventArgs e)
    {
      INSTITUTION byId = new InstitutionsDAL().GetByID(Convert.ToInt64(((Control) sender).ID.Remove(0, 8)));
      this.Session["AdminSelectedInstitution"] = (object) byId;
      this.FillPanelUsers(byId);
      this.FillPanelServices(byId);
    }
  }
}
