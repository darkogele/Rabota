﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.StatusForRegularStudent {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="StatusForRegularStudent.ISRegStudent")]
    public interface ISRegStudent {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ISRegStudent/GetStuS", ReplyAction="http://interop.org/ISRegStudent/GetStuSResponse")]
        string GetStuS(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/ISRegStudent/GetStuS", ReplyAction="http://interop.org/ISRegStudent/GetStuSResponse")]
        System.Threading.Tasks.Task<string> GetStuSAsync(string EMBG);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISRegStudentChannel : TestApp.StatusForRegularStudent.ISRegStudent, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SRegStudentClient : System.ServiceModel.ClientBase<TestApp.StatusForRegularStudent.ISRegStudent>, TestApp.StatusForRegularStudent.ISRegStudent {
        
        public SRegStudentClient() {
        }
        
        public SRegStudentClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SRegStudentClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SRegStudentClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SRegStudentClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetStuS(string EMBG) {
            return base.Channel.GetStuS(EMBG);
        }
        
        public System.Threading.Tasks.Task<string> GetStuSAsync(string EMBG) {
            return base.Channel.GetStuSAsync(EMBG);
        }
    }
}