// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.IWCFServiceAKNDataSets
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace interop.WebServiceAKNOpstiniDataSets
{
  [ServiceContract(ConfigurationName = "WebServiceAKNOpstiniDataSets.IWCFServiceAKNDataSets")]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public interface IWCFServiceAKNDataSets
  {
    [OperationContract(Action = "http://tempuri.org/IWCFServiceAKNDataSets/GetGradoviDataSet", ReplyAction = "http://tempuri.org/IWCFServiceAKNDataSets/GetGradoviDataSetResponse")]
    opstini GetGradoviDataSet();

    [OperationContract(Action = "http://tempuri.org/IWCFServiceAKNDataSets/GetOpstiniDataSet", ReplyAction = "http://tempuri.org/IWCFServiceAKNDataSets/GetOpstiniDataSetResponse")]
    katopstini GetOpstiniDataSet(string opstina);
  }
}
