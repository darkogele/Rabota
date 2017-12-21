// Decompiled with JetBrains decompiler
// Type: interop.DataClasses1DataContext
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Database(Name = "InteropProduction_db")]
  public class DataClasses1DataContext : DataContext
  {
    private static MappingSource mappingSource = (MappingSource) new AttributeMappingSource();

    public Table<interop.BASIS> BASIS
    {
      get
      {
        return this.GetTable<interop.BASIS>();
      }
    }

    public Table<interop.WEBSERVICESBASIS> WEBSERVICESBASIS
    {
      get
      {
        return this.GetTable<interop.WEBSERVICESBASIS>();
      }
    }

    public Table<INSTITUTION> INSTITUTIONs
    {
      get
      {
        return this.GetTable<INSTITUTION>();
      }
    }

    public Table<LOG> LOGs
    {
      get
      {
        return this.GetTable<LOG>();
      }
    }

    public Table<PARAM> PARAMs
    {
      get
      {
        return this.GetTable<PARAM>();
      }
    }

    public Table<PERMISSION> PERMISSIONs
    {
      get
      {
        return this.GetTable<PERMISSION>();
      }
    }

    public Table<REPORT> REPORTs
    {
      get
      {
        return this.GetTable<REPORT>();
      }
    }

    public Table<WEBSERVICE> WEBSERVICEs
    {
      get
      {
        return this.GetTable<WEBSERVICE>();
      }
    }

    public Table<CERTIFICATE> CERTIFICATEs
    {
      get
      {
        return this.GetTable<CERTIFICATE>();
      }
    }

    public Table<DOCUMENTSTRUCTURE> DOCUMENTSTRUCTUREs
    {
      get
      {
        return this.GetTable<DOCUMENTSTRUCTURE>();
      }
    }

    public Table<TempUser> TempUsers
    {
      get
      {
        return this.GetTable<TempUser>();
      }
    }

    public Table<APP_SETTING> APP_SETTINGs
    {
      get
      {
        return this.GetTable<APP_SETTING>();
      }
    }

    public Table<USER> USERs
    {
      get
      {
        return this.GetTable<USER>();
      }
    }

    public DataClasses1DataContext()
      : base(ConfigurationManager.ConnectionStrings["InteropProduction_dbConnectionString"].ConnectionString, DataClasses1DataContext.mappingSource)
    {
    }

    public DataClasses1DataContext(string connection)
      : base(connection, DataClasses1DataContext.mappingSource)
    {
    }

    public DataClasses1DataContext(IDbConnection connection)
      : base(connection, DataClasses1DataContext.mappingSource)
    {
    }

    public DataClasses1DataContext(string connection, MappingSource mappingSource)
      : base(connection, mappingSource)
    {
    }

    public DataClasses1DataContext(IDbConnection connection, MappingSource mappingSource)
      : base(connection, mappingSource)
    {
    }
  }
}
