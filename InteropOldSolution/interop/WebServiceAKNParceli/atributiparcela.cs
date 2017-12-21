// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.atributiparcela
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceAKNParceli
{
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKN.AKNWebService")]
  [Serializable]
  public class atributiparcela : INotifyPropertyChanged
  {
    private string broj_delFieldField;
    private string ilistFieldField;
    private string kopsFieldField;
    private string kulturaFieldField;
    private string mestoFieldField;
    private int objektFieldField;
    private string opsFieldField;
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

    [XmlElement(IsNullable = true, Order = 4)]
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
