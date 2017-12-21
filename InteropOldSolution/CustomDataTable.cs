// Decompiled with JetBrains decompiler
// Type: CustomDataTable
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Data;

public class CustomDataTable
{
  private DataTable _Table;

  public DataTable Tabela
  {
    get
    {
      return this._Table;
    }
    set
    {
      this.Tabela = this._Table;
    }
  }

  public DataTable CustomDataTableColumsAdd(DataTable tabela, string Column)
  {
    tabela.Columns.Add(new DataColumn()
    {
      DataType = Type.GetType("System.String"),
      ColumnName = Column
    });
    return tabela;
  }
}
