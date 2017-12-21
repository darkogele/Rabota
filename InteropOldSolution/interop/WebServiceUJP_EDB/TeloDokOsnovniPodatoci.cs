// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_EDB.TeloDokOsnovniPodatoci
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
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication")]
  [Serializable]
  public class TeloDokOsnovniPodatoci : INotifyPropertyChanged
  {
    private string adresaField;
    private string bankaField;
    private TeloDokOsnovniPodatociDokumentOpis dokumentOpisField;
    private string edbField;
    private string faksField;
    private string greshkaPorakaField;
    private string mbField;
    private string mesto_nazivField;
    private string nazivField;
    private string of_opisField;
    private string opis_naceField;
    private string telefonField;
    private string ziroField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string Adresa
    {
      get
      {
        return this.adresaField;
      }
      set
      {
        this.adresaField = value;
        this.RaisePropertyChanged("Adresa");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string Banka
    {
      get
      {
        return this.bankaField;
      }
      set
      {
        this.bankaField = value;
        this.RaisePropertyChanged("Banka");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public TeloDokOsnovniPodatociDokumentOpis DokumentOpis
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

    [XmlElement(IsNullable = true, Order = 3)]
    public string Edb
    {
      get
      {
        return this.edbField;
      }
      set
      {
        this.edbField = value;
        this.RaisePropertyChanged("Edb");
      }
    }

    [XmlElement(IsNullable = true, Order = 4)]
    public string Faks
    {
      get
      {
        return this.faksField;
      }
      set
      {
        this.faksField = value;
        this.RaisePropertyChanged("Faks");
      }
    }

    [XmlElement(IsNullable = true, Order = 5)]
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

    [XmlElement(IsNullable = true, Order = 6)]
    public string MB
    {
      get
      {
        return this.mbField;
      }
      set
      {
        this.mbField = value;
        this.RaisePropertyChanged("MB");
      }
    }

    [XmlElement(IsNullable = true, Order = 7)]
    public string Mesto_naziv
    {
      get
      {
        return this.mesto_nazivField;
      }
      set
      {
        this.mesto_nazivField = value;
        this.RaisePropertyChanged("Mesto_naziv");
      }
    }

    [XmlElement(IsNullable = true, Order = 8)]
    public string Naziv
    {
      get
      {
        return this.nazivField;
      }
      set
      {
        this.nazivField = value;
        this.RaisePropertyChanged("Naziv");
      }
    }

    [XmlElement(IsNullable = true, Order = 9)]
    public string Of_opis
    {
      get
      {
        return this.of_opisField;
      }
      set
      {
        this.of_opisField = value;
        this.RaisePropertyChanged("Of_opis");
      }
    }

    [XmlElement(IsNullable = true, Order = 10)]
    public string Opis_nace
    {
      get
      {
        return this.opis_naceField;
      }
      set
      {
        this.opis_naceField = value;
        this.RaisePropertyChanged("Opis_nace");
      }
    }

    [XmlElement(IsNullable = true, Order = 11)]
    public string Telefon
    {
      get
      {
        return this.telefonField;
      }
      set
      {
        this.telefonField = value;
        this.RaisePropertyChanged("Telefon");
      }
    }

    [XmlElement(IsNullable = true, Order = 12)]
    public string Ziro
    {
      get
      {
        return this.ziroField;
      }
      set
      {
        this.ziroField = value;
        this.RaisePropertyChanged("Ziro");
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
