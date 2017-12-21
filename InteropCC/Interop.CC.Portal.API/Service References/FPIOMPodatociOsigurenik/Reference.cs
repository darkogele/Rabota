﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interop.CC.Portal.API.FPIOMPodatociOsigurenik {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropFault", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DataAccessLibrary")]
    [System.SerializableAttribute()]
    public partial class InteropFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorDetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ResultField;
        
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
        public string ErrorDetails {
            get {
                return this.ErrorDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorDetailsField, value) != true)) {
                    this.ErrorDetailsField = value;
                    this.RaisePropertyChanged("ErrorDetails");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="FPIOMPodatociOsigurenik.IDataForEnsurers")]
    public interface IDataForEnsurers {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEnsurers/GetDataForEnsurees", ReplyAction="http://interop.org/IDataForEnsurers/GetDataForEnsureesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Interop.CC.Portal.API.FPIOMPodatociOsigurenik.InteropFault), Action="http://interop.org/IDataForEnsurers/GetDataForEnsureesInteropFaultFault", Name="InteropFault", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DataAccessLibrary")]
        string GetDataForEnsurees(string embg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEnsurers/GetDataForEnsurees", ReplyAction="http://interop.org/IDataForEnsurers/GetDataForEnsureesResponse")]
        System.Threading.Tasks.Task<string> GetDataForEnsureesAsync(string embg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataForEnsurersChannel : Interop.CC.Portal.API.FPIOMPodatociOsigurenik.IDataForEnsurers, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataForEnsurersClient : System.ServiceModel.ClientBase<Interop.CC.Portal.API.FPIOMPodatociOsigurenik.IDataForEnsurers>, Interop.CC.Portal.API.FPIOMPodatociOsigurenik.IDataForEnsurers {
        
        public DataForEnsurersClient() {
        }
        
        public DataForEnsurersClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataForEnsurersClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataForEnsurersClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataForEnsurersClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetDataForEnsurees(string embg) {
            return base.Channel.GetDataForEnsurees(embg);
        }
        
        public System.Threading.Tasks.Task<string> GetDataForEnsureesAsync(string embg) {
            return base.Channel.GetDataForEnsureesAsync(embg);
        }
    }
}
