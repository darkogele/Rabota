﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAKNAdapterForParcel.AKNAdapterService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="dzgr", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class dzgr : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string dataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ilistField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string kopsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string messageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestAKNAdapterForParcel.AKNAdapterService.Objects[] nizobjField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestAKNAdapterForParcel.AKNAdapterService.Parcel[] nizparField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestAKNAdapterForParcel.AKNAdapterService.Owner[] nizsopField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestAKNAdapterForParcel.AKNAdapterService.Loads[] niztovField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string opsField;
        
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
        public string data {
            get {
                return this.dataField;
            }
            set {
                if ((object.ReferenceEquals(this.dataField, value) != true)) {
                    this.dataField = value;
                    this.RaisePropertyChanged("data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ilist {
            get {
                return this.ilistField;
            }
            set {
                if ((object.ReferenceEquals(this.ilistField, value) != true)) {
                    this.ilistField = value;
                    this.RaisePropertyChanged("ilist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string kops {
            get {
                return this.kopsField;
            }
            set {
                if ((object.ReferenceEquals(this.kopsField, value) != true)) {
                    this.kopsField = value;
                    this.RaisePropertyChanged("kops");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string message {
            get {
                return this.messageField;
            }
            set {
                if ((object.ReferenceEquals(this.messageField, value) != true)) {
                    this.messageField = value;
                    this.RaisePropertyChanged("message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestAKNAdapterForParcel.AKNAdapterService.Objects[] nizobj {
            get {
                return this.nizobjField;
            }
            set {
                if ((object.ReferenceEquals(this.nizobjField, value) != true)) {
                    this.nizobjField = value;
                    this.RaisePropertyChanged("nizobj");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestAKNAdapterForParcel.AKNAdapterService.Parcel[] nizpar {
            get {
                return this.nizparField;
            }
            set {
                if ((object.ReferenceEquals(this.nizparField, value) != true)) {
                    this.nizparField = value;
                    this.RaisePropertyChanged("nizpar");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestAKNAdapterForParcel.AKNAdapterService.Owner[] nizsop {
            get {
                return this.nizsopField;
            }
            set {
                if ((object.ReferenceEquals(this.nizsopField, value) != true)) {
                    this.nizsopField = value;
                    this.RaisePropertyChanged("nizsop");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestAKNAdapterForParcel.AKNAdapterService.Loads[] niztov {
            get {
                return this.niztovField;
            }
            set {
                if ((object.ReferenceEquals(this.niztovField, value) != true)) {
                    this.niztovField = value;
                    this.RaisePropertyChanged("niztov");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ops {
            get {
                return this.opsField;
            }
            set {
                if ((object.ReferenceEquals(this.opsField, value) != true)) {
                    this.opsField = value;
                    this.RaisePropertyChanged("ops");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Objects", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class Objects : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string brojField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string godinagradbaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string katField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mestoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string namenaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int objektField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string osnovField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long povrsinaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string pravoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string stanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string vlezField;
        
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
        public string broj {
            get {
                return this.brojField;
            }
            set {
                if ((object.ReferenceEquals(this.brojField, value) != true)) {
                    this.brojField = value;
                    this.RaisePropertyChanged("broj");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string godinagradba {
            get {
                return this.godinagradbaField;
            }
            set {
                if ((object.ReferenceEquals(this.godinagradbaField, value) != true)) {
                    this.godinagradbaField = value;
                    this.RaisePropertyChanged("godinagradba");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string kat {
            get {
                return this.katField;
            }
            set {
                if ((object.ReferenceEquals(this.katField, value) != true)) {
                    this.katField = value;
                    this.RaisePropertyChanged("kat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoField, value) != true)) {
                    this.mestoField = value;
                    this.RaisePropertyChanged("mesto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string namena {
            get {
                return this.namenaField;
            }
            set {
                if ((object.ReferenceEquals(this.namenaField, value) != true)) {
                    this.namenaField = value;
                    this.RaisePropertyChanged("namena");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                if ((this.objektField.Equals(value) != true)) {
                    this.objektField = value;
                    this.RaisePropertyChanged("objekt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string osnov {
            get {
                return this.osnovField;
            }
            set {
                if ((object.ReferenceEquals(this.osnovField, value) != true)) {
                    this.osnovField = value;
                    this.RaisePropertyChanged("osnov");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                if ((this.povrsinaField.Equals(value) != true)) {
                    this.povrsinaField = value;
                    this.RaisePropertyChanged("povrsina");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoField, value) != true)) {
                    this.pravoField = value;
                    this.RaisePropertyChanged("pravo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string stan {
            get {
                return this.stanField;
            }
            set {
                if ((object.ReferenceEquals(this.stanField, value) != true)) {
                    this.stanField = value;
                    this.RaisePropertyChanged("stan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string vlez {
            get {
                return this.vlezField;
            }
            set {
                if ((object.ReferenceEquals(this.vlezField, value) != true)) {
                    this.vlezField = value;
                    this.RaisePropertyChanged("vlez");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Parcel", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class Parcel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string broj_delField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string klasaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string kulturaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mestoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int objektField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long povrsinaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string pravoField;
        
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
        public string broj_del {
            get {
                return this.broj_delField;
            }
            set {
                if ((object.ReferenceEquals(this.broj_delField, value) != true)) {
                    this.broj_delField = value;
                    this.RaisePropertyChanged("broj_del");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string klasa {
            get {
                return this.klasaField;
            }
            set {
                if ((object.ReferenceEquals(this.klasaField, value) != true)) {
                    this.klasaField = value;
                    this.RaisePropertyChanged("klasa");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string kultura {
            get {
                return this.kulturaField;
            }
            set {
                if ((object.ReferenceEquals(this.kulturaField, value) != true)) {
                    this.kulturaField = value;
                    this.RaisePropertyChanged("kultura");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoField, value) != true)) {
                    this.mestoField = value;
                    this.RaisePropertyChanged("mesto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                if ((this.objektField.Equals(value) != true)) {
                    this.objektField = value;
                    this.RaisePropertyChanged("objekt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                if ((this.povrsinaField.Equals(value) != true)) {
                    this.povrsinaField = value;
                    this.RaisePropertyChanged("povrsina");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoField, value) != true)) {
                    this.pravoField = value;
                    this.RaisePropertyChanged("pravo");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Owner", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class Owner : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string brojField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string delField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string embgField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string imeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mestoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ulicaField;
        
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
        public string broj {
            get {
                return this.brojField;
            }
            set {
                if ((object.ReferenceEquals(this.brojField, value) != true)) {
                    this.brojField = value;
                    this.RaisePropertyChanged("broj");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string del {
            get {
                return this.delField;
            }
            set {
                if ((object.ReferenceEquals(this.delField, value) != true)) {
                    this.delField = value;
                    this.RaisePropertyChanged("del");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string embg {
            get {
                return this.embgField;
            }
            set {
                if ((object.ReferenceEquals(this.embgField, value) != true)) {
                    this.embgField = value;
                    this.RaisePropertyChanged("embg");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ime {
            get {
                return this.imeField;
            }
            set {
                if ((object.ReferenceEquals(this.imeField, value) != true)) {
                    this.imeField = value;
                    this.RaisePropertyChanged("ime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoField, value) != true)) {
                    this.mestoField = value;
                    this.RaisePropertyChanged("mesto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ulica {
            get {
                return this.ulicaField;
            }
            set {
                if ((object.ReferenceEquals(this.ulicaField, value) != true)) {
                    this.ulicaField = value;
                    this.RaisePropertyChanged("ulica");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Loads", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class Loads : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string textField;
        
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
        public string text {
            get {
                return this.textField;
            }
            set {
                if ((object.ReferenceEquals(this.textField, value) != true)) {
                    this.textField = value;
                    this.RaisePropertyChanged("text");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ATRparceli", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class ATRparceli : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string messageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestAKNAdapterForParcel.AKNAdapterService.ParcelAtr[] nizparField;
        
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
        public string message {
            get {
                return this.messageField;
            }
            set {
                if ((object.ReferenceEquals(this.messageField, value) != true)) {
                    this.messageField = value;
                    this.RaisePropertyChanged("message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestAKNAdapterForParcel.AKNAdapterService.ParcelAtr[] nizpar {
            get {
                return this.nizparField;
            }
            set {
                if ((object.ReferenceEquals(this.nizparField, value) != true)) {
                    this.nizparField = value;
                    this.RaisePropertyChanged("nizpar");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ParcelAtr", Namespace="http://schemas.datacontract.org/2004/07/AdapterServiceAKN")]
    [System.SerializableAttribute()]
    public partial class ParcelAtr : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string broj_delField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ilistField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string kopsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string kulturaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mestoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int objektField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string opsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long povrsinaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string pravoField;
        
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
        public string broj_del {
            get {
                return this.broj_delField;
            }
            set {
                if ((object.ReferenceEquals(this.broj_delField, value) != true)) {
                    this.broj_delField = value;
                    this.RaisePropertyChanged("broj_del");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ilist {
            get {
                return this.ilistField;
            }
            set {
                if ((object.ReferenceEquals(this.ilistField, value) != true)) {
                    this.ilistField = value;
                    this.RaisePropertyChanged("ilist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string kops {
            get {
                return this.kopsField;
            }
            set {
                if ((object.ReferenceEquals(this.kopsField, value) != true)) {
                    this.kopsField = value;
                    this.RaisePropertyChanged("kops");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string kultura {
            get {
                return this.kulturaField;
            }
            set {
                if ((object.ReferenceEquals(this.kulturaField, value) != true)) {
                    this.kulturaField = value;
                    this.RaisePropertyChanged("kultura");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                if ((object.ReferenceEquals(this.mestoField, value) != true)) {
                    this.mestoField = value;
                    this.RaisePropertyChanged("mesto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                if ((this.objektField.Equals(value) != true)) {
                    this.objektField = value;
                    this.RaisePropertyChanged("objekt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ops {
            get {
                return this.opsField;
            }
            set {
                if ((object.ReferenceEquals(this.opsField, value) != true)) {
                    this.opsField = value;
                    this.RaisePropertyChanged("ops");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                if ((this.povrsinaField.Equals(value) != true)) {
                    this.povrsinaField = value;
                    this.RaisePropertyChanged("povrsina");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                if ((object.ReferenceEquals(this.pravoField, value) != true)) {
                    this.pravoField = value;
                    this.RaisePropertyChanged("pravo");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AKNAdapterService.IAKNService")]
    public interface IAKNService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetPropertyList", ReplyAction="http://interop.org/IAKNService/GetPropertyListResponse")]
        TestAKNAdapterForParcel.AKNAdapterService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetPropertyList", ReplyAction="http://interop.org/IAKNService/GetPropertyListResponse")]
        System.Threading.Tasks.Task<TestAKNAdapterForParcel.AKNAdapterService.dzgr> GetPropertyListAsync(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetCadastrialParcel", ReplyAction="http://interop.org/IAKNService/GetCadastrialParcelResponse")]
        TestAKNAdapterForParcel.AKNAdapterService.ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IAKNService/GetCadastrialParcel", ReplyAction="http://interop.org/IAKNService/GetCadastrialParcelResponse")]
        System.Threading.Tasks.Task<TestAKNAdapterForParcel.AKNAdapterService.ATRparceli> GetCadastrialParcelAsync(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAKNServiceChannel : TestAKNAdapterForParcel.AKNAdapterService.IAKNService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AKNServiceClient : System.ServiceModel.ClientBase<TestAKNAdapterForParcel.AKNAdapterService.IAKNService>, TestAKNAdapterForParcel.AKNAdapterService.IAKNService {
        
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
        
        public TestAKNAdapterForParcel.AKNAdapterService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList) {
            return base.Channel.GetPropertyList(username, password, opstina, katastarskaOpstina, brImotenList);
        }
        
        public System.Threading.Tasks.Task<TestAKNAdapterForParcel.AKNAdapterService.dzgr> GetPropertyListAsync(string username, string password, string opstina, string katastarskaOpstina, string brImotenList) {
            return base.Channel.GetPropertyListAsync(username, password, opstina, katastarskaOpstina, brImotenList);
        }
        
        public TestAKNAdapterForParcel.AKNAdapterService.ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela) {
            return base.Channel.GetCadastrialParcel(username, password, opstina, katastarskaOpstina, brParcela);
        }
        
        public System.Threading.Tasks.Task<TestAKNAdapterForParcel.AKNAdapterService.ATRparceli> GetCadastrialParcelAsync(string username, string password, string opstina, string katastarskaOpstina, string brParcela) {
            return base.Channel.GetCadastrialParcelAsync(username, password, opstina, katastarskaOpstina, brParcela);
        }
    }
}