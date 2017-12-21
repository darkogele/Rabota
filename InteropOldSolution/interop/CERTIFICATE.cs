// Decompiled with JetBrains decompiler
// Type: interop.CERTIFICATE
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
  [Table(Name = "dbo.CERTIFICATE")]
  public class CERTIFICATE : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Subject;
    private string _Thumbprint;
    private string _SerialNumber;
    private string _Issuer;
    private DateTime _ValidFrom;
    private DateTime _ValidTo;
    private EntitySet<USER> _USERs;

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

    [Column(CanBeNull = false, DbType = "NVarChar(350) NOT NULL", Storage = "_Subject")]
    public string Subject
    {
      get
      {
        return this._Subject;
      }
      set
      {
        if (!(this._Subject != value))
          return;
        this.SendPropertyChanging();
        this._Subject = value;
        this.SendPropertyChanged("Subject");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(350) NOT NULL", Storage = "_Thumbprint")]
    public string Thumbprint
    {
      get
      {
        return this._Thumbprint;
      }
      set
      {
        if (!(this._Thumbprint != value))
          return;
        this.SendPropertyChanging();
        this._Thumbprint = value;
        this.SendPropertyChanged("Thumbprint");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(350) NOT NULL", Storage = "_SerialNumber")]
    public string SerialNumber
    {
      get
      {
        return this._SerialNumber;
      }
      set
      {
        if (!(this._SerialNumber != value))
          return;
        this.SendPropertyChanging();
        this._SerialNumber = value;
        this.SendPropertyChanged("SerialNumber");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(350) NOT NULL", Storage = "_Issuer")]
    public string Issuer
    {
      get
      {
        return this._Issuer;
      }
      set
      {
        if (!(this._Issuer != value))
          return;
        this.SendPropertyChanging();
        this._Issuer = value;
        this.SendPropertyChanged("Issuer");
      }
    }

    [Column(DbType = "DateTime NOT NULL", Storage = "_ValidFrom")]
    public DateTime ValidFrom
    {
      get
      {
        return this._ValidFrom;
      }
      set
      {
        if (!(this._ValidFrom != value))
          return;
        this.SendPropertyChanging();
        this._ValidFrom = value;
        this.SendPropertyChanged("ValidFrom");
      }
    }

    [Column(DbType = "DateTime NOT NULL", Storage = "_ValidTo")]
    public DateTime ValidTo
    {
      get
      {
        return this._ValidTo;
      }
      set
      {
        if (!(this._ValidTo != value))
          return;
        this.SendPropertyChanging();
        this._ValidTo = value;
        this.SendPropertyChanged("ValidTo");
      }
    }

    [Association(Name = "CERTIFICATE_USER", OtherKey = "ID_CERT", Storage = "_USERs", ThisKey = "ID")]
    public EntitySet<USER> USERs
    {
      get
      {
        return this._USERs;
      }
      set
      {
        this._USERs.Assign((IEnumerable<USER>) value);
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public CERTIFICATE()
    {
      this._USERs = new EntitySet<USER>(new Action<USER>(this.attach_USERs), new Action<USER>(this.detach_USERs));
    }

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, CERTIFICATE.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void attach_USERs(USER entity)
    {
      this.SendPropertyChanging();
      entity.CERTIFICATE = this;
    }

    private void detach_USERs(USER entity)
    {
      this.SendPropertyChanging();
      entity.CERTIFICATE = (CERTIFICATE) null;
    }
  }
}
