﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRRMTest.AdapterListOfSubjects {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="AdapterListOfSubjects.IListOfSubjectsCU")]
    public interface IListOfSubjectsCU {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IListOfSubjectsCU/GetSubjectsCU", ReplyAction="http://interop.org/IListOfSubjectsCU/GetSubjectsCUResponse")]
        string GetSubjectsCU(string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IListOfSubjectsCU/GetSubjectsCU", ReplyAction="http://interop.org/IListOfSubjectsCU/GetSubjectsCUResponse")]
        System.Threading.Tasks.Task<string> GetSubjectsCUAsync(string param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IListOfSubjectsCUChannel : CRRMTest.AdapterListOfSubjects.IListOfSubjectsCU, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ListOfSubjectsCUClient : System.ServiceModel.ClientBase<CRRMTest.AdapterListOfSubjects.IListOfSubjectsCU>, CRRMTest.AdapterListOfSubjects.IListOfSubjectsCU {
        
        public ListOfSubjectsCUClient() {
        }
        
        public ListOfSubjectsCUClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ListOfSubjectsCUClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ListOfSubjectsCUClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ListOfSubjectsCUClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetSubjectsCU(string param) {
            return base.Channel.GetSubjectsCU(param);
        }
        
        public System.Threading.Tasks.Task<string> GetSubjectsCUAsync(string param) {
            return base.Channel.GetSubjectsCUAsync(param);
        }
    }
}
