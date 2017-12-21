// Decompiled with JetBrains decompiler
// Type: interop.LocalAdminPanel.WebServicesLog
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using AjaxControlToolkit;
using interop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplicationInterop;

namespace interop.LocalAdminPanel
{
  public class WebServicesLog : UserControl
  {
    protected Image Image1;
    protected Panel PanelTable;
    protected HtmlGenericControl divSearch;
    protected DropDownList DropDownListUsers;
    protected DropDownList DropDownListServices;
    protected Label Label1;
    protected TextBox txtDateFrom;
    protected CalendarExtender calendarButtonExtender;
    protected Label Label2;
    protected TextBox txtDateTo;
    protected CalendarExtender CalendarExtender1;
    protected ImageButton ImageButton1;
    protected GridView LOGGridView;
    protected Panel PanelData;
    protected ImageButton ImageButton2;
    protected Label Label5;
    protected TextBox TextBoxActivity;
    protected Label Label23;
    protected TextBox TextBoxRequestID;
    protected Label Label43;
    protected TextBox TextBoxTimeRequest;
    protected Label Label6;
    protected TextBox TextBoxUserID;
    protected Label Label7;
    protected TextBox TextBoxUserName;
    protected Label Label8;
    protected TextBox TextBoxUserSurname;
    protected Label Label9;
    protected TextBox TextBoxEMail;
    protected Label Label10;
    protected TextBox TextBoxUserAct;
    protected Label Label3;
    protected TextBox TextBoxPermision;
    protected Label Label11;
    protected TextBox TextBoxPermAct;
    protected Label Label12;
    protected TextBox TextBoxInstName;
    protected Label Label13;
    protected TextBox TextBoxInstDesc;
    protected Label Label14;
    protected TextBox TextBoxInstAct;
    protected Label Label15;
    protected TextBox TextBoxServisTittle;
    protected Label Label16;
    protected TextBox TextBoxServisNote;
    protected Label Label17;
    protected TextBox TextBoxServisAct;
    protected Label Label18;
    protected TextBox TextBoxServisDesc;
    protected Label Label19;
    protected TextBox TextBoxServisURL;
    protected Label Label20;
    protected TextBox TextBoxTimeResponse;
    protected Label Label21;
    protected TextBox TextBoxResponseID;
    protected Label Label22;
    protected TextBox TextBoxResponseBody;
    protected Label Label25;
    protected TextBox TextBoxRequestBasis;
    protected Panel Panel1;
    protected HtmlGenericControl XMLSearch;
    protected HtmlGenericControl div1;
    protected Label Label4;
    protected TextBox txtDataOd;
    protected CalendarExtender CalendarExtender2;
    protected Label Label27;
    protected TextBox txtDataDo;
    protected CalendarExtender CalendarExtender3;
    protected ImageButton ButtonXMLSearch;
    protected GridView FileSystemGrid;
    protected Panel PanelNewUser;
    protected Image Image3;
    protected Panel PanelInstitution;
    protected Label Label24;
    protected Label LabelOdI;
    protected Label Label28;
    protected Label LabelDoI;
    protected Label Label30;
    protected Label LabelInst;
    protected Label Label31;
    protected Label lblVkBr;
    protected Panel PanelFill2;
    protected Panel PanelSvrNum;
    protected Panel PanelFill1;
    protected Panel PanelUsNum;
    protected Panel PanelService;
    protected Label Label32;
    protected Label LabelOdS;
    protected Label Label34;
    protected Label LabelDoS;
    protected Label Label36;
    protected Label LabelService;
    protected Label Label26;
    protected Label lblVkBrWS;
    protected Panel PanelFill4;
    protected Panel PanelInstVkBr;
    protected Panel PanelFillUsers;
    protected Panel PanelVkBrUs;
    protected Panel PanelUsers;
    protected Label Label37;
    protected Label LabelOdU;
    protected Label Label39;
    protected Label LabelDoU;
    protected Label Label41;
    protected Label LabelUsers;
    protected Label Label42;
    protected Label lblVkBrUs;
    protected Panel PanelFill3;
    protected Panel PanelBRUSWS;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["HelpMenu2"] = (object) "Help/admin/HelpServisLogovi.aspx";
      this.UpdateLists();
      this.LOGGridView.DataSource = (object) (List<SearchLog>) this.Session["BigListFilter"];
      this.LOGGridView.DataBind();
      DateTime.Now.AddDays(-1.0);
      DateTime now = DateTime.Now;
      ((INSTITUTION) this.Session["UserInstitution"]).ID.ToString();
    }

