﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contracts.AKNMunicipalities {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.katastar.gov.mk/", ConfigurationName="AKNMunicipalities.Service_MACEDONIAN_CADASTRESoap")]
    public interface Service_MACEDONIAN_CADASTRESoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/LOG", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int LOG(string us, string ps, string[] nizaserial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/LOG", ReplyAction="*")]
        System.Threading.Tasks.Task<int> LOGAsync(string us, string ps, string[] nizaserial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/vratiDATAsostojba", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string vratiDATAsostojba();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/vratiDATAsostojba", ReplyAction="*")]
        System.Threading.Tasks.Task<string> vratiDATAsostojbaAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_DATASET", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VRATIPODATOCI_STRUKTURA_DATASET(string us, string ps, string ops, string kops, string ilist, string[] nizaserial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_DATASET", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> VRATIPODATOCI_STRUKTURA_DATASETAsync(string us, string ps, string ops, string kops, string ilist, string[] nizaserial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_DZGR", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Contracts.AKNMunicipalities.dzgr VRATIPODATOCI_STRUKTURA_DZGR(string us, string ps, string ops, string kops, string ilist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_DZGR", ReplyAction="*")]
        System.Threading.Tasks.Task<Contracts.AKNMunicipalities.dzgr> VRATIPODATOCI_STRUKTURA_DZGRAsync(string us, string ps, string ops, string kops, string ilist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIOPSDATASET", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VRATIOPSDATASET();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIOPSDATASET", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> VRATIOPSDATASETAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIKATOPSDATASET", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VRATIKATOPSDATASET(string ops);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIKATOPSDATASET", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> VRATIKATOPSDATASETAsync(string ops);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIKREDIT", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int VRATIKREDIT(string us, string ps);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIKREDIT", ReplyAction="*")]
        System.Threading.Tasks.Task<int> VRATIKREDITAsync(string us, string ps);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_PARCELA", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Contracts.AKNMunicipalities.ATRparceli VRATIPODATOCI_STRUKTURA_PARCELA(string us, string ps, string ops, string kops, string broj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIPODATOCI_STRUKTURA_PARCELA", ReplyAction="*")]
        System.Threading.Tasks.Task<Contracts.AKNMunicipalities.ATRparceli> VRATIPODATOCI_STRUKTURA_PARCELAAsync(string us, string ps, string ops, string kops, string broj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIulici", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Contracts.AKNMunicipalities.ulici VRATIulici(string us);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.katastar.gov.mk/VRATIulici", ReplyAction="*")]
        System.Threading.Tasks.Task<Contracts.AKNMunicipalities.ulici> VRATIuliciAsync(string us);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class dzgr : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string opsField;
        
        private string kopsField;
        
        private string ilistField;
        
        private tovari[] niztovField;
        
        private objekti[] nizobjField;
        
        private sopstvenici[] nizsopField;
        
        private parceli[] nizparField;
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ops {
            get {
                return this.opsField;
            }
            set {
                this.opsField = value;
                this.RaisePropertyChanged("ops");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string kops {
            get {
                return this.kopsField;
            }
            set {
                this.kopsField = value;
                this.RaisePropertyChanged("kops");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ilist {
            get {
                return this.ilistField;
            }
            set {
                this.ilistField = value;
                this.RaisePropertyChanged("ilist");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public tovari[] niztov {
            get {
                return this.niztovField;
            }
            set {
                this.niztovField = value;
                this.RaisePropertyChanged("niztov");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public objekti[] nizobj {
            get {
                return this.nizobjField;
            }
            set {
                this.nizobjField = value;
                this.RaisePropertyChanged("nizobj");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=5)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public sopstvenici[] nizsop {
            get {
                return this.nizsopField;
            }
            set {
                this.nizsopField = value;
                this.RaisePropertyChanged("nizsop");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=6)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public parceli[] nizpar {
            get {
                return this.nizparField;
            }
            set {
                this.nizparField = value;
                this.RaisePropertyChanged("nizpar");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class tovari : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string textField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
                this.RaisePropertyChanged("text");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class ulici : object, System.ComponentModel.INotifyPropertyChanged {
        
        private tovari[] ulField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public tovari[] ul {
            get {
                return this.ulField;
            }
            set {
                this.ulField = value;
                this.RaisePropertyChanged("ul");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class atributiparcela : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string opsField;
        
        private string kopsField;
        
        private string ilistField;
        
        private string broj_delField;
        
        private int objektField;
        
        private string mestoField;
        
        private string kulturaField;
        
        private long povrsinaField;
        
        private string pravoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ops {
            get {
                return this.opsField;
            }
            set {
                this.opsField = value;
                this.RaisePropertyChanged("ops");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string kops {
            get {
                return this.kopsField;
            }
            set {
                this.kopsField = value;
                this.RaisePropertyChanged("kops");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ilist {
            get {
                return this.ilistField;
            }
            set {
                this.ilistField = value;
                this.RaisePropertyChanged("ilist");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string broj_del {
            get {
                return this.broj_delField;
            }
            set {
                this.broj_delField = value;
                this.RaisePropertyChanged("broj_del");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                this.objektField = value;
                this.RaisePropertyChanged("objekt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                this.mestoField = value;
                this.RaisePropertyChanged("mesto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string kultura {
            get {
                return this.kulturaField;
            }
            set {
                this.kulturaField = value;
                this.RaisePropertyChanged("kultura");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                this.povrsinaField = value;
                this.RaisePropertyChanged("povrsina");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                this.pravoField = value;
                this.RaisePropertyChanged("pravo");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class ATRparceli : object, System.ComponentModel.INotifyPropertyChanged {
        
        private atributiparcela[] nizparField;
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public atributiparcela[] nizpar {
            get {
                return this.nizparField;
            }
            set {
                this.nizparField = value;
                this.RaisePropertyChanged("nizpar");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class parceli : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string broj_delField;
        
        private int objektField;
        
        private string mestoField;
        
        private string kulturaField;
        
        private long povrsinaField;
        
        private string pravoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string broj_del {
            get {
                return this.broj_delField;
            }
            set {
                this.broj_delField = value;
                this.RaisePropertyChanged("broj_del");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                this.objektField = value;
                this.RaisePropertyChanged("objekt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                this.mestoField = value;
                this.RaisePropertyChanged("mesto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string kultura {
            get {
                return this.kulturaField;
            }
            set {
                this.kulturaField = value;
                this.RaisePropertyChanged("kultura");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                this.povrsinaField = value;
                this.RaisePropertyChanged("povrsina");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                this.pravoField = value;
                this.RaisePropertyChanged("pravo");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class sopstvenici : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string imeField;
        
        private string mestoField;
        
        private string ulicaField;
        
        private string brojField;
        
        private string delField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ime {
            get {
                return this.imeField;
            }
            set {
                this.imeField = value;
                this.RaisePropertyChanged("ime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                this.mestoField = value;
                this.RaisePropertyChanged("mesto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ulica {
            get {
                return this.ulicaField;
            }
            set {
                this.ulicaField = value;
                this.RaisePropertyChanged("ulica");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string broj {
            get {
                return this.brojField;
            }
            set {
                this.brojField = value;
                this.RaisePropertyChanged("broj");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string del {
            get {
                return this.delField;
            }
            set {
                this.delField = value;
                this.RaisePropertyChanged("del");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.katastar.gov.mk/")]
    public partial class objekti : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string brojField;
        
        private int objektField;
        
        private string vlezField;
        
        private string katField;
        
        private string stanField;
        
        private string namenaField;
        
        private string mestoField;
        
        private long povrsinaField;
        
        private string pravoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string broj {
            get {
                return this.brojField;
            }
            set {
                this.brojField = value;
                this.RaisePropertyChanged("broj");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int objekt {
            get {
                return this.objektField;
            }
            set {
                this.objektField = value;
                this.RaisePropertyChanged("objekt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string vlez {
            get {
                return this.vlezField;
            }
            set {
                this.vlezField = value;
                this.RaisePropertyChanged("vlez");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string kat {
            get {
                return this.katField;
            }
            set {
                this.katField = value;
                this.RaisePropertyChanged("kat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string stan {
            get {
                return this.stanField;
            }
            set {
                this.stanField = value;
                this.RaisePropertyChanged("stan");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string namena {
            get {
                return this.namenaField;
            }
            set {
                this.namenaField = value;
                this.RaisePropertyChanged("namena");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string mesto {
            get {
                return this.mestoField;
            }
            set {
                this.mestoField = value;
                this.RaisePropertyChanged("mesto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public long povrsina {
            get {
                return this.povrsinaField;
            }
            set {
                this.povrsinaField = value;
                this.RaisePropertyChanged("povrsina");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string pravo {
            get {
                return this.pravoField;
            }
            set {
                this.pravoField = value;
                this.RaisePropertyChanged("pravo");
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
    public interface Service_MACEDONIAN_CADASTRESoapChannel : Contracts.AKNMunicipalities.Service_MACEDONIAN_CADASTRESoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service_MACEDONIAN_CADASTRESoapClient : System.ServiceModel.ClientBase<Contracts.AKNMunicipalities.Service_MACEDONIAN_CADASTRESoap>, Contracts.AKNMunicipalities.Service_MACEDONIAN_CADASTRESoap {
        
        public Service_MACEDONIAN_CADASTRESoapClient() {
        }
        
        public Service_MACEDONIAN_CADASTRESoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service_MACEDONIAN_CADASTRESoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service_MACEDONIAN_CADASTRESoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service_MACEDONIAN_CADASTRESoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int LOG(string us, string ps, string[] nizaserial) {
            return base.Channel.LOG(us, ps, nizaserial);
        }
        
        public System.Threading.Tasks.Task<int> LOGAsync(string us, string ps, string[] nizaserial) {
            return base.Channel.LOGAsync(us, ps, nizaserial);
        }
        
        public string vratiDATAsostojba() {
            return base.Channel.vratiDATAsostojba();
        }
        
        public System.Threading.Tasks.Task<string> vratiDATAsostojbaAsync() {
            return base.Channel.vratiDATAsostojbaAsync();
        }
        
        public System.Data.DataSet VRATIPODATOCI_STRUKTURA_DATASET(string us, string ps, string ops, string kops, string ilist, string[] nizaserial) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_DATASET(us, ps, ops, kops, ilist, nizaserial);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> VRATIPODATOCI_STRUKTURA_DATASETAsync(string us, string ps, string ops, string kops, string ilist, string[] nizaserial) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_DATASETAsync(us, ps, ops, kops, ilist, nizaserial);
        }
        
        public Contracts.AKNMunicipalities.dzgr VRATIPODATOCI_STRUKTURA_DZGR(string us, string ps, string ops, string kops, string ilist) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_DZGR(us, ps, ops, kops, ilist);
        }
        
        public System.Threading.Tasks.Task<Contracts.AKNMunicipalities.dzgr> VRATIPODATOCI_STRUKTURA_DZGRAsync(string us, string ps, string ops, string kops, string ilist) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_DZGRAsync(us, ps, ops, kops, ilist);
        }
        
        public System.Data.DataSet VRATIOPSDATASET() {
            return base.Channel.VRATIOPSDATASET();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> VRATIOPSDATASETAsync() {
            return base.Channel.VRATIOPSDATASETAsync();
        }
        
        public System.Data.DataSet VRATIKATOPSDATASET(string ops) {
            return base.Channel.VRATIKATOPSDATASET(ops);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> VRATIKATOPSDATASETAsync(string ops) {
            return base.Channel.VRATIKATOPSDATASETAsync(ops);
        }
        
        public int VRATIKREDIT(string us, string ps) {
            return base.Channel.VRATIKREDIT(us, ps);
        }
        
        public System.Threading.Tasks.Task<int> VRATIKREDITAsync(string us, string ps) {
            return base.Channel.VRATIKREDITAsync(us, ps);
        }
        
        public Contracts.AKNMunicipalities.ATRparceli VRATIPODATOCI_STRUKTURA_PARCELA(string us, string ps, string ops, string kops, string broj) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_PARCELA(us, ps, ops, kops, broj);
        }
        
        public System.Threading.Tasks.Task<Contracts.AKNMunicipalities.ATRparceli> VRATIPODATOCI_STRUKTURA_PARCELAAsync(string us, string ps, string ops, string kops, string broj) {
            return base.Channel.VRATIPODATOCI_STRUKTURA_PARCELAAsync(us, ps, ops, kops, broj);
        }
        
        public Contracts.AKNMunicipalities.ulici VRATIulici(string us) {
            return base.Channel.VRATIulici(us);
        }
        
        public System.Threading.Tasks.Task<Contracts.AKNMunicipalities.ulici> VRATIuliciAsync(string us) {
            return base.Channel.VRATIuliciAsync(us);
        }
    }
}
