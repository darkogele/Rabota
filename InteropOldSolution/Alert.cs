// Decompiled with JetBrains decompiler
// Type: Alert
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.Web;
using System.Web.UI;

public static class Alert
{
  public static void Show(string message)
  {
    string script = "<script type=\"text/javascript\">alert('" + message.Replace("'", "\\'") + "');</script>";
    Page page = HttpContext.Current.CurrentHandler as Page;
    if (page == null || page.ClientScript.IsClientScriptBlockRegistered("alert"))
      return;
    page.ClientScript.RegisterClientScriptBlock(typeof (Alert), "alert", script);
  }
}
