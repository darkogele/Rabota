// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.CertificatesDAL
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
  public class CertificatesDAL
  {
    public CERTIFICATE GetByID(long id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.ID == id));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetBySubject(string Subject)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.Subject == Subject));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetByThumbprint(string Thumbprint)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.Thumbprint == Thumbprint));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetByIssuer(string Issuer)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.Issuer == Issuer));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetBySerialNumber(string SerialNumber)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.SerialNumber == SerialNumber));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetByValidationPeriod(DateTime ValidFrom, DateTime ValidTo)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, bool>>) (p => p.ValidFrom == ValidFrom && p.ValidTo == ValidTo));
      }
      catch
      {
        return (CERTIFICATE) null;
      }
    }

    public CERTIFICATE GetByUser(USER User)
    {
        DataClasses1DataContext pom = new DataClasses1DataContext();
        CERTIFICATE result;
        try
        {
            IQueryable<CERTIFICATE> source =
                from u in pom.USERs
                from c in pom.CERTIFICATEs
                where u.ID == User.ID && u.ID_CERT == (long?)c.ID
                select c;
            List<CERTIFICATE> list = source.ToList<CERTIFICATE>();
            CERTIFICATE cERTIFICATE = new CERTIFICATE();
            if (list.Count == 1)
            {
                result = list[0];
            }
            else
            {
                result = null;
            }
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public List<CERTIFICATE> GetAllCertificates()
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Enumerable.ToList<CERTIFICATE>((IEnumerable<CERTIFICATE>) Queryable.Select<CERTIFICATE, CERTIFICATE>((IQueryable<CERTIFICATE>) classes1DataContext.CERTIFICATEs, (Expression<Func<CERTIFICATE, CERTIFICATE>>) (c => c)));
      }
      catch
      {
        return (List<CERTIFICATE>) null;
      }
    }
  }
}
