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

namespace Interop.CS.Portal.API.KIBSref {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="wsTSATestSoap", Namespace="http://kibs.com.mk/wsTSATest")]
    public partial class wsTSATest : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback funGenerateTS_BytesOperationCompleted;
        
        private System.Threading.SendOrPostCallback funGenerateTS_StringOperationCompleted;
        
        private System.Threading.SendOrPostCallback funCheckTS_BytesOperationCompleted;
        
        private System.Threading.SendOrPostCallback funCheckTS_StringOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsTSATest() {
            this.Url = global::Interop.CS.Portal.API.Properties.Settings.Default.Interop_CS_Portal_API_KIBSref_wsTSATest;
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
        public event funGenerateTS_BytesCompletedEventHandler funGenerateTS_BytesCompleted;
        
        /// <remarks/>
        public event funGenerateTS_StringCompletedEventHandler funGenerateTS_StringCompleted;
        
        /// <remarks/>
        public event funCheckTS_BytesCompletedEventHandler funCheckTS_BytesCompleted;
        
        /// <remarks/>
        public event funCheckTS_StringCompletedEventHandler funCheckTS_StringCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://kibs.com.mk/wsTSATest/funGenerateTS_Bytes", RequestNamespace="http://kibs.com.mk/wsTSATest", ResponseNamespace="http://kibs.com.mk/wsTSATest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TSResponse_Bytes funGenerateTS_Bytes([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] bytHash) {
            object[] results = this.Invoke("funGenerateTS_Bytes", new object[] {
                        bytHash});
            return ((TSResponse_Bytes)(results[0]));
        }
        
        /// <remarks/>
        public void funGenerateTS_BytesAsync(byte[] bytHash) {
            this.funGenerateTS_BytesAsync(bytHash, null);
        }
        
        /// <remarks/>
        public void funGenerateTS_BytesAsync(byte[] bytHash, object userState) {
            if ((this.funGenerateTS_BytesOperationCompleted == null)) {
                this.funGenerateTS_BytesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfunGenerateTS_BytesOperationCompleted);
            }
            this.InvokeAsync("funGenerateTS_Bytes", new object[] {
                        bytHash}, this.funGenerateTS_BytesOperationCompleted, userState);
        }
        
        private void OnfunGenerateTS_BytesOperationCompleted(object arg) {
            if ((this.funGenerateTS_BytesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.funGenerateTS_BytesCompleted(this, new funGenerateTS_BytesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://kibs.com.mk/wsTSATest/funGenerateTS_String", RequestNamespace="http://kibs.com.mk/wsTSATest", ResponseNamespace="http://kibs.com.mk/wsTSATest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TSResponse_String funGenerateTS_String(string strBase64Hash) {
            object[] results = this.Invoke("funGenerateTS_String", new object[] {
                        strBase64Hash});
            return ((TSResponse_String)(results[0]));
        }
        
        /// <remarks/>
        public void funGenerateTS_StringAsync(string strBase64Hash) {
            this.funGenerateTS_StringAsync(strBase64Hash, null);
        }
        
        /// <remarks/>
        public void funGenerateTS_StringAsync(string strBase64Hash, object userState) {
            if ((this.funGenerateTS_StringOperationCompleted == null)) {
                this.funGenerateTS_StringOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfunGenerateTS_StringOperationCompleted);
            }
            this.InvokeAsync("funGenerateTS_String", new object[] {
                        strBase64Hash}, this.funGenerateTS_StringOperationCompleted, userState);
        }
        
        private void OnfunGenerateTS_StringOperationCompleted(object arg) {
            if ((this.funGenerateTS_StringCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.funGenerateTS_StringCompleted(this, new funGenerateTS_StringCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://kibs.com.mk/wsTSATest/funCheckTS_Bytes", RequestNamespace="http://kibs.com.mk/wsTSATest", ResponseNamespace="http://kibs.com.mk/wsTSATest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TSCheck_Bytes funCheckTS_Bytes([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] bytTSToken) {
            object[] results = this.Invoke("funCheckTS_Bytes", new object[] {
                        bytTSToken});
            return ((TSCheck_Bytes)(results[0]));
        }
        
        /// <remarks/>
        public void funCheckTS_BytesAsync(byte[] bytTSToken) {
            this.funCheckTS_BytesAsync(bytTSToken, null);
        }
        
        /// <remarks/>
        public void funCheckTS_BytesAsync(byte[] bytTSToken, object userState) {
            if ((this.funCheckTS_BytesOperationCompleted == null)) {
                this.funCheckTS_BytesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfunCheckTS_BytesOperationCompleted);
            }
            this.InvokeAsync("funCheckTS_Bytes", new object[] {
                        bytTSToken}, this.funCheckTS_BytesOperationCompleted, userState);
        }
        
        private void OnfunCheckTS_BytesOperationCompleted(object arg) {
            if ((this.funCheckTS_BytesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.funCheckTS_BytesCompleted(this, new funCheckTS_BytesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://kibs.com.mk/wsTSATest/funCheckTS_String", RequestNamespace="http://kibs.com.mk/wsTSATest", ResponseNamespace="http://kibs.com.mk/wsTSATest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public TSCheck_String funCheckTS_String(string strBase64TSToken) {
            object[] results = this.Invoke("funCheckTS_String", new object[] {
                        strBase64TSToken});
            return ((TSCheck_String)(results[0]));
        }
        
        /// <remarks/>
        public void funCheckTS_StringAsync(string strBase64TSToken) {
            this.funCheckTS_StringAsync(strBase64TSToken, null);
        }
        
        /// <remarks/>
        public void funCheckTS_StringAsync(string strBase64TSToken, object userState) {
            if ((this.funCheckTS_StringOperationCompleted == null)) {
                this.funCheckTS_StringOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfunCheckTS_StringOperationCompleted);
            }
            this.InvokeAsync("funCheckTS_String", new object[] {
                        strBase64TSToken}, this.funCheckTS_StringOperationCompleted, userState);
        }
        
        private void OnfunCheckTS_StringOperationCompleted(object arg) {
            if ((this.funCheckTS_StringCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.funCheckTS_StringCompleted(this, new funCheckTS_StringCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://kibs.com.mk/wsTSATest")]
    public partial class TSResponse_Bytes {
        
        private string strFailureInfoField;
        
        private byte[] bytTSTokenField;
        
        private string strCenaUslugaField;
        
        /// <remarks/>
        public string strFailureInfo {
            get {
                return this.strFailureInfoField;
            }
            set {
                this.strFailureInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] bytTSToken {
            get {
                return this.bytTSTokenField;
            }
            set {
                this.bytTSTokenField = value;
            }
        }
        
        /// <remarks/>
        public string strCenaUsluga {
            get {
                return this.strCenaUslugaField;
            }
            set {
                this.strCenaUslugaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://kibs.com.mk/wsTSATest")]
    public partial class TSCheck_String {
        
        private string strSignatureIntegrityField;
        
        private string strTSACertSubjectField;
        
        private string strTSACertValidFromToField;
        
        private string strCertificateStatusField;
        
        private string strUTCTimeField;
        
        private string strHashAlgorithmField;
        
        private string strBase64HashMessageField;
        
        private string strFailureInfoField;
        
        private string strCenaUslugaField;
        
        /// <remarks/>
        public string strSignatureIntegrity {
            get {
                return this.strSignatureIntegrityField;
            }
            set {
                this.strSignatureIntegrityField = value;
            }
        }
        
        /// <remarks/>
        public string strTSACertSubject {
            get {
                return this.strTSACertSubjectField;
            }
            set {
                this.strTSACertSubjectField = value;
            }
        }
        
        /// <remarks/>
        public string strTSACertValidFromTo {
            get {
                return this.strTSACertValidFromToField;
            }
            set {
                this.strTSACertValidFromToField = value;
            }
        }
        
        /// <remarks/>
        public string strCertificateStatus {
            get {
                return this.strCertificateStatusField;
            }
            set {
                this.strCertificateStatusField = value;
            }
        }
        
        /// <remarks/>
        public string strUTCTime {
            get {
                return this.strUTCTimeField;
            }
            set {
                this.strUTCTimeField = value;
            }
        }
        
        /// <remarks/>
        public string strHashAlgorithm {
            get {
                return this.strHashAlgorithmField;
            }
            set {
                this.strHashAlgorithmField = value;
            }
        }
        
        /// <remarks/>
        public string strBase64HashMessage {
            get {
                return this.strBase64HashMessageField;
            }
            set {
                this.strBase64HashMessageField = value;
            }
        }
        
        /// <remarks/>
        public string strFailureInfo {
            get {
                return this.strFailureInfoField;
            }
            set {
                this.strFailureInfoField = value;
            }
        }
        
        /// <remarks/>
        public string strCenaUsluga {
            get {
                return this.strCenaUslugaField;
            }
            set {
                this.strCenaUslugaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://kibs.com.mk/wsTSATest")]
    public partial class TSCheck_Bytes {
        
        private string strSignatureIntegrityField;
        
        private string strTSACertSubjectField;
        
        private string strTSACertValidFromToField;
        
        private string strCertificateStatusField;
        
        private string strUTCTimeField;
        
        private string strHashAlgorithmField;
        
        private byte[] bytHashMessageField;
        
        private string strFailureInfoField;
        
        private string strCenaUslugaField;
        
        /// <remarks/>
        public string strSignatureIntegrity {
            get {
                return this.strSignatureIntegrityField;
            }
            set {
                this.strSignatureIntegrityField = value;
            }
        }
        
        /// <remarks/>
        public string strTSACertSubject {
            get {
                return this.strTSACertSubjectField;
            }
            set {
                this.strTSACertSubjectField = value;
            }
        }
        
        /// <remarks/>
        public string strTSACertValidFromTo {
            get {
                return this.strTSACertValidFromToField;
            }
            set {
                this.strTSACertValidFromToField = value;
            }
        }
        
        /// <remarks/>
        public string strCertificateStatus {
            get {
                return this.strCertificateStatusField;
            }
            set {
                this.strCertificateStatusField = value;
            }
        }
        
        /// <remarks/>
        public string strUTCTime {
            get {
                return this.strUTCTimeField;
            }
            set {
                this.strUTCTimeField = value;
            }
        }
        
        /// <remarks/>
        public string strHashAlgorithm {
            get {
                return this.strHashAlgorithmField;
            }
            set {
                this.strHashAlgorithmField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] bytHashMessage {
            get {
                return this.bytHashMessageField;
            }
            set {
                this.bytHashMessageField = value;
            }
        }
        
        /// <remarks/>
        public string strFailureInfo {
            get {
                return this.strFailureInfoField;
            }
            set {
                this.strFailureInfoField = value;
            }
        }
        
        /// <remarks/>
        public string strCenaUsluga {
            get {
                return this.strCenaUslugaField;
            }
            set {
                this.strCenaUslugaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://kibs.com.mk/wsTSATest")]
    public partial class TSResponse_String {
        
        private string strFailureInfoField;
        
        private string strBase64TSTokenField;
        
        private string strCenaUslugaField;
        
        /// <remarks/>
        public string strFailureInfo {
            get {
                return this.strFailureInfoField;
            }
            set {
                this.strFailureInfoField = value;
            }
        }
        
        /// <remarks/>
        public string strBase64TSToken {
            get {
                return this.strBase64TSTokenField;
            }
            set {
                this.strBase64TSTokenField = value;
            }
        }
        
        /// <remarks/>
        public string strCenaUsluga {
            get {
                return this.strCenaUslugaField;
            }
            set {
                this.strCenaUslugaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void funGenerateTS_BytesCompletedEventHandler(object sender, funGenerateTS_BytesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class funGenerateTS_BytesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal funGenerateTS_BytesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TSResponse_Bytes Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TSResponse_Bytes)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void funGenerateTS_StringCompletedEventHandler(object sender, funGenerateTS_StringCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class funGenerateTS_StringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal funGenerateTS_StringCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TSResponse_String Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TSResponse_String)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void funCheckTS_BytesCompletedEventHandler(object sender, funCheckTS_BytesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class funCheckTS_BytesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal funCheckTS_BytesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TSCheck_Bytes Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TSCheck_Bytes)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void funCheckTS_StringCompletedEventHandler(object sender, funCheckTS_StringCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class funCheckTS_StringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal funCheckTS_StringCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public TSCheck_String Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((TSCheck_String)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591