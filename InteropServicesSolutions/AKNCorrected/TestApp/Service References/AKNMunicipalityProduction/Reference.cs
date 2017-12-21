﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.AKNMunicipalityProduction {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AKNMunicipalityProduction.IAKNMunicipality")]
    public interface IAKNMunicipality {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetMunicipalitiesResponse")]
        Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[] GetMunicipalities();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetMunicipalitiesResponse")]
        System.Threading.Tasks.Task<Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[]> GetMunicipalitiesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetCMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetCMunicipalitiesResponse")]
        Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[] GetCMunicipalities(string municipalityValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetCMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetCMunicipalitiesResponse")]
        System.Threading.Tasks.Task<Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[]> GetCMunicipalitiesAsync(string municipalityValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNMunicipalityChannel : TestApp.AKNMunicipalityProduction.IAKNMunicipality, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNMunicipalityClient : System.ServiceModel.ClientBase<TestApp.AKNMunicipalityProduction.IAKNMunicipality>, TestApp.AKNMunicipalityProduction.IAKNMunicipality {
        
        public AKNMunicipalityClient() {
        }
        
        public AKNMunicipalityClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AKNMunicipalityClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNMunicipalityClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNMunicipalityClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[] GetMunicipalities() {
            return base.Channel.GetMunicipalities();
        }
        
        public System.Threading.Tasks.Task<Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[]> GetMunicipalitiesAsync() {
            return base.Channel.GetMunicipalitiesAsync();
        }
        
        public Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[] GetCMunicipalities(string municipalityValue) {
            return base.Channel.GetCMunicipalities(municipalityValue);
        }
        
        public System.Threading.Tasks.Task<Contracts.DTO_s.AKNMunicipalityService.MunicipalityDTO[]> GetCMunicipalitiesAsync(string municipalityValue) {
            return base.Channel.GetCMunicipalitiesAsync(municipalityValue);
        }
    }
}
