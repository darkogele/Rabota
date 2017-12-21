// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.WSBasisDAL
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
  public class WSBasisDAL
  {
    public WEBSERVICESBASIS GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (WEBSERVICESBASIS) null;
      }
    }

    public List<WEBSERVICESBASIS> GetActiveByWebServiceID(long id)
    {
        DataClasses1DataContext pom = new DataClasses1DataContext();
        List<WEBSERVICESBASIS> result;
        try
        {
            IQueryable<WEBSERVICESBASIS> queryable =
                from wb in pom.WEBSERVICESBASIS
                from b in pom.BASIS
                from p in pom.PERMISSIONs
                from ws in pom.WEBSERVICEs
                where p.ID == wb.ID_Permission && wb.ID_Basis == b.ID && p.ID_WS == id && wb.Active == true && p.Active == true
                select wb;
            List<WEBSERVICESBASIS> list = new List<WEBSERVICESBASIS>();
            foreach (WEBSERVICESBASIS current in queryable)
            {
                list.Add(new WEBSERVICESBASIS
                {
                    Active = current.Active,
                    BASIS = current.BASIS,
                    CreatedOn = current.CreatedOn,
                    ID = current.ID,
                    ID_Basis = current.ID_Basis,
                    ID_Permission = current.ID_Permission,
                    PERMISSION = current.PERMISSION
                });
            }
            result = list;
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public List<WEBSERVICESBASIS> GetActiveByBasisID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<WEBSERVICESBASIS> queryable = Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (wb => wb.ID_Basis == id && wb.Active == true));
        List<WEBSERVICESBASIS> list = new List<WEBSERVICESBASIS>();
        foreach (WEBSERVICESBASIS webservicesbasis in (IEnumerable<WEBSERVICESBASIS>) queryable)
          list.Add(new WEBSERVICESBASIS()
          {
            Active = webservicesbasis.Active,
            BASIS = webservicesbasis.BASIS,
            CreatedOn = webservicesbasis.CreatedOn,
            ID = webservicesbasis.ID,
            ID_Basis = webservicesbasis.ID_Basis,
            ID_Permission = webservicesbasis.ID_Permission,
            PERMISSION = webservicesbasis.PERMISSION
          });
        return list;
      }
      catch
      {
        return (List<WEBSERVICESBASIS>) null;
      }
    }

    public List<WEBSERVICESBASIS> GetActiveByInstitutionAndWebService(long INST_ID, long WS_ID)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<WEBSERVICESBASIS> queryable = Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (wb => wb.PERMISSION.ID_INST == INST_ID && wb.PERMISSION.ID_WS == WS_ID && wb.Active == true && wb.PERMISSION.Active == true));
        List<WEBSERVICESBASIS> list = new List<WEBSERVICESBASIS>();
        foreach (WEBSERVICESBASIS webservicesbasis in (IEnumerable<WEBSERVICESBASIS>) queryable)
          list.Add(new WEBSERVICESBASIS()
          {
            Active = webservicesbasis.Active,
            BASIS = webservicesbasis.BASIS,
            CreatedOn = webservicesbasis.CreatedOn,
            ID = webservicesbasis.ID,
            ID_Basis = webservicesbasis.ID_Basis,
            ID_Permission = webservicesbasis.ID_Permission,
            PERMISSION = webservicesbasis.PERMISSION
          });
        return list;
      }
      catch
      {
        return (List<WEBSERVICESBASIS>) null;
      }
    }

    public List<WEBSERVICESBASIS> GetActiveByInstitutionWebServiceAndBasis(long INST_ID, long WS_ID, long Basis_ID)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        IQueryable<WEBSERVICESBASIS> queryable = Queryable.Where<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (wb => wb.PERMISSION.ID_INST == INST_ID && wb.PERMISSION.ID_WS == WS_ID && wb.BASIS.ID == Basis_ID && wb.Active == true));
        List<WEBSERVICESBASIS> list = new List<WEBSERVICESBASIS>();
        foreach (WEBSERVICESBASIS webservicesbasis in (IEnumerable<WEBSERVICESBASIS>) queryable)
          list.Add(new WEBSERVICESBASIS()
          {
            Active = webservicesbasis.Active,
            BASIS = webservicesbasis.BASIS,
            CreatedOn = webservicesbasis.CreatedOn,
            ID = webservicesbasis.ID,
            ID_Basis = webservicesbasis.ID_Basis,
            ID_Permission = webservicesbasis.ID_Permission,
            PERMISSION = webservicesbasis.PERMISSION
          });
        return list;
      }
      catch
      {
        return (List<WEBSERVICESBASIS>) null;
      }
    }

   public List<WEBSERVICESBASIS> GetActiveByUserAndWebService(Guid User_ID, long WS_ID)
		{
			DataClasses1DataContext pom = new DataClasses1DataContext();
			List<WEBSERVICESBASIS> result;
			try
			{
				IQueryable<WEBSERVICESBASIS> queryable = 
					from wb in pom.WEBSERVICESBASIS
					from b in pom.BASIS
					from p in pom.PERMISSIONs
					from ws in pom.WEBSERVICEs
					from u in pom.USERs
					where p.ID == wb.ID_Permission && wb.ID_Basis == b.ID && p.ID_USER == u.ID && ws.ID == WS_ID && u.ID == User_ID && u.Active == true && wb.Active == true && p.Active == true
					select wb;
				List<WEBSERVICESBASIS> list = new List<WEBSERVICESBASIS>();
				foreach (WEBSERVICESBASIS current in queryable)
				{
					list.Add(new WEBSERVICESBASIS
					{
						Active = current.Active,
						BASIS = current.BASIS,
						CreatedOn = current.CreatedOn,
						ID = current.ID,
						ID_Basis = current.ID_Basis,
						ID_Permission = current.ID_Permission,
						PERMISSION = current.PERMISSION
					});
				}
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}

    public long Insert(long BasisID, long ID_Permission, DateTime CreatedOn, bool Active)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      WEBSERVICESBASIS entity = new WEBSERVICESBASIS();
      entity.ID_Permission = ID_Permission;
      entity.CreatedOn = CreatedOn;
      entity.Active = Active;
      entity.ID_Basis = BasisID;
      classes1DataContext.WEBSERVICESBASIS.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }

    public void Update(long? BasisID, long? ID_Permission, DateTime? CreatedOn, bool? Active, long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      WEBSERVICESBASIS webservicesbasis = Queryable.Single<WEBSERVICESBASIS>((IQueryable<WEBSERVICESBASIS>) classes1DataContext.WEBSERVICESBASIS, (Expression<Func<WEBSERVICESBASIS, bool>>) (p => p.ID == id));
      if (BasisID.HasValue)
        webservicesbasis.ID_Basis = BasisID.Value;
      if (ID_Permission.HasValue)
        webservicesbasis.ID_Permission = ID_Permission.Value;
      if (CreatedOn.HasValue)
        webservicesbasis.CreatedOn = Convert.ToDateTime((object) CreatedOn);
      if (Active.HasValue)
        webservicesbasis.Active = Convert.ToBoolean((object) Active);
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
