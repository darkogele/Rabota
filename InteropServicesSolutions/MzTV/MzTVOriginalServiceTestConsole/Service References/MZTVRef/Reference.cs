﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MzTVOriginalServiceTestConsole.MZTVRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropInputViewModel", Namespace="https://www.gradezna-dozvola.mk/Services/")]
    [System.SerializableAttribute()]
    public partial class InteropInputViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArchiveNumberField;
        
        private System.DateTime SendDateField;
        
        private int ConstructionTypeIdField;
        
        private int MunicipalityIdField;
        
        private bool GetDocumentsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string ArchiveNumber {
            get {
                return this.ArchiveNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ArchiveNumberField, value) != true)) {
                    this.ArchiveNumberField = value;
                    this.RaisePropertyChanged("ArchiveNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime SendDate {
            get {
                return this.SendDateField;
            }
            set {
                if ((this.SendDateField.Equals(value) != true)) {
                    this.SendDateField = value;
                    this.RaisePropertyChanged("SendDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int ConstructionTypeId {
            get {
                return this.ConstructionTypeIdField;
            }
            set {
                if ((this.ConstructionTypeIdField.Equals(value) != true)) {
                    this.ConstructionTypeIdField = value;
                    this.RaisePropertyChanged("ConstructionTypeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int MunicipalityId {
            get {
                return this.MunicipalityIdField;
            }
            set {
                if ((this.MunicipalityIdField.Equals(value) != true)) {
                    this.MunicipalityIdField = value;
                    this.RaisePropertyChanged("MunicipalityId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public bool GetDocuments {
            get {
                return this.GetDocumentsField;
            }
            set {
                if ((this.GetDocumentsField.Equals(value) != true)) {
                    this.GetDocumentsField = value;
                    this.RaisePropertyChanged("GetDocuments");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropOutputViewModel", Namespace="https://www.gradezna-dozvola.mk/Services/")]
    [System.SerializableAttribute()]
    public partial class InteropOutputViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MzTVOriginalServiceTestConsole.MZTVRef.ArrayOfString InvestorsField;
        
        private System.DateTime SendDateField;
        
        private System.DateTime ArchiveDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConstructionDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConstructionTypeNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConstructionAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MzTVOriginalServiceTestConsole.MZTVRef.InteropRequestMunicipalityViewModel[] MunicipalitiesField;
        
        private System.Nullable<System.DateTime> EffectDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MzTVOriginalServiceTestConsole.MZTVRef.InteropDocumentsViewModel[] DocumentsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public MzTVOriginalServiceTestConsole.MZTVRef.ArrayOfString Investors {
            get {
                return this.InvestorsField;
            }
            set {
                if ((object.ReferenceEquals(this.InvestorsField, value) != true)) {
                    this.InvestorsField = value;
                    this.RaisePropertyChanged("Investors");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public System.DateTime SendDate {
            get {
                return this.SendDateField;
            }
            set {
                if ((this.SendDateField.Equals(value) != true)) {
                    this.SendDateField = value;
                    this.RaisePropertyChanged("SendDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public System.DateTime ArchiveDate {
            get {
                return this.ArchiveDateField;
            }
            set {
                if ((this.ArchiveDateField.Equals(value) != true)) {
                    this.ArchiveDateField = value;
                    this.RaisePropertyChanged("ArchiveDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string ConstructionDescription {
            get {
                return this.ConstructionDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.ConstructionDescriptionField, value) != true)) {
                    this.ConstructionDescriptionField = value;
                    this.RaisePropertyChanged("ConstructionDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string ConstructionTypeName {
            get {
                return this.ConstructionTypeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ConstructionTypeNameField, value) != true)) {
                    this.ConstructionTypeNameField = value;
                    this.RaisePropertyChanged("ConstructionTypeName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string ConstructionAddress {
            get {
                return this.ConstructionAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ConstructionAddressField, value) != true)) {
                    this.ConstructionAddressField = value;
                    this.RaisePropertyChanged("ConstructionAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropRequestMunicipalityViewModel[] Municipalities {
            get {
                return this.MunicipalitiesField;
            }
            set {
                if ((object.ReferenceEquals(this.MunicipalitiesField, value) != true)) {
                    this.MunicipalitiesField = value;
                    this.RaisePropertyChanged("Municipalities");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=8)]
        public System.Nullable<System.DateTime> EffectDate {
            get {
                return this.EffectDateField;
            }
            set {
                if ((this.EffectDateField.Equals(value) != true)) {
                    this.EffectDateField = value;
                    this.RaisePropertyChanged("EffectDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropDocumentsViewModel[] Documents {
            get {
                return this.DocumentsField;
            }
            set {
                if ((object.ReferenceEquals(this.DocumentsField, value) != true)) {
                    this.DocumentsField = value;
                    this.RaisePropertyChanged("Documents");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="https://www.gradezna-dozvola.mk/Services/", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropRequestMunicipalityViewModel", Namespace="https://www.gradezna-dozvola.mk/Services/")]
    [System.SerializableAttribute()]
    public partial class InteropRequestMunicipalityViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MunicipalityNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MzTVOriginalServiceTestConsole.MZTVRef.InteropCadastreMunicipalityViewModel[] CadastreMunicipalitiesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string MunicipalityName {
            get {
                return this.MunicipalityNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MunicipalityNameField, value) != true)) {
                    this.MunicipalityNameField = value;
                    this.RaisePropertyChanged("MunicipalityName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropCadastreMunicipalityViewModel[] CadastreMunicipalities {
            get {
                return this.CadastreMunicipalitiesField;
            }
            set {
                if ((object.ReferenceEquals(this.CadastreMunicipalitiesField, value) != true)) {
                    this.CadastreMunicipalitiesField = value;
                    this.RaisePropertyChanged("CadastreMunicipalities");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropDocumentsViewModel", Namespace="https://www.gradezna-dozvola.mk/Services/")]
    [System.SerializableAttribute()]
    public partial class InteropDocumentsViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ContentBytesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public byte[] ContentBytes {
            get {
                return this.ContentBytesField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentBytesField, value) != true)) {
                    this.ContentBytesField = value;
                    this.RaisePropertyChanged("ContentBytes");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropCadastreMunicipalityViewModel", Namespace="https://www.gradezna-dozvola.mk/Services/")]
    [System.SerializableAttribute()]
    public partial class InteropCadastreMunicipalityViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KpField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Ko {
            get {
                return this.KoField;
            }
            set {
                if ((object.ReferenceEquals(this.KoField, value) != true)) {
                    this.KoField = value;
                    this.RaisePropertyChanged("Ko");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Kp {
            get {
                return this.KpField;
            }
            set {
                if ((object.ReferenceEquals(this.KpField, value) != true)) {
                    this.KpField = value;
                    this.RaisePropertyChanged("Kp");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.gradezna-dozvola.mk/Services/", ConfigurationName="MZTVRef.InteropWebServiceSoap")]
    public interface InteropWebServiceSoap {
        
        // CODEGEN: Generating message contract since element name parameters from namespace https://www.gradezna-dozvola.mk/Services/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="https://www.gradezna-dozvola.mk/Services/GetRequestDetails", ReplyAction="*")]
        MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse GetRequestDetails(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.gradezna-dozvola.mk/Services/GetRequestDetails", ReplyAction="*")]
        System.Threading.Tasks.Task<MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse> GetRequestDetailsAsync(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRequestDetailsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRequestDetails", Namespace="https://www.gradezna-dozvola.mk/Services/", Order=0)]
        public MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequestBody Body;
        
        public GetRequestDetailsRequest() {
        }
        
        public GetRequestDetailsRequest(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.gradezna-dozvola.mk/Services/")]
    public partial class GetRequestDetailsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropInputViewModel parameters;
        
        public GetRequestDetailsRequestBody() {
        }
        
        public GetRequestDetailsRequestBody(MzTVOriginalServiceTestConsole.MZTVRef.InteropInputViewModel parameters) {
            this.parameters = parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRequestDetailsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRequestDetailsResponse", Namespace="https://www.gradezna-dozvola.mk/Services/", Order=0)]
        public MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponseBody Body;
        
        public GetRequestDetailsResponse() {
        }
        
        public GetRequestDetailsResponse(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.gradezna-dozvola.mk/Services/")]
    public partial class GetRequestDetailsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropOutputViewModel GetRequestDetailsResult;
        
        public GetRequestDetailsResponseBody() {
        }
        
        public GetRequestDetailsResponseBody(MzTVOriginalServiceTestConsole.MZTVRef.InteropOutputViewModel GetRequestDetailsResult) {
            this.GetRequestDetailsResult = GetRequestDetailsResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface InteropWebServiceSoapChannel : MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InteropWebServiceSoapClient : System.ServiceModel.ClientBase<MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap>, MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap {
        
        public InteropWebServiceSoapClient() {
        }
        
        public InteropWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InteropWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InteropWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InteropWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap.GetRequestDetails(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest request) {
            return base.Channel.GetRequestDetails(request);
        }
        
        public MzTVOriginalServiceTestConsole.MZTVRef.InteropOutputViewModel GetRequestDetails(MzTVOriginalServiceTestConsole.MZTVRef.InteropInputViewModel parameters) {
            MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest inValue = new MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest();
            inValue.Body = new MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequestBody();
            inValue.Body.parameters = parameters;
            MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse retVal = ((MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap)(this)).GetRequestDetails(inValue);
            return retVal.Body.GetRequestDetailsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse> MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap.GetRequestDetailsAsync(MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest request) {
            return base.Channel.GetRequestDetailsAsync(request);
        }
        
        public System.Threading.Tasks.Task<MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsResponse> GetRequestDetailsAsync(MzTVOriginalServiceTestConsole.MZTVRef.InteropInputViewModel parameters) {
            MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest inValue = new MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequest();
            inValue.Body = new MzTVOriginalServiceTestConsole.MZTVRef.GetRequestDetailsRequestBody();
            inValue.Body.parameters = parameters;
            return ((MzTVOriginalServiceTestConsole.MZTVRef.InteropWebServiceSoap)(this)).GetRequestDetailsAsync(inValue);
        }
    }
}
