﻿// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.GetPodatociFirmiNaziviByNazivResponse
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceUJP_NAZIV
{
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://ujpwcfserviceapplication.interop.local/GetNaziviFirmiByNaziv.svc")]
  [DebuggerStepThrough]
  [Serializable]
  public class GetPodatociFirmiNaziviByNazivResponse : INotifyPropertyChanged
  {
    private TransFirmiNaziviStruktura getPodatociFirmiNaziviByNazivResultField;

    [XmlElement(IsNullable = true, Order = 0)]
    public TransFirmiNaziviStruktura GetPodatociFirmiNaziviByNazivResult
    {
      get
      {
        return this.getPodatociFirmiNaziviByNazivResultField;
      }
      set
      {
        this.getPodatociFirmiNaziviByNazivResultField = value;
        this.RaisePropertyChanged("GetPodatociFirmiNaziviByNazivResult");
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
