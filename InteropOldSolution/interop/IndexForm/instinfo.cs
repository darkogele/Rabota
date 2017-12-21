// Decompiled with JetBrains decompiler
// Type: interop.IndexForm.instinfo
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

namespace interop.IndexForm
{
  public class instinfo : UserControl
  {
    protected Image Image1;
    protected Panel PanelInstitucii;
    protected Image Image2;
    protected Label Label1;
    protected Label Label2;
    protected Image Image3;
    protected Panel PanelServices;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu3"] = (object) "Help/korisnik/Help_institucii_obicen.aspx";
    }

    protected void PanelServices_Load(object sender, EventArgs e)
    {
      INSTITUTION objectInstitution = (INSTITUTION) this.Session["SelectedInstitution"];
      this.Label1.Text = objectInstitution.Tittle;
      this.Label2.Text = objectInstitution.Description;
      this.FillPanelServices(objectInstitution);
    }

    public void FillPanelServices(INSTITUTION objectInstitution)
    {
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      INSTITUTION institution = (INSTITUTION) this.Session["SelectedInstitution"];
      List<WEBSERVICE> list = new List<WEBSERVICE>();
      foreach (HelpClassWebServices classWebServices in servicesPermissions)
      {
        if (classWebServices.IDInstitution == institution.ID)
          list.Add(classWebServices.WSObj);
      }
      this.PanelServices.Controls.Clear();
      foreach (WEBSERVICE webservice in list)
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
      List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, (USER) this.Session["user"]);
      List<INSTITUTION> list = new List<INSTITUTION>();
      foreach (long id in Enumerable.Distinct<long>(Enumerable.Select<HelpClassWebServices, long>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, long>) (p => p.IDInstitution))))
      {
        INSTITUTION byId = new InstitutionsDAL().GetByID(id);
        if (byId != null)
          list.Add(byId);
      }
      this.PanelInstitucii.Controls.Clear();
      foreach (INSTITUTION institution in list)
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
      if (user == null)
        return;
      if (user.Type == 1)
      {
        this.Session["SelectedWS"] = (object) byIdPermisions;
        this.Response.Redirect("Default.aspx");
      }
      else if (user.Type == 2)
      {
        this.Session["SelectedWS"] = (object) byIdPermisions;
        this.Response.Redirect("Default.aspx");
      }
      else if (user.Type == 3)
      {
        this.Session["SelectedWS"] = (object) byIdPermisions;
        this.Response.Redirect("Default.aspx");
      }
    }

    protected void LinkInst_Click(object sender, EventArgs e)
    {
      INSTITUTION byId = new InstitutionsDAL().GetByID(Convert.ToInt64(((Control) sender).ID.Remove(0, 8)));
      this.Session["SelectedInstitution"] = (object) byId;
      this.Label1.Text = byId.Tittle;
      this.Label2.Text = byId.Description;
      this.FillPanelServices(byId);
    }
  }
}
