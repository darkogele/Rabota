// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_UJP.EMBS
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace interop.WebServiceCR_UJP
{
  [XmlType(AnonymousType = true, Namespace = "http://CRBizTalkApplicationUJP.EMBS")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [DebuggerStepThrough]
  [Serializable]
  public class EMBS : INotifyPropertyChanged
  {
    private string eMBS1Field;
    private string eMBS2Field;
    private string eMBS3Field;
    private string eMBS4Field;
    private string eMBS5Field;
    private string eMBS6Field;
    private string eMBS7Field;
    private string eMBS8Field;
    private string usernameField;
    private string passwordField;
    private string osnovNaBaranjeField;
    private string nacinNaIsprakjanjeField;
    private string timeStampField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string EMBS1
    {
      get
      {
        return this.eMBS1Field;
      }
      set
      {
        this.eMBS1Field = value;
        this.RaisePropertyChanged("EMBS1");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string EMBS2
    {
      get
      {
        return this.eMBS2Field;
      }
      set
      {
        this.eMBS2Field = value;
        this.RaisePropertyChanged("EMBS2");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public string EMBS3
    {
      get
      {
        return this.eMBS3Field;
      }
      set
      {
        this.eMBS3Field = value;
        this.RaisePropertyChanged("EMBS3");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public string EMBS4
    {
      get
      {
        return this.eMBS4Field;
      }
      set
      {
        this.eMBS4Field = value;
        this.RaisePropertyChanged("EMBS4");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public string EMBS5
    {
      get
      {
        return this.eMBS5Field;
      }
      set
      {
        this.eMBS5Field = value;
        this.RaisePropertyChanged("EMBS5");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
    public string EMBS6
    {
      get
      {
        return this.eMBS6Field;
      }
      set
      {
        this.eMBS6Field = value;
        this.RaisePropertyChanged("EMBS6");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
    public string EMBS7
    {
      get
      {
        return this.eMBS7Field;
      }
      set
      {
        this.eMBS7Field = value;
        this.RaisePropertyChanged("EMBS7");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
    public string EMBS8
    {
      get
      {
        return this.eMBS8Field;
      }
      set
      {
        this.eMBS8Field = value;
        this.RaisePropertyChanged("EMBS8");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
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
