// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.UsersDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplicationInterop
{
  public class UsersDAL
  {
    public USER GetByID(Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (USER) null;
      }
    }

    public HelpClassUsers GetByIDLog(Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        USER user = Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id));
        return new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user,
          ip = user.IpAdress
        };
      }
      catch
      {
        return (HelpClassUsers) null;
      }
    }

    public HelpClassUsers GetByIDPermisions(Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        USER user = Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id));
        List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
        HelpClassUsers helpClassUsers = new HelpClassUsers();
        helpClassUsers.ID = user.ID;
        helpClassUsers.Name = user.Name;
        helpClassUsers.Surname = user.Surname;
        helpClassUsers.NameSurname = user.Name + " " + user.Surname;
        helpClassUsers.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
        helpClassUsers.Email = user.email;
        helpClassUsers.Active = user.Active;
        helpClassUsers.CreatedOn = user.CreateOn;
        helpClassUsers.user = user.username;
        helpClassUsers.pass = user.password;
        helpClassUsers.Type = user.Type;
        helpClassUsers.UserObj = user;
        helpClassUsers.ip = user.IpAdress;
        if (permisionsByUser.Count != 0)
        {
          helpClassUsers.IDInstitution = permisionsByUser[0].INSTITUTION.ID;
          helpClassUsers.InstitutionName = permisionsByUser[0].INSTITUTION.Tittle;
        }
        helpClassUsers.PermissionList = permisionsByUser;
        return helpClassUsers;
      }
      catch
      {
        return (HelpClassUsers) null;
      }
    }

    public List<HelpClassUsers> GetUsersByWebServiceAndUsage(long WS_ID, int usage)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      IQueryable<PERMISSION> source = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.ID_WS == WS_ID && p.Usage == usage));
      Expression<Func<PERMISSION, USER>> selector = (Expression<Func<PERMISSION, USER>>) (p => p.USER);
      foreach (USER user in (IEnumerable<USER>) Queryable.Select<PERMISSION, USER>(source, selector))
        list.Add(new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user,
          ip = user.IpAdress
        });
      return list;
    }

    public List<HelpClassUsers> GetAllActiveDeleted(bool sign)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      Table<USER> useRs = classes1DataContext.USERs;
      Expression<Func<USER, bool>> predicate = (Expression<Func<USER, bool>>) (p => p.Active == sign);
      foreach (USER user in (IEnumerable<USER>) Queryable.Where<USER>((IQueryable<USER>) useRs, predicate))
        list.Add(new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user
        });
      return list;
    }

    public List<HelpClassUsers> GetUsersByInstitution(INSTITUTION inst)
    {
      IQueryable<USER> queryable = Queryable.Distinct<USER>(Queryable.Select<PERMISSION, USER>(Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.INSTITUTION.ID == inst.ID && p.USER.Active == true)), (Expression<Func<PERMISSION, USER>>) (p => p.USER)));
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      foreach (USER user in (IEnumerable<USER>) queryable)
        list.Add(new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user,
          ip = user.IpAdress
        });
      return list;
    }

    public List<HelpClassUsers> GetUsersByInstitutionLog(INSTITUTION inst)
    {
      IQueryable<USER> queryable = Queryable.Distinct<USER>(Queryable.Select<PERMISSION, USER>(Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.INSTITUTION.ID == inst.ID && p.USER.Active == true)), (Expression<Func<PERMISSION, USER>>) (p => p.USER)));
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      foreach (USER user in (IEnumerable<USER>) queryable)
      {
        HelpClassUsers helpClassUsers = new HelpClassUsers();
        helpClassUsers.ID = user.ID;
        helpClassUsers.Name = user.Name;
        helpClassUsers.Surname = user.Surname;
        helpClassUsers.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
        helpClassUsers.NameSurname = user.Name + " " + user.Surname;
        helpClassUsers.Email = user.email;
        helpClassUsers.Active = user.Active;
        helpClassUsers.CreatedOn = user.CreateOn;
        helpClassUsers.user = user.username;
        helpClassUsers.pass = user.password;
        helpClassUsers.Type = user.Type;
        helpClassUsers.UserObj = user;
        helpClassUsers.ip = user.IpAdress;
        if (helpClassUsers.Active)
          list.Add(helpClassUsers);
      }
      return list;
    }

    public List<HelpClassUsers> GetUsersByWS(WEBSERVICE ws)
    {
      IQueryable<USER> queryable = Queryable.Distinct<USER>(Queryable.Select<PERMISSION, USER>(Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Usage == 1 && p.Active == true && p.ID_WS == ws.ID && p.USER.Active == true)), (Expression<Func<PERMISSION, USER>>) (p => p.USER)));
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      foreach (USER user in (IEnumerable<USER>) queryable)
        list.Add(new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user,
          ip = user.IpAdress
        });
      return list;
    }

    public List<HelpClassUsers> GetUsersByWSLog()
    {
      DataClassesBAMDataContext classesBamDataContext = new DataClassesBAMDataContext();
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      Table<bam_UniversalServiceControlProduction_Completed> productionCompleteds = classesBamDataContext.bam_UniversalServiceControlProduction_Completeds;
      Expression<Func<bam_UniversalServiceControlProduction_Completed, string>> selector = (Expression<Func<bam_UniversalServiceControlProduction_Completed, string>>) (log => log.UserID);
      foreach (string g in (IEnumerable<string>) Queryable.Distinct<string>(Queryable.Select<bam_UniversalServiceControlProduction_Completed, string>((IQueryable<bam_UniversalServiceControlProduction_Completed>) productionCompleteds, selector)))
      {
        if (g != null)
        {
          USER byId = new UsersDAL().GetByID(new Guid(g));
          list.Add(new HelpClassUsers()
          {
            ID = byId.ID,
            Name = byId.Name,
            Surname = byId.Surname,
            ID_Cert = Convert.ToInt64((object) byId.ID_CERT),
            NameSurname = byId.Name + " " + byId.Surname,
            Email = byId.email,
            Active = byId.Active,
            CreatedOn = byId.CreateOn,
            user = byId.username,
            pass = byId.password,
            Type = byId.Type,
            UserObj = byId
          });
        }
      }
      return list;
    }

    public List<HelpClassUsers> GetUsersByWSLogForLocalAdmin(INSTITUTION AdminInstitution)
    {
      List<WEBSERVICE> servicesByIstitution = new WebservicesDAL().GetServicesByIstitution(AdminInstitution);
      List<string> list1 = new List<string>();
      foreach (WEBSERVICE webservice in servicesByIstitution)
      {
        string tittle = webservice.Tittle;
        list1.Add(tittle);
      }
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      DataClassesBAMDataContext classesBamDataContext = new DataClassesBAMDataContext();
      List<HelpClassUsers> list2 = new List<HelpClassUsers>();
      List<HelpClassUsers> list3 = new List<HelpClassUsers>();
      using (List<string>.Enumerator enumerator1 = list1.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          string w = enumerator1.Current;
          IQueryable<bam_UniversalServiceControlProduction_Completed> source1 = Queryable.Where<bam_UniversalServiceControlProduction_Completed>((IQueryable<bam_UniversalServiceControlProduction_Completed>) classesBamDataContext.bam_UniversalServiceControlProduction_Completeds, (Expression<Func<bam_UniversalServiceControlProduction_Completed, bool>>) (log => log.WSTittle == w && log.InstitutionTittle == AdminInstitution.Tittle));
          Expression<Func<bam_UniversalServiceControlProduction_Completed, string>> selector1 = (Expression<Func<bam_UniversalServiceControlProduction_Completed, string>>) (log => log.UserID);
          using (IEnumerator<string> enumerator2 = Queryable.Distinct<string>(Queryable.Select<bam_UniversalServiceControlProduction_Completed, string>(source1, selector1)).GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              string b = enumerator2.Current;
              IQueryable<PERMISSION> source2 = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID_USER.ToString() == b));
              Expression<Func<PERMISSION, USER>> selector2 = (Expression<Func<PERMISSION, USER>>) (p => p.USER);
              foreach (USER user in (IEnumerable<USER>) Queryable.Distinct<USER>(Queryable.Select<PERMISSION, USER>(source2, selector2)))
              {
                HelpClassUsers helpClassUsers1 = new HelpClassUsers();
                helpClassUsers1.ID = user.ID;
                helpClassUsers1.Name = user.Name;
                helpClassUsers1.Surname = user.Surname;
                helpClassUsers1.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
                helpClassUsers1.NameSurname = user.Name + " " + user.Surname;
                helpClassUsers1.Email = user.email;
                helpClassUsers1.Active = user.Active;
                helpClassUsers1.CreatedOn = user.CreateOn;
                helpClassUsers1.user = user.username;
                helpClassUsers1.pass = user.password;
                helpClassUsers1.Type = user.Type;
                helpClassUsers1.UserObj = user;
                if (list3.Count != 0)
                {
                  foreach (HelpClassUsers helpClassUsers2 in list3)
                  {
                    if (helpClassUsers2.ID == helpClassUsers1.ID)
                      list2.Add(helpClassUsers1);
                  }
                }
                list3.Add(helpClassUsers1);
              }
            }
          }
          if (list2.Count != 0)
          {
            foreach (HelpClassUsers helpClassUsers in list2)
              list3.Remove(helpClassUsers);
          }
        }
      }
      return list3;
    }

    public List<HelpClassUsers> GetUsersByWSNew(WEBSERVICE ws)
    {
      IQueryable<USER> queryable = Queryable.Distinct<USER>(Queryable.Select<PERMISSION, USER>(Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.ID_WS == ws.ID && p.USER.Active == true)), (Expression<Func<PERMISSION, USER>>) (p => p.USER)));
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      foreach (USER user in (IEnumerable<USER>) queryable)
        list.Add(new HelpClassUsers()
        {
          ID = user.ID,
          Name = user.Name,
          Surname = user.Surname,
          ID_Cert = Convert.ToInt64((object) user.ID_CERT),
          NameSurname = user.Name + " " + user.Surname,
          Email = user.email,
          Active = user.Active,
          CreatedOn = user.CreateOn,
          user = user.username,
          pass = user.password,
          Type = user.Type,
          UserObj = user,
          ip = user.IpAdress
        });
      return list;
    }

    public List<USER> GetUsersByTypeActive(int type, bool sign)
    {
      IQueryable<USER> queryable = Queryable.Where<USER>((IQueryable<USER>) new DataClasses1DataContext().USERs, (Expression<Func<USER, bool>>) (p => p.Type == type && p.Active == sign));
      List<USER> list = new List<USER>();
      foreach (USER user in (IEnumerable<USER>) queryable)
        list.Add(user);
      return list;
    }

    public USER CheckUser(string user, string pass)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.username == user && p.password == pass && p.Active == true));
      }
      catch
      {
        return (USER) null;
      }
    }

    public bool? CheckUserNameExists(string UserName)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        if (Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.username == UserName)) != null)
          return new bool?(true);
        return new bool?(false);
      }
      catch (Exception ex)
      {
        if (ex.Message == "Sequence contains no elements")
          return new bool?(false);
        if (ex.Message == "Sequence contains more than one element")
          return new bool?(true);
        return new bool?();
      }
    }

    public List<HelpClassUsers> GetUsersInstitutionsPermissions(int usertype, bool active)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      try
      {
        foreach (USER user in this.GetUsersByTypeActive(usertype, active))
        {
          HelpClassUsers helpClassUsers = new HelpClassUsers();
          List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
          helpClassUsers.ID = user.ID;
          helpClassUsers.Name = user.Name;
          helpClassUsers.Surname = user.Surname;
          helpClassUsers.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
          helpClassUsers.NameSurname = user.Name + " " + user.Surname;
          helpClassUsers.Email = user.email;
          helpClassUsers.Active = user.Active;
          helpClassUsers.CreatedOn = user.CreateOn;
          helpClassUsers.user = user.username;
          helpClassUsers.pass = user.password;
          helpClassUsers.Type = user.Type;
          helpClassUsers.UserObj = user;
          helpClassUsers.ip = user.IpAdress;
          if (permisionsByUser.Count != 0)
          {
            helpClassUsers.IDInstitution = permisionsByUser[0].INSTITUTION.ID;
            helpClassUsers.InstitutionName = permisionsByUser[0].INSTITUTION.Tittle;
          }
          helpClassUsers.PermissionList = permisionsByUser;
          list.Add(helpClassUsers);
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public List<HelpClassUsers> GetUsersPermissionsByInstitution(bool active, INSTITUTION inst)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      try
      {
        foreach (USER user in this.GetUsersByTypeActive(3, active))
        {
          HelpClassUsers helpClassUsers = new HelpClassUsers();
          List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
          helpClassUsers.ID = user.ID;
          helpClassUsers.Name = user.Name;
          helpClassUsers.Surname = user.Surname;
          helpClassUsers.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
          helpClassUsers.NameSurname = user.Name + " " + user.Surname;
          helpClassUsers.Email = user.email;
          helpClassUsers.Active = user.Active;
          helpClassUsers.CreatedOn = user.CreateOn;
          helpClassUsers.user = user.username;
          helpClassUsers.pass = user.password;
          helpClassUsers.Type = user.Type;
          helpClassUsers.UserObj = user;
          helpClassUsers.ip = user.IpAdress;
          if (permisionsByUser.Count != 0)
          {
            helpClassUsers.IDInstitution = permisionsByUser[0].INSTITUTION.ID;
            helpClassUsers.InstitutionName = permisionsByUser[0].INSTITUTION.Tittle;
            helpClassUsers.PermissionList = permisionsByUser;
            if (helpClassUsers.IDInstitution == inst.ID)
              list.Add(helpClassUsers);
          }
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public List<HelpClassUsers> GetUsersPermissionsByInstitutionNew(bool active, INSTITUTION inst)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassUsers> list = new List<HelpClassUsers>();
      try
      {
        foreach (USER user in this.GetUsersByTypeActive(2, active))
        {
          HelpClassUsers helpClassUsers = new HelpClassUsers();
          List<PERMISSION> permisionsByUser = new PermissionsDAL().GetPermisionsByUser(user);
          helpClassUsers.ID = user.ID;
          helpClassUsers.Name = user.Name;
          helpClassUsers.Surname = user.Surname;
          helpClassUsers.ID_Cert = Convert.ToInt64((object) user.ID_CERT);
          helpClassUsers.NameSurname = user.Name + " " + user.Surname;
          helpClassUsers.Email = user.email;
          helpClassUsers.Active = user.Active;
          helpClassUsers.CreatedOn = user.CreateOn;
          helpClassUsers.user = user.username;
          helpClassUsers.pass = user.password;
          helpClassUsers.Type = user.Type;
          helpClassUsers.UserObj = user;
          helpClassUsers.ip = user.IpAdress;
          if (permisionsByUser.Count != 0)
          {
            helpClassUsers.IDInstitution = permisionsByUser[0].INSTITUTION.ID;
            helpClassUsers.InstitutionName = permisionsByUser[0].INSTITUTION.Tittle;
            helpClassUsers.PermissionList = permisionsByUser;
            if (helpClassUsers.IDInstitution == inst.ID)
              list.Add(helpClassUsers);
          }
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public Guid Insert(string name, string surname, long? ID_Cert, string em, bool active, DateTime created, string user, string pass, int type, string ip)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      USER entity = new USER();
      entity.ID = Guid.NewGuid();
      entity.Name = name;
      entity.Surname = surname;
      if (ID_Cert.HasValue)
        entity.ID_CERT = new long?(Convert.ToInt64((object) ID_Cert));
      entity.email = em;
      entity.Active = active;
      entity.CreateOn = created;
      entity.username = user;
      entity.password = pass;
      entity.Type = type;
      entity.ModifiedAt = new DateTime?(DateTime.Now);
      entity.IpAdress = ip;
      classes1DataContext.USERs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public int InsertTemp(string Username, string Password, string ip, string Certificate, DateTime created)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      TempUser entity = new TempUser();
      entity.Username = Username;
      entity.Password = Password;
      entity.Ip = ip;
      entity.Certificate = Certificate;
      entity.DateCreated = new DateTime?(created);
      classes1DataContext.TempUsers.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(string name, string surname, long? ID_Cert, string em, bool? active, DateTime? created, string user, string pass, int? type, string ip, Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      USER user1 = Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id));
      if (name != null)
        user1.Name = Convert.ToString(name);
      if (surname != null)
        user1.Surname = Convert.ToString(surname);
      if (ID_Cert.HasValue)
        user1.ID_CERT = new long?(Convert.ToInt64((object) ID_Cert));
      if (em != null)
        user1.email = Convert.ToString(em);
      if (active.HasValue)
        user1.Active = Convert.ToBoolean((object) active);
      if (created.HasValue)
        user1.CreateOn = Convert.ToDateTime((object) created);
      if (user != null)
        user1.username = Convert.ToString(user);
      if (pass != null)
        user1.password = Convert.ToString(pass);
      if (type.HasValue)
        user1.Type = Convert.ToInt32((object) type);
      if (ip != null)
        user1.IpAdress = ip;
      user1.ModifiedAt = new DateTime?(DateTime.Now);
      classes1DataContext.SubmitChanges();
    }

    public void UpdateIp(string ip, Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id)).IpAdress = ip;
      classes1DataContext.SubmitChanges();
    }

    public void Delete(Guid id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<USER>((IQueryable<USER>) classes1DataContext.USERs, (Expression<Func<USER, bool>>) (p => p.ID == id)).Active = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
