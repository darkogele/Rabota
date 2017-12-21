// Decompiled with JetBrains decompiler
// Type: interop.PARAM
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.PARAMS")]
  public class PARAM : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Tittle;
    private string _Description;
    private int _MaxLength;
    private int _Type;
    private long _ID_WS;
    private bool _Acitve;
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

    [Column(DbType = "Int NOT NULL", Storage = "_MaxLength")]
    public int MaxLength
    {
      get
      {
        return this._MaxLength;
      }
      set
      {
        if (this._MaxLength == value)
          return;
        this.SendPropertyChanging();
        this._MaxLength = value;
        this.SendPropertyChanged("MaxLength");
      }
    }

    [Column(DbType = "Int NOT NULL", Storage = "_Type")]
    public int Type
    {
      get
      {
        return this._Type;
      }
      set
      {
        if (this._Type == value)
          return;
        this.SendPropertyChanging();
        this._Type = value;
        this.SendPropertyChanged("Type");
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

    [Column(DbType = "Bit NOT NULL", Storage = "_Acitve")]
    public bool Acitve
    {
      get
      {
        return this._Acitve;
      }
      set
      {
        if (this._Acitve == value)
          return;
        this.SendPropertyChanging();
        this._Acitve = value;
        this.SendPropertyChanged("Acitve");
      }
    }

    [Association(IsForeignKey = true, Name = "WEBSERVICE_PARAM", OtherKey = "ID", Storage = "_WEBSERVICE", ThisKey = "ID_WS")]
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
          entity.PARAMs.Remove(this);
        }
        this._WEBSERVICE.Entity = value;
        if (value != null)
        {
          value.PARAMs.Add(this);
          this._ID_WS = value.ID;
        }
        else
          this._ID_WS = 0L;
        this.SendPropertyChanged("WEBSERVICE");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public PARAM()
    {
      this._WEBSERVICE = new EntityRef<WEBSERVICE>();
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, PARAM.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
