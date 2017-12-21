// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.sopstvenici
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
  public class sopstvenici : INotifyPropertyChanged
  {
    private string brojFieldField;
    private string delFieldField;
    private string embgFieldField;
    private string imeFieldField;
    private string mestoFieldField;
    private string ulicaFieldField;

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
    public string delField
    {
      get
      {
        return this.delFieldField;
      }
      set
      {
        this.delFieldField = value;
        this.RaisePropertyChanged("delField");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string embgField
    {
      get
      {
        return this.embgFieldField;
      }
      set
      {
        this.embgFieldField = value;
        this.RaisePropertyChanged("embgField");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public string imeField
    {
      get
      {
        return this.imeFieldField;
      }
      set
      {
        this.imeFieldField = value;
        this.RaisePropertyChanged("imeField");
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

    [XmlElement(IsNullable = true, Order = 5)]
    public string ulicaField
    {
      get
      {
        return this.ulicaFieldField;
      }
      set
      {
        this.ulicaFieldField = value;
        this.RaisePropertyChanged("ulicaField");
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
