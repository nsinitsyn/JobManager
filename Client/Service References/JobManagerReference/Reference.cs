﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.JobManagerReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JobDto", Namespace="http://schemas.datacontract.org/2004/07/JobManager.JobManagerService.DTO")]
    [System.SerializableAttribute()]
    public partial class JobDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClassNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.JobManagerReference.TransferData DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ScheduledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.JobManagerReference.TriggerDto[] TriggersField;
        
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
        public string ClassName {
            get {
                return this.ClassNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClassNameField, value) != true)) {
                    this.ClassNameField = value;
                    this.RaisePropertyChanged("ClassName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.JobManagerReference.TransferData Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Scheduled {
            get {
                return this.ScheduledField;
            }
            set {
                if ((this.ScheduledField.Equals(value) != true)) {
                    this.ScheduledField = value;
                    this.RaisePropertyChanged("Scheduled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.JobManagerReference.TriggerDto[] Triggers {
            get {
                return this.TriggersField;
            }
            set {
                if ((object.ReferenceEquals(this.TriggersField, value) != true)) {
                    this.TriggersField = value;
                    this.RaisePropertyChanged("Triggers");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TransferData", Namespace="http://schemas.datacontract.org/2004/07/JobManager.JobManagerService.DTO")]
    [System.SerializableAttribute()]
    public partial class TransferData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.MemoryStream SerializedDataField;
        
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
        public System.IO.MemoryStream SerializedData {
            get {
                return this.SerializedDataField;
            }
            set {
                if ((object.ReferenceEquals(this.SerializedDataField, value) != true)) {
                    this.SerializedDataField = value;
                    this.RaisePropertyChanged("SerializedData");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TriggerDto", Namespace="http://schemas.datacontract.org/2004/07/JobManager.JobManagerService.DTO")]
    [System.SerializableAttribute()]
    public partial class TriggerDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CronField;
        
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
        public string Cron {
            get {
                return this.CronField;
            }
            set {
                if ((object.ReferenceEquals(this.CronField, value) != true)) {
                    this.CronField = value;
                    this.RaisePropertyChanged("Cron");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="WorkerDto", Namespace="http://schemas.datacontract.org/2004/07/JobManager.JobManagerService.DTO")]
    [System.SerializableAttribute()]
    public partial class WorkerDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid JobIdField;
        
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
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid JobId {
            get {
                return this.JobIdField;
            }
            set {
                if ((this.JobIdField.Equals(value) != true)) {
                    this.JobIdField = value;
                    this.RaisePropertyChanged("JobId");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="JobEventDto", Namespace="http://schemas.datacontract.org/2004/07/JobManager.JobManagerService.DTO")]
    [System.SerializableAttribute()]
    public partial class JobEventDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsReturnResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.JobManagerReference.TransferData TransferDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid WorkerIdField;
        
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
        public bool IsReturnResult {
            get {
                return this.IsReturnResultField;
            }
            set {
                if ((this.IsReturnResultField.Equals(value) != true)) {
                    this.IsReturnResultField = value;
                    this.RaisePropertyChanged("IsReturnResult");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.JobManagerReference.TransferData TransferData {
            get {
                return this.TransferDataField;
            }
            set {
                if ((object.ReferenceEquals(this.TransferDataField, value) != true)) {
                    this.TransferDataField = value;
                    this.RaisePropertyChanged("TransferData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid WorkerId {
            get {
                return this.WorkerIdField;
            }
            set {
                if ((this.WorkerIdField.Equals(value) != true)) {
                    this.WorkerIdField = value;
                    this.RaisePropertyChanged("WorkerId");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="JobManagerReference.IJobManagerService", CallbackContract=typeof(Client.JobManagerReference.IJobManagerServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IJobManagerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/RunJob", ReplyAction="http://tempuri.org/IJobManagerService/RunJobResponse")]
        Client.JobManagerReference.WorkerDto RunJob(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/RunJob", ReplyAction="http://tempuri.org/IJobManagerService/RunJobResponse")]
        System.Threading.Tasks.Task<Client.JobManagerReference.WorkerDto> RunJobAsync(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/Signal", ReplyAction="http://tempuri.org/IJobManagerService/SignalResponse")]
        Client.JobManagerReference.TransferData Signal(System.Guid workerId, Client.JobManagerReference.TransferData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/Signal", ReplyAction="http://tempuri.org/IJobManagerService/SignalResponse")]
        System.Threading.Tasks.Task<Client.JobManagerReference.TransferData> SignalAsync(System.Guid workerId, Client.JobManagerReference.TransferData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/GetJob", ReplyAction="http://tempuri.org/IJobManagerService/GetJobResponse")]
        Client.JobManagerReference.JobDto GetJob(System.Guid jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/GetJob", ReplyAction="http://tempuri.org/IJobManagerService/GetJobResponse")]
        System.Threading.Tasks.Task<Client.JobManagerReference.JobDto> GetJobAsync(System.Guid jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/ScheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/ScheduleJobResponse")]
        System.Guid ScheduleJob(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/ScheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/ScheduleJobResponse")]
        System.Threading.Tasks.Task<System.Guid> ScheduleJobAsync(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/RescheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/RescheduleJobResponse")]
        void RescheduleJob(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/RescheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/RescheduleJobResponse")]
        System.Threading.Tasks.Task RescheduleJobAsync(Client.JobManagerReference.JobDto jobDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/UnscheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/UnscheduleJobResponse")]
        void UnscheduleJob(System.Guid jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/UnscheduleJob", ReplyAction="http://tempuri.org/IJobManagerService/UnscheduleJobResponse")]
        System.Threading.Tasks.Task UnscheduleJobAsync(System.Guid jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/DeleteJob", ReplyAction="http://tempuri.org/IJobManagerService/DeleteJobResponse")]
        void DeleteJob(System.Guid jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/DeleteJob", ReplyAction="http://tempuri.org/IJobManagerService/DeleteJobResponse")]
        System.Threading.Tasks.Task DeleteJobAsync(System.Guid jobId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobManagerServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJobManagerService/OnEvent")]
        void OnEvent(Client.JobManagerReference.JobEventDto eventDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobManagerService/OnEventSync", ReplyAction="http://tempuri.org/IJobManagerService/OnEventSyncResponse")]
        Client.JobManagerReference.TransferData OnEventSync(Client.JobManagerReference.JobEventDto eventDto);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJobManagerService/WorkerWasStarted")]
        void WorkerWasStarted(Client.JobManagerReference.WorkerDto worker);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobManagerServiceChannel : Client.JobManagerReference.IJobManagerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JobManagerServiceClient : System.ServiceModel.DuplexClientBase<Client.JobManagerReference.IJobManagerService>, Client.JobManagerReference.IJobManagerService {
        
        public JobManagerServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public JobManagerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public JobManagerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobManagerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobManagerServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public Client.JobManagerReference.WorkerDto RunJob(Client.JobManagerReference.JobDto jobDto) {
            return base.Channel.RunJob(jobDto);
        }
        
        public System.Threading.Tasks.Task<Client.JobManagerReference.WorkerDto> RunJobAsync(Client.JobManagerReference.JobDto jobDto) {
            return base.Channel.RunJobAsync(jobDto);
        }
        
        public Client.JobManagerReference.TransferData Signal(System.Guid workerId, Client.JobManagerReference.TransferData data) {
            return base.Channel.Signal(workerId, data);
        }
        
        public System.Threading.Tasks.Task<Client.JobManagerReference.TransferData> SignalAsync(System.Guid workerId, Client.JobManagerReference.TransferData data) {
            return base.Channel.SignalAsync(workerId, data);
        }
        
        public Client.JobManagerReference.JobDto GetJob(System.Guid jobId) {
            return base.Channel.GetJob(jobId);
        }
        
        public System.Threading.Tasks.Task<Client.JobManagerReference.JobDto> GetJobAsync(System.Guid jobId) {
            return base.Channel.GetJobAsync(jobId);
        }
        
        public System.Guid ScheduleJob(Client.JobManagerReference.JobDto jobDto) {
            return base.Channel.ScheduleJob(jobDto);
        }
        
        public System.Threading.Tasks.Task<System.Guid> ScheduleJobAsync(Client.JobManagerReference.JobDto jobDto) {
            return base.Channel.ScheduleJobAsync(jobDto);
        }
        
        public void RescheduleJob(Client.JobManagerReference.JobDto jobDto) {
            base.Channel.RescheduleJob(jobDto);
        }
        
        public System.Threading.Tasks.Task RescheduleJobAsync(Client.JobManagerReference.JobDto jobDto) {
            return base.Channel.RescheduleJobAsync(jobDto);
        }
        
        public void UnscheduleJob(System.Guid jobId) {
            base.Channel.UnscheduleJob(jobId);
        }
        
        public System.Threading.Tasks.Task UnscheduleJobAsync(System.Guid jobId) {
            return base.Channel.UnscheduleJobAsync(jobId);
        }
        
        public void DeleteJob(System.Guid jobId) {
            base.Channel.DeleteJob(jobId);
        }
        
        public System.Threading.Tasks.Task DeleteJobAsync(System.Guid jobId) {
            return base.Channel.DeleteJobAsync(jobId);
        }
    }
}
