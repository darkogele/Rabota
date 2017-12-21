// Decompiled with JetBrains decompiler
// Type: interop.WSSendEmail.Service1
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace interop.WSSendEmail
{
  [DebuggerStepThrough]
  [WebServiceBinding(Name = "Service1Soap", Namespace = "http://tempuri.org/")]
  [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
  [DesignerCategory("code")]
  public class Service1 : SoapHttpClientProtocol
  {
    private SendOrPostCallback SendMailOperationCompleted;
    private bool useDefaultCredentialsSetExplicitly;

    public new string Url
    {
      get
      {
        return base.Url;
      }
      set
      {
        if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
          base.UseDefaultCredentials = false;
        base.Url = value;
      }
    }

    public new bool UseDefaultCredentials
    {
      get
      {
        return base.UseDefaultCredentials;
      }
      set
      {
        base.UseDefaultCredentials = value;
        this.useDefaultCredentialsSetExplicitly = true;
      }
    }

    public event SendMailCompletedEventHandler SendMailCompleted;

    public Service1()
    {
      this.Url = Settings.Default.interop_WSSendEmail_Service1;
      if (this.IsLocalFileSystemWebService(this.Url))
      {
        this.UseDefaultCredentials = true;
        this.useDefaultCredentialsSetExplicitly = false;
      }
      else
        this.useDefaultCredentialsSetExplicitly = true;
    }

    [SoapDocumentMethod("http://tempuri.org/SendMail", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal)]
    public bool SendMail(string control, string Subject, string Body, string to)
    {
      return (bool) this.Invoke("SendMail", new object[4]
      {
        (object) control,
        (object) Subject,
        (object) Body,
        (object) to
      })[0];
    }

    public void SendMailAsync(string control, string Subject, string Body, string to)
    {
      this.SendMailAsync(control, Subject, Body, to, (object) null);
    }

    public void SendMailAsync(string control, string Subject, string Body, string to, object userState)
    {
      if (this.SendMailOperationCompleted == null)
        this.SendMailOperationCompleted = new SendOrPostCallback(this.OnSendMailOperationCompleted);
      this.InvokeAsync("SendMail", new object[4]
      {
        (object) control,
        (object) Subject,
        (object) Body,
        (object) to
      }, this.SendMailOperationCompleted, userState);
    }

    private void OnSendMailOperationCompleted(object arg)
    {
      if (this.SendMailCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendMailCompleted((object) this, new SendMailCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public new void CancelAsync(object userState)
    {
      base.CancelAsync(userState);
    }

    private bool IsLocalFileSystemWebService(string url)
    {
      if (url == null || url == string.Empty)
        return false;
      Uri uri = new Uri(url);
      return uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}
