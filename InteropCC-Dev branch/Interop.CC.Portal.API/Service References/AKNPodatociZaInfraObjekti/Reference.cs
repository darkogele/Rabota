﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interop.CC.Portal.API.AKNPodatociZaInfraObjekti {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AKNDocOutput", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DataAccessLibrary")]
    [System.SerializableAttribute()]
    public partial class AKNDocOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] DocumentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasDocumentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
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
        public byte[] Document {
            get {
                return this.DocumentField;
            }
            set {
                if ((object.ReferenceEquals(this.DocumentField, value) != true)) {
                    this.DocumentField = value;
                    this.RaisePropertyChanged("Document");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasDocument {
            get {
                return this.HasDocumentField;
            }
            set {
                if ((this.HasDocumentField.Equals(value) != true)) {
                    this.HasDocumentField = value;
                    this.RaisePropertyChanged("HasDocument");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AKNPodatociZaInfraObjekti.IAKNDataForIFDocProduction")]
    public interface IAKNDataForIFDocProduction {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNDataForIFDocProduction/GetIFDoc", ReplyAction="http://interop.org/IAKNDataForIFDocProduction/GetIFDocResponse")]
        Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.AKNDocOutput GetIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNDataForIFDocProduction/GetIFDoc", ReplyAction="http://interop.org/IAKNDataForIFDocProduction/GetIFDocResponse")]
        System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.AKNDocOutput> GetIFDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNDataForIFDocProductionChannel : Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.IAKNDataForIFDocProduction, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNDataForIFDocProductionClient : System.ServiceModel.ClientBase<Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.IAKNDataForIFDocProduction>, Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.IAKNDataForIFDocProduction {
        
        public AKNDataForIFDocProductionClient() {
        }
        
        public AKNDataForIFDocProductionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AKNDataForIFDocProductionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNDataForIFDocProductionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNDataForIFDocProductionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.AKNDocOutput GetIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb) {
            return base.Channel.GetIFDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEmb);
        }
        
        public System.Threading.Tasks.Task<Interop.CC.Portal.API.AKNPodatociZaInfraObjekti.AKNDocOutput> GetIFDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb) {
            return base.Channel.GetIFDocAsync(opstina, katastarskaOpstina, brImotenList, brParcela, showEmb);
        }
    }
}