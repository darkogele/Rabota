﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdapterServiceFPIOM.FPIOMOriginalService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interoperab_db2_nova/WSIO113.wsdl", ConfigurationName="FPIOMOriginalService.WSIO113PortType")]
    public interface WSIO113PortType {
        
        // CODEGEN: Generating message contract since the wrapper namespace (WSIO113) of message proveriDostapnost0Request does not match the default value (http://interoperab_db2_nova/WSIO113.wsdl)
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response proveriDostapnost(AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response> proveriDostapnostAsync(AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (WSIO113) of message penzioner1s1Request does not match the default value (http://interoperab_db2_nova/WSIO113.wsdl)
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response penzioner1s(AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response> penzioner1sAsync(AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (WSIO113) of message osigurenik1s2Request does not match the default value (http://interoperab_db2_nova/WSIO113.wsdl)
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response osigurenik1s(AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response> osigurenik1sAsync(AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="proveriDostapnost", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class proveriDostapnost0Request {
        
        public proveriDostapnost0Request() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="proveriDostapnostResponse", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class proveriDostapnost0Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string @return;
        
        public proveriDostapnost0Response() {
        }
        
        public proveriDostapnost0Response(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="penzioner1s", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class penzioner1s1Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string vString;
        
        public penzioner1s1Request() {
        }
        
        public penzioner1s1Request(string vString) {
            this.vString = vString;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="penzioner1sResponse", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class penzioner1s1Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string @return;
        
        public penzioner1s1Response() {
        }
        
        public penzioner1s1Response(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="osigurenik1s", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class osigurenik1s2Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string vString;
        
        public osigurenik1s2Request() {
        }
        
        public osigurenik1s2Request(string vString) {
            this.vString = vString;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="osigurenik1sResponse", WrapperNamespace="WSIO113", IsWrapped=true)]
    public partial class osigurenik1s2Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string @return;
        
        public osigurenik1s2Response() {
        }
        
        public osigurenik1s2Response(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSIO113PortTypeChannel : AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSIO113PortTypeClient : System.ServiceModel.ClientBase<AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType>, AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType {
        
        public WSIO113PortTypeClient() {
        }
        
        public WSIO113PortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSIO113PortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSIO113PortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSIO113PortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.proveriDostapnost(AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request request) {
            return base.Channel.proveriDostapnost(request);
        }
        
        public string proveriDostapnost() {
            AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request();
            AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response retVal = ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).proveriDostapnost(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response> AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.proveriDostapnostAsync(AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request request) {
            return base.Channel.proveriDostapnostAsync(request);
        }
        
        public System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Response> proveriDostapnostAsync() {
            AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.proveriDostapnost0Request();
            return ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).proveriDostapnostAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.penzioner1s(AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request request) {
            return base.Channel.penzioner1s(request);
        }
        
        public string penzioner1s(string vString) {
            AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request();
            inValue.vString = vString;
            AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response retVal = ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).penzioner1s(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response> AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.penzioner1sAsync(AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request request) {
            return base.Channel.penzioner1sAsync(request);
        }
        
        public System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Response> penzioner1sAsync(string vString) {
            AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.penzioner1s1Request();
            inValue.vString = vString;
            return ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).penzioner1sAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.osigurenik1s(AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request request) {
            return base.Channel.osigurenik1s(request);
        }
        
        public string osigurenik1s(string vString) {
            AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request();
            inValue.vString = vString;
            AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response retVal = ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).osigurenik1s(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response> AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType.osigurenik1sAsync(AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request request) {
            return base.Channel.osigurenik1sAsync(request);
        }
        
        public System.Threading.Tasks.Task<AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Response> osigurenik1sAsync(string vString) {
            AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request inValue = new AdapterServiceFPIOM.FPIOMOriginalService.osigurenik1s2Request();
            inValue.vString = vString;
            return ((AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortType)(this)).osigurenik1sAsync(inValue);
        }
    }
}