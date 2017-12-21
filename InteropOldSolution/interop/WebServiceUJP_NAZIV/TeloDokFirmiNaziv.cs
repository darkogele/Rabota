// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.TeloDokFirmiNaziv
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
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication.NaziviFirmiByNaziv")]
  [Serializable]
  public class TeloDokFirmiNaziv : INotifyPropertyChanged
  {
    private DokumentOpisStruktura dokumentOpisField;
    private string greshkaPorakaField;
    private DokFirmiNazivStrukturaNazivEdb[] nazivEdbField;
    private object nazivField;

    [XmlElement(IsNullable = true, Order = 0)]
    public DokumentOpisStruktura DokumentOpis
    {
      get
      {
        return this.dokumentOpisField;
      }
      set
      {
        this.dokumentOpisField = value;
        this.RaisePropertyChanged("DokumentOpis");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string GreshkaPoraka
    {
      get
      {
        return this.greshkaPorakaField;
      }
      set
      {
        this.greshkaPorakaField = value;
        this.RaisePropertyChanged("GreshkaPoraka");
      }
    }

    [XmlArray(IsNullable = true, Order = 2)]
    public DokFirmiNazivStrukturaNazivEdb[] NazivEdb
    {
      get
      {
        return this.nazivEdbField;
      }
      set
      {
        this.nazivEdbField = value;
        this.RaisePropertyChanged("NazivEdb");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public object naziv
    {
      get
      {
        return this.nazivField;
      }
      set
      {
        this.nazivField = value;
        this.RaisePropertyChanged("naziv");
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
