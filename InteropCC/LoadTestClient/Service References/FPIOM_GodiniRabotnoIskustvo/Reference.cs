﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadTestClient.FPIOM_GodiniRabotnoIskustvo {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="YearsOfWorkExperienceOutput", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YearsOfWorkExperienceOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEGeneralData GeneralDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEInsuranceData[] InsuranceDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEInvalidM4[] InvalidM4Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEM4[] M4Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEOldAndForegnExperience[] OldAndForeignExperienceField;
        
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
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEGeneralData GeneralData {
            get {
                return this.GeneralDataField;
            }
            set {
                if ((object.ReferenceEquals(this.GeneralDataField, value) != true)) {
                    this.GeneralDataField = value;
                    this.RaisePropertyChanged("GeneralData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEInsuranceData[] InsuranceData {
            get {
                return this.InsuranceDataField;
            }
            set {
                if ((object.ReferenceEquals(this.InsuranceDataField, value) != true)) {
                    this.InsuranceDataField = value;
                    this.RaisePropertyChanged("InsuranceData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEInvalidM4[] InvalidM4 {
            get {
                return this.InvalidM4Field;
            }
            set {
                if ((object.ReferenceEquals(this.InvalidM4Field, value) != true)) {
                    this.InvalidM4Field = value;
                    this.RaisePropertyChanged("InvalidM4");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEM4[] M4 {
            get {
                return this.M4Field;
            }
            set {
                if ((object.ReferenceEquals(this.M4Field, value) != true)) {
                    this.M4Field = value;
                    this.RaisePropertyChanged("M4");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YWEOldAndForegnExperience[] OldAndForeignExperience {
            get {
                return this.OldAndForeignExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.OldAndForeignExperienceField, value) != true)) {
                    this.OldAndForeignExperienceField = value;
                    this.RaisePropertyChanged("OldAndForeignExperience");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="YWEGeneralData", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YWEGeneralData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateOfBirhtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SurnameField;
        
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
        public string DateOfBirht {
            get {
                return this.DateOfBirhtField;
            }
            set {
                if ((object.ReferenceEquals(this.DateOfBirhtField, value) != true)) {
                    this.DateOfBirhtField = value;
                    this.RaisePropertyChanged("DateOfBirht");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((object.ReferenceEquals(this.GenderField, value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Surname {
            get {
                return this.SurnameField;
            }
            set {
                if ((object.ReferenceEquals(this.SurnameField, value) != true)) {
                    this.SurnameField = value;
                    this.RaisePropertyChanged("Surname");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="YWEInsuranceData", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YWEInsuranceData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyRegistrationNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EDBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EMBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StartDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WeeklyHoursField;
        
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
        public string CompanyName {
            get {
                return this.CompanyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyNameField, value) != true)) {
                    this.CompanyNameField = value;
                    this.RaisePropertyChanged("CompanyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyRegistrationNumber {
            get {
                return this.CompanyRegistrationNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyRegistrationNumberField, value) != true)) {
                    this.CompanyRegistrationNumberField = value;
                    this.RaisePropertyChanged("CompanyRegistrationNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EDB {
            get {
                return this.EDBField;
            }
            set {
                if ((object.ReferenceEquals(this.EDBField, value) != true)) {
                    this.EDBField = value;
                    this.RaisePropertyChanged("EDB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMB {
            get {
                return this.EMBField;
            }
            set {
                if ((object.ReferenceEquals(this.EMBField, value) != true)) {
                    this.EMBField = value;
                    this.RaisePropertyChanged("EMB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EndDate {
            get {
                return this.EndDateField;
            }
            set {
                if ((object.ReferenceEquals(this.EndDateField, value) != true)) {
                    this.EndDateField = value;
                    this.RaisePropertyChanged("EndDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StartData {
            get {
                return this.StartDataField;
            }
            set {
                if ((object.ReferenceEquals(this.StartDataField, value) != true)) {
                    this.StartDataField = value;
                    this.RaisePropertyChanged("StartData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int WeeklyHours {
            get {
                return this.WeeklyHoursField;
            }
            set {
                if ((this.WeeklyHoursField.Equals(value) != true)) {
                    this.WeeklyHoursField = value;
                    this.RaisePropertyChanged("WeeklyHours");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="YWEInvalidM4", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YWEInvalidM4 : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DegreeOfIncreaseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DurationWorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EffectiveDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RegistrationNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SalaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeOfFormField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkingHoursField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string YearField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string YearOfSickLeaveField;
        
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
        public string DegreeOfIncrease {
            get {
                return this.DegreeOfIncreaseField;
            }
            set {
                if ((object.ReferenceEquals(this.DegreeOfIncreaseField, value) != true)) {
                    this.DegreeOfIncreaseField = value;
                    this.RaisePropertyChanged("DegreeOfIncrease");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DurationWorkExperience {
            get {
                return this.DurationWorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.DurationWorkExperienceField, value) != true)) {
                    this.DurationWorkExperienceField = value;
                    this.RaisePropertyChanged("DurationWorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EffectiveDuration {
            get {
                return this.EffectiveDurationField;
            }
            set {
                if ((object.ReferenceEquals(this.EffectiveDurationField, value) != true)) {
                    this.EffectiveDurationField = value;
                    this.RaisePropertyChanged("EffectiveDuration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodFrom {
            get {
                return this.PeriodFromField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodFromField, value) != true)) {
                    this.PeriodFromField = value;
                    this.RaisePropertyChanged("PeriodFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodTo {
            get {
                return this.PeriodToField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodToField, value) != true)) {
                    this.PeriodToField = value;
                    this.RaisePropertyChanged("PeriodTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RegistrationNumber {
            get {
                return this.RegistrationNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.RegistrationNumberField, value) != true)) {
                    this.RegistrationNumberField = value;
                    this.RaisePropertyChanged("RegistrationNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Salary {
            get {
                return this.SalaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SalaryField, value) != true)) {
                    this.SalaryField = value;
                    this.RaisePropertyChanged("Salary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TypeOfForm {
            get {
                return this.TypeOfFormField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeOfFormField, value) != true)) {
                    this.TypeOfFormField = value;
                    this.RaisePropertyChanged("TypeOfForm");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkingHours {
            get {
                return this.WorkingHoursField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkingHoursField, value) != true)) {
                    this.WorkingHoursField = value;
                    this.RaisePropertyChanged("WorkingHours");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Year {
            get {
                return this.YearField;
            }
            set {
                if ((object.ReferenceEquals(this.YearField, value) != true)) {
                    this.YearField = value;
                    this.RaisePropertyChanged("Year");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string YearOfSickLeave {
            get {
                return this.YearOfSickLeaveField;
            }
            set {
                if ((object.ReferenceEquals(this.YearOfSickLeaveField, value) != true)) {
                    this.YearOfSickLeaveField = value;
                    this.RaisePropertyChanged("YearOfSickLeave");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="YWEM4", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YWEM4 : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DegreeOfIncreaseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DurationWorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EffectiveDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MeseciField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RegistrationNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SalaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeOfFormField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkingHoursField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string YearField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string YearOfSickLeaveField;
        
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
        public string DegreeOfIncrease {
            get {
                return this.DegreeOfIncreaseField;
            }
            set {
                if ((object.ReferenceEquals(this.DegreeOfIncreaseField, value) != true)) {
                    this.DegreeOfIncreaseField = value;
                    this.RaisePropertyChanged("DegreeOfIncrease");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DurationWorkExperience {
            get {
                return this.DurationWorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.DurationWorkExperienceField, value) != true)) {
                    this.DurationWorkExperienceField = value;
                    this.RaisePropertyChanged("DurationWorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EffectiveDuration {
            get {
                return this.EffectiveDurationField;
            }
            set {
                if ((object.ReferenceEquals(this.EffectiveDurationField, value) != true)) {
                    this.EffectiveDurationField = value;
                    this.RaisePropertyChanged("EffectiveDuration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Meseci {
            get {
                return this.MeseciField;
            }
            set {
                if ((object.ReferenceEquals(this.MeseciField, value) != true)) {
                    this.MeseciField = value;
                    this.RaisePropertyChanged("Meseci");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodFrom {
            get {
                return this.PeriodFromField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodFromField, value) != true)) {
                    this.PeriodFromField = value;
                    this.RaisePropertyChanged("PeriodFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodTo {
            get {
                return this.PeriodToField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodToField, value) != true)) {
                    this.PeriodToField = value;
                    this.RaisePropertyChanged("PeriodTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RegistrationNumber {
            get {
                return this.RegistrationNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.RegistrationNumberField, value) != true)) {
                    this.RegistrationNumberField = value;
                    this.RaisePropertyChanged("RegistrationNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Salary {
            get {
                return this.SalaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SalaryField, value) != true)) {
                    this.SalaryField = value;
                    this.RaisePropertyChanged("Salary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TypeOfForm {
            get {
                return this.TypeOfFormField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeOfFormField, value) != true)) {
                    this.TypeOfFormField = value;
                    this.RaisePropertyChanged("TypeOfForm");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkingHours {
            get {
                return this.WorkingHoursField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkingHoursField, value) != true)) {
                    this.WorkingHoursField = value;
                    this.RaisePropertyChanged("WorkingHours");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Year {
            get {
                return this.YearField;
            }
            set {
                if ((object.ReferenceEquals(this.YearField, value) != true)) {
                    this.YearField = value;
                    this.RaisePropertyChanged("Year");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string YearOfSickLeave {
            get {
                return this.YearOfSickLeaveField;
            }
            set {
                if ((object.ReferenceEquals(this.YearOfSickLeaveField, value) != true)) {
                    this.YearOfSickLeaveField = value;
                    this.RaisePropertyChanged("YearOfSickLeave");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="YWEOldAndForegnExperience", Namespace="http://schemas.datacontract.org/2004/07/InteropServices.FPIOM.Implementations")]
    [System.SerializableAttribute()]
    public partial class YWEOldAndForegnExperience : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DegreeOfIncreaseOfWorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DurationWorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PeriodToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeOfWorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkExperienceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string YearField;
        
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
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DegreeOfIncreaseOfWorkExperience {
            get {
                return this.DegreeOfIncreaseOfWorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.DegreeOfIncreaseOfWorkExperienceField, value) != true)) {
                    this.DegreeOfIncreaseOfWorkExperienceField = value;
                    this.RaisePropertyChanged("DegreeOfIncreaseOfWorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DurationWorkExperience {
            get {
                return this.DurationWorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.DurationWorkExperienceField, value) != true)) {
                    this.DurationWorkExperienceField = value;
                    this.RaisePropertyChanged("DurationWorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PS {
            get {
                return this.PSField;
            }
            set {
                if ((object.ReferenceEquals(this.PSField, value) != true)) {
                    this.PSField = value;
                    this.RaisePropertyChanged("PS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodFrom {
            get {
                return this.PeriodFromField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodFromField, value) != true)) {
                    this.PeriodFromField = value;
                    this.RaisePropertyChanged("PeriodFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PeriodTo {
            get {
                return this.PeriodToField;
            }
            set {
                if ((object.ReferenceEquals(this.PeriodToField, value) != true)) {
                    this.PeriodToField = value;
                    this.RaisePropertyChanged("PeriodTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TypeOfWorkExperience {
            get {
                return this.TypeOfWorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeOfWorkExperienceField, value) != true)) {
                    this.TypeOfWorkExperienceField = value;
                    this.RaisePropertyChanged("TypeOfWorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkExperience {
            get {
                return this.WorkExperienceField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkExperienceField, value) != true)) {
                    this.WorkExperienceField = value;
                    this.RaisePropertyChanged("WorkExperience");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Year {
            get {
                return this.YearField;
            }
            set {
                if ((object.ReferenceEquals(this.YearField, value) != true)) {
                    this.YearField = value;
                    this.RaisePropertyChanged("Year");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interop.org/", ConfigurationName="FPIOM_GodiniRabotnoIskustvo.IYearsOfWorkExperience")]
    public interface IYearsOfWorkExperience {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IYearsOfWorkExperience/GetYWExpXML", ReplyAction="http://interop.org/IYearsOfWorkExperience/GetYWExpXMLResponse")]
        LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YearsOfWorkExperienceOutput GetYWExpXML(string EMBG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://interop.org/IYearsOfWorkExperience/GetYWExpXML", ReplyAction="http://interop.org/IYearsOfWorkExperience/GetYWExpXMLResponse")]
        System.Threading.Tasks.Task<LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YearsOfWorkExperienceOutput> GetYWExpXMLAsync(string EMBG);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IYearsOfWorkExperienceChannel : LoadTestClient.FPIOM_GodiniRabotnoIskustvo.IYearsOfWorkExperience, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class YearsOfWorkExperienceClient : System.ServiceModel.ClientBase<LoadTestClient.FPIOM_GodiniRabotnoIskustvo.IYearsOfWorkExperience>, LoadTestClient.FPIOM_GodiniRabotnoIskustvo.IYearsOfWorkExperience {
        
        public YearsOfWorkExperienceClient() {
        }
        
        public YearsOfWorkExperienceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public YearsOfWorkExperienceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public YearsOfWorkExperienceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public YearsOfWorkExperienceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YearsOfWorkExperienceOutput GetYWExpXML(string EMBG) {
            return base.Channel.GetYWExpXML(EMBG);
        }
        
        public System.Threading.Tasks.Task<LoadTestClient.FPIOM_GodiniRabotnoIskustvo.YearsOfWorkExperienceOutput> GetYWExpXMLAsync(string EMBG) {
            return base.Channel.GetYWExpXMLAsync(EMBG);
        }
    }
}
