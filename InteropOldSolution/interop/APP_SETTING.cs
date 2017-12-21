// Decompiled with JetBrains decompiler
// Type: interop.APP_SETTING
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace interop
{
  [Table(Name = "dbo.APP_SETTINGS")]
  public class APP_SETTING : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private int _ID;
    private bool? _CanCopyPrintScreen;
    private bool? _IPLimit;

    [Column(AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true, Storage = "_ID")]
    public int ID
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

    [Column(DbType = "Bit", Storage = "_CanCopyPrintScreen")]
    public bool? CanCopyPrintScreen
    {
      get
      {
        return this._CanCopyPrintScreen;
      }
      set
      {
        bool? nullable1 = this._CanCopyPrintScreen;
        bool? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.SendPropertyChanging();
        this._CanCopyPrintScreen = value;
        this.SendPropertyChanged("CanCopyPrintScreen");
      }
    }

    [Column(DbType = "Bit", Storage = "_IPLimit")]
    public bool? IPLimit
    {
      get
      {
        return this._IPLimit;
      }
      set
      {
        bool? nullable1 = this._IPLimit;
        bool? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.SendPropertyChanging();
        this._IPLimit = value;
        this.SendPropertyChanged("IPLimit");
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, APP_SETTING.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
