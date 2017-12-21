﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAKNDocServices.AdapterIFDocTEST {
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AdapterIFDocTEST.IAKNDataForIFDoc")]
    public interface IAKNDataForIFDoc {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNDataForIFDoc/GetDataForIFDoc", ReplyAction="http://interop.org/IAKNDataForIFDoc/GetDataForIFDocResponse")]
        TestAKNDocServices.AdapterIFDocTEST.AKNDocOutput GetDataForIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNDataForIFDoc/GetDataForIFDoc", ReplyAction="http://interop.org/IAKNDataForIFDoc/GetDataForIFDocResponse")]
        System.Threading.Tasks.Task<TestAKNDocServices.AdapterIFDocTEST.AKNDocOutput> GetDataForIFDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNDataForIFDocChannel : TestAKNDocServices.AdapterIFDocTEST.IAKNDataForIFDoc, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNDataForIFDocClient : System.ServiceModel.ClientBase<TestAKNDocServices.AdapterIFDocTEST.IAKNDataForIFDoc>, TestAKNDocServices.AdapterIFDocTEST.IAKNDataForIFDoc {
        
        public AKNDataForIFDocClient() {
        }
        
        public AKNDataForIFDocClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AKNDataForIFDocClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNDataForIFDocClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNDataForIFDocClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TestAKNDocServices.AdapterIFDocTEST.AKNDocOutput GetDataForIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB) {
            return base.Channel.GetDataForIFDoc(opstina, katastarskaOpstina, brImotenList, brParcela, ShowEMB);
        }
        
        public System.Threading.Tasks.Task<TestAKNDocServices.AdapterIFDocTEST.AKNDocOutput> GetDataForIFDocAsync(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB) {
            return base.Channel.GetDataForIFDocAsync(opstina, katastarskaOpstina, brImotenList, brParcela, ShowEMB);
        }
    }
}
