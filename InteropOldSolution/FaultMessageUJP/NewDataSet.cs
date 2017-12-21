// Decompiled with JetBrains decompiler
// Type: FaultMessageUJP.NewDataSet
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace FaultMessageUJP
{
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://interop.vista.local/")]
  [XmlRoot(IsNullable = false, Namespace = "http://interop.vista.local/")]
  [GeneratedCode("xsd", "2.0.50727.3038")]
  [DesignerCategory("code")]
  [Serializable]
  public class NewDataSet
  {
      private FaultMessageUJPClass[] itemsField;

    [XmlElement("FaultMessageUJP")]
      public FaultMessageUJPClass[] Items
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
