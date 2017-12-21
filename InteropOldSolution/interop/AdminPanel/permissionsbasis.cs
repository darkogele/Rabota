// Decompiled with JetBrains decompiler
// Type: interop.AdminPanel.permissionsbasis
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

namespace interop.AdminPanel
{
  public class permissionsbasis : UserControl
  {
    protected Panel PanelNewUser;
    protected Image Image1;
    protected Label lblInstitucija;
    protected DropDownList DropDonwListInstitution;
    protected Label lblServis;
    protected DropDownList DropDownListServis;
    protected GridView GridviewOsnoviNaBaranje;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu1"] = (object) "Help/superadmin/HelpOsnovServisi.aspx";
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
      if (this.Session["ListOsnoviSelected"] == null)
        this.Session["ListOsnoviSelected"] = (object) new List<BASIS>();
      if (this.DropDonwListInstitution.SelectedValue == "0" || this.DropDonwListInstitution.SelectedValue == string.Empty)
      {
        List<INSTITUTION> list = new List<INSTITUTION>();
        List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
        list.Add(new INSTITUTION()
        {
          ID = 0L,
          Tittle = "-- Институции --"
        });
        list.AddRange((IEnumerable<INSTITUTION>) allActiveDeleted);
        this.DropDonwListInstitution.DataTextField = "Tittle";
        this.DropDonwListInstitution.DataValueField = "ID";
        this.DropDonwListInstitution.DataSource = (object) list;
        this.DropDonwListInstitution.DataBind();
      }
      else
      {
        this.GridviewOsnoviNaBaranje.DataSource = this.Session["ListOsnoviAll"];
        this.GridviewOsnoviNaBaranje.DataBind();
      }
    }

    protected void btn_vnesi_Click(object sender, EventArgs e)
    {
    }

    protected void GridviewOsnoviNaBaranje_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ImageButton imageButton = (ImageButton) e.Row.FindControl("Insert");
      BASIS c = (BASIS) e.Row.DataItem;
      if (c != null)
      {
        imageButton.CommandArgument = c.ID.ToString();
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
        long num = Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]);
        List<BASIS> list1 = (List<BASIS>) this.Session["ListOsnoviSelected"];
        List<BASIS> list2 = (List<BASIS>) this.Session["ListOsnoviSelected"];
        List<BASIS> list3 = new List<BASIS>();
        foreach (BASIS basis in list1)
        {
          if (basis.ID == num)
          {
            list1.Remove(basis);
            using (List<WEBSERVICESBASIS>.Enumerator enumerator = new WSBasisDAL().GetActiveByInstitutionWebServiceAndBasis(Convert.ToInt64(this.DropDonwListInstitution.SelectedValue), Convert.ToInt64(this.DropDownListServis.SelectedValue), basis.ID).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                WEBSERVICESBASIS current = enumerator.Current;
                new WSBasisDAL().Update(new long?(), new long?(), new DateTime?(), new bool?(false), current.ID);
                string old = "";
                string newone = "";
                new LOGDAL().Insert(Enumerable.First<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "WEBSERVICESBASIS")).Key, (USER) this.Session["user"], current.ID.ToString(), 2, DateTime.Now, old, newone);
              }
              break;
            }
          }
        }
        this.Session["ListOsnoviSelected"] = (object) list1;
        this.GridviewOsnoviNaBaranje.DataBind();
      }
      else
      {
        if (!(e.CommandName == "InsertWS"))
          return;
        BASIS byId = new BasisDAL().GetByID(Convert.ToInt64(((WebControl) e.CommandSource).Attributes["1"]));
        List<BASIS> list = (List<BASIS>) this.Session["ListOsnoviSelected"];
        list.Add(byId);
        foreach (PERMISSION permission in new PermissionsDAL().GetPermisionsByInstitutionandWebService(Convert.ToInt64(this.DropDonwListInstitution.SelectedValue), Convert.ToInt64(this.DropDownListServis.SelectedValue)))
        {
          long num = new WSBasisDAL().Insert(byId.ID, permission.ID, DateTime.Now, true);
          string old = "";
          string newone = "";
          new LOGDAL().Insert(Enumerable.First<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (d => d.Value == "WEBSERVICESBASIS")).Key, (USER) this.Session["user"], num.ToString(), 2, DateTime.Now, old, newone);
        }
        this.Session["ListOsnoviSelected"] = (object) list;
        this.GridviewOsnoviNaBaranje.DataBind();
      }
    }

    protected void DropDonwListInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
      WEBSERVICE webservice = new WEBSERVICE();
      webservice.ID = 0L;
      webservice.Tittle = "-- Сервиси --";
      List<WEBSERVICE> list = new List<WEBSERVICE>();
      List<WEBSERVICE> institutionPermission = new WebservicesDAL().GetAllByInstitutionPermission(Convert.ToInt64(this.DropDonwListInstitution.SelectedValue));
      list.Add(webservice);
      list.AddRange((IEnumerable<WEBSERVICE>) institutionPermission);
      this.DropDownListServis.DataTextField = "Tittle";
      this.DropDownListServis.DataValueField = "ID";
      this.DropDownListServis.DataSource = (object) list;
      this.DropDownListServis.DataBind();
    }

    protected void DropDownListServis_SelectedIndexChanged(object sender, EventArgs e)
    {
      List<BASIS> allByStatus = new BasisDAL().GetAllByStatus(true);
      this.Session["ListOsnoviAll"] = (object) allByStatus;
      List<BASIS> institutionAndWebService = new BasisDAL().GetActiveByInstitutionAndWebService(Convert.ToInt64(this.DropDonwListInstitution.SelectedValue), Convert.ToInt64(this.DropDownListServis.SelectedValue));
      if (institutionAndWebService != null)
        this.Session["ListOsnoviSelected"] = (object) institutionAndWebService;
      else
        this.Session["ListOsnoviSelected"] = (object) new List<BASIS>();
      this.GridviewOsnoviNaBaranje.DataSource = (object) allByStatus;
      this.GridviewOsnoviNaBaranje.DataBind();
    }

    protected void GridviewOsnoviNaBaranje_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.GridviewOsnoviNaBaranje.PageIndex = e.NewPageIndex;
      this.GridviewOsnoviNaBaranje.DataBind();
    }
  }
}
