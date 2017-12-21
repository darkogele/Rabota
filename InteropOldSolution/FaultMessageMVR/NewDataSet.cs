// Decompiled with JetBrains decompiler
// Type: FaultMessageMVR.NewDataSet
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace FaultMessageMVR
{
  [XmlType(AnonymousType = true, Namespace = "http://MVRBizTalkApplication.MVRFultMessage")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlRoot(IsNullable = false, Namespace = "http://MVRBizTalkApplication.MVRFultMessage")]
  [GeneratedCode("xsd", "2.0.50727.3038")]
  [Serializable]
  public class NewDataSet
  {
    private MVRFultMessage[] itemsField;

    [XmlElement("MVRFultMessage")]
    public MVRFultMessage[] Items
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
