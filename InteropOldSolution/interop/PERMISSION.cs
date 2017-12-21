// Decompiled with JetBrains decompiler
// Type: interop.PERMISSION
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
  [Table(Name = "dbo.PERMISSIONS")]
  public class PERMISSION : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private long _ID_INST;
    private Guid _ID_USER;
    private long _ID_WS;
    private int _Usage;
    private bool _Active;
    private Guid? _CreatedBy;
    private DateTime _CreatedOn;
    private EntitySet<interop.WEBSERVICESBASIS> _WEBSERVICESBASIS;
    private EntityRef<INSTITUTION> _INSTITUTION;
    private EntityRef<WEBSERVICE> _WEBSERVICE;
    private EntityRef<USER> _USER;

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

    [Column(DbType = "BigInt NOT NULL", Storage = "_ID_INST")]
    public long ID_INST
    {
      get
      {
        return this._ID_INST;
      }
      set
      {
        if (this._ID_INST == value)
          return;
        if (this._INSTITUTION.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_INST = value;
        this.SendPropertyChanged("ID_INST");
      }
    }

    [Column(DbType = "UniqueIdentifier NOT NULL", Storage = "_ID_USER")]
    public Guid ID_USER
    {
      get
      {
        return this._ID_USER;
      }
      set
      {
        if (!(this._ID_USER != value))
          return;
        if (this._USER.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_USER = value;
        this.SendPropertyChanged("ID_USER");
      }
    }

    [Column(DbType = "BigInt NOT NULL", Storage = "_ID_WS")]
    public long ID_WS
    {
      get
      {
        return this._ID_WS;
      }
      set
      {
        if (this._ID_WS == value)
          return;
        if (this._WEBSERVICE.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_WS = value;
        this.SendPropertyChanged("ID_WS");
      }
    }

    [Column(DbType = "Int NOT NULL", Storage = "_Usage")]
    public int Usage
    {
      get
      {
        return this._Usage;
      }
      set
      {
        if (this._Usage == value)
          return;
        this.SendPropertyChanging();
        this._Usage = value;
        this.SendPropertyChanged("Usage");
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

    [Column(DbType = "UniqueIdentifier", Storage = "_CreatedBy")]
    public Guid? CreatedBy
    {
      get
      {
        return this._CreatedBy;
      }
      set
      {
        Guid? nullable1 = this._CreatedBy;
        Guid? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._CreatedBy = value;
        this.SendPropertyChanged("CreatedBy");
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

    [Association(Name = "PERMISSION_WEBSERVICESBASIS", OtherKey = "ID_Permission", Storage = "_WEBSERVICESBASIS", ThisKey = "ID")]
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

    [Association(IsForeignKey = true, Name = "INSTITUTION_PERMISSION", OtherKey = "ID", Storage = "_INSTITUTION", ThisKey = "ID_INST")]
    public INSTITUTION INSTITUTION
    {
      get
      {
        return this._INSTITUTION.Entity;
      }
      set
      {
        INSTITUTION entity = this._INSTITUTION.Entity;
        if (entity == value && this._INSTITUTION.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._INSTITUTION.Entity = (INSTITUTION) null;
          entity.PERMISSIONs.Remove(this);
        }
        this._INSTITUTION.Entity = value;
        if (value != null)
        {
          value.PERMISSIONs.Add(this);
          this._ID_INST = value.ID;
        }
        else
          this._ID_INST = 0L;
        this.SendPropertyChanged("INSTITUTION");
      }
    }

    [Association(IsForeignKey = true, Name = "WEBSERVICE_PERMISSION", OtherKey = "ID", Storage = "_WEBSERVICE", ThisKey = "ID_WS")]
    public WEBSERVICE WEBSERVICE
    {
      get
      {
        return this._WEBSERVICE.Entity;
      }
      set
      {
        WEBSERVICE entity = this._WEBSERVICE.Entity;
        if (entity == value && this._WEBSERVICE.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._WEBSERVICE.Entity = (WEBSERVICE) null;
          entity.PERMISSIONs.Remove(this);
        }
        this._WEBSERVICE.Entity = value;
        if (value != null)
        {
          value.PERMISSIONs.Add(this);
          this._ID_WS = value.ID;
        }
        else
          this._ID_WS = 0L;
        this.SendPropertyChanged("WEBSERVICE");
      }
    }

    [Association(IsForeignKey = true, Name = "USER_PERMISSION", OtherKey = "ID", Storage = "_USER", ThisKey = "ID_USER")]
    public USER USER
    {
      get
      {
        return this._USER.Entity;
      }
      set
      {
        USER entity = this._USER.Entity;
        if (entity == value && this._USER.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._USER.Entity = (USER) null;
          entity.PERMISSIONs.Remove(this);
        }
        this._USER.Entity = value;
        if (value != null)
        {
          value.PERMISSIONs.Add(this);
          this._ID_USER = value.ID;
        }
        else
          this._ID_USER = new Guid();
        this.SendPropertyChanged("USER");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public PERMISSION()
    {
      this._WEBSERVICESBASIS = new EntitySet<interop.WEBSERVICESBASIS>(new Action<interop.WEBSERVICESBASIS>(this.attach_WEBSERVICESBASIS), new Action<interop.WEBSERVICESBASIS>(this.detach_WEBSERVICESBASIS));
      this._INSTITUTION = new EntityRef<INSTITUTION>();
      this._WEBSERVICE = new EntityRef<WEBSERVICE>();
      this._USER = new EntityRef<USER>();
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, PERMISSION.emptyChangingEventArgs);
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
      entity.PERMISSION = this;
    }

    private void detach_WEBSERVICESBASIS(interop.WEBSERVICESBASIS entity)
    {
      this.SendPropertyChanging();
      entity.PERMISSION = (PERMISSION) null;
    }
  }
}