    public void UpdateLists()
    {
      string selectedValue1 = this.DropDownListUsers.SelectedValue;
      this.DropDownListUsers.Items.Clear();
      this.DropDownListUsers.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Корисник --"
      });
      this.DropDownListUsers.DataSource = (object) Enumerable.Distinct<HelpClassUsers>((IEnumerable<HelpClassUsers>) this.Session["ListLogU"]);
      this.DropDownListUsers.DataBind();
      this.DropDownListUsers.SelectedValue = selectedValue1;
      ListItem listItem = new ListItem()
      {
        Value = "0",
        Text = "-- Институција --"
      };
      string selectedValue2 = this.DropDownListServices.SelectedValue;
      this.DropDownListServices.Items.Clear();
      this.DropDownListServices.Items.Add(new ListItem()
      {
        Value = "0",
        Text = "-- Сервиси --"
      });
      this.DropDownListServices.DataSource = (object) (List<WEBSERVICE>) this.Session["ListLogWS"];
      this.DropDownListServices.DataBind();
      this.DropDownListServices.SelectedValue = selectedValue2;
    }

    public void FileSystemUpdate(string institucija, DateTime odData, DateTime doData, string req)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo("\\\\192.168.4.2\\XMLMessages\\");
      DirectoryInfo directoryInfo2 = new DirectoryInfo("\\\\192.168.7.2\\XMLMessages\\");
      DirectoryInfo directoryInfo3 = new DirectoryInfo("\\\\192.168.3.2\\XMLMessages\\");
      List<Filebrouws> list1 = new List<Filebrouws>();
      List<Filebrouws> list2;
      if (this.Session["filelista"] == null)
      {
        if (institucija == "0")
        {
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.4.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Агенција за катастар и недвижности"
            });
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.7.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Централен регистар"
            });
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.3.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Управа за јавни приходи"
            });
        }
        else
        {
          switch (institucija)
          {
            case "1":
              foreach (FileSystemInfo fileSystemInfo in Enumerable.ToArray<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.OrderByDescending<FileSystemInfo, DateTime>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos(), (Func<FileSystemInfo, DateTime>) (p => p.CreationTime))))
              {
                if (fileSystemInfo.CreationTime > odData && fileSystemInfo.CreationTime < doData)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.4.2\\XMLMessages\\" + fileSystemInfo.Name,
                    Name = fileSystemInfo.Name,
                    CreationTime = fileSystemInfo.CreationTime,
                    LastWriteTime = fileSystemInfo.LastWriteTime,
                    Institution = "Агенција за катастар и недвижности"
                  });
                  if (list1.Count > 100)
                    break;
                }
              }
              break;
            case "8":
              foreach (FileSystemInfo fileSystemInfo in Enumerable.ToArray<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.OrderByDescending<FileSystemInfo, DateTime>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos(), (Func<FileSystemInfo, DateTime>) (p => p.CreationTime))))
              {
                if (fileSystemInfo.CreationTime > odData && fileSystemInfo.CreationTime < doData)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.3.2\\XMLMessages\\" + fileSystemInfo.Name,
                    Name = fileSystemInfo.Name,
                    CreationTime = fileSystemInfo.CreationTime,
                    LastWriteTime = fileSystemInfo.LastWriteTime,
                    Institution = "Управа за јавни приходи"
                  });
                  if (list1.Count > 100)
                    break;
                }
              }
              break;
            case "9":
              foreach (FileSystemInfo fileSystemInfo in Enumerable.ToArray<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.OrderByDescending<FileSystemInfo, DateTime>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos(), (Func<FileSystemInfo, DateTime>) (p => p.CreationTime))))
              {
                if (fileSystemInfo.CreationTime > odData && fileSystemInfo.CreationTime < doData)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.7.2\\XMLMessages\\" + fileSystemInfo.Name,
                    Name = fileSystemInfo.Name,
                    CreationTime = fileSystemInfo.CreationTime,
                    LastWriteTime = fileSystemInfo.LastWriteTime,
                    Institution = "Централен регистар"
                  });
                  if (list1.Count > 100)
                    break;
                }
              }
              break;
            default:
              this.XMLSearch.Visible = false;
              break;
          }
        }
        List<Filebrouws> list3;
        if (req == "")
        {
          list3 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.CreationTime > odData && p.CreationTime < doData)));
        }
        else
        {
          string reqSearch = "req_" + req + ".xml";
          string resSearch = "res_" + req + ".xml";
          list3 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.Name == reqSearch || p.Name == resSearch)));
        }
        list2 = Enumerable.ToList<Filebrouws>((IEnumerable<Filebrouws>) Enumerable.OrderByDescending<Filebrouws, DateTime>((IEnumerable<Filebrouws>) list3, (Func<Filebrouws, DateTime>) (p => p.CreationTime)));
        if (list2.Count > 100)
          list2 = Enumerable.ToList<Filebrouws>(Enumerable.Take<Filebrouws>((IEnumerable<Filebrouws>) list2, 100));
        this.Session["filelista"] = (object) list2;
      }
      else
      {
        list2 = (List<Filebrouws>) this.Session["filelista"];
        if (req != "")
        {
          string reqSearch = "req_" + req + ".xml";
          string resSearch = "res_" + req + ".xml";
          list2 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list2, (Func<Filebrouws, bool>) (p => p.Name == reqSearch || p.Name == resSearch)));
        }
      }
      this.FileSystemGrid.DataSource = (object) list2;
      this.FileSystemGrid.DataBind();
    }

    public void FileSystemUpdateSearch(string institucija, DateTime odData, DateTime doData, string req)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo("\\\\192.168.4.2\\XMLMessages\\");
      DirectoryInfo directoryInfo2 = new DirectoryInfo("\\\\192.168.7.2\\XMLMessages\\");
      DirectoryInfo directoryInfo3 = new DirectoryInfo("\\\\192.168.3.2\\XMLMessages\\");
      List<Filebrouws> list1 = new List<Filebrouws>();
      List<Filebrouws> list2;
      if (this.Session["filelista"] == null)
      {
        if (institucija == "0")
        {
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.4.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Агенција за катастар и недвижности"
            });
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.7.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Централен регистар"
            });
          foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
            list1.Add(new Filebrouws()
            {
              FullName = "\\\\192.168.3.2\\XMLMessages\\" + fileSystemInfo.Name,
              Name = fileSystemInfo.Name,
              CreationTime = fileSystemInfo.CreationTime,
              LastWriteTime = fileSystemInfo.LastWriteTime,
              Institution = "Управа за јавни приходи"
            });
        }
        else
        {
          switch (institucija)
          {
            case "1":
              using (List<FileSystemInfo>.Enumerator enumerator = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  FileSystemInfo current = enumerator.Current;
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.4.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Агенција за катастар и недвижности"
                  });
                }
                break;
              }
            case "8":
              using (List<FileSystemInfo>.Enumerator enumerator = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  FileSystemInfo current = enumerator.Current;
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.3.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Управа за јавни приходи"
                  });
                }
                break;
              }
            case "9":
              using (List<FileSystemInfo>.Enumerator enumerator = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  FileSystemInfo current = enumerator.Current;
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.7.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Централен регистар"
                  });
                }
                break;
              }
          }
        }
        List<Filebrouws> list3;
        if (req == "")
        {
          list3 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.CreationTime > odData && p.CreationTime < doData)));
        }
        else
        {
          string reqSearch = "req_" + req + ".xml";
          string resSearch = "res_" + req + ".xml";
          list3 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.Name == reqSearch || p.Name == resSearch)));
        }
        list2 = Enumerable.ToList<Filebrouws>((IEnumerable<Filebrouws>) Enumerable.OrderByDescending<Filebrouws, DateTime>((IEnumerable<Filebrouws>) list3, (Func<Filebrouws, DateTime>) (p => p.CreationTime)));
        this.Session["filelista"] = (object) list2;
      }
      else
        list2 = (List<Filebrouws>) this.Session["filelista"];
      this.FileSystemGrid.DataSource = (object) list2;
      this.FileSystemGrid.DataBind();
    }

    public void FileSystemSelected(string institucija, DateTime odData, DateTime doData, string req)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo("\\\\192.168.4.2\\XMLMessages\\");
      DirectoryInfo directoryInfo2 = new DirectoryInfo("\\\\192.168.7.2\\XMLMessages\\");
      DirectoryInfo directoryInfo3 = new DirectoryInfo("\\\\192.168.3.2\\XMLMessages\\");
      List<Filebrouws> list1 = new List<Filebrouws>();
      if (institucija == "0")
      {
        foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
          list1.Add(new Filebrouws()
          {
            FullName = "\\\\192.168.4.2\\XMLMessages\\" + fileSystemInfo.Name,
            Name = fileSystemInfo.Name,
            CreationTime = fileSystemInfo.CreationTime,
            LastWriteTime = fileSystemInfo.LastWriteTime,
            Institution = "Агенција за катастар и недвижности"
          });
        foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
          list1.Add(new Filebrouws()
          {
            FullName = "\\\\192.168.7.2\\XMLMessages\\" + fileSystemInfo.Name,
            Name = fileSystemInfo.Name,
            CreationTime = fileSystemInfo.CreationTime,
            LastWriteTime = fileSystemInfo.LastWriteTime,
            Institution = "Централен регистар"
          });
        foreach (FileSystemInfo fileSystemInfo in Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config"))))
          list1.Add(new Filebrouws()
          {
            FullName = "\\\\192.168.3.2\\XMLMessages\\" + fileSystemInfo.Name,
            Name = fileSystemInfo.Name,
            CreationTime = fileSystemInfo.CreationTime,
            LastWriteTime = fileSystemInfo.LastWriteTime,
            Institution = "Управа за јавни приходи"
          });
      }
      else
      {
        string str1 = "req_" + req + ".xml";
        string str2 = "res_" + req + ".xml";
        switch (institucija)
        {
          case "1":
            List<FileSystemInfo> list2 = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo1.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config")));
            foreach (FileSystemInfo fileSystemInfo in list2)
            {
              if (fileSystemInfo.Name == str1)
              {
                list1.Add(new Filebrouws()
                {
                  FullName = "\\\\192.168.4.2\\XMLMessages\\" + fileSystemInfo.Name,
                  Name = fileSystemInfo.Name,
                  CreationTime = fileSystemInfo.CreationTime,
                  LastWriteTime = fileSystemInfo.LastWriteTime,
                  Institution = "Агенција за катастар и недвижности"
                });
                break;
              }
            }
            using (List<FileSystemInfo>.Enumerator enumerator = list2.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FileSystemInfo current = enumerator.Current;
                if (current.Name == str2)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.4.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Агенција за катастар и недвижности"
                  });
                  break;
                }
              }
              break;
            }
          case "8":
            List<FileSystemInfo> list3 = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo3.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config")));
            foreach (FileSystemInfo fileSystemInfo in list3)
            {
              if (fileSystemInfo.Name == str1)
              {
                list1.Add(new Filebrouws()
                {
                  FullName = "\\\\192.168.3.2\\XMLMessages\\" + fileSystemInfo.Name,
                  Name = fileSystemInfo.Name,
                  CreationTime = fileSystemInfo.CreationTime,
                  LastWriteTime = fileSystemInfo.LastWriteTime,
                  Institution = "Управа за јавни приходи"
                });
                break;
              }
            }
            using (List<FileSystemInfo>.Enumerator enumerator = list3.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FileSystemInfo current = enumerator.Current;
                if (current.Name == str2)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.3.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Управа за јавни приходи"
                  });
                  break;
                }
              }
              break;
            }
          case "9":
            List<FileSystemInfo> list4 = Enumerable.ToList<FileSystemInfo>(Enumerable.Where<FileSystemInfo>((IEnumerable<FileSystemInfo>) Enumerable.ToList<FileSystemInfo>((IEnumerable<FileSystemInfo>) directoryInfo2.GetFileSystemInfos()), (Func<FileSystemInfo, bool>) (p => p.Name != "web.config")));
            foreach (FileSystemInfo fileSystemInfo in list4)
            {
              if (fileSystemInfo.Name == str1)
              {
                list1.Add(new Filebrouws()
                {
                  FullName = "\\\\192.168.7.2\\XMLMessages\\" + fileSystemInfo.Name,
                  Name = fileSystemInfo.Name,
                  CreationTime = fileSystemInfo.CreationTime,
                  LastWriteTime = fileSystemInfo.LastWriteTime,
                  Institution = "Централен регистар"
                });
                break;
              }
            }
            using (List<FileSystemInfo>.Enumerator enumerator = list4.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FileSystemInfo current = enumerator.Current;
                if (current.Name == str2)
                {
                  list1.Add(new Filebrouws()
                  {
                    FullName = "\\\\192.168.7.2\\XMLMessages\\" + current.Name,
                    Name = current.Name,
                    CreationTime = current.CreationTime,
                    LastWriteTime = current.LastWriteTime,
                    Institution = "Централен регистар"
                  });
                  break;
                }
              }
              break;
            }
        }
      }
      List<Filebrouws> list5;
      if (req == "")
      {
        list5 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.CreationTime > odData && p.CreationTime < doData)));
      }
      else
      {
        string reqSearch = "req_" + req + ".xml";
        string resSearch = "res_" + req + ".xml";
        list5 = Enumerable.ToList<Filebrouws>(Enumerable.Where<Filebrouws>((IEnumerable<Filebrouws>) list1, (Func<Filebrouws, bool>) (p => p.Name == reqSearch || p.Name == resSearch)));
      }
      this.FileSystemGrid.DataSource = (object) Enumerable.ToList<Filebrouws>((IEnumerable<Filebrouws>) Enumerable.OrderByDescending<Filebrouws, DateTime>((IEnumerable<Filebrouws>) list5, (Func<Filebrouws, DateTime>) (p => p.CreationTime)));
      this.FileSystemGrid.DataBind();
    }

    protected void LOGGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.LOGGridView.PageIndex = e.NewPageIndex;
      this.LOGGridView.DataBind();
      this.LOGGridView.SelectedRowStyle.Reset();
      this.LOGGridView.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.LOGGridView.RowStyle.CssClass = "RowStyle";
    }

    protected void FileSystemGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      DateTime odData = DateTime.Now.AddDays(-1.0);
      DateTime doData = DateTime.Now;
      if (this.txtDataOd.Text != "")
        odData = Convert.ToDateTime(this.txtDataOd.Text, (IFormatProvider) new CultureInfo("mk-MK"));
      if (this.txtDataDo.Text != "")
        doData = Convert.ToDateTime(this.txtDataDo.Text, (IFormatProvider) new CultureInfo("mk-MK"));
      this.FileSystemUpdate(((INSTITUTION) this.Session["UserInstitution"]).ID.ToString(), odData, doData, "");
      this.FileSystemGrid.PageIndex = e.NewPageIndex;
      this.FileSystemGrid.DataBind();
      this.FileSystemGrid.SelectedRowStyle.Reset();
      this.FileSystemGrid.AlternatingRowStyle.CssClass = "AltRowStyle";
      this.FileSystemGrid.RowStyle.CssClass = "RowStyle";
    }

    protected void LOGGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
      if (!(e.SortExpression == "TimeRequest"))
        return;
      List<SearchLog> list1 = (List<SearchLog>) this.Session["BigListFilter"];
      List<SearchLog> list2 = new List<SearchLog>();
      foreach (SearchLog searchLog in (IEnumerable<SearchLog>) Enumerable.OrderByDescending<SearchLog, DateTime?>((IEnumerable<SearchLog>) list1, (Func<SearchLog, DateTime?>) (p => p.TimeRequest)))
        list2.Add(searchLog);
      if (list2[0].ActivityID == list1[0].ActivityID && list2[list2.Count - 1].ActivityID == list1[list1.Count - 1].ActivityID)
      {
        list2.Clear();
        foreach (SearchLog searchLog in (IEnumerable<SearchLog>) Enumerable.OrderBy<SearchLog, DateTime?>((IEnumerable<SearchLog>) list1, (Func<SearchLog, DateTime?>) (p => p.TimeRequest)))
          list2.Add(searchLog);
      }
      this.Session["BigList"] = (object) list1;
      this.Session["BigListFilter"] = (object) list2;
      this.LOGGridView.DataSource = (object) list2;
      this.LOGGridView.DataBind();
    }

    protected void LOGGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      string row = (string) this.LOGGridView.SelectedDataKey.Value;
      SearchLog SelectedLog = Enumerable.Single<SearchLog>((IEnumerable<SearchLog>) this.Session["BigList"], (Func<SearchLog, bool>) (p => p.ActivityID == row));
      this.PanelData.Visible = true;
      this.PanelTable.Visible = false;
      this.XMLSearch.Visible = false;
      this.EmptyBoxes();
      this.TextBoxActivity.Text = SelectedLog.ActivityID;
      this.TextBoxTimeRequest.Text = SelectedLog.TimeRequest.ToString();
      this.TextBoxRequestID.Text = SelectedLog.RequestID;
      this.TextBoxUserID.Text = SelectedLog.UserID;
      this.TextBoxUserName.Text = SelectedLog.UserName;
      this.TextBoxUserSurname.Text = SelectedLog.UserSurname;
      this.TextBoxEMail.Text = SelectedLog.UserEMail;
      this.TextBoxUserAct.Text = SelectedLog.UserActive;
      this.TextBoxPermision.Text = SelectedLog.PermisionUse;
      this.TextBoxPermAct.Text = SelectedLog.PermisionActive;
      this.TextBoxInstName.Text = SelectedLog.InstitutionTitle;
      this.TextBoxInstDesc.Text = SelectedLog.InstitutionDesc;
      this.TextBoxInstAct.Text = SelectedLog.InstitutionActive;
      this.TextBoxServisTittle.Text = SelectedLog.WSTitle;
      this.TextBoxServisNote.Text = SelectedLog.WSNote;
      this.TextBoxServisDesc.Text = SelectedLog.WSDesc;
      this.TextBoxServisURL.Text = SelectedLog.WSURL;
      this.TextBoxServisAct.Text = SelectedLog.WSActive;
      if (SelectedLog.TimeResponse.HasValue)
        this.TextBoxTimeResponse.Text = SelectedLog.TimeResponse.ToString();
      this.TextBoxResponseID.Text = SelectedLog.ResponseID;
      this.TextBoxResponseBody.Text = SelectedLog.ResponseBody;
      this.TextBoxRequestBasis.Text = SelectedLog.RequestBasis;
      this.PanelNewUser.CssClass = "content-box closed-box";
      this.FileSystemSelected(Enumerable.Single<ServisInst>((IEnumerable<ServisInst>) this.NapolniServisi(), (Func<ServisInst, bool>) (p => p.Servis == SelectedLog.WSTitle)).Inst, DateTime.Now, DateTime.Now, SelectedLog.RequestID);
    }

    public void EmptyBoxes()
    {
      this.TextBoxActivity.Text = string.Empty;
      this.TextBoxTimeRequest.Text = string.Empty;
      this.TextBoxRequestID.Text = string.Empty;
      this.TextBoxUserID.Text = string.Empty;
      this.TextBoxUserName.Text = string.Empty;
      this.TextBoxUserSurname.Text = string.Empty;
      this.TextBoxEMail.Text = string.Empty;
      this.TextBoxUserAct.Text = string.Empty;
      this.TextBoxPermision.Text = string.Empty;
      this.TextBoxPermAct.Text = string.Empty;
      this.TextBoxInstName.Text = string.Empty;
      this.TextBoxInstDesc.Text = string.Empty;
      this.TextBoxInstAct.Text = string.Empty;
      this.TextBoxServisTittle.Text = string.Empty;
      this.TextBoxServisNote.Text = string.Empty;
      this.TextBoxServisDesc.Text = string.Empty;
      this.TextBoxServisURL.Text = string.Empty;
      this.TextBoxTimeResponse.Text = string.Empty;
      this.TextBoxResponseID.Text = string.Empty;
      this.TextBoxResponseBody.Text = string.Empty;
      this.TextBoxRequestBasis.Text = string.Empty;
    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
      try
      {
        List<SearchLog> allSearchLog = new InteropDAL().GetAllSearchLog();
        this.Session["BigList"] = (object) allSearchLog;
        List<SearchLog> list1 = new List<SearchLog>();
        list1.AddRange((IEnumerable<SearchLog>) allSearchLog);
        this.Session["BigListFilter"] = (object) list1;
        this.LOGGridView.DataSource = (object) list1;
        this.LOGGridView.DataBind();
        List<HelpClassUsers> list2 = new List<HelpClassUsers>();
        foreach (string g in Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) allSearchLog, (Func<SearchLog, string>) (p => p.UserID))))
        {
          if (g != null)
          {
            HelpClassUsers byIdLog = new UsersDAL().GetByIDLog(new Guid(g));
            list2.Add(byIdLog);
          }
        }
        this.Session["ListLogU"] = (object) list2;
        List<INSTITUTION> list3 = new List<INSTITUTION>();
        foreach (string tittle in Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) allSearchLog, (Func<SearchLog, string>) (p => p.InstitutionTitle))))
        {
          if (tittle != null)
          {
            INSTITUTION byTittle = new InstitutionsDAL().GetByTittle(tittle);
            list3.Add(byTittle);
          }
        }
        this.Session["ListLogI"] = (object) list3;
        List<WEBSERVICE> list4 = new List<WEBSERVICE>();
        foreach (string tittle in Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) allSearchLog, (Func<SearchLog, string>) (p => p.WSTitle))))
        {
          if (tittle != null)
          {
            WEBSERVICE byTittle = new WebservicesDAL().GetByTittle(tittle);
            list4.Add(byTittle);
          }
        }
        this.Session["ListLogWS"] = (object) list4;
        this.DropDownListServices.SelectedValue = "0";
        this.DropDownListUsers.SelectedValue = "0";
        this.PanelInstitution.Visible = false;
        this.PanelService.Visible = false;
        this.PanelUsers.Visible = false;
        this.UpdateLists();
      }
      catch
      {
      }
    }

    protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!(this.DropDownListUsers.SelectedValue != "0"))
        return;
      this.PanelInstitution.Visible = false;
      this.PanelService.Visible = false;
      this.PanelUsers.Visible = true;
      this.DropDownListUsersFunction(this.DropDownListUsers.SelectedValue, this.DropDownListUsers.SelectedItem.Text);
      this.PanelNewUser.CssClass = "content-box";
    }

    public void DropDownListUsersFunction(string id, string name)
    {
      USER byId = new UsersDAL().GetByID(new Guid(id));
      List<SearchLog> list = new InteropDAL().GetTop50SearchLogByUserForLocalAdmin(byId, (INSTITUTION) this.Session["UserInstitution"]);
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      try
      {
        nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
      }
      catch
      {
      }
      try
      {
        DateTime dateTime = Convert.ToDateTime(this.txtDateTo.Text);
        dateTime = dateTime.AddHours(24.0);
        nullable2 = new DateTime?(dateTime.AddMilliseconds(-1.0));
      }
      catch
      {
      }
      if (nullable1.HasValue && nullable2.HasValue)
        list = new InteropDAL().GetSearchLogByUserForDateForLocalAdmin(byId, nullable1.Value, nullable2.Value, (INSTITUTION) this.Session["UserInstitution"]);
      List<SearchLog> helpListLogs = new List<SearchLog>();
      helpListLogs.AddRange((IEnumerable<SearchLog>) list);
      if (this.DropDownListServices.SelectedValue != "0")
      {
        foreach (SearchLog searchLog in list)
        {
          if (searchLog.WSTitle != this.DropDownListServices.SelectedItem.Text)
            helpListLogs.Remove(searchLog);
        }
      }
      this.FillStatisticsByUser(new UsersDAL().GetByIDLog(new Guid(id)), helpListLogs);
      this.Session["BigList"] = (object) helpListLogs;
      this.Session["BigListFilter"] = (object) helpListLogs;
      this.LOGGridView.DataSource = (object) helpListLogs;
      this.LOGGridView.DataBind();
    }

    protected void DropDownListServices_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!(this.DropDownListServices.SelectedValue != "0"))
        return;
      this.PanelInstitution.Visible = false;
      this.PanelService.Visible = true;
      this.PanelUsers.Visible = false;
      this.DropDownListServicesFunction(this.DropDownListServices.SelectedItem.Text);
      this.PanelNewUser.CssClass = "content-box";
    }

    public void DropDownListServicesFunction(string tittle)
    {
      WEBSERVICE byTittle = new WebservicesDAL().GetByTittle(tittle);
      List<SearchLog> list = new InteropDAL().GetTop50SearchLogByServiceForLocalAdmin(byTittle, (INSTITUTION) this.Session["UserInstitution"]);
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      try
      {
        nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
      }
      catch
      {
      }
      try
      {
        DateTime dateTime = Convert.ToDateTime(this.txtDateTo.Text);
        dateTime = dateTime.AddHours(24.0);
        nullable2 = new DateTime?(dateTime.AddMilliseconds(-1.0));
      }
      catch
      {
      }
      if (nullable1.HasValue && nullable2.HasValue)
        list = new InteropDAL().GetSearchLogByServiceForDateForLocalAdmin(byTittle, nullable1.Value, nullable2.Value, (INSTITUTION) this.Session["UserInstitution"]);
      List<SearchLog> helpListLogs = new List<SearchLog>();
      helpListLogs.AddRange((IEnumerable<SearchLog>) list);
      if (this.DropDownListUsers.SelectedValue != "0")
      {
        foreach (SearchLog searchLog in list)
        {
          if (searchLog.UserID.ToUpper() != this.DropDownListUsers.SelectedValue.ToUpper())
            helpListLogs.Remove(searchLog);
        }
      }
      this.FillStatisticsByServices(tittle, helpListLogs);
      this.Session["BigList"] = (object) helpListLogs;
      this.Session["BigListFilter"] = (object) helpListLogs;
      this.LOGGridView.DataSource = (object) helpListLogs;
      this.LOGGridView.DataBind();
    }

    public void FillStatisticsByInst(string tittle, List<SearchLog> helpListLogs, DateTime? datef, DateTime? datet)
    {
      int count = helpListLogs.Count;
      this.LabelInst.Text = tittle;
      this.lblVkBr.Text = count.ToString();
      IEnumerable<string> enumerable1 = Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) helpListLogs, (Func<SearchLog, string>) (p => p.UserID)));
      this.PanelFill1.Controls.Clear();
      using (IEnumerator<string> enumerator = enumerable1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string c = enumerator.Current;
          HelpClassUsers byIdLog = new UsersDAL().GetByIDLog(new Guid(c));
          int num = Enumerable.Count<SearchLog>(Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) helpListLogs, (Func<SearchLog, bool>) (p => p.UserID == c)));
          Label label1 = new Label();
          Panel panel = new Panel();
          Label label2 = new Label();
          label2.Text = num.ToString();
          LiteralControl literalControl1 = new LiteralControl("<p>");
          LiteralControl literalControl2 = new LiteralControl("</p>");
          this.PanelUsNum.Controls.Add((Control) literalControl1);
          this.PanelUsNum.Controls.Add((Control) label2);
          this.PanelUsNum.Controls.Add((Control) literalControl2);
          LiteralControl literalControl3 = new LiteralControl("<p>");
          LiteralControl literalControl4 = new LiteralControl("</p>");
          label1.Text = byIdLog.NameSurname;
          this.PanelFill1.Controls.Add((Control) literalControl3);
          this.PanelFill1.Controls.Add((Control) label1);
          this.PanelFill1.Controls.Add((Control) literalControl4);
        }
      }
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(new InstitutionsDAL().GetByTittle(tittle));
      if (servicesByIstitution != null)
      {
        List<SearchLog> allSearchLog = new InteropDAL().GetAllSearchLog();
        List<SearchLog> list1 = new List<SearchLog>();
        list1.AddRange((IEnumerable<SearchLog>) allSearchLog);
        if (datef.HasValue)
        {
          IEnumerable<SearchLog> enumerable2 = Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) list1, (Func<SearchLog, bool>) (p =>
          {
            DateTime? timeRequest = p.TimeRequest;
            DateTime? nullable = datef;
            return timeRequest.HasValue & nullable.HasValue && timeRequest.GetValueOrDefault() > nullable.GetValueOrDefault();
          }));
          List<SearchLog> list2 = new List<SearchLog>();
          foreach (SearchLog searchLog in enumerable2)
            list2.Add(searchLog);
          list1.Clear();
          list1.AddRange((IEnumerable<SearchLog>) list2);
        }
        if (datet.HasValue)
        {
          IEnumerable<SearchLog> enumerable2 = Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) list1, (Func<SearchLog, bool>) (p =>
          {
            DateTime? timeRequest = p.TimeRequest;
            DateTime? nullable = datet;
            return timeRequest.HasValue & nullable.HasValue && timeRequest.GetValueOrDefault() < nullable.GetValueOrDefault();
          }));
          List<SearchLog> list2 = new List<SearchLog>();
          foreach (SearchLog searchLog in enumerable2)
            list2.Add(searchLog);
          list1.Clear();
          list1.AddRange((IEnumerable<SearchLog>) list2);
        }
        this.LabelOdI.Text = !datef.HasValue ? Convert.ToDateTime((object) list1[0].TimeRequest).Date.ToString() : datef.ToString();
        if (datet.HasValue)
        {
          this.LabelDoI.Text = datet.ToString();
        }
        else
        {
          Label label = this.LabelDoI;
          DateTime dateTime = Convert.ToDateTime((object) list1[list1.Count - 1].TimeRequest);
          dateTime = dateTime.Date;
          string str = dateTime.ToString();
          label.Text = str;
        }
        this.PanelFill2.Controls.Clear();
        using (List<WEBSERVICE>.Enumerator enumerator = servicesByIstitution.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            WEBSERVICE c = enumerator.Current;
            int num = Enumerable.Count<SearchLog>(Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) list1, (Func<SearchLog, bool>) (p => p.WSTitle == c.Tittle)));
            Label label1 = new Label();
            Label label2 = new Label();
            label2.Text = num.ToString();
            label1.Text = c.Tittle;
            LiteralControl literalControl1 = new LiteralControl("<p>");
            LiteralControl literalControl2 = new LiteralControl("</p>");
            this.PanelFill2.Controls.Add((Control) literalControl1);
            this.PanelFill2.Controls.Add((Control) label1);
            this.PanelFill2.Controls.Add((Control) literalControl2);
            this.PanelSvrNum.Controls.Add((Control) new LiteralControl("<p>"));
            this.PanelSvrNum.Controls.Add((Control) label2);
            this.PanelSvrNum.Controls.Add((Control) new LiteralControl("</p>"));
          }
        }
      }
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      try
      {
        nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
      }
      catch
      {
      }
      try
      {
        nullable2 = new DateTime?(Convert.ToDateTime(this.txtDateTo.Text).AddHours(24.0).AddMilliseconds(-1.0));
      }
      catch
      {
      }
    }

    public void FillStatisticsByUser(HelpClassUsers user, List<SearchLog> helpListLogs)
    {
      int count = helpListLogs.Count;
      this.LabelUsers.Text = user.NameSurname;
      this.lblVkBrUs.Text = count.ToString();
      IEnumerable<string> enumerable = Enumerable.Distinct<string>(Enumerable.Select<SearchLog, string>((IEnumerable<SearchLog>) helpListLogs, (Func<SearchLog, string>) (p => p.WSTitle)));
      this.PanelFill3.Controls.Clear();
      using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string c = enumerator.Current;
          int num = Enumerable.Count<SearchLog>(Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) helpListLogs, (Func<SearchLog, bool>) (p => p.WSTitle == c)));
          Label label1 = new Label();
          Label label2 = new Label();
          label2.Text = num.ToString();
          label1.Text = c;
          LiteralControl literalControl1 = new LiteralControl("<p>");
          LiteralControl literalControl2 = new LiteralControl("</p>");
          this.PanelBRUSWS.Controls.Add((Control) literalControl1);
          this.PanelBRUSWS.Controls.Add((Control) label2);
          this.PanelBRUSWS.Controls.Add((Control) literalControl2);
          LiteralControl literalControl3 = new LiteralControl("<p>");
          LiteralControl literalControl4 = new LiteralControl("</p>");
          this.PanelFill3.Controls.Add((Control) literalControl3);
          this.PanelFill3.Controls.Add((Control) label1);
          this.PanelFill3.Controls.Add((Control) literalControl4);
        }
      }
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      try
      {
        nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
      }
      catch
      {
      }
      try
      {
        nullable2 = new DateTime?(Convert.ToDateTime(this.txtDateTo.Text).AddHours(24.0).AddMilliseconds(-1.0));
      }
      catch
      {
      }
      DateTime dateTime;
      if (nullable1.HasValue)
        this.LabelOdU.Text = nullable1.ToString();
      else if (helpListLogs.Count != 0)
      {
        Label label = this.LabelOdU;
        dateTime = Convert.ToDateTime((object) helpListLogs[0].TimeRequest);
        dateTime = dateTime.Date;
        string str = dateTime.ToString();
        label.Text = str;
      }
      else
        this.LabelOdU.Text = string.Empty;
      if (nullable2.HasValue)
        this.LabelDoU.Text = nullable2.ToString();
      else if (helpListLogs.Count != 0)
      {
        Label label = this.LabelDoU;
        dateTime = Convert.ToDateTime((object) helpListLogs[helpListLogs.Count - 1].TimeRequest);
        dateTime = dateTime.Date;
        string str = dateTime.ToString();
        label.Text = str;
      }
      else
        this.LabelDoU.Text = string.Empty;
    }

    public void FillStatisticsByServices(string tittle, List<SearchLog> helpListLogs)
    {
      int count1 = helpListLogs.Count;
      this.LabelService.Text = tittle;
      this.lblVkBrWS.Text = count1.ToString();
      List<INSTITUTION> allActiveDeleted = new InstitutionsDAL().GetAllActiveDeleted(true);
      this.PanelFill4.Controls.Clear();
      using (List<INSTITUTION>.Enumerator enumerator1 = allActiveDeleted.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          INSTITUTION c = enumerator1.Current;
          List<SearchLog> list = new List<SearchLog>();
          foreach (SearchLog searchLog in Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) helpListLogs, (Func<SearchLog, bool>) (p => p.InstitutionTitle == c.Tittle)))
            list.Add(searchLog);
          int count2 = list.Count;
          Label label1 = new Label();
          Label label2 = new Label();
          label2.Text = count2.ToString();
          label1.Text = c.Tittle;
          LiteralControl literalControl1 = new LiteralControl("<p>");
          LiteralControl literalControl2 = new LiteralControl("</p>");
          this.PanelFill4.Controls.Add((Control) literalControl1);
          this.PanelFill4.Controls.Add((Control) label1);
          this.PanelFill4.Controls.Add((Control) literalControl2);
          LiteralControl literalControl3 = new LiteralControl("<p>");
          LiteralControl literalControl4 = new LiteralControl("</p>");
          this.PanelInstVkBr.Controls.Add((Control) literalControl3);
          this.PanelInstVkBr.Controls.Add((Control) label2);
          this.PanelInstVkBr.Controls.Add((Control) literalControl4);
          List<HelpClassUsers> byInstitutionLog = new UsersDAL().GetUsersByInstitutionLog(c);
          if (byInstitutionLog != null)
          {
            using (List<HelpClassUsers>.Enumerator enumerator2 = byInstitutionLog.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                HelpClassUsers x = enumerator2.Current;
                int num = Enumerable.Count<SearchLog>(Enumerable.Where<SearchLog>((IEnumerable<SearchLog>) list, (Func<SearchLog, bool>) (p => p.UserNameSurname == x.NameSurname)));
                if (num != 0)
                {
                  Label label3 = new Label();
                  Label label4 = new Label();
                  Label label5 = new Label();
                  label5.Text = "(" + c.Tittle + ") ";
                  label4.Text = num.ToString();
                  label3.Text = x.NameSurname;
                  LiteralControl literalControl5 = new LiteralControl("<p>");
                  LiteralControl literalControl6 = new LiteralControl("</p>");
                  LiteralControl literalControl7 = new LiteralControl("<p>");
                  LiteralControl literalControl8 = new LiteralControl("</p>");
                  this.PanelFillUsers.Controls.Add((Control) literalControl5);
                  this.PanelFillUsers.Controls.Add((Control) label5);
                  this.PanelFillUsers.Controls.Add((Control) label3);
                  this.PanelFillUsers.Controls.Add((Control) literalControl6);
                  this.PanelVkBrUs.Controls.Add((Control) literalControl7);
                  this.PanelVkBrUs.Controls.Add((Control) label4);
                  this.PanelVkBrUs.Controls.Add((Control) literalControl8);
                }
              }
            }
          }
        }
      }
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      try
      {
        nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
      }
      catch
      {
      }
      try
      {
        DateTime dateTime = Convert.ToDateTime(this.txtDateTo.Text).AddHours(24.0);
        dateTime = dateTime.AddMilliseconds(-1.0);
        dateTime = dateTime.AddMilliseconds(-1.0);
        nullable2 = new DateTime?(dateTime.AddMilliseconds(-1.0));
      }
      catch
      {
      }
      if (nullable1.HasValue)
        this.LabelOdU.Text = nullable1.ToString();
      else if (helpListLogs.Count != 0)
      {
        Label label = this.LabelOdU;
        DateTime dateTime = Convert.ToDateTime((object) helpListLogs[0].TimeRequest);
        dateTime = dateTime.Date;
        string str = dateTime.ToString();
        label.Text = str;
      }
      else
        this.LabelOdU.Text = string.Empty;
      if (nullable2.HasValue)
        this.LabelDoU.Text = nullable2.ToString();
      else
        this.LabelDoU.Text = helpListLogs.Count == 0 ? string.Empty : Convert.ToDateTime((object) helpListLogs[helpListLogs.Count - 1].TimeRequest).Date.ToString();
    }

    protected void ButtonBack_Click(object sender, EventArgs e)
    {
      this.PanelData.Visible = false;
      this.PanelTable.Visible = true;
      string str = ((INSTITUTION) this.Session["UserInstitution"]).ID.ToString();
      if (str != "1" && str != "8" && str != "9")
        this.XMLSearch.Visible = false;
      else
        this.XMLSearch.Visible = true;
    }

    protected void ButtonDate_Click(object sender, EventArgs e)
    {
      if (!(this.DropDownListUsers.SelectedValue != "0"))
        return;
      this.DropDownListUsersFunction(this.DropDownListUsers.SelectedValue.ToString(), this.DropDownListUsers.SelectedItem.Text);
    }

    protected void ButtonDate_Click(object sender, ImageClickEventArgs e)
    {
      if (this.DropDownListUsers.SelectedValue != "0")
        this.DropDownListUsersFunction(this.DropDownListUsers.SelectedValue.ToString(), this.DropDownListUsers.SelectedItem.Text);
      else if (this.DropDownListServices.SelectedValue != "0")
      {
        this.DropDownListServicesFunction(this.DropDownListServices.SelectedItem.Text);
      }
      else
      {
        List<SearchLog> list = new InteropDAL().GetTop50SearchLogForLocalAdmin((INSTITUTION) this.Session["UserInstitution"]);
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        try
        {
          nullable1 = new DateTime?(Convert.ToDateTime(this.txtDateFrom.Text));
        }
        catch
        {
        }
        try
        {
          nullable2 = new DateTime?(Convert.ToDateTime(this.txtDateTo.Text).AddHours(24.0).AddMilliseconds(-1.0));
        }
        catch
        {
        }
        if (nullable1.HasValue && nullable2.HasValue)
          list = new InteropDAL().GetSearchLogByDateForLocalAdmin(nullable1.Value, nullable2.Value, (INSTITUTION) this.Session["UserInstitution"], (List<WEBSERVICE>) this.Session["ListLogWS"]);
        this.Session["BigListFilter"] = (object) list;
        this.Session["BigList"] = (object) list;
        this.LOGGridView.DataSource = (object) list;
        this.LOGGridView.DataBind();
      }
    }

    protected void ButtonXMLSearch_Click(object sender, EventArgs e)
    {
      this.Session["filelista"] = (object) null;
      DateTime odData = DateTime.Now.AddDays(-1.0);
      DateTime doData = DateTime.Now;
      if (this.txtDataOd.Text != "")
        odData = Convert.ToDateTime(this.txtDataOd.Text, (IFormatProvider) new CultureInfo("mk-MK"));
      if (this.txtDataDo.Text != "")
        doData = Convert.ToDateTime(this.txtDataDo.Text, (IFormatProvider) new CultureInfo("mk-MK"));
      this.FileSystemUpdateSearch(((INSTITUTION) this.Session["UserInstitution"]).ID.ToString(), odData, doData, "");
    }

    public List<ServisInst> NapolniServisi()
    {
      return new List<ServisInst>()
      {
        new ServisInst()
        {
          Servis = "Имотен Лист",
          Inst = "1"
        },
        new ServisInst()
        {
          Servis = "Податоци за Парцели",
          Inst = "1"
        },
        new ServisInst()
        {
          Servis = "Податоци за ЕМБС за потребите на АКН",
          Inst = "9"
        },
        new ServisInst()
        {
          Servis = "Податоци за ЕМБС за потребите на ЦУ",
          Inst = "9"
        },
        new ServisInst()
        {
          Servis = "Податоци за ЕМБС за потребите на УЈП",
          Inst = "9"
        },
        new ServisInst()
        {
          Servis = "Единствен Даночен Број На Субјектот",
          Inst = "8"
        },
        new ServisInst()
        {
          Servis = "Назив На Субјектот",
          Inst = "8"
        }
      };
    }

    protected void LinkXML_Click(object sender, EventArgs e)
    {
      try
      {
        string text = ((LinkButton) sender).Text;
        string[] strArray = text.Split('\\');
        string destFileName = this.Server.MapPath("\\XMLMessages\\" + strArray[strArray.Length - 1]);
        File.Copy(text, destFileName, true);
        this.RedirectTo((string) this.Application["root"] + (object) "/XMLMessages/" + strArray[strArray.Length - 1]);
      }
      catch
      {
      }
    }

    private void RedirectTo(string url)
    {
      ScriptManager.RegisterStartupScript((Control) this, typeof (Page), "RedirectTo", "window.open('" + this.Page.ResolveClientUrl(url) + "', '_newtab')", true);
    }
  }
}
