// Decompiled with JetBrains decompiler
// Type: interop.USER
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
  [Table(Name = "dbo.USERS")]
  public class USER : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private Guid _ID;
    private long? _ID_CERT;
    private string _Name;
    private string _Surname;
    private string _email;
    private bool _Active;
    private DateTime _CreateOn;
    private string _username;
    private string _password;
    private int _Type;
    private DateTime? _ModifiedAt;
    private string _IpAdress;
    private EntitySet<PERMISSION> _PERMISSIONs;
    private EntityRef<CERTIFICATE> _CERTIFICATE;

    [Column(DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true, Storage = "_ID")]
    public Guid ID
    {
      get
      {
        return this._ID;
      }
      set
      {
        if (!(this._ID != value))
          return;
        this.SendPropertyChanging();
        this._ID = value;
        this.SendPropertyChanged("ID");
      }
    }

    [Column(DbType = "BigInt", Storage = "_ID_CERT")]
    public long? ID_CERT
    {
      get
      {
        return this._ID_CERT;
      }
      set
      {
        long? nullable1 = this._ID_CERT;
        long? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        if (this._CERTIFICATE.HasLoadedOrAssignedValue)
          throw new ForeignKeyReferenceAlreadyHasValueException();
        this.SendPropertyChanging();
        this._ID_CERT = value;
        this.SendPropertyChanged("ID_CERT");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(100) NOT NULL", Storage = "_Name")]
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

    [Column(CanBeNull = false, DbType = "NVarChar(100) NOT NULL", Storage = "_Surname")]
    public string Surname
    {
      get
      {
        return this._Surname;
      }
      set
      {
        if (!(this._Surname != value))
          return;
        this.SendPropertyChanging();
        this._Surname = value;
        this.SendPropertyChanged("Surname");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(50) NOT NULL", Storage = "_email")]
    public string email
    {
      get
      {
        return this._email;
      }
      set
      {
        if (!(this._email != value))
          return;
        this.SendPropertyChanging();
        this._email = value;
        this.SendPropertyChanged("email");
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

    [Column(DbType = "DateTime NOT NULL", Storage = "_CreateOn")]
    public DateTime CreateOn
    {
      get
      {
        return this._CreateOn;
      }
      set
      {
        if (!(this._CreateOn != value))
          return;
        this.SendPropertyChanging();
        this._CreateOn = value;
        this.SendPropertyChanged("CreateOn");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(100) NOT NULL", Storage = "_username")]
    public string username
    {
      get
      {
        return this._username;
      }
      set
      {
        if (!(this._username != value))
          return;
        this.SendPropertyChanging();
        this._username = value;
        this.SendPropertyChanged("username");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(100) NOT NULL", Storage = "_password")]
    public string password
    {
      get
      {
        return this._password;
      }
      set
      {
        if (!(this._password != value))
          return;
        this.SendPropertyChanging();
        this._password = value;
        this.SendPropertyChanged("password");
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

    [Column(DbType = "DateTime", Storage = "_ModifiedAt")]
    public DateTime? ModifiedAt
    {
      get
      {
        return this._ModifiedAt;
      }
      set
      {
        DateTime? nullable1 = this._ModifiedAt;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._ModifiedAt = value;
        this.SendPropertyChanged("ModifiedAt");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_IpAdress")]
    public string IpAdress
    {
      get
      {
        return this._IpAdress;
      }
      set
      {
        if (!(this._IpAdress != value))
          return;
        this.SendPropertyChanging();
        this._IpAdress = value;
        this.SendPropertyChanged("IpAdress");
      }
    }

    [Association(Name = "USER_PERMISSION", OtherKey = "ID_USER", Storage = "_PERMISSIONs", ThisKey = "ID")]
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

    [Association(IsForeignKey = true, Name = "CERTIFICATE_USER", OtherKey = "ID", Storage = "_CERTIFICATE", ThisKey = "ID_CERT")]
    public CERTIFICATE CERTIFICATE
    {
      get
      {
        return this._CERTIFICATE.Entity;
      }
      set
      {
        CERTIFICATE entity = this._CERTIFICATE.Entity;
        if (entity == value && this._CERTIFICATE.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._CERTIFICATE.Entity = (CERTIFICATE) null;
          entity.USERs.Remove(this);
        }
        this._CERTIFICATE.Entity = value;
        if (value != null)
        {
          value.USERs.Add(this);
          this._ID_CERT = new long?(value.ID);
        }
        else
          this._ID_CERT = new long?();
        this.SendPropertyChanged("CERTIFICATE");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public USER()
    {
      this._PERMISSIONs = new EntitySet<PERMISSION>(new Action<PERMISSION>(this.attach_PERMISSIONs), new Action<PERMISSION>(this.detach_PERMISSIONs));
      this._CERTIFICATE = new EntityRef<CERTIFICATE>();
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, USER.emptyChangingEventArgs);
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
      entity.USER = this;
    }

    private void detach_PERMISSIONs(PERMISSION entity)
    {
      this.SendPropertyChanging();
      entity.USER = (USER) null;
    }
  }
}
