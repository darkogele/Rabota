﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.DataForEDB {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org", ConfigurationName="DataForEDB.IDataForEDB")]
    public interface IDataForEDB {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEDB/GetEDB", ReplyAction="http://interop.org/IDataForEDB/GetEDBResponse")]
        string GetEDB(string edb);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDataForEDB/GetEDB", ReplyAction="http://interop.org/IDataForEDB/GetEDBResponse")]
        System.Threading.Tasks.Task<string> GetEDBAsync(string edb);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataForEDBChannel : TestApp.DataForEDB.IDataForEDB, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataForEDBClient : System.ServiceModel.ClientBase<TestApp.DataForEDB.IDataForEDB>, TestApp.DataForEDB.IDataForEDB {
        
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
        
        public string GetEDB(string edb) {
            return base.Channel.GetEDB(edb);
        }
        
        public System.Threading.Tasks.Task<string> GetEDBAsync(string edb) {
            return base.Channel.GetEDBAsync(edb);
        }
    }
}
