// Decompiled with JetBrains decompiler
// Type: interop.REPORT
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.REPORTS")]
  public class REPORT : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _RecordID;
    private Guid _IDUserCreator;
    private string _ReportFilePath;
    private string _ReportFileName;
    private DateTime _CreatedOn;

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

    [Column(CanBeNull = false, DbType = "NVarChar(50) NOT NULL", Storage = "_RecordID")]
    public string RecordID
    {
      get
      {
        return this._RecordID;
      }
      set
      {
        if (!(this._RecordID != value))
          return;
        this.SendPropertyChanging();
        this._RecordID = value;
        this.SendPropertyChanged("RecordID");
      }
    }

    [Column(DbType = "UniqueIdentifier NOT NULL", Storage = "_IDUserCreator")]
    public Guid IDUserCreator
    {
      get
      {
        return this._IDUserCreator;
      }
      set
      {
        if (!(this._IDUserCreator != value))
          return;
        this.SendPropertyChanging();
        this._IDUserCreator = value;
        this.SendPropertyChanged("IDUserCreator");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(250) NOT NULL", Storage = "_ReportFilePath")]
    public string ReportFilePath
    {
      get
      {
        return this._ReportFilePath;
      }
      set
      {
        if (!(this._ReportFilePath != value))
          return;
        this.SendPropertyChanging();
        this._ReportFilePath = value;
        this.SendPropertyChanged("ReportFilePath");
      }
    }

    [Column(CanBeNull = false, DbType = "NVarChar(250) NOT NULL", Storage = "_ReportFileName")]
    public string ReportFileName
    {
      get
      {
        return this._ReportFileName;
      }
      set
      {
        if (!(this._ReportFileName != value))
          return;
        this.SendPropertyChanging();
        this._ReportFileName = value;
        this.SendPropertyChanged("ReportFileName");
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

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, REPORT.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
