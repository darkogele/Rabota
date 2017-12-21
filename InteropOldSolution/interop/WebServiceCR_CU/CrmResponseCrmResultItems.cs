// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_CU.CrmResponseCrmResultItems
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceCR_CU
{
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/CR_CU_RESPONSE")]
  [DebuggerStepThrough]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [Serializable]
  public class CrmResponseCrmResultItems : INotifyPropertyChanged
  {
    private CrmResponseCrmResultItemsCrmResultItem[] crmResultItemFieldField;
    private string templateNameFieldField;

    [XmlArray(IsNullable = true, Order = 0)]
    public CrmResponseCrmResultItemsCrmResultItem[] crmResultItemField
    {
      get
      {
        return this.crmResultItemFieldField;
      }
      set
      {
        this.crmResultItemFieldField = value;
        this.RaisePropertyChanged("crmResultItemField");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string templateNameField
    {
      get
      {
        return this.templateNameFieldField;
      }
      set
      {
        this.templateNameFieldField = value;
        this.RaisePropertyChanged("templateNameField");
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
