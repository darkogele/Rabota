﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TSAdapterAKN.Test_TS_AKN {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="Test_TS_AKN.ITest_TS_AKN")]
    public interface ITest_TS_AKN {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ITest_TS_AKN/Get_TS_AKN", ReplyAction="http://interop.org/ITest_TS_AKN/Get_TS_AKNResponse")]
        string Get_TS_AKN(string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ITest_TS_AKN/Get_TS_AKN", ReplyAction="http://interop.org/ITest_TS_AKN/Get_TS_AKNResponse")]
        System.Threading.Tasks.Task<string> Get_TS_AKNAsync(string param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITest_TS_AKNChannel : TSAdapterAKN.Test_TS_AKN.ITest_TS_AKN, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Test_TS_AKNClient : System.ServiceModel.ClientBase<TSAdapterAKN.Test_TS_AKN.ITest_TS_AKN>, TSAdapterAKN.Test_TS_AKN.ITest_TS_AKN {
        
        public Test_TS_AKNClient() {
        }
        
        public Test_TS_AKNClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Test_TS_AKNClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Test_TS_AKNClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Test_TS_AKNClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Get_TS_AKN(string param) {
            return base.Channel.Get_TS_AKN(param);
        }
        
        public System.Threading.Tasks.Task<string> Get_TS_AKNAsync(string param) {
            return base.Channel.Get_TS_AKNAsync(param);
        }
    }
}
