// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.wsusagel
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using FaultMessageAKN;
using FaultMessageCU;
using FaultMessageUJP;
using interop;
using interop.AdminPanel;
using interop.WebServiceAKN;
using interop.WebServiceAKNOpstiniDataSets;
using interop.WebServiceAKNParceli;
using interop.WebServiceCR_AKN;
using interop.WebServiceCR_CU;
using interop.WebServiceCR_UJP;
using interop.WebServiceUJP_EDB;
using interop.WebServiceUJP_NAZIV;
using interop.WSSendEmail;
using InteropClassLibrary;
using StringProtection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WebApplicationInterop;

namespace interop.LocalAdminPanel
{
  public class wsusagel : UserControl
  {
    private IRepository classobj = (IRepository) new InteropClassLibrary.InteropDAL(0);
    private Service1 wsEmail = new Service1();
    protected Image Image1;
    protected Label Label2;
    protected Panel ControlParams;
    protected Label lblosnov;
    protected DropDownList DropDownListOsnov;
    protected Button btnSubmit;
    protected Label Label1;
    protected Panel PanelInfoParams;
    protected Image Image2;
    protected MultiView MultiView;
    protected View ViewAKN;
    protected ImageButton ImageButtonAKN;
    protected Panel WSResponseControlPanelAKN;
    protected View ViewAKNParceli;
    protected ImageButton ImageButtonAKNParceli;
    protected Panel WSResponseControlPanelAKNParceli;
    protected View ViewMVR;
    protected ImageButton ImageButtonMVR;
    protected Panel WSResponseControlPanelMVR;
    protected View ViewCR_AKN;
    protected ImageButton ImageButtonCR_AKN;
    protected Panel WSResponseControlPanelCR_AKN;
    protected View ViewCR_CU;
    protected ImageButton ImageButtonCR_CU;
    protected Panel WSResponseControlPanelCR_CU;
    protected View ViewCR_UJP;
    protected ImageButton ImageButtonCR_UJP;
    protected Panel WSResponseControlPanelCR_UJP;
    protected View ViewUJP_EDB;
    protected ImageButton ImageButtonUJP_EDB;
    protected Panel WSResponseControlPanelUJP_EDB;
    protected View ViewUJP_NAZIV;
    protected Panel WSResponseControlPanelUJP_NAZIV;
    protected GridView NaziviGridView;
    protected ImageButton ImageButtonUJP_NAZIV;
    private bool _Completed;

    public bool Completed
    {
      get
      {
        return this._Completed;
      }
      set
      {
        this._Completed = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      HelpClassWebServices classWebServices = (HelpClassWebServices) this.Session["LocalSelectedWS"];
      if (classWebServices == null)
        return;
      if (classWebServices.ID == 1L)
        this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiImotenListPrebaruvanje.aspx";
      else if (classWebServices.ID == 16L)
        this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiPodatociZaParceliPrebaruvanje.aspx";
      else if (classWebServices.ID == 2L)
        this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiDrzavjanstvoPrebaruvanje.aspx";
      else if (classWebServices.ID == 10L)
        this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiMBAKNPrebaruvanje.aspx";
      else if (classWebServices.ID == 15L)
        this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisiMBCUPrebaruvanje.aspx";
      List<BASIS> list1 = new List<BASIS>();
      List<interop.PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser((interop.USER) this.Session["user"]);
      if (permisionsByUser.Count > 0)
        list1 = new BasisDAL().GetActiveByInstitutionAndWebService(permisionsByUser[0].ID_INST, classWebServices.ID);
      List<BASIS> list2 = new List<BASIS>();
      list2.Add(new BASIS()
      {
        ID = 0L,
        Tittle = "-- Основ на барање --"
      });
      foreach (BASIS basis in list1)
        list2.Add(basis);
      this.DropDownListOsnov.DataSource = (object) Enumerable.Distinct<BASIS>((IEnumerable<BASIS>) list2);
      this.DropDownListOsnov.DataTextField = "Tittle";
      this.DropDownListOsnov.DataValueField = "ID";
      this.DropDownListOsnov.DataBind();
      this.Label1.Text = classWebServices.Description;
      this.Label1.Text = classWebServices.Description;
      string tittle = classWebServices.Tittle;
      this.Label2.Text = tittle + ":";
      this.Session["NazivServis"] = (object) tittle;
      List<interop.PARAM> paramsForWebservice = new ParamsDAL().GetParamsForWebservice(new WebservicesDAL().GetByID(classWebServices.ID));
      this.Session["WebServiceParametri"] = (object) paramsForWebservice;
      int num = 0;
      foreach (interop.PARAM obj in paramsForWebservice)
      {
        TextBox textBox = new TextBox();
        textBox.ID = "txt" + num.ToString();
        textBox.Width = (Unit) 150;
        Label label1 = new Label();
        label1.Text = obj.Tittle;
        label1.Width = (Unit) 150;
        new Label()
        {
          Text = obj.Description
        }.Width = (Unit) 200;
        if (obj.Tittle == "Матичен број")
          textBox.MaxLength = 13;
        else if (obj.Tittle == "Матичен број на правно лице")
        {
          obj.Tittle = "Матичен број на правно лице, даночен број или назив на субјектот";
          label1.Text = obj.Tittle;
          label1.Width = (Unit) 300;
          label1.Style.Add("padding-bottom", "10px");
          textBox.Style.Add("Width", "305px");
        }
        else if (obj.Tittle == "Даночен број")
        {
          textBox.MaxLength = 13;
          label1.Style.Add("padding-bottom", "10px");
          textBox.Style.Add("Width", "305px");
        }
        label1.ID = "lbl" + num.ToString();
        this.Session["txt" + num.ToString()] = (object) textBox;
        LiteralControl literalControl1 = new LiteralControl("<p>");
        LiteralControl literalControl2 = new LiteralControl("</p>");
        this.ControlParams.Controls.Add((Control) label1);
        this.ControlParams.Controls.Add((Control) textBox);
        Label label2 = new Label();
        label2.Text = obj.Tittle + " -- ";
        label2.Font.Bold = true;
        Label label3 = new Label();
        label3.Text = obj.Description;
        LiteralControl literalControl3 = new LiteralControl("<p>");
        LiteralControl literalControl4 = new LiteralControl("</p>");
        LiteralControl literalControl5 = new LiteralControl("<br />");
        this.PanelInfoParams.Controls.Add((Control) literalControl3);
        this.PanelInfoParams.Controls.Add((Control) label2);
        this.PanelInfoParams.Controls.Add((Control) label3);
        this.PanelInfoParams.Controls.Add((Control) literalControl4);
        this.PanelInfoParams.Controls.Add((Control) literalControl5);
        LiteralControl literalControl6;
        try
        {
          if (obj.Tittle == "Град")
          {
            WCFServiceAKNDataSetsClient aknDataSetsClient = new WCFServiceAKNDataSetsClient();
            aknDataSetsClient.ClientCredentials.UserName.UserName = "interop\\administrator";
            aknDataSetsClient.ClientCredentials.UserName.Password = "Ursula23";
            opstini gradoviDataSet = aknDataSetsClient.GetGradoviDataSet();
            List<KatOpstini> list3 = new List<KatOpstini>();
            list3.Add(new KatOpstini()
            {
              IDopstina = "0",
              Naziv = "- - Град - -"
            });
            foreach (opstina opstina in gradoviDataSet.nizopsField)
              list3.Add(new KatOpstini()
              {
                IDopstina = opstina.opsField.ToString(),
                Naziv = opstina.nazivopstinaField
              });
            GradComparerByName gradComparerByName = new GradComparerByName();
            list3.Sort((IComparer<KatOpstini>) gradComparerByName);
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = "GradID";
            dropDownList.AutoPostBack = true;
            dropDownList.DataTextField = "Naziv";
            dropDownList.DataValueField = "IDopstina";
            dropDownList.Width = (Unit) 160;
            dropDownList.SelectedIndexChanged += new EventHandler(this.DropGrad_SelectedIndexChanged);
            dropDownList.Attributes.Add("1", textBox.ID);
            dropDownList.Items.Clear();
            dropDownList.DataSource = (object) list3;
            dropDownList.DataBind();
            literalControl6 = new LiteralControl("&nbsp");
            Label label4 = new Label();
            label4.Width = (Unit) 150;
            this.ControlParams.Controls.Add((Control) label4);
            this.ControlParams.Controls.Add((Control) dropDownList);
          }
          if (obj.Tittle == "Општина")
          {
            DropDownList dropDownList1 = (DropDownList) this.ControlParams.FindControl("GradID");
            DropDownList dropDownList2 = new DropDownList();
            dropDownList2.ID = "filter";
            dropDownList2.AutoPostBack = true;
            dropDownList2.DataTextField = "Naziv";
            dropDownList2.DataValueField = "IDopstina";
            dropDownList2.Width = (Unit) 160;
            dropDownList2.Items.Add(new ListItem()
            {
              Value = "0",
              Text = "- - Општина - -"
            });
            dropDownList2.SelectedIndexChanged += new EventHandler(this.DropGrad1_SelectedIndexChanged);
            dropDownList2.Attributes.Add("1", textBox.ID);
            literalControl6 = new LiteralControl("&nbsp");
            Label label4 = new Label();
            label4.Width = (Unit) 150;
            this.ControlParams.Controls.Add((Control) label4);
            this.ControlParams.Controls.Add((Control) dropDownList2);
          }
        }
        catch
        {
          List<KatOpstini> list3 = new List<KatOpstini>();
          list3.Add(new KatOpstini()
          {
            IDopstina = "0",
            Naziv = "Услугата е недостапна"
          });
          DropDownList dropDownList = new DropDownList();
          dropDownList.ID = "GradID";
          dropDownList.AutoPostBack = true;
          dropDownList.DataTextField = "Naziv";
          dropDownList.DataValueField = "IDopstina";
          dropDownList.Width = (Unit) 160;
          dropDownList.SelectedIndexChanged += new EventHandler(this.DropGrad_SelectedIndexChanged);
          dropDownList.Attributes.Add("1", textBox.ID);
          dropDownList.Items.Clear();
          dropDownList.DataSource = (object) list3;
          dropDownList.DataBind();
          literalControl6 = new LiteralControl("&nbsp");
          Label label4 = new Label();
          label4.Width = (Unit) 150;
          this.ControlParams.Controls.Add((Control) label4);
          this.ControlParams.Controls.Add((Control) dropDownList);
          dropDownList.DataSource = (object) list3;
          dropDownList.DataBind();
        }
        ++num;
        if (!(tittle == "Податоци за ЕМБС за потребите на ЦУ") && !(tittle == "Податоци за ЕМБС за потребите на УЈП"))
        {
          if (tittle == "Единствен Даночен Број На Субјектот")
          {
            this.Session["NaziviCR_CU"] = (object) null;
            this.Session["NaziviCR_AKN"] = (object) null;
            this.Session["NaziviCR_UJP"] = (object) null;
            break;
          }
          if (tittle == "Назив На Субјектот")
          {
            this.Session["NaziviCR_CU"] = (object) null;
            this.Session["NaziviCR_AKN"] = (object) null;
            this.Session["NaziviCR_UJP"] = (object) null;
            break;
          }
        }
        else
          break;
      }
    }

    protected void DropGrad_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((TextBox) this.ControlParams.FindControl(((WebControl) sender).Attributes["1"])).Text = ((ListControl) sender).SelectedItem.Text.Trim(' ');
      string opstina = ((ListControl) sender).SelectedItem.Value;
      katopstini opstiniDataSet = new WCFServiceAKNDataSetsClient().GetOpstiniDataSet(opstina);
      List<KatOpstini> list = new List<KatOpstini>();
      DataSet dataSet = new DataSet();
      list.Add(new KatOpstini()
      {
        IDopstina = "0",
        Naziv = "- - Општина - -"
      });
      foreach (katopstina katopstina in opstiniDataSet.nizkopsField)
      {
        KatOpstini katOpstini = new KatOpstini();
        int num = katopstina.opsField;
        string str1 = num.ToString();
        num = katopstina.kopsField;
        string str2 = num.ToString();
        string str3 = katopstina.nazivkatastarskaopstinaField.Trim(' ');
        if (str1 == opstina)
        {
          katOpstini.IDopstina = str2;
          katOpstini.Naziv = str3;
          list.Add(katOpstini);
        }
      }
      GradComparerByName gradComparerByName = new GradComparerByName();
      list.Sort((IComparer<KatOpstini>) gradComparerByName);
      this.Session["OpstiniFilter"] = (object) list;
      ((BaseDataBoundControl) this.ControlParams.FindControl("filter")).DataSource = (object) list;
      this.ControlParams.FindControl("filter").DataBind();
    }

    protected void DropGrad1_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((TextBox) this.ControlParams.FindControl(((WebControl) sender).Attributes["1"])).Text = ((ListControl) sender).SelectedItem.Text;
    }

