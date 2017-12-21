﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Implementations.Implementations.CRRMProd {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://e-services.crm.com.mk/XWSS/", ConfigurationName="CRRMProd.XmlWebServiceSoap")]
    public interface XmlWebServiceSoap {
        
        // CODEGEN: Generating message contract since message ProcessRequestRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="https://e-services.crm.com.mk/XWSS/ProcessRequest", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Implementations.CRRMProd.ProcessRequestResponse ProcessRequest(Implementations.CRRMProd.ProcessRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://e-services.crm.com.mk/XWSS/ProcessRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessRequestResponse> ProcessRequestAsync(Implementations.CRRMProd.ProcessRequestRequest request);
        
        // CODEGEN: Generating message contract since message ProcessSignedRequestRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="https://e-services.crm.com.mk/XWSS/ProcessSignedRequest", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Implementations.CRRMProd.ProcessSignedRequestResponse ProcessSignedRequest(Implementations.CRRMProd.ProcessSignedRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://e-services.crm.com.mk/XWSS/ProcessSignedRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessSignedRequestResponse> ProcessSignedRequestAsync(Implementations.CRRMProd.ProcessSignedRequestRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://e-services.crm.com.mk/XWSS/")]
    public partial class XmlSoapHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessRequest", WrapperNamespace="https://e-services.crm.com.mk/XWSS/", IsWrapped=true)]
    public partial class ProcessRequestRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://e-services.crm.com.mk/XWSS/")]
        public Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://e-services.crm.com.mk/XWSS/", Order=0)]
        public string parameters;
        
        public ProcessRequestRequest() {
        }
        
        public ProcessRequestRequest(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            this.XmlSoapHeader = XmlSoapHeader;
            this.parameters = parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessRequestResponse", WrapperNamespace="https://e-services.crm.com.mk/XWSS/", IsWrapped=true)]
    public partial class ProcessRequestResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://e-services.crm.com.mk/XWSS/")]
        public Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://e-services.crm.com.mk/XWSS/", Order=0)]
        public string ProcessRequestResult;
        
        public ProcessRequestResponse() {
        }
        
        public ProcessRequestResponse(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string ProcessRequestResult) {
            this.XmlSoapHeader = XmlSoapHeader;
            this.ProcessRequestResult = ProcessRequestResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessSignedRequest", WrapperNamespace="https://e-services.crm.com.mk/XWSS/", IsWrapped=true)]
    public partial class ProcessSignedRequestRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://e-services.crm.com.mk/XWSS/")]
        public Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://e-services.crm.com.mk/XWSS/", Order=0)]
        public string parameters;
        
        public ProcessSignedRequestRequest() {
        }
        
        public ProcessSignedRequestRequest(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            this.XmlSoapHeader = XmlSoapHeader;
            this.parameters = parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessSignedRequestResponse", WrapperNamespace="https://e-services.crm.com.mk/XWSS/", IsWrapped=true)]
    public partial class ProcessSignedRequestResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="https://e-services.crm.com.mk/XWSS/")]
        public Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://e-services.crm.com.mk/XWSS/", Order=0)]
        public string ProcessSignedRequestResult;
        
        public ProcessSignedRequestResponse() {
        }
        
        public ProcessSignedRequestResponse(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string ProcessSignedRequestResult) {
            this.XmlSoapHeader = XmlSoapHeader;
            this.ProcessSignedRequestResult = ProcessSignedRequestResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface XmlWebServiceSoapChannel : Implementations.CRRMProd.XmlWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class XmlWebServiceSoapClient : System.ServiceModel.ClientBase<Implementations.CRRMProd.XmlWebServiceSoap>, Implementations.CRRMProd.XmlWebServiceSoap {
        
        public XmlWebServiceSoapClient() {
        }
        
        public XmlWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public XmlWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public XmlWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public XmlWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Implementations.CRRMProd.ProcessRequestResponse Implementations.CRRMProd.XmlWebServiceSoap.ProcessRequest(Implementations.CRRMProd.ProcessRequestRequest request) {
            return base.Channel.ProcessRequest(request);
        }
        
        public string ProcessRequest(ref Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            Implementations.CRRMProd.ProcessRequestRequest inValue = new Implementations.CRRMProd.ProcessRequestRequest();
            inValue.XmlSoapHeader = XmlSoapHeader;
            inValue.parameters = parameters;
            Implementations.CRRMProd.ProcessRequestResponse retVal = ((Implementations.CRRMProd.XmlWebServiceSoap)(this)).ProcessRequest(inValue);
            XmlSoapHeader = retVal.XmlSoapHeader;
            return retVal.ProcessRequestResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessRequestResponse> Implementations.CRRMProd.XmlWebServiceSoap.ProcessRequestAsync(Implementations.CRRMProd.ProcessRequestRequest request) {
            return base.Channel.ProcessRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessRequestResponse> ProcessRequestAsync(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            Implementations.CRRMProd.ProcessRequestRequest inValue = new Implementations.CRRMProd.ProcessRequestRequest();
            inValue.XmlSoapHeader = XmlSoapHeader;
            inValue.parameters = parameters;
            return ((Implementations.CRRMProd.XmlWebServiceSoap)(this)).ProcessRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Implementations.CRRMProd.ProcessSignedRequestResponse Implementations.CRRMProd.XmlWebServiceSoap.ProcessSignedRequest(Implementations.CRRMProd.ProcessSignedRequestRequest request) {
            return base.Channel.ProcessSignedRequest(request);
        }
        
        public string ProcessSignedRequest(ref Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            Implementations.CRRMProd.ProcessSignedRequestRequest inValue = new Implementations.CRRMProd.ProcessSignedRequestRequest();
            inValue.XmlSoapHeader = XmlSoapHeader;
            inValue.parameters = parameters;
            Implementations.CRRMProd.ProcessSignedRequestResponse retVal = ((Implementations.CRRMProd.XmlWebServiceSoap)(this)).ProcessSignedRequest(inValue);
            XmlSoapHeader = retVal.XmlSoapHeader;
            return retVal.ProcessSignedRequestResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessSignedRequestResponse> Implementations.CRRMProd.XmlWebServiceSoap.ProcessSignedRequestAsync(Implementations.CRRMProd.ProcessSignedRequestRequest request) {
            return base.Channel.ProcessSignedRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<Implementations.CRRMProd.ProcessSignedRequestResponse> ProcessSignedRequestAsync(Implementations.CRRMProd.XmlSoapHeader XmlSoapHeader, string parameters) {
            Implementations.CRRMProd.ProcessSignedRequestRequest inValue = new Implementations.CRRMProd.ProcessSignedRequestRequest();
            inValue.XmlSoapHeader = XmlSoapHeader;
            inValue.parameters = parameters;
            return ((Implementations.CRRMProd.XmlWebServiceSoap)(this)).ProcessSignedRequestAsync(inValue);
        }
    }
}
