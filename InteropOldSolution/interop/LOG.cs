// Decompiled with JetBrains decompiler
// Type: interop.LOG
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.[LOG]")]
  public class LOG : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private int _Id_Table;
    private Guid _Id_User;
    private string _Id_Item;
    private int _ActiveType;
    private DateTime _DateTime;
    private string _Old;
    private string _New;

    [Column(AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true, Storage = "_ID")]
    public long ID
    {
      get
      {
        return this._ID;
      }
      set
      {
        if (this._ID == value)
          return;
        this.SendPropertyChanging();
        this._ID = value;
        this.SendPropertyChanged("ID");
      }
    }

    [Column(DbType = "Int NOT NULL", Storage = "_Id_Table")]
    public int Id_Table
    {
      get
      {
        return this._Id_Table;
      }
      set
      {
        if (this._Id_Table == value)
          return;
        this.SendPropertyChanging();
        this._Id_Table = value;
        this.SendPropertyChanged("Id_Table");
      }
    }

    [Column(DbType = "UniqueIdentifier NOT NULL", Storage = "_Id_User")]
    public Guid Id_User
    {
      get
      {
        return this._Id_User;
      }
      set
      {
        if (!(this._Id_User != value))
          return;
        this.SendPropertyChanging();
        this._Id_User = value;
        this.SendPropertyChanged("Id_User");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(50) NOT NULL", Storage = "_Id_Item")]
    public string Id_Item
    {
      get
      {
        return this._Id_Item;
      }
      set
      {
        if (!(this._Id_Item != value))
          return;
        this.SendPropertyChanging();
        this._Id_Item = value;
        this.SendPropertyChanged("Id_Item");
      }
    }

    [Column(DbType = "Int NOT NULL", Storage = "_ActiveType")]
    public int ActiveType
    {
      get
      {
        return this._ActiveType;
      }
      set
      {
        if (this._ActiveType == value)
          return;
        this.SendPropertyChanging();
        this._ActiveType = value;
        this.SendPropertyChanged("ActiveType");
      }
    }

    [Column(DbType = "DateTime NOT NULL", Storage = "_DateTime")]
    public DateTime DateTime
    {
      get
      {
        return this._DateTime;
      }
      set
      {
        if (!(this._DateTime != value))
          return;
        this.SendPropertyChanging();
        this._DateTime = value;
        this.SendPropertyChanged("DateTime");
      }
    }

    [Column(DbType = "NVarChar(1000)", Storage = "_Old")]
    public string Old
    {
      get
      {
        return this._Old;
      }
      set
      {
        if (!(this._Old != value))
          return;
        this.SendPropertyChanging();
        this._Old = value;
        this.SendPropertyChanged("Old");
      }
    }

    [Column(DbType = "NVarChar(1000)", Storage = "_New")]
    public string New
    {
      get
      {
        return this._New;
      }
      set
      {
        if (!(this._New != value))
          return;
        this.SendPropertyChanging();
        this._New = value;
        this.SendPropertyChanged("New");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, LOG.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
