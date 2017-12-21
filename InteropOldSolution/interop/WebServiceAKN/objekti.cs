// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.objekti
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
  [DebuggerStepThrough]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKN.AKNWebService")]
  [DesignerCategory("code")]
  [Serializable]
  public class objekti : INotifyPropertyChanged
  {
    private string brojFieldField;
    private string godinagradbaFieldField;
    private string katFieldField;
    private string mestoFieldField;
    private string namenaFieldField;
    private int objektFieldField;
    private string osnovFieldField;
    private long povrsinaFieldField;
    private string pravoFieldField;
    private string stanFieldField;
    private string vlezFieldField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string brojField
    {
      get
      {
        return this.brojFieldField;
      }
      set
      {
        this.brojFieldField = value;
        this.RaisePropertyChanged("brojField");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string godinagradbaField
    {
      get
      {
        return this.godinagradbaFieldField;
      }
      set
      {
        this.godinagradbaFieldField = value;
        this.RaisePropertyChanged("godinagradbaField");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string katField
    {
      get
      {
        return this.katFieldField;
      }
      set
      {
        this.katFieldField = value;
        this.RaisePropertyChanged("katField");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public string mestoField
    {
      get
      {
        return this.mestoFieldField;
      }
      set
      {
        this.mestoFieldField = value;
        this.RaisePropertyChanged("mestoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 4)]
    public string namenaField
    {
      get
      {
        return this.namenaFieldField;
      }
      set
      {
        this.namenaFieldField = value;
        this.RaisePropertyChanged("namenaField");
      }
    }

    [XmlElement(Order = 5)]
    public int objektField
    {
      get
      {
        return this.objektFieldField;
      }
      set
      {
        this.objektFieldField = value;
        this.RaisePropertyChanged("objektField");
      }
    }

    [XmlElement(IsNullable = true, Order = 6)]
    public string osnovField
    {
      get
      {
        return this.osnovFieldField;
      }
      set
      {
        this.osnovFieldField = value;
        this.RaisePropertyChanged("osnovField");
      }
    }

    [XmlElement(Order = 7)]
    public long povrsinaField
    {
      get
      {
        return this.povrsinaFieldField;
      }
      set
      {
        this.povrsinaFieldField = value;
        this.RaisePropertyChanged("povrsinaField");
      }
    }

    [XmlElement(IsNullable = true, Order = 8)]
    public string pravoField
    {
      get
      {
        return this.pravoFieldField;
      }
      set
      {
        this.pravoFieldField = value;
        this.RaisePropertyChanged("pravoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 9)]
    public string stanField
    {
      get
      {
        return this.stanFieldField;
      }
      set
      {
        this.stanFieldField = value;
        this.RaisePropertyChanged("stanField");
      }
    }

    [XmlElement(IsNullable = true, Order = 10)]
    public string vlezField
    {
      get
      {
        return this.vlezFieldField;
      }
      set
      {
        this.vlezFieldField = value;
        this.RaisePropertyChanged("vlezField");
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
