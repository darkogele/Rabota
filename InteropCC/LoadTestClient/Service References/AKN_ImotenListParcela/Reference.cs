﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadTestClient.AKN_ImotenListParcela {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="dzgr", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class dzgr : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string ilistFieldField;
        
        private string kopsFieldField;
        
        private string messageFieldField;
        
        private LoadTestClient.AKN_ImotenListParcela.objekti[] nizobjFieldField;
        
        private LoadTestClient.AKN_ImotenListParcela.parceli[] nizparFieldField;
        
        private LoadTestClient.AKN_ImotenListParcela.sopstvenici[] nizsopFieldField;
        
        private LoadTestClient.AKN_ImotenListParcela.tovari[] niztovFieldField;
        
        private string opsFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ilistField {
            get {
                return this.ilistFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.ilistFieldField, value) != true)) {
                    this.ilistFieldField = value;
                    this.RaisePropertyChanged("ilistField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string kopsField {
            get {
                return this.kopsFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.kopsFieldField, value) != true)) {
                    this.kopsFieldField = value;
                    this.RaisePropertyChanged("kopsField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string messageField {
            get {
                return this.messageFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.messageFieldField, value) != true)) {
                    this.messageFieldField = value;
                    this.RaisePropertyChanged("messageField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public LoadTestClient.AKN_ImotenListParcela.objekti[] nizobjField {
            get {
                return this.nizobjFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.nizobjFieldField, value) != true)) {
                    this.nizobjFieldField = value;
                    this.RaisePropertyChanged("nizobjField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public LoadTestClient.AKN_ImotenListParcela.parceli[] nizparField {
            get {
                return this.nizparFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.nizparFieldField, value) != true)) {
                    this.nizparFieldField = value;
                    this.RaisePropertyChanged("nizparField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public LoadTestClient.AKN_ImotenListParcela.sopstvenici[] nizsopField {
            get {
                return this.nizsopFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.nizsopFieldField, value) != true)) {
                    this.nizsopFieldField = value;
                    this.RaisePropertyChanged("nizsopField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public LoadTestClient.AKN_ImotenListParcela.tovari[] niztovField {
            get {
                return this.niztovFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.niztovFieldField, value) != true)) {
                    this.niztovFieldField = value;
                    this.RaisePropertyChanged("niztovField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string opsField {
            get {
                return this.opsFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.opsFieldField, value) != true)) {
                    this.opsFieldField = value;
                    this.RaisePropertyChanged("opsField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="objekti", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class objekti : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string brojFieldField;
        
        private string katFieldField;
        
        private string mestoFieldField;
        
        private string namenaFieldField;
        
        private int objektFieldField;
        
        private long povrsinaFieldField;
        
        private string pravoFieldField;
        
        private string stanFieldField;
        
        private string vlezFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string brojField {
            get {
                return this.brojFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.brojFieldField, value) != true)) {
                    this.brojFieldField = value;
                    this.RaisePropertyChanged("brojField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string katField {
            get {
                return this.katFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.katFieldField, value) != true)) {
                    this.katFieldField = value;
                    this.RaisePropertyChanged("katField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string mestoField {
            get {
                return this.mestoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoFieldField, value) != true)) {
                    this.mestoFieldField = value;
                    this.RaisePropertyChanged("mestoField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string namenaField {
            get {
                return this.namenaFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.namenaFieldField, value) != true)) {
                    this.namenaFieldField = value;
                    this.RaisePropertyChanged("namenaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int objektField {
            get {
                return this.objektFieldField;
            }
            set {
                if ((this.objektFieldField.Equals(value) != true)) {
                    this.objektFieldField = value;
                    this.RaisePropertyChanged("objektField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long povrsinaField {
            get {
                return this.povrsinaFieldField;
            }
            set {
                if ((this.povrsinaFieldField.Equals(value) != true)) {
                    this.povrsinaFieldField = value;
                    this.RaisePropertyChanged("povrsinaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string pravoField {
            get {
                return this.pravoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoFieldField, value) != true)) {
                    this.pravoFieldField = value;
                    this.RaisePropertyChanged("pravoField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string stanField {
            get {
                return this.stanFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.stanFieldField, value) != true)) {
                    this.stanFieldField = value;
                    this.RaisePropertyChanged("stanField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string vlezField {
            get {
                return this.vlezFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.vlezFieldField, value) != true)) {
                    this.vlezFieldField = value;
                    this.RaisePropertyChanged("vlezField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="parceli", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class parceli : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string broj_delFieldField;
        
        private string kulturaFieldField;
        
        private string mestoFieldField;
        
        private int objektFieldField;
        
        private long povrsinaFieldField;
        
        private string pravoFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string broj_delField {
            get {
                return this.broj_delFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.broj_delFieldField, value) != true)) {
                    this.broj_delFieldField = value;
                    this.RaisePropertyChanged("broj_delField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string kulturaField {
            get {
                return this.kulturaFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.kulturaFieldField, value) != true)) {
                    this.kulturaFieldField = value;
                    this.RaisePropertyChanged("kulturaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string mestoField {
            get {
                return this.mestoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoFieldField, value) != true)) {
                    this.mestoFieldField = value;
                    this.RaisePropertyChanged("mestoField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int objektField {
            get {
                return this.objektFieldField;
            }
            set {
                if ((this.objektFieldField.Equals(value) != true)) {
                    this.objektFieldField = value;
                    this.RaisePropertyChanged("objektField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long povrsinaField {
            get {
                return this.povrsinaFieldField;
            }
            set {
                if ((this.povrsinaFieldField.Equals(value) != true)) {
                    this.povrsinaFieldField = value;
                    this.RaisePropertyChanged("povrsinaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string pravoField {
            get {
                return this.pravoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoFieldField, value) != true)) {
                    this.pravoFieldField = value;
                    this.RaisePropertyChanged("pravoField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="sopstvenici", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class sopstvenici : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string brojFieldField;
        
        private string delFieldField;
        
        private string imeFieldField;
        
        private string mestoFieldField;
        
        private string ulicaFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string brojField {
            get {
                return this.brojFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.brojFieldField, value) != true)) {
                    this.brojFieldField = value;
                    this.RaisePropertyChanged("brojField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string delField {
            get {
                return this.delFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.delFieldField, value) != true)) {
                    this.delFieldField = value;
                    this.RaisePropertyChanged("delField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string imeField {
            get {
                return this.imeFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.imeFieldField, value) != true)) {
                    this.imeFieldField = value;
                    this.RaisePropertyChanged("imeField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string mestoField {
            get {
                return this.mestoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoFieldField, value) != true)) {
                    this.mestoFieldField = value;
                    this.RaisePropertyChanged("mestoField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ulicaField {
            get {
                return this.ulicaFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.ulicaFieldField, value) != true)) {
                    this.ulicaFieldField = value;
                    this.RaisePropertyChanged("ulicaField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="tovari", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class tovari : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string textFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string textField {
            get {
                return this.textFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.textFieldField, value) != true)) {
                    this.textFieldField = value;
                    this.RaisePropertyChanged("textField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ATRparceli", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class ATRparceli : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string messageFieldField;
        
        private LoadTestClient.AKN_ImotenListParcela.atributiparcela[] nizparFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string messageField {
            get {
                return this.messageFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.messageFieldField, value) != true)) {
                    this.messageFieldField = value;
                    this.RaisePropertyChanged("messageField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public LoadTestClient.AKN_ImotenListParcela.atributiparcela[] nizparField {
            get {
                return this.nizparFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.nizparFieldField, value) != true)) {
                    this.nizparFieldField = value;
                    this.RaisePropertyChanged("nizparField");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="atributiparcela", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN.AKNOriginalService")]
    [System.SerializableAttribute()]
    public partial class atributiparcela : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged1Field;
        
        private string broj_delFieldField;
        
        private string ilistFieldField;
        
        private string kopsFieldField;
        
        private string kulturaFieldField;
        
        private string mestoFieldField;
        
        private int objektFieldField;
        
        private string opsFieldField;
        
        private long povrsinaFieldField;
        
        private string pravoFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="PropertyChanged", IsRequired=true)]
        public System.ComponentModel.PropertyChangedEventHandler PropertyChanged1 {
            get {
                return this.PropertyChanged1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PropertyChanged1Field, value) != true)) {
                    this.PropertyChanged1Field = value;
                    this.RaisePropertyChanged("PropertyChanged1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string broj_delField {
            get {
                return this.broj_delFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.broj_delFieldField, value) != true)) {
                    this.broj_delFieldField = value;
                    this.RaisePropertyChanged("broj_delField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ilistField {
            get {
                return this.ilistFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.ilistFieldField, value) != true)) {
                    this.ilistFieldField = value;
                    this.RaisePropertyChanged("ilistField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string kopsField {
            get {
                return this.kopsFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.kopsFieldField, value) != true)) {
                    this.kopsFieldField = value;
                    this.RaisePropertyChanged("kopsField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string kulturaField {
            get {
                return this.kulturaFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.kulturaFieldField, value) != true)) {
                    this.kulturaFieldField = value;
                    this.RaisePropertyChanged("kulturaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string mestoField {
            get {
                return this.mestoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoFieldField, value) != true)) {
                    this.mestoFieldField = value;
                    this.RaisePropertyChanged("mestoField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int objektField {
            get {
                return this.objektFieldField;
            }
            set {
                if ((this.objektFieldField.Equals(value) != true)) {
                    this.objektFieldField = value;
                    this.RaisePropertyChanged("objektField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string opsField {
            get {
                return this.opsFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.opsFieldField, value) != true)) {
                    this.opsFieldField = value;
                    this.RaisePropertyChanged("opsField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long povrsinaField {
            get {
                return this.povrsinaFieldField;
            }
            set {
                if ((this.povrsinaFieldField.Equals(value) != true)) {
                    this.povrsinaFieldField = value;
                    this.RaisePropertyChanged("povrsinaField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string pravoField {
            get {
                return this.pravoFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoFieldField, value) != true)) {
                    this.pravoFieldField = value;
                    this.RaisePropertyChanged("pravoField");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AKN_ImotenListParcela.IAKNService")]
    public interface IAKNService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetPropertyList", ReplyAction="http://interop.org/IAKNService/GetPropertyListResponse")]
        LoadTestClient.AKN_ImotenListParcela.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetPropertyList", ReplyAction="http://interop.org/IAKNService/GetPropertyListResponse")]
        System.Threading.Tasks.Task<LoadTestClient.AKN_ImotenListParcela.dzgr> GetPropertyListAsync(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetCadastrialParcel", ReplyAction="http://interop.org/IAKNService/GetCadastrialParcelResponse")]
        LoadTestClient.AKN_ImotenListParcela.ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetCadastrialParcel", ReplyAction="http://interop.org/IAKNService/GetCadastrialParcelResponse")]
        System.Threading.Tasks.Task<LoadTestClient.AKN_ImotenListParcela.ATRparceli> GetCadastrialParcelAsync(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNServiceChannel : LoadTestClient.AKN_ImotenListParcela.IAKNService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNServiceClient : System.ServiceModel.ClientBase<LoadTestClient.AKN_ImotenListParcela.IAKNService>, LoadTestClient.AKN_ImotenListParcela.IAKNService {
        
        public AKNServiceClient() {
        }
        
        public AKNServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AKNServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AKNServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LoadTestClient.AKN_ImotenListParcela.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList) {
            return base.Channel.GetPropertyList(username, password, opstina, katastarskaOpstina, brImotenList);
        }
        
        public System.Threading.Tasks.Task<LoadTestClient.AKN_ImotenListParcela.dzgr> GetPropertyListAsync(string username, string password, string opstina, string katastarskaOpstina, string brImotenList) {
            return base.Channel.GetPropertyListAsync(username, password, opstina, katastarskaOpstina, brImotenList);
        }
        
        public LoadTestClient.AKN_ImotenListParcela.ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela) {
            return base.Channel.GetCadastrialParcel(username, password, opstina, katastarskaOpstina, brParcela);
        }
        
        public System.Threading.Tasks.Task<LoadTestClient.AKN_ImotenListParcela.ATRparceli> GetCadastrialParcelAsync(string username, string password, string opstina, string katastarskaOpstina, string brParcela) {
            return base.Channel.GetCadastrialParcelAsync(username, password, opstina, katastarskaOpstina, brParcela);
        }
    }
}
