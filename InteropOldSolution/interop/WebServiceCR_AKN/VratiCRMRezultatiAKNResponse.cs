// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_AKN.VratiCRMRezultatiAKNResponse
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
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebservicecr.vista.local/WCFServiceAKN.svc")]
  [Serializable]
  public class VratiCRMRezultatiAKNResponse : INotifyPropertyChanged
  {
    private CrmResponse vratiCRMRezultatiAKNResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public CrmResponse VratiCRMRezultatiAKNResult
    {
      get
      {
        return this.vratiCRMRezultatiAKNResultField;
      }
      set
      {
        this.vratiCRMRezultatiAKNResultField = value;
        this.RaisePropertyChanged("VratiCRMRezultatiAKNResult");
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
