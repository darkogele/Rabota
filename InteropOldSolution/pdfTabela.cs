// Decompiled with JetBrains decompiler
// Type: pdfTabela
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Web.UI.WebControls;

internal class pdfTabela
{
  private PdfPTable datatable = new PdfPTable(11);

  public PdfPTable Tabela
  {
    get
    {
      return this.datatable;
    }
    set
    {
    }
  }

  public pdfTabela(GridView GridViewDanocenBroj, int tip)
  {
    this.datatable = new PdfPTable(GridViewDanocenBroj.HeaderRow.Cells.Count);
    this.datatable.WidthPercentage=(100f);
    this.datatable.DefaultCell.Padding=(1f);
    string str = ConfigurationManager.AppSettings["dir"] + "fonts\\gothic.ttf";
    BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\gothic.ttf", "Cp1251", true);
    Font font = new Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\arial.ttf", "Cp1251", true), 12f);
    float[] numArray = new float[GridViewDanocenBroj.HeaderRow.Cells.Count];
    for (int index = 0; index < GridViewDanocenBroj.HeaderRow.Cells.Count; ++index)
      numArray[index] = 10f;
    if (numArray.Length == 2)
      numArray[1] = 20f;
    if (tip == 1)
    {
      if (numArray.Length == 11)
      {
        numArray[0] = 12f;
        numArray[1] = 7f;
        numArray[2] = 15f;
        numArray[3] = 15f;
        numArray[4] = 15f;
        numArray[5] = 5f;
        numArray[6] = 5f;
        numArray[7] = 5f;
        numArray[8] = 7f;
        numArray[9] = 9f;
      }
      if (numArray.Length == 10)
      {
        numArray[0] = 9f;
        numArray[5] = 5f;
        numArray[6] = 6f;
        numArray[7] = 7f;
        numArray[8] = 14f;
      }
    }
    if (tip == 2 && numArray.Length == 8)
    {
      numArray[1] = 15f;
      numArray[5] = 8f;
      numArray[7] = 15f;
    }
    this.datatable.SetWidths(numArray);
    ((Rectangle) this.datatable.DefaultCell).Border=(0);
    this.datatable.DefaultCell.HorizontalAlignment=(0);
    this.datatable.HeaderRows=(1);
    PdfPCell pdfPcell = new PdfPCell();
    ((Rectangle) pdfPcell).Border=(0);
    ((Rectangle) pdfPcell).BorderColorBottom=(new Color(100, 100, 100));
    ((Rectangle) pdfPcell).BorderWidthBottom=(2f);
    pdfPcell.PaddingBottom=(3f);
    pdfPcell.PaddingTop=(3f);
    ((Rectangle) pdfPcell).BorderColorTop=(new Color(100, 100, 100));
    ((Rectangle) pdfPcell).BorderWidthTop=(2f);
    ((Rectangle) pdfPcell).BackgroundColor=(new Color(230, 240, 250));
    for (int index = 0; index < GridViewDanocenBroj.HeaderRow.Cells.Count; ++index)
    {
      pdfPcell.Phrase=(new Phrase(GridViewDanocenBroj.HeaderRow.Cells[index].Text, font));
      this.datatable.AddCell(pdfPcell);
    }
  }
}
