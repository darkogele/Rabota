﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EDB_EMB_Output", Namespace="http://schemas.datacontract.org/2004/07/Contracts.Models.DataFor_EDB_EMB")]
    [System.SerializableAttribute()]
    public partial class EDB_EMB_Output : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BankaZiroField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DatumPrijavaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DejnostNaceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EdbField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmbField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NazivField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrijavaStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrijavaVidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SedisteBrojField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SedisteNazivField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SedisteTelefaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SedisteTelefonField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SedisteUlicaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ZiroField;
        
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
        public string BankaZiro {
            get {
                return this.BankaZiroField;
            }
            set {
                if ((object.ReferenceEquals(this.BankaZiroField, value) != true)) {
                    this.BankaZiroField = value;
                    this.RaisePropertyChanged("BankaZiro");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DatumPrijava {
            get {
                return this.DatumPrijavaField;
            }
            set {
                if ((object.ReferenceEquals(this.DatumPrijavaField, value) != true)) {
                    this.DatumPrijavaField = value;
                    this.RaisePropertyChanged("DatumPrijava");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DejnostNace {
            get {
                return this.DejnostNaceField;
            }
            set {
                if ((object.ReferenceEquals(this.DejnostNaceField, value) != true)) {
                    this.DejnostNaceField = value;
                    this.RaisePropertyChanged("DejnostNace");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Edb {
            get {
                return this.EdbField;
            }
            set {
                if ((object.ReferenceEquals(this.EdbField, value) != true)) {
                    this.EdbField = value;
                    this.RaisePropertyChanged("Edb");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Emb {
            get {
                return this.EmbField;
            }
            set {
                if ((object.ReferenceEquals(this.EmbField, value) != true)) {
                    this.EmbField = value;
                    this.RaisePropertyChanged("Emb");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Naziv {
            get {
                return this.NazivField;
            }
            set {
                if ((object.ReferenceEquals(this.NazivField, value) != true)) {
                    this.NazivField = value;
                    this.RaisePropertyChanged("Naziv");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrijavaStatus {
            get {
                return this.PrijavaStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.PrijavaStatusField, value) != true)) {
                    this.PrijavaStatusField = value;
                    this.RaisePropertyChanged("PrijavaStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrijavaVid {
            get {
                return this.PrijavaVidField;
            }
            set {
                if ((object.ReferenceEquals(this.PrijavaVidField, value) != true)) {
                    this.PrijavaVidField = value;
                    this.RaisePropertyChanged("PrijavaVid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SedisteBroj {
            get {
                return this.SedisteBrojField;
            }
            set {
                if ((object.ReferenceEquals(this.SedisteBrojField, value) != true)) {
                    this.SedisteBrojField = value;
                    this.RaisePropertyChanged("SedisteBroj");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SedisteNaziv {
            get {
                return this.SedisteNazivField;
            }
            set {
                if ((object.ReferenceEquals(this.SedisteNazivField, value) != true)) {
                    this.SedisteNazivField = value;
                    this.RaisePropertyChanged("SedisteNaziv");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SedisteTelefax {
            get {
                return this.SedisteTelefaxField;
            }
            set {
                if ((object.ReferenceEquals(this.SedisteTelefaxField, value) != true)) {
                    this.SedisteTelefaxField = value;
                    this.RaisePropertyChanged("SedisteTelefax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SedisteTelefon {
            get {
                return this.SedisteTelefonField;
            }
            set {
                if ((object.ReferenceEquals(this.SedisteTelefonField, value) != true)) {
                    this.SedisteTelefonField = value;
                    this.RaisePropertyChanged("SedisteTelefon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SedisteUlica {
            get {
                return this.SedisteUlicaField;
            }
            set {
                if ((object.ReferenceEquals(this.SedisteUlicaField, value) != true)) {
                    this.SedisteUlicaField = value;
                    this.RaisePropertyChanged("SedisteUlica");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ziro {
            get {
                return this.ZiroField;
            }
            set {
                if ((object.ReferenceEquals(this.ZiroField, value) != true)) {
                    this.ZiroField = value;
                    this.RaisePropertyChanged("Ziro");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="InteropFault", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DataAccessLibrary.InteropFault")]
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org", ConfigurationName="UJPEdinstvenDanocenBrojTest.IDataForEDB")]
    public interface IDataForEDB {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEDB/GetEDB", ReplyAction="http://interop.org/IDataForEDB/GetEDBResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.InteropFault), Action="http://interop.org/IDataForEDB/GetEDBInteropFaultFault", Name="InteropFault", Namespace="http://schemas.datacontract.org/2004/07/Contracts.DataAccessLibrary.InteropFault")]
        Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.EDB_EMB_Output[] GetEDB(string emb);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEDB/GetEDB", ReplyAction="http://interop.org/IDataForEDB/GetEDBResponse")]
        System.Threading.Tasks.Task<Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.EDB_EMB_Output[]> GetEDBAsync(string emb);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataForEDBChannel : Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.IDataForEDB, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataForEDBClient : System.ServiceModel.ClientBase<Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.IDataForEDB>, Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.IDataForEDB {
        
        public DataForEDBClient() {
        }
        
        public DataForEDBClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataForEDBClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataForEDBClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataForEDBClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.EDB_EMB_Output[] GetEDB(string emb) {
            return base.Channel.GetEDB(emb);
        }
        
        public System.Threading.Tasks.Task<Interop.CC.Portal.API.UJPEdinstvenDanocenBrojTest.EDB_EMB_Output[]> GetEDBAsync(string emb) {
            return base.Channel.GetEDBAsync(emb);
        }
    }
}
