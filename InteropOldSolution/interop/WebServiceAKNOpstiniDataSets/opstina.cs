// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.opstina
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace interop.WebServiceAKNOpstiniDataSets
{
  [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
  [DataContract(Name = "opstina", Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKNDataSets.WebServisKatastar")]
  [DebuggerStepThrough]
  [Serializable]
  public class opstina : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    private string nazivopstinaFieldField;
    private int opsFieldField;

    [Browsable(false)]
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [DataMember(IsRequired = true)]
    public string nazivopstinaField
    {
      get
      {
        return this.nazivopstinaFieldField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.nazivopstinaFieldField, (object) value))
          return;
        this.nazivopstinaFieldField = value;
        this.RaisePropertyChanged("nazivopstinaField");
      }
    }

    [DataMember(IsRequired = true)]
    public int opsField
    {
      get
      {
        return this.opsFieldField;
      }
      set
      {
        if (this.opsFieldField.Equals(value))
          return;
        this.opsFieldField = value;
        this.RaisePropertyChanged("opsField");
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
