// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKN.ImotenListSchema
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace interop.WebServiceAKN
{
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://AKNBizTalkApplication.ImotenListSchema")]
  [Serializable]
  public class ImotenListSchema : INotifyPropertyChanged
  {
    private string usernameField;
    private string passwordField;
    private string gradField;
    private string opstinaField;
    private string imotenListField;
    private string timeStampField;
    private string osnovNaBaranjeField;
    private string nacinNaIsprakjanjeField;

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
    public string Grad
    {
      get
      {
        return this.gradField;
      }
      set
      {
        this.gradField = value;
        this.RaisePropertyChanged("Grad");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public string Opstina
    {
      get
      {
        return this.opstinaField;
      }
      set
      {
        this.opstinaField = value;
        this.RaisePropertyChanged("Opstina");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public string ImotenList
    {
      get
      {
        return this.imotenListField;
      }
      set
      {
        this.imotenListField = value;
        this.RaisePropertyChanged("ImotenList");
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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
