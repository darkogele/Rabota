﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InstitutionNameServicesTest.Host.MockServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MockServiceReference.IMockServiceTest")]
    public interface IMockServiceTest {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMockServiceTest/GetEnvironmentName_TestMethod", ReplyAction="http://tempuri.org/IMockServiceTest/GetEnvironmentName_TestMethodResponse")]
        string GetEnvironmentName_TestMethod();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMockServiceTest/GetEnvironmentName_TestMethod", ReplyAction="http://tempuri.org/IMockServiceTest/GetEnvironmentName_TestMethodResponse")]
        System.Threading.Tasks.Task<string> GetEnvironmentName_TestMethodAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMockServiceTestChannel : InstitutionNameServicesTest.Host.MockServiceReference.IMockServiceTest, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MockServiceTestClient : System.ServiceModel.ClientBase<InstitutionNameServicesTest.Host.MockServiceReference.IMockServiceTest>, InstitutionNameServicesTest.Host.MockServiceReference.IMockServiceTest {
        
        public MockServiceTestClient() {
        }
        
        public MockServiceTestClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MockServiceTestClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MockServiceTestClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MockServiceTestClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetEnvironmentName_TestMethod() {
            return base.Channel.GetEnvironmentName_TestMethod();
        }
        
        public System.Threading.Tasks.Task<string> GetEnvironmentName_TestMethodAsync() {
            return base.Channel.GetEnvironmentName_TestMethodAsync();
        }
    }
}
