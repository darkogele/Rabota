// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.katopstina
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
  [DataContract(Name = "katopstina", Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKNDataSets.WebServisKatastar")]
  [DebuggerStepThrough]
  [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
  [Serializable]
  public class katopstina : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    private int kopsFieldField;
    private string nazivkatastarskaopstinaFieldField;
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
    public int kopsField
    {
      get
      {
        return this.kopsFieldField;
      }
      set
      {
        if (this.kopsFieldField.Equals(value))
          return;
        this.kopsFieldField = value;
        this.RaisePropertyChanged("kopsField");
      }
    }

    [DataMember(IsRequired = true)]
    public string nazivkatastarskaopstinaField
    {
      get
      {
        return this.nazivkatastarskaopstinaFieldField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.nazivkatastarskaopstinaFieldField, (object) value))
          return;
        this.nazivkatastarskaopstinaFieldField = value;
        this.RaisePropertyChanged("nazivkatastarskaopstinaField");
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
