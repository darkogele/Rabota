﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.DataForRegularStudent {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="DataForRegularStudent.IDRegStudent")]
    public interface IDRegStudent {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDRegStudent/GetStuD", ReplyAction="http://interop.org/IDRegStudent/GetStuDResponse")]
        string GetStuD(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IDRegStudent/GetStuD", ReplyAction="http://interop.org/IDRegStudent/GetStuDResponse")]
        System.Threading.Tasks.Task<string> GetStuDAsync(string EMBG);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDRegStudentChannel : TestApp.DataForRegularStudent.IDRegStudent, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DRegStudentClient : System.ServiceModel.ClientBase<TestApp.DataForRegularStudent.IDRegStudent>, TestApp.DataForRegularStudent.IDRegStudent {
        
        public DRegStudentClient() {
        }
        
        public DRegStudentClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DRegStudentClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DRegStudentClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DRegStudentClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetStuD(string EMBG) {
            return base.Channel.GetStuD(EMBG);
        }
        
        public System.Threading.Tasks.Task<string> GetStuDAsync(string EMBG) {
            return base.Channel.GetStuDAsync(EMBG);
        }
    }
}
