﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interop.CC.Portal.API.InstitutionBNameSurname {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="InstitutionBNameSurname.INameSurname")]
    public interface INameSurname {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/INameSurname/GetNameSurname", ReplyAction="http://interop.org/INameSurname/GetNameSurnameResponse")]
        string GetNameSurname(string name, string surname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/INameSurname/GetNameSurname", ReplyAction="http://interop.org/INameSurname/GetNameSurnameResponse")]
        System.Threading.Tasks.Task<string> GetNameSurnameAsync(string name, string surname);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INameSurnameChannel : Interop.CC.Portal.API.InstitutionBNameSurname.INameSurname, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NameSurnameClient : System.ServiceModel.ClientBase<Interop.CC.Portal.API.InstitutionBNameSurname.INameSurname>, Interop.CC.Portal.API.InstitutionBNameSurname.INameSurname {
        
        public NameSurnameClient() {
        }
        
        public NameSurnameClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NameSurnameClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NameSurnameClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NameSurnameClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetNameSurname(string name, string surname) {
            return base.Channel.GetNameSurname(name, surname);
        }
        
        public System.Threading.Tasks.Task<string> GetNameSurnameAsync(string name, string surname) {
            return base.Channel.GetNameSurnameAsync(name, surname);
        }
    }
}
