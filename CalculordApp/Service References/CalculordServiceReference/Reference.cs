﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalculordApp.CalculordServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CalculordServiceReference.ICalculord", CallbackContract=typeof(CalculordApp.CalculordServiceReference.ICalculordCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ICalculord {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/SetConnection")]
        void SetConnection(string id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/SetConnection")]
        System.Threading.Tasks.Task SetConnectionAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/Calculate")]
        void Calculate(string input);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/Calculate")]
        System.Threading.Tasks.Task CalculateAsync(string input);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculordCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/Authorize")]
        void Authorize(string id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/Equals")]
        void Equals(double result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculord/Reject")]
        void Reject();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculordChannel : CalculordApp.CalculordServiceReference.ICalculord, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculordClient : System.ServiceModel.DuplexClientBase<CalculordApp.CalculordServiceReference.ICalculord>, CalculordApp.CalculordServiceReference.ICalculord {
        
        public CalculordClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CalculordClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CalculordClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CalculordClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CalculordClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SetConnection(string id) {
            base.Channel.SetConnection(id);
        }
        
        public System.Threading.Tasks.Task SetConnectionAsync(string id) {
            return base.Channel.SetConnectionAsync(id);
        }
        
        public void Calculate(string input) {
            base.Channel.Calculate(input);
        }
        
        public System.Threading.Tasks.Task CalculateAsync(string input) {
            return base.Channel.CalculateAsync(input);
        }
    }
}
