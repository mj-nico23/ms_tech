using System;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ms_tech.Clases
{
    public class PdfEventsGenerico : PdfPageEventHelper
    {
        int pages = 1; //contador paginas
        public string Titulo { get; set; }
        public string Titulo1 { get; set; }
        public string Usuario { get; set; }
        public string Municipalidad { get; set; }
        public string Logo { get; set; }
        public string txt2 { get; set; }
        public string txt3 { get; set; }
        public DataTable dt { get; set; }
        public DataTable dtTrans { get; set; }
        public string MD5 { get; set; }

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate template;

        BaseFont arial_regular_base = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        Font arial_regular;

        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
            pages = 1;

            //Paths to our font files
            var arial_regular_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");

            //Create base fonts
            arial_regular_base = BaseFont.CreateFont(arial_regular_path, BaseFont.IDENTITY_H, false);

            //Create sized-fonts using the bases above
            arial_regular = new iTextSharp.text.Font(arial_regular_base, 7);
            try
            {
                PrintTime = DateTime.Now;
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Font fArial12b = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
            Font fArial10 = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);

            #region Header
            PdfPTable tableHeader = new PdfPTable(4);
            float cellHeight = document.TopMargin;
            Rectangle page = document.PageSize;
            tableHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tableHeader.HorizontalAlignment = 0;
            tableHeader.SetWidths(new int[4] { 15, 35, 25, 25 });

            PdfPCell cellLogo = new PdfPCell();
            //cellLogo.Rowspan = 3;
            cellLogo.HorizontalAlignment = 0;
            cellLogo.Border = 0;
            //cellLogo.Rowspan = 2;
            try
            {
                iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(Logo);
                float fH = 90;
                float fW = 60;
                gif.ScaleToFit(fH, fW);
                cellLogo.AddElement(gif);
            }
            catch (Exception) { }


            tableHeader.AddCell(cellLogo);

            PdfPCell celltxt1 = new PdfPCell(tableHeader.DefaultCell);

            Phrase phrase = new Phrase();
            phrase.Add(new Chunk(Municipalidad + "\n", FNC_iTextSharp.GetFont(FNC_iTextSharp.Fuente.fArial12b)));
            phrase.Add(new Chunk(txt2 + "\n" + txt3, FNC_iTextSharp.GetFont(FNC_iTextSharp.Fuente.fArial10)));

            celltxt1.Border = 0;
            celltxt1.AddElement(phrase);
            celltxt1.Colspan = 3;
            celltxt1.HorizontalAlignment = 0;
            tableHeader.AddCell(celltxt1);

            tableHeader.AddCell(FNC_iTextSharp.GetCell(Titulo, FNC_iTextSharp.Fuente.fArial11b, 1, null, 4));
            tableHeader.AddCell(FNC_iTextSharp.GetCell(Titulo1, FNC_iTextSharp.Fuente.fArial11b, 1, null, 4));
            tableHeader.AddCell(FNC_iTextSharp.GetCell("", FNC_iTextSharp.Fuente.fTimes11b, 1, null, 4));
            tableHeader.AddCell(FNC_iTextSharp.GetCell("", FNC_iTextSharp.Fuente.fTimes11b, 1, null, 4));


            tableHeader.WriteSelectedRows(0, -1, 40, page.Height - cellHeight + tableHeader.TotalHeight, writer.DirectContent);
            #endregion

        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            #region Footer
            PdfPTable tabFot = new PdfPTable(3);
            tabFot.SetWidths(new int[3] { 8, 46, 46 });
            tabFot.HorizontalAlignment = 1;
            tabFot.TotalWidth = document.PageSize.Width - 50;


            tabFot.AddCell(FNC_iTextSharp.GetCell("", FNC_iTextSharp.Fuente.fArial10, 0));

            tabFot.WriteSelectedRows(0, -1, 0, document.Bottom + tabFot.TotalHeight - 30, writer.DirectContent);

            string text = "Página " + pages.ToString() + " de ";

            float len = 0;

            if (arial_regular_base != null)
                len = arial_regular_base.GetWidthPoint(text, 9);

            cb.BeginText();
            cb.SetFontAndSize(arial_regular_base, 9);
            cb.SetTextMatrix(document.PageSize.GetRight(56) - len, document.PageSize.GetBottom(25));
            cb.ShowText(text);
            cb.EndText();

            cb.AddTemplate(template, document.PageSize.GetRight(56), document.PageSize.GetBottom(25));

            pages++;
            #endregion
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            template.BeginText();
            template.SetFontAndSize(arial_regular_base, 9);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1).ToString());
            template.EndText();
        }
    }

}