﻿// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNParceli.Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceAKNParceli
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [ServiceContract(ConfigurationName = "WebServiceAKNParceli.Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT")]
  public interface Module_1_AKNParceliOrchestration_WCF_PARCELI_REQRESP_PORT
  {
    [OperationContract(Action = "Operation_Request", ReplyAction = "*")]
    [XmlSerializerFormat]
    Operation_RequestResponse Operation_Request(Operation_RequestRequest request);
  }
}
