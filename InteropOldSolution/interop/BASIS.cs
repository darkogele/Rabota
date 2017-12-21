// Decompiled with JetBrains decompiler
// Type: interop.BASIS
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.BASIS")]
  public class BASIS : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Tittle;
    private DateTime _CreatedOn;
    private bool _Active;
    private EntitySet<interop.WEBSERVICESBASIS> _WEBSERVICESBASIS;

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

    [Column(CanBeNull = false, DbType = "NVarChar(2000) NOT NULL", Storage = "_Tittle")]
    public string Tittle
    {
      get
      {
        return this._Tittle;
      }
      set
      {
        if (!(this._Tittle != value))
          return;
        this.SendPropertyChanging();
        this._Tittle = value;
        this.SendPropertyChanged("Tittle");
      }
    }

    [Column(DbType = "DateTime NOT NULL", Storage = "_CreatedOn")]
    public DateTime CreatedOn
    {
      get
      {
        return this._CreatedOn;
      }
      set
      {
        if (!(this._CreatedOn != value))
          return;
        this.SendPropertyChanging();
        this._CreatedOn = value;
        this.SendPropertyChanged("CreatedOn");
      }
    }

    [Column(DbType = "Bit NOT NULL", Storage = "_Active")]
    public bool Active
    {
      get
      {
        return this._Active;
      }
      set
      {
        if (this._Active == value)
          return;
        this.SendPropertyChanging();
        this._Active = value;
        this.SendPropertyChanged("Active");
      }
    }

    [Association(Name = "BASIS_WEBSERVICESBASIS", OtherKey = "ID_Basis", Storage = "_WEBSERVICESBASIS", ThisKey = "ID")]
    public EntitySet<interop.WEBSERVICESBASIS> WEBSERVICESBASIS
    {
      get
      {
        return this._WEBSERVICESBASIS;
      }
      set
      {
        this._WEBSERVICESBASIS.Assign((IEnumerable<interop.WEBSERVICESBASIS>) value);
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public BASIS()
    {
      this._WEBSERVICESBASIS = new EntitySet<interop.WEBSERVICESBASIS>(new Action<interop.WEBSERVICESBASIS>(this.attach_WEBSERVICESBASIS), new Action<interop.WEBSERVICESBASIS>(this.detach_WEBSERVICESBASIS));
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, BASIS.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void attach_WEBSERVICESBASIS(interop.WEBSERVICESBASIS entity)
    {
      this.SendPropertyChanging();
      entity.BASIS = this;
    }

    private void detach_WEBSERVICESBASIS(interop.WEBSERVICESBASIS entity)
    {
      this.SendPropertyChanging();
      entity.BASIS = (BASIS) null;
    }
  }
}
