// Decompiled with JetBrains decompiler
// Type: interop.bam_UniversalServiceControlProduction_Completed
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.bam_UniversalServiceControlProduction_Completed")]
  public class bam_UniversalServiceControlProduction_Completed : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _RecordID;
    private string _ActivityID;
    private DateTime? _TimeRequest;
    private string _RequestID;
    private string _UserID;
    private string _Name;
    private string _Surname;
    private string _UserEMail;
    private string _UserActive;
    private int? _PermisionUse;
    private string _PermisionActive;
    private string _InstitutionTittle;
    private string _InstitutionDesc;
    private string _InstitutionActive;
    private string _WSTittle;
    private string _WSNote;
    private string _WSActive;
    private string _WSDesc;
    private string _WSURL;
    private DateTime? _TimeResponse;
    private string _ResponseID;
    private string _ResponseBody;
    private string _Username;
    private string _RequestBasis;
    private DateTime? _LastModified;

    [Column(AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true, Storage = "_RecordID")]
    public long RecordID
    {
      get
      {
        return this._RecordID;
      }
      set
      {
        if (this._RecordID == value)
          return;
        this.SendPropertyChanging();
        this._RecordID = value;
        this.SendPropertyChanged("RecordID");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(128) NOT NULL", Storage = "_ActivityID")]
    public string ActivityID
    {
      get
      {
        return this._ActivityID;
      }
      set
      {
        if (!(this._ActivityID != value))
          return;
        this.SendPropertyChanging();
        this._ActivityID = value;
        this.SendPropertyChanged("ActivityID");
      }
    }

    [Column(DbType = "DateTime", Storage = "_TimeRequest")]
    public DateTime? TimeRequest
    {
      get
      {
        return this._TimeRequest;
      }
      set
      {
        DateTime? nullable1 = this._TimeRequest;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._TimeRequest = value;
        this.SendPropertyChanged("TimeRequest");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_RequestID")]
    public string RequestID
    {
      get
      {
        return this._RequestID;
      }
      set
      {
        if (!(this._RequestID != value))
          return;
        this.SendPropertyChanging();
        this._RequestID = value;
        this.SendPropertyChanged("RequestID");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_UserID")]
    public string UserID
    {
      get
      {
        return this._UserID;
      }
      set
      {
        if (!(this._UserID != value))
          return;
        this.SendPropertyChanging();
        this._UserID = value;
        this.SendPropertyChanged("UserID");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_Name")]
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

    [Column(DbType = "NVarChar(50)", Storage = "_Surname")]
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

    [Column(DbType = "NVarChar(50)", Storage = "_UserEMail")]
    public string UserEMail
    {
      get
      {
        return this._UserEMail;
      }
      set
      {
        if (!(this._UserEMail != value))
          return;
        this.SendPropertyChanging();
        this._UserEMail = value;
        this.SendPropertyChanged("UserEMail");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_UserActive")]
    public string UserActive
    {
      get
      {
        return this._UserActive;
      }
      set
      {
        if (!(this._UserActive != value))
          return;
        this.SendPropertyChanging();
        this._UserActive = value;
        this.SendPropertyChanged("UserActive");
      }
    }

    [Column(DbType = "Int", Storage = "_PermisionUse")]
    public int? PermisionUse
    {
      get
      {
        return this._PermisionUse;
      }
      set
      {
        int? nullable1 = this._PermisionUse;
        int? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.SendPropertyChanging();
        this._PermisionUse = value;
        this.SendPropertyChanged("PermisionUse");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_PermisionActive")]
    public string PermisionActive
    {
      get
      {
        return this._PermisionActive;
      }
      set
      {
        if (!(this._PermisionActive != value))
          return;
        this.SendPropertyChanging();
        this._PermisionActive = value;
        this.SendPropertyChanged("PermisionActive");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_InstitutionTittle")]
    public string InstitutionTittle
    {
      get
      {
        return this._InstitutionTittle;
      }
      set
      {
        if (!(this._InstitutionTittle != value))
          return;
        this.SendPropertyChanging();
        this._InstitutionTittle = value;
        this.SendPropertyChanged("InstitutionTittle");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_InstitutionDesc")]
    public string InstitutionDesc
    {
      get
      {
        return this._InstitutionDesc;
      }
      set
      {
        if (!(this._InstitutionDesc != value))
          return;
        this.SendPropertyChanging();
        this._InstitutionDesc = value;
        this.SendPropertyChanged("InstitutionDesc");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_InstitutionActive")]
    public string InstitutionActive
    {
      get
      {
        return this._InstitutionActive;
      }
      set
      {
        if (!(this._InstitutionActive != value))
          return;
        this.SendPropertyChanging();
        this._InstitutionActive = value;
        this.SendPropertyChanged("InstitutionActive");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_WSTittle")]
    public string WSTittle
    {
      get
      {
        return this._WSTittle;
      }
      set
      {
        if (!(this._WSTittle != value))
          return;
        this.SendPropertyChanging();
        this._WSTittle = value;
        this.SendPropertyChanged("WSTittle");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_WSNote")]
    public string WSNote
    {
      get
      {
        return this._WSNote;
      }
      set
      {
        if (!(this._WSNote != value))
          return;
        this.SendPropertyChanging();
        this._WSNote = value;
        this.SendPropertyChanged("WSNote");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_WSActive")]
    public string WSActive
    {
      get
      {
        return this._WSActive;
      }
      set
      {
        if (!(this._WSActive != value))
          return;
        this.SendPropertyChanging();
        this._WSActive = value;
        this.SendPropertyChanged("WSActive");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_WSDesc")]
    public string WSDesc
    {
      get
      {
        return this._WSDesc;
      }
      set
      {
        if (!(this._WSDesc != value))
          return;
        this.SendPropertyChanging();
        this._WSDesc = value;
        this.SendPropertyChanged("WSDesc");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_WSURL")]
    public string WSURL
    {
      get
      {
        return this._WSURL;
      }
      set
      {
        if (!(this._WSURL != value))
          return;
        this.SendPropertyChanging();
        this._WSURL = value;
        this.SendPropertyChanged("WSURL");
      }
    }

    [Column(DbType = "DateTime", Storage = "_TimeResponse")]
    public DateTime? TimeResponse
    {
      get
      {
        return this._TimeResponse;
      }
      set
      {
        DateTime? nullable1 = this._TimeResponse;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._TimeResponse = value;
        this.SendPropertyChanged("TimeResponse");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_ResponseID")]
    public string ResponseID
    {
      get
      {
        return this._ResponseID;
      }
      set
      {
        if (!(this._ResponseID != value))
          return;
        this.SendPropertyChanging();
        this._ResponseID = value;
        this.SendPropertyChanged("ResponseID");
      }
    }

    [Column(DbType = "NVarChar(100)", Storage = "_ResponseBody")]
    public string ResponseBody
    {
      get
      {
        return this._ResponseBody;
      }
      set
      {
        if (!(this._ResponseBody != value))
          return;
        this.SendPropertyChanging();
        this._ResponseBody = value;
        this.SendPropertyChanged("ResponseBody");
      }
    }

    [Column(DbType = "NVarChar(50)", Storage = "_Username")]
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

    [Column(DbType = "NVarChar(350)", Storage = "_RequestBasis")]
    public string RequestBasis
    {
      get
      {
        return this._RequestBasis;
      }
      set
      {
        if (!(this._RequestBasis != value))
          return;
        this.SendPropertyChanging();
        this._RequestBasis = value;
        this.SendPropertyChanged("RequestBasis");
      }
    }

    [Column(DbType = "DateTime", Storage = "_LastModified")]
    public DateTime? LastModified
    {
      get
      {
        return this._LastModified;
      }
      set
      {
        DateTime? nullable1 = this._LastModified;
        DateTime? nullable2 = value;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.SendPropertyChanging();
        this._LastModified = value;
        this.SendPropertyChanged("LastModified");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, bam_UniversalServiceControlProduction_Completed.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
