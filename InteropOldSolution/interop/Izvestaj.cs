// Decompiled with JetBrains decompiler
// Type: interop.Izvestaj
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WebApplicationInterop;

namespace interop
{
  public class Izvestaj : Page
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Session["user"] == null)
        return;
      if (this.Session["ReportIme"] != null)
      {
        this.Response.ContentType = "application/pdf";
        this.Response.AppendHeader("Content-Disposition", "inline;filename=" + new Transcription().DoTranscription(this.Session["ParametersPrint"].ToString().Replace(' ', '_')).ToUpper());
        this.Response.TransmitFile(this.Session["PdfFilePath"].ToString());
        this.Session["PdfFilePath"] = this.Session["ReportIme"] = (object) null;
        this.Response.Flush();
        this.Response.Close();
      }
      else
        this.Response.Redirect("Login.aspx");
    }
  }
}
