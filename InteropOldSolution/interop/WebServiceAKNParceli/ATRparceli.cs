// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.ATRparceli
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
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKN.AKNWebService")]
  [DebuggerStepThrough]
  [Serializable]
  public class ATRparceli : INotifyPropertyChanged
  {
    private string messageFieldField;
    private atributiparcela[] nizparFieldField;

    [XmlElement(IsNullable = true, Order = 0)]
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

    [XmlArray(IsNullable = true, Order = 1)]
    public atributiparcela[] nizparField
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
