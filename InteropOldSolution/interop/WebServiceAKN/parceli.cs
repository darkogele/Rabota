// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.parceli
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
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKN.AKNWebService")]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class parceli : INotifyPropertyChanged
  {
    private string broj_delFieldField;
    private string kulturaFieldField;
    private string mestoFieldField;
    private int objektFieldField;
    private long povrsinaFieldField;
    private string pravoFieldField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string broj_delField
    {
      get
      {
        return this.broj_delFieldField;
      }
      set
      {
        this.broj_delFieldField = value;
        this.RaisePropertyChanged("broj_delField");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string kulturaField
    {
      get
      {
        return this.kulturaFieldField;
      }
      set
      {
        this.kulturaFieldField = value;
        this.RaisePropertyChanged("kulturaField");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
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

    [XmlElement(Order = 3)]
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

    [XmlElement(Order = 4)]
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

    [XmlElement(IsNullable = true, Order = 5)]
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
