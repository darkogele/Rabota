// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.TransInformacijaStrukturaVremenskaMarka
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceUJP_EDB
{
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication")]
  [Serializable]
  public class TransInformacijaStrukturaVremenskaMarka : INotifyPropertyChanged
  {
    private DateTime vremeDobienoCSField;
    private bool vremeDobienoCSFieldSpecified;
    private DateTime vremeDobienoDstField;
    private bool vremeDobienoDstFieldSpecified;
    private bool vremeDobienoDstSpecified1Field;
    private bool vremeDobienoDstSpecified1FieldSpecified;
    private DateTime vremePratenoCSField;
    private bool vremePratenoCSFieldSpecified;
    private bool vremePratenoCSSpecified1Field;
    private bool vremePratenoCSSpecified1FieldSpecified;
    private DateTime vremePratenoIzvField;
    private bool vremePratenoIzvFieldSpecified;
    private bool vremePratenoIzvSpecified1Field;
    private bool vremePratenoIzvSpecified1FieldSpecified;

    [XmlElement(Order = 0)]
    public DateTime VremeDobienoCS
    {
      get
      {
        return this.vremeDobienoCSField;
      }
      set
      {
        this.vremeDobienoCSField = value;
        this.RaisePropertyChanged("VremeDobienoCS");
      }
    }

    [XmlIgnore]
    public bool VremeDobienoCSSpecified
    {
      get
      {
        return this.vremeDobienoCSFieldSpecified;
      }
      set
      {
        this.vremeDobienoCSFieldSpecified = value;
        this.RaisePropertyChanged("VremeDobienoCSSpecified");
      }
    }

    [XmlElement(Order = 1)]
    public DateTime VremeDobienoDst
    {
      get
      {
        return this.vremeDobienoDstField;
      }
      set
      {
        this.vremeDobienoDstField = value;
        this.RaisePropertyChanged("VremeDobienoDst");
      }
    }

    [XmlIgnore]
    public bool VremeDobienoDstSpecified
    {
      get
      {
        return this.vremeDobienoDstFieldSpecified;
      }
      set
      {
        this.vremeDobienoDstFieldSpecified = value;
        this.RaisePropertyChanged("VremeDobienoDstSpecified");
      }
    }

    [XmlElement("VremeDobienoDstSpecified", Order = 2)]
    public bool VremeDobienoDstSpecified1
    {
      get
      {
        return this.vremeDobienoDstSpecified1Field;
      }
      set
      {
        this.vremeDobienoDstSpecified1Field = value;
        this.RaisePropertyChanged("VremeDobienoDstSpecified1");
      }
    }

    [XmlIgnore]
    public bool VremeDobienoDstSpecified1Specified
    {
      get
      {
        return this.vremeDobienoDstSpecified1FieldSpecified;
      }
      set
      {
        this.vremeDobienoDstSpecified1FieldSpecified = value;
        this.RaisePropertyChanged("VremeDobienoDstSpecified1Specified");
      }
    }

    [XmlElement(Order = 3)]
    public DateTime VremePratenoCS
    {
      get
      {
        return this.vremePratenoCSField;
      }
      set
      {
        this.vremePratenoCSField = value;
        this.RaisePropertyChanged("VremePratenoCS");
      }
    }

    [XmlIgnore]
    public bool VremePratenoCSSpecified
    {
      get
      {
        return this.vremePratenoCSFieldSpecified;
      }
      set
      {
        this.vremePratenoCSFieldSpecified = value;
        this.RaisePropertyChanged("VremePratenoCSSpecified");
      }
    }

    [XmlElement("VremePratenoCSSpecified", Order = 4)]
    public bool VremePratenoCSSpecified1
    {
      get
      {
        return this.vremePratenoCSSpecified1Field;
      }
      set
      {
        this.vremePratenoCSSpecified1Field = value;
        this.RaisePropertyChanged("VremePratenoCSSpecified1");
      }
    }

    [XmlIgnore]
    public bool VremePratenoCSSpecified1Specified
    {
      get
      {
        return this.vremePratenoCSSpecified1FieldSpecified;
      }
      set
      {
        this.vremePratenoCSSpecified1FieldSpecified = value;
        this.RaisePropertyChanged("VremePratenoCSSpecified1Specified");
      }
    }

    [XmlElement(Order = 5)]
    public DateTime VremePratenoIzv
    {
      get
      {
        return this.vremePratenoIzvField;
      }
      set
      {
        this.vremePratenoIzvField = value;
        this.RaisePropertyChanged("VremePratenoIzv");
      }
    }

    [XmlIgnore]
    public bool VremePratenoIzvSpecified
    {
      get
      {
        return this.vremePratenoIzvFieldSpecified;
      }
      set
      {
        this.vremePratenoIzvFieldSpecified = value;
        this.RaisePropertyChanged("VremePratenoIzvSpecified");
      }
    }

    [XmlElement("VremePratenoIzvSpecified", Order = 6)]
    public bool VremePratenoIzvSpecified1
    {
      get
      {
        return this.vremePratenoIzvSpecified1Field;
      }
      set
      {
        this.vremePratenoIzvSpecified1Field = value;
        this.RaisePropertyChanged("VremePratenoIzvSpecified1");
      }
    }

    [XmlIgnore]
    public bool VremePratenoIzvSpecified1Specified
    {
      get
      {
        return this.vremePratenoIzvSpecified1FieldSpecified;
      }
      set
      {
        this.vremePratenoIzvSpecified1FieldSpecified = value;
        this.RaisePropertyChanged("VremePratenoIzvSpecified1Specified");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
