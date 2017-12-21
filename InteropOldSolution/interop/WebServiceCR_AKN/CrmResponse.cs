// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_AKN.CrmResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceCR_AKN
{
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/CR_AKN_RESPONSE")]
  [DebuggerStepThrough]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [Serializable]
  public class CrmResponse : INotifyPropertyChanged
  {
    private CrmResponseCrmResultItems[] itemsFieldField;

    [XmlArray(IsNullable = true, Order = 0)]
    public CrmResponseCrmResultItems[] itemsField
    {
      get
      {
        return this.itemsFieldField;
      }
      set
      {
        this.itemsFieldField = value;
        this.RaisePropertyChanged("itemsField");
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
