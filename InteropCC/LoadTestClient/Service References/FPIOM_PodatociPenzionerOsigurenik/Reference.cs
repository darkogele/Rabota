﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadTestClient.FPIOM_PodatociPenzionerOsigurenik {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="FPIOM_PodatociPenzionerOsigurenik.IFPIOMService")]
    public interface IFPIOMService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IFPIOMService/GetDataForRetired", ReplyAction="http://interop.org/IFPIOMService/GetDataForRetiredResponse")]
        string GetDataForRetired(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IFPIOMService/GetDataForRetired", ReplyAction="http://interop.org/IFPIOMService/GetDataForRetiredResponse")]
        System.Threading.Tasks.Task<string> GetDataForRetiredAsync(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IFPIOMService/GetDataForEnsurees", ReplyAction="http://interop.org/IFPIOMService/GetDataForEnsureesResponse")]
        string GetDataForEnsurees(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IFPIOMService/GetDataForEnsurees", ReplyAction="http://interop.org/IFPIOMService/GetDataForEnsureesResponse")]
        System.Threading.Tasks.Task<string> GetDataForEnsureesAsync(string EMBG);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFPIOMServiceChannel : LoadTestClient.FPIOM_PodatociPenzionerOsigurenik.IFPIOMService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FPIOMServiceClient : System.ServiceModel.ClientBase<LoadTestClient.FPIOM_PodatociPenzionerOsigurenik.IFPIOMService>, LoadTestClient.FPIOM_PodatociPenzionerOsigurenik.IFPIOMService {
        
        public FPIOMServiceClient() {
        }
        
        public FPIOMServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FPIOMServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FPIOMServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FPIOMServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetDataForRetired(string EMBG) {
            return base.Channel.GetDataForRetired(EMBG);
        }
        
        public System.Threading.Tasks.Task<string> GetDataForRetiredAsync(string EMBG) {
            return base.Channel.GetDataForRetiredAsync(EMBG);
        }
        
        public string GetDataForEnsurees(string EMBG) {
            return base.Channel.GetDataForEnsurees(EMBG);
        }
        
        public System.Threading.Tasks.Task<string> GetDataForEnsureesAsync(string EMBG) {
            return base.Channel.GetDataForEnsureesAsync(EMBG);
        }
    }
}
