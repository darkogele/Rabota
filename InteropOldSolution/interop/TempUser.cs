// Decompiled with JetBrains decompiler
// Type: interop.TempUser
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.TempUsers")]
  public class TempUser : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private int _ID;
    private string _Username;
    private string _Password;
    private string _Ip;
    private string _Certificate;
    private DateTime? _DateCreated;

    [Column(AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true, Storage = "_ID")]
    public int ID
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

    [Column(DbType = "NVarChar(MAX)", Storage = "_Username")]
    public string Username
    {
      get
      {
        return this._Username;
      }
      set
      {
        if (!(this._Username != value))
          return;
        this.SendPropertyChanging();
        this._Username = value;
        this.SendPropertyChanged("Username");
      }
    }

    [Column(DbType = "NVarChar(MAX)", Storage = "_Password")]
    public string Password
    {
      get
      {
        return this._Password;
      }
      set
      {
        if (!(this._Password != value))
          return;
        this.SendPropertyChanging();
        this._Password = value;
        this.SendPropertyChanged("Password");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_Ip")]
    public string Ip
    {
      get
      {
        return this._Ip;
      }
      set
      {
        if (!(this._Ip != value))
          return;
        this.SendPropertyChanging();
        this._Ip = value;
        this.SendPropertyChanged("Ip");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_Certificate")]
    public string Certificate
    {
      get
      {
        return this._Certificate;
      }
      set
      {
        if (!(this._Certificate != value))
          return;
        this.SendPropertyChanging();
        this._Certificate = value;
        this.SendPropertyChanged("Certificate");
      }
    }

    [Column(DbType = "DateTime", Storage = "_DateCreated")]
    public DateTime? DateCreated
    {
      get
      {
        return this._DateCreated;
      }
      set
      {
        DateTime? nullable1 = this._DateCreated;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._DateCreated = value;
        this.SendPropertyChanged("DateCreated");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, TempUser.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
