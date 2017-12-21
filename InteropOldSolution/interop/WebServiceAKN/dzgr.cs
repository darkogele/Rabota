// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.dzgr
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceAKN
{
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKN.AKNWebService")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class dzgr : INotifyPropertyChanged
  {
    private string dataFieldField;
    private string ilistFieldField;
    private string kopsFieldField;
    private string messageFieldField;
    private objekti[] nizobjFieldField;
    private parceli[] nizparFieldField;
    private sopstvenici[] nizsopFieldField;
    private tovari[] niztovFieldField;
    private string opsFieldField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string dataField
    {
      get
      {
        return this.dataFieldField;
      }
      set
      {
        this.dataFieldField = value;
        this.RaisePropertyChanged("dataField");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string ilistField
    {
      get
      {
        return this.ilistFieldField;
      }
      set
      {
        this.ilistFieldField = value;
        this.RaisePropertyChanged("ilistField");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string kopsField
    {
      get
      {
        return this.kopsFieldField;
      }
      set
      {
        this.kopsFieldField = value;
        this.RaisePropertyChanged("kopsField");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public string messageField
    {
      get
      {
        return this.messageFieldField;
      }
      set
      {
        this.messageFieldField = value;
        this.RaisePropertyChanged("messageField");
      }
    }

    [XmlArray(IsNullable = true, Order = 4)]
    public objekti[] nizobjField
    {
      get
      {
        return this.nizobjFieldField;
      }
      set
      {
        this.nizobjFieldField = value;
        this.RaisePropertyChanged("nizobjField");
      }
    }

    [XmlArray(IsNullable = true, Order = 5)]
    public parceli[] nizparField
    {
      get
      {
        return this.nizparFieldField;
      }
      set
      {
        this.nizparFieldField = value;
        this.RaisePropertyChanged("nizparField");
      }
    }

    [XmlArray(IsNullable = true, Order = 6)]
    public sopstvenici[] nizsopField
    {
      get
      {
        return this.nizsopFieldField;
      }
      set
      {
        this.nizsopFieldField = value;
        this.RaisePropertyChanged("nizsopField");
      }
    }

    [XmlArray(IsNullable = true, Order = 7)]
    public tovari[] niztovField
    {
      get
      {
        return this.niztovFieldField;
      }
      set
      {
        this.niztovFieldField = value;
        this.RaisePropertyChanged("niztovField");
      }
    }

    [XmlElement(IsNullable = true, Order = 8)]
    public string opsField
    {
      get
      {
        return this.opsFieldField;
      }
      set
      {
        this.opsFieldField = value;
        this.RaisePropertyChanged("opsField");
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
