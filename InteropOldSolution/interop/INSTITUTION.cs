// Decompiled with JetBrains decompiler
// Type: interop.INSTITUTION
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
  [Table(Name = "dbo.INSTITUTIONS")]
  public class INSTITUTION : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Tittle;
    private string _Description;
    private bool _Active;
    private DateTime _CreatedOn;
    private EntitySet<PERMISSION> _PERMISSIONs;

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

    [Column(CanBeNull = false, DbType = "NVarChar(300) NOT NULL", Storage = "_Tittle")]
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

    [Column(DbType = "NVarChar(1000)", Storage = "_Description")]
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

    [Association(Name = "INSTITUTION_PERMISSION", OtherKey = "ID_INST", Storage = "_PERMISSIONs", ThisKey = "ID")]
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

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public INSTITUTION()
    {
      this._PERMISSIONs = new EntitySet<PERMISSION>(new Action<PERMISSION>(this.attach_PERMISSIONs), new Action<PERMISSION>(this.detach_PERMISSIONs));
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, INSTITUTION.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void attach_PERMISSIONs(PERMISSION entity)
    {
      this.SendPropertyChanging();
      entity.INSTITUTION = this;
    }

    private void detach_PERMISSIONs(PERMISSION entity)
    {
      this.SendPropertyChanging();
      entity.INSTITUTION = (INSTITUTION) null;
    }
  }
}
