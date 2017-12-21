// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.TransInformacijaStruktura
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
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/UJPWcfServiceApplication.NaziviFirmiByNaziv")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class TransInformacijaStruktura : INotifyPropertyChanged
  {
    private string institucijaDoRefField;
    private string institucijaOdRefField;
    private string nacinNaIsprakanjeField;
    private string osnovNaBaranjeRefField;
    private string porakaUIDField;
    private string tipNaDokumentField;
    private string transPredhodnikRefField;
    private string uIDField;
    private TransInformacijaStrukturaVremenskaMarka vremenskaMarkaField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string InstitucijaDoRef
    {
      get
      {
        return this.institucijaDoRefField;
      }
      set
      {
        this.institucijaDoRefField = value;
        this.RaisePropertyChanged("InstitucijaDoRef");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string InstitucijaOdRef
    {
      get
      {
        return this.institucijaOdRefField;
      }
      set
      {
        this.institucijaOdRefField = value;
        this.RaisePropertyChanged("InstitucijaOdRef");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string NacinNaIsprakanje
    {
      get
      {
        return this.nacinNaIsprakanjeField;
      }
      set
      {
        this.nacinNaIsprakanjeField = value;
        this.RaisePropertyChanged("NacinNaIsprakanje");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public string OsnovNaBaranjeRef
    {
      get
      {
        return this.osnovNaBaranjeRefField;
      }
      set
      {
        this.osnovNaBaranjeRefField = value;
        this.RaisePropertyChanged("OsnovNaBaranjeRef");
      }
    }

    [XmlElement(IsNullable = true, Order = 4)]
    public string PorakaUID
    {
      get
      {
        return this.porakaUIDField;
      }
      set
      {
        this.porakaUIDField = value;
        this.RaisePropertyChanged("PorakaUID");
      }
    }

    [XmlElement(IsNullable = true, Order = 5)]
    public string TipNaDokument
    {
      get
      {
        return this.tipNaDokumentField;
      }
      set
      {
        this.tipNaDokumentField = value;
        this.RaisePropertyChanged("TipNaDokument");
      }
    }

    [XmlElement(IsNullable = true, Order = 6)]
    public string TransPredhodnikRef
    {
      get
      {
        return this.transPredhodnikRefField;
      }
      set
      {
        this.transPredhodnikRefField = value;
        this.RaisePropertyChanged("TransPredhodnikRef");
      }
    }

    [XmlElement(IsNullable = true, Order = 7)]
    public string UID
    {
      get
      {
        return this.uIDField;
      }
      set
      {
        this.uIDField = value;
        this.RaisePropertyChanged("UID");
      }
    }

    [XmlElement(IsNullable = true, Order = 8)]
    public TransInformacijaStrukturaVremenskaMarka VremenskaMarka
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
