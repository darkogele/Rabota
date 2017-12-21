// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.GetPodatociStrukturaParcelaResponse
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
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebserviceakn.vista.local/WCFServeiceAKNParcela.svc")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [Serializable]
  public class GetPodatociStrukturaParcelaResponse : INotifyPropertyChanged
  {
    private ATRparceli getPodatociStrukturaParcelaResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public ATRparceli GetPodatociStrukturaParcelaResult
    {
      get
      {
        return this.getPodatociStrukturaParcelaResultField;
      }
      set
      {
        this.getPodatociStrukturaParcelaResultField = value;
        this.RaisePropertyChanged("GetPodatociStrukturaParcelaResult");
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
