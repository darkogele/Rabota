using interop;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using WebApplicationInterop;
public class pdfReport
{
    private PdfPTable datatable;
    private Document doc = new Document();
    private DataView _DataViewReport;
    private USER _User;
    private string ime = "";
    private string _PdfFilePath = "";
    private string _Servis;
    private string[] od_datum = new string[3];
    private string[] do_datum = new string[3];
    private string _RowFilter;
    private GridView _GridViewpdf;
    private List<GridView> _GridViewpdf1;
    private List<GridView> _GridViewpdf2;
    private List<GridView> _GridViewpdf3;
    private GridView _GridViewpdf4;
    private GridView _GridViewpdf5;
    private List<GridView> _GridViewpdf6;
    private List<GridView> _GridViewpdf7;
    private string _PdfFileName;
    public ComputedTransaction Transaction
    {
        get;
        set;
    }
    public List<string> ListaRezultHeaders
    {
        get;
        set;
    }
    public DataView DataViewReport
    {
        get
        {
            return this._DataViewReport;
        }
        set
        {
            this._DataViewReport = value;
        }
    }
    public string PdfFileName
    {
        get
        {
            return this._PdfFileName;
        }
        set
        {
            this._PdfFileName = value;
        }
    }
    public string PdfFilePath
    {
        get
        {
            return this._PdfFilePath;
        }
        set
        {
            this._PdfFilePath = value;
        }
    }
    public string Servis
    {
        get
        {
            return this._Servis;
        }
        set
        {
            this._Servis = value;
        }
    }
    public USER User
    {
        get
        {
            return this._User;
        }
        set
        {
            this._User = value;
        }
    }
    public string RowFilter
    {
        get
        {
            return this._RowFilter;
        }
        set
        {
            this._RowFilter = value;
        }
    }
    public string Ime
    {
        get
        {
            return this.ime;
        }
        set
        {
            this.ime = value;
        }
    }
    public GridView GridViewpdf
    {
        get
        {
            return this._GridViewpdf;
        }
        set
        {
            this._GridViewpdf = value;
        }
    }
    public List<GridView> GridViewpdf1
    {
        get
        {
            return this._GridViewpdf1;
        }
        set
        {
            this._GridViewpdf1 = value;
        }
    }
    public List<GridView> GridViewpdf2
    {
        get
        {
            return this._GridViewpdf2;
        }
        set
        {
            this._GridViewpdf2 = value;
        }
    }
    public List<GridView> GridViewpdf3
    {
        get
        {
            return this._GridViewpdf3;
        }
        set
        {
            this._GridViewpdf3 = value;
        }
    }
    public GridView GridViewpdf4
    {
        get
        {
            return this._GridViewpdf4;
        }
        set
        {
            this._GridViewpdf4 = value;
        }
    }
    public GridView GridViewpdf5
    {
        get
        {
            return this._GridViewpdf5;
        }
        set
        {
            this._GridViewpdf5 = value;
        }
    }
    public List<GridView> GridViewpdf6
    {
        get
        {
            return this._GridViewpdf6;
        }
        set
        {
            this._GridViewpdf6 = value;
        }
    }
    public List<GridView> GridViewpdf7
    {
        get
        {
            return this._GridViewpdf7;
        }
        set
        {
            this._GridViewpdf7 = value;
        }
    }
    public pdfReport()
    {
    }
    public pdfReport(string datum)
    {
        Rectangle rectangle = new Rectangle(842f, 595f);
        this.doc.AddAuthor("VISTA");
        this.doc = new Document(rectangle, 20f, 20f, 20f, 20f);
        this.ime = datum;
        this.od_datum = datum.Split(new char[]
		{
			'.'
		});
        this.do_datum = this.od_datum;
    }
    public pdfReport(string _od, string _do)
    {
        Rectangle rectangle = new Rectangle(842f, 595f);
        this.doc.AddAuthor("VISTA");
        this.doc = new Document(rectangle, 20f, 20f, 20f, 20f);
        this.ime = _od + "-" + _do;
        this.od_datum = _od.Split(new char[]
		{
			'.'
		});
        this.do_datum = _do.Split(new char[]
		{
			'.'
		});
    }
    public void generirajReport()
    {
        BaseFont baseFont = BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\gothic.ttf", "Cp1251", true);
        BaseFont baseFont2 = BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\arial.ttf", "Cp1251", true);
        Font font = new Font(baseFont, 14f);
        Font font2 = new Font(baseFont2, 8f);
        try
        {
            CultureInfo provider = new CultureInfo("mk-MK");
            this.PdfFilePath = string.Concat(new string[]
			{
				ConfigurationManager.AppSettings["dir"],
				"izvestai\\",
				this.PdfFileName.ToUpper(),
				"_",
				this.Transaction.TimeResponse.Value.ToString("dd/MM/yyyy_hh:mm:ss", provider).Replace('.', '-').Replace(':', '_'),
				".pdf"
			});
            this.doc.SetPageSize(new Rectangle(600f, 800f));
            PdfWriter instance = PdfWriter.GetInstance(this.doc, new FileStream(this.PdfFilePath, FileMode.Create, FileAccess.ReadWrite));
            instance.SetFullCompression();
            instance.StrictImageSequence=(true);
            instance.SetLinearPageMode();
            this.doc.Open();
            PdfPTable pdfPTable = new PdfPTable(2);
            float[] widths = new float[]
			{
				5f,
				55f
			};
            pdfPTable.WidthPercentage=(85f);
            pdfPTable.SetWidths(widths);
            pdfPTable.DefaultCell.Border=(0);
            Phrase phrase = new Phrase("Извештај за\n" + this.Servis, new Font(baseFont, 30f, 0, new Color(0, 0, 0)));
            PdfPCell pdfPCell = new PdfPCell();
            pdfPCell.Phrase=(phrase);
            pdfPCell.Border=(0);
            pdfPCell.BorderColorBottom=(new Color(0, 0, 0));
            pdfPCell.BorderWidthBottom=(2f);
            pdfPCell.PaddingBottom=(40f);
            pdfPCell.PaddingTop=(80f);
            pdfPTable.HorizontalAlignment=(0);
            PdfPCell pdfPCell2 = new PdfPCell();
            pdfPCell2.Border=(0);
            phrase = new Phrase(this.ime, new Font(baseFont, 32f, 0, new Color(0, 0, 0)));
            PdfPCell pdfPCell3 = new PdfPCell();
            pdfPCell3.Phrase=(phrase);
            pdfPCell3.HorizontalAlignment=(2);
            pdfPCell3.PaddingTop=(20f);
            pdfPCell3.Border=(0);
            PdfPCell pdfPCell4 = new PdfPCell();
            phrase = new Phrase(string.Concat(new string[]
			{
				"Извештајот е креиран од корисникот   \" ",
				this.User.Name,
				" ",
				this.User.Surname,
				" \""
			}), new Font(baseFont, 12f, 0, new Color(0, 0, 0)));
            pdfPCell4.Phrase=(phrase);
            pdfPCell4.HorizontalAlignment=(2);
            pdfPCell4.PaddingTop=(180f);
            pdfPCell4.Border=(0);
            PdfPCell pdfPCell5 = new PdfPCell();
            phrase = new Phrase("Број на трансакција   \" " + this.Transaction.RequestID + " \"", new Font(baseFont, 10f, 0, new Color(0, 0, 0)));
            pdfPCell5.Phrase=(phrase);
            pdfPCell5.HorizontalAlignment=(2);
            pdfPCell5.PaddingTop=(6f);
            pdfPCell5.Border=(0);
            DateTime now = DateTime.Now;
            string format = "dddd, dd MMMM yyyy";
            string str = this.Transaction.TimeRequest.Value.ToString(format);
            PdfPCell pdfPCell6 = new PdfPCell();
            phrase = new Phrase("Време на поднесено барање   \" " + str + " \"", new Font(baseFont, 12f, 0, new Color(0, 0, 0)));
            pdfPCell6.Phrase=(phrase);
            pdfPCell6.HorizontalAlignment=(2);
            pdfPCell6.PaddingTop=(6f);
            pdfPCell6.Border=(0);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell3);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell4);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell5);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell6);
            pdfPTable.AddCell(pdfPCell2);
            this.doc.Add(pdfPTable);
            Font font3 = new Font(baseFont2, 10f, 0, new Color(0, 0, 0));
            HeaderFooter headerFooter = new HeaderFooter(new Phrase(20f, "Извештај за " + this.Servis, font3), false);
            headerFooter.Border=(0);
            headerFooter.SetAlignment("center");
            this.doc.Header=(headerFooter);
            bool flag = this.Transaction.TimeRequest.Value.IsDaylightSavingTime();
            string text = string.Empty;
            if (!flag)
            {
                text = this.Transaction.TimeRequest.Value.ToString() + " UTC+1";
            }
            else
            {
                text = this.Transaction.TimeRequest.Value.ToString() + " UTC+2";
            }
            string text2 = "                  ";
            string text3 = string.Concat(new string[]
			{
				"Број на трансакција   \" ",
				this.Transaction.RequestID,
				" \" ",
				text2,
				"Време на поднесено барање   \" ",
				text,
				" \" ",
				text2,
				text2,
				text2,
				text2,
				text2
			});
            Font font4 = new Font(baseFont2, 8f, 0, new Color(0, 0, 0));
            HeaderFooter headerFooter2 = new HeaderFooter(new Phrase(text3, font4), true);
            headerFooter2.Border=(0);
            headerFooter2.Alignment=(2);
            this.doc.Footer=(headerFooter2);
            DataView dataViewReport = this.DataViewReport;
            this.doc.SetPageSize(new Rectangle(800f, 600f));
            this.doc.NewPage();
            if (this.RowFilter != "")
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                Phrase phrase2 = new Phrase(this.RowFilter, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(40f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
            }
            if (this.GridViewpdf != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                string text4 = this.ListaRezultHeaders[0];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(10f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf, 1).Tabela);
                for (int i = 0; i < this.GridViewpdf.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(1f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf1 != null)
            {
                if (this.GridViewpdf1.Count > 0 && this.GridViewpdf1[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[1];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf1.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf1[k];
                        if (this.GridViewpdf1.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 1).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(1f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf2 != null)
            {
                if (this.GridViewpdf2.Count > 0 && this.GridViewpdf2[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[2];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf2.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf2[k];
                        if (this.GridViewpdf2.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 2).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(1f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf3 != null)
            {
                if (this.GridViewpdf3.Count > 0 && this.GridViewpdf3[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[3];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf3.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf3[k];
                        if (this.GridViewpdf3.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(1f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf4 != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                string text4 = this.ListaRezultHeaders[4];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(10f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf4, 0).Tabela);
                for (int i = 0; i < this.GridViewpdf4.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(1f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf4.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf4.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf4.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf5 != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                string text4 = this.ListaRezultHeaders[5];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(10f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf5, 0).Tabela);
                for (int i = 0; i < this.GridViewpdf5.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(1f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf5.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf5.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf5.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf6 != null)
            {
                if (this.GridViewpdf6.Count > 0 && this.GridViewpdf6[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[6];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf6.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf6[k];
                        if (this.GridViewpdf6.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(1f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf7 != null)
            {
                if (this.GridViewpdf7.Count > 0 && this.GridViewpdf7[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[7];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf7.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf7[k];
                        if (this.GridViewpdf7.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(1f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            this.doc.Close();
        }
        catch (DocumentException var_45_204F)
        {
        }
        catch (IOException var_46_2055)
        {
        }
        this.doc.Close();
    }
    public void generirajReportUJP()
    {
        BaseFont baseFont = BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\gothic.ttf", "Cp1251", true);
        BaseFont baseFont2 = BaseFont.CreateFont(ConfigurationManager.AppSettings["dir"] + "fonts\\arial.ttf", "Cp1251", true);
        Font font = new Font(baseFont, 14f);
        Font font2 = new Font(baseFont2, 8f);
        try
        {
            CultureInfo provider = new CultureInfo("mk-MK");
            this.PdfFilePath = string.Concat(new string[]
			{
				ConfigurationManager.AppSettings["dir"],
				"izvestai\\",
				this.PdfFileName.ToUpper(),
				"_",
				this.Transaction.TimeResponse.Value.ToString("dd/MM/yyyy_hh:mm:ss", provider).Replace('.', '-').Replace(':', '_'),
				".pdf"
			});
            this.doc.SetPageSize(new Rectangle(600f, 800f));
            PdfWriter instance = PdfWriter.GetInstance(this.doc, new FileStream(this.PdfFilePath, FileMode.Create, FileAccess.ReadWrite));
            instance.SetFullCompression();
            instance.StrictImageSequence=(true);
            instance.SetLinearPageMode();
            this.doc.Open();
            PdfPTable pdfPTable = new PdfPTable(2);
            float[] widths = new float[]
			{
				5f,
				55f
			};
            pdfPTable.WidthPercentage=(85f);
            pdfPTable.SetWidths(widths);
            pdfPTable.DefaultCell.Border=(0);
            Phrase phrase = new Phrase("Извештај за\nтековна состојба на субјектот", new Font(baseFont, 30f, 0, new Color(0, 0, 0)));
            PdfPCell pdfPCell = new PdfPCell();
            pdfPCell.Phrase=(phrase);
            pdfPCell.Border=(0);
            pdfPCell.BorderColorBottom=(new Color(0, 0, 0));
            pdfPCell.BorderWidthBottom=(2f);
            pdfPCell.PaddingBottom=(40f);
            pdfPCell.PaddingTop=(80f);
            pdfPTable.HorizontalAlignment=(0);
            PdfPCell pdfPCell2 = new PdfPCell();
            pdfPCell2.Border=(0);
            phrase = new Phrase(this.ime, new Font(baseFont, 32f, 0, new Color(0, 0, 0)));
            PdfPCell pdfPCell3 = new PdfPCell();
            pdfPCell3.Phrase=(phrase);
            pdfPCell3.HorizontalAlignment=(2);
            pdfPCell3.PaddingTop=(20f);
            pdfPCell3.Border=(0);
            PdfPCell pdfPCell4 = new PdfPCell();
            phrase = new Phrase(string.Concat(new string[]
			{
				"Извештајот е креиран од корисникот   \" ",
				this.User.Name,
				" ",
				this.User.Surname,
				" \""
			}), new Font(baseFont, 12f, 0, new Color(0, 0, 0)));
            pdfPCell4.Phrase=(phrase);
            pdfPCell4.HorizontalAlignment=(2);
            pdfPCell4.PaddingTop=(180f);
            pdfPCell4.Border=(0);
            PdfPCell pdfPCell5 = new PdfPCell();
            phrase = new Phrase("Број на трансакција   \" " + this.Transaction.RequestID + " \"", new Font(baseFont, 10f, 0, new Color(0, 0, 0)));
            pdfPCell5.Phrase=(phrase);
            pdfPCell5.HorizontalAlignment=(2);
            pdfPCell5.PaddingTop=(6f);
            pdfPCell5.Border=(0);
            DateTime now = DateTime.Now;
            string format = "dddd, dd MMMM yyyy";
            string str = this.Transaction.TimeRequest.Value.ToString(format);
            PdfPCell pdfPCell6 = new PdfPCell();
            phrase = new Phrase("Време на поднесено барање   \" " + str + " \"", new Font(baseFont, 12f, 0, new Color(0, 0, 0)));
            pdfPCell6.Phrase=(phrase);
            pdfPCell6.HorizontalAlignment=(2);
            pdfPCell6.PaddingTop=(6f);
            pdfPCell6.Border=(0);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell3);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell4);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell5);
            pdfPTable.AddCell(pdfPCell2);
            pdfPTable.AddCell(pdfPCell6);
            pdfPTable.AddCell(pdfPCell2);
            this.doc.Add(pdfPTable);
            Font font3 = new Font(baseFont2, 10f, 0, new Color(0, 0, 0));
            HeaderFooter headerFooter = new HeaderFooter(new Phrase(20f, "Извештај за  тековна состојба на субјектот", font3), false);
            headerFooter.Border = 0;
            headerFooter.SetAlignment("center");
            this.doc.Header = headerFooter;
            bool flag = this.Transaction.TimeRequest.Value.IsDaylightSavingTime();
            string text = string.Empty;
            if (!flag)
            {
                text = this.Transaction.TimeRequest.Value.ToString() + " UTC+1";
            }
            else
            {
                text = this.Transaction.TimeRequest.Value.ToString() + " UTC+2";
            }
            string text2 = "                  ";
            string text3 = string.Concat(new string[]
			{
				"Број на трансакција   \" ",
				this.Transaction.RequestID,
				" \" Време на поднесено барање   \" ",
				text,
				" \" ",
				text2,
				text2,
				text2,
				text2,
				text2
			});
            Font font4 = new Font(baseFont2, 8f, 0, new Color(0, 0, 0));
            HeaderFooter headerFooter2 = new HeaderFooter(new Phrase(text3, font4), true);
            headerFooter2.Border = 0;
            headerFooter2.Alignment = 2;
            this.doc.Footer = headerFooter2;
            DataView dataViewReport = this.DataViewReport;
            this.doc.SetPageSize(new Rectangle(600f, 800f));
            this.doc.NewPage();
            if (this.RowFilter != "")
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage = 85f;
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border = 0;
                Phrase phrase2 = new Phrase(this.RowFilter, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase = phrase2;
                pdfPCell7.Border = 0;
                pdfPCell7.PaddingBottom = 40f;
                pdfPCell7.PaddingTop = 10f;
                pdfPTable2.HorizontalAlignment = 0;
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border = 0;
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
            }
            if (this.GridViewpdf != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage = 85f;
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border =0;
                string text4 = this.ListaRezultHeaders[0];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase = (phrase2);
                pdfPCell7.Border = (0);
                pdfPCell7.PaddingBottom = (10f);
                pdfPCell7.PaddingTop = (10f);
                pdfPTable2.HorizontalAlignment = (0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border =(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf, 1).Tabela);
                for (int i = 0; i < this.GridViewpdf.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(0f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf6 != null)
            {
                if (this.GridViewpdf6.Count > 0 && this.GridViewpdf6[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[6];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf6.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf6[k];
                        if (this.GridViewpdf6.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(0f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf3 != null)
            {
                if (this.GridViewpdf3.Count > 0 && this.GridViewpdf3[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[3];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf3.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf3[k];
                        if (this.GridViewpdf3.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(0f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf2 != null)
            {
                if (this.GridViewpdf2.Count > 0 && this.GridViewpdf2[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[2];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf2.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf2[k];
                        if (this.GridViewpdf2.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(0f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf1 != null)
            {
                if (this.GridViewpdf1.Count > 0 && this.GridViewpdf1[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[1];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf1.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf1[k];
                        if (this.GridViewpdf1.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(0f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            if (this.GridViewpdf4 != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                string text4 = this.ListaRezultHeaders[4];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(10f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf4, 0).Tabela);
                for (int i = 0; i < this.GridViewpdf4.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(0f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf4.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf4.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf4.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf5 != null)
            {
                PdfPTable pdfPTable2 = new PdfPTable(1);
                float[] widths2 = new float[]
				{
					55f
				};
                pdfPTable2.WidthPercentage=(85f);
                pdfPTable2.SetWidths(widths2);
                pdfPTable2.DefaultCell.Border=(0);
                string text4 = this.ListaRezultHeaders[5];
                Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                PdfPCell pdfPCell7 = new PdfPCell();
                pdfPCell7.Phrase=(phrase2);
                pdfPCell7.Border=(0);
                pdfPCell7.PaddingBottom=(10f);
                pdfPCell7.PaddingTop=(10f);
                pdfPTable2.HorizontalAlignment=(0);
                PdfPCell pdfPCell8 = new PdfPCell();
                pdfPCell2.Border=(0);
                pdfPTable2.AddCell(pdfPCell7);
                this.doc.Add(pdfPTable2);
                this.datatable = new PdfPTable(new pdfTabela(this.GridViewpdf5, 0).Tabela);
                for (int i = 0; i < this.GridViewpdf5.Rows.Count; i++)
                {
                    PdfPCell pdfPCell9 = new PdfPCell();
                    pdfPCell9.Border=(0);
                    pdfPCell9.VerticalAlignment=(5);
                    pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                    pdfPCell9.BorderWidthBottom=(0f);
                    pdfPCell9.PaddingBottom=(2f);
                    pdfPCell9.PaddingTop=(2f);
                    pdfPCell9.HorizontalAlignment=(0);
                    for (int j = 0; j < this.GridViewpdf5.HeaderRow.Cells.Count; j++)
                    {
                        if (this.GridViewpdf5.Rows[i].Cells[j].Text == "&nbsp;")
                        {
                            pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                        }
                        else
                        {
                            pdfPCell9.Phrase=(new Phrase(this.GridViewpdf5.Rows[i].Cells[j].Text, font2));
                        }
                        this.datatable.AddCell(pdfPCell9);
                    }
                }
                this.doc.Add(this.datatable);
            }
            if (this.GridViewpdf7 != null)
            {
                if (this.GridViewpdf7.Count > 0 && this.GridViewpdf7[0] != null)
                {
                    PdfPTable pdfPTable3 = new PdfPTable(1);
                    float[] widths3 = new float[]
					{
						55f
					};
                    pdfPTable3.WidthPercentage=(85f);
                    pdfPTable3.SetWidths(widths3);
                    pdfPTable3.DefaultCell.Border=(0);
                    string text5 = this.ListaRezultHeaders[7];
                    Phrase phrase3 = new Phrase(text5, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                    PdfPCell pdfPCell10 = new PdfPCell();
                    pdfPCell10.Phrase=(phrase3);
                    pdfPCell10.Border=(0);
                    pdfPCell10.PaddingBottom=(10f);
                    pdfPCell10.PaddingTop=(10f);
                    pdfPTable3.HorizontalAlignment=(0);
                    PdfPCell pdfPCell11 = new PdfPCell();
                    pdfPCell2.Border=(0);
                    pdfPTable3.AddCell(pdfPCell10);
                    this.doc.Add(pdfPTable3);
                    for (int k = 0; k < this.GridViewpdf7.Count; k++)
                    {
                        GridView gridView = this.GridViewpdf7[k];
                        if (this.GridViewpdf7.Count > 1)
                        {
                            PdfPTable pdfPTable2 = new PdfPTable(1);
                            float[] widths2 = new float[]
							{
								55f
							};
                            pdfPTable2.WidthPercentage=(85f);
                            pdfPTable2.SetWidths(widths2);
                            pdfPTable2.DefaultCell.Border=(0);
                            string text4 = "Запис " + k.ToString();
                            Phrase phrase2 = new Phrase(text4, new Font(baseFont, 14f, 0, new Color(0, 0, 0)));
                            PdfPCell pdfPCell7 = new PdfPCell();
                            pdfPCell7.Phrase=(phrase2);
                            pdfPCell7.Border=(0);
                            pdfPCell7.PaddingBottom=(10f);
                            pdfPCell7.PaddingTop=(10f);
                            pdfPTable2.HorizontalAlignment=(0);
                            PdfPCell pdfPCell8 = new PdfPCell();
                            pdfPCell2.Border=(0);
                            pdfPTable2.AddCell(pdfPCell7);
                            this.doc.Add(pdfPTable2);
                        }
                        this.datatable = new PdfPTable(new pdfTabela(gridView, 0).Tabela);
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            PdfPCell pdfPCell9 = new PdfPCell();
                            pdfPCell9.Border=(0);
                            pdfPCell9.VerticalAlignment=(5);
                            pdfPCell9.BorderColorBottom=(new Color(0, 0, 0));
                            pdfPCell9.BorderWidthBottom=(0f);
                            pdfPCell9.PaddingBottom=(2f);
                            pdfPCell9.PaddingTop=(2f);
                            pdfPCell9.HorizontalAlignment=(0);
                            for (int j = 0; j < gridView.HeaderRow.Cells.Count; j++)
                            {
                                if (gridView.Rows[i].Cells[j].Text == "&nbsp;")
                                {
                                    pdfPCell9.Phrase=(new Phrase(string.Empty, font2));
                                }
                                else
                                {
                                    pdfPCell9.Phrase=(new Phrase(gridView.Rows[i].Cells[j].Text, font2));
                                }
                                this.datatable.AddCell(pdfPCell9);
                            }
                        }
                        this.doc.Add(this.datatable);
                    }
                }
            }
            this.doc.Close();
        }
        catch (DocumentException var_45_2014)
        {
        }
        catch (IOException var_46_201A)
        {
        }
        this.doc.Close();
    }
}
