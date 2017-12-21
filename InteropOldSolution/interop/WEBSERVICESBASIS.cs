// Decompiled with JetBrains decompiler
// Type: interop.WEBSERVICESBASIS
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.WEBSERVICESBASIS")]
  public class WEBSERVICESBASIS : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private long _ID_Permission;
    private long _ID_Basis;
    private bool _Active;
    private DateTime _CreatedOn;
    private EntityRef<BASIS> _BASIS;
    private EntityRef<PERMISSION> _PERMISSION;

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

    [Column(DbType = "BigInt NOT NULL", Storage = "_ID_Permission")]
    public long ID_Permission
    {
      get
      {
        return this._ID_Permission;
      }
      set
      {
        if (this._ID_Permission == value)
          return;
        if (this._PERMISSION.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_Permission = value;
        this.SendPropertyChanged("ID_Permission");
      }
    }

    [Column(DbType = "BigInt NOT NULL", Storage = "_ID_Basis")]
    public long ID_Basis
    {
      get
      {
        return this._ID_Basis;
      }
      set
      {
        if (this._ID_Basis == value)
          return;
        if (this._BASIS.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_Basis = value;
        this.SendPropertyChanged("ID_Basis");
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

    [Association(IsForeignKey = true, Name = "BASIS_WEBSERVICESBASIS", OtherKey = "ID", Storage = "_BASIS", ThisKey = "ID_Basis")]
    public BASIS BASIS
    {
      get
      {
        return this._BASIS.Entity;
      }
      set
      {
        BASIS entity = this._BASIS.Entity;
        if (entity == value && this._BASIS.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._BASIS.Entity = (BASIS) null;
          entity.WEBSERVICESBASIS.Remove(this);
        }
        this._BASIS.Entity = value;
        if (value != null)
        {
          value.WEBSERVICESBASIS.Add(this);
          this._ID_Basis = value.ID;
        }
        else
          this._ID_Basis = 0L;
        this.SendPropertyChanged("BASIS");
      }
    }

    [Association(IsForeignKey = true, Name = "PERMISSION_WEBSERVICESBASIS", OtherKey = "ID", Storage = "_PERMISSION", ThisKey = "ID_Permission")]
    public PERMISSION PERMISSION
    {
      get
      {
        return this._PERMISSION.Entity;
      }
      set
      {
        PERMISSION entity = this._PERMISSION.Entity;
        if (entity == value && this._PERMISSION.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._PERMISSION.Entity = (PERMISSION) null;
          entity.WEBSERVICESBASIS.Remove(this);
        }
        this._PERMISSION.Entity = value;
        if (value != null)
        {
          value.WEBSERVICESBASIS.Add(this);
          this._ID_Permission = value.ID;
        }
        else
          this._ID_Permission = 0L;
        this.SendPropertyChanged("PERMISSION");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public WEBSERVICESBASIS()
    {
      this._BASIS = new EntityRef<BASIS>();
      this._PERMISSION = new EntityRef<PERMISSION>();
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, WEBSERVICESBASIS.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
