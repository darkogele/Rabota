// Decompiled with JetBrains decompiler
// Type: interop.DOCUMENTSTRUCTURE
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.DOCUMENTSTRUCTURE")]
  public class DOCUMENTSTRUCTURE : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private long _ID_WS;
    private string _Name;
    private string _Description;
    private string _XMLSchema;
    private string _Purpose;
    private DateTime _CreatedOn;
    private Guid _CreatedBy;
    private DateTime? _ModifiedOn;
    private Guid? _ModifiedBy;
    private EntityRef<WEBSERVICE> _WEBSERVICE;

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

    [Column(CanBeNull = false, DbType = "NVarChar(MAX) NOT NULL", Storage = "_Name")]
    public string Name
    {
      get
      {
        return this._Name;
      }
      set
      {
        if (!(this._Name != value))
          return;
        this.SendPropertyChanging();
        this._Name = value;
        this.SendPropertyChanged("Name");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(MAX) NOT NULL", Storage = "_Description")]
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

    [Column(CanBeNull = false, DbType = "NVarChar(MAX) NOT NULL", Storage = "_XMLSchema")]
    public string XMLSchema
    {
      get
      {
        return this._XMLSchema;
      }
      set
      {
        if (!(this._XMLSchema != value))
          return;
        this.SendPropertyChanging();
        this._XMLSchema = value;
        this.SendPropertyChanged("XMLSchema");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(MAX) NOT NULL", Storage = "_Purpose")]
    public string Purpose
    {
      get
      {
        return this._Purpose;
      }
      set
      {
        if (!(this._Purpose != value))
          return;
        this.SendPropertyChanging();
        this._Purpose = value;
        this.SendPropertyChanged("Purpose");
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

    [Column(DbType = "UniqueIdentifier NOT NULL", Storage = "_CreatedBy")]
    public Guid CreatedBy
    {
      get
      {
        return this._CreatedBy;
      }
      set
      {
        if (!(this._CreatedBy != value))
          return;
        this.SendPropertyChanging();
        this._CreatedBy = value;
        this.SendPropertyChanged("CreatedBy");
      }
    }

    [Column(DbType = "DateTime", Storage = "_ModifiedOn")]
    public DateTime? ModifiedOn
    {
      get
      {
        return this._ModifiedOn;
      }
      set
      {
        DateTime? nullable1 = this._ModifiedOn;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._ModifiedOn = value;
        this.SendPropertyChanged("ModifiedOn");
      }
    }

    [Column(DbType = "UniqueIdentifier", Storage = "_ModifiedBy")]
    public Guid? ModifiedBy
    {
      get
      {
        return this._ModifiedBy;
      }
      set
      {
        Guid? nullable1 = this._ModifiedBy;
        Guid? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._ModifiedBy = value;
        this.SendPropertyChanged("ModifiedBy");
      }
    }

    [Association(IsForeignKey = true, Name = "WEBSERVICE_DOCUMENTSTRUCTURE", OtherKey = "ID", Storage = "_WEBSERVICE", ThisKey = "ID_WS")]
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
          entity.DOCUMENTSTRUCTUREs.Remove(this);
        }
        this._WEBSERVICE.Entity = value;
        if (value != null)
        {
          value.DOCUMENTSTRUCTUREs.Add(this);
          this._ID_WS = value.ID;
        }
        else
          this._ID_WS = 0L;
        this.SendPropertyChanged("WEBSERVICE");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public DOCUMENTSTRUCTURE()
    {
      this._WEBSERVICE = new EntityRef<WEBSERVICE>();
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, DOCUMENTSTRUCTURE.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
