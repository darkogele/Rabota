﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAKNDocServices.PListDocPROD {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AKNDocOutput", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="PListDocPROD.IAKNPListDocProduction")]
    public interface IAKNPListDocProduction {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNPListDocProduction/GetPListDoc", ReplyAction="http://interop.org/IAKNPListDocProduction/GetPListDocResponse")]
        TestAKNDocServices.PListDocPROD.AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNPListDocProduction/GetPListDoc", ReplyAction="http://interop.org/IAKNPListDocProduction/GetPListDocResponse")]
        System.Threading.Tasks.Task<TestAKNDocServices.PListDocPROD.AKNDocOutput> GetPListDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNPListDocProductionChannel : TestAKNDocServices.PListDocPROD.IAKNPListDocProduction, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNPListDocProductionClient : System.ServiceModel.ClientBase<TestAKNDocServices.PListDocPROD.IAKNPListDocProduction>, TestAKNDocServices.PListDocPROD.IAKNPListDocProduction {
        
        public AKNPListDocProductionClient() {
        }
        
        public AKNPListDocProductionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AKNPListDocProductionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNPListDocProductionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNPListDocProductionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TestAKNDocServices.PListDocPROD.AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB) {
            return base.Channel.GetPListDoc(opstina, katastarskaOpstina, brImotenList, brParcela, ShowEMB);
        }
        
        public System.Threading.Tasks.Task<TestAKNDocServices.PListDocPROD.AKNDocOutput> GetPListDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB) {
            return base.Channel.GetPListDocAsync(opstina, katastarskaOpstina, brImotenList, brParcela, ShowEMB);
        }
    }
}
