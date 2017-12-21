// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.DokumentOpisStruktura
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
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DesignerCategory("code")]
  [Serializable]
  public class DokumentOpisStruktura : INotifyPropertyChanged
  {
    private string dokumentIDField;
    private string imeField;
    private string opisField;
    private DateTime vremenskaMarkaField;
    private bool vremenskaMarkaFieldSpecified;

    [XmlElement(IsNullable = true, Order = 0)]
    public string DokumentID
    {
      get
      {
        return this.dokumentIDField;
      }
      set
      {
        this.dokumentIDField = value;
        this.RaisePropertyChanged("DokumentID");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string Ime
    {
      get
      {
        return this.imeField;
      }
      set
      {
        this.imeField = value;
        this.RaisePropertyChanged("Ime");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string Opis
    {
      get
      {
        return this.opisField;
      }
      set
      {
        this.opisField = value;
        this.RaisePropertyChanged("Opis");
      }
    }

    [XmlElement(Order = 3)]
    public DateTime VremenskaMarka
    {
      get
      {
        return this.vremenskaMarkaField;
      }
      set
      {
        this.vremenskaMarkaField = value;
        this.RaisePropertyChanged("VremenskaMarka");
      }
    }

    [XmlIgnore]
    public bool VremenskaMarkaSpecified
    {
      get
      {
        return this.vremenskaMarkaFieldSpecified;
      }
      set
      {
        this.vremenskaMarkaFieldSpecified = value;
        this.RaisePropertyChanged("VremenskaMarkaSpecified");
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
