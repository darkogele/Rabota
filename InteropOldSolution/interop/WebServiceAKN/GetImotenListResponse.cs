// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.GetImotenListResponse
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
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebserviceakn.vista.local/WCFServiceAKN.svc")]
  [DebuggerStepThrough]
  [Serializable]
  public class GetImotenListResponse : INotifyPropertyChanged
  {
    private dzgr getImotenListResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public dzgr GetImotenListResult
    {
      get
      {
        return this.getImotenListResultField;
      }
      set
      {
        this.getImotenListResultField = value;
        this.RaisePropertyChanged("GetImotenListResult");
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
