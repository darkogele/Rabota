// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.InteropDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace WebApplicationInterop
{
  public class InteropDAL
  {
    public static Dictionary<int, string> TablesDictionary = (Dictionary<int, string>) null;

    public void Set()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["InteropProduction_dbConnectionString"].ConnectionString;
      DataTable dataTable = new DataTable("Tables");
      using (SqlConnection sqlConnection = new SqlConnection(connectionString))
      {
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandText = "SELECT * FROM sysobjects WHERE type = 'U'";
        sqlConnection.Open();
        dataTable.Load((IDataReader) command.ExecuteReader(CommandBehavior.CloseConnection));
      }
      InteropDAL.TablesDictionary = new Dictionary<int, string>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow dataRow = dataTable.Rows[index];
        dataRow[0].ToString();
        InteropDAL.TablesDictionary.Add(Convert.ToInt32(dataRow[1]), dataRow[0].ToString());
      }
    }

    public APP_SETTING GetSetting()
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<APP_SETTING>((IQueryable<APP_SETTING>) classes1DataContext.APP_SETTINGs, (Expression<Func<APP_SETTING, bool>>) (p => p.ID == 1));
      }
      catch
      {
        return (APP_SETTING) null;
      }
    }

    public List<SearchObjects> Search(string searchparam, USER userLogIn)
    {
      List<SearchObjects> list1 = new List<SearchObjects>();
      List<HelpClassWebServices> list2 = new List<HelpClassWebServices>();
      List<INSTITUTION> list3 = new List<INSTITUTION>();
      string str1 = searchparam;
      if (str1 == "")
        str1 = searchparam.ToUpper();
      if (userLogIn.Type == 1)
      {
        foreach (HelpClassWebServices classWebServices in new WebservicesDAL().GetWebServicesInstitutionsPermissions(true))
        {
          string str2 = classWebServices.Tittle.ToUpper();
          if (str2 == "")
            str2 = classWebServices.Tittle.ToUpper();
          if (str2.Contains(str1.ToUpper()))
            list1.Add(new SearchObjects()
            {
              Rezult = classWebServices.Tittle,
              RezultID = classWebServices.ID,
              ResultSession = "AdminSelectedWS",
              Obj = (object) classWebServices,
              Description = classWebServices.Description
            });
        }
        foreach (INSTITUTION institution in new InstitutionsDAL().GetAllActiveDeleted(true))
        {
          string str2 = institution.Tittle.ToUpper();
          if (str2 == "")
            str2 = institution.Tittle.ToUpper();
          if (str2.Contains(str1.ToUpper()))
            list1.Add(new SearchObjects()
            {
              Rezult = institution.Tittle,
              RezultID = institution.ID,
              ResultSession = "AdminSelectedInstitution",
              Obj = (object) institution,
              Description = institution.Description
            });
        }
      }
      else if (userLogIn.Type == 2)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, userLogIn);
        foreach (HelpClassWebServices classWebServices in servicesPermissions)
        {
          string str2 = classWebServices.Tittle.ToUpper();
          if (str2 == "")
            str2 = classWebServices.Tittle.ToUpper();
          if (str2.Contains(str1.ToUpper()))
            list1.Add(new SearchObjects()
            {
              Rezult = classWebServices.Tittle,
              RezultID = classWebServices.ID,
              ResultSession = "LocalSelectedWS",
              Obj = (object) classWebServices,
              Description = classWebServices.Description
            });
        }
        foreach (long id in Enumerable.Distinct<long>(Enumerable.Select<HelpClassWebServices, long>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, long>) (p => p.IDInstitution))))
        {
          INSTITUTION byId = new InstitutionsDAL().GetByID(id);
          if (byId != null)
            list3.Add(byId);
        }
        foreach (INSTITUTION institution in list3)
        {
          if (institution != null)
          {
            string str2 = institution.Tittle.ToUpper();
            if (str2 == "")
              str2 = institution.Tittle.ToUpper();
            if (str2.Contains(str1.ToUpper()))
              list1.Add(new SearchObjects()
              {
                Rezult = institution.Tittle,
                RezultID = institution.ID,
                ResultSession = "LocalSelectedInstitution",
                Obj = (object) institution,
                Description = institution.Description
              });
          }
        }
      }
      else if (userLogIn.Type == 3)
      {
        List<HelpClassWebServices> servicesPermissions = new WebservicesDAL().GetUsersWebServicesPermissions(true, userLogIn);
        foreach (HelpClassWebServices classWebServices in servicesPermissions)
        {
          string str2 = classWebServices.Tittle.ToUpper();
          if (str2 == "")
            str2 = classWebServices.Tittle.ToUpper();
          if (str2.Contains(str1.ToUpper()))
            list1.Add(new SearchObjects()
            {
              Rezult = classWebServices.Tittle,
              RezultID = classWebServices.ID,
              ResultSession = "SelectedWS",
              Obj = (object) classWebServices,
              Description = classWebServices.Description
            });
        }
        foreach (long id in Enumerable.Distinct<long>(Enumerable.Select<HelpClassWebServices, long>((IEnumerable<HelpClassWebServices>) servicesPermissions, (Func<HelpClassWebServices, long>) (p => p.IDInstitution))))
          list3.Add(new InstitutionsDAL().GetByID(id));
        foreach (INSTITUTION institution in list3)
        {
          if (institution != null)
          {
            string str2 = institution.Tittle.ToUpper();
            if (str2 == "")
              str2 = institution.Tittle.ToUpper();
            if (str2.Contains(str1.ToUpper()))
              list1.Add(new SearchObjects()
              {
                Rezult = institution.Tittle,
                RezultID = institution.ID,
                ResultSession = "SelectedInstitution",
                Obj = (object) institution,
                Description = institution.Description
              });
          }
        }
      }
      return list1;
    }

    public List<SearchLog> GetAllSearchLog()
    {
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Select<bam_UniversalServiceControlProduction_Completed, bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bam_UniversalServiceControlProduction_Completed>>) (p => p));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLog()
    {
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Select<bam_UniversalServiceControlProduction_Completed, bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bam_UniversalServiceControlProduction_Completed>>) (p => p)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogForLocalAdmin(INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.InstitutionTittle == AdminInstitution.Tittle && ListWSString.Contains(p.WSTittle))), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByUser(USER User)
    {
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.UserID == User.ID.ToString())), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByUserForLocalAdmin(USER User, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.UserID == User.ID.ToString() && ListWSString.Contains(p.WSTittle))), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByUserForDate(USER User, DateTime DateFrom, DateTime DateTo)
    {
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.UserID == User.ID.ToString() && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByUserForDateForLocalAdmin(USER User, DateTime DateFrom, DateTime DateTo, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.UserID == User.ID.ToString() && ListWSString.Contains(p.WSTittle) && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByInstitution(INSTITUTION Institution)
    {
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.InstitutionTittle == Institution.Tittle && p.InstitutionDesc == Institution.Description && p.InstitutionActive == "true")), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByInstitutionForLocalAdmin(INSTITUTION Institution, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.InstitutionTittle == Institution.Tittle && ListWSString.Contains(p.WSTittle) && p.InstitutionDesc == Institution.Description && p.InstitutionActive == "true")), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByInstitutionForDate(INSTITUTION Institution, DateTime DateFrom, DateTime DateTo)
    {
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.InstitutionTittle == Institution.Tittle && p.InstitutionDesc == Institution.Description && p.InstitutionActive == "true" && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByInstitutionForDateForLocalAdmin(INSTITUTION Institution, DateTime DateFrom, DateTime DateTo, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.InstitutionTittle == Institution.Tittle && p.InstitutionDesc == Institution.Description && ListWSString.Contains(p.WSTittle) && p.InstitutionActive == "true" && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByService(WEBSERVICE Service)
    {
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSActive == "true" && p.WSTittle == Service.Tittle)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetTop50SearchLogByServiceForLocalAdmin(WEBSERVICE Service, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IQueryable<bam_UniversalServiceControlProduction_Completed> queryable = Queryable.Take<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSActive == "true" && ListWSString.Contains(p.WSTittle) && p.WSTittle == Service.Tittle)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest)), 50);
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) queryable)
      {
        SearchLog searchLog = new SearchLog();
        searchLog.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable;
        try
        {
          nullable = productionCompleted.TimeRequest;
          searchLog.TimeRequest = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeRequest).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.RequestID = productionCompleted.RequestID;
        searchLog.UserID = productionCompleted.UserID;
        searchLog.UserName = productionCompleted.Name;
        searchLog.UserSurname = productionCompleted.Surname;
        searchLog.UserEMail = productionCompleted.UserEMail;
        searchLog.UserActive = productionCompleted.UserActive;
        searchLog.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog.UserUsername = productionCompleted.Username;
        try
        {
          searchLog.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog.PermisionUse = "";
        }
        searchLog.PermisionActive = productionCompleted.PermisionActive;
        searchLog.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog.WSTitle = productionCompleted.WSTittle;
        searchLog.WSNote = productionCompleted.WSNote;
        searchLog.WSActive = productionCompleted.WSActive;
        searchLog.WSDesc = productionCompleted.WSDesc;
        searchLog.WSURL = productionCompleted.WSURL;
        try
        {
          nullable = productionCompleted.TimeResponse;
          searchLog.TimeResponse = nullable.Value.IsDaylightSavingTime() ? new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(2.0)) : new DateTime?(Convert.ToDateTime((object) productionCompleted.TimeResponse).AddHours(1.0));
        }
        catch
        {
          searchLog.TimeRequest = new DateTime?();
        }
        searchLog.ResponseID = productionCompleted.ResponseID;
        searchLog.ResponseBody = productionCompleted.ResponseBody;
        searchLog.RequestBasis = productionCompleted.RequestBasis;
        nullable = searchLog.TimeRequest;
        if (nullable.HasValue)
          list.Add(searchLog);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByServiceForDate(WEBSERVICE Service, DateTime? DateFrom, DateTime? DateTo)
    {
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSTittle == Service.Tittle && p.LastModified >= DateFrom && p.LastModified <= DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByServiceForDateForLocalAdmin(WEBSERVICE Service, DateTime DateFrom, DateTime DateTo, INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> ListWSString = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        ListWSString.Add(tittle);
      }
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSTittle == Service.Tittle && ListWSString.Contains(p.WSTittle) && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByDate(DateTime DateFrom, DateTime DateTo)
    {
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSActive == "true" && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        SearchLog searchLog1 = new SearchLog();
        searchLog1.ActivityID = productionCompleted.ActivityID;
        DateTime? nullable1;
        try
        {
          nullable1 = productionCompleted.TimeRequest;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeRequest = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeRequest = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.RequestID = productionCompleted.RequestID;
        searchLog1.UserID = productionCompleted.UserID;
        searchLog1.UserName = productionCompleted.Name;
        searchLog1.UserSurname = productionCompleted.Surname;
        searchLog1.UserEMail = productionCompleted.UserEMail;
        searchLog1.UserActive = productionCompleted.UserActive;
        searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
        searchLog1.UserUsername = productionCompleted.Username;
        try
        {
          searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
        }
        catch
        {
          searchLog1.PermisionUse = "";
        }
        searchLog1.PermisionActive = productionCompleted.PermisionActive;
        searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
        searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
        searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
        searchLog1.WSTitle = productionCompleted.WSTittle;
        searchLog1.WSNote = productionCompleted.WSNote;
        searchLog1.WSActive = productionCompleted.WSActive;
        searchLog1.WSDesc = productionCompleted.WSDesc;
        searchLog1.WSURL = productionCompleted.WSURL;
        try
        {
          nullable1 = productionCompleted.TimeResponse;
          DateTime dateTime = nullable1.Value;
          if (!dateTime.IsDaylightSavingTime())
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
            searchLog2.TimeResponse = nullable2;
          }
          else
          {
            SearchLog searchLog2 = searchLog1;
            dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
            DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
            searchLog2.TimeResponse = nullable2;
          }
        }
        catch
        {
          searchLog1.TimeRequest = new DateTime?();
        }
        searchLog1.ResponseID = productionCompleted.ResponseID;
        searchLog1.ResponseBody = productionCompleted.ResponseBody;
        searchLog1.RequestBasis = productionCompleted.RequestBasis;
        nullable1 = searchLog1.TimeRequest;
        if (nullable1.HasValue)
          list.Add(searchLog1);
      }
      return list;
    }

    public List<SearchLog> GetSearchLogByDateForLocalAdmin(DateTime DateFrom, DateTime DateTo, INSTITUTION AdminInstitution, List<WEBSERVICE> WebServices)
    {
      IOrderedQueryable<bam_UniversalServiceControlProduction_Completed> orderedQueryable = Queryable.OrderByDescending<bam_UniversalServiceControlProduction_Completed, DateTime?>(Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) new DataClassesBAMDataContext().bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (p => p.WSActive == "true" && p.InstitutionTittle == AdminInstitution.Tittle && p.LastModified >= (DateTime?) DateFrom && p.LastModified <= (DateTime?) DateTo)), (Expression<Func<bam_UniversalServiceControlProduction_Completed, DateTime?>>) (p => p.TimeRequest));
      List<SearchLog> list = new List<SearchLog>();
      foreach (bam_UniversalServiceControlProduction_Completed productionCompleted in (IEnumerable<bam_UniversalServiceControlProduction_Completed>) orderedQueryable)
      {
        foreach (WEBSERVICE webservice in WebServices)
        {
          if (webservice.Tittle == productionCompleted.WSTittle)
          {
            SearchLog searchLog1 = new SearchLog();
            searchLog1.ActivityID = productionCompleted.ActivityID;
            try
            {
              DateTime dateTime = productionCompleted.TimeRequest.Value;
              if (!dateTime.IsDaylightSavingTime())
              {
                SearchLog searchLog2 = searchLog1;
                dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
                DateTime? nullable = new DateTime?(dateTime.AddHours(1.0));
                searchLog2.TimeRequest = nullable;
              }
              else
              {
                SearchLog searchLog2 = searchLog1;
                dateTime = Convert.ToDateTime((object) productionCompleted.TimeRequest);
                DateTime? nullable = new DateTime?(dateTime.AddHours(2.0));
                searchLog2.TimeRequest = nullable;
              }
            }
            catch
            {
              searchLog1.TimeRequest = new DateTime?();
            }
            searchLog1.RequestID = productionCompleted.RequestID;
            searchLog1.UserID = productionCompleted.UserID;
            searchLog1.UserName = productionCompleted.Name;
            searchLog1.UserSurname = productionCompleted.Surname;
            searchLog1.UserEMail = productionCompleted.UserEMail;
            searchLog1.UserActive = productionCompleted.UserActive;
            searchLog1.UserNameSurname = productionCompleted.Name + " " + productionCompleted.Surname;
            searchLog1.UserUsername = productionCompleted.Username;
            try
            {
              searchLog1.PermisionUse = productionCompleted.PermisionUse.ToString();
            }
            catch
            {
              searchLog1.PermisionUse = "";
            }
            searchLog1.PermisionActive = productionCompleted.PermisionActive;
            searchLog1.InstitutionTitle = productionCompleted.InstitutionTittle;
            searchLog1.InstitutionDesc = productionCompleted.InstitutionDesc;
            searchLog1.InstitutionActive = productionCompleted.InstitutionActive;
            searchLog1.WSTitle = productionCompleted.WSTittle;
            searchLog1.WSNote = productionCompleted.WSNote;
            searchLog1.WSActive = productionCompleted.WSActive;
            searchLog1.WSDesc = productionCompleted.WSDesc;
            searchLog1.WSURL = productionCompleted.WSURL;
            DateTime? nullable1;
            try
            {
              nullable1 = productionCompleted.TimeResponse;
              DateTime dateTime = nullable1.Value;
              if (!dateTime.IsDaylightSavingTime())
              {
                SearchLog searchLog2 = searchLog1;
                dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
                DateTime? nullable2 = new DateTime?(dateTime.AddHours(1.0));
                searchLog2.TimeResponse = nullable2;
              }
              else
              {
                SearchLog searchLog2 = searchLog1;
                dateTime = Convert.ToDateTime((object) productionCompleted.TimeResponse);
                DateTime? nullable2 = new DateTime?(dateTime.AddHours(2.0));
                searchLog2.TimeResponse = nullable2;
              }
            }
            catch
            {
              searchLog1.TimeRequest = new DateTime?();
            }
            searchLog1.ResponseID = productionCompleted.ResponseID;
            searchLog1.ResponseBody = productionCompleted.ResponseBody;
            searchLog1.RequestBasis = productionCompleted.RequestBasis;
            nullable1 = searchLog1.TimeRequest;
            if (nullable1.HasValue)
              list.Add(searchLog1);
          }
        }
      }
      return list;
    }

    public ComputedTransaction GetTransaction(string Username, string ServiceName)
    {
        Thread.Sleep(3000);
        DataClassesBAMDataContext dataClassesBAMDataContext = new DataClassesBAMDataContext();
        long RecordId = (
            from p in dataClassesBAMDataContext.bam_UniversalServiceControlProduction_Completeds
            select p.RecordID).Max<long>();
        IQueryable<ComputedTransaction> queryable =
            from p in dataClassesBAMDataContext.bam_UniversalServiceControlProduction_Completeds
            where p.RecordID == RecordId && p.Username == Username && p.WSTittle == ServiceName
            select new ComputedTransaction
            {
                ActivityID = p.ActivityID,
                InstitutionActive = p.InstitutionActive,
                InstitutionDesc = p.InstitutionDesc,
                InstitutionTittle = p.InstitutionTittle,
                LastModified = p.LastModified.ToString(),
                Name = p.Name,
                PermisionActive = p.PermisionActive,
                PermisionUse = p.PermisionUse.ToString(),
                RecordID = p.RecordID,
                RequestID = p.RequestID,
                ResponseBody = p.ResponseID,
                RequestBasis = p.RequestBasis,
                Surname = p.Surname,
                TimeRequest = p.TimeRequest,
                TimeResponse = p.TimeResponse,
                UserActive = p.UserActive,
                UserEMail = p.UserEMail,
                UserID = p.UserID,
                Username = p.Username,
                WSActive = p.WSActive,
                WSDesc = p.WSDesc,
                WSNote = p.WSNote,
                WSTittle = p.WSTittle,
                WSURL = p.WSURL
            };
        ComputedTransaction computedTransaction = new ComputedTransaction();
        foreach (ComputedTransaction current in queryable)
        {
            computedTransaction.ActivityID = current.ActivityID;
            computedTransaction.InstitutionActive = current.InstitutionActive;
            computedTransaction.InstitutionDesc = current.InstitutionDesc;
            computedTransaction.InstitutionTittle = current.InstitutionTittle;
            computedTransaction.LastModified = current.LastModified;
            computedTransaction.Name = current.Name;
            computedTransaction.PermisionActive = current.PermisionActive;
            computedTransaction.PermisionUse = current.PermisionUse;
            computedTransaction.RecordID = current.RecordID;
            computedTransaction.RequestBasis = current.RequestBasis;
            computedTransaction.RequestID = current.RequestID;
            computedTransaction.ResponseBody = current.ResponseBody;
            computedTransaction.Surname = current.Surname;
            try
            {
                if (!current.TimeRequest.Value.IsDaylightSavingTime())
                {
                    computedTransaction.TimeRequest = new DateTime?(Convert.ToDateTime(current.TimeRequest).AddHours(1.0));
                }
                else
                {
                    computedTransaction.TimeRequest = new DateTime?(Convert.ToDateTime(current.TimeRequest).AddHours(2.0));
                }
            }
            catch
            {
                computedTransaction.TimeRequest = null;
            }
            try
            {
                if (!current.TimeResponse.Value.IsDaylightSavingTime())
                {
                    computedTransaction.TimeResponse = new DateTime?(Convert.ToDateTime(current.TimeResponse).AddHours(1.0));
                }
                else
                {
                    computedTransaction.TimeResponse = new DateTime?(Convert.ToDateTime(current.TimeResponse).AddHours(2.0));
                }
            }
            catch
            {
                computedTransaction.TimeResponse = null;
            }
            computedTransaction.UserActive = current.UserActive;
            computedTransaction.UserEMail = current.UserEMail;
            computedTransaction.UserID = current.UserID;
            computedTransaction.Username = current.Username;
            computedTransaction.WSActive = current.WSActive;
            computedTransaction.WSDesc = current.WSDesc;
            computedTransaction.WSNote = current.WSNote;
            computedTransaction.WSTittle = current.WSTittle;
            computedTransaction.WSURL = current.WSURL;
        }
        long arg_901_0 = computedTransaction.RecordID;
        if (computedTransaction.RecordID == 0L || !computedTransaction.TimeRequest.HasValue)
        {
            this.GetTransaction(Username, ServiceName);
        }
        return computedTransaction;
    }

    public void InsertToReportAndLog(USER user, string RecordID, pdfReport report, DateTime ReportCreatedOn)
    {
      try
      {
        long num = new ReportsDAL().Insert(user, RecordID, report.PdfFilePath, report.PdfFileName, ReportCreatedOn);
        new LOGDAL().Insert(Enumerable.Single<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>) InteropDAL.TablesDictionary, (Func<KeyValuePair<int, string>, bool>) (p => p.Value == "REPORTS")).Key, user, num.ToString(), 1, DateTime.Now, string.Empty, string.Empty);
      }
      catch
      {
      }
    }
  }
}
