﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRRMTest.CURM_TEST {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="CURM_TEST.ITest_TS_CURM")]
    public interface ITest_TS_CURM {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ITest_TS_CURM/Get_TS_CURM", ReplyAction="http://interop.org/ITest_TS_CURM/Get_TS_CURMResponse")]
        string Get_TS_CURM(string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ITest_TS_CURM/Get_TS_CURM", ReplyAction="http://interop.org/ITest_TS_CURM/Get_TS_CURMResponse")]
        System.Threading.Tasks.Task<string> Get_TS_CURMAsync(string param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITest_TS_CURMChannel : CRRMTest.CURM_TEST.ITest_TS_CURM, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Test_TS_CURMClient : System.ServiceModel.ClientBase<CRRMTest.CURM_TEST.ITest_TS_CURM>, CRRMTest.CURM_TEST.ITest_TS_CURM {
        
        public Test_TS_CURMClient() {
        }
        
        public Test_TS_CURMClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Test_TS_CURMClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Test_TS_CURMClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Test_TS_CURMClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Get_TS_CURM(string param) {
            return base.Channel.Get_TS_CURM(param);
        }
        
        public System.Threading.Tasks.Task<string> Get_TS_CURMAsync(string param) {
            return base.Channel.Get_TS_CURMAsync(param);
        }
    }
}
