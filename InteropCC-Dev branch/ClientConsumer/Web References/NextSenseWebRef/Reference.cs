﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18408.
// 
#pragma warning disable 1591

namespace ClientConsumer.NextSenseWebRef {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="M1M2ServiceSoap", Namespace="http://interop.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogBaseClass))]
    public partial class M1M2Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetM1M2OperationCompleted;
        
        private System.Threading.SendOrPostCallback GetM1M2ByIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocumentOperationCompleted;
        
        private System.Threading.SendOrPostCallback FullNameOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public M1M2Service() {
            this.Url = global::ClientConsumer.Properties.Settings.Default.ClientConsumer_NextSenseWebRef_M1M2Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetM1M2CompletedEventHandler GetM1M2Completed;
        
        /// <remarks/>
        public event GetM1M2ByIdCompletedEventHandler GetM1M2ByIdCompleted;
        
        /// <remarks/>
        public event GetDocumentCompletedEventHandler GetDocumentCompleted;
        
        /// <remarks/>
        public event FullNameCompletedEventHandler FullNameCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://interop.org/GetM1M2", RequestNamespace="http://interop.org/", ResponseNamespace="http://interop.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public M1M2[] GetM1M2(string personUidn, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<int> companyUidn, System.DateTime checkInDate) {
            object[] results = this.Invoke("GetM1M2", new object[] {
                        personUidn,
                        companyUidn,
                        checkInDate});
            return ((M1M2[])(results[0]));
        }
        
        /// <remarks/>
        public void GetM1M2Async(string personUidn, System.Nullable<int> companyUidn, System.DateTime checkInDate) {
            this.GetM1M2Async(personUidn, companyUidn, checkInDate, null);
        }
        
