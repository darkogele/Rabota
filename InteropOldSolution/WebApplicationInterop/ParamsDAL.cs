// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.ParamsDAL
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
  public class ParamsDAL
  {
    public PARAM GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<PARAM>((IQueryable<PARAM>) classes1DataContext.PARAMs, (Expression<Func<PARAM, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (PARAM) null;
      }
    }

    public List<PARAM> GetAllActiveDeleted(bool sign)
    {
      IQueryable<PARAM> queryable = Queryable.Where<PARAM>((IQueryable<PARAM>) new DataClasses1DataContext().PARAMs, (Expression<Func<PARAM, bool>>) (p => p.Acitve == sign));
      List<PARAM> list = new List<PARAM>();
      foreach (PARAM obj in (IEnumerable<PARAM>) queryable)
        list.Add(obj);
      return list;
    }

    public List<PARAM> GetParamsForWebservice(WEBSERVICE ws)
    {
      IQueryable<PARAM> queryable = Queryable.Where<PARAM>((IQueryable<PARAM>) new DataClasses1DataContext().PARAMs, (Expression<Func<PARAM, bool>>) (p => p.Acitve == true && p.WEBSERVICE.ID == ws.ID));
      List<PARAM> list = new List<PARAM>();
      foreach (PARAM obj in (IEnumerable<PARAM>) queryable)
        list.Add(obj);
      return list;
    }

    public long Insert(string title, string desc, int maxlen, int type, WEBSERVICE ws, bool active)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      PARAM entity = new PARAM();
      entity.Tittle = title;
      entity.Description = desc;
      entity.MaxLength = maxlen;
      entity.Type = type;
      entity.ID_WS = ws.ID;
      entity.Acitve = active;
      classes1DataContext.PARAMs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(string title, string desc, int? maxlen, int? type, WEBSERVICE ws, bool? active, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      PARAM obj = Queryable.Single<PARAM>((IQueryable<PARAM>) classes1DataContext.PARAMs, (Expression<Func<PARAM, bool>>) (p => p.ID == id));
      if (title != null)
        obj.Tittle = Convert.ToString(title);
      if (desc != null)
        obj.Description = Convert.ToString(desc);
      if (maxlen.HasValue)
        obj.MaxLength = Convert.ToInt32((object) maxlen);
      if (type.HasValue)
        obj.Type = Convert.ToInt32((object) type);
      if (ws != null)
        obj.WEBSERVICE = ws;
      if (active.HasValue)
        obj.Acitve = Convert.ToBoolean((object) active);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<PARAM>((IQueryable<PARAM>) classes1DataContext.PARAMs, (Expression<Func<PARAM, bool>>) (p => p.ID == id)).Acitve = false;
      classes1DataContext.SubmitChanges();
    }
  }
}