    private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
    {
      return true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      HelpClassWebServices classWebServices = (HelpClassWebServices) this.Session["LocalSelectedWS"];
      if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceAKN"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewAKN);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          this.ImotenListData();
        }
        else
        {
          this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
          this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
          this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenAKN"] = (object) label2.Text;
          this.ImageButtonAKN.Visible = false;
        }
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceAKNParceli"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewAKNParceli);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          this.ParceliData();
        }
        else
        {
          this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
          this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
          this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenAKN"] = (object) label2.Text;
          this.ImageButtonAKN.Visible = false;
        }
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceMVR"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewMVR);
        this.WSResponseControlPanelMVR.Controls.Add((Control) new LiteralControl("<h3>"));
        Label label1 = new Label();
        label1.ID = "lblporaka";
        label1.Text = "Порака: ";
        Label label2 = new Label();
        label2.ID = "lblporakavalue";
        label2.Text = "Услугата е недостапна. Во моментов е во изградба!";
        this.WSResponseControlPanelMVR.Controls.Add((Control) label1);
        this.WSResponseControlPanelMVR.Controls.Add((Control) label2);
        this.WSResponseControlPanelMVR.Controls.Add((Control) new LiteralControl("</h3>"));
        this.Session["neuspesenMVR"] = (object) label2.Text;
        this.ImageButtonMVR.Visible = false;
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceUJP_EDB"])
      {
        this.Session["IsEDB"] = (object) null;
        this.Session["EDB"] = (object) null;
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewUJP_EDB);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          try
          {
            double result;
            if (double.TryParse(((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim(), out result) && result.ToString().Length == 13)
            {
              this.GetDanocenBrojData();
            }
            else
            {
              this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
              this.GetFirmiByNaziv();
            }
          }
          catch
          {
          }
        }
        else
        {
          this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label1);
          this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label2);
          this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenUJP_EDB"] = (object) label2.Text;
          this.ImageButtonUJP_EDB.Visible = false;
        }
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceUJP_NAZIV"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          this.GetFirmiByNaziv();
        }
        else
        {
          this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
          this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
          this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenUJP_NAZIV"] = (object) label2.Text;
          this.ImageButtonUJP_NAZIV.Visible = false;
        }
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceCR_AKN"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewCR_AKN);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          try
          {
            string s = ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim();
            double result;
            bool flag = double.TryParse(s, out result);
            if (flag && result.ToString().Length == 7)
              this.GetEMBSforAKN();
            else if (flag && result.ToString().Length == 13)
            {
              interop.USER user = (interop.USER) this.Session["user"];
              this.Session["IsMBS"] = (object) true;
              WSHttpBinding wsHttpBinding = new WSHttpBinding();
              wsHttpBinding.Name = "myBinding";
              wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
              wsHttpBinding.Security.Mode = SecurityMode.Message;
              wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
              EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
              UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
              reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
              EDB EDB = new EDB();
              EDB.Username = user.username;
              EDB.Password = user.password;
              EDB.EDB1 = s;
              EDB.NacinNaIsprakjanje = "EORPD";
              EDB.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
              EDB.TimeStamp = DateTime.Now.ToString();
              reqrespPortClient.Open();
              this.Session["MBS"] = (object) reqrespPortClient.GetDataByEDB(EDB).GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
              this.GetEMBSforAKN();
            }
            else
            {
              this.Session["NaziviCR_AKN"] = (object) true;
              this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
              this.GetFirmiByNaziv();
            }
          }
          catch
          {
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Системска грешка. Обидете се повторно.";
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
        }
        else
        {
          this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
          this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
          this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenAKN"] = (object) label2.Text;
          this.ImageButtonAKN.Visible = false;
        }
      }
      else if (classWebServices.URL == ConfigurationManager.AppSettings["WebServiceCR_CU"])
      {
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewCR_CU);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          try
          {
            string s = ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim();
            double result;
            bool flag = double.TryParse(s, out result);
            if (flag && result.ToString().Length == 7)
              this.GetEMBSforCU();
            else if (flag && result.ToString().Length == 13)
            {
              interop.USER user = (interop.USER) this.Session["user"];
              this.Session["IsMBS"] = (object) true;
              WSHttpBinding wsHttpBinding = new WSHttpBinding();
              wsHttpBinding.Name = "myBinding";
              wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
              wsHttpBinding.Security.Mode = SecurityMode.Message;
              wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
              EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
              UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
              reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserCU");
              EDB EDB = new EDB();
              EDB.Username = user.username;
              EDB.Password = user.password;
              EDB.EDB1 = s;
              EDB.NacinNaIsprakjanje = "EORPD";
              EDB.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
              EDB.TimeStamp = DateTime.Now.ToString();
              reqrespPortClient.Open();
              this.Session["MBS"] = (object) reqrespPortClient.GetDataByEDB(EDB).GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
              this.GetEMBSforCU();
            }
            else
            {
              this.Session["NaziviCR_CU"] = (object) true;
              this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
              this.GetFirmiByNaziv();
            }
          }
          catch
          {
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Системска грешка. Обидете се повторно.";
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
        }
        else
        {
          this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
          this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
          this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenAKN"] = (object) label2.Text;
          this.ImageButtonAKN.Visible = false;
        }
      }
      else
      {
        if (!(classWebServices.URL == ConfigurationManager.AppSettings["WebServiceCR_UJP"]))
          return;
        this.MultiView.Visible = true;
        this.MultiView.SetActiveView(this.ViewCR_UJP);
        if (this.DropDownListOsnov.SelectedValue != "0" || !this.DropDownListOsnov.Visible)
        {
          try
          {
            string s = ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim();
            double result;
            bool flag = double.TryParse(s, out result);
            if (flag && result.ToString().Length == 7)
              this.GetEMBSforUJP();
            else if (flag && result.ToString().Length == 13)
            {
              interop.USER user = (interop.USER) this.Session["user"];
              this.Session["IsMBS"] = (object) true;
              WSHttpBinding wsHttpBinding = new WSHttpBinding();
              wsHttpBinding.Name = "myBinding";
              wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
              wsHttpBinding.Security.Mode = SecurityMode.Message;
              wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
              EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
              UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
              reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
              EDB EDB = new EDB();
              EDB.Username = user.username;
              EDB.Password = user.password;
              EDB.EDB1 = s;
              EDB.NacinNaIsprakjanje = "EORPD";
              EDB.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
              EDB.TimeStamp = DateTime.Now.ToString();
              reqrespPortClient.Open();
              this.Session["MBS"] = (object) reqrespPortClient.GetDataByEDB(EDB).GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
              this.GetEMBSforUJP();
            }
            else
            {
              this.Session["NaziviCR_UJP"] = (object) true;
              this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
              this.GetFirmiByNaziv();
            }
          }
          catch
          {
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Системска грешка. Обидете се повторно.";
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
            this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
        }
        else
        {
          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
          Label label1 = new Label();
          label1.ID = "lblporaka";
          label1.Text = "Порака: ";
          Label label2 = new Label();
          label2.ID = "lblporakavalue";
          label2.Text = "Задолжително одберете основ на барање!";
          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
          this.Session["neuspesenCR_UJP"] = (object) label2.Text;
          this.ImageButtonAKN.Visible = false;
        }
      }
    }

    protected void ControlParams_Load(object sender, EventArgs e)
    {
      List<interop.PARAM> list = (List<interop.PARAM>) this.Session["WebServiceParametri"];
      if (list == null)
        return;
      int num = 0;
      foreach (interop.PARAM obj in list)
      {
        TextBox textBox = new TextBox();
        textBox.ID = "txt" + num.ToString();
        Label label = new Label();
        label.Text = obj.Tittle;
        label.ID = "lbl" + num.ToString();
        this.Session["txt" + num.ToString()] = (object) textBox;
        this.ControlParams.Controls.Add((Control) label);
        this.ControlParams.Controls.Add((Control) textBox);
        ++num;
      }
    }

    private void GetDanocenBrojData()
    {
      interop.USER user = (interop.USER) this.Session["user"];
      string str = this.Session["IsEDB"] == null || !Convert.ToBoolean(this.Session["IsEDB"]) || (this.Session["EDB"] == null || this.Session["EDB"].ToString().Length != 13) ? ((TextBox) this.ControlParams.FindControl("txt0")).Text : this.Session["EDB"].ToString();
      WSHttpBinding wsHttpBinding = new WSHttpBinding();
      wsHttpBinding.Name = "myBinding";
      wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      wsHttpBinding.Security.Mode = SecurityMode.Message;
      wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
      EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
      UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
      reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserUJP");
      EDB EDB = new EDB();
      EDB.Username = user.username;
      EDB.Password = user.password;
      EDB.EDB1 = str;
      EDB.NacinNaIsprakjanje = "EORPD";
      EDB.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
      EDB.TimeStamp = DateTime.Now.ToString();
      int num = 1;
      while (num <= 3 && !this.Completed)
      {
        ++num;
        try
        {
          reqrespPortClient.Open();
          GetPodatociByEDBResponse dataByEdb = reqrespPortClient.GetDataByEDB(EDB);
          if (dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka != null && dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka.Equals("NEUSPESNA TRANSAKCIJA!"))
          {
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕУСПЕШНА ОПЕРАЦИЈА!";
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_EDB"] = (object) label2.Text;
            this.ImageButtonUJP_EDB.Visible = false;
          }
          else if (dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka != null && dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka.Equals("Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!"))
          {
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_EDB"] = (object) label2.Text;
            this.ImageButtonUJP_EDB.Visible = false;
          }
          else if (dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka != null && dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka.Trim() != null && dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka.Trim() != string.Empty)
          {
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.GreshkaPoraka.Trim();
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_EDB"] = (object) label2.Text;
            this.ImageButtonUJP_EDB.Visible = false;
          }
          else
          {
            GridView gridView = new GridView();
            gridView.CssClass = "GridViewStyle";
            gridView.AlternatingRowStyle.CssClass = "AltRowStyle";
            gridView.RowStyle.CssClass = "RowStyle";
            gridView.HeaderStyle.CssClass = "HeaderStyle";
            gridView.Attributes.Add("style", "width: auto; min-width:500px");
            gridView.ID = "GridViewList";
            DataColumn column1 = new DataColumn("Наслов на податок");
            DataColumn column2 = new DataColumn("Податок");
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(column1);
            dataTable.Columns.Add(column2);
            DataRow row1 = dataTable.NewRow();
            row1[0] = (object) "Адреса";
            row1[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Adresa;
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2[0] = (object) "Банка";
            row2[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Banka;
            dataTable.Rows.Add(row2);
            DataRow row3 = dataTable.NewRow();
            row3[0] = (object) "ЕДБ";
            row3[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Edb;
            dataTable.Rows.Add(row3);
            DataRow row4 = dataTable.NewRow();
            row4[0] = (object) "Факс";
            row4[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Faks;
            dataTable.Rows.Add(row4);
            DataRow row5 = dataTable.NewRow();
            row5[0] = (object) "Матичен Број";
            row5[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB;
            dataTable.Rows.Add(row5);
            DataRow row6 = dataTable.NewRow();
            row6[0] = (object) "Место";
            row6[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Mesto_naziv;
            dataTable.Rows.Add(row6);
            DataRow row7 = dataTable.NewRow();
            row7[0] = (object) "Назив";
            row7[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Naziv;
            dataTable.Rows.Add(row7);
            DataRow row8 = dataTable.NewRow();
            row8[0] = (object) "Тип";
            row8[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Of_opis;
            dataTable.Rows.Add(row8);
            DataRow row9 = dataTable.NewRow();
            row9[0] = (object) "Опис";
            row9[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Opis_nace;
            dataTable.Rows.Add(row9);
            DataRow row10 = dataTable.NewRow();
            row10[0] = (object) "Телефон";
            row10[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Telefon;
            dataTable.Rows.Add(row10);
            DataRow row11 = dataTable.NewRow();
            row11[0] = (object) "Жиро Сметка";
            row11[1] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.Ziro;
            dataTable.Rows.Add(row11);
            gridView.DataSource = (object) dataTable;
            gridView.DataBind();
            List<string> list = new List<string>();
            Label label1 = new Label();
            label1.Text = "Управа за јавни приходи: ";
            Label label2 = new Label();
            label2.Text = "Податоци за субјектот: ";
            label2.Attributes.Add("style", "font-weight: bold;");
            gridView.DataSource = (object) dataTable;
            gridView.Attributes.Add("style", "width: 70%");
            gridView.CssClass = "GridViewStyle";
            gridView.AlternatingRowStyle.CssClass = "AltRowStyle";
            gridView.RowStyle.CssClass = "RowStyle";
            gridView.HeaderStyle.CssClass = "HeaderStyle";
            gridView.Attributes.Add("style", "width: auto; min-width:500px");
            gridView.DataBind();
            LiteralControl literalControl1 = new LiteralControl("<h3>");
            list.Add(new Label()
            {
              Text = "Единствен Даночен Број На Субјектот"
            }.Text);
            LiteralControl literalControl2 = new LiteralControl("</h3>");
            LiteralControl literalControl3 = new LiteralControl("<p>");
            LiteralControl literalControl4 = new LiteralControl("</p>");
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) literalControl3);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) literalControl4);
            LiteralControl literalControl5 = new LiteralControl("<br></br>");
            this.WSResponseControlPanelUJP_EDB.Controls.Add((Control) gridView);
            this.Session["GridViewListEDB"] = (object) gridView;
            this.ImageButtonUJP_EDB.Visible = true;
            this.Session["ListaRezultHeaders"] = (object) list;
            this.Session["ParametersPrint"] = (object) ("Единствен Даночен Број_" + EDB.EDB1);
          }
          this.Completed = true;
          reqrespPortClient.Close();
        }
        catch
        {
          if (num == 3)
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.ImageButtonAKN.Visible = false;
            reqrespPortClient.Close();
          }
          reqrespPortClient.Close();
        }
      }
    }

    private void GetFirmiByNaziv()
    {
      interop.USER user = (interop.USER) this.Session["user"];
      WSHttpBinding wsHttpBinding = new WSHttpBinding();
      wsHttpBinding.Name = "myBinding";
      wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      wsHttpBinding.Security.Mode = SecurityMode.Message;
      wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
      EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationnazivi.interop.local/UJPBizTalkApplicationNazivi/UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
      UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationNazivi_Orchestration_1_UJP_NAZIVI_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
      reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserUJP");
      FirmiNaziv FirmiNaziv = new FirmiNaziv();
      FirmiNaziv.Username = user.username;
      FirmiNaziv.Password = user.password;
      FirmiNaziv.Naziv = ((TextBox) this.ControlParams.FindControl("txt0")).Text;
      FirmiNaziv.NacinNaIsprakjanje = "EORPD";
      FirmiNaziv.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
      FirmiNaziv.TimeStamp = DateTime.Now.ToString();
      int num = 1;
      while (num <= 3 && !this.Completed)
      {
        ++num;
        try
        {
          reqrespPortClient.Open();
          GetPodatociFirmiNaziviByNazivResponse firmiNaziv = reqrespPortClient.GetFirmiNaziv(FirmiNaziv);
          if (firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka != null && firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka.Trim().Equals("NEUSPESNA TRANSAKCIJA!"))
          {
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕУСПЕШНА ОПЕРАЦИЈА!";
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_NAZIV"] = (object) label2.Text;
            this.ImageButtonUJP_NAZIV.Visible = false;
          }
          else if (firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka != null && firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka.Trim().Equals("Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!"))
          {
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_NAZIV"] = (object) label2.Text;
            this.ImageButtonUJP_NAZIV.Visible = false;
          }
          else if (firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka != null && firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka.Trim() != null && firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka.Trim() != string.Empty)
          {
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.GreshkaPoraka.Trim();
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenUJP_NAZIV"] = (object) label2.Text;
            this.ImageButtonUJP_NAZIV.Visible = false;
          }
          else
          {
            firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.naziv.ToString();
            DataTable dataTable = new DataTable();
            DataColumn column1 = new DataColumn("Naziv");
            DataColumn column2 = new DataColumn("EDB");
            dataTable.Columns.Add(column1);
            dataTable.Columns.Add(column2);
            foreach (DokFirmiNazivStrukturaNazivEdb strukturaNazivEdb in firmiNaziv.GetPodatociFirmiNaziviByNazivResult.Telo.DokFirmiNaziv.NazivEdb)
            {
              DataRow row = dataTable.NewRow();
              row["Naziv"] = (object) strukturaNazivEdb.naziv;
              row["EDB"] = (object) strukturaNazivEdb.edb;
              dataTable.Rows.Add(row);
            }
            this.Session["NaziviDataTableList"] = (object) dataTable;
            this.NaziviGridView.Visible = true;
            this.NaziviGridView.DataSource = (object) dataTable;
            this.NaziviGridView.DataBind();
            List<string> list = new List<string>();
            Label label1 = new Label();
            label1.Text = "Управа за јавни приходи: ";
            Label label2 = new Label();
            label2.Text = ((TextBox) this.ControlParams.FindControl("txt0")).Text;
            label2.Attributes.Add("style", "font-weight: bold;");
            LiteralControl literalControl1 = new LiteralControl("<h3>");
            new Label().Text = "Податоци за субјекти";
            LiteralControl literalControl2 = new LiteralControl("</h3>");
            LiteralControl literalControl3 = new LiteralControl("<p>");
            LiteralControl literalControl4 = new LiteralControl("</p>");
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) literalControl3);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) literalControl4);
            LiteralControl literalControl5 = new LiteralControl("<br></br>");
            this.ImageButtonUJP_NAZIV.Visible = false;
            this.Session["ListaRezultHeaders"] = (object) list;
            this.Session["ParametersPrint"] = (object) ("Назив на субјекти_" + FirmiNaziv.Naziv);
          }
          this.Completed = true;
          reqrespPortClient.Close();
        }
        catch
        {
          if (num == 3)
          {
            this.WSResponseControlPanelUJP_NAZIV.Controls.Clear();
            this.NaziviGridView.Visible = false;
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на УЈП во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
            this.ImageButtonAKN.Visible = false;
            this.Completed = true;
            this.ImageButtonUJP_NAZIV.Visible = false;
          }
          else
          {
            this.WSResponseControlPanelUJP_NAZIV.Controls.Clear();
            this.NaziviGridView.Visible = false;
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Невалидни карактери! Не е подржан внесениот фонт";
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label1);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) label2);
            this.WSResponseControlPanelUJP_NAZIV.Controls.Add((Control) new LiteralControl("</h3>"));
            this.ImageButtonAKN.Visible = false;
            this.Completed = true;
            this.ImageButtonUJP_NAZIV.Visible = false;
          }
          this.SendMail("Neuspeshen povik na servisot: Податоци за субјекти od institucija: " + user.CERTIFICATE.Subject + "korisnik: " + user.username);
        }
      }
    }

    private void ImotenListData()
    {
      this.Completed = false;
      this.Session["GridViewAKN1"] = (object) null;
      this.Session["GridViewAKN2"] = (object) null;
      this.Session["GridViewAKN3"] = (object) null;
      this.Session["GridViewAKN4"] = (object) null;
      this.Session["neuspesenAKN"] = (object) "";
      interop.USER user = (interop.USER) this.Session["user"];
      WSHttpBinding wsHttpBinding = new WSHttpBinding();
      wsHttpBinding.Name = "myBinding";
      wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      wsHttpBinding.Security.Mode = SecurityMode.Message;
      wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
      EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://aknbiztalkapplication.interop.local/AKNBizTalkApplication/AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESP.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
      AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient portReqrespClient = new AKNBizTalkApplication_AKNBizTalkOrchestration_RECEIVE_PORT_REQRESPClient((Binding) wsHttpBinding, remoteAddress);
      portReqrespClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object)"UserKatastar");
      ImotenListSchema ImotenListSchema = new ImotenListSchema();
      ImotenListSchema.Username = user.username;
      ImotenListSchema.Password = user.password;
      ImotenListSchema.Grad = ((TextBox) this.ControlParams.FindControl("txt0")).Text;
      ImotenListSchema.Opstina = ((TextBox) this.ControlParams.FindControl("txt1")).Text;
      ImotenListSchema.ImotenList = ((TextBox) this.ControlParams.FindControl("txt2")).Text;
      ImotenListSchema.NacinNaIsprakjanje = "EORPD";
      ImotenListSchema.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
      ImotenListSchema.TimeStamp = DateTime.Now.ToString();
      int num1 = 1;
      while (num1 <= 3 && !this.Completed)
      {
        ++num1;
        try
        {
          portReqrespClient.Open();
          GetImotenListResponse imotenList = portReqrespClient.GetImotenList(ImotenListSchema);
          if (imotenList.GetImotenListResult.messageField.Equals("НЕУСПЕШНА ОПЕРАЦИЈА"))
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕУСПЕШНА ОПЕРАЦИЈА!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
          else if (imotenList.GetImotenListResult.messageField.Equals("NE POSTOI TAKOV I LIST "))
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕ ПОСТОИ БАРАНИОТ ИМОТЕН ЛИСТ!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
          else if (imotenList.GetImotenListResult.messageField.Equals("НЕ ПОСТОИ ИМОТНИОТ ЛИСТ "))
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕ ПОСТОИ БАРАНИОТ ИМОТЕН ЛИСТ!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
          else if (imotenList.GetImotenListResult.messageField.Equals("Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!"))
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKN.Visible = false;
          }
          else
          {
            GridView gridView1 = new GridView();
            gridView1.ID = "GridViewList";
            DataColumn column1 = new DataColumn("Број");
            DataColumn column2 = new DataColumn("Град");
            DataColumn column3 = new DataColumn("Општина");
            DataColumn column4 = new DataColumn("Порака");
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(column1);
            dataTable1.Columns.Add(column2);
            dataTable1.Columns.Add(column3);
            dataTable1.Columns.Add(column4);
            DataRow row1 = dataTable1.NewRow();
            row1["Број"] = (object) imotenList.GetImotenListResult.ilistField;
            row1["Град"] = (object) ((TextBox) this.ControlParams.FindControl("txt0")).Text.ToUpper();
            row1["Општина"] = (object) ((TextBox) this.ControlParams.FindControl("txt1")).Text.ToUpper();
            row1["Порака"] = !imotenList.GetImotenListResult.messageField.Equals("USPESNA OPERACAIJA") ? (object) "НЕУСПЕШНА ОПЕРАЦИЈА" : (object) "УСПЕШНА ОПЕРАЦИЈА";
            dataTable1.Rows.Add(row1);
            List<string> list = new List<string>();
            Label label1 = new Label();
            label1.Text = "Сектор за катастар на недвижности: ";
            Label label2 = new Label();
            label2.Text = ((TextBox) this.ControlParams.FindControl("txt0")).Text.ToUpper();
            label2.Attributes.Add("style", "font-weight: bold;");
            Label label3 = new Label();
            label3.Text = "Катастарска општина: ";
            Label label4 = new Label();
            label4.Text = ((TextBox) this.ControlParams.FindControl("txt1")).Text.ToUpper();
            label4.Attributes.Add("style", "font-weight: bold;");
            Label label5 = new Label();
            Label label6 = new Label();
            label6.Text = "Имотен лист бр.: ";
            label5.Text = imotenList.GetImotenListResult.ilistField;
            label5.Attributes.Add("style", "font-weight: bold;");
            gridView1.DataSource = (object) dataTable1;
            gridView1.Attributes.Add("style", "width: 70%");
            gridView1.CssClass = "GridViewStyle";
            gridView1.AlternatingRowStyle.CssClass = "AltRowStyle";
            gridView1.RowStyle.CssClass = "RowStyle";
            gridView1.HeaderStyle.CssClass = "HeaderStyle";
            gridView1.Attributes.Add("style", "width: 100%");
            gridView1.DataBind();
            LiteralControl literalControl1 = new LiteralControl("<h3>");
            new Label().Text = "Имотен лист";
            LiteralControl literalControl2 = new LiteralControl("</h3>");
            LiteralControl literalControl3 = new LiteralControl("<p>");
            LiteralControl literalControl4 = new LiteralControl("</p>");
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl3);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl4);
            LiteralControl literalControl5 = new LiteralControl("<br></br>");
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl5);
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl3);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label3);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label4);
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl4);
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl5);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label6);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label5);
            this.WSResponseControlPanelAKN.Controls.Add((Control) literalControl5);
            if (imotenList.GetImotenListResult.nizobjField != null)
            {
              if (imotenList.GetImotenListResult.nizobjField.Length != 0)
              {
                GridView gridView2 = new GridView();
                gridView2.ID = "GridViewObjekti";
                DataColumn column5 = new DataColumn("Бр. парцела");
                DataColumn column6 = new DataColumn("Објект");
                DataColumn column7 = new DataColumn("Намена");
                DataColumn column8 = new DataColumn("Површина во м2");
                DataColumn column9 = new DataColumn("Место");
                DataColumn column10 = new DataColumn("Влез");
                DataColumn column11 = new DataColumn("Кат");
                DataColumn column12 = new DataColumn("Стан");
                DataColumn column13 = new DataColumn("Год. на градба");
                DataColumn column14 = new DataColumn("Основ на градба");
                DataColumn column15 = new DataColumn("Право");
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(column5);
                dataTable2.Columns.Add(column6);
                dataTable2.Columns.Add(column7);
                dataTable2.Columns.Add(column8);
                dataTable2.Columns.Add(column9);
                dataTable2.Columns.Add(column10);
                dataTable2.Columns.Add(column11);
                dataTable2.Columns.Add(column12);
                dataTable2.Columns.Add(column13);
                dataTable2.Columns.Add(column14);
                dataTable2.Columns.Add(column15);
                foreach (objekti objekti in imotenList.GetImotenListResult.nizobjField)
                {
                  DataRow row2 = dataTable2.NewRow();
                  row2["Објект"] = (object) objekti.objektField;
                  row2["Намена"] = (object) objekti.namenaField;
                  row2["Површина во м2"] = (object) objekti.povrsinaField;
                  row2["Место"] = (object) objekti.mestoField;
                  row2["Бр. парцела"] = (object) objekti.brojField;
                  row2["Влез"] = (object) objekti.vlezField;
                  row2["Кат"] = (object) objekti.katField;
                  row2["Стан"] = (object) objekti.stanField;
                  row2["Право"] = (object) objekti.pravoField;
                  row2["Основ на градба"] = (object) objekti.osnovField;
                  row2["Год. на градба"] = (object) objekti.godinagradbaField;
                  dataTable2.Rows.Add(row2);
                }
                gridView2.DataSource = (object) dataTable2;
                gridView2.CssClass = "GridViewStyle";
                gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                gridView2.RowStyle.CssClass = "RowStyle";
                gridView2.HeaderStyle.CssClass = "HeaderStyle";
                gridView2.Attributes.Add("style", "width: 100%");
                gridView2.DataBind();
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label7 = new Label();
                label7.Text = "Објекти";
                list.Add(label7.Text);
                this.WSResponseControlPanelAKN.Controls.Add((Control) label7);
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelAKN.Controls.Add((Control) gridView2);
                this.Session["GridViewAKN1"] = (object) gridView2;
              }
              else
                list.Add(string.Empty);
            }
            else
              list.Add(string.Empty);
            if (imotenList.GetImotenListResult.nizparField != null)
            {
              if (imotenList.GetImotenListResult.nizparField.Length != 0)
              {
                GridView gridView2 = new GridView();
                gridView2.ID = "GridViewParceli";
                DataColumn column5 = new DataColumn("Бр. парцела");
                DataColumn column6 = new DataColumn("Објект");
                DataColumn column7 = new DataColumn("Место");
                DataColumn column8 = new DataColumn("Намена");
                DataColumn column9 = new DataColumn("Класа");
                DataColumn column10 = new DataColumn("Површина во м2");
                DataColumn column11 = new DataColumn("Право");
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(column5);
                dataTable2.Columns.Add(column6);
                dataTable2.Columns.Add(column7);
                dataTable2.Columns.Add(column8);
                dataTable2.Columns.Add(column9);
                dataTable2.Columns.Add(column10);
                dataTable2.Columns.Add(column11);
                foreach (parceli parceli in imotenList.GetImotenListResult.nizparField)
                {
                  DataRow row2 = dataTable2.NewRow();
                  row2["Бр. парцела"] = (object) parceli.broj_delField;
                  row2["Намена"] = (object) parceli.kulturaField;
                  row2["Место"] = (object) parceli.mestoField;
                  row2["Објект"] = (object) parceli.objektField.ToString();
                  row2["Површина во м2"] = (object) parceli.povrsinaField.ToString();
                  row2["Право"] = (object) parceli.pravoField;
                  dataTable2.Rows.Add(row2);
                }
                gridView2.DataSource = (object) dataTable2;
                gridView2.CssClass = "GridViewStyle";
                gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                gridView2.RowStyle.CssClass = "RowStyle";
                gridView2.HeaderStyle.CssClass = "HeaderStyle";
                gridView2.Attributes.Add("style", "width: 100%");
                gridView2.DataBind();
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label7 = new Label();
                label7.Text = "Парцели";
                list.Add(label7.Text);
                this.WSResponseControlPanelAKN.Controls.Add((Control) label7);
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelAKN.Controls.Add((Control) gridView2);
                this.Session["GridViewAKN2"] = (object) gridView2;
              }
              else
                list.Add(string.Empty);
            }
            else
              list.Add(string.Empty);
            if (imotenList.GetImotenListResult.nizsopField != null)
            {
              if (imotenList.GetImotenListResult.nizsopField.Length != 0)
              {
                GridView gridView2 = new GridView();
                gridView2.ID = "GridViewSopstvenici";
                DataColumn column5 = new DataColumn("Ред. број");
                DataColumn column6 = new DataColumn("Име и презиме");
                DataColumn column7 = new DataColumn("ЕМБГ/ЕМБС");
                DataColumn column8 = new DataColumn("Место");
                DataColumn column9 = new DataColumn("Улица");
                DataColumn column10 = new DataColumn("Број");
                DataColumn column11 = new DataColumn("Дел на посед");
                DataColumn column12 = new DataColumn("Правен основ на запишување");
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(column5);
                dataTable2.Columns.Add(column6);
                dataTable2.Columns.Add(column7);
                dataTable2.Columns.Add(column8);
                dataTable2.Columns.Add(column9);
                dataTable2.Columns.Add(column10);
                dataTable2.Columns.Add(column11);
                dataTable2.Columns.Add(column12);
                int num2 = 1;
                foreach (sopstvenici sopstvenici in imotenList.GetImotenListResult.nizsopField)
                {
                  DataRow row2 = dataTable2.NewRow();
                  row2["Ред. број"] = (object) num2;
                  row2["Број"] = (object) sopstvenici.brojField;
                  row2["Дел на посед"] = (object) sopstvenici.delField;
                  row2["Име и презиме"] = (object) sopstvenici.imeField;
                  row2["ЕМБГ/ЕМБС"] = (object) sopstvenici.embgField;
                  row2["Место"] = (object) sopstvenici.mestoField;
                  row2["Улица"] = (object) sopstvenici.ulicaField;
                  dataTable2.Rows.Add(row2);
                  ++num2;
                }
                gridView2.DataSource = (object) dataTable2;
                gridView2.CssClass = "GridViewStyle";
                gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                gridView2.RowStyle.CssClass = "RowStyle";
                gridView2.HeaderStyle.CssClass = "HeaderStyle";
                gridView2.Attributes.Add("style", "width: 100%");
                gridView2.ID = "GridViewObjekt";
                gridView2.DataBind();
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label7 = new Label();
                label7.Text = "Сопственици";
                list.Add(label7.Text);
                this.WSResponseControlPanelAKN.Controls.Add((Control) label7);
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelAKN.Controls.Add((Control) gridView2);
                this.Session["GridViewAKN3"] = (object) gridView2;
              }
              else
                list.Add(string.Empty);
            }
            else
              list.Add(string.Empty);
            if (imotenList.GetImotenListResult.niztovField != null)
            {
              if (imotenList.GetImotenListResult.niztovField.Length != 0)
              {
                GridView gridView2 = new GridView();
                gridView2.ID = "GridViewTovari";
                DataColumn column5 = new DataColumn("Ред. број");
                DataColumn column6 = new DataColumn("Товари");
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(column5);
                dataTable2.Columns.Add(column6);
                int num2 = 1;
                foreach (tovari tovari in imotenList.GetImotenListResult.niztovField)
                {
                  DataRow row2 = dataTable2.NewRow();
                  row2["Ред. број"] = (object) num2;
                  row2["Товари"] = (object) tovari.textField;
                  dataTable2.Rows.Add(row2);
                  ++num2;
                }
                gridView2.DataSource = (object) dataTable2;
                gridView2.CssClass = "GridViewStyle";
                gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                gridView2.RowStyle.CssClass = "RowStyle";
                gridView2.HeaderStyle.CssClass = "HeaderStyle";
                gridView2.Attributes.Add("style", "width: 100%");
                gridView2.ID = "GridViewTovari";
                gridView2.DataBind();
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label7 = new Label();
                label7.Text = "Товари";
                list.Add(label7.Text);
                this.WSResponseControlPanelAKN.Controls.Add((Control) label7);
                this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelAKN.Controls.Add((Control) gridView2);
                this.Session["GridViewAKN4"] = (object) gridView2;
              }
              else
                list.Add(string.Empty);
            }
            else
              list.Add(string.Empty);
            this.ImageButtonAKN.Visible = true;
            this.Session["ListaRezultHeaders"] = (object) list;
            this.Session["ParametersPrint"] = (object) ("Имотен Лист_" + ImotenListSchema.Grad + "_" + ImotenListSchema.Opstina + "_" + ImotenListSchema.ImotenList);
          }
          this.Completed = true;
          portReqrespClient.Close();
        }
        catch
        {
          if (num1 == 3)
          {
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelAKN.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKN.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKN.Controls.Add((Control) new LiteralControl("</h3>"));
            this.ImageButtonAKN.Visible = false;
          }
          this.SendMail("Neuspeshen povik na servisot: Имотен лист od institucija: " + user.CERTIFICATE.Subject + "korisnik: " + user.username);
        }
      }
    }

    private void ParceliData()
    {
      this.Completed = false;
      this.Session["GridViewList1"] = (object) null;
      this.Session["neuspesenAKNParcela"] = (object) "";
      interop.USER user = (interop.USER) this.Session["user"];
      WSHttpBinding wsHttpBinding = new WSHttpBinding();
      wsHttpBinding.Name = "myBinding";
      wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      wsHttpBinding.Security.Mode = SecurityMode.Message;
      wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
      EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://aknparceliwcfserviceapplication.interop.local/AKNParceliWCFServiceApplication/Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
      Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient reqrespPortClient = new Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
      reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserKatastar");
      ParceliSchema ParceliSchema = new ParceliSchema();
      ParceliSchema.Username = user.username;
      ParceliSchema.Password = user.password;
      ParceliSchema.Grad = ((TextBox) this.ControlParams.FindControl("txt0")).Text;
      ParceliSchema.Opstina = ((TextBox) this.ControlParams.FindControl("txt1")).Text;
      ParceliSchema.Broj = ((TextBox) this.ControlParams.FindControl("txt2")).Text;
      ParceliSchema.NacinNaIsprakjanje = "EORPD";
      ParceliSchema.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
      ParceliSchema.TimeStamp = DateTime.Now.ToString();
      int num1 = 1;
      while (num1 <= 3 && !this.Completed)
      {
        ++num1;
        try
        {
          reqrespPortClient.Open();
          GetPodatociStrukturaParcelaResponse strukturaParcelaResponse = reqrespPortClient.Operation_Request(ParceliSchema);
          List<string> list = new List<string>();
          if (strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.messageField.Equals("НЕУСПЕШНА ОПЕРАЦИЈА"))
          {
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕУСПЕШНА ОПЕРАЦИЈА!";
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKNParceli.Visible = false;
          }
          else if (strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.messageField.Equals("NE POSTOI BARANATA PARCELA "))
          {
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕ ПОСТОИ БАРАНАТА ПАРЦЕЛА!";
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKNParceli.Visible = false;
          }
          else if (strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.messageField.Equals("НЕ ПОСТОИ ПАРЦЕЛА"))
          {
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "НЕ ПОСТОИ БАРАНАТА ПАРЦЕЛА!";
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKNParceli.Visible = false;
          }
          else if (strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.messageField.Equals("Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!"))
          {
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
            this.Session["neuspesenAKN"] = (object) label2.Text;
            this.ImageButtonAKNParceli.Visible = false;
          }
          else
          {
            if (strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.nizparField != null && strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.nizparField.Length != 0)
            {
              Label label1 = new Label();
              label1.Text = "Сектор за катастар на недвижности: ";
              Label label2 = new Label();
              label2.Text = ((TextBox) this.ControlParams.FindControl("txt0")).Text.ToUpper();
              label2.Attributes.Add("style", "font-weight: bold;");
              Label label3 = new Label();
              label3.Text = "Катастарска општина: ";
              Label label4 = new Label();
              label4.Text = ((TextBox) this.ControlParams.FindControl("txt1")).Text.ToUpper();
              label4.Attributes.Add("style", "font-weight: bold;");
              Label label5 = new Label();
              Label label6 = new Label();
              label6.Text = "Парцела бр.: ";
              label5.Text = ((TextBox) this.ControlParams.FindControl("txt2")).Text.ToUpper();
              label5.Attributes.Add("style", "font-weight: bold;");
              LiteralControl literalControl1 = new LiteralControl("<p>");
              LiteralControl literalControl2 = new LiteralControl("</p>");
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl2);
              LiteralControl literalControl3 = new LiteralControl("<br></br>");
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl3);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label3);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label4);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl2);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl3);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label6);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label5);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) literalControl3);
              GridView gridView = new GridView();
              gridView.ID = "GridViewParceli";
              string[] strArray = new string[1]
              {
                "Реден број"
              };
              gridView.DataKeyNames = strArray;
              gridView.AutoGenerateColumns = false;
              gridView.PageIndexChanging += new GridViewPageEventHandler(this.GridViewParceli_PageIndexChanging);
              BoundField boundField1 = new BoundField();
              BoundField boundField2 = new BoundField();
              BoundField boundField3 = new BoundField();
              BoundField boundField4 = new BoundField();
              BoundField boundField5 = new BoundField();
              BoundField boundField6 = new BoundField();
              BoundField boundField7 = new BoundField();
              BoundField boundField8 = new BoundField();
              BoundField boundField9 = new BoundField();
              BoundField boundField10 = new BoundField();
              boundField1.DataField = "Реден број";
              boundField1.HeaderText = "Реден број";
              boundField2.DataField = "Број на парцела/дел";
              boundField2.HeaderText = "Број на парцела/дел";
              boundField3.DataField = "Имотен лист";
              boundField3.HeaderText = "Имотен лист";
              boundField4.DataField = "Катастарска општина";
              boundField4.HeaderText = "Катастарска општина";
              boundField5.DataField = "Намена";
              boundField5.HeaderText = "Намена";
              boundField6.DataField = "Место";
              boundField6.HeaderText = "Место";
              boundField7.DataField = "Објект";
              boundField7.HeaderText = "Објект";
              boundField8.DataField = "Општина";
              boundField8.HeaderText = "Општина";
              boundField9.DataField = "Површина во м2";
              boundField9.HeaderText = "Површина во м2";
              boundField10.DataField = "Право";
              boundField10.HeaderText = "Право";
              gridView.Columns.Add((DataControlField) boundField1);
              gridView.Columns.Add((DataControlField) boundField2);
              gridView.Columns.Add((DataControlField) boundField3);
              gridView.Columns.Add((DataControlField) boundField4);
              gridView.Columns.Add((DataControlField) boundField5);
              gridView.Columns.Add((DataControlField) boundField6);
              gridView.Columns.Add((DataControlField) boundField7);
              gridView.Columns.Add((DataControlField) boundField8);
              gridView.Columns.Add((DataControlField) boundField9);
              gridView.Columns.Add((DataControlField) boundField10);
              DataColumn column1 = new DataColumn("Реден број");
              DataColumn column2 = new DataColumn("Број на парцела/дел");
              DataColumn column3 = new DataColumn("Имотен лист");
              DataColumn column4 = new DataColumn("Катастарска општина");
              DataColumn column5 = new DataColumn("Намена");
              DataColumn column6 = new DataColumn("Место");
              DataColumn column7 = new DataColumn("Објект");
              DataColumn column8 = new DataColumn("Општина");
              DataColumn column9 = new DataColumn("Површина во м2");
              DataColumn column10 = new DataColumn("Право");
              DataTable dataTable = new DataTable();
              dataTable.Columns.Add(column1);
              dataTable.Columns.Add(column2);
              dataTable.Columns.Add(column3);
              dataTable.Columns.Add(column4);
              dataTable.Columns.Add(column5);
              dataTable.Columns.Add(column6);
              dataTable.Columns.Add(column7);
              dataTable.Columns.Add(column8);
              dataTable.Columns.Add(column9);
              dataTable.Columns.Add(column10);
              int num2 = 1;
              foreach (atributiparcela atributiparcela in strukturaParcelaResponse.GetPodatociStrukturaParcelaResult.nizparField)
              {
                DataRow row = dataTable.NewRow();
                row["Реден број"] = (object) num2.ToString();
                row["Број на парцела/дел"] = (object) atributiparcela.broj_delField;
                row["Имотен лист"] = (object) atributiparcela.ilistField;
                row["Катастарска општина"] = (object) atributiparcela.kopsField;
                row["Намена"] = (object) atributiparcela.kulturaField;
                row["Место"] = (object) atributiparcela.mestoField;
                row["Објект"] = (object) atributiparcela.objektField;
                row["Општина"] = (object) atributiparcela.opsField;
                row["Површина во м2"] = (object) atributiparcela.povrsinaField;
                row["Право"] = (object) atributiparcela.pravoField;
                dataTable.Rows.Add(row);
                ++num2;
              }
              gridView.DataSource = (object) dataTable;
              gridView.CssClass = "GridViewStyle";
              gridView.AlternatingRowStyle.CssClass = "AltRowStyle";
              gridView.RowStyle.CssClass = "RowStyle";
              gridView.HeaderStyle.CssClass = "HeaderStyle";
              gridView.Attributes.Add("style", "width: 100%");
              gridView.DataBind();
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label7 = new Label();
              label7.Text = "Податоци за парцела";
              list.Add(label7.Text);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label7);
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
              this.WSResponseControlPanelAKNParceli.Controls.Add((Control) gridView);
              this.Session["GridViewAKNParceli"] = (object) gridView;
            }
            this.ImageButtonAKNParceli.Visible = true;
            this.Session["ParametersPrint"] = (object) ("Парцела_" + ParceliSchema.Grad + "_" + ParceliSchema.Opstina + "_" + ParceliSchema.Broj);
            this.Session["ListaRezultHeaders"] = (object) list;
          }
          this.Completed = true;
          reqrespPortClient.Close();
        }
        catch
        {
          if (num1 == 3)
          {
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("<h3>"));
            Label label1 = new Label();
            label1.ID = "lblporaka";
            label1.Text = "Порака: ";
            Label label2 = new Label();
            label2.ID = "lblporakavalue";
            label2.Text = "Сервисот на АКН во моментов е недостапен. Ве молиме обидете се подоцна!";
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label1);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) label2);
            this.WSResponseControlPanelAKNParceli.Controls.Add((Control) new LiteralControl("</h3>"));
            this.ImageButtonAKNParceli.Visible = false;
          }
          this.SendMail("Neuspeshen povik na servisot: Податоци за парцела od institucija: " + user.CERTIFICATE.Subject + "korisnik: " + user.username);
        }
      }
    }

    protected void GridViewParceli_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridView gridView = (GridView) this.WSResponseControlPanelAKNParceli.FindControl("GridViewParceli");
      gridView.PageIndex = e.NewPageIndex;
      gridView.DataBind();
      gridView.SelectedRowStyle.Reset();
      gridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      gridView.RowStyle.CssClass = "RowStyle";
    }

    private void GetEMBSforAKN()
    {
      this.Completed = false;
      string s = this.Session["IsMBS"] == null || !Convert.ToBoolean(this.Session["IsMBS"]) ? ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim() : this.Session["MBS"].ToString();
      double result;
      if (double.TryParse(s, out result) && result.ToString().Length == 7)
      {
        this.Session["GridViewList1"] = (object) null;
        this.Session["GridViewList2"] = (object) null;
        this.Session["GridViewList3"] = (object) null;
        this.Session["GridViewList4"] = (object) null;
        this.Session["GridViewList5"] = (object) null;
        this.Session["GridViewList6"] = (object) null;
        this.Session["GridViewList7"] = (object) null;
        this.Session["GridViewList8"] = (object) null;
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://crbiztalkwcfserviceapplicationakn.interop.local/CRBizTalkWCFServiceApplicationAKN/Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORTClient reqrespPortClient = new Module_1_CRBizTalkOrchestrationAKN_RECEIVE_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "UserKatastar");
        interop.WebServiceCR_AKN.EMBS EMBS = new interop.WebServiceCR_AKN.EMBS();
        EMBS.Username = user.username;
        EMBS.Password = user.password;
        EMBS.EMBS1 = s;
        EMBS.NacinNaIsprakjanje = "EORPD";
        EMBS.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
        EMBS.TimeStamp = DateTime.Now.ToString();
        WebApplicationInterop.InteropDAL interopDal = new WebApplicationInterop.InteropDAL();
        int num = 1;
        while (num <= 3 && !this.Completed)
        {
          ++num;
          try
          {
            reqrespPortClient.Open();
            VratiCRMRezultatiAKNResponse rezultatiAknResponse = reqrespPortClient.Operation_Request(EMBS);
            List<string> list1 = new List<string>();
            if (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0] != null && (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0] != null) && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0].entranceNoField == "Не е пронајден запис за барањето!")
            {
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Не е пронајден запис за барањето!";
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_AKN.Visible = false;
            }
            else if (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0] != null && (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0] != null) && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0].entranceNoField == "Невалиден сертификат на серверот за комуникација со ЦР!")
            {
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Невалиден сертификат на серверот за комуникација со ЦР!";
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_AKN.Visible = false;
            }
            else if (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0] != null && (rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField != null && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0] != null) && rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].crmResultItemField[0].entranceNoField == "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!")
            {
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_AKN.Visible = false;
            }
            else
            {
              Label label1 = new Label();
              label1.Text = "Матичен број на правно лице: ";
              Label label2 = new Label();
              label2.Text = s;
              label2.Attributes.Add("style", "font-weight: bold;");
              LiteralControl literalControl1 = new LiteralControl("<p>");
              LiteralControl literalControl2 = new LiteralControl("</p>");
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) literalControl2);
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<br></br>"));
              this.WSResponseControlPanelCR_AKN.Controls.Add((Control) literalControl1);
              GridView gridView1 = new GridView();
              GridView gridView2 = new GridView();
              GridView gridView3 = new GridView();
              GridView gridView4 = new GridView();
              GridView gridView5 = new GridView();
              GridView gridView6 = new GridView();
              GridView gridView7 = new GridView();
              GridView gridView8 = new GridView();
              DataTable dataTable1 = (DataTable) null;
              DataTable dataTable2 = (DataTable) null;
              DataTable dataTable3 = (DataTable) null;
              DataTable dataTable4 = (DataTable) null;
              DataTable dataTable5 = (DataTable) null;
              DataTable dataTable6 = (DataTable) null;
              DataTable dataTable7 = (DataTable) null;
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems1 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[1];
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems2 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[2];
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems3 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[3];
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems4 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[4];
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems5 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[5];
              interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems6 = rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[6];
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[1] = responseCrmResultItems6;
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[2] = responseCrmResultItems3;
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[3] = responseCrmResultItems2;
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[4] = responseCrmResultItems1;
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[5] = responseCrmResultItems4;
              rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[6] = responseCrmResultItems5;
              List<Akteri> list2 = new List<Akteri>();
              foreach (interop.WebServiceCR_AKN.CrmResponseCrmResultItems responseCrmResultItems7 in rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField)
              {
                string templateNameField = responseCrmResultItems7.templateNameField;
                foreach (interop.WebServiceCR_AKN.CrmResponseCrmResultItemsCrmResultItem itemsCrmResultItem in responseCrmResultItems7.crmResultItemField)
                {
                  interop.WebServiceCR_AKN.CrmResponseCrmResultItemsCrmResultItem objekt = itemsCrmResultItem;
                  if (objekt.lEIDField != null)
                  {
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[0].templateNameField && (objekt.isLETerminatedField != null || objekt.lEIDField != null || (objekt.lEFullNameField != null || objekt.shortNameField != null) || (objekt.terminationTypeIDField != null || objekt.terminationTypeDescField != null || (objekt.lETypeIDField != null || objekt.lETypeDescField != null)) || (objekt.lESizeIDField != null || objekt.lESizeDescField != null || (objekt.municipalityField != null || objekt.municipalityCodeField != null) || (objekt.placeField != null || objekt.placeCodeField != null || (objekt.streetField != null || objekt.streetCodeField != null))) || (objekt.houseNoField != null || objekt.entranceNoField != null || (objekt.flatNoField != null || objekt.organisationTypeCodeField != null) || (objekt.organisationTypeDescField != null || objekt.registerCategoryIDField != null || (objekt.registerCategoryField != null || objekt.authorisedRegisterIDField != null)) || (objekt.authorisedRegisterField != null || objekt.ownershipTypeIDField != null || (objekt.ownershipTypeDescField != null || objekt.isForeignActField != null) || (objekt.isActivityNoLicenceField != null || objekt.coreActivityCodeField != null || (objekt.coreActivityDescField != null || objekt.emailField != null)))) || (objekt.foreignActivityField != null || objekt.additionalInfoField != null || (objekt.actingPeriodField != null || objekt.isDataConfirmedField != null)) || objekt.isAAActiveField != null))
                    {
                      if (dataTable1 == null)
                      {
                        gridView1.CssClass = "GridViewStyle";
                        gridView1.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView1.RowStyle.CssClass = "RowStyle";
                        gridView1.HeaderStyle.CssClass = "HeaderStyle";
                        gridView1.ControlStyle.Font.Bold = true;
                        gridView1.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView1.ID = "GridViewListTemplate1";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable1 = new DataTable();
                        dataTable1.Columns.Add(column1);
                        dataTable1.Columns.Add(column2);
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Основ на бришење";
                        row5[1] = (object) objekt.terminationBasisField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Датум на бришење во ЦРРМ";
                        row6[1] = (object) objekt.dateDeletedInCRField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Кратко име";
                        row7[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Причина за престанок";
                        row8[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на престанокот";
                        row9[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Вид на субјект";
                        row10[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на вид на субјект";
                        row11[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Големина на субјект";
                        row12[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Опис на големина на субјект";
                        row13[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Општина";
                        row14[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Место";
                        row15[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Улица";
                        row16[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Број на куќа";
                        row17[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Број на влез";
                        row18[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Број на стан";
                        row19[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Организационен облик";
                        row20[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Регистар";
                        row21[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Надлежен регистар";
                        row22[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Вид на сопственост";
                        row23[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Евидентирани дејности во надворешен промет";
                        row24[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Општа клаузула за бизнис";
                        row25[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Опис на претежна дејност";
                        row26[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Е-пошта";
                        row27[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Други дејности";
                        row28[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Дополнителни инфо.";
                        row29[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Времетраење";
                        row30[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row30);
                        DataRow row31 = dataTable1.NewRow();
                        row31[0] = (object) "Потврдени податоци";
                        row31[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row31);
                        DataRow row32 = dataTable1.NewRow();
                        row32[0] = (object) "Активност од регистар на г.с.";
                        row32[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row32[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row32[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row32[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row32);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за субјетот"
                        });
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                      else if (dataTable1 != null)
                      {
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Основ на бришење";
                        row5[1] = (object) objekt.terminationBasisField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Датум на бришење во ЦРРМ";
                        row6[1] = (object) objekt.dateDeletedInCRField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Кратко име";
                        row7[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Причина за престанок";
                        row8[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на престанокот";
                        row9[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Вид на субјект";
                        row10[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на вид на субјект";
                        row11[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Големина на субјект";
                        row12[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Опис на големина на субјект";
                        row13[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Општина";
                        row14[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Место";
                        row15[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Улица";
                        row16[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Број на куќа";
                        row17[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Број на влез";
                        row18[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Број на стан";
                        row19[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Организационен облик";
                        row20[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Регистар";
                        row21[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Надлежен регистар";
                        row22[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Опис на вид на сопственост";
                        row23[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Евидентирани дејности во надворешен промет";
                        row24[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Општа клаузула за бизнис";
                        row25[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Опис на претежна дејност";
                        row26[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Е-пошта";
                        row27[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Други дејности";
                        row28[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Дополнителни инфо.";
                        row29[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Времетраење";
                        row30[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row30);
                        DataRow row31 = dataTable1.NewRow();
                        row31[0] = (object) "Потврдени податоци";
                        row31[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row31);
                        DataRow row32 = dataTable1.NewRow();
                        row32[0] = (object) "Активност од регистар на г.с.";
                        row32[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row32[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row32[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row32[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row32);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                    }
                    List<GridView> list3;
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[1].templateNameField && (objekt.lEIDField != null || objekt.capitalOriginIDField != null || (objekt.capitalOriginDescField != null || objekt.fCCodeField != null) || (objekt.capitalFC_CashField != null || objekt.capitalFC_NonCashField != null || (objekt.capitalFC_PaydField != null || objekt.sharesTotalField != null)) || (objekt.sharesPaydField != null || objekt.typeOfSharesField != null || (objekt.sharesPaymentField != null || objekt.sharesPublishingField != null) || objekt.licenceField != null) || objekt.capitalFC_TotalField != null))
                    {
                      if (dataTable7 == null)
                      {
                        dataTable7 = new DataTable();
                        gridView7.CssClass = "GridViewStyle";
                        gridView7.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView7.RowStyle.CssClass = "RowStyle";
                        gridView7.HeaderStyle.CssClass = "HeaderStyle";
                        gridView7.ControlStyle.Font.Bold = true;
                        gridView7.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView7.ID = "GridViewListTemplate7";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable7.Columns.Add(column1);
                        dataTable7.Columns.Add(column2);
                        DataRow row1 = dataTable7.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable7.Rows.Add(row1);
                        DataRow row2 = dataTable7.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable7.Rows.Add(row2);
                        DataRow row3 = dataTable7.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable7.Rows.Add(row3);
                        DataRow row4 = dataTable7.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable7.Rows.Add(row4);
                        DataRow row5 = dataTable7.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable7.Rows.Add(row5);
                        DataRow row6 = dataTable7.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable7.Rows.Add(row6);
                        DataRow row7 = dataTable7.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable7.Rows.Add(row7);
                        DataRow row8 = dataTable7.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable7.Rows.Add(row8);
                        DataRow row9 = dataTable7.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable7.Rows.Add(row9);
                        DataRow row10 = dataTable7.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable7.Rows.Add(row10);
                        DataRow row11 = dataTable7.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable7.Rows.Add(row11);
                        DataRow row12 = dataTable7.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable7.Rows.Add(row12);
                        DataRow row13 = dataTable7.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable7.Rows.Add(row13);
                        DataRow row14 = dataTable7.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable7.Rows.Add(row14);
                        DataRow row15 = dataTable7.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable7.Rows.Add(row15);
                        gridView7.DataSource = (object) dataTable7;
                        gridView7.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за основање"
                        });
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView7);
                        list3 = (List<GridView>) this.Session["GridViewList7"];
                        this.Session["GridViewList7"] = (object) new List<GridView>()
                        {
                          gridView7
                        };
                      }
                      else if (dataTable7 != null)
                      {
                        DataTable dataTable8 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable8.Rows.Add(row9);
                        DataRow row10 = dataTable8.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable8.Rows.Add(row10);
                        DataRow row11 = dataTable8.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable8.Rows.Add(row11);
                        DataRow row12 = dataTable8.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable8.Rows.Add(row12);
                        DataRow row13 = dataTable8.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable8.Rows.Add(row13);
                        DataRow row14 = dataTable8.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable8.Rows.Add(row14);
                        DataRow row15 = dataTable8.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable8.Rows.Add(row15);
                        gridView9.DataSource = (object) dataTable8;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList7"];
                        list4.Add(gridView9);
                        this.Session["GridViewList7"] = (object) list4;
                      }
                    }
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[2].templateNameField && (objekt.lEIDField != null || objekt.personOrLEIDField != null || (objekt.personOrLEDescField != null || objekt.ownerTypeIDField != null) || (objekt.ownerTypeDescField != null || objekt.liabilityIDField != null || (objekt.liabilityDescField != null || objekt.ownerNameField != null)) || (objekt.ownerSurnameField != null || objekt.countryCodeField != null || (objekt.municipalityField != null || objekt.placeField != null) || (objekt.streetField != null || objekt.houseNoField != null || (objekt.entranceNoField != null || objekt.flatNoField != null))) || (objekt.emailField != null || objekt.fCCodeField != null || (objekt.participationFC_CashField != null || objekt.participationFC_NonCashField != null) || (objekt.participationFC_PaydField != null || objekt.participationFC_TotalField != null)) || objekt.addInfo != null))
                    {
                      if (dataTable4 == null)
                      {
                        dataTable4 = new DataTable();
                        gridView4.CssClass = "GridViewStyle";
                        gridView4.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView4.RowStyle.CssClass = "RowStyle";
                        gridView4.HeaderStyle.CssClass = "HeaderStyle";
                        gridView4.ControlStyle.Font.Bold = true;
                        gridView4.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView4.ID = "GridViewListTemplate4";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable4.Columns.Add(column1);
                        dataTable4.Columns.Add(column2);
                        DataRow row1 = dataTable4.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable4.Rows.Add(row1);
                        DataRow row2 = dataTable4.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable4.Rows.Add(row2);
                        DataRow row3 = dataTable4.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable4.Rows.Add(row3);
                        DataRow row4 = dataTable4.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable4.Rows.Add(row4);
                        DataRow row5 = dataTable4.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable4.Rows.Add(row5);
                        DataRow row6 = dataTable4.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable4.Rows.Add(row6);
                        DataRow row7 = dataTable4.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable4.Rows.Add(row7);
                        DataRow row8 = dataTable4.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable4.Rows.Add(row8);
                        DataRow row9 = dataTable4.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable4.Rows.Add(row9);
                        DataRow row10 = dataTable4.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable4.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable4.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable4.Rows.Add(row12);
                        DataRow row13 = dataTable4.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable4.Rows.Add(row13);
                        DataRow row14 = dataTable4.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable4.Rows.Add(row14);
                        DataRow row15 = dataTable4.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable4.Rows.Add(row15);
                        DataRow row16 = dataTable4.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable4.Rows.Add(row16);
                        DataRow row17 = dataTable4.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable4.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable4.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable4.Rows.Add(row19);
                        DataRow row20 = dataTable4.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable4.Rows.Add(row20);
                        DataRow row21 = dataTable4.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable4.Rows.Add(row21);
                        DataRow row22 = dataTable4.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable4.Rows.Add(row22);
                        DataRow row23 = dataTable4.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable4.Rows.Add(row23);
                        DataRow row24 = dataTable4.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable4.Rows.Add(row24);
                        gridView4.DataSource = (object) dataTable4;
                        gridView4.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за сопственици"
                        });
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView4);
                        list3 = (List<GridView>) this.Session["GridViewList4"];
                        this.Session["GridViewList4"] = (object) new List<GridView>()
                        {
                          gridView4
                        };
                      }
                      else if (dataTable4 != null)
                      {
                        DataTable dataTable8 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable8.Rows.Add(row9);
                        DataRow row10 = dataTable8.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable8.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable8.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable8.Rows.Add(row12);
                        DataRow row13 = dataTable8.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable8.Rows.Add(row13);
                        DataRow row14 = dataTable8.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable8.Rows.Add(row14);
                        DataRow row15 = dataTable8.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable8.Rows.Add(row15);
                        DataRow row16 = dataTable8.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable8.Rows.Add(row16);
                        DataRow row17 = dataTable8.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable8.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable8.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable8.Rows.Add(row19);
                        DataRow row20 = dataTable8.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable8.Rows.Add(row20);
                        DataRow row21 = dataTable8.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable8.Rows.Add(row21);
                        DataRow row22 = dataTable8.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable8.Rows.Add(row22);
                        DataRow row23 = dataTable8.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable8.Rows.Add(row23);
                        DataRow row24 = dataTable8.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable8.Rows.Add(row24);
                        gridView9.DataSource = (object) dataTable8;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList4"];
                        list4.Add(gridView9);
                        this.Session["GridViewList4"] = (object) list4;
                      }
                    }
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[3].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.personOrLEIDField != null || objekt.personOrLEDescField != null) || (objekt.actorTypeIDField != null || objekt.actorTypeDescField != null || (objekt.actorNameField != null || objekt.actorSurnameField != null)) || (objekt.countryCodeField != null || objekt.municipalityField != null || (objekt.placeField != null || objekt.streetField != null) || (objekt.houseNoField != null || objekt.flatNoField != null || (objekt.emailField != null || objekt.descriptionField != null))) || (objekt.restrictionsField != null || objekt.authorisationTypeIDField != null) || objekt.authorisationTypeDescField != null))
                    {
                      if (dataTable3 == null)
                      {
                        dataTable3 = new DataTable();
                        gridView3.CssClass = "GridViewStyle";
                        gridView3.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView3.RowStyle.CssClass = "RowStyle";
                        gridView3.HeaderStyle.CssClass = "HeaderStyle";
                        gridView3.ControlStyle.Font.Bold = true;
                        gridView3.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView3.ID = "GridViewListTemplate3";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable3.Columns.Add(column1);
                        dataTable3.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable3.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable3.Rows.Add(row1);
                        DataRow row2 = dataTable3.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable3.Rows.Add(row2);
                        DataRow row3 = dataTable3.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable3.Rows.Add(row3);
                        DataRow row4 = dataTable3.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable3.Rows.Add(row4);
                        DataRow row5 = dataTable3.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable3.Rows.Add(row5);
                        DataRow row6 = dataTable3.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable3.Rows.Add(row6);
                        DataRow row7 = dataTable3.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable3.Rows.Add(row7);
                        DataRow row8 = dataTable3.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable3.Rows.Add(row8);
                        DataRow row9 = dataTable3.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable3.Rows.Add(row9);
                        DataRow row10 = dataTable3.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable3.Rows.Add(row10);
                        DataRow row11 = dataTable3.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable3.Rows.Add(row11);
                        DataRow row12 = dataTable3.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable3.Rows.Add(row12);
                        DataRow row13 = dataTable3.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable3.Rows.Add(row13);
                        DataRow row14 = dataTable3.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable3.Rows.Add(row14);
                        DataRow row15 = dataTable3.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable3.Rows.Add(row15);
                        DataRow row16 = dataTable3.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable3.Rows.Add(row16);
                        DataRow row17 = dataTable3.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable3.Rows.Add(row17);
                        DataRow row18 = dataTable3.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable3.Rows.Add(row18);
                        DataRow row19 = dataTable3.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable3.Rows.Add(row19);
                        DataRow row20 = dataTable3.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable3.Rows.Add(row20);
                        DataRow row21 = dataTable3.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable3.Rows.Add(row21);
                        list2.Add(akteri);
                        if (!(objekt.unitNoField == "0"))
                          dataTable3 = (DataTable) null;
                      }
                      else if (dataTable3 != null)
                      {
                        DataTable dataTable8 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable8.Rows.Add(row9);
                        DataRow row10 = dataTable8.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable8.Rows.Add(row10);
                        DataRow row11 = dataTable8.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable8.Rows.Add(row11);
                        DataRow row12 = dataTable8.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable8.Rows.Add(row12);
                        DataRow row13 = dataTable8.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable8.Rows.Add(row13);
                        DataRow row14 = dataTable8.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable8.Rows.Add(row14);
                        DataRow row15 = dataTable8.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable8.Rows.Add(row15);
                        DataRow row16 = dataTable8.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable8.Rows.Add(row16);
                        DataRow row17 = dataTable8.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable8.Rows.Add(row17);
                        DataRow row18 = dataTable8.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable8.Rows.Add(row18);
                        DataRow row19 = dataTable8.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable8.Rows.Add(row19);
                        DataRow row20 = dataTable8.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable8.Rows.Add(row20);
                        DataRow row21 = dataTable8.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable8.Rows.Add(row21);
                        list2.Add(akteri);
                        if (!(objekt.unitNoField == "0"))
                          ;
                      }
                    }
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[4].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.unitNameField != null || objekt.unitTypeIDField != null) || (objekt.unitTypeDescrField != null || objekt.unitDescrField != null || (objekt.otherInfoField != null || objekt.countryCodeField != null)) || (objekt.municipalityField != null || objekt.municipalityCodeField != null || (objekt.placeField != null || objekt.placeCodeField != null) || (objekt.streetField != null || objekt.streetCodeField != null || (objekt.houseNoField != null || objekt.entranceNoField != null))) || (objekt.flatNoField != null || objekt.activityCodeField != null) || objekt.activityDescField != null))
                    {
                      if (dataTable2 == null)
                      {
                        gridView2.CssClass = "GridViewStyle";
                        gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView2.RowStyle.CssClass = "RowStyle";
                        gridView2.HeaderStyle.CssClass = "HeaderStyle";
                        gridView2.ControlStyle.Font.Bold = true;
                        gridView2.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView2.ID = "GridViewListTemplate2";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable2 = new DataTable();
                        dataTable2.Columns.Add(column1);
                        dataTable2.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable2.NewRow();
                        row1[0] = (object) "Број на подружница";
                        row1[1] = (object) objekt.unitNoField;
                        dataTable2.Rows.Add(row1);
                        DataRow row2 = dataTable2.NewRow();
                        row2[0] = (object) "Назив на подружница";
                        row2[1] = (object) objekt.unitNameField;
                        dataTable2.Rows.Add(row2);
                        DataRow row3 = dataTable2.NewRow();
                        row3[0] = (object) "Тип на подружница";
                        row3[1] = (object) objekt.unitTypeDescrField;
                        dataTable2.Rows.Add(row3);
                        DataRow row4 = dataTable2.NewRow();
                        row4[0] = (object) "Опис на подружница";
                        row4[1] = (object) objekt.unitDescrField;
                        dataTable2.Rows.Add(row4);
                        DataRow row5 = dataTable2.NewRow();
                        row5[0] = (object) "Останати информации";
                        row5[1] = (object) objekt.otherInfoField;
                        dataTable2.Rows.Add(row5);
                        DataRow row6 = dataTable2.NewRow();
                        row6[0] = (object) "Код на земја";
                        row6[1] = (object) objekt.countryCodeField;
                        dataTable2.Rows.Add(row6);
                        DataRow row7 = dataTable2.NewRow();
                        row7[0] = (object) "Адреса";
                        row7[1] = (object) (objekt.streetField + " " + objekt.houseNoField + " " + objekt.entranceNoField + " " + objekt.flatNoField + " " + objekt.municipalityField + " " + objekt.placeField);
                        dataTable2.Rows.Add(row7);
                        DataRow row8 = dataTable2.NewRow();
                        row8[0] = (object) "Код на претежна дејност";
                        row8[1] = (object) objekt.activityCodeField;
                        dataTable2.Rows.Add(row8);
                        DataRow row9 = dataTable2.NewRow();
                        row9[0] = (object) "Опис на претежна дејност";
                        row9[1] = (object) objekt.activityDescField;
                        dataTable2.Rows.Add(row9);
                        if (akteri != null)
                        {
                          DataRow row10 = dataTable2.NewRow();
                          row10[0] = (object) "Овластени лица на подружницата";
                          row10[1] = (object) "";
                          dataTable2.Rows.Add(row10);
                          DataRow row11 = dataTable2.NewRow();
                          row11[0] = (object) "Број на подружница";
                          row11[1] = (object) akteri.zapisPodroznica;
                          dataTable2.Rows.Add(row11);
                          DataRow row12 = dataTable2.NewRow();
                          row12[0] = (object) "Тип на овластено лице";
                          row12[1] = (object) akteri.zapis4;
                          dataTable2.Rows.Add(row12);
                          DataRow row13 = dataTable2.NewRow();
                          row13[0] = (object) "Матичен број на актер";
                          row13[1] = (object) akteri.zapis5;
                          dataTable2.Rows.Add(row13);
                          DataRow row14 = dataTable2.NewRow();
                          row14[0] = (object) "Тип на актер";
                          row14[1] = (object) akteri.zapis7;
                          dataTable2.Rows.Add(row14);
                          DataRow row15 = dataTable2.NewRow();
                          row15[0] = (object) "Име";
                          row15[1] = (object) akteri.zapis8;
                          dataTable2.Rows.Add(row15);
                          DataRow row16 = dataTable2.NewRow();
                          row16[0] = (object) "Презиме";
                          row16[1] = (object) akteri.zapis9;
                          dataTable2.Rows.Add(row16);
                          DataRow row17 = dataTable2.NewRow();
                          row17[0] = (object) "Код на земја";
                          row17[1] = (object) akteri.zapis10;
                          dataTable2.Rows.Add(row17);
                          DataRow row18 = dataTable2.NewRow();
                          row18[0] = (object) "Адреса";
                          row18[1] = (object) (akteri.zapis13 + " " + akteri.zapis14 + " " + akteri.zapis15 + " " + akteri.zapis16 + " " + akteri.zapis11 + " " + akteri.zapis12);
                          dataTable2.Rows.Add(row18);
                          DataRow row19 = dataTable2.NewRow();
                          row19[0] = (object) "Електронско сандаче";
                          row19[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row19);
                          DataRow row20 = dataTable2.NewRow();
                          row20[0] = (object) "Овластувања";
                          row20[1] = (object) akteri.zapis18;
                          dataTable2.Rows.Add(row20);
                          DataRow row21 = dataTable2.NewRow();
                          row21[0] = (object) "Ограничувања";
                          row21[1] = (object) akteri.zapis19;
                          dataTable2.Rows.Add(row21);
                          DataRow row22 = dataTable2.NewRow();
                          row22[0] = (object) "Тип на овластување";
                          row22[1] = (object) akteri.zapis21;
                          dataTable2.Rows.Add(row22);
                        }
                        gridView2.DataSource = (object) dataTable2;
                        gridView2.DataBind();
                        list3 = (List<GridView>) this.Session["GridViewList2"];
                        this.Session["GridViewList2"] = (object) new List<GridView>()
                        {
                          gridView2
                        };
                      }
                      else if (dataTable2 != null)
                      {
                        DataTable dataTable8 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Број на подружница";
                        row1[1] = (object) objekt.unitNoField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Назив на подружница";
                        row2[1] = (object) objekt.unitNameField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Тип на подружница";
                        row3[1] = (object) objekt.unitTypeDescrField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Опис на подружница";
                        row4[1] = (object) objekt.unitDescrField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Останати информации";
                        row5[1] = (object) objekt.otherInfoField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Код на земја";
                        row6[1] = (object) objekt.countryCodeField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Адреса";
                        row7[1] = (object) (objekt.streetField + " " + objekt.houseNoField + " " + objekt.entranceNoField + " " + objekt.flatNoField + " " + objekt.municipalityField + " " + objekt.placeField);
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Код на претежна дејност";
                        row8[1] = (object) objekt.activityCodeField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Опис на претежна дејност";
                        row9[1] = (object) objekt.activityDescField;
                        dataTable8.Rows.Add(row9);
                        if (akteri != null)
                        {
                          DataRow row10 = dataTable8.NewRow();
                          row10[0] = (object) "Овластени лица на подружницата";
                          row10[1] = (object) "";
                          dataTable8.Rows.Add(row10);
                          DataRow row11 = dataTable8.NewRow();
                          row11[0] = (object) "Број на подружница";
                          row11[1] = (object) akteri.zapisPodroznica;
                          dataTable8.Rows.Add(row11);
                          DataRow row12 = dataTable8.NewRow();
                          row12[0] = (object) "Тип на овластено лице";
                          row12[1] = (object) akteri.zapis4;
                          dataTable8.Rows.Add(row12);
                          DataRow row13 = dataTable8.NewRow();
                          row13[0] = (object) "Матичен број на актер";
                          row13[1] = (object) akteri.zapis5;
                          dataTable8.Rows.Add(row13);
                          DataRow row14 = dataTable8.NewRow();
                          row14[0] = (object) "Тип на актер";
                          row14[1] = (object) akteri.zapis7;
                          dataTable8.Rows.Add(row14);
                          DataRow row15 = dataTable8.NewRow();
                          row15[0] = (object) "Име";
                          row15[1] = (object) akteri.zapis8;
                          dataTable8.Rows.Add(row15);
                          DataRow row16 = dataTable8.NewRow();
                          row16[0] = (object) "Презиме";
                          row16[1] = (object) akteri.zapis9;
                          dataTable8.Rows.Add(row16);
                          DataRow row17 = dataTable8.NewRow();
                          row17[0] = (object) "Код на земја";
                          row17[1] = (object) akteri.zapis10;
                          dataTable8.Rows.Add(row17);
                          DataRow row18 = dataTable8.NewRow();
                          row18[0] = (object) "Адреса";
                          row18[1] = (object) (akteri.zapis13 + " " + akteri.zapis14 + " " + akteri.zapis15 + " " + akteri.zapis16 + " " + akteri.zapis11 + " " + akteri.zapis12);
                          dataTable8.Rows.Add(row18);
                          DataRow row19 = dataTable2.NewRow();
                          row19[0] = (object) "Електронско сандаче";
                          row19[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row19);
                          DataRow row20 = dataTable8.NewRow();
                          row20[0] = (object) "Овластувања";
                          row20[1] = (object) akteri.zapis18;
                          dataTable8.Rows.Add(row20);
                          DataRow row21 = dataTable8.NewRow();
                          row21[0] = (object) "Ограничувања";
                          row21[1] = (object) akteri.zapis19;
                          dataTable8.Rows.Add(row21);
                          DataRow row22 = dataTable8.NewRow();
                          row22[0] = (object) "Тип на овластување";
                          row22[1] = (object) akteri.zapis21;
                          dataTable8.Rows.Add(row22);
                        }
                        gridView9.DataSource = (object) dataTable8;
                        gridView9.DataBind();
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList2"];
                        list4.Add(gridView9);
                        this.Session["GridViewList2"] = (object) list4;
                      }
                    }
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[5].templateNameField && (objekt.lEIDField != null || objekt.activityDescField != null))
                    {
                      if (dataTable5 == null)
                      {
                        dataTable5 = new DataTable();
                        gridView5.CssClass = "GridViewStyle";
                        gridView5.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView5.RowStyle.CssClass = "RowStyle";
                        gridView5.ControlStyle.Font.Bold = true;
                        gridView5.HeaderStyle.CssClass = "HeaderStyle";
                        gridView5.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView5.ID = "GridViewListTemplate5";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Код на дејност");
                        DataColumn column3 = new DataColumn("Опис на дејност");
                        dataTable5.Columns.Add(column1);
                        dataTable5.Columns.Add(column2);
                        dataTable5.Columns.Add(column3);
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                      else if (dataTable5 != null)
                      {
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                    }
                    if (templateNameField == rezultatiAknResponse.VratiCRMRezultatiAKNResult.itemsField[6].templateNameField && objekt.lEIDField != null && (objekt.memberOfField != null || objekt.membersField != null))
                    {
                      if (dataTable6 == null)
                      {
                        dataTable6 = new DataTable();
                        gridView6.CssClass = "GridViewStyle";
                        gridView6.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView6.RowStyle.CssClass = "RowStyle";
                        gridView6.HeaderStyle.CssClass = "HeaderStyle";
                        gridView6.ControlStyle.Font.Bold = true;
                        gridView6.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView6.ID = "GridViewListTemplate6";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Матичен број на правно лице членка на синдикат");
                        DataColumn column3 = new DataColumn("Матичен број на правно лице кое пристапува како членка на синдикат");
                        dataTable6.Columns.Add(column1);
                        dataTable6.Columns.Add(column2);
                        dataTable6.Columns.Add(column3);
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                      else if (dataTable6 != null)
                      {
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                    }
                  }
                }
              }
              if (list2.Count > 0)
              {
                List<Akteri> list3 = new List<Akteri>();
                list3.AddRange((IEnumerable<Akteri>) Enumerable.ToList<Akteri>(Enumerable.Where<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == "0"))));
                List<Akteri> list4 = Enumerable.ToList<Akteri>((IEnumerable<Akteri>) Enumerable.OrderBy<Akteri, string>((IEnumerable<Akteri>) list3, (Func<Akteri, string>) (p => p.zapisTipAkter)));
                for (int index = 0; index < list4.Count; ++index)
                {
                  DataTable dataTable8 = new DataTable();
                  GridView gridView9 = new GridView();
                  gridView9.CssClass = "GridViewStyle";
                  gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                  gridView9.RowStyle.CssClass = "RowStyle";
                  gridView9.HeaderStyle.CssClass = "HeaderStyle";
                  gridView9.ControlStyle.Font.Bold = true;
                  gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                  DataColumn column1 = new DataColumn(list4[index].zapis7);
                  DataColumn column2 = new DataColumn(" ");
                  dataTable8.Columns.Add(column1);
                  dataTable8.Columns.Add(column2);
                  Akteri akteri = new Akteri();
                  DataRow row1 = dataTable8.NewRow();
                  row1[0] = (object) "Тип на овластено лице";
                  row1[1] = (object) list4[index].zapis4;
                  dataTable8.Rows.Add(row1);
                  DataRow row2 = dataTable8.NewRow();
                  row2[0] = (object) "Матичен број на актер";
                  row2[1] = (object) list4[index].zapis5;
                  dataTable8.Rows.Add(row2);
                  DataRow row3 = dataTable8.NewRow();
                  row3[0] = (object) "Тип на актер";
                  row3[1] = (object) list4[index].zapis7;
                  dataTable8.Rows.Add(row3);
                  DataRow row4 = dataTable8.NewRow();
                  row4[0] = (object) "Име";
                  row4[1] = (object) list4[index].zapis8;
                  dataTable8.Rows.Add(row4);
                  DataRow row5 = dataTable8.NewRow();
                  row5[0] = (object) "Презиме";
                  row5[1] = (object) list4[index].zapis9;
                  dataTable8.Rows.Add(row5);
                  DataRow row6 = dataTable8.NewRow();
                  row6[0] = (object) "Код на земја";
                  row6[1] = (object) list4[index].zapis10;
                  dataTable8.Rows.Add(row6);
                  DataRow row7 = dataTable8.NewRow();
                  row7[0] = (object) "Адреса";
                  row7[1] = (object) (list4[index].zapis13 + " " + list4[index].zapis14 + " " + list4[index].zapis15 + " " + list4[index].zapis16 + " " + list4[index].zapis11 + " " + list4[index].zapis12);
                  dataTable8.Rows.Add(row7);
                  DataRow row8 = dataTable8.NewRow();
                  row8[0] = (object) "Електронско сандаче";
                  row8[1] = (object) list4[index].zapis17;
                  dataTable8.Rows.Add(row8);
                  DataRow row9 = dataTable8.NewRow();
                  row9[0] = (object) "Овластувања";
                  row9[1] = (object) list4[index].zapis18;
                  dataTable8.Rows.Add(row9);
                  DataRow row10 = dataTable8.NewRow();
                  row10[0] = (object) "Ограничувања";
                  row10[1] = (object) list4[index].zapis19;
                  dataTable8.Rows.Add(row10);
                  DataRow row11 = dataTable8.NewRow();
                  row11[0] = (object) "Тип на овластување";
                  row11[1] = (object) list4[index].zapis21;
                  dataTable8.Rows.Add(row11);
                  gridView9.DataSource = (object) dataTable8;
                  gridView9.DataBind();
                  if (index == 0)
                  {
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                    {
                      Text = "Податоци за актери"
                    });
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                  }
                  else
                  {
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                  }
                  this.WSResponseControlPanelCR_AKN.Controls.Add((Control) gridView9);
                  List<GridView> list5 = (List<GridView>) this.Session["GridViewList3"] ?? new List<GridView>();
                  list5.Add(gridView9);
                  this.Session["GridViewList3"] = (object) list5;
                }
              }
              this.ImageButtonCR_AKN.Visible = true;
              if ((GridView) this.Session["GridViewList1"] != null)
                list1.Add("Податоци за субјетот");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList2"] != null)
              {
                List<GridView> list3 = (List<GridView>) this.Session["GridViewList2"];
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за подружници"
                });
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) list3[0]);
                if (list3.Count > 1)
                {
                  for (int index = 1; index < list3.Count; ++index)
                  {
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                    this.WSResponseControlPanelCR_AKN.Controls.Add((Control) list3[index]);
                  }
                }
                list1.Add("Податоци за подружници");
              }
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList3"] != null)
                list1.Add("Податоци за актери");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList4"] != null)
                list1.Add("Податоци за сопственици");
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList5"] != null)
              {
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за активности"
                });
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) this.Session["GridViewList5"]);
                list1.Add("Податоци за активности");
              }
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList6"] != null)
              {
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за членство во синдикат"
                });
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) this.Session["GridViewList6"]);
                list1.Add("Податоци за членство во синдикат");
              }
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList7"] != null)
                list1.Add("Податоци за основање");
              else
                list1.Add((string) null);
              this.Session["ParametersPrint"] = (object) ("Единствен Матичен Број На Субјектот За АКН_" + EMBS.EMBS1);
              this.Session["ListaRezultHeaders"] = (object) list1;
            }
            this.Completed = true;
            reqrespPortClient.Close();
          }
          catch (Exception ex)
          {
            if (num == 3)
            {
              try
              {
                  FaultMessageAKNClass faultMessageAkn;
                using (StringReader stringReader = new StringReader(ex.Message))
                    faultMessageAkn = (FaultMessageAKNClass)new XmlSerializer(typeof(FaultMessageAKNClass)).Deserialize((TextReader)stringReader);
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = faultMessageAkn.Error;
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
              }
              catch
              {
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_AKN.Visible = false;
              }
            }
            reqrespPortClient.Abort();
          }
        }
      }
      else
      {
        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("<h3>"));
        Label label1 = new Label();
        label1.ID = "lblporaka";
        label1.Text = "Порака: ";
        Label label2 = new Label();
        label2.ID = "lblporakavalue";
        label2.Text = "Не се пронајдени податоци за параметарот по кој пребарувате!";
        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label1);
        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) label2);
        this.WSResponseControlPanelCR_AKN.Controls.Add((Control) new LiteralControl("</h3>"));
        this.ImageButtonCR_AKN.Visible = false;
      }
    }

    private void GetEMBSforCU()
    {
      this.Completed = false;
      string s = this.Session["IsMBS"] == null || !Convert.ToBoolean(this.Session["IsMBS"]) ? ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim() : this.Session["MBS"].ToString();
      double result;
      if (double.TryParse(s, out result) && result.ToString().Length == 7)
      {
        this.Session["GridViewList1"] = (object) null;
        this.Session["GridViewList2"] = (object) null;
        this.Session["GridViewList3"] = (object) null;
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://crbiztalkwcfserviceapplicationcu.interop.local/CRBizTalkWCFServiceApplicationCU/Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESP.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient receiveReqrespClient = new Module_1_CRBizTalkOrchestrationCU_WCF_CR_CU_RECEIVE_REQRESPClient((Binding) wsHttpBinding, remoteAddress);
        receiveReqrespClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserCU");
        interop.WebServiceCR_CU.EMBS EMBS = new interop.WebServiceCR_CU.EMBS();
        EMBS.Username = user.username;
        EMBS.Password = user.password;
        EMBS.EMBS1 = s;
        EMBS.EMBS2 = s;
        EMBS.EMBS3 = s;
        EMBS.NacinNaIsprakjanje = "EORPD";
        EMBS.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
        EMBS.TimeStamp = DateTime.Now.ToString();
        int num = 1;
        while (num <= 3 && !this.Completed)
        {
          ++num;
          try
          {
            receiveReqrespClient.Open();
            VratiCRMRezultatiCUResponse rezultatiCuResponse = receiveReqrespClient.Operation_Request(EMBS);
            List<string> list1 = new List<string>();
            if (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0] != null && (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0] != null) && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0].entranceNoField == "Не е пронајден запис за барањето!")
            {
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Не е пронајден запис за барањето!";
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_CU.Visible = false;
            }
            else if (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0] != null && (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0] != null) && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0].entranceNoField == "Невалиден сертификат на серверот за комуникација со ЦР!")
            {
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Невалиден сертификат на серверот за комуникација со ЦР!";
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_CU.Visible = false;
            }
            else if (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0] != null && (rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField != null && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0] != null) && rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField[0].crmResultItemField[0].entranceNoField == "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!")
            {
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_CU.Visible = false;
            }
            else
            {
              Label label1 = new Label();
              label1.Text = "Матичен број на правно лице: ";
              Label label2 = new Label();
              label2.Text = s;
              label2.Attributes.Add("style", "font-weight: bold;");
              LiteralControl literalControl1 = new LiteralControl("<p>");
              LiteralControl literalControl2 = new LiteralControl("</p>");
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) literalControl2);
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<br></br>"));
              this.WSResponseControlPanelCR_CU.Controls.Add((Control) literalControl1);
              GridView gridView1 = new GridView();
              GridView gridView2 = new GridView();
              GridView gridView3 = new GridView();
              DataTable dataTable1 = (DataTable) null;
              DataTable dataTable2 = (DataTable) null;
              DataTable dataTable3 = (DataTable) null;
              foreach (interop.WebServiceCR_CU.CrmResponseCrmResultItems responseCrmResultItems in rezultatiCuResponse.VratiCRMRezultatiCUResult.itemsField)
              {
                foreach (interop.WebServiceCR_CU.CrmResponseCrmResultItemsCrmResultItem itemsCrmResultItem in responseCrmResultItems.crmResultItemField)
                {
                  if (itemsCrmResultItem.lEIDField != null)
                  {
                    if ((itemsCrmResultItem.lEFullNameField != null || itemsCrmResultItem.municipalityField != null || (itemsCrmResultItem.municipalityCodeField != null || itemsCrmResultItem.placeField != null) || (itemsCrmResultItem.placeCodeField != null || itemsCrmResultItem.streetField != null || (itemsCrmResultItem.streetCodeField != null || itemsCrmResultItem.houseNoField != null)) || (itemsCrmResultItem.entranceNoField != null || itemsCrmResultItem.flatNoField != null)) && itemsCrmResultItem.taxPayerNumberField != null)
                    {
                      if (dataTable1 == null)
                      {
                        gridView1.CssClass = "GridViewStyle";
                        gridView1.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView1.RowStyle.CssClass = "RowStyle";
                        gridView1.HeaderStyle.CssClass = "HeaderStyle";
                        gridView1.ControlStyle.Font.Bold = true;
                        gridView1.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView1.ID = "GridViewListTemplate1";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable1 = new DataTable();
                        dataTable1.Columns.Add(column1);
                        dataTable1.Columns.Add(column2);
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Целосен назив на правно лице";
                        row2[1] = (object) itemsCrmResultItem.lEFullNameField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Даночен број";
                        row3[1] = (object) itemsCrmResultItem.taxPayerNumberField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Општина";
                        row4[1] = (object) itemsCrmResultItem.municipalityField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Код на општина";
                        row5[1] = (object) itemsCrmResultItem.municipalityCodeField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Место";
                        row6[1] = (object) itemsCrmResultItem.placeField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Код на место";
                        row7[1] = (object) itemsCrmResultItem.placeCodeField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Улица";
                        row8[1] = (object) itemsCrmResultItem.streetField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Код на улица";
                        row9[1] = (object) itemsCrmResultItem.streetCodeField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Број на куќа";
                        row10[1] = (object) itemsCrmResultItem.houseNoField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Број на влез";
                        row11[1] = (object) itemsCrmResultItem.entranceNoField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Број на стан";
                        row12[1] = (object) itemsCrmResultItem.flatNoField;
                        dataTable1.Rows.Add(row12);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        Label label3 = new Label();
                        label3.Text = "Податоци за субјетот";
                        list1.Add(label3.Text);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) label3);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                      else if (dataTable1 != null)
                      {
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Целосен назив на правно лице";
                        row2[1] = (object) itemsCrmResultItem.lEFullNameField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Даночен број";
                        row3[1] = (object) itemsCrmResultItem.taxPayerNumberField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Општина";
                        row4[1] = (object) itemsCrmResultItem.municipalityField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Код на општина";
                        row5[1] = (object) itemsCrmResultItem.municipalityCodeField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Место";
                        row6[1] = (object) itemsCrmResultItem.placeField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Код на место";
                        row7[1] = (object) itemsCrmResultItem.placeCodeField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Улица";
                        row8[1] = (object) itemsCrmResultItem.streetField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Код на улица";
                        row9[1] = (object) itemsCrmResultItem.streetCodeField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Број на куќа";
                        row10[1] = (object) itemsCrmResultItem.houseNoField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Број на влез";
                        row11[1] = (object) itemsCrmResultItem.entranceNoField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Број на стан";
                        row12[1] = (object) itemsCrmResultItem.flatNoField;
                        dataTable1.Rows.Add(row12);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                    }
                    List<GridView> list2;
                    if ((itemsCrmResultItem.lEIDField != null || itemsCrmResultItem.personTypeField != null || (itemsCrmResultItem.actorNameField != null || itemsCrmResultItem.actorSurnameField != null) || (itemsCrmResultItem.authorisationField != null || itemsCrmResultItem.restrictionsField != null || (itemsCrmResultItem.authTypeIDField != null || itemsCrmResultItem.authTypeDescField != null)) || (itemsCrmResultItem.municipalityField != null || itemsCrmResultItem.placeField != null || (itemsCrmResultItem.streetField != null || itemsCrmResultItem.houseNoField != null) || (itemsCrmResultItem.entranceNoField != null || itemsCrmResultItem.flatNoField != null || (itemsCrmResultItem.countryField != null || itemsCrmResultItem.phoneField != null))) || (itemsCrmResultItem.faxField != null || itemsCrmResultItem.emailField != null)) && itemsCrmResultItem.actorIDField != null)
                    {
                      if (dataTable2 == null)
                      {
                        gridView2.CssClass = "GridViewStyle";
                        gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView2.RowStyle.CssClass = "RowStyle";
                        gridView2.HeaderStyle.CssClass = "HeaderStyle";
                        gridView2.ControlStyle.Font.Bold = true;
                        gridView2.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView2.ID = "GridViewListTemplate2";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable2 = new DataTable();
                        dataTable2.Columns.Add(column1);
                        dataTable2.Columns.Add(column2);
                        DataRow row1 = dataTable2.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable2.Rows.Add(row1);
                        DataRow row2 = dataTable2.NewRow();
                        row2[0] = (object) "Матичен број на овластено лице";
                        row2[1] = (object) itemsCrmResultItem.actorIDField;
                        dataTable2.Rows.Add(row2);
                        DataRow row3 = dataTable2.NewRow();
                        row3[0] = (object) "Шифра на тип на овластено лице";
                        row3[1] = (object) itemsCrmResultItem.personTypeField;
                        dataTable2.Rows.Add(row3);
                        DataRow row4 = dataTable2.NewRow();
                        row4[0] = (object) "Име";
                        row4[1] = (object) itemsCrmResultItem.actorNameField;
                        dataTable2.Rows.Add(row4);
                        DataRow row5 = dataTable2.NewRow();
                        row5[0] = (object) "Презиме";
                        row5[1] = (object) itemsCrmResultItem.actorSurnameField;
                        dataTable2.Rows.Add(row5);
                        DataRow row6 = dataTable2.NewRow();
                        row6[0] = (object) "Управител без ограничувања";
                        row6[1] = (object) itemsCrmResultItem.authorisationField;
                        dataTable2.Rows.Add(row6);
                        DataRow row7 = dataTable2.NewRow();
                        row7[0] = (object) "Опис на ограничување";
                        row7[1] = (object) itemsCrmResultItem.restrictionsField;
                        dataTable2.Rows.Add(row7);
                        DataRow row8 = dataTable2.NewRow();
                        row8[0] = (object) "Тип на овластување";
                        row8[1] = (object) itemsCrmResultItem.authTypeIDField;
                        dataTable2.Rows.Add(row8);
                        DataRow row9 = dataTable2.NewRow();
                        row9[0] = (object) "Опис на типот на овластувањето";
                        row9[1] = (object) itemsCrmResultItem.authTypeDescField;
                        dataTable2.Rows.Add(row9);
                        DataRow row10 = dataTable2.NewRow();
                        row10[0] = (object) "Општина";
                        row10[1] = (object) itemsCrmResultItem.municipalityField;
                        dataTable2.Rows.Add(row10);
                        DataRow row11 = dataTable2.NewRow();
                        row11[0] = (object) "Место";
                        row11[1] = (object) itemsCrmResultItem.placeField;
                        dataTable2.Rows.Add(row11);
                        DataRow row12 = dataTable2.NewRow();
                        row12[0] = (object) "Улица";
                        row12[1] = (object) itemsCrmResultItem.streetField;
                        dataTable2.Rows.Add(row12);
                        DataRow row13 = dataTable2.NewRow();
                        row13[0] = (object) "Број на куќа";
                        row13[1] = (object) itemsCrmResultItem.houseNoField;
                        dataTable2.Rows.Add(row13);
                        DataRow row14 = dataTable2.NewRow();
                        row14[0] = (object) "Број на влез";
                        row14[1] = (object) itemsCrmResultItem.entranceNoField;
                        dataTable2.Rows.Add(row14);
                        DataRow row15 = dataTable2.NewRow();
                        row15[0] = (object) "Број на стан";
                        row15[1] = (object) itemsCrmResultItem.flatNoField;
                        dataTable2.Rows.Add(row15);
                        DataRow row16 = dataTable2.NewRow();
                        row16[0] = (object) "Код на држава";
                        row16[1] = (object) itemsCrmResultItem.countryCodeField;
                        dataTable2.Rows.Add(row16);
                        DataRow row17 = dataTable2.NewRow();
                        row17[0] = (object) "Држава";
                        row17[1] = (object) itemsCrmResultItem.countryField;
                        dataTable2.Rows.Add(row17);
                        DataRow row18 = dataTable2.NewRow();
                        row18[0] = (object) "Телефонски број";
                        row18[1] = (object) itemsCrmResultItem.phoneField;
                        dataTable2.Rows.Add(row18);
                        DataRow row19 = dataTable2.NewRow();
                        row19[0] = (object) "Факс";
                        row19[1] = (object) itemsCrmResultItem.faxField;
                        dataTable2.Rows.Add(row19);
                        DataRow row20 = dataTable2.NewRow();
                        row20[0] = (object) "Ел.пошта";
                        row20[1] = (object) itemsCrmResultItem.emailField;
                        dataTable2.Rows.Add(row20);
                        DataRow row21 = dataTable2.NewRow();
                        row21[0] = (object) "Веб адреса";
                        row21[1] = (object) itemsCrmResultItem.webAddressField;
                        dataTable2.Rows.Add(row21);
                        gridView2.DataSource = (object) dataTable2;
                        gridView2.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        Label label3 = new Label();
                        label3.Text = "Податоци за сопственици";
                        list1.Add(label3.Text);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) label3);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView2);
                        list2 = (List<GridView>) this.Session["GridViewList2"];
                        this.Session["GridViewList2"] = (object) new List<GridView>()
                        {
                          gridView2
                        };
                      }
                      else if (dataTable2 != null)
                      {
                        DataTable dataTable4 = new DataTable();
                        GridView gridView4 = new GridView();
                        gridView4.CssClass = "GridViewStyle";
                        gridView4.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView4.RowStyle.CssClass = "RowStyle";
                        gridView4.ControlStyle.Font.Bold = true;
                        gridView4.HeaderStyle.CssClass = "HeaderStyle";
                        gridView4.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable4.Columns.Add(column1);
                        dataTable4.Columns.Add(column2);
                        DataRow row1 = dataTable4.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable4.Rows.Add(row1);
                        DataRow row2 = dataTable4.NewRow();
                        row2[0] = (object) "Матичен број на овластено лице";
                        row2[1] = (object) itemsCrmResultItem.actorIDField;
                        dataTable4.Rows.Add(row2);
                        DataRow row3 = dataTable4.NewRow();
                        row3[0] = (object) "Шифра на тип на овластено лице";
                        row3[1] = (object) itemsCrmResultItem.personTypeField;
                        dataTable4.Rows.Add(row3);
                        DataRow row4 = dataTable4.NewRow();
                        row4[0] = (object) "Име";
                        row4[1] = (object) itemsCrmResultItem.actorNameField;
                        dataTable4.Rows.Add(row4);
                        DataRow row5 = dataTable4.NewRow();
                        row5[0] = (object) "Презиме";
                        row5[1] = (object) itemsCrmResultItem.actorSurnameField;
                        dataTable4.Rows.Add(row5);
                        DataRow row6 = dataTable4.NewRow();
                        row6[0] = (object) "Управител без ограничувања";
                        row6[1] = (object) itemsCrmResultItem.authorisationField;
                        dataTable4.Rows.Add(row6);
                        DataRow row7 = dataTable4.NewRow();
                        row7[0] = (object) "Опис на ограничување";
                        row7[1] = (object) itemsCrmResultItem.restrictionsField;
                        dataTable4.Rows.Add(row7);
                        DataRow row8 = dataTable4.NewRow();
                        row8[0] = (object) "Тип на овластување";
                        row8[1] = (object) itemsCrmResultItem.authTypeIDField;
                        dataTable4.Rows.Add(row8);
                        DataRow row9 = dataTable4.NewRow();
                        row9[0] = (object) "Опис на типот на овластувањето";
                        row9[1] = (object) itemsCrmResultItem.authTypeDescField;
                        dataTable4.Rows.Add(row9);
                        DataRow row10 = dataTable4.NewRow();
                        row10[0] = (object) "Општина";
                        row10[1] = (object) itemsCrmResultItem.municipalityField;
                        dataTable4.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Место";
                        row11[1] = (object) itemsCrmResultItem.placeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable4.NewRow();
                        row12[0] = (object) "Улица";
                        row12[1] = (object) itemsCrmResultItem.streetField;
                        dataTable4.Rows.Add(row12);
                        DataRow row13 = dataTable4.NewRow();
                        row13[0] = (object) "Број на куќа";
                        row13[1] = (object) itemsCrmResultItem.houseNoField;
                        dataTable4.Rows.Add(row13);
                        DataRow row14 = dataTable4.NewRow();
                        row14[0] = (object) "Број на влез";
                        row14[1] = (object) itemsCrmResultItem.entranceNoField;
                        dataTable4.Rows.Add(row14);
                        DataRow row15 = dataTable4.NewRow();
                        row15[0] = (object) "Број на стан";
                        row15[1] = (object) itemsCrmResultItem.flatNoField;
                        dataTable4.Rows.Add(row15);
                        DataRow row16 = dataTable4.NewRow();
                        row16[0] = (object) "Код на држава";
                        row16[1] = (object) itemsCrmResultItem.countryCodeField;
                        dataTable4.Rows.Add(row16);
                        DataRow row17 = dataTable4.NewRow();
                        row17[0] = (object) "Држава";
                        row17[1] = (object) itemsCrmResultItem.countryField;
                        dataTable4.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Телефонски број";
                        row18[1] = (object) itemsCrmResultItem.phoneField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable4.NewRow();
                        row19[0] = (object) "Факс";
                        row19[1] = (object) itemsCrmResultItem.faxField;
                        dataTable4.Rows.Add(row19);
                        DataRow row20 = dataTable4.NewRow();
                        row20[0] = (object) "Ел.пошта";
                        row20[1] = (object) itemsCrmResultItem.emailField;
                        dataTable4.Rows.Add(row20);
                        DataRow row21 = dataTable4.NewRow();
                        row21[0] = (object) "Веб адреса";
                        row21[1] = (object) itemsCrmResultItem.webAddressField;
                        dataTable4.Rows.Add(row21);
                        gridView4.DataSource = (object) dataTable4;
                        gridView4.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView4);
                        List<GridView> list3 = (List<GridView>) this.Session["GridViewList2"];
                        list3.Add(gridView4);
                        this.Session["GridViewList2"] = (object) list3;
                      }
                    }
                    if (itemsCrmResultItem.bankruptcyCourtNameField != null || itemsCrmResultItem.courtJournalIDField != null || itemsCrmResultItem.validityDateField != null || itemsCrmResultItem.validityTimeField != null)
                    {
                      if (dataTable3 == null)
                      {
                        dataTable3 = new DataTable();
                        gridView3.CssClass = "GridViewStyle";
                        gridView3.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView3.RowStyle.CssClass = "RowStyle";
                        gridView3.HeaderStyle.CssClass = "HeaderStyle";
                        gridView3.ControlStyle.Font.Bold = true;
                        gridView3.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView3.ID = "GridViewListTemplate3";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable3.Columns.Add(column1);
                        dataTable3.Columns.Add(column2);
                        DataRow row1 = dataTable3.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable3.Rows.Add(row1);
                        DataRow row2 = dataTable3.NewRow();
                        row2[0] = (object) "Име на суд";
                        row2[1] = (object) itemsCrmResultItem.bankruptcyCourtNameField;
                        dataTable3.Rows.Add(row2);
                        DataRow row3 = dataTable3.NewRow();
                        row3[0] = (object) "Број на судска постапка";
                        row3[1] = (object) itemsCrmResultItem.courtJournalIDField;
                        dataTable3.Rows.Add(row3);
                        DataRow row4 = dataTable3.NewRow();
                        row4[0] = (object) "Датум на одлука";
                        row4[1] = (object) itemsCrmResultItem.decisionDateField;
                        dataTable3.Rows.Add(row4);
                        DataRow row5 = dataTable3.NewRow();
                        row5[0] = (object) "Датум на отварање на постапка";
                        row5[1] = (object) itemsCrmResultItem.validityDateField;
                        dataTable3.Rows.Add(row5);
                        DataRow row6 = dataTable3.NewRow();
                        row6[0] = (object) "Време на отварање на постапка";
                        row6[1] = (object) itemsCrmResultItem.validityTimeField;
                        dataTable3.Rows.Add(row6);
                        DataRow row7 = dataTable3.NewRow();
                        row7[0] = (object) "Статус на постапка";
                        row7[1] = (object) itemsCrmResultItem.stageIDField;
                        dataTable3.Rows.Add(row7);
                        DataRow row8 = dataTable3.NewRow();
                        row8[0] = (object) "Опис на статусот на постапката";
                        row8[1] = (object) itemsCrmResultItem.stageDescField;
                        dataTable3.Rows.Add(row8);
                        DataRow row9 = dataTable3.NewRow();
                        row9[0] = (object) "Опис на постапката";
                        row9[1] = (object) itemsCrmResultItem.dispositionField;
                        dataTable3.Rows.Add(row9);
                        DataRow row10 = dataTable3.NewRow();
                        row10[0] = (object) "Тип на постапка";
                        row10[1] = (object) itemsCrmResultItem.typeIDField;
                        dataTable3.Rows.Add(row10);
                        DataRow row11 = dataTable3.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) itemsCrmResultItem.typeDescField;
                        dataTable3.Rows.Add(row11);
                        gridView3.DataSource = (object) dataTable3;
                        gridView3.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        Label label3 = new Label();
                        label3.Text = "Податоци за судски постапки";
                        list1.Add(label3.Text);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) label3);
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView3);
                        list2 = (List<GridView>) this.Session["GridViewList3"];
                        this.Session["GridViewList3"] = (object) new List<GridView>()
                        {
                          gridView3
                        };
                      }
                      else if (dataTable3 != null)
                      {
                        DataTable dataTable4 = new DataTable();
                        GridView gridView4 = new GridView();
                        gridView4.CssClass = "GridViewStyle";
                        gridView4.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView4.RowStyle.CssClass = "RowStyle";
                        gridView4.HeaderStyle.CssClass = "HeaderStyle";
                        gridView4.ControlStyle.Font.Bold = true;
                        gridView4.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable4.Columns.Add(column1);
                        dataTable4.Columns.Add(column2);
                        DataRow row1 = dataTable4.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) itemsCrmResultItem.lEIDField;
                        dataTable4.Rows.Add(row1);
                        DataRow row2 = dataTable4.NewRow();
                        row2[0] = (object) "Име на суд";
                        row2[1] = (object) itemsCrmResultItem.bankruptcyCourtNameField;
                        dataTable4.Rows.Add(row2);
                        DataRow row3 = dataTable4.NewRow();
                        row3[0] = (object) "Број на судска постапка";
                        row3[1] = (object) itemsCrmResultItem.courtJournalIDField;
                        dataTable4.Rows.Add(row3);
                        DataRow row4 = dataTable4.NewRow();
                        row4[0] = (object) "Датум на одлука";
                        row4[1] = (object) itemsCrmResultItem.decisionDateField;
                        dataTable4.Rows.Add(row4);
                        DataRow row5 = dataTable4.NewRow();
                        row5[0] = (object) "Датум на отварање на постапка";
                        row5[1] = (object) itemsCrmResultItem.validityDateField;
                        dataTable4.Rows.Add(row5);
                        DataRow row6 = dataTable4.NewRow();
                        row6[0] = (object) "Време на отварање на постапка";
                        row6[1] = (object) itemsCrmResultItem.validityTimeField;
                        dataTable4.Rows.Add(row6);
                        DataRow row7 = dataTable4.NewRow();
                        row7[0] = (object) "Статус на постапка";
                        row7[1] = (object) itemsCrmResultItem.stageIDField;
                        dataTable4.Rows.Add(row7);
                        DataRow row8 = dataTable4.NewRow();
                        row8[0] = (object) "Опис на статусот на постапката";
                        row8[1] = (object) itemsCrmResultItem.stageDescField;
                        dataTable4.Rows.Add(row8);
                        DataRow row9 = dataTable4.NewRow();
                        row9[0] = (object) "Опис на постапката";
                        row9[1] = (object) itemsCrmResultItem.dispositionField;
                        dataTable4.Rows.Add(row9);
                        DataRow row10 = dataTable4.NewRow();
                        row10[0] = (object) "Тип на постапка";
                        row10[1] = (object) itemsCrmResultItem.typeIDField;
                        dataTable4.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) itemsCrmResultItem.typeDescField;
                        dataTable4.Rows.Add(row11);
                        gridView4.DataSource = (object) dataTable4;
                        gridView4.DataBind();
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_CU.Controls.Add((Control) gridView4);
                        List<GridView> list3 = (List<GridView>) this.Session["GridViewList3"];
                        list3.Add(gridView4);
                        this.Session["GridViewList3"] = (object) list3;
                      }
                    }
                  }
                }
              }
              this.ImageButtonCR_CU.Visible = true;
              this.Session["ParametersPrint"] = (object) ("Единствен Матичен Број На Субјектот За ЦУ_" + EMBS.EMBS1);
              this.Session["ListaRezultHeaders"] = (object) list1;
            }
            this.Completed = true;
            receiveReqrespClient.Close();
          }
          catch (Exception ex)
          {
            if (num == 3)
            {
              try
              {
                  FaultMessageCUClass faultMessageCu;
                using (StringReader stringReader = new StringReader(ex.Message))
                    faultMessageCu = (FaultMessageCUClass)new XmlSerializer(typeof(FaultMessageCUClass)).Deserialize((TextReader)stringReader);
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = faultMessageCu.Error;
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_CU.Visible = false;
              }
              catch
              {
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_CU.Visible = false;
              }
            }
          }
        }
      }
      else
      {
        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("<h3>"));
        Label label1 = new Label();
        label1.ID = "lblporaka";
        label1.Text = "Порака: ";
        Label label2 = new Label();
        label2.ID = "lblporakavalue";
        label2.Text = "Не се пронајдени податоци за параметарот по кој пребарувате!";
        this.WSResponseControlPanelCR_CU.Controls.Add((Control) label1);
        this.WSResponseControlPanelCR_CU.Controls.Add((Control) label2);
        this.WSResponseControlPanelCR_CU.Controls.Add((Control) new LiteralControl("</h3>"));
        this.ImageButtonCR_CU.Visible = false;
      }
    }

    private void GetEMBSforUJP()
    {
      this.Completed = false;
      string s = this.Session["IsMBS"] == null || !Convert.ToBoolean(this.Session["IsMBS"]) ? ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim() : this.Session["MBS"].ToString();
      double result;
      if (double.TryParse(s, out result) && result.ToString().Length == 7)
      {
        this.Session["GridViewList1"] = (object) null;
        this.Session["GridViewList2"] = (object) null;
        this.Session["GridViewList3"] = (object) null;
        this.Session["GridViewList4"] = (object) null;
        this.Session["GridViewList5"] = (object) null;
        this.Session["GridViewList6"] = (object) null;
        this.Session["GridViewList7"] = (object) null;
        this.Session["GridViewList8"] = (object) null;
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://crbiztalkwcfserviceapplicationujp.interop.local/CRBizTalkWCFServiceApplicationUJP/Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient reqrespPortClient = new Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) "UserUJP");
        interop.WebServiceCR_UJP.EMBS EMBS = new interop.WebServiceCR_UJP.EMBS();
        EMBS.Username = user.username;
        EMBS.Password = user.password;
        EMBS.EMBS1 = s;
        EMBS.EMBS2 = s;
        EMBS.EMBS3 = s;
        EMBS.EMBS4 = s;
        EMBS.EMBS5 = s;
        EMBS.EMBS6 = s;
        EMBS.EMBS7 = s;
        EMBS.EMBS8 = s;
        EMBS.NacinNaIsprakjanje = "EORPD";
        EMBS.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
        EMBS.TimeStamp = DateTime.Now.ToString();
        int num = 1;
        while (num <= 3 && !this.Completed)
        {
          ++num;
          try
          {
            reqrespPortClient.Open();
            VratiCRMRezultatiUJPResponse dataByEmbs = reqrespPortClient.GetDataByEMBS(EMBS);
            List<string> list1 = new List<string>();
            if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Не е пронајден запис за барањето!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Не е пронајден запис за барањето!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Невалиден сертификат на серверот за комуникација со ЦР!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Невалиден сертификат на серверот за комуникација со ЦР!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else
            {
              Label label1 = new Label();
              label1.Text = "Матичен број на правно лице: ";
              Label label2 = new Label();
              label2.Text = s;
              label2.Attributes.Add("style", "font-weight: bold;");
              LiteralControl literalControl1 = new LiteralControl("<p>");
              LiteralControl literalControl2 = new LiteralControl("</p>");
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<br></br>"));
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl1);
              GridView gridView1 = new GridView();
              GridView gridView2 = new GridView();
              GridView gridView3 = new GridView();
              GridView gridView4 = new GridView();
              GridView gridView5 = new GridView();
              GridView gridView6 = new GridView();
              GridView gridView7 = new GridView();
              GridView gridView8 = new GridView();
              DataTable dataTable1 = (DataTable) null;
              DataTable dataTable2 = (DataTable) null;
              DataTable dataTable3 = (DataTable) null;
              DataTable dataTable4 = (DataTable) null;
              DataTable dataTable5 = (DataTable) null;
              DataTable dataTable6 = (DataTable) null;
              DataTable dataTable7 = (DataTable) null;
              DataTable dataTable8 = (DataTable) null;
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems1 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems2 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems3 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems4 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems5 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems6 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6];
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1] = responseCrmResultItems6;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2] = responseCrmResultItems3;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3] = responseCrmResultItems2;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4] = responseCrmResultItems1;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5] = responseCrmResultItems4;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6] = responseCrmResultItems5;
              List<Akteri> list2 = new List<Akteri>();
              foreach (interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems7 in dataByEmbs.VratiCRMRezultatiUJPResult.itemsField)
              {
                string templateNameField = responseCrmResultItems7.templateNameField;
                foreach (interop.WebServiceCR_UJP.CrmResponseCrmResultItemsCrmResultItem itemsCrmResultItem in responseCrmResultItems7.crmResultItemField)
                {
                  interop.WebServiceCR_UJP.CrmResponseCrmResultItemsCrmResultItem objekt = itemsCrmResultItem;
                  if (objekt.lEIDField != null)
                  {
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].templateNameField && (objekt.isLETerminatedField != null || objekt.lEIDField != null || (objekt.lEFullNameField != null || objekt.shortNameField != null) || (objekt.terminationTypeIDField != null || objekt.terminationTypeDescField != null || (objekt.lETypeIDField != null || objekt.lETypeDescField != null)) || (objekt.lESizeIDField != null || objekt.lESizeDescField != null || (objekt.municipalityField != null || objekt.municipalityCodeField != null) || (objekt.placeField != null || objekt.placeCodeField != null || (objekt.streetField != null || objekt.streetCodeField != null))) || (objekt.houseNoField != null || objekt.entranceNoField != null || (objekt.flatNoField != null || objekt.organisationTypeCodeField != null) || (objekt.organisationTypeDescField != null || objekt.registerCategoryIDField != null || (objekt.registerCategoryField != null || objekt.authorisedRegisterIDField != null)) || (objekt.authorisedRegisterField != null || objekt.ownershipTypeIDField != null || (objekt.ownershipTypeDescField != null || objekt.isForeignActField != null) || (objekt.isActivityNoLicenceField != null || objekt.coreActivityCodeField != null || (objekt.coreActivityDescField != null || objekt.emailField != null)))) || (objekt.foreignActivityField != null || objekt.additionalInfoField != null || (objekt.actingPeriodField != null || objekt.isDataConfirmedField != null) || objekt.isAAActiveField != null)) && objekt.taxPayerNumberField != null)
                    {
                      if (dataTable1 == null)
                      {
                        gridView1.CssClass = "GridViewStyle";
                        gridView1.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView1.RowStyle.CssClass = "RowStyle";
                        gridView1.HeaderStyle.CssClass = "HeaderStyle";
                        gridView1.ControlStyle.Font.Bold = true;
                        gridView1.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView1.ID = "GridViewListTemplate1";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable1 = new DataTable();
                        dataTable1.Columns.Add(column1);
                        dataTable1.Columns.Add(column2);
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Кратко име";
                        row5[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Причина за престанок";
                        row6[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Опис на престанокот";
                        row7[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Вид на субјект";
                        row8[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на вид на субјект";
                        row9[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Големина на субјект";
                        row10[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на големина на субјект";
                        row11[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Број на куќа";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Број на влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Број на стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Организационен облик";
                        row18[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Регистар";
                        row19[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Надлежен регистар";
                        row20[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Вид на сопственост";
                        row21[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Евидентирани дејности во надворешен промет";
                        row22[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Општа клаузула за бизнис";
                        row23[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Опис на претежна дејност";
                        row24[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Е-пошта";
                        row25[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Други дејности";
                        row26[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Дополнителни инфо.";
                        row27[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Времетраење";
                        row28[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Потврдени податоци";
                        row29[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Активност од регистар на г.с.";
                        row30[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row30[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row30[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row30[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row30);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за субјетот"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                      else if (dataTable1 != null)
                      {
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Кратко име";
                        row5[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Причина за престанок";
                        row6[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Опис на престанокот";
                        row7[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Вид на субјект";
                        row8[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на вид на субјект";
                        row9[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Големина на субјект";
                        row10[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на големина на субјект";
                        row11[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Број на куќа";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Број на влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Број на стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Организационен облик";
                        row18[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Регистар";
                        row19[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Надлежен регистар";
                        row20[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Опис на вид на сопственост";
                        row21[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Евидентирани дејности во надворешен промет";
                        row22[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Општа клаузула за бизнис";
                        row23[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Опис на претежна дејност";
                        row24[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Е-пошта";
                        row25[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Други дејности";
                        row26[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Дополнителни инфо.";
                        row27[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Времетраење";
                        row28[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Потврдени податоци";
                        row29[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Активност од регистар на г.с.";
                        row30[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row30[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row30[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row30[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row30);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                    }
                    List<GridView> list3;
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1].templateNameField && (objekt.lEIDField != null || objekt.capitalOriginIDField != null || (objekt.capitalOriginDescField != null || objekt.fCCodeField != null) || (objekt.capitalFC_CashField != null || objekt.capitalFC_NonCashField != null || (objekt.capitalFC_PaydField != null || objekt.sharesTotalField != null)) || (objekt.sharesPaydField != null || objekt.typeOfSharesField != null || (objekt.sharesPaymentField != null || objekt.sharesPublishingField != null) || (objekt.licenceField != null || objekt.capitalFC_TotalField != null))) && objekt.foundingDateField != null)
                    {
                      if (dataTable7 == null)
                      {
                        dataTable7 = new DataTable();
                        gridView7.CssClass = "GridViewStyle";
                        gridView7.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView7.RowStyle.CssClass = "RowStyle";
                        gridView7.HeaderStyle.CssClass = "HeaderStyle";
                        gridView7.ControlStyle.Font.Bold = true;
                        gridView7.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView7.ID = "GridViewListTemplate7";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable7.Columns.Add(column1);
                        dataTable7.Columns.Add(column2);
                        DataRow row1 = dataTable7.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable7.Rows.Add(row1);
                        DataRow row2 = dataTable7.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable7.Rows.Add(row2);
                        DataRow row3 = dataTable7.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable7.Rows.Add(row3);
                        DataRow row4 = dataTable7.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable7.Rows.Add(row4);
                        DataRow row5 = dataTable7.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable7.Rows.Add(row5);
                        DataRow row6 = dataTable7.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable7.Rows.Add(row6);
                        DataRow row7 = dataTable7.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable7.Rows.Add(row7);
                        DataRow row8 = dataTable7.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable7.Rows.Add(row8);
                        DataRow row9 = dataTable7.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable7.Rows.Add(row9);
                        DataRow row10 = dataTable7.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable7.Rows.Add(row10);
                        DataRow row11 = dataTable7.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable7.Rows.Add(row11);
                        DataRow row12 = dataTable7.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable7.Rows.Add(row12);
                        DataRow row13 = dataTable7.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable7.Rows.Add(row13);
                        DataRow row14 = dataTable7.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable7.Rows.Add(row14);
                        DataRow row15 = dataTable7.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable7.Rows.Add(row15);
                        gridView7.DataSource = (object) dataTable7;
                        gridView7.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за основање"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView7);
                        list3 = (List<GridView>) this.Session["GridViewList7"];
                        this.Session["GridViewList7"] = (object) new List<GridView>()
                        {
                          gridView7
                        };
                      }
                      else if (dataTable7 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable9.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable9.Rows.Add(row15);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList7"];
                        list4.Add(gridView9);
                        this.Session["GridViewList7"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2].templateNameField && (objekt.lEIDField != null || objekt.personOrLEIDField != null || (objekt.personOrLEDescField != null || objekt.ownerTypeIDField != null) || (objekt.ownerTypeDescField != null || objekt.liabilityIDField != null || (objekt.liabilityDescField != null || objekt.ownerNameField != null)) || (objekt.ownerSurnameField != null || objekt.countryCodeField != null || (objekt.municipalityField != null || objekt.placeField != null) || (objekt.streetField != null || objekt.houseNoField != null || (objekt.entranceNoField != null || objekt.flatNoField != null))) || (objekt.emailField != null || objekt.fCCodeField != null || (objekt.participationFC_CashField != null || objekt.participationFC_NonCashField != null) || (objekt.participationFC_PaydField != null || objekt.participationFC_TotalField != null || objekt.addInfo != null))) && objekt.ownerIDField != null)
                    {
                      if (dataTable4 == null)
                      {
                        dataTable4 = new DataTable();
                        gridView4.CssClass = "GridViewStyle";
                        gridView4.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView4.RowStyle.CssClass = "RowStyle";
                        gridView4.HeaderStyle.CssClass = "HeaderStyle";
                        gridView4.ControlStyle.Font.Bold = true;
                        gridView4.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView4.ID = "GridViewListTemplate4";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable4.Columns.Add(column1);
                        dataTable4.Columns.Add(column2);
                        DataRow row1 = dataTable4.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable4.Rows.Add(row1);
                        DataRow row2 = dataTable4.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable4.Rows.Add(row2);
                        DataRow row3 = dataTable4.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable4.Rows.Add(row3);
                        DataRow row4 = dataTable4.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable4.Rows.Add(row4);
                        DataRow row5 = dataTable4.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable4.Rows.Add(row5);
                        DataRow row6 = dataTable4.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable4.Rows.Add(row6);
                        DataRow row7 = dataTable4.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable4.Rows.Add(row7);
                        DataRow row8 = dataTable4.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable4.Rows.Add(row8);
                        DataRow row9 = dataTable4.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable4.Rows.Add(row9);
                        DataRow row10 = dataTable4.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable4.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable4.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable4.Rows.Add(row12);
                        DataRow row13 = dataTable4.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable4.Rows.Add(row13);
                        DataRow row14 = dataTable4.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable4.Rows.Add(row14);
                        DataRow row15 = dataTable4.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable4.Rows.Add(row15);
                        DataRow row16 = dataTable4.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable4.Rows.Add(row16);
                        DataRow row17 = dataTable4.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable4.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable4.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable4.Rows.Add(row19);
                        DataRow row20 = dataTable4.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable4.Rows.Add(row20);
                        DataRow row21 = dataTable4.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable4.Rows.Add(row21);
                        DataRow row22 = dataTable4.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable4.Rows.Add(row22);
                        DataRow row23 = dataTable4.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable4.Rows.Add(row23);
                        DataRow row24 = dataTable4.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable4.Rows.Add(row24);
                        gridView4.DataSource = (object) dataTable4;
                        gridView4.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за сопственици"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView4);
                        list3 = (List<GridView>) this.Session["GridViewList4"];
                        this.Session["GridViewList4"] = (object) new List<GridView>()
                        {
                          gridView4
                        };
                      }
                      else if (dataTable4 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable9.Rows.Add(row15);
                        DataRow row16 = dataTable9.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable9.Rows.Add(row16);
                        DataRow row17 = dataTable9.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable9.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable9.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable9.Rows.Add(row19);
                        DataRow row20 = dataTable9.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable9.Rows.Add(row20);
                        DataRow row21 = dataTable9.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable9.Rows.Add(row21);
                        DataRow row22 = dataTable9.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable9.Rows.Add(row22);
                        DataRow row23 = dataTable9.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable9.Rows.Add(row23);
                        DataRow row24 = dataTable9.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable9.Rows.Add(row24);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList4"];
                        list4.Add(gridView9);
                        this.Session["GridViewList4"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.personOrLEIDField != null || objekt.personOrLEDescField != null) || (objekt.actorTypeIDField != null || objekt.actorTypeDescField != null || (objekt.actorNameField != null || objekt.actorSurnameField != null)) || (objekt.countryCodeField != null || objekt.municipalityField != null || (objekt.placeField != null || objekt.streetField != null) || (objekt.houseNoField != null || objekt.flatNoField != null || (objekt.emailField != null || objekt.descriptionField != null))) || (objekt.restrictionsField != null || objekt.authorisationTypeIDField != null || objekt.authorisationTypeDescField != null)) && objekt.actorIDField != null)
                    {
                      if (dataTable3 == null)
                      {
                        dataTable3 = new DataTable();
                        gridView3.CssClass = "GridViewStyle";
                        gridView3.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView3.RowStyle.CssClass = "RowStyle";
                        gridView3.HeaderStyle.CssClass = "HeaderStyle";
                        gridView3.ControlStyle.Font.Bold = true;
                        gridView3.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView3.ID = "GridViewListTemplate3";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable3.Columns.Add(column1);
                        dataTable3.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable3.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable3.Rows.Add(row1);
                        DataRow row2 = dataTable3.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable3.Rows.Add(row2);
                        DataRow row3 = dataTable3.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable3.Rows.Add(row3);
                        DataRow row4 = dataTable3.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable3.Rows.Add(row4);
                        DataRow row5 = dataTable3.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable3.Rows.Add(row5);
                        DataRow row6 = dataTable3.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable3.Rows.Add(row6);
                        DataRow row7 = dataTable3.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable3.Rows.Add(row7);
                        DataRow row8 = dataTable3.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable3.Rows.Add(row8);
                        DataRow row9 = dataTable3.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable3.Rows.Add(row9);
                        DataRow row10 = dataTable3.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable3.Rows.Add(row10);
                        DataRow row11 = dataTable3.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable3.Rows.Add(row11);
                        DataRow row12 = dataTable3.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable3.Rows.Add(row12);
                        DataRow row13 = dataTable3.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable3.Rows.Add(row13);
                        DataRow row14 = dataTable3.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable3.Rows.Add(row14);
                        DataRow row15 = dataTable3.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable3.Rows.Add(row15);
                        DataRow row16 = dataTable3.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable3.Rows.Add(row16);
                        DataRow row17 = dataTable3.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable3.Rows.Add(row17);
                        DataRow row18 = dataTable3.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable3.Rows.Add(row18);
                        DataRow row19 = dataTable3.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable3.Rows.Add(row19);
                        DataRow row20 = dataTable3.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable3.Rows.Add(row20);
                        DataRow row21 = dataTable3.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable3.Rows.Add(row21);
                        list2.Add(akteri);
                        if (!(objekt.unitNoField == "0"))
                          dataTable3 = (DataTable) null;
                      }
                      else if (dataTable3 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable9.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable9.Rows.Add(row15);
                        DataRow row16 = dataTable9.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable9.Rows.Add(row16);
                        DataRow row17 = dataTable9.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable9.Rows.Add(row17);
                        DataRow row18 = dataTable9.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable9.Rows.Add(row18);
                        DataRow row19 = dataTable9.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable9.Rows.Add(row19);
                        DataRow row20 = dataTable9.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable9.Rows.Add(row20);
                        DataRow row21 = dataTable9.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable9.Rows.Add(row21);
                        list2.Add(akteri);
                        if (!(objekt.unitNoField == "0"))
                          ;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.unitNameField != null || objekt.unitTypeIDField != null) || (objekt.unitTypeDescrField != null || objekt.unitDescrField != null || (objekt.otherInfoField != null || objekt.countryCodeField != null)) || (objekt.municipalityField != null || objekt.municipalityCodeField != null || (objekt.placeField != null || objekt.placeCodeField != null) || (objekt.streetField != null || objekt.streetCodeField != null || (objekt.houseNoField != null || objekt.entranceNoField != null))) || (objekt.flatNoField != null || objekt.activityCodeField != null || objekt.activityDescField != null)) && objekt.unitNoField != null)
                    {
                      if (dataTable2 == null)
                      {
                        gridView2.CssClass = "GridViewStyle";
                        gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView2.RowStyle.CssClass = "RowStyle";
                        gridView2.HeaderStyle.CssClass = "HeaderStyle";
                        gridView2.ControlStyle.Font.Bold = true;
                        gridView2.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView2.ID = "GridViewListTemplate2";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable2 = new DataTable();
                        dataTable2.Columns.Add(column1);
                        dataTable2.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable2.NewRow();
                        row1[0] = (object) "Број на подружница";
                        row1[1] = (object) objekt.unitNoField;
                        dataTable2.Rows.Add(row1);
                        DataRow row2 = dataTable2.NewRow();
                        row2[0] = (object) "Назив на подружница";
                        row2[1] = (object) objekt.unitNameField;
                        dataTable2.Rows.Add(row2);
                        DataRow row3 = dataTable2.NewRow();
                        row3[0] = (object) "Тип на подружница";
                        row3[1] = (object) objekt.unitTypeDescrField;
                        dataTable2.Rows.Add(row3);
                        DataRow row4 = dataTable2.NewRow();
                        row4[0] = (object) "Опис на подружница";
                        row4[1] = (object) objekt.unitDescrField;
                        dataTable2.Rows.Add(row4);
                        DataRow row5 = dataTable2.NewRow();
                        row5[0] = (object) "Останати информации";
                        row5[1] = (object) objekt.otherInfoField;
                        dataTable2.Rows.Add(row5);
                        DataRow row6 = dataTable2.NewRow();
                        row6[0] = (object) "Код на земја";
                        row6[1] = (object) objekt.countryCodeField;
                        dataTable2.Rows.Add(row6);
                        DataRow row7 = dataTable2.NewRow();
                        row7[0] = (object) "Адреса";
                        row7[1] = (object) (objekt.streetField + " " + objekt.houseNoField + " " + objekt.entranceNoField + " " + objekt.flatNoField + " " + objekt.municipalityField + " " + objekt.placeField);
                        dataTable2.Rows.Add(row7);
                        DataRow row8 = dataTable2.NewRow();
                        row8[0] = (object) "Код на претежна дејност";
                        row8[1] = (object) objekt.activityCodeField;
                        dataTable2.Rows.Add(row8);
                        DataRow row9 = dataTable2.NewRow();
                        row9[0] = (object) "Опис на претежна дејност";
                        row9[1] = (object) objekt.activityDescField;
                        dataTable2.Rows.Add(row9);
                        if (akteri != null)
                        {
                          DataRow row10 = dataTable2.NewRow();
                          row10[0] = (object) "Овластени лица на подружницата";
                          row10[1] = (object) "";
                          dataTable2.Rows.Add(row10);
                          DataRow row11 = dataTable2.NewRow();
                          row11[0] = (object) "Број на подружница";
                          row11[1] = (object) akteri.zapisPodroznica;
                          dataTable2.Rows.Add(row11);
                          DataRow row12 = dataTable2.NewRow();
                          row12[0] = (object) "Тип на овластено лице";
                          row12[1] = (object) akteri.zapis4;
                          dataTable2.Rows.Add(row12);
                          DataRow row13 = dataTable2.NewRow();
                          row13[0] = (object) "Матичен број на актер";
                          row13[1] = (object) akteri.zapis5;
                          dataTable2.Rows.Add(row13);
                          DataRow row14 = dataTable2.NewRow();
                          row14[0] = (object) "Тип на актер";
                          row14[1] = (object) akteri.zapis7;
                          dataTable2.Rows.Add(row14);
                          DataRow row15 = dataTable2.NewRow();
                          row15[0] = (object) "Име";
                          row15[1] = (object) akteri.zapis8;
                          dataTable2.Rows.Add(row15);
                          DataRow row16 = dataTable2.NewRow();
                          row16[0] = (object) "Презиме";
                          row16[1] = (object) akteri.zapis9;
                          dataTable2.Rows.Add(row16);
                          DataRow row17 = dataTable2.NewRow();
                          row17[0] = (object) "Код на земја";
                          row17[1] = (object) akteri.zapis10;
                          dataTable2.Rows.Add(row17);
                          DataRow row18 = dataTable2.NewRow();
                          row18[0] = (object) "Адреса";
                          row18[1] = (object) (akteri.zapis13 + " " + akteri.zapis14 + " " + akteri.zapis15 + " " + akteri.zapis16 + " " + akteri.zapis11 + " " + akteri.zapis12);
                          dataTable2.Rows.Add(row18);
                          DataRow row19 = dataTable2.NewRow();
                          row19[0] = (object) "Електронско сандаче";
                          row19[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row19);
                          DataRow row20 = dataTable2.NewRow();
                          row20[0] = (object) "Овластувања";
                          row20[1] = (object) akteri.zapis18;
                          dataTable2.Rows.Add(row20);
                          DataRow row21 = dataTable2.NewRow();
                          row21[0] = (object) "Ограничувања";
                          row21[1] = (object) akteri.zapis19;
                          dataTable2.Rows.Add(row21);
                          DataRow row22 = dataTable2.NewRow();
                          row22[0] = (object) "Тип на овластување";
                          row22[1] = (object) akteri.zapis21;
                          dataTable2.Rows.Add(row22);
                        }
                        gridView2.DataSource = (object) dataTable2;
                        gridView2.DataBind();
                        list3 = (List<GridView>) this.Session["GridViewList2"];
                        this.Session["GridViewList2"] = (object) new List<GridView>()
                        {
                          gridView2
                        };
                      }
                      else if (dataTable2 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Број на подружница";
                        row1[1] = (object) objekt.unitNoField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Назив на подружница";
                        row2[1] = (object) objekt.unitNameField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Тип на подружница";
                        row3[1] = (object) objekt.unitTypeDescrField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Опис на подружница";
                        row4[1] = (object) objekt.unitDescrField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Останати информации";
                        row5[1] = (object) objekt.otherInfoField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Код на земја";
                        row6[1] = (object) objekt.countryCodeField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Адреса";
                        row7[1] = (object) (objekt.streetField + " " + objekt.houseNoField + " " + objekt.entranceNoField + " " + objekt.flatNoField + " " + objekt.municipalityField + " " + objekt.placeField);
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Код на претежна дејност";
                        row8[1] = (object) objekt.activityCodeField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Опис на претежна дејност";
                        row9[1] = (object) objekt.activityDescField;
                        dataTable9.Rows.Add(row9);
                        if (akteri != null)
                        {
                          DataRow row10 = dataTable9.NewRow();
                          row10[0] = (object) "Овластени лица на подружницата";
                          row10[1] = (object) "";
                          dataTable9.Rows.Add(row10);
                          DataRow row11 = dataTable9.NewRow();
                          row11[0] = (object) "Број на подружница";
                          row11[1] = (object) akteri.zapisPodroznica;
                          dataTable9.Rows.Add(row11);
                          DataRow row12 = dataTable9.NewRow();
                          row12[0] = (object) "Тип на овластено лице";
                          row12[1] = (object) akteri.zapis4;
                          dataTable9.Rows.Add(row12);
                          DataRow row13 = dataTable9.NewRow();
                          row13[0] = (object) "Матичен број на актер";
                          row13[1] = (object) akteri.zapis5;
                          dataTable9.Rows.Add(row13);
                          DataRow row14 = dataTable9.NewRow();
                          row14[0] = (object) "Тип на актер";
                          row14[1] = (object) akteri.zapis7;
                          dataTable9.Rows.Add(row14);
                          DataRow row15 = dataTable9.NewRow();
                          row15[0] = (object) "Име";
                          row15[1] = (object) akteri.zapis8;
                          dataTable9.Rows.Add(row15);
                          DataRow row16 = dataTable9.NewRow();
                          row16[0] = (object) "Презиме";
                          row16[1] = (object) akteri.zapis9;
                          dataTable9.Rows.Add(row16);
                          DataRow row17 = dataTable9.NewRow();
                          row17[0] = (object) "Код на земја";
                          row17[1] = (object) akteri.zapis10;
                          dataTable9.Rows.Add(row17);
                          DataRow row18 = dataTable9.NewRow();
                          row18[0] = (object) "Адреса";
                          row18[1] = (object) (akteri.zapis13 + " " + akteri.zapis14 + " " + akteri.zapis15 + " " + akteri.zapis16 + " " + akteri.zapis11 + " " + akteri.zapis12);
                          dataTable9.Rows.Add(row18);
                          DataRow row19 = dataTable2.NewRow();
                          row19[0] = (object) "Електронско сандаче";
                          row19[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row19);
                          DataRow row20 = dataTable9.NewRow();
                          row20[0] = (object) "Овластувања";
                          row20[1] = (object) akteri.zapis18;
                          dataTable9.Rows.Add(row20);
                          DataRow row21 = dataTable9.NewRow();
                          row21[0] = (object) "Ограничувања";
                          row21[1] = (object) akteri.zapis19;
                          dataTable9.Rows.Add(row21);
                          DataRow row22 = dataTable9.NewRow();
                          row22[0] = (object) "Тип на овластување";
                          row22[1] = (object) akteri.zapis21;
                          dataTable9.Rows.Add(row22);
                        }
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList2"];
                        list4.Add(gridView9);
                        this.Session["GridViewList2"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5].templateNameField && (objekt.lEIDField != null || objekt.activityDescField != null) && objekt.activityCodeField != null)
                    {
                      if (dataTable5 == null)
                      {
                        dataTable5 = new DataTable();
                        gridView5.CssClass = "GridViewStyle";
                        gridView5.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView5.RowStyle.CssClass = "RowStyle";
                        gridView5.ControlStyle.Font.Bold = true;
                        gridView5.HeaderStyle.CssClass = "HeaderStyle";
                        gridView5.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView5.ID = "GridViewListTemplate5";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Код на дејност");
                        DataColumn column3 = new DataColumn("Опис на дејност");
                        dataTable5.Columns.Add(column1);
                        dataTable5.Columns.Add(column2);
                        dataTable5.Columns.Add(column3);
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                      else if (dataTable5 != null)
                      {
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6].templateNameField && objekt.lEIDField != null && (objekt.memberOfField != null || objekt.membersField != null))
                    {
                      if (dataTable6 == null)
                      {
                        dataTable6 = new DataTable();
                        gridView6.CssClass = "GridViewStyle";
                        gridView6.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView6.RowStyle.CssClass = "RowStyle";
                        gridView6.HeaderStyle.CssClass = "HeaderStyle";
                        gridView6.ControlStyle.Font.Bold = true;
                        gridView6.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView6.ID = "GridViewListTemplate6";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Матичен број на правно лице членка на синдикат");
                        DataColumn column3 = new DataColumn("Матичен број на правно лице кое пристапува како членка на синдикат");
                        dataTable6.Columns.Add(column1);
                        dataTable6.Columns.Add(column2);
                        dataTable6.Columns.Add(column3);
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                      else if (dataTable6 != null)
                      {
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[7].templateNameField && (objekt.lEIDField != null || objekt.decisionDateField != null || (objekt.validityDateField != null || objekt.validityHourField != null) || (objekt.stageIDField != null || objekt.stageDescField != null || (objekt.descriptionField != null || objekt.bankruptcyCourtNameField != null)) || (objekt.typeIDField != null || objekt.typeDescField != null)) && objekt.courtJournalIDField != null)
                    {
                      if (dataTable8 == null)
                      {
                        dataTable8 = new DataTable();
                        gridView8.CssClass = "GridViewStyle";
                        gridView8.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView8.RowStyle.CssClass = "RowStyle";
                        gridView8.ControlStyle.Font.Bold = true;
                        gridView8.HeaderStyle.CssClass = "HeaderStyle";
                        gridView8.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView8.ID = "GridViewListTemplate8";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Број на судска постапка";
                        row2[1] = (object) objekt.courtJournalIDField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Датум на одлука";
                        row3[1] = (object) objekt.decisionDateField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Датум на отварање на постапката";
                        row4[1] = (object) objekt.validityDateField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Време на отварање на постапката";
                        row5[1] = (object) objekt.validityHourField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Статус на постапката";
                        row6[1] = (object) objekt.stageIDField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Опис на статусот на постапката";
                        row7[1] = (object) objekt.stageDescField;
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Опис на постапката";
                        row8[1] = (object) objekt.descriptionField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Име на стечаен суд";
                        row9[1] = (object) objekt.bankruptcyCourtNameField;
                        dataTable8.Rows.Add(row9);
                        DataRow row10 = dataTable8.NewRow();
                        row10[0] = (object) "Тип на постапката";
                        row10[1] = (object) objekt.typeIDField;
                        dataTable8.Rows.Add(row10);
                        DataRow row11 = dataTable8.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) objekt.typeDescField;
                        dataTable8.Rows.Add(row11);
                        gridView8.DataSource = (object) dataTable8;
                        gridView8.DataBind();
                        list3 = (List<GridView>) this.Session["GridViewList8"];
                        this.Session["GridViewList8"] = (object) new List<GridView>()
                        {
                          gridView8
                        };
                      }
                      else if (dataTable8 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Број на судска постапка";
                        row2[1] = (object) objekt.courtJournalIDField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Датум на одлука";
                        row3[1] = (object) objekt.decisionDateField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Датум на отварање на постапката";
                        row4[1] = (object) objekt.validityDateField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Време на отварање на постапката";
                        row5[1] = (object) objekt.validityHourField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Статус на постапката";
                        row6[1] = (object) objekt.stageIDField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Опис на статусот на постапката";
                        row7[1] = (object) objekt.stageDescField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Опис на постапката";
                        row8[1] = (object) objekt.descriptionField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Име на стечаен суд";
                        row9[1] = (object) objekt.bankruptcyCourtNameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Тип на постапката";
                        row10[1] = (object) objekt.typeIDField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) objekt.typeDescField;
                        dataTable9.Rows.Add(row11);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList8"];
                        list4.Add(gridView9);
                        this.Session["GridViewList8"] = (object) list4;
                      }
                    }
                  }
                }
              }
              if (list2.Count > 0)
              {
                List<Akteri> list3 = new List<Akteri>();
                list3.AddRange((IEnumerable<Akteri>) Enumerable.ToList<Akteri>(Enumerable.Where<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == "0"))));
                List<Akteri> list4 = Enumerable.ToList<Akteri>((IEnumerable<Akteri>) Enumerable.OrderBy<Akteri, string>((IEnumerable<Akteri>) list3, (Func<Akteri, string>) (p => p.zapisTipAkter)));
                for (int index = 0; index < list4.Count; ++index)
                {
                  DataTable dataTable9 = new DataTable();
                  GridView gridView9 = new GridView();
                  gridView9.CssClass = "GridViewStyle";
                  gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                  gridView9.RowStyle.CssClass = "RowStyle";
                  gridView9.HeaderStyle.CssClass = "HeaderStyle";
                  gridView9.ControlStyle.Font.Bold = true;
                  gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                  DataColumn column1 = new DataColumn(list4[index].zapis7);
                  DataColumn column2 = new DataColumn(" ");
                  dataTable9.Columns.Add(column1);
                  dataTable9.Columns.Add(column2);
                  Akteri akteri = new Akteri();
                  DataRow row1 = dataTable9.NewRow();
                  row1[0] = (object) "Тип на овластено лице";
                  row1[1] = (object) list4[index].zapis4;
                  dataTable9.Rows.Add(row1);
                  DataRow row2 = dataTable9.NewRow();
                  row2[0] = (object) "Матичен број на актер";
                  row2[1] = (object) list4[index].zapis5;
                  dataTable9.Rows.Add(row2);
                  DataRow row3 = dataTable9.NewRow();
                  row3[0] = (object) "Тип на актер";
                  row3[1] = (object) list4[index].zapis7;
                  dataTable9.Rows.Add(row3);
                  DataRow row4 = dataTable9.NewRow();
                  row4[0] = (object) "Име";
                  row4[1] = (object) list4[index].zapis8;
                  dataTable9.Rows.Add(row4);
                  DataRow row5 = dataTable9.NewRow();
                  row5[0] = (object) "Презиме";
                  row5[1] = (object) list4[index].zapis9;
                  dataTable9.Rows.Add(row5);
                  DataRow row6 = dataTable9.NewRow();
                  row6[0] = (object) "Код на земја";
                  row6[1] = (object) list4[index].zapis10;
                  dataTable9.Rows.Add(row6);
                  DataRow row7 = dataTable9.NewRow();
                  row7[0] = (object) "Адреса";
                  row7[1] = (object) (list4[index].zapis13 + " " + list4[index].zapis14 + " " + list4[index].zapis15 + " " + list4[index].zapis16 + " " + list4[index].zapis11 + " " + list4[index].zapis12);
                  dataTable9.Rows.Add(row7);
                  DataRow row8 = dataTable9.NewRow();
                  row8[0] = (object) "Електронско сандаче";
                  row8[1] = (object) list4[index].zapis17;
                  dataTable9.Rows.Add(row8);
                  DataRow row9 = dataTable9.NewRow();
                  row9[0] = (object) "Овластувања";
                  row9[1] = (object) list4[index].zapis18;
                  dataTable9.Rows.Add(row9);
                  DataRow row10 = dataTable9.NewRow();
                  row10[0] = (object) "Ограничувања";
                  row10[1] = (object) list4[index].zapis19;
                  dataTable9.Rows.Add(row10);
                  DataRow row11 = dataTable9.NewRow();
                  row11[0] = (object) "Тип на овластување";
                  row11[1] = (object) list4[index].zapis21;
                  dataTable9.Rows.Add(row11);
                  gridView9.DataSource = (object) dataTable9;
                  gridView9.DataBind();
                  if (index == 0)
                  {
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                    {
                      Text = "Податоци за актери"
                    });
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                  }
                  else
                  {
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                  }
                  this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                  List<GridView> list5 = (List<GridView>) this.Session["GridViewList3"] ?? new List<GridView>();
                  list5.Add(gridView9);
                  this.Session["GridViewList3"] = (object) list5;
                }
              }
              this.ImageButtonCR_UJP.Visible = true;
              if ((GridView) this.Session["GridViewList1"] != null)
                list1.Add("Податоци за субјетот");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList2"] != null)
              {
                List<GridView> list3 = (List<GridView>) this.Session["GridViewList2"];
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за подружници"
                });
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) list3[0]);
                if (list3.Count > 1)
                {
                  for (int index = 1; index < list3.Count; ++index)
                  {
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) list3[index]);
                  }
                }
                list1.Add("Податоци за подружници");
              }
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList3"] != null)
                list1.Add("Податоци за актери");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList4"] != null)
                list1.Add("Податоци за сопственици");
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList5"] != null)
              {
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за активности"
                });
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) this.Session["GridViewList5"]);
                list1.Add("Податоци за активности");
              }
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList6"] != null)
              {
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за членство во синдикат"
                });
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) this.Session["GridViewList6"]);
                list1.Add("Податоци за членство во синдикат");
              }
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList7"] != null)
                list1.Add("Податоци за основање");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList8"] != null)
              {
                List<GridView> list3 = (List<GridView>) this.Session["GridViewList8"];
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                {
                  Text = "Податоци за судски процеси"
                });
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) list3[0]);
                if (list3.Count > 1)
                {
                  for (int index = 1; index < list3.Count; ++index)
                  {
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                    this.WSResponseControlPanelCR_UJP.Controls.Add((Control) list3[index]);
                  }
                }
                list1.Add("Податоци за судски процеси");
              }
              else
                list1.Add((string) null);
              this.Session["ParametersPrint"] = (object) ("Единствен Матичен Број На Субјектот За УЈП_" + EMBS.EMBS1);
              this.Session["ListaRezultHeaders"] = (object) list1;
            }
            this.Completed = true;
            reqrespPortClient.Close();
          }
          catch (Exception ex)
          {
            if (num == 3)
            {
              try
              {
                  FaultMessageUJPClass faultMessageUjp;
                using (StringReader stringReader = new StringReader(ex.Message))
                    faultMessageUjp = (FaultMessageUJPClass)new XmlSerializer(typeof(FaultMessageUJPClass)).Deserialize((TextReader)stringReader);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = faultMessageUjp.Error;
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_UJP.Visible = false;
              }
              catch
              {
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_UJP.Visible = false;
              }
            }
          }
        }
      }
      else
      {
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
        Label label1 = new Label();
        label1.ID = "lblporaka";
        label1.Text = "Порака: ";
        Label label2 = new Label();
        label2.ID = "lblporakavalue";
        label2.Text = "Не се пронајдени податоци за параметарот по кој пребарувате!";
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
        this.ImageButtonCR_UJP.Visible = false;
      }
    }

    private void GetEMBSforUJPOld()
    {
      this.Completed = false;
      string s = this.Session["IsMBS"] == null || !Convert.ToBoolean(this.Session["IsMBS"]) ? ((TextBox) this.ControlParams.FindControl("txt0")).Text.Trim() : this.Session["MBS"].ToString();
      double result;
      if (double.TryParse(s, out result) && result.ToString().Length == 7)
      {
        this.Session["GridViewList1"] = (object) null;
        this.Session["GridViewList2"] = (object) null;
        this.Session["GridViewList3"] = (object) null;
        this.Session["GridViewList4"] = (object) null;
        this.Session["GridViewList5"] = (object) null;
        this.Session["GridViewList6"] = (object) null;
        this.Session["GridViewList7"] = (object) null;
        this.Session["GridViewList8"] = (object) null;
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://crbiztalkwcfserviceapplicationujp.interop.local/CRBizTalkWCFServiceApplicationUJP/Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient reqrespPortClient = new Module_1_CRBizTalkOrchestrationUJP_RECEIVE_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
        interop.WebServiceCR_UJP.EMBS EMBS = new interop.WebServiceCR_UJP.EMBS();
        EMBS.Username = user.username;
        EMBS.Password = user.password;
        EMBS.EMBS1 = s;
        EMBS.EMBS2 = s;
        EMBS.EMBS3 = s;
        EMBS.EMBS4 = s;
        EMBS.EMBS5 = s;
        EMBS.EMBS6 = s;
        EMBS.EMBS7 = s;
        EMBS.EMBS8 = s;
        EMBS.NacinNaIsprakjanje = "EORPD";
        EMBS.OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text;
        EMBS.TimeStamp = DateTime.Now.ToString();
        int num = 1;
        while (num <= 3 && !this.Completed)
        {
          ++num;
          try
          {
            reqrespPortClient.Open();
            VratiCRMRezultatiUJPResponse dataByEmbs = reqrespPortClient.GetDataByEMBS(EMBS);
            List<string> list1 = new List<string>();
            if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Не е пронајден запис за барањето!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Не е пронајден запис за барањето!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Невалиден сертификат на серверот за комуникација со ЦР!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Невалиден сертификат на серверот за комуникација со ЦР!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else if (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0] != null && (dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField != null && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0] != null) && dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].crmResultItemField[0].entranceNoField == "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!")
            {
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
              Label label1 = new Label();
              label1.ID = "lblporaka";
              label1.Text = "Порака: ";
              Label label2 = new Label();
              label2.ID = "lblporakavalue";
              label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
              this.ImageButtonCR_UJP.Visible = false;
            }
            else
            {
              Label label1 = new Label();
              label1.Text = "Матичен број на правно лице: ";
              Label label2 = new Label();
              label2.Text = s;
              label2.Attributes.Add("style", "font-weight: bold;");
              LiteralControl literalControl1 = new LiteralControl("<p>");
              LiteralControl literalControl2 = new LiteralControl("</p>");
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl2);
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<br></br>"));
              this.WSResponseControlPanelCR_UJP.Controls.Add((Control) literalControl1);
              GridView gridView1 = new GridView();
              GridView gridView2 = new GridView();
              GridView gridView3 = new GridView();
              GridView gridView4 = new GridView();
              GridView gridView5 = new GridView();
              GridView gridView6 = new GridView();
              GridView gridView7 = new GridView();
              GridView gridView8 = new GridView();
              DataTable dataTable1 = (DataTable) null;
              DataTable dataTable2 = (DataTable) null;
              DataTable dataTable3 = (DataTable) null;
              DataTable dataTable4 = (DataTable) null;
              DataTable dataTable5 = (DataTable) null;
              DataTable dataTable6 = (DataTable) null;
              DataTable dataTable7 = (DataTable) null;
              DataTable dataTable8 = (DataTable) null;
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems1 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems2 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems3 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems4 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems5 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5];
              interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems6 = dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6];
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1] = responseCrmResultItems6;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2] = responseCrmResultItems3;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3] = responseCrmResultItems2;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4] = responseCrmResultItems1;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5] = responseCrmResultItems4;
              dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6] = responseCrmResultItems5;
              List<Akteri> list2 = new List<Akteri>();
              foreach (interop.WebServiceCR_UJP.CrmResponseCrmResultItems responseCrmResultItems7 in dataByEmbs.VratiCRMRezultatiUJPResult.itemsField)
              {
                string templateNameField = responseCrmResultItems7.templateNameField;
                foreach (interop.WebServiceCR_UJP.CrmResponseCrmResultItemsCrmResultItem itemsCrmResultItem in responseCrmResultItems7.crmResultItemField)
                {
                  interop.WebServiceCR_UJP.CrmResponseCrmResultItemsCrmResultItem objekt = itemsCrmResultItem;
                  if (objekt.lEIDField != null)
                  {
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[0].templateNameField && (objekt.isLETerminatedField != null || objekt.lEIDField != null || (objekt.lEFullNameField != null || objekt.shortNameField != null) || (objekt.terminationTypeIDField != null || objekt.terminationTypeDescField != null || (objekt.lETypeIDField != null || objekt.lETypeDescField != null)) || (objekt.lESizeIDField != null || objekt.lESizeDescField != null || (objekt.municipalityField != null || objekt.municipalityCodeField != null) || (objekt.placeField != null || objekt.placeCodeField != null || (objekt.streetField != null || objekt.streetCodeField != null))) || (objekt.houseNoField != null || objekt.entranceNoField != null || (objekt.flatNoField != null || objekt.organisationTypeCodeField != null) || (objekt.organisationTypeDescField != null || objekt.registerCategoryIDField != null || (objekt.registerCategoryField != null || objekt.authorisedRegisterIDField != null)) || (objekt.authorisedRegisterField != null || objekt.ownershipTypeIDField != null || (objekt.ownershipTypeDescField != null || objekt.isForeignActField != null) || (objekt.isActivityNoLicenceField != null || objekt.coreActivityCodeField != null || (objekt.coreActivityDescField != null || objekt.emailField != null)))) || (objekt.foreignActivityField != null || objekt.additionalInfoField != null || (objekt.actingPeriodField != null || objekt.isDataConfirmedField != null) || objekt.isAAActiveField != null)) && objekt.taxPayerNumberField != null)
                    {
                      if (dataTable1 == null)
                      {
                        gridView1.CssClass = "GridViewStyle";
                        gridView1.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView1.RowStyle.CssClass = "RowStyle";
                        gridView1.HeaderStyle.CssClass = "HeaderStyle";
                        gridView1.ControlStyle.Font.Bold = true;
                        gridView1.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView1.ID = "GridViewListTemplate1";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable1 = new DataTable();
                        dataTable1.Columns.Add(column1);
                        dataTable1.Columns.Add(column2);
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Кратко име";
                        row5[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Причина за престанок";
                        row6[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Опис на престанокот";
                        row7[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Вид на субјект";
                        row8[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на вид на субјект";
                        row9[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Големина на субјект";
                        row10[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на големина на субјект";
                        row11[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Код на општина";
                        row13[1] = (object) objekt.municipalityCodeField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Место";
                        row14[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Код на место";
                        row15[1] = (object) objekt.placeCodeField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Улица";
                        row16[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Код на Улица";
                        row17[1] = (object) objekt.streetCodeField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Број на куќа";
                        row18[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Број на влез";
                        row19[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Број на стан";
                        row20[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Код на организационен облик";
                        row21[1] = (object) objekt.organisationTypeCodeField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Опис на организационен облик";
                        row22[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Регистар";
                        row23[1] = (object) objekt.registerCategoryIDField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Опис на регистарот";
                        row24[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Надлежен регистар";
                        row25[1] = (object) objekt.authorisationTypeIDField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Опис на надлежен регистар";
                        row26[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Вид на споственост";
                        row27[1] = (object) objekt.ownershipTypeIDField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Опис на вид на сопственост";
                        row28[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Евидентирани дејности во надворешен промет";
                        row29[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Општа клаузула за бизнис";
                        row30[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row30);
                        DataRow row31 = dataTable1.NewRow();
                        row31[0] = (object) "Код на претежна дејност";
                        row31[1] = (object) objekt.coreActivityCodeField;
                        dataTable1.Rows.Add(row31);
                        DataRow row32 = dataTable1.NewRow();
                        row32[0] = (object) "Опис на претежна дејност";
                        row32[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row32);
                        DataRow row33 = dataTable1.NewRow();
                        row33[0] = (object) "Е-пошта";
                        row33[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row33);
                        DataRow row34 = dataTable1.NewRow();
                        row34[0] = (object) "Други дејности";
                        row34[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row34);
                        DataRow row35 = dataTable1.NewRow();
                        row35[0] = (object) "Дополнителни инфо.";
                        row35[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row35);
                        DataRow row36 = dataTable1.NewRow();
                        row36[0] = (object) "Времетраење";
                        row36[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row36);
                        DataRow row37 = dataTable1.NewRow();
                        row37[0] = (object) "Потврдени податоци";
                        row37[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row37);
                        DataRow row38 = dataTable1.NewRow();
                        row38[0] = (object) "Активност од регистар на г.с.";
                        row38[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row38[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row38[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row38[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row38);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за субјетот"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                      else if (dataTable1 != null)
                      {
                        DataRow row1 = dataTable1.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable1.Rows.Add(row1);
                        DataRow row2 = dataTable1.NewRow();
                        row2[0] = (object) "Терминиран";
                        row2[1] = (object) objekt.isLETerminatedField;
                        dataTable1.Rows.Add(row2);
                        DataRow row3 = dataTable1.NewRow();
                        row3[0] = (object) "Целосен назив на правно лице";
                        row3[1] = (object) objekt.lEFullNameField;
                        dataTable1.Rows.Add(row3);
                        DataRow row4 = dataTable1.NewRow();
                        row4[0] = (object) "Даночен број";
                        row4[1] = (object) objekt.taxPayerNumberField;
                        dataTable1.Rows.Add(row4);
                        DataRow row5 = dataTable1.NewRow();
                        row5[0] = (object) "Кратко име";
                        row5[1] = (object) objekt.shortNameField;
                        dataTable1.Rows.Add(row5);
                        DataRow row6 = dataTable1.NewRow();
                        row6[0] = (object) "Причина за престанок";
                        row6[1] = (object) objekt.terminationTypeIDField;
                        dataTable1.Rows.Add(row6);
                        DataRow row7 = dataTable1.NewRow();
                        row7[0] = (object) "Опис на престанокот";
                        row7[1] = (object) objekt.terminationTypeDescField;
                        dataTable1.Rows.Add(row7);
                        DataRow row8 = dataTable1.NewRow();
                        row8[0] = (object) "Вид на субјект";
                        row8[1] = (object) objekt.lETypeIDField;
                        dataTable1.Rows.Add(row8);
                        DataRow row9 = dataTable1.NewRow();
                        row9[0] = (object) "Опис на вид на субјект";
                        row9[1] = (object) objekt.lETypeDescField;
                        dataTable1.Rows.Add(row9);
                        DataRow row10 = dataTable1.NewRow();
                        row10[0] = (object) "Големина на субјект";
                        row10[1] = (object) objekt.lESizeIDField;
                        dataTable1.Rows.Add(row10);
                        DataRow row11 = dataTable1.NewRow();
                        row11[0] = (object) "Опис на големина на субјект";
                        row11[1] = (object) objekt.lESizeDescField;
                        dataTable1.Rows.Add(row11);
                        DataRow row12 = dataTable1.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable1.Rows.Add(row12);
                        DataRow row13 = dataTable1.NewRow();
                        row13[0] = (object) "Код на општина";
                        row13[1] = (object) objekt.municipalityCodeField;
                        dataTable1.Rows.Add(row13);
                        DataRow row14 = dataTable1.NewRow();
                        row14[0] = (object) "Место";
                        row14[1] = (object) objekt.placeField;
                        dataTable1.Rows.Add(row14);
                        DataRow row15 = dataTable1.NewRow();
                        row15[0] = (object) "Код на место";
                        row15[1] = (object) objekt.placeCodeField;
                        dataTable1.Rows.Add(row15);
                        DataRow row16 = dataTable1.NewRow();
                        row16[0] = (object) "Улица";
                        row16[1] = (object) objekt.streetField;
                        dataTable1.Rows.Add(row16);
                        DataRow row17 = dataTable1.NewRow();
                        row17[0] = (object) "Код на Улица";
                        row17[1] = (object) objekt.streetCodeField;
                        dataTable1.Rows.Add(row17);
                        DataRow row18 = dataTable1.NewRow();
                        row18[0] = (object) "Број на куќа";
                        row18[1] = (object) objekt.houseNoField;
                        dataTable1.Rows.Add(row18);
                        DataRow row19 = dataTable1.NewRow();
                        row19[0] = (object) "Број на влез";
                        row19[1] = (object) objekt.entranceNoField;
                        dataTable1.Rows.Add(row19);
                        DataRow row20 = dataTable1.NewRow();
                        row20[0] = (object) "Број на стан";
                        row20[1] = (object) objekt.flatNoField;
                        dataTable1.Rows.Add(row20);
                        DataRow row21 = dataTable1.NewRow();
                        row21[0] = (object) "Код на организационен облик";
                        row21[1] = (object) objekt.organisationTypeCodeField;
                        dataTable1.Rows.Add(row21);
                        DataRow row22 = dataTable1.NewRow();
                        row22[0] = (object) "Опис на организационен облик";
                        row22[1] = (object) objekt.organisationTypeDescField;
                        dataTable1.Rows.Add(row22);
                        DataRow row23 = dataTable1.NewRow();
                        row23[0] = (object) "Регистар";
                        row23[1] = (object) objekt.registerCategoryIDField;
                        dataTable1.Rows.Add(row23);
                        DataRow row24 = dataTable1.NewRow();
                        row24[0] = (object) "Опис на регистарот";
                        row24[1] = (object) objekt.registerCategoryField;
                        dataTable1.Rows.Add(row24);
                        DataRow row25 = dataTable1.NewRow();
                        row25[0] = (object) "Надлежен регистар";
                        row25[1] = (object) objekt.authorisationTypeIDField;
                        dataTable1.Rows.Add(row25);
                        DataRow row26 = dataTable1.NewRow();
                        row26[0] = (object) "Опис на надлежен регистар";
                        row26[1] = (object) objekt.authorisedRegisterField;
                        dataTable1.Rows.Add(row26);
                        DataRow row27 = dataTable1.NewRow();
                        row27[0] = (object) "Вид на споственост";
                        row27[1] = (object) objekt.ownershipTypeIDField;
                        dataTable1.Rows.Add(row27);
                        DataRow row28 = dataTable1.NewRow();
                        row28[0] = (object) "Опис на вид на сопственост";
                        row28[1] = (object) objekt.ownershipTypeDescField;
                        dataTable1.Rows.Add(row28);
                        DataRow row29 = dataTable1.NewRow();
                        row29[0] = (object) "Евидентирани дејности во надворешен промет";
                        row29[1] = (object) objekt.isForeignActField;
                        dataTable1.Rows.Add(row29);
                        DataRow row30 = dataTable1.NewRow();
                        row30[0] = (object) "Општа клаузула за бизнис";
                        row30[1] = (object) objekt.isActivityNoLicenceField;
                        dataTable1.Rows.Add(row30);
                        DataRow row31 = dataTable1.NewRow();
                        row31[0] = (object) "Код на претежна дејност";
                        row31[1] = (object) objekt.coreActivityCodeField;
                        dataTable1.Rows.Add(row31);
                        DataRow row32 = dataTable1.NewRow();
                        row32[0] = (object) "Опис на претежна дејност";
                        row32[1] = (object) objekt.coreActivityDescField;
                        dataTable1.Rows.Add(row32);
                        DataRow row33 = dataTable1.NewRow();
                        row33[0] = (object) "Е-пошта";
                        row33[1] = (object) objekt.emailField;
                        dataTable1.Rows.Add(row33);
                        DataRow row34 = dataTable1.NewRow();
                        row34[0] = (object) "Други дејности";
                        row34[1] = (object) objekt.foreignActivityField;
                        dataTable1.Rows.Add(row34);
                        DataRow row35 = dataTable1.NewRow();
                        row35[0] = (object) "Дополнителни инфо.";
                        row35[1] = (object) objekt.additionalInfoField;
                        dataTable1.Rows.Add(row35);
                        DataRow row36 = dataTable1.NewRow();
                        row36[0] = (object) "Времетраење";
                        row36[1] = (object) objekt.actingPeriodField;
                        dataTable1.Rows.Add(row36);
                        DataRow row37 = dataTable1.NewRow();
                        row37[0] = (object) "Потврдени податоци";
                        row37[1] = (object) objekt.isDataConfirmedField;
                        dataTable1.Rows.Add(row37);
                        DataRow row38 = dataTable1.NewRow();
                        row38[0] = (object) "Активност од регистар на г.с.";
                        row38[1] = (object) objekt.isAAActiveField;
                        try
                        {
                          if (objekt.isAAActiveField == "0")
                            row38[1] = (object) "Неактивен";
                          if (objekt.isAAActiveField == "1")
                            row38[1] = (object) "Активен";
                          if (objekt.isAAActiveField == "2")
                            row38[1] = (object) "Во постапка на утврдување статус";
                        }
                        catch
                        {
                        }
                        dataTable1.Rows.Add(row38);
                        gridView1.DataSource = (object) dataTable1;
                        gridView1.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView1);
                        this.Session["GridViewList1"] = (object) gridView1;
                      }
                    }
                    List<GridView> list3;
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[1].templateNameField && (objekt.lEIDField != null || objekt.capitalOriginIDField != null || (objekt.capitalOriginDescField != null || objekt.fCCodeField != null) || (objekt.capitalFC_CashField != null || objekt.capitalFC_NonCashField != null || (objekt.capitalFC_PaydField != null || objekt.sharesTotalField != null)) || (objekt.sharesPaydField != null || objekt.typeOfSharesField != null || (objekt.sharesPaymentField != null || objekt.sharesPublishingField != null) || (objekt.licenceField != null || objekt.capitalFC_TotalField != null))) && objekt.foundingDateField != null)
                    {
                      if (dataTable7 == null)
                      {
                        dataTable7 = new DataTable();
                        gridView7.CssClass = "GridViewStyle";
                        gridView7.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView7.RowStyle.CssClass = "RowStyle";
                        gridView7.HeaderStyle.CssClass = "HeaderStyle";
                        gridView7.ControlStyle.Font.Bold = true;
                        gridView7.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView7.ID = "GridViewListTemplate7";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable7.Columns.Add(column1);
                        dataTable7.Columns.Add(column2);
                        DataRow row1 = dataTable7.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable7.Rows.Add(row1);
                        DataRow row2 = dataTable7.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable7.Rows.Add(row2);
                        DataRow row3 = dataTable7.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable7.Rows.Add(row3);
                        DataRow row4 = dataTable7.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable7.Rows.Add(row4);
                        DataRow row5 = dataTable7.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable7.Rows.Add(row5);
                        DataRow row6 = dataTable7.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable7.Rows.Add(row6);
                        DataRow row7 = dataTable7.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable7.Rows.Add(row7);
                        DataRow row8 = dataTable7.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable7.Rows.Add(row8);
                        DataRow row9 = dataTable7.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable7.Rows.Add(row9);
                        DataRow row10 = dataTable7.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable7.Rows.Add(row10);
                        DataRow row11 = dataTable7.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable7.Rows.Add(row11);
                        DataRow row12 = dataTable7.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable7.Rows.Add(row12);
                        DataRow row13 = dataTable7.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable7.Rows.Add(row13);
                        DataRow row14 = dataTable7.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable7.Rows.Add(row14);
                        DataRow row15 = dataTable7.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable7.Rows.Add(row15);
                        gridView7.DataSource = (object) dataTable7;
                        gridView7.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за основање"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView7);
                        list3 = (List<GridView>) this.Session["GridViewList7"];
                        this.Session["GridViewList7"] = (object) new List<GridView>()
                        {
                          gridView7
                        };
                      }
                      else if (dataTable7 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Датум на основање";
                        row2[1] = (object) objekt.foundingDateField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Потекло на капиталот";
                        row3[1] = (object) objekt.capitalOriginIDField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Опис на потекло на капиталот";
                        row4[1] = (object) objekt.capitalOriginDescField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Валута";
                        row5[1] = (object) objekt.fCCodeField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Паричен влог";
                        row6[1] = (object) objekt.capitalFC_CashField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Непаричен влог";
                        row7[1] = (object) objekt.capitalFC_NonCashField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Уплатен долг";
                        row8[1] = (object) objekt.capitalFC_PaydField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Број на издадени акции";
                        row9[1] = (object) objekt.sharesTotalField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Вкупен број на уплатени акции";
                        row10[1] = (object) objekt.sharesPaydField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Вид на акции";
                        row11[1] = (object) objekt.typeOfSharesField;
                        dataTable9.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Начин на плаќање";
                        row12[1] = (object) objekt.sharesPaymentField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Начин на објавување";
                        row13[1] = (object) objekt.sharesPublishingField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Лиценци";
                        row14[1] = (object) objekt.licenceField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Вкупно основна главина";
                        row15[1] = (object) objekt.capitalFC_TotalField;
                        dataTable9.Rows.Add(row15);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList7"];
                        list4.Add(gridView9);
                        this.Session["GridViewList7"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[2].templateNameField && (objekt.lEIDField != null || objekt.personOrLEIDField != null || (objekt.personOrLEDescField != null || objekt.ownerTypeIDField != null) || (objekt.ownerTypeDescField != null || objekt.liabilityIDField != null || (objekt.liabilityDescField != null || objekt.ownerNameField != null)) || (objekt.ownerSurnameField != null || objekt.countryCodeField != null || (objekt.municipalityField != null || objekt.placeField != null) || (objekt.streetField != null || objekt.houseNoField != null || (objekt.entranceNoField != null || objekt.flatNoField != null))) || (objekt.emailField != null || objekt.fCCodeField != null || (objekt.participationFC_CashField != null || objekt.participationFC_NonCashField != null) || (objekt.participationFC_PaydField != null || objekt.participationFC_TotalField != null || objekt.addInfo != null))) && objekt.ownerIDField != null)
                    {
                      if (dataTable4 == null)
                      {
                        dataTable4 = new DataTable();
                        gridView4.CssClass = "GridViewStyle";
                        gridView4.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView4.RowStyle.CssClass = "RowStyle";
                        gridView4.HeaderStyle.CssClass = "HeaderStyle";
                        gridView4.ControlStyle.Font.Bold = true;
                        gridView4.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView4.ID = "GridViewListTemplate4";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable4.Columns.Add(column1);
                        dataTable4.Columns.Add(column2);
                        DataRow row1 = dataTable4.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable4.Rows.Add(row1);
                        DataRow row2 = dataTable4.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable4.Rows.Add(row2);
                        DataRow row3 = dataTable4.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable4.Rows.Add(row3);
                        DataRow row4 = dataTable4.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable4.Rows.Add(row4);
                        DataRow row5 = dataTable4.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable4.Rows.Add(row5);
                        DataRow row6 = dataTable4.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable4.Rows.Add(row6);
                        DataRow row7 = dataTable4.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable4.Rows.Add(row7);
                        DataRow row8 = dataTable4.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable4.Rows.Add(row8);
                        DataRow row9 = dataTable4.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable4.Rows.Add(row9);
                        DataRow row10 = dataTable4.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable4.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable4.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable4.Rows.Add(row12);
                        DataRow row13 = dataTable4.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable4.Rows.Add(row13);
                        DataRow row14 = dataTable4.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable4.Rows.Add(row14);
                        DataRow row15 = dataTable4.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable4.Rows.Add(row15);
                        DataRow row16 = dataTable4.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable4.Rows.Add(row16);
                        DataRow row17 = dataTable4.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable4.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable4.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable4.Rows.Add(row19);
                        DataRow row20 = dataTable4.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable4.Rows.Add(row20);
                        DataRow row21 = dataTable4.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable4.Rows.Add(row21);
                        DataRow row22 = dataTable4.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable4.Rows.Add(row22);
                        DataRow row23 = dataTable4.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable4.Rows.Add(row23);
                        DataRow row24 = dataTable4.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable4.Rows.Add(row24);
                        gridView4.DataSource = (object) dataTable4;
                        gridView4.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за сопственици"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView4);
                        list3 = (List<GridView>) this.Session["GridViewList4"];
                        this.Session["GridViewList4"] = (object) new List<GridView>()
                        {
                          gridView4
                        };
                      }
                      else if (dataTable4 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Тип на сопственик";
                        row2[1] = (object) objekt.personOrLEIDField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Опис на тип на сопственик";
                        row3[1] = (object) objekt.personOrLEDescField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Матичен број на сопственик";
                        row4[1] = (object) objekt.ownerIDField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Тип на сопственик 2";
                        row5[1] = (object) objekt.ownerTypeIDField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Опис на тип на сопственик 2";
                        row6[1] = (object) objekt.ownerTypeDescField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Вид на одговорност";
                        row7[1] = (object) objekt.liabilityIDField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Опис на вид на одговорност";
                        row8[1] = (object) objekt.liabilityDescField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Име";
                        row9[1] = (object) objekt.ownerNameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Презиме";
                        row10[1] = (object) objekt.ownerSurnameField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable4.NewRow();
                        row11[0] = (object) "Код на земја";
                        row11[1] = (object) objekt.countryCodeField;
                        dataTable4.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Општина";
                        row12[1] = (object) objekt.municipalityField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Место";
                        row13[1] = (object) objekt.placeField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Улица";
                        row14[1] = (object) objekt.streetField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable9.Rows.Add(row15);
                        DataRow row16 = dataTable9.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable9.Rows.Add(row16);
                        DataRow row17 = dataTable9.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable9.Rows.Add(row17);
                        DataRow row18 = dataTable4.NewRow();
                        row18[0] = (object) "Електронско сандаче";
                        row18[1] = (object) objekt.emailField;
                        dataTable4.Rows.Add(row18);
                        DataRow row19 = dataTable9.NewRow();
                        row19[0] = (object) "Валута";
                        row19[1] = (object) objekt.fCCodeField;
                        dataTable9.Rows.Add(row19);
                        DataRow row20 = dataTable9.NewRow();
                        row20[0] = (object) "Паричен влог";
                        row20[1] = (object) objekt.participationFC_CashField;
                        dataTable9.Rows.Add(row20);
                        DataRow row21 = dataTable9.NewRow();
                        row21[0] = (object) "Непаричен влог";
                        row21[1] = (object) objekt.participationFC_NonCashField;
                        dataTable9.Rows.Add(row21);
                        DataRow row22 = dataTable9.NewRow();
                        row22[0] = (object) "Уплатен долг";
                        row22[1] = (object) objekt.participationFC_PaydField;
                        dataTable9.Rows.Add(row22);
                        DataRow row23 = dataTable9.NewRow();
                        row23[0] = (object) "Вкупен долг";
                        row23[1] = (object) objekt.participationFC_TotalField;
                        dataTable9.Rows.Add(row23);
                        DataRow row24 = dataTable9.NewRow();
                        row24[0] = (object) "Забелешка";
                        row24[1] = (object) objekt.addInfo;
                        dataTable9.Rows.Add(row24);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList4"];
                        list4.Add(gridView9);
                        this.Session["GridViewList4"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[3].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.personOrLEIDField != null || objekt.personOrLEDescField != null) || (objekt.actorTypeIDField != null || objekt.actorTypeDescField != null || (objekt.actorNameField != null || objekt.actorSurnameField != null)) || (objekt.countryCodeField != null || objekt.municipalityField != null || (objekt.placeField != null || objekt.streetField != null) || (objekt.houseNoField != null || objekt.flatNoField != null || (objekt.emailField != null || objekt.descriptionField != null))) || (objekt.restrictionsField != null || objekt.authorisationTypeIDField != null || objekt.authorisationTypeDescField != null)) && objekt.actorIDField != null)
                    {
                      if (dataTable3 == null)
                      {
                        dataTable3 = new DataTable();
                        gridView3.CssClass = "GridViewStyle";
                        gridView3.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView3.RowStyle.CssClass = "RowStyle";
                        gridView3.HeaderStyle.CssClass = "HeaderStyle";
                        gridView3.ControlStyle.Font.Bold = true;
                        gridView3.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView3.ID = "GridViewListTemplate3";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable3.Columns.Add(column1);
                        dataTable3.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable3.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable3.Rows.Add(row1);
                        DataRow row2 = dataTable3.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable3.Rows.Add(row2);
                        DataRow row3 = dataTable3.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable3.Rows.Add(row3);
                        DataRow row4 = dataTable3.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable3.Rows.Add(row4);
                        DataRow row5 = dataTable3.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable3.Rows.Add(row5);
                        DataRow row6 = dataTable3.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable3.Rows.Add(row6);
                        DataRow row7 = dataTable3.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable3.Rows.Add(row7);
                        DataRow row8 = dataTable3.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable3.Rows.Add(row8);
                        DataRow row9 = dataTable3.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable3.Rows.Add(row9);
                        DataRow row10 = dataTable3.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable3.Rows.Add(row10);
                        DataRow row11 = dataTable3.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable3.Rows.Add(row11);
                        DataRow row12 = dataTable3.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable3.Rows.Add(row12);
                        DataRow row13 = dataTable3.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable3.Rows.Add(row13);
                        DataRow row14 = dataTable3.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable3.Rows.Add(row14);
                        DataRow row15 = dataTable3.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable3.Rows.Add(row15);
                        DataRow row16 = dataTable3.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable3.Rows.Add(row16);
                        DataRow row17 = dataTable3.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable3.Rows.Add(row17);
                        DataRow row18 = dataTable3.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable3.Rows.Add(row18);
                        DataRow row19 = dataTable3.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable3.Rows.Add(row19);
                        DataRow row20 = dataTable3.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable3.Rows.Add(row20);
                        DataRow row21 = dataTable3.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable3.Rows.Add(row21);
                        list2.Add(akteri);
                        if (objekt.unitNoField == "0")
                        {
                          gridView3.DataSource = (object) dataTable3;
                          gridView3.DataBind();
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                          {
                            Text = "Податоци за актери"
                          });
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView3);
                          list3 = (List<GridView>) this.Session["GridViewList3"];
                          this.Session["GridViewList3"] = (object) new List<GridView>()
                          {
                            gridView3
                          };
                        }
                        else
                          dataTable3 = (DataTable) null;
                      }
                      else if (dataTable3 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        Akteri akteri = new Akteri();
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        akteri.zapis1 = objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        akteri.zapisPodroznica = objekt.unitNoField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Тип на овластено лице";
                        row3[1] = (object) objekt.personOrLEIDField;
                        akteri.zapis3 = objekt.personOrLEIDField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Опис на тип на овластено лице";
                        row4[1] = (object) objekt.personOrLEDescField;
                        akteri.zapis4 = objekt.personOrLEDescField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Матичен број на актер";
                        row5[1] = (object) objekt.actorIDField;
                        akteri.zapis5 = objekt.actorIDField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Шифра на тип на актер";
                        row6[1] = (object) objekt.actorTypeIDField;
                        akteri.zapisTipAkter = objekt.actorTypeIDField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Опис на тип на актер";
                        row7[1] = (object) objekt.actorTypeDescField;
                        akteri.zapis7 = objekt.actorTypeDescField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Име";
                        row8[1] = (object) objekt.actorNameField;
                        akteri.zapis8 = objekt.actorNameField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Презиме";
                        row9[1] = (object) objekt.actorSurnameField;
                        akteri.zapis9 = objekt.actorSurnameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Код на земја";
                        row10[1] = (object) objekt.countryCodeField;
                        akteri.zapis10 = objekt.countryCodeField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Општина";
                        row11[1] = (object) objekt.municipalityField;
                        akteri.zapis11 = objekt.municipalityField;
                        dataTable9.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Место";
                        row12[1] = (object) objekt.placeField;
                        akteri.zapis12 = objekt.placeField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        akteri.zapis13 = objekt.streetField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Број";
                        row14[1] = (object) objekt.houseNoField;
                        akteri.zapis14 = objekt.houseNoField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Влез";
                        row15[1] = (object) objekt.entranceNoField;
                        akteri.zapis15 = objekt.entranceNoField;
                        dataTable9.Rows.Add(row15);
                        DataRow row16 = dataTable9.NewRow();
                        row16[0] = (object) "Стан";
                        row16[1] = (object) objekt.flatNoField;
                        akteri.zapis16 = objekt.flatNoField;
                        dataTable9.Rows.Add(row16);
                        DataRow row17 = dataTable9.NewRow();
                        row17[0] = (object) "Електронско сандаче";
                        row17[1] = (object) objekt.emailField;
                        akteri.zapis17 = objekt.emailField;
                        dataTable9.Rows.Add(row17);
                        DataRow row18 = dataTable9.NewRow();
                        row18[0] = (object) "Овластувања";
                        row18[1] = (object) objekt.descriptionField;
                        akteri.zapis18 = objekt.descriptionField;
                        dataTable9.Rows.Add(row18);
                        DataRow row19 = dataTable9.NewRow();
                        row19[0] = (object) "Ограничувања";
                        row19[1] = (object) objekt.restrictionsField;
                        akteri.zapis19 = objekt.restrictionsField;
                        dataTable9.Rows.Add(row19);
                        DataRow row20 = dataTable9.NewRow();
                        row20[0] = (object) "Тип на овластување";
                        row20[1] = (object) objekt.authorisationTypeIDField;
                        akteri.zapis20 = objekt.authorisationTypeIDField;
                        dataTable9.Rows.Add(row20);
                        DataRow row21 = dataTable9.NewRow();
                        row21[0] = (object) "Опис на тип на овластување";
                        row21[1] = (object) objekt.authorisationTypeDescField;
                        akteri.zapis21 = objekt.authorisationTypeDescField;
                        dataTable9.Rows.Add(row21);
                        list2.Add(akteri);
                        if (objekt.unitNoField == "0")
                        {
                          gridView9.DataSource = (object) dataTable9;
                          gridView9.DataBind();
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                          this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                          List<GridView> list4 = (List<GridView>) this.Session["GridViewList3"];
                          list4.Add(gridView9);
                          this.Session["GridViewList3"] = (object) list4;
                        }
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[4].templateNameField && (objekt.lEIDField != null || objekt.unitNoField != null || (objekt.unitNameField != null || objekt.unitTypeIDField != null) || (objekt.unitTypeDescrField != null || objekt.unitDescrField != null || (objekt.otherInfoField != null || objekt.countryCodeField != null)) || (objekt.municipalityField != null || objekt.municipalityCodeField != null || (objekt.placeField != null || objekt.placeCodeField != null) || (objekt.streetField != null || objekt.streetCodeField != null || (objekt.houseNoField != null || objekt.entranceNoField != null))) || (objekt.flatNoField != null || objekt.activityCodeField != null || objekt.activityDescField != null)) && objekt.unitNoField != null)
                    {
                      if (dataTable2 == null)
                      {
                        gridView2.CssClass = "GridViewStyle";
                        gridView2.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView2.RowStyle.CssClass = "RowStyle";
                        gridView2.HeaderStyle.CssClass = "HeaderStyle";
                        gridView2.ControlStyle.Font.Bold = true;
                        gridView2.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView2.ID = "GridViewListTemplate2";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable2 = new DataTable();
                        dataTable2.Columns.Add(column1);
                        dataTable2.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable2.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable2.Rows.Add(row1);
                        DataRow row2 = dataTable2.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        dataTable2.Rows.Add(row2);
                        DataRow row3 = dataTable2.NewRow();
                        row3[0] = (object) "Назив на подружница";
                        row3[1] = (object) objekt.unitNameField;
                        dataTable2.Rows.Add(row3);
                        DataRow row4 = dataTable2.NewRow();
                        row4[0] = (object) "Тип на подружница";
                        row4[1] = (object) objekt.unitTypeIDField;
                        dataTable2.Rows.Add(row4);
                        DataRow row5 = dataTable2.NewRow();
                        row5[0] = (object) "Опис на тип на подружница";
                        row5[1] = (object) objekt.unitTypeDescrField;
                        dataTable2.Rows.Add(row5);
                        DataRow row6 = dataTable2.NewRow();
                        row6[0] = (object) "Опис на подружница";
                        row6[1] = (object) objekt.unitDescrField;
                        dataTable2.Rows.Add(row6);
                        DataRow row7 = dataTable2.NewRow();
                        row7[0] = (object) "Останати информации";
                        row7[1] = (object) objekt.otherInfoField;
                        dataTable2.Rows.Add(row7);
                        DataRow row8 = dataTable2.NewRow();
                        row8[0] = (object) "Код на земја";
                        row8[1] = (object) objekt.countryCodeField;
                        dataTable2.Rows.Add(row8);
                        DataRow row9 = dataTable2.NewRow();
                        row9[0] = (object) "Општина";
                        row9[1] = (object) objekt.municipalityField;
                        dataTable2.Rows.Add(row9);
                        DataRow row10 = dataTable2.NewRow();
                        row10[0] = (object) "Код на општина";
                        row10[1] = (object) objekt.municipalityCodeField;
                        dataTable2.Rows.Add(row10);
                        DataRow row11 = dataTable2.NewRow();
                        row11[0] = (object) "Место";
                        row11[1] = (object) objekt.placeField;
                        dataTable2.Rows.Add(row11);
                        DataRow row12 = dataTable2.NewRow();
                        row12[0] = (object) "Код на место";
                        row12[1] = (object) objekt.placeCodeField;
                        dataTable2.Rows.Add(row12);
                        DataRow row13 = dataTable2.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        dataTable2.Rows.Add(row13);
                        DataRow row14 = dataTable2.NewRow();
                        row14[0] = (object) "Код на улица";
                        row14[1] = (object) objekt.streetCodeField;
                        dataTable2.Rows.Add(row14);
                        DataRow row15 = dataTable2.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable2.Rows.Add(row15);
                        DataRow row16 = dataTable2.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable2.Rows.Add(row16);
                        DataRow row17 = dataTable2.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable2.Rows.Add(row17);
                        DataRow row18 = dataTable2.NewRow();
                        row18[0] = (object) "Код на претежна дејност";
                        row18[1] = (object) objekt.activityCodeField;
                        dataTable2.Rows.Add(row18);
                        DataRow row19 = dataTable2.NewRow();
                        row19[0] = (object) "Опис на претежна дејност";
                        row19[1] = (object) objekt.activityDescField;
                        dataTable2.Rows.Add(row19);
                        if (akteri != null)
                        {
                          DataRow row20 = dataTable2.NewRow();
                          row20[0] = (object) "Овластени лица на подружницата";
                          row20[1] = (object) "";
                          dataTable2.Rows.Add(row20);
                          DataRow row21 = dataTable2.NewRow();
                          row21[0] = (object) "Матичен број на правно лице";
                          row21[1] = (object) akteri.zapis1;
                          dataTable2.Rows.Add(row21);
                          DataRow row22 = dataTable2.NewRow();
                          row22[0] = (object) "Број на подружница";
                          row22[1] = (object) akteri.zapisPodroznica;
                          dataTable2.Rows.Add(row22);
                          DataRow row23 = dataTable2.NewRow();
                          row23[0] = (object) "Тип на овластено лице";
                          row23[1] = (object) akteri.zapis3;
                          dataTable2.Rows.Add(row23);
                          DataRow row24 = dataTable2.NewRow();
                          row24[0] = (object) "Опис на тип на овластено лице";
                          row24[1] = (object) akteri.zapis4;
                          dataTable2.Rows.Add(row24);
                          DataRow row25 = dataTable2.NewRow();
                          row25[0] = (object) "Матичен број на актер";
                          row25[1] = (object) akteri.zapis5;
                          dataTable2.Rows.Add(row25);
                          DataRow row26 = dataTable2.NewRow();
                          row26[0] = (object) "Шифра на тип на актер";
                          row26[1] = (object) akteri.zapisTipAkter;
                          dataTable2.Rows.Add(row26);
                          DataRow row27 = dataTable2.NewRow();
                          row27[0] = (object) "Опис на тип на актер";
                          row27[1] = (object) akteri.zapis7;
                          dataTable2.Rows.Add(row27);
                          DataRow row28 = dataTable2.NewRow();
                          row28[0] = (object) "Име";
                          row28[1] = (object) akteri.zapis8;
                          dataTable2.Rows.Add(row28);
                          DataRow row29 = dataTable2.NewRow();
                          row29[0] = (object) "Презиме";
                          row29[1] = (object) akteri.zapis9;
                          dataTable2.Rows.Add(row29);
                          DataRow row30 = dataTable2.NewRow();
                          row30[0] = (object) "Код на земја";
                          row30[1] = (object) akteri.zapis10;
                          dataTable2.Rows.Add(row30);
                          DataRow row31 = dataTable2.NewRow();
                          row31[0] = (object) "Општина";
                          row31[1] = (object) akteri.zapis11;
                          dataTable2.Rows.Add(row31);
                          DataRow row32 = dataTable2.NewRow();
                          row32[0] = (object) "Место";
                          row32[1] = (object) akteri.zapis12;
                          dataTable2.Rows.Add(row32);
                          DataRow row33 = dataTable2.NewRow();
                          row33[0] = (object) "Улица";
                          row33[1] = (object) akteri.zapis13;
                          dataTable2.Rows.Add(row33);
                          DataRow row34 = dataTable2.NewRow();
                          row34[0] = (object) "Број";
                          row34[1] = (object) akteri.zapis14;
                          dataTable2.Rows.Add(row34);
                          DataRow row35 = dataTable2.NewRow();
                          row35[0] = (object) "Влез";
                          row35[1] = (object) akteri.zapis15;
                          dataTable2.Rows.Add(row35);
                          DataRow row36 = dataTable2.NewRow();
                          row36[0] = (object) "Стан";
                          row36[1] = (object) akteri.zapis16;
                          dataTable2.Rows.Add(row36);
                          DataRow row37 = dataTable2.NewRow();
                          row37[0] = (object) "Електронско сандаче";
                          row37[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row37);
                          DataRow row38 = dataTable2.NewRow();
                          row38[0] = (object) "Овластувања";
                          row38[1] = (object) akteri.zapis18;
                          dataTable2.Rows.Add(row38);
                          DataRow row39 = dataTable2.NewRow();
                          row39[0] = (object) "Ограничувања";
                          row39[1] = (object) akteri.zapis19;
                          dataTable2.Rows.Add(row39);
                          DataRow row40 = dataTable2.NewRow();
                          row40[0] = (object) "Тип на овластување";
                          row40[1] = (object) akteri.zapis20;
                          dataTable2.Rows.Add(row40);
                          DataRow row41 = dataTable2.NewRow();
                          row41[0] = (object) "Опис на тип на овластување";
                          row41[1] = (object) akteri.zapis21;
                          dataTable2.Rows.Add(row41);
                        }
                        gridView2.DataSource = (object) dataTable2;
                        gridView2.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за подружници"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView2);
                        list3 = (List<GridView>) this.Session["GridViewList2"];
                        this.Session["GridViewList2"] = (object) new List<GridView>()
                        {
                          gridView2
                        };
                      }
                      else if (dataTable2 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        Akteri akteri = (Akteri) null;
                        try
                        {
                          akteri = Enumerable.Single<Akteri>((IEnumerable<Akteri>) list2, (Func<Akteri, bool>) (p => p.zapisPodroznica == objekt.unitNoField));
                        }
                        catch
                        {
                        }
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Број на подружница";
                        row2[1] = (object) objekt.unitNoField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Назив на подружница";
                        row3[1] = (object) objekt.unitNameField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Тип на подружница";
                        row4[1] = (object) objekt.unitTypeIDField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Опис на тип на подружница";
                        row5[1] = (object) objekt.unitTypeDescrField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Опис на подружница";
                        row6[1] = (object) objekt.unitDescrField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Останати информации";
                        row7[1] = (object) objekt.otherInfoField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Код на земја";
                        row8[1] = (object) objekt.countryCodeField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Општина";
                        row9[1] = (object) objekt.municipalityField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Код на општина";
                        row10[1] = (object) objekt.municipalityCodeField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Место";
                        row11[1] = (object) objekt.placeField;
                        dataTable9.Rows.Add(row11);
                        DataRow row12 = dataTable9.NewRow();
                        row12[0] = (object) "Код на место";
                        row12[1] = (object) objekt.placeCodeField;
                        dataTable9.Rows.Add(row12);
                        DataRow row13 = dataTable9.NewRow();
                        row13[0] = (object) "Улица";
                        row13[1] = (object) objekt.streetField;
                        dataTable9.Rows.Add(row13);
                        DataRow row14 = dataTable9.NewRow();
                        row14[0] = (object) "Код на улица";
                        row14[1] = (object) objekt.streetCodeField;
                        dataTable9.Rows.Add(row14);
                        DataRow row15 = dataTable9.NewRow();
                        row15[0] = (object) "Број";
                        row15[1] = (object) objekt.houseNoField;
                        dataTable9.Rows.Add(row15);
                        DataRow row16 = dataTable9.NewRow();
                        row16[0] = (object) "Влез";
                        row16[1] = (object) objekt.entranceNoField;
                        dataTable9.Rows.Add(row16);
                        DataRow row17 = dataTable9.NewRow();
                        row17[0] = (object) "Стан";
                        row17[1] = (object) objekt.flatNoField;
                        dataTable9.Rows.Add(row17);
                        DataRow row18 = dataTable9.NewRow();
                        row18[0] = (object) "Код на претежна дејност";
                        row18[1] = (object) objekt.activityCodeField;
                        dataTable9.Rows.Add(row18);
                        DataRow row19 = dataTable9.NewRow();
                        row19[0] = (object) "Опис на претежна дејност";
                        row19[1] = (object) objekt.activityDescField;
                        dataTable9.Rows.Add(row19);
                        if (akteri != null)
                        {
                          DataRow row20 = dataTable9.NewRow();
                          row20[0] = (object) "Овластени лица на подружницата";
                          row20[1] = (object) "";
                          dataTable9.Rows.Add(row20);
                          DataRow row21 = dataTable9.NewRow();
                          row21[0] = (object) "Матичен број на правно лице";
                          row21[1] = (object) akteri.zapis1;
                          dataTable9.Rows.Add(row21);
                          DataRow row22 = dataTable9.NewRow();
                          row22[0] = (object) "Број на подружница";
                          row22[1] = (object) akteri.zapisPodroznica;
                          dataTable9.Rows.Add(row22);
                          DataRow row23 = dataTable9.NewRow();
                          row23[0] = (object) "Тип на овластено лице";
                          row23[1] = (object) akteri.zapis3;
                          dataTable9.Rows.Add(row23);
                          DataRow row24 = dataTable9.NewRow();
                          row24[0] = (object) "Опис на тип на овластено лице";
                          row24[1] = (object) akteri.zapis4;
                          dataTable9.Rows.Add(row24);
                          DataRow row25 = dataTable9.NewRow();
                          row25[0] = (object) "Матичен број на актер";
                          row25[1] = (object) akteri.zapis5;
                          dataTable9.Rows.Add(row25);
                          DataRow row26 = dataTable9.NewRow();
                          row26[0] = (object) "Шифра на тип на актер";
                          row26[1] = (object) akteri.zapisTipAkter;
                          dataTable9.Rows.Add(row26);
                          DataRow row27 = dataTable9.NewRow();
                          row27[0] = (object) "Опис на тип на актер";
                          row27[1] = (object) akteri.zapis7;
                          dataTable9.Rows.Add(row27);
                          DataRow row28 = dataTable9.NewRow();
                          row28[0] = (object) "Име";
                          row28[1] = (object) akteri.zapis8;
                          dataTable9.Rows.Add(row28);
                          DataRow row29 = dataTable9.NewRow();
                          row29[0] = (object) "Презиме";
                          row29[1] = (object) akteri.zapis9;
                          dataTable9.Rows.Add(row29);
                          DataRow row30 = dataTable9.NewRow();
                          row30[0] = (object) "Код на земја";
                          row30[1] = (object) akteri.zapis10;
                          dataTable9.Rows.Add(row30);
                          DataRow row31 = dataTable9.NewRow();
                          row31[0] = (object) "Општина";
                          row31[1] = (object) akteri.zapis11;
                          dataTable9.Rows.Add(row31);
                          DataRow row32 = dataTable9.NewRow();
                          row32[0] = (object) "Место";
                          row32[1] = (object) akteri.zapis12;
                          dataTable9.Rows.Add(row32);
                          DataRow row33 = dataTable9.NewRow();
                          row33[0] = (object) "Улица";
                          row33[1] = (object) akteri.zapis13;
                          dataTable9.Rows.Add(row33);
                          DataRow row34 = dataTable9.NewRow();
                          row34[0] = (object) "Број";
                          row34[1] = (object) akteri.zapis14;
                          dataTable9.Rows.Add(row34);
                          DataRow row35 = dataTable9.NewRow();
                          row35[0] = (object) "Влез";
                          row35[1] = (object) akteri.zapis15;
                          dataTable9.Rows.Add(row35);
                          DataRow row36 = dataTable9.NewRow();
                          row36[0] = (object) "Стан";
                          row36[1] = (object) akteri.zapis16;
                          dataTable9.Rows.Add(row36);
                          DataRow row37 = dataTable2.NewRow();
                          row37[0] = (object) "Електронско сандаче";
                          row37[1] = (object) akteri.zapis17;
                          dataTable2.Rows.Add(row37);
                          DataRow row38 = dataTable9.NewRow();
                          row38[0] = (object) "Овластувања";
                          row38[1] = (object) akteri.zapis18;
                          dataTable9.Rows.Add(row38);
                          DataRow row39 = dataTable9.NewRow();
                          row39[0] = (object) "Ограничувања";
                          row39[1] = (object) akteri.zapis19;
                          dataTable9.Rows.Add(row39);
                          DataRow row40 = dataTable9.NewRow();
                          row40[0] = (object) "Тип на овластување";
                          row40[1] = (object) akteri.zapis20;
                          dataTable9.Rows.Add(row40);
                          DataRow row41 = dataTable9.NewRow();
                          row41[0] = (object) "Опис на тип на овластување";
                          row41[1] = (object) akteri.zapis21;
                          dataTable9.Rows.Add(row41);
                        }
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList2"];
                        list4.Add(gridView9);
                        this.Session["GridViewList2"] = (object) list4;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[5].templateNameField && (objekt.lEIDField != null || objekt.activityDescField != null) && objekt.activityCodeField != null)
                    {
                      if (dataTable5 == null)
                      {
                        dataTable5 = new DataTable();
                        gridView5.CssClass = "GridViewStyle";
                        gridView5.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView5.RowStyle.CssClass = "RowStyle";
                        gridView5.ControlStyle.Font.Bold = true;
                        gridView5.HeaderStyle.CssClass = "HeaderStyle";
                        gridView5.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView5.ID = "GridViewListTemplate5";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Код на дејност");
                        DataColumn column3 = new DataColumn("Опис на дејност");
                        dataTable5.Columns.Add(column1);
                        dataTable5.Columns.Add(column2);
                        dataTable5.Columns.Add(column3);
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за активности"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView5);
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                      else if (dataTable5 != null)
                      {
                        DataRow row = dataTable5.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Код на дејност"] = (object) objekt.activityCodeField;
                        row["Опис на дејност"] = (object) objekt.activityDescField;
                        dataTable5.Rows.Add(row);
                        gridView5.DataSource = (object) dataTable5;
                        gridView5.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView5);
                        this.Session["GridViewList5"] = (object) gridView5;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[6].templateNameField && objekt.lEIDField != null && (objekt.memberOfField != null || objekt.membersField != null))
                    {
                      if (dataTable6 == null)
                      {
                        dataTable6 = new DataTable();
                        gridView6.CssClass = "GridViewStyle";
                        gridView6.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView6.RowStyle.CssClass = "RowStyle";
                        gridView6.HeaderStyle.CssClass = "HeaderStyle";
                        gridView6.ControlStyle.Font.Bold = true;
                        gridView6.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView6.ID = "GridViewListTemplate6";
                        DataColumn column1 = new DataColumn("Матичен број на правно лице");
                        DataColumn column2 = new DataColumn("Матичен број на правно лице членка на синдикат");
                        DataColumn column3 = new DataColumn("Матичен број на правно лице кое пристапува како членка на синдикат");
                        dataTable6.Columns.Add(column1);
                        dataTable6.Columns.Add(column2);
                        dataTable6.Columns.Add(column3);
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за членство во синдикат"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView6);
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                      else if (dataTable6 != null)
                      {
                        DataRow row = dataTable6.NewRow();
                        row["Матичен број на правно лице"] = (object) objekt.lEIDField;
                        row["Матичен број на правно лице членка на синдикат"] = (object) objekt.memberOfField;
                        row["Матичен број на правно лице кое пристапува како членка на синдикат"] = (object) objekt.membersField;
                        dataTable6.Rows.Add(row);
                        gridView6.DataSource = (object) dataTable6;
                        gridView6.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView6);
                        this.Session["GridViewList6"] = (object) gridView6;
                      }
                    }
                    if (templateNameField == dataByEmbs.VratiCRMRezultatiUJPResult.itemsField[7].templateNameField && (objekt.lEIDField != null || objekt.decisionDateField != null || (objekt.validityDateField != null || objekt.validityHourField != null) || (objekt.stageIDField != null || objekt.stageDescField != null || (objekt.descriptionField != null || objekt.bankruptcyCourtNameField != null)) || (objekt.typeIDField != null || objekt.typeDescField != null)) && objekt.courtJournalIDField != null)
                    {
                      if (dataTable8 == null)
                      {
                        dataTable8 = new DataTable();
                        gridView8.CssClass = "GridViewStyle";
                        gridView8.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView8.RowStyle.CssClass = "RowStyle";
                        gridView8.ControlStyle.Font.Bold = true;
                        gridView8.HeaderStyle.CssClass = "HeaderStyle";
                        gridView8.Attributes.Add("style", "width: auto; min-width:500px");
                        gridView8.ID = "GridViewListTemplate8";
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable8.Columns.Add(column1);
                        dataTable8.Columns.Add(column2);
                        DataRow row1 = dataTable8.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable8.Rows.Add(row1);
                        DataRow row2 = dataTable8.NewRow();
                        row2[0] = (object) "Број на судска постапка";
                        row2[1] = (object) objekt.courtJournalIDField;
                        dataTable8.Rows.Add(row2);
                        DataRow row3 = dataTable8.NewRow();
                        row3[0] = (object) "Датум на одлука";
                        row3[1] = (object) objekt.decisionDateField;
                        dataTable8.Rows.Add(row3);
                        DataRow row4 = dataTable8.NewRow();
                        row4[0] = (object) "Датум на отварање на постапката";
                        row4[1] = (object) objekt.validityDateField;
                        dataTable8.Rows.Add(row4);
                        DataRow row5 = dataTable8.NewRow();
                        row5[0] = (object) "Време на отварање на постапката";
                        row5[1] = (object) objekt.validityHourField;
                        dataTable8.Rows.Add(row5);
                        DataRow row6 = dataTable8.NewRow();
                        row6[0] = (object) "Статус на постапката";
                        row6[1] = (object) objekt.stageIDField;
                        dataTable8.Rows.Add(row6);
                        DataRow row7 = dataTable8.NewRow();
                        row7[0] = (object) "Опис на статусот на постапката";
                        row7[1] = (object) objekt.stageDescField;
                        dataTable8.Rows.Add(row7);
                        DataRow row8 = dataTable8.NewRow();
                        row8[0] = (object) "Опис на постапката";
                        row8[1] = (object) objekt.descriptionField;
                        dataTable8.Rows.Add(row8);
                        DataRow row9 = dataTable8.NewRow();
                        row9[0] = (object) "Име на стечаен суд";
                        row9[1] = (object) objekt.bankruptcyCourtNameField;
                        dataTable8.Rows.Add(row9);
                        DataRow row10 = dataTable8.NewRow();
                        row10[0] = (object) "Тип на постапката";
                        row10[1] = (object) objekt.typeIDField;
                        dataTable8.Rows.Add(row10);
                        DataRow row11 = dataTable8.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) objekt.typeDescField;
                        dataTable8.Rows.Add(row11);
                        gridView8.DataSource = (object) dataTable8;
                        gridView8.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new Label()
                        {
                          Text = "Податоци за судски процеси"
                        });
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView8);
                        list3 = (List<GridView>) this.Session["GridViewList8"];
                        this.Session["GridViewList8"] = (object) new List<GridView>()
                        {
                          gridView8
                        };
                      }
                      else if (dataTable8 != null)
                      {
                        DataTable dataTable9 = new DataTable();
                        GridView gridView9 = new GridView();
                        gridView9.CssClass = "GridViewStyle";
                        gridView9.AlternatingRowStyle.CssClass = "AltRowStyle";
                        gridView9.RowStyle.CssClass = "RowStyle";
                        gridView9.ControlStyle.Font.Bold = true;
                        gridView9.HeaderStyle.CssClass = "HeaderStyle";
                        gridView9.Attributes.Add("style", "width: auto; min-width:500px");
                        DataColumn column1 = new DataColumn("Наслов на податок");
                        DataColumn column2 = new DataColumn("Податок");
                        dataTable9.Columns.Add(column1);
                        dataTable9.Columns.Add(column2);
                        DataRow row1 = dataTable9.NewRow();
                        row1[0] = (object) "Матичен број на правно лице";
                        row1[1] = (object) objekt.lEIDField;
                        dataTable9.Rows.Add(row1);
                        DataRow row2 = dataTable9.NewRow();
                        row2[0] = (object) "Број на судска постапка";
                        row2[1] = (object) objekt.courtJournalIDField;
                        dataTable9.Rows.Add(row2);
                        DataRow row3 = dataTable9.NewRow();
                        row3[0] = (object) "Датум на одлука";
                        row3[1] = (object) objekt.decisionDateField;
                        dataTable9.Rows.Add(row3);
                        DataRow row4 = dataTable9.NewRow();
                        row4[0] = (object) "Датум на отварање на постапката";
                        row4[1] = (object) objekt.validityDateField;
                        dataTable9.Rows.Add(row4);
                        DataRow row5 = dataTable9.NewRow();
                        row5[0] = (object) "Време на отварање на постапката";
                        row5[1] = (object) objekt.validityHourField;
                        dataTable9.Rows.Add(row5);
                        DataRow row6 = dataTable9.NewRow();
                        row6[0] = (object) "Статус на постапката";
                        row6[1] = (object) objekt.stageIDField;
                        dataTable9.Rows.Add(row6);
                        DataRow row7 = dataTable9.NewRow();
                        row7[0] = (object) "Опис на статусот на постапката";
                        row7[1] = (object) objekt.stageDescField;
                        dataTable9.Rows.Add(row7);
                        DataRow row8 = dataTable9.NewRow();
                        row8[0] = (object) "Опис на постапката";
                        row8[1] = (object) objekt.descriptionField;
                        dataTable9.Rows.Add(row8);
                        DataRow row9 = dataTable9.NewRow();
                        row9[0] = (object) "Име на стечаен суд";
                        row9[1] = (object) objekt.bankruptcyCourtNameField;
                        dataTable9.Rows.Add(row9);
                        DataRow row10 = dataTable9.NewRow();
                        row10[0] = (object) "Тип на постапката";
                        row10[1] = (object) objekt.typeIDField;
                        dataTable9.Rows.Add(row10);
                        DataRow row11 = dataTable9.NewRow();
                        row11[0] = (object) "Опис на тип на постапката";
                        row11[1] = (object) objekt.typeDescField;
                        dataTable9.Rows.Add(row11);
                        gridView9.DataSource = (object) dataTable9;
                        gridView9.DataBind();
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) gridView9);
                        List<GridView> list4 = (List<GridView>) this.Session["GridViewList8"];
                        list4.Add(gridView9);
                        this.Session["GridViewList8"] = (object) list4;
                      }
                    }
                  }
                }
              }
              this.ImageButtonCR_UJP.Visible = true;
              if ((GridView) this.Session["GridViewList1"] != null)
                list1.Add("Податоци за субјетот");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList2"] != null)
                list1.Add("Податоци за подружници");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList3"] != null)
                list1.Add("Податоци за актери");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList4"] != null)
                list1.Add("Податоци за сопственици");
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList5"] != null)
                list1.Add("Податоци за активности");
              else
                list1.Add((string) null);
              if ((GridView) this.Session["GridViewList6"] != null)
                list1.Add("Податоци за членство во синдикат");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList7"] != null)
                list1.Add("Податоци за основање");
              else
                list1.Add((string) null);
              if ((List<GridView>) this.Session["GridViewList8"] != null)
                list1.Add("Податоци за судски процеси");
              else
                list1.Add((string) null);
              this.Session["ParametersPrint"] = (object) ("Единствен Матичен Број На Субјектот За УЈП_" + EMBS.EMBS1);
              this.Session["ListaRezultHeaders"] = (object) list1;
            }
            this.Completed = true;
            reqrespPortClient.Close();
          }
          catch (Exception ex)
          {
            if (num == 3)
            {
              try
              {
                  FaultMessageUJPClass faultMessageUjp;
                using (StringReader stringReader = new StringReader(ex.Message))
                    faultMessageUjp = (FaultMessageUJPClass)new XmlSerializer(typeof(FaultMessageUJPClass)).Deserialize((TextReader)stringReader);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = faultMessageUjp.Error;
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_UJP.Visible = false;
              }
              catch
              {
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
                Label label1 = new Label();
                label1.ID = "lblporaka";
                label1.Text = "Порака: ";
                Label label2 = new Label();
                label2.ID = "lblporakavalue";
                label2.Text = "Сервисот на ЦР во моментов е недостапен. Ве молиме обидете се подоцна!";
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
                this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
                this.ImageButtonCR_UJP.Visible = false;
              }
            }
          }
        }
      }
      else
      {
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("<h3>"));
        Label label1 = new Label();
        label1.ID = "lblporaka";
        label1.Text = "Порака: ";
        Label label2 = new Label();
        label2.ID = "lblporakavalue";
        label2.Text = "Не се пронајдени податоци за параметарот по кој пребарувате!";
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label1);
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) label2);
        this.WSResponseControlPanelCR_UJP.Controls.Add((Control) new LiteralControl("</h3>"));
        this.ImageButtonCR_UJP.Visible = false;
      }
    }

    protected void ImageButtonCR_CU_Click(object sender, ImageClickEventArgs e)
    {
      GridView gridView = (GridView) this.Session["GridViewList1"];
      List<GridView> list1 = (List<GridView>) this.Session["GridViewList2"];
      List<GridView> list2 = (List<GridView>) this.Session["GridViewList3"];
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Податоци за ЕМБС за потребите на ЦУ";
      report.GridViewpdf = gridView;
      report.GridViewpdf1 = list1;
      report.GridViewpdf2 = list2;
      report.RowFilter = "";
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Ime = "Централен регистар";
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReport();
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonCR_CU, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void ImageButtonCR_UJP_Click(object sender, ImageClickEventArgs e)
    {
      GridView gridView1 = (GridView) this.Session["GridViewList1"];
      List<GridView> list1 = (List<GridView>) this.Session["GridViewList2"];
      List<GridView> list2 = (List<GridView>) this.Session["GridViewList3"];
      List<GridView> list3 = (List<GridView>) this.Session["GridViewList4"];
      GridView gridView2 = (GridView) this.Session["GridViewList5"];
      GridView gridView3 = (GridView) this.Session["GridViewList6"];
      List<GridView> list4 = (List<GridView>) this.Session["GridViewList7"];
      List<GridView> list5 = (List<GridView>) this.Session["GridViewList8"];
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Податоци за ЕМБС за потребите на УЈП";
      report.GridViewpdf = gridView1;
      report.GridViewpdf1 = list1;
      report.GridViewpdf2 = list2;
      report.GridViewpdf3 = list3;
      report.GridViewpdf4 = gridView2;
      report.GridViewpdf5 = gridView3;
      report.GridViewpdf6 = list4;
      report.GridViewpdf7 = list5;
      report.RowFilter = "";
      report.Ime = "Централен регистар";
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReportUJP();
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonCR_CU, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void ImageButtonCR_AKN_Click(object sender, ImageClickEventArgs e)
    {
      GridView gridView1 = (GridView) this.Session["GridViewList1"];
      List<GridView> list1 = (List<GridView>) this.Session["GridViewList2"];
      List<GridView> list2 = (List<GridView>) this.Session["GridViewList3"];
      List<GridView> list3 = (List<GridView>) this.Session["GridViewList4"];
      GridView gridView2 = (GridView) this.Session["GridViewList5"];
      GridView gridView3 = (GridView) this.Session["GridViewList6"];
      List<GridView> list4 = (List<GridView>) this.Session["GridViewList7"];
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Податоци за ЕМБС за потребите на АКН";
      report.GridViewpdf = gridView1;
      report.GridViewpdf1 = list1;
      report.GridViewpdf2 = list2;
      report.GridViewpdf3 = list3;
      report.GridViewpdf4 = gridView2;
      report.GridViewpdf5 = gridView3;
      report.GridViewpdf6 = list4;
      report.RowFilter = "";
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Ime = "Централен Регистар";
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReport();
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonCR_AKN, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void ImageButtonUJP_EDB_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        GridView gridView = (GridView) this.Session["GridViewListEDB"];
        DataTable dataTable = (DataTable) gridView.DataSource;
        ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, "Единствен Даночен Број На Субјектот");
        this.Session["Transactions"] = (object) transaction;
        pdfReport report = new pdfReport();
        report.DataViewReport = dataTable.DefaultView;
        report.User = (interop.USER) this.Session["user"];
        report.Servis = "Податоци за единствен даночен број на субјектот";
        report.GridViewpdf = gridView;
        report.RowFilter = "";
        report.PdfFileName = this.Session["ParametersPrint"].ToString();
        report.Ime = "Управа за јавни приходи";
        report.Transaction = (ComputedTransaction) this.Session["Transactions"];
        report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
        report.generirajReport();
        DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
        this.Session["ReportIme"] = (object) report.Ime;
        this.Session["PdfFilePath"] = (object) report.PdfFilePath;
        new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
        ScriptManager.RegisterStartupScript((Control) this.ImageButtonUJP_EDB, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
      }
      catch
      {
      }
    }

    protected void ImageButtonUJP_NAZIV_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        GridView gridView = (GridView) this.Session["GridViewListNaziv"];
        DataTable dataTable = (DataTable) gridView.DataSource;
        ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
        this.Session["Transactions"] = (object) transaction;
        pdfReport report = new pdfReport();
        report.DataViewReport = dataTable.DefaultView;
        report.User = (interop.USER) this.Session["user"];
        report.Servis = "Податоци за субјектите по назив";
        report.GridViewpdf = gridView;
        report.RowFilter = "";
        report.PdfFileName = this.Session["ParametersPrint"].ToString();
        report.Ime = "Управа за јавни приходи";
        report.Transaction = (ComputedTransaction) this.Session["Transactions"];
        report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
        report.generirajReport();
        DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
        this.Session["ReportIme"] = (object) report.Ime;
        this.Session["PdfFilePath"] = (object) report.PdfFilePath;
        new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
        ScriptManager.RegisterStartupScript((Control) this.ImageButtonUJP_EDB, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
      }
      catch
      {
      }
    }

    protected void ImageButtonMVR_Click(object sender, ImageClickEventArgs e)
    {
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Државјанство";
      report.GridViewpdf = (GridView) null;
      report.RowFilter = (string) this.Session["drzavjanstvo"];
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Ime = "Министерство за внатрешни работи";
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReport();
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonMVR, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void ImageButtonAKN_Click(object sender, ImageClickEventArgs e)
    {
      GridView gridView = (GridView) this.Session["GridViewAKN1"];
      List<GridView> list1 = new List<GridView>();
      list1.Add((GridView) this.Session["GridViewAKN2"]);
      List<GridView> list2 = new List<GridView>();
      list2.Add((GridView) this.Session["GridViewAKN3"]);
      List<GridView> list3 = new List<GridView>();
      list3.Add((GridView) this.Session["GridViewAKN4"]);
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Имотен Лист";
      report.GridViewpdf = gridView;
      report.GridViewpdf1 = list1;
      report.GridViewpdf2 = list2;
      report.GridViewpdf3 = list3;
      report.RowFilter = (string) this.Session["neuspesenAKN"];
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Ime = "Агенција за катастар на недвижности";
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReport();
      if (!File.Exists(report.PdfFilePath))
        return;
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonAKN, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void ImageButtonAKNParceli_Click(object sender, ImageClickEventArgs e)
    {
      GridView gridView = (GridView) this.Session["GridViewAKNParceli"];
      ComputedTransaction transaction = new WebApplicationInterop.InteropDAL().GetTransaction(((interop.USER) this.Session["user"]).username, this.Session["NazivServis"].ToString());
      this.Session["Transactions"] = (object) transaction;
      pdfReport report = new pdfReport();
      report.User = (interop.USER) this.Session["user"];
      report.Servis = "Податоци за парцели";
      report.GridViewpdf = gridView;
      report.RowFilter = (string) this.Session["neuspesenAKN"];
      report.PdfFileName = this.Session["ParametersPrint"].ToString();
      report.Ime = "Агенција за катастар на недвижности";
      report.Transaction = (ComputedTransaction) this.Session["Transactions"];
      report.ListaRezultHeaders = (List<string>) this.Session["ListaRezultHeaders"];
      report.generirajReport();
      if (!File.Exists(report.PdfFilePath))
        return;
      DateTime creationTime = File.GetCreationTime(report.PdfFilePath);
      this.Session["ReportIme"] = (object) report.Ime;
      this.Session["PdfFilePath"] = (object) report.PdfFilePath;
      new WebApplicationInterop.InteropDAL().InsertToReportAndLog((interop.USER) this.Session["user"], transaction.RecordID.ToString(), report, creationTime);
      ScriptManager.RegisterStartupScript((Control) this.ImageButtonAKN, this.GetType(), "OnClientClick", "window.open('../Izvestaj.aspx', '_blank')", true);
    }

    protected void NaziviGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      string str = ((string) this.NaziviGridView.SelectedDataKey.Value).Trim();
      this.Session["EDB"] = (object) str;
      this.Session["IsEDB"] = (object) true;
      this.MultiView.SetActiveView(this.ViewUJP_EDB);
      if (this.Session["NaziviCR_CU"] != null && Convert.ToBoolean(this.Session["NaziviCR_CU"]))
      {
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
        GetPodatociByEDBResponse dataByEdb = reqrespPortClient.GetDataByEDB(new EDB()
        {
          Username = user.username,
          Password = user.password,
          EDB1 = str,
          NacinNaIsprakjanje = "EORPD",
          OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text,
          TimeStamp = DateTime.Now.ToString()
        });
        this.Session["IsMBS"] = (object) true;
        this.Session["MBS"] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
        this.MultiView.SetActiveView(this.ViewCR_CU);
        this.GetEMBSforCU();
      }
      else if (this.Session["NaziviCR_UJP"] != null && Convert.ToBoolean(this.Session["NaziviCR_UJP"]))
      {
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
        GetPodatociByEDBResponse dataByEdb = reqrespPortClient.GetDataByEDB(new EDB()
        {
          Username = user.username,
          Password = user.password,
          EDB1 = str,
          NacinNaIsprakjanje = "EORPD",
          OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text,
          TimeStamp = DateTime.Now.ToString()
        });
        this.Session["IsMBS"] = (object) true;
        this.Session["MBS"] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
        this.MultiView.SetActiveView(this.ViewCR_UJP);
        this.GetEMBSforUJP();
      }
      else if (this.Session["NaziviCR_AKN"] != null && Convert.ToBoolean(this.Session["NaziviCR_AKN"]))
      {
        interop.USER user = (interop.USER) this.Session["user"];
        WSHttpBinding wsHttpBinding = new WSHttpBinding();
        wsHttpBinding.Name = "myBinding";
        wsHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
        wsHttpBinding.Security.Mode = SecurityMode.Message;
        wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        EndpointAddress remoteAddress = new EndpointAddress(new Uri("http://ujpbiztalkwcfserviceapplicationedb.interop.local/UJPBizTalkApplicationEDB/UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORT.svc"), EndpointIdentity.CreateDnsIdentity(ConfigurationManager.AppSettings["DNSIdentity"]), new AddressHeader[0]);
        UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient reqrespPortClient = new UJPBizTalkApplicationEDB_Orchestration_1_UJP_EDB_REQRESP_PORTClient((Binding) wsHttpBinding, remoteAddress);
        reqrespPortClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, (object) this.Session["CertificateName"].ToString().Split('=')[1]);
        GetPodatociByEDBResponse dataByEdb = reqrespPortClient.GetDataByEDB(new EDB()
        {
          Username = user.username,
          Password = user.password,
          EDB1 = str,
          NacinNaIsprakjanje = "EORPD",
          OsnovNaBaranje = this.DropDownListOsnov.SelectedItem.Text,
          TimeStamp = DateTime.Now.ToString()
        });
        this.Session["IsMBS"] = (object) true;
        this.Session["MBS"] = (object) dataByEdb.GetPodatociByEDBResult.Telo.DokOsnovniPodatoci.MB.Trim();
        this.MultiView.SetActiveView(this.ViewCR_AKN);
        this.GetEMBSforAKN();
      }
      else
        this.GetDanocenBrojData();
    }

    protected void WSGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void NaziviGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.MultiView.SetActiveView(this.ViewUJP_NAZIV);
      this.NaziviGridView.DataSource = (object) (DataTable) this.Session["NaziviDataTableList"];
      this.NaziviGridView.PageIndex = e.NewPageIndex;
      this.NaziviGridView.DataBind();
    }

    protected void NaziviGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
    }

    protected void SendMail(string messagetosend)
    {
      try
      {
        string Subject = "Порака за тревога во системот за Интероперабилност.";
        string Body = "Порака за тревога во системот за Интероперабилност. \n \n" + messagetosend;
        bool flag = false;
        flag = this.wsEmail.SendMail(new Crypto().EncryptStringAES(Subject, "SecRet@admiN$"), Subject, Body, "darkop@vista.com.mk");
      }
      catch
      {
      }
    }
  }
}
