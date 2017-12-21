// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.DocumentStructureDAL
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
  public class DocumentStructureDAL
  {
    public DOCUMENTSTRUCTURE GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<DOCUMENTSTRUCTURE>((IQueryable<DOCUMENTSTRUCTURE>) classes1DataContext.DOCUMENTSTRUCTUREs, (Expression<Func<DOCUMENTSTRUCTURE, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (DOCUMENTSTRUCTURE) null;
      }
    }

    public DOCUMENTSTRUCTURE GetByWebService(WEBSERVICE ws)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<DOCUMENTSTRUCTURE>((IQueryable<DOCUMENTSTRUCTURE>) classes1DataContext.DOCUMENTSTRUCTUREs, (Expression<Func<DOCUMENTSTRUCTURE, bool>>) (p => p.ID_WS == ws.ID));
      }
      catch
      {
        return (DOCUMENTSTRUCTURE) null;
      }
    }

    public DOCUMENTSTRUCTURE GetByName(string name)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<DOCUMENTSTRUCTURE>((IQueryable<DOCUMENTSTRUCTURE>) classes1DataContext.DOCUMENTSTRUCTUREs, (Expression<Func<DOCUMENTSTRUCTURE, bool>>) (p => p.Name == name));
      }
      catch
      {
        return (DOCUMENTSTRUCTURE) null;
      }
    }

    public List<DOCUMENTSTRUCTURE> GetAllDocumentStructures()
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Enumerable.ToList<DOCUMENTSTRUCTURE>((IEnumerable<DOCUMENTSTRUCTURE>) Queryable.Select<DOCUMENTSTRUCTURE, DOCUMENTSTRUCTURE>((IQueryable<DOCUMENTSTRUCTURE>) classes1DataContext.DOCUMENTSTRUCTUREs, (Expression<Func<DOCUMENTSTRUCTURE, DOCUMENTSTRUCTURE>>) (p => p)));
      }
      catch
      {
        return (List<DOCUMENTSTRUCTURE>) null;
      }
    }

    public long Insert(long ID_WS, string Name, string Description, string XMLSchema, string Purpose, DateTime CreatedOn, Guid CreatedBy, DateTime? ModifiedOn, Guid? ModifiedBy)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        DOCUMENTSTRUCTURE entity = new DOCUMENTSTRUCTURE();
        entity.ID_WS = ID_WS;
        entity.Name = Name;
        entity.Description = Description;
        entity.XMLSchema = XMLSchema;
        entity.Purpose = Purpose;
        entity.CreatedOn = CreatedOn;
        entity.CreatedBy = CreatedBy;
        classes1DataContext.DOCUMENTSTRUCTUREs.InsertOnSubmit(entity);
        classes1DataContext.SubmitChanges();
        return entity.ID;
      }
      catch
      {
        return 0L;
      }
    }

    public void Update(long ID, long? ID_WS, string Name, string Description, string XMLSchema, string Purpose, DateTime? CreatedOn, Guid? CreatedBy, DateTime? ModifiedOn, Guid? ModifiedBy)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        DOCUMENTSTRUCTURE documentstructure = Queryable.Single<DOCUMENTSTRUCTURE>((IQueryable<DOCUMENTSTRUCTURE>) classes1DataContext.DOCUMENTSTRUCTUREs, (Expression<Func<DOCUMENTSTRUCTURE, bool>>) (p => p.ID == ID));
        if (ID_WS.HasValue)
          documentstructure.ID_WS = Convert.ToInt64((object) ID_WS);
        if (Name != null)
          documentstructure.Name = Name.ToString();
        if (Description != null)
          documentstructure.Description = Description.ToString();
        if (XMLSchema != null)
          documentstructure.XMLSchema = XMLSchema.ToString();
        if (Purpose != null)
          documentstructure.Purpose = Purpose.ToString();
        if (CreatedOn.HasValue)
          documentstructure.CreatedOn = Convert.ToDateTime((object) CreatedOn);
        if (CreatedBy.HasValue)
          documentstructure.CreatedBy = CreatedBy.Value;
        if (ModifiedOn.HasValue)
          documentstructure.ModifiedOn = new DateTime?(Convert.ToDateTime((object) ModifiedOn));
        if (ModifiedBy.HasValue)
          documentstructure.ModifiedBy = new Guid?(ModifiedBy.Value);
        classes1DataContext.SubmitChanges();
      }
      catch
      {
      }
    }
  }
}
