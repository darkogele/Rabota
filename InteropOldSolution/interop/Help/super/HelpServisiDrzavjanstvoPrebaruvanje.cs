// Decompiled with JetBrains decompiler
// Type: interop.Help.super.HelpServisiDrzavjanstvoPrebaruvanje
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Web.UI;

namespace interop.Help.super
{
  public class HelpServisiDrzavjanstvoPrebaruvanje : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Session["user"] != null)
      {
        if (((USER) this.Session["user"]).Type == 1)
          return;
        this.Response.Redirect("../OdbienPristap.aspx");
      }
      else
        this.Response.Redirect("Login.aspx");
    }
  }
}
