// Decompiled with JetBrains decompiler
// Type: FaultMessageAKN.NewDataSet
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace FaultMessageAKN
{
  [XmlRoot(IsNullable = false, Namespace = "http://wcfwebservicecr.vista.local/WCFServiceAKN.svc")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://wcfwebservicecr.vista.local/WCFServiceAKN.svc")]
  [DesignerCategory("code")]
  [GeneratedCode("xsd", "2.0.50727.3038")]
  [Serializable]
  public class NewDataSet
  {
      private FaultMessageAKNClass[] itemsField;

    [XmlElement("FaultMessageAKN")]
      public FaultMessageAKNClass[] Items
    {
      get
      {
        return this.itemsField;
      }
      set
      {
        this.itemsField = value;
      }
    }
  }
}
