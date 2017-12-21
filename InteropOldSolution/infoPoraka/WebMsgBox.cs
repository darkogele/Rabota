// Decompiled with JetBrains decompiler
// Type: infoPoraka.WebMsgBox
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;

namespace infoPoraka
{
  public class WebMsgBox
  {
    protected static Hashtable handlerPages = new Hashtable();

    private WebMsgBox()
    {
    }

    public static void Show(string Message)
    {
      if (!WebMsgBox.handlerPages.Contains((object) HttpContext.Current.Handler))
      {
        Page page = (Page) HttpContext.Current.Handler;
        if (page == null)
          return;
        Queue queue = new Queue();
        queue.Enqueue((object) Message);
        WebMsgBox.handlerPages.Add((object) HttpContext.Current.Handler, (object) queue);
        page.Unload += new EventHandler(WebMsgBox.CurrentPageUnload);
      }
      else
        ((Queue) WebMsgBox.handlerPages[(object) HttpContext.Current.Handler]).Enqueue((object) Message);
    }

    private static void CurrentPageUnload(object sender, EventArgs e)
    {
      Queue queue = (Queue) WebMsgBox.handlerPages[(object) HttpContext.Current.Handler];
      if (queue == null)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      int count = queue.Count;
      stringBuilder.Append("<script language='javascript'>");
      while (count > 0)
      {
        --count;
        string str = Convert.ToString(queue.Dequeue()).Replace("\"", "'");
        stringBuilder.Append("alert( \"" + str + "\" );");
      }
      stringBuilder.Append("</script>");
      WebMsgBox.handlerPages.Remove((object) HttpContext.Current.Handler);
      HttpContext.Current.Response.Write(stringBuilder.ToString());
    }
  }
}
