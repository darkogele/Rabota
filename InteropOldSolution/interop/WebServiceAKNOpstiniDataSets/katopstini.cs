// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.katopstini
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
  [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
  [DataContract(Name = "katopstini", Namespace = "http://schemas.datacontract.org/2004/07/WCFServiceAKNDataSets.WebServisKatastar")]
  [Serializable]
  public class katopstini : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    private katopstina[] nizkopsFieldField;

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
    public katopstina[] nizkopsField
    {
      get
      {
        return this.nizkopsFieldField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.nizkopsFieldField, (object) value))
          return;
        this.nizkopsFieldField = value;
        this.RaisePropertyChanged("nizkopsField");
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
