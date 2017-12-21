// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_CU.VratiCRMRezultatiCUResponse
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
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebservicecr.vista.local/WCFServiceCU.svc")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class VratiCRMRezultatiCUResponse : INotifyPropertyChanged
  {
    private CrmResponse vratiCRMRezultatiCUResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public CrmResponse VratiCRMRezultatiCUResult
    {
      get
      {
        return this.vratiCRMRezultatiCUResultField;
      }
      set
      {
        this.vratiCRMRezultatiCUResultField = value;
        this.RaisePropertyChanged("VratiCRMRezultatiCUResult");
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
