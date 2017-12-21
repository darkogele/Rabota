// Decompiled with JetBrains decompiler
// Type: interop.WEBSERVICE
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
  [Table(Name = "dbo.WEBSERVICES")]
  public class WEBSERVICE : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Tittle;
    private string _Description;
    private string _Note;
    private string _URL;
    private bool _Active;
    private DateTime _CreatedOn;
    private EntitySet<PARAM> _PARAMs;
    private EntitySet<PERMISSION> _PERMISSIONs;
    private EntitySet<DOCUMENTSTRUCTURE> _DOCUMENTSTRUCTUREs;

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

    [Column(CanBeNull = false, DbType = "NVarChar(500) NOT NULL", Storage = "_Tittle")]
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

    [Column(DbType = "NVarChar(2000)", Storage = "_Description")]
    public string Description
    {
      get
      {
        return this._Description;
      }
      set
      {
        if (!(this._Description != value))
          return;
        this.SendPropertyChanging();
        this._Description = value;
        this.SendPropertyChanged("Description");
      }
    }

    [Column(DbType = "NVarChar(2000)", Storage = "_Note")]
    public string Note
    {
      get
      {
        return this._Note;
      }
      set
      {
        if (!(this._Note != value))
          return;
        this.SendPropertyChanging();
        this._Note = value;
        this.SendPropertyChanged("Note");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(1000) NOT NULL", Storage = "_URL")]
    public string URL
    {
      get
      {
        return this._URL;
      }
      set
      {
        if (!(this._URL != value))
          return;
        this.SendPropertyChanging();
        this._URL = value;
        this.SendPropertyChanged("URL");
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

    [Association(Name = "WEBSERVICE_PARAM", OtherKey = "ID_WS", Storage = "_PARAMs", ThisKey = "ID")]
    public EntitySet<PARAM> PARAMs
    {
      get
      {
        return this._PARAMs;
      }
      set
      {
        this._PARAMs.Assign((IEnumerable<PARAM>) value);
      }
    }

    [Association(Name = "WEBSERVICE_PERMISSION", OtherKey = "ID_WS", Storage = "_PERMISSIONs", ThisKey = "ID")]
    public EntitySet<PERMISSION> PERMISSIONs
    {
      get
      {
        return this._PERMISSIONs;
      }
      set
      {
        this._PERMISSIONs.Assign((IEnumerable<PERMISSION>) value);
      }
    }

    [Association(Name = "WEBSERVICE_DOCUMENTSTRUCTURE", OtherKey = "ID_WS", Storage = "_DOCUMENTSTRUCTUREs", ThisKey = "ID")]
    public EntitySet<DOCUMENTSTRUCTURE> DOCUMENTSTRUCTUREs
    {
      get
      {
        return this._DOCUMENTSTRUCTUREs;
      }
      set
      {
        this._DOCUMENTSTRUCTUREs.Assign((IEnumerable<DOCUMENTSTRUCTURE>) value);
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public WEBSERVICE()
    {
      this._PARAMs = new EntitySet<PARAM>(new Action<PARAM>(this.attach_PARAMs), new Action<PARAM>(this.detach_PARAMs));
      this._PERMISSIONs = new EntitySet<PERMISSION>(new Action<PERMISSION>(this.attach_PERMISSIONs), new Action<PERMISSION>(this.detach_PERMISSIONs));
      this._DOCUMENTSTRUCTUREs = new EntitySet<DOCUMENTSTRUCTURE>(new Action<DOCUMENTSTRUCTURE>(this.attach_DOCUMENTSTRUCTUREs), new Action<DOCUMENTSTRUCTURE>(this.detach_DOCUMENTSTRUCTUREs));
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, WEBSERVICE.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void attach_PARAMs(PARAM entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = this;
    }

    private void detach_PARAMs(PARAM entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = (WEBSERVICE) null;
    }

    private void attach_PERMISSIONs(PERMISSION entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = this;
    }

    private void detach_PERMISSIONs(PERMISSION entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = (WEBSERVICE) null;
    }

    private void attach_DOCUMENTSTRUCTUREs(DOCUMENTSTRUCTURE entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = this;
    }

    private void detach_DOCUMENTSTRUCTUREs(DOCUMENTSTRUCTURE entity)
    {
      this.SendPropertyChanging();
      entity.WEBSERVICE = (WEBSERVICE) null;
    }
  }
}
