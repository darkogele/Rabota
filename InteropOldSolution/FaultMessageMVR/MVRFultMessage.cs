// Decompiled with JetBrains decompiler
// Type: FaultMessageMVR.MVRFultMessage
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaultMessageMVR
{
  [XmlType(AnonymousType = true, Namespace = "http://MVRBizTalkApplication.MVRFultMessage")]
  [XmlRoot(IsNullable = false, Namespace = "http://MVRBizTalkApplication.MVRFultMessage")]
  [GeneratedCode("xsd", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class MVRFultMessage
  {
    private string errorField;

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Error
    {
      get
      {
        return this.errorField;
      }
      set
      {
        this.errorField = value;
      }
    }
  }
}
