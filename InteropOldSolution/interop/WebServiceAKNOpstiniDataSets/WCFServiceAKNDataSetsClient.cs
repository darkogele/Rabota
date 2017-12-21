// Decompiled with JetBrains decompiler
// Type: interop.WebServiceAKNOpstiniDataSets.WCFServiceAKNDataSetsClient
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace interop.WebServiceAKNOpstiniDataSets
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  public class WCFServiceAKNDataSetsClient : ClientBase<IWCFServiceAKNDataSets>, IWCFServiceAKNDataSets
  {
    public WCFServiceAKNDataSetsClient()
    {
    }

    public WCFServiceAKNDataSetsClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public WCFServiceAKNDataSetsClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WCFServiceAKNDataSetsClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WCFServiceAKNDataSetsClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public opstini GetGradoviDataSet()
    {
      return this.Channel.GetGradoviDataSet();
    }

    public katopstini GetOpstiniDataSet(string opstina)
    {
      return this.Channel.GetOpstiniDataSet(opstina);
    }
  }
}
