// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.TransFirmiNaziviStruktura
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
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication.NaziviFirmiByNaziv")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [Serializable]
  public class TransFirmiNaziviStruktura : INotifyPropertyChanged
  {
    private Telo teloField;
    private TransInformacijaStruktura transInformacijaField;

    [XmlElement(IsNullable = true, Order = 0)]
    public Telo Telo
    {
      get
      {
        return this.teloField;
      }
      set
      {
        this.teloField = value;
        this.RaisePropertyChanged("Telo");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public TransInformacijaStruktura TransInformacija
    {
      get
      {
        return this.transInformacijaField;
      }
      set
      {
        this.transInformacijaField = value;
        this.RaisePropertyChanged("TransInformacija");
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