        /// <remarks/>
        public void GetM1M2Async(string personUidn, System.Nullable<int> companyUidn, System.DateTime checkInDate, object userState) {
            if ((this.GetM1M2OperationCompleted == null)) {
                this.GetM1M2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetM1M2OperationCompleted);
            }
            this.InvokeAsync("GetM1M2", new object[] {
                        personUidn,
                        companyUidn,
                        checkInDate}, this.GetM1M2OperationCompleted, userState);
        }
        
        private void OnGetM1M2OperationCompleted(object arg) {
            if ((this.GetM1M2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetM1M2Completed(this, new GetM1M2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://interop.org/GetM1M2ById", RequestNamespace="http://interop.org/", ResponseNamespace="http://interop.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public M1M2 GetM1M2ById(int id) {
            object[] results = this.Invoke("GetM1M2ById", new object[] {
                        id});
            return ((M1M2)(results[0]));
        }
        
        /// <remarks/>
        public void GetM1M2ByIdAsync(int id) {
            this.GetM1M2ByIdAsync(id, null);
        }
        
        /// <remarks/>
        public void GetM1M2ByIdAsync(int id, object userState) {
            if ((this.GetM1M2ByIdOperationCompleted == null)) {
                this.GetM1M2ByIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetM1M2ByIdOperationCompleted);
            }
            this.InvokeAsync("GetM1M2ById", new object[] {
                        id}, this.GetM1M2ByIdOperationCompleted, userState);
        }
        
        private void OnGetM1M2ByIdOperationCompleted(object arg) {
            if ((this.GetM1M2ByIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetM1M2ByIdCompleted(this, new GetM1M2ByIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://interop.org/GetDocument", RequestNamespace="http://interop.org/", ResponseNamespace="http://interop.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetDocument(string personUidn) {
            object[] results = this.Invoke("GetDocument", new object[] {
                        personUidn});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetDocumentAsync(string personUidn) {
            this.GetDocumentAsync(personUidn, null);
        }
        
        /// <remarks/>
        public void GetDocumentAsync(string personUidn, object userState) {
            if ((this.GetDocumentOperationCompleted == null)) {
                this.GetDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocumentOperationCompleted);
            }
            this.InvokeAsync("GetDocument", new object[] {
                        personUidn}, this.GetDocumentOperationCompleted, userState);
        }
        
        private void OnGetDocumentOperationCompleted(object arg) {
            if ((this.GetDocumentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocumentCompleted(this, new GetDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://interop.org/FullName", RequestNamespace="http://interop.org/", ResponseNamespace="http://interop.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FullName(Person p) {
            object[] results = this.Invoke("FullName", new object[] {
                        p});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FullNameAsync(Person p) {
            this.FullNameAsync(p, null);
        }
        
        /// <remarks/>
        public void FullNameAsync(Person p, object userState) {
            if ((this.FullNameOperationCompleted == null)) {
                this.FullNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFullNameOperationCompleted);
            }
            this.InvokeAsync("FullName", new object[] {
                        p}, this.FullNameOperationCompleted, userState);
        }
        
        private void OnFullNameOperationCompleted(object arg) {
            if ((this.FullNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FullNameCompleted(this, new FullNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://interop.org/")]
    public partial class M1M2 : LogBaseClass {
        
        private int idField;
        
        private int isuranceTypeAvrmIdField;
        
        private System.DateTime checkInDateField;
        
        private System.Nullable<System.DateTime> checkOutDateField;
        
        private System.Nullable<int> checkOutReasonIdField;
        
        private int workingHoursField;
        
        private string occupationNameField;
        
        private System.Nullable<int> occupationIdField;
        
        private System.Nullable<int> authorisedTypeIdField;
        
        private string hiringCompanyField;
        
        private string taxNumberField;
        
        private string companyUinField;
        
        private int companyOrderField;
        
        private string companyNameField;
        
        private string companyAddressField;
        
        private System.Nullable<int> companyPlaceAvrmIdField;
        
        private System.Nullable<System.DateTime> foundingDateField;
        
        private System.Nullable<System.DateTime> terminationDateField;
        
        private System.Nullable<int> companyStatusIdField;
        
        private string companyActivityIdField;
        
        private string companyActivityTypeField;
        
        private string personUidnField;
        
        private string personNameField;
        
        private string personLastNameField;
        
        private string personAddressField;
        
        private System.Nullable<int> personPlaceAvrmIdField;
        
        private string personOfficialAddressField;
        
        private System.Nullable<int> personOfficialPlaceAvrmIdField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public int IsuranceTypeAvrmId {
            get {
                return this.isuranceTypeAvrmIdField;
            }
            set {
                this.isuranceTypeAvrmIdField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime CheckInDate {
            get {
                return this.checkInDateField;
            }
            set {
                this.checkInDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CheckOutDate {
            get {
                return this.checkOutDateField;
            }
            set {
                this.checkOutDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> CheckOutReasonId {
            get {
                return this.checkOutReasonIdField;
            }
            set {
                this.checkOutReasonIdField = value;
            }
        }
        
        /// <remarks/>
        public int WorkingHours {
            get {
                return this.workingHoursField;
            }
            set {
                this.workingHoursField = value;
            }
        }
        
        /// <remarks/>
        public string OccupationName {
            get {
                return this.occupationNameField;
            }
            set {
                this.occupationNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> OccupationId {
            get {
                return this.occupationIdField;
            }
            set {
                this.occupationIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> AuthorisedTypeId {
            get {
                return this.authorisedTypeIdField;
            }
            set {
                this.authorisedTypeIdField = value;
            }
        }
        
        /// <remarks/>
        public string HiringCompany {
            get {
                return this.hiringCompanyField;
            }
            set {
                this.hiringCompanyField = value;
            }
        }
        
        /// <remarks/>
        public string TaxNumber {
            get {
                return this.taxNumberField;
            }
            set {
                this.taxNumberField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyUin {
            get {
                return this.companyUinField;
            }
            set {
                this.companyUinField = value;
            }
        }
        
        /// <remarks/>
        public int CompanyOrder {
            get {
                return this.companyOrderField;
            }
            set {
                this.companyOrderField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyName {
            get {
                return this.companyNameField;
            }
            set {
                this.companyNameField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyAddress {
            get {
                return this.companyAddressField;
            }
            set {
                this.companyAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> CompanyPlaceAvrmId {
            get {
                return this.companyPlaceAvrmIdField;
            }
            set {
                this.companyPlaceAvrmIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> FoundingDate {
            get {
                return this.foundingDateField;
            }
            set {
                this.foundingDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> TerminationDate {
            get {
                return this.terminationDateField;
            }
            set {
                this.terminationDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> CompanyStatusId {
            get {
                return this.companyStatusIdField;
            }
            set {
                this.companyStatusIdField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyActivityId {
            get {
                return this.companyActivityIdField;
            }
            set {
                this.companyActivityIdField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyActivityType {
            get {
                return this.companyActivityTypeField;
            }
            set {
                this.companyActivityTypeField = value;
            }
        }
        
        /// <remarks/>
        public string PersonUidn {
            get {
                return this.personUidnField;
            }
            set {
                this.personUidnField = value;
            }
        }
        
        /// <remarks/>
        public string PersonName {
            get {
                return this.personNameField;
            }
            set {
                this.personNameField = value;
            }
        }
        
        /// <remarks/>
        public string PersonLastName {
            get {
                return this.personLastNameField;
            }
            set {
                this.personLastNameField = value;
            }
        }
        
        /// <remarks/>
        public string PersonAddress {
            get {
                return this.personAddressField;
            }
            set {
                this.personAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> PersonPlaceAvrmId {
            get {
                return this.personPlaceAvrmIdField;
            }
            set {
                this.personPlaceAvrmIdField = value;
            }
        }
        
        /// <remarks/>
        public string PersonOfficialAddress {
            get {
                return this.personOfficialAddressField;
            }
            set {
                this.personOfficialAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> PersonOfficialPlaceAvrmId {
            get {
                return this.personOfficialPlaceAvrmIdField;
            }
            set {
                this.personOfficialPlaceAvrmIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(M1M2))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://interop.org/")]
    public partial class LogBaseClass {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://interop.org/")]
    public partial class Person {
        
        private string firstNameField;
        
        private string surNameField;
        
        /// <remarks/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string SurName {
            get {
                return this.surNameField;
            }
            set {
                this.surNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetM1M2CompletedEventHandler(object sender, GetM1M2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetM1M2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetM1M2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public M1M2[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((M1M2[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetM1M2ByIdCompletedEventHandler(object sender, GetM1M2ByIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetM1M2ByIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetM1M2ByIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public M1M2 Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((M1M2)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetDocumentCompletedEventHandler(object sender, GetDocumentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocumentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocumentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void FullNameCompletedEventHandler(object sender, FullNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FullNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FullNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591