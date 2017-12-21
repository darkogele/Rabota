// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.GetPodatociByEDBResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceUJP_EDB
{
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://ujpwcfserviceapplication.interop.local/GetOsnovniPodatociByEDB.svc")]
  [Serializable]
  public class GetPodatociByEDBResponse : INotifyPropertyChanged
  {
    private TransOsnovniPodatociStruktura getPodatociByEDBResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public TransOsnovniPodatociStruktura GetPodatociByEDBResult
    {
      get
      {
        return this.getPodatociByEDBResultField;
      }
      set
      {
        this.getPodatociByEDBResultField = value;
        this.RaisePropertyChanged("GetPodatociByEDBResult");
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
