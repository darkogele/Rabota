﻿// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_UJP.VratiCRMRezultatiUJPResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceCR_UJP
{
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebservicecr.vista.local/WCFServiceUJP.svc")]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [DebuggerStepThrough]
  [Serializable]
  public class VratiCRMRezultatiUJPResponse : INotifyPropertyChanged
  {
    private CrmResponse vratiCRMRezultatiUJPResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public CrmResponse VratiCRMRezultatiUJPResult
    {
      get
      {
        return this.vratiCRMRezultatiUJPResultField;
      }
      set
      {
        this.vratiCRMRezultatiUJPResultField = value;
        this.RaisePropertyChanged("VratiCRMRezultatiUJPResult");
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