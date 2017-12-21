// Decompiled with JetBrains decompiler
// Type: interop.WSSendEmail.SendMailCompletedEventArgs
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace interop.WSSendEmail
{
  [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
  [DesignerCategory("code")]
  [DebuggerStepThrough]
  public class SendMailCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    public bool Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (bool) this.results[0];
      }
    }

    internal SendMailCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }
  }
}
