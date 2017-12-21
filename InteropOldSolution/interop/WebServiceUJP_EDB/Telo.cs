// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.Telo
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
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication")]
  [DebuggerStepThrough]
  [Serializable]
  public class Telo : INotifyPropertyChanged
  {
    private TeloDokOsnovniPodatoci dokOsnovniPodatociField;
    private string signatureField;

    [XmlElement(IsNullable = true, Order = 0)]
    public TeloDokOsnovniPodatoci DokOsnovniPodatoci
    {
      get
      {
        return this.dokOsnovniPodatociField;
      }
      set
      {
        this.dokOsnovniPodatociField = value;
        this.RaisePropertyChanged("DokOsnovniPodatoci");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string Signature
    {
      get
      {
        return this.signatureField;
      }
      set
      {
        this.signatureField = value;
        this.RaisePropertyChanged("Signature");
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
