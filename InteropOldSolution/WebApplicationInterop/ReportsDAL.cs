// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.ReportsDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplicationInterop
{
  public class ReportsDAL
  {
    public REPORT GetByID(long ID)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<REPORT>((IQueryable<REPORT>) classes1DataContext.REPORTs, (Expression<Func<REPORT, bool>>) (p => p.ID == ID));
      }
      catch
      {
        return (REPORT) null;
      }
    }

    public long Insert(USER UserCreator, string RecordID, string ReportFilePath, string ReportFileName, DateTime CreatedOn)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      REPORT entity = new REPORT();
      entity.IDUserCreator = UserCreator.ID;
      entity.RecordID = RecordID;
      entity.ReportFilePath = ReportFilePath;
      entity.ReportFileName = ReportFileName;
      entity.CreatedOn = CreatedOn;
      classes1DataContext.REPORTs.InsertOnSubmit(entity);
      classes1DataContext.SubmitChanges();
      return entity.ID;
    }
  }
}
