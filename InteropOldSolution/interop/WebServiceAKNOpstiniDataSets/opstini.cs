// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.opstini
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
  [DebuggerStepThrough]
  [DataContract(Name = "opstini", Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKNDataSets.WebServisKatastar")]
  [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
  [Serializable]
  public class opstini : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    private opstina[] nizopsFieldField;

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
    public opstina[] nizopsField
    {
      get
      {
        return this.nizopsFieldField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.nizopsFieldField, (object) value))
          return;
        this.nizopsFieldField = value;
        this.RaisePropertyChanged("nizopsField");
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
