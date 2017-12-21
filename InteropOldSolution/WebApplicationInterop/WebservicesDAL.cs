// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.WebservicesDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebApplicationInterop
{
  public class WebservicesDAL
  {
    public WEBSERVICE GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<WEBSERVICE>((IQueryable<WEBSERVICE>) classes1DataContext.WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (WEBSERVICE) null;
      }
    }

    public WEBSERVICE GetByTittle(string tittle)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<WEBSERVICE>((IQueryable<WEBSERVICE>) classes1DataContext.WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.Tittle == tittle));
      }
      catch
      {
        return (WEBSERVICE) null;
      }
    }

    public HelpClassWebServices GetByIDPermisions(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        WEBSERVICE ws = Queryable.Single<WEBSERVICE>((IQueryable<WEBSERVICE>) classes1DataContext.WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.ID == id));
        HelpClassWebServices classWebServices = new HelpClassWebServices();
        List<PERMISSION> permisionsByWs = new PermissionsDAL().GetPermisionsByWS(ws);
        classWebServices.ID = ws.ID;
        classWebServices.Tittle = ws.Tittle;
        classWebServices.Description = ws.Description;
        classWebServices.Note = ws.Note;
        classWebServices.URL = ws.URL;
        classWebServices.Active = ws.Active;
        classWebServices.CreatedOn = ws.CreatedOn;
        classWebServices.WSObj = ws;
        classWebServices.IDInstitution = 0L;
        classWebServices.InstitutionName = "x";
        if (permisionsByWs.Count != 0)
        {
          classWebServices.IDInstitution = permisionsByWs[0].INSTITUTION.ID;
          classWebServices.InstitutionName = permisionsByWs[0].INSTITUTION.Tittle;
        }
        classWebServices.PermissionList = permisionsByWs;
        return classWebServices;
      }
      catch
      {
        return (HelpClassWebServices) null;
      }
    }

    public List<WEBSERVICE> GetAllActiveDeleted(bool sign)
    {
      IQueryable<WEBSERVICE> queryable = Queryable.Where<WEBSERVICE>((IQueryable<WEBSERVICE>) new DataClasses1DataContext().WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.Active == sign));
      List<WEBSERVICE> list = new List<WEBSERVICE>();
      foreach (WEBSERVICE webservice in (IEnumerable<WEBSERVICE>) queryable)
        list.Add(webservice);
      return list;
    }

    public List<WEBSERVICE> GetAllByInstitutionPermission(long ID_Inst)
    {
        DataClasses1DataContext pom = new DataClasses1DataContext();
        IQueryable<WEBSERVICE> queryable = (
            from p in pom.PERMISSIONs
            from i in pom.INSTITUTIONs
            from w in pom.WEBSERVICEs
            where p.ID_INST == i.ID && p.ID_WS == w.ID && p.ID_INST == ID_Inst && p.Active == true && i.Active == true
            select w).Distinct<WEBSERVICE>();
        List<WEBSERVICE> list = new List<WEBSERVICE>();
        foreach (WEBSERVICE current in queryable)
        {
            list.Add(current);
        }
        return list;
    }

    public List<WEBSERVICE> GetServicesByIstitution(INSTITUTION inst)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<WEBSERVICE> allActiveDeleted = this.GetAllActiveDeleted(true);
      List<WEBSERVICE> list = new List<WEBSERVICE>();
      using (List<WEBSERVICE>.Enumerator enumerator = allActiveDeleted.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WEBSERVICE c = enumerator.Current;
          IQueryable<PERMISSION> source = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.INSTITUTION.ID == inst.ID && p.ID_WS == c.ID && p.WEBSERVICE.Active == true));
          try
          {
            if (Queryable.Count<PERMISSION>(source) != 0)
              list.Add(c);
          }
          catch
          {
          }
        }
      }
      return list;
    }

    public List<WEBSERVICE> GetAllOtherWebServices(INSTITUTION inst)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<WEBSERVICE> allActiveDeleted = this.GetAllActiveDeleted(true);
      List<WEBSERVICE> list = new List<WEBSERVICE>();
      using (List<WEBSERVICE>.Enumerator enumerator = allActiveDeleted.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WEBSERVICE c = enumerator.Current;
          if (Queryable.Count<PERMISSION>(Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.INSTITUTION.ID == inst.ID && p.Usage == 1 && p.ID_WS == c.ID))) == 0)
            list.Add(c);
        }
      }
      return list;
    }

    public List<HelpClassWebServices> GetWebServicesInstitutionsPermissions(bool active)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassWebServices> list = new List<HelpClassWebServices>();
      try
      {
        foreach (WEBSERVICE ws in this.GetAllActiveDeleted(active))
        {
          HelpClassWebServices classWebServices = new HelpClassWebServices();
          List<PERMISSION> permisionsByWs = new PermissionsDAL().GetPermisionsByWS(ws);
          classWebServices.ID = ws.ID;
          classWebServices.Tittle = ws.Tittle;
          classWebServices.Description = ws.Description;
          classWebServices.Note = ws.Note;
          classWebServices.URL = ws.URL;
          classWebServices.Active = ws.Active;
          classWebServices.CreatedOn = ws.CreatedOn;
          classWebServices.WSObj = ws;
          classWebServices.IDInstitution = 0L;
          classWebServices.InstitutionName = "x";
          if (permisionsByWs.Count != 0)
          {
            classWebServices.IDInstitution = permisionsByWs[0].INSTITUTION.ID;
            classWebServices.InstitutionName = permisionsByWs[0].INSTITUTION.Tittle;
          }
          classWebServices.PermissionList = permisionsByWs;
          list.Add(classWebServices);
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public List<HelpClassWebServices> GetUsersWebServicesPermissions(bool active, USER user)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassWebServices> list = new List<HelpClassWebServices>();
      try
      {
        foreach (PERMISSION permission in new PermissionsDAL().GetPermisionsByUser(user))
        {
          if (permission.WEBSERVICE.Active == active && permission.Active)
          {
            HelpClassWebServices classWebServices = new HelpClassWebServices();
            List<PERMISSION> permisionsByWs = new PermissionsDAL().GetPermisionsByWS(permission.WEBSERVICE);
            classWebServices.ID = permission.WEBSERVICE.ID;
            classWebServices.Tittle = permission.WEBSERVICE.Tittle;
            classWebServices.Description = permission.WEBSERVICE.Description;
            classWebServices.Note = permission.WEBSERVICE.Note;
            classWebServices.URL = permission.WEBSERVICE.URL;
            classWebServices.Active = permission.WEBSERVICE.Active;
            classWebServices.CreatedOn = permission.WEBSERVICE.CreatedOn;
            classWebServices.WSObj = permission.WEBSERVICE;
            classWebServices.IDInstitution = 0L;
            classWebServices.InstitutionName = "x";
            if (permisionsByWs.Count != 0)
            {
              classWebServices.IDInstitution = permisionsByWs[0].INSTITUTION.ID;
              classWebServices.InstitutionName = permisionsByWs[0].INSTITUTION.Tittle;
            }
            classWebServices.PermissionList = permisionsByWs;
            list.Add(classWebServices);
          }
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public List<HelpClassWebServices> GetUsersWebServicesPermissions(bool active, USER user, INSTITUTION inst)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      List<HelpClassWebServices> list = new List<HelpClassWebServices>();
      try
      {
        foreach (PERMISSION permission in new PermissionsDAL().GetPermisionsByUser(user))
        {
          if (permission.WEBSERVICE.Active == active && permission.Active)
          {
            HelpClassWebServices classWebServices = new HelpClassWebServices();
            List<PERMISSION> permisionsByWs = new PermissionsDAL().GetPermisionsByWS(permission.WEBSERVICE);
            classWebServices.ID = permission.WEBSERVICE.ID;
            classWebServices.Tittle = permission.WEBSERVICE.Tittle;
            classWebServices.Description = permission.WEBSERVICE.Description;
            classWebServices.Note = permission.WEBSERVICE.Note;
            classWebServices.URL = permission.WEBSERVICE.URL;
            classWebServices.Active = permission.WEBSERVICE.Active;
            classWebServices.CreatedOn = permission.WEBSERVICE.CreatedOn;
            classWebServices.WSObj = permission.WEBSERVICE;
            classWebServices.IDInstitution = 0L;
            classWebServices.InstitutionName = "x";
            classWebServices.PermissionList = permisionsByWs;
            if (permisionsByWs.Count != 0)
            {
              classWebServices.IDInstitution = permisionsByWs[0].INSTITUTION.ID;
              classWebServices.InstitutionName = permisionsByWs[0].INSTITUTION.Tittle;
              if (classWebServices.IDInstitution == inst.ID)
                list.Add(classWebServices);
            }
          }
        }
        return list;
      }
      catch
      {
        return list;
      }
    }

    public long Insert(string tittle, string desc, string note, string url, bool active, DateTime created)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      WEBSERVICE entity = new WEBSERVICE();
      entity.Tittle = tittle;
      entity.Description = desc;
      entity.Note = note;
      entity.URL = url;
      entity.Active = active;
      entity.CreatedOn = created;
      classes1DataContext.WEBSERVICEs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(string tittle, string desc, string note, string url, bool? active, DateTime? created, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      WEBSERVICE webservice = Queryable.Single<WEBSERVICE>((IQueryable<WEBSERVICE>) classes1DataContext.WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.ID == id));
      if (tittle != null)
        webservice.Tittle = Convert.ToString(tittle);
      if (desc != null)
        webservice.Description = Convert.ToString(desc);
      if (note != null)
        webservice.Note = Convert.ToString(note);
      if (url != null)
        webservice.URL = Convert.ToString(url);
      if (active.HasValue)
        webservice.Active = Convert.ToBoolean((object) active);
      if (created.HasValue)
        webservice.CreatedOn = Convert.ToDateTime((object) created);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<WEBSERVICE>((IQueryable<WEBSERVICE>) classes1DataContext.WEBSERVICEs, (Expression<Func<WEBSERVICE, bool>>) (p => p.ID == (long) id)).Active = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
