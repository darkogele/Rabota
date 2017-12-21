// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.InstitutionsDAL
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
  public class InstitutionsDAL
  {
    public INSTITUTION GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<INSTITUTION>((IQueryable<INSTITUTION>) classes1DataContext.INSTITUTIONs, (Expression<Func<INSTITUTION, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (INSTITUTION) null;
      }
    }

    public INSTITUTION GetByTittle(string tittle)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<INSTITUTION>((IQueryable<INSTITUTION>) classes1DataContext.INSTITUTIONs, (Expression<Func<INSTITUTION, bool>>) (p => p.Tittle == tittle));
      }
      catch
      {
        return (INSTITUTION) null;
      }
    }

    public List<INSTITUTION> GetAllActiveDeleted(bool sign)
    {
      IQueryable<INSTITUTION> queryable = Queryable.Where<INSTITUTION>((IQueryable<INSTITUTION>) new DataClasses1DataContext().INSTITUTIONs, (Expression<Func<INSTITUTION, bool>>) (p => p.Active == sign));
      List<INSTITUTION> list = new List<INSTITUTION>();
      foreach (INSTITUTION institution in (IEnumerable<INSTITUTION>) queryable)
        list.Add(institution);
      return list;
    }

    public long Insert(string title, string desc, bool active, DateTime created)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      INSTITUTION entity = new INSTITUTION();
      entity.Tittle = title;
      entity.Description = desc;
      entity.Active = active;
      entity.CreatedOn = created;
      classes1DataContext.INSTITUTIONs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(string title, string desc, bool? active, DateTime? created, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      INSTITUTION institution = Queryable.Single<INSTITUTION>((IQueryable<INSTITUTION>) classes1DataContext.INSTITUTIONs, (Expression<Func<INSTITUTION, bool>>) (p => p.ID == id));
      if (title != null)
        institution.Tittle = Convert.ToString(title);
      if (desc != null)
        institution.Description = Convert.ToString(desc);
      if (active.HasValue)
        institution.Active = Convert.ToBoolean((object) active);
      if (created.HasValue)
        institution.CreatedOn = Convert.ToDateTime((object) created);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<INSTITUTION>((IQueryable<INSTITUTION>) classes1DataContext.INSTITUTIONs, (Expression<Func<INSTITUTION, bool>>) (p => p.ID == (long) id)).Active = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
