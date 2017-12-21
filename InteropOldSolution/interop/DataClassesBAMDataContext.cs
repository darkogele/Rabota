// Decompiled with JetBrains decompiler
// Type: interop.DataClassesBAMDataContext
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Database(Name = "BAMPrimaryImport")]
  public class DataClassesBAMDataContext : DataContext
  {
    private static MappingSource mappingSource = (MappingSource) new AttributeMappingSource();

    public Table<bam_UniversalServiceControlProduction_Completed> bam_UniversalServiceControlProduction_Completeds
    {
      get
      {
        return this.GetTable<bam_UniversalServiceControlProduction_Completed>();
      }
    }

    public DataClassesBAMDataContext()
      : base(ConfigurationManager.ConnectionStrings["BAMPrimaryImportConnectionString"].ConnectionString, DataClassesBAMDataContext.mappingSource)
    {
    }

    public DataClassesBAMDataContext(string connection)
      : base(connection, DataClassesBAMDataContext.mappingSource)
    {
    }

    public DataClassesBAMDataContext(IDbConnection connection)
      : base(connection, DataClassesBAMDataContext.mappingSource)
    {
    }

    public DataClassesBAMDataContext(string connection, MappingSource mappingSource)
      : base(connection, mappingSource)
    {
    }

    public DataClassesBAMDataContext(IDbConnection connection, MappingSource mappingSource)
      : base(connection, mappingSource)
    {
    }
  }
}
