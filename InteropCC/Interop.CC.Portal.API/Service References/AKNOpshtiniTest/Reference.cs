﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interop.CC.Portal.API.AKNOpshtiniTest {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MunicipalityDTO", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DTO_s.AKNMunicipalityService")]
    [System.SerializableAttribute()]
    public partial class MunicipalityDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AKNOpshtiniTest.IAKNMunicipality")]
    public interface IAKNMunicipality {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetMunicipalitiesResponse")]
        Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[] GetMunicipalities();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetMunicipalitiesResponse")]
        System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[]> GetMunicipalitiesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetCMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetCMunicipalitiesResponse")]
        Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[] GetCMunicipalities(string municipalityValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNMunicipality/GetCMunicipalities", ReplyAction="http://interop.org/IAKNMunicipality/GetCMunicipalitiesResponse")]
        System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[]> GetCMunicipalitiesAsync(string municipalityValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNMunicipalityChannel : Interop.CC.Portal.API.AKNOpshtiniTest.IAKNMunicipality, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNMunicipalityClient : System.ServiceModel.ClientBase<Interop.CC.Portal.API.AKNOpshtiniTest.IAKNMunicipality>, Interop.CC.Portal.API.AKNOpshtiniTest.IAKNMunicipality {
        
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
        
        public Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[] GetMunicipalities() {
            return base.Channel.GetMunicipalities();
        }
        
        public System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[]> GetMunicipalitiesAsync() {
            return base.Channel.GetMunicipalitiesAsync();
        }
        
        public Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[] GetCMunicipalities(string municipalityValue) {
            return base.Channel.GetCMunicipalities(municipalityValue);
        }
        
        public System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNOpshtiniTest.MunicipalityDTO[]> GetCMunicipalitiesAsync(string municipalityValue) {
            return base.Channel.GetCMunicipalitiesAsync(municipalityValue);
        }
    }
}
