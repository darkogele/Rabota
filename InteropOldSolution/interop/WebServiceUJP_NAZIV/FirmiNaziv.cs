// Decompiled with JetBrains decompiler
// Type: interop.WebServiceUJP_NAZIV.FirmiNaziv
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace interop.WebServiceUJP_NAZIV
{
  [XmlType(AnonymousType = true, Namespace = "http://UJPBizTalkApplicationNazivi.FirmiNaziv")]
  [GeneratedCode("System.Xml", "2.0.50727.3053")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class FirmiNaziv : INotifyPropertyChanged
  {
    private string usernameField;
    private string passwordField;
    private string timeStampField;
    private string osnovNaBaranjeField;
    private string nacinNaIsprakjanjeField;
    private string nazivField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string Username
    {
      get
      {
        return this.usernameField;
      }
      set
      {
        this.usernameField = value;
        this.RaisePropertyChanged("Username");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string Password
    {
      get
      {
        return this.passwordField;
      }
      set
      {
        this.passwordField = value;
        this.RaisePropertyChanged("Password");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public string TimeStamp
    {
      get
      {
        return this.timeStampField;
      }
      set
      {
        this.timeStampField = value;
        this.RaisePropertyChanged("TimeStamp");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public string OsnovNaBaranje
    {
      get
      {
        return this.osnovNaBaranjeField;
      }
      set
      {
        this.osnovNaBaranjeField = value;
        this.RaisePropertyChanged("OsnovNaBaranje");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public string NacinNaIsprakjanje
    {
      get
      {
        return this.nacinNaIsprakjanjeField;
      }
      set
      {
        this.nacinNaIsprakjanjeField = value;
        this.RaisePropertyChanged("NacinNaIsprakjanje");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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
