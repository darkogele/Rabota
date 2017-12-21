// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.BasisDAL
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
  public class BasisDAL
  {
    public List<BASIS> GetAllByStatus(bool Active)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<BASIS> queryable = Queryable.Where<BASIS>((IQueryable<BASIS>) classes1DataContext.BASIS, (Expression<Func<BASIS, bool>>) (p => p.Active == Active));
        List<BASIS> list = new List<BASIS>();
        foreach (BASIS basis in (IEnumerable<BASIS>) queryable)
          list.Add(new BASIS()
          {
            Active = basis.Active,
            CreatedOn = basis.CreatedOn,
            ID = basis.ID,
            Tittle = basis.Tittle,
            WEBSERVICESBASIS = basis.WEBSERVICESBASIS
          });
        return list;
      }
      catch
      {
        return (List<BASIS>) null;
      }
    }

    public BASIS GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<BASIS>((IQueryable<BASIS>) classes1DataContext.BASIS, (Expression<Func<BASIS, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (BASIS) null;
      }
    }

    public List<BASIS> GetActiveByWebServiceID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<BASIS> queryable = Queryable.Select<WEBSERVICESBASIS, BASIS>(Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (p => p.PERMISSION.ID_WS == id && p.Active == true)), (Expression<Func<WEBSERVICESBASIS, BASIS>>) (p => p.BASIS));
        List<BASIS> list = new List<BASIS>();
        foreach (BASIS basis in (IEnumerable<BASIS>) queryable)
          list.Add(new BASIS()
          {
            Active = basis.Active,
            CreatedOn = basis.CreatedOn,
            ID = basis.ID,
            Tittle = basis.Tittle,
            WEBSERVICESBASIS = basis.WEBSERVICESBASIS
          });
        return list;
      }
      catch
      {
        return (List<BASIS>) null;
      }
    }

    public List<BASIS> GetActiveByInstitutionAndWebService(long INST_ID, long WS_ID)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<BASIS> queryable = Queryable.Distinct<BASIS>(Queryable.Select<WEBSERVICESBASIS, BASIS>(Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (p => p.PERMISSION.ID_WS == WS_ID && p.PERMISSION.ID_INST == INST_ID && p.Active == true)), (Expression<Func<WEBSERVICESBASIS, BASIS>>) (p => p.BASIS)));
        List<BASIS> list = new List<BASIS>();
        foreach (BASIS basis in (IEnumerable<BASIS>) queryable)
          list.Add(new BASIS()
          {
            Active = basis.Active,
            CreatedOn = basis.CreatedOn,
            ID = basis.ID,
            Tittle = basis.Tittle,
            WEBSERVICESBASIS = basis.WEBSERVICESBASIS
          });
        return list;
      }
      catch
      {
        return (List<BASIS>) null;
      }
    }

    public List<BASIS> GetActiveByUserAndWebService(Guid User_ID, long WS_ID)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<BASIS> queryable = Queryable.Distinct<BASIS>(Queryable.Select<WEBSERVICESBASIS, BASIS>(Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (p => p.PERMISSION.ID_WS == WS_ID && p.PERMISSION.ID_USER == User_ID && p.Active == true)), (Expression<Func<WEBSERVICESBASIS, BASIS>>) (p => p.BASIS)));
        List<BASIS> list = new List<BASIS>();
        foreach (BASIS basis in (IEnumerable<BASIS>) queryable)
          list.Add(new BASIS()
          {
            Active = basis.Active,
            CreatedOn = basis.CreatedOn,
            ID = basis.ID,
            Tittle = basis.Tittle
          });
        return list;
      }
      catch
      {
        return (List<BASIS>) null;
      }
    }

    public BASIS GetByTittle(string tittle)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<BASIS>((IQueryable<BASIS>) classes1DataContext.BASIS, (Expression<Func<BASIS, bool>>) (p => p.Tittle == tittle));
      }
      catch
      {
        return (BASIS) null;
      }
    }

    public long Insert(string tittle, bool active, DateTime createdon)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      BASIS entity = new BASIS();
      entity.Tittle = tittle;
      entity.CreatedOn = createdon;
      entity.Active = active;
      classes1DataContext.BASIS.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(string tittle, bool active, DateTime? created, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      BASIS basis = Queryable.Single<BASIS>((IQueryable<BASIS>) classes1DataContext.BASIS, (Expression<Func<BASIS, bool>>) (p => p.ID == id));
      if (tittle != null)
        basis.Tittle = Convert.ToString(tittle);
      bool flag = 1 == 0;
      basis.Active = Convert.ToBoolean(active);
      if (created.HasValue)
        basis.CreatedOn = Convert.ToDateTime((object) created);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<BASIS>((IQueryable<BASIS>) classes1DataContext.BASIS, (Expression<Func<BASIS, bool>>) (p => p.ID == (long) id)).Active = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
