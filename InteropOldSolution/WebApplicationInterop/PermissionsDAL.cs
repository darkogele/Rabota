// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.PermissionsDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplicationInterop
{
  public class PermissionsDAL
  {
    public PERMISSION GetByID(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID == (long) id));
      }
      catch
      {
        return (PERMISSION) null;
      }
    }

    public List<PERMISSION> GetAllActiveDeleted(bool sign)
    {
      IQueryable<PERMISSION> queryable = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == sign));
      List<PERMISSION> list = new List<PERMISSION>();
      foreach (PERMISSION permission in (IEnumerable<PERMISSION>) queryable)
        list.Add(permission);
      return list;
    }

    public List<PERMISSION> GetPermisionsByUser(USER user)
    {
      IQueryable<PERMISSION> queryable = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID_USER == user.ID));
      List<PERMISSION> list = new List<PERMISSION>();
      foreach (PERMISSION permission in (IEnumerable<PERMISSION>) queryable)
        list.Add(permission);
      return list;
    }

    public List<PERMISSION> GetPermisionsByInstitutionandWebService(long ID_INST, long ID_WS)
    {
      IQueryable<PERMISSION> queryable = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID_INST == ID_INST && p.ID_WS == ID_WS && p.Active == true));
      List<PERMISSION> list = new List<PERMISSION>();
      foreach (PERMISSION permission in (IEnumerable<PERMISSION>) queryable)
        list.Add(permission);
      return list;
    }

    public List<PERMISSION> GetPermisionsByWS(WEBSERVICE ws)
    {
      IQueryable<PERMISSION> queryable = Queryable.Where<PERMISSION>((IQueryable<PERMISSION>) new DataClasses1DataContext().PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.Active == true && p.WEBSERVICE.ID == ws.ID && p.Usage == 1));
      List<PERMISSION> list = new List<PERMISSION>();
      foreach (PERMISSION permission in (IEnumerable<PERMISSION>) queryable)
        list.Add(permission);
      return list;
    }

    public long Insert(INSTITUTION inst, USER user, WEBSERVICE ws, int use, bool active, DateTime created)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      PERMISSION entity = new PERMISSION();
      entity.ID_INST = inst.ID;
      entity.ID_USER = user.ID;
      entity.ID_WS = ws.ID;
      entity.Usage = use;
      entity.Active = active;
      entity.CreatedOn = created;
      classes1DataContext.PERMISSIONs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(INSTITUTION inst, USER user, WEBSERVICE ws, int? use, bool? active, DateTime? created, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      PERMISSION permission = Queryable.Single<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID == id));
      if (inst != null)
        permission.INSTITUTION = inst;
      if (user != null)
        permission.USER = user;
      if (ws != null)
        permission.WEBSERVICE = ws;
      if (use.HasValue)
        permission.Usage = Convert.ToInt32((object) use);
      if (active.HasValue)
        permission.Active = Convert.ToBoolean((object) active);
      if (created.HasValue)
        permission.CreatedOn = Convert.ToDateTime((object) created);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<PERMISSION>((IQueryable<PERMISSION>) classes1DataContext.PERMISSIONs, (Expression<Func<PERMISSION, bool>>) (p => p.ID == (long) id)).Active = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
