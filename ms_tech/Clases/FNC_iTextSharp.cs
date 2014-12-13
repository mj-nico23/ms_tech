using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ms_tech.Clases
{
    public class FNC_iTextSharp
    {
        #region Fuentes
        static BaseFont bf1 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false);
        static BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

        static Font fTimes7 = new Font(bfTimes, 7, Font.NORMAL, BaseColor.BLACK);
        static Font fTimes8 = new Font(bfTimes, 8, Font.NORMAL, BaseColor.BLACK);
        static Font fTimes8b = new Font(bf1, 8, Font.NORMAL, BaseColor.BLACK);
        static Font fTimes10 = new Font(bfTimes, 10, Font.NORMAL, BaseColor.BLACK);
        static Font fTimes10b = new Font(bf1, 10, Font.NORMAL, BaseColor.BLACK);
        static Font fTimes11b = new Font(bf1, 11, Font.NORMAL, BaseColor.BLACK);

        static Font fArial6 = FontFactory.GetFont("Arial", 6);
        static Font fArial7 = FontFactory.GetFont("Arial", 7);
        static Font fArial8 = FontFactory.GetFont("Arial", 8);
        static Font fArial8u = FontFactory.GetFont("Arial", 8, Font.UNDERLINE, BaseColor.BLACK);
        static Font fArial8b = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);
        static Font fArial9 = FontFactory.GetFont("Arial", 9);
        static Font fArial9u = FontFactory.GetFont("Arial", 9, Font.UNDERLINE);
        static Font fArial9b = FontFactory.GetFont("Arial", 9, Font.BOLD, BaseColor.BLACK);
        static Font fArial9bu = FontFactory.GetFont("Arial", 9, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
        static Font fArial10 = FontFactory.GetFont("Arial", 10);
        static Font fArial10b = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
        static Font fArial10u = FontFactory.GetFont("Arial", 10, Font.UNDERLINE, BaseColor.BLACK);
        static Font fArial10bu = FontFactory.GetFont("Arial", 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
        static Font fArial11b = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK);
        static Font fArial11bu = FontFactory.GetFont("Arial", 11, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
        static Font fArial12b = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
        static Font fArial16b = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
        static Font fArial20b = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK);
        static Font fArial36b = FontFactory.GetFont("Arial", 36, Font.BOLD, BaseColor.BLACK);
        #endregion

        public enum Fuente
        {
            fTimes7,
            fTimes8,
            fTimes8b,
            fTimes10,
            fTimes10b,
            fTimes11b,
            fArial6,
            fArial7,
            fArial8,
            fArial8u,
            fArial8b,
            fArial9,
            fArial9u,
            fArial9b,
            fArial9bu,
            fArial10,
            fArial10b,
            fArial10u,
            fArial10bu,
            fArial11b,
            fArial11bu,
            fArial12b,
            fArial16b,
            fArial20b,
            fArial36b
        }

        #region Metodos Auxiliares

        public static PdfPCell GetCell(string strTexto, Fuente f, int align)
        {
            return GetCell(strTexto, f, align, null);
        }

        public static PdfPCell GetCell(string strTexto, Fuente f, int align, BaseColor color)
        {
            return GetCell(strTexto, f, align, color, 0);
        }

        public static PdfPCell GetCell(string strTexto, Fuente f, int align, BaseColor color, int colspan)
        {
            return GetCell(strTexto, f, align, color, colspan, 0, 0, 0, 0);
        }

        public static PdfPCell GetCell(string strTexto, Fuente f, int align, BaseColor color, int colspan, int borderTop, int borderLeft, int borderBotton, int borderRight)
        {
            Font selFont = GetFont(f);

            PdfPCell cell = new PdfPCell(new Phrase(strTexto, selFont));
            cell.HorizontalAlignment = align;
            if (color != null)
                cell.BackgroundColor = color;
            cell.Colspan = colspan;

            cell.BorderWidthTop = borderTop;
            cell.BorderWidthLeft = borderLeft;
            cell.BorderWidthBottom = borderBotton;
            cell.BorderWidthRight = borderRight;

            return cell;
        }

        public static PdfPCell GetCell(Phrase pharse, int align, BaseColor color, int colspan, int borderTop, int borderLeft, int borderBotton, int borderRight)
        {
            PdfPCell cell = new PdfPCell(pharse);
            cell.HorizontalAlignment = align;
            if (color != null)
                cell.BackgroundColor = color;
            cell.Colspan = colspan;

            cell.BorderWidthTop = borderTop;
            cell.BorderWidthLeft = borderLeft;
            cell.BorderWidthBottom = borderBotton;
            cell.BorderWidthRight = borderRight;

            return cell;
        }

        public static Font GetFont(Fuente f)
        {
            Font selFont = null;
            switch (f)
            {
                case Fuente.fTimes7:
                    selFont = fTimes7;
                    break;
                case Fuente.fTimes8:
                    selFont = fTimes8;
                    break;
                case Fuente.fTimes8b:
                    selFont = fTimes8b;
                    break;
                case Fuente.fTimes10:
                    selFont = fTimes10;
                    break;
                case Fuente.fTimes10b:
                    selFont = fTimes10b;
                    break;
                case Fuente.fTimes11b:
                    selFont = fTimes11b;
                    break;
                case Fuente.fArial6:
                    selFont = fArial6;
                    break;
                case Fuente.fArial7:
                    selFont = fArial7;
                    break;
                case Fuente.fArial8:
                    selFont = fArial8;
                    break;
                case Fuente.fArial8u:
                    selFont = fArial8u;
                    break;
                case Fuente.fArial8b:
                    selFont = fArial8b;
                    break;
                case Fuente.fArial9:
                    selFont = fArial9;
                    break;
                case Fuente.fArial9b:
                    selFont = fArial9b;
                    break;
                case Fuente.fArial9u:
                    selFont = fArial9u;
                    break;
                case Fuente.fArial9bu:
                    selFont = fArial9bu;
                    break;
                case Fuente.fArial10:
                    selFont = fArial10;
                    break;
                case Fuente.fArial10b:
                    selFont = fArial10b;
                    break;
                case Fuente.fArial10u:
                    selFont = fArial10u;
                    break;
                case Fuente.fArial10bu:
                    selFont = fArial10bu;
                    break;
                case Fuente.fArial11b:
                    selFont = fArial11b;
                    break;
                case Fuente.fArial11bu:
                    selFont = fArial11bu;
                    break;
                case Fuente.fArial12b:
                    selFont = fArial12b;
                    break;
                case Fuente.fArial16b:
                    selFont = fArial16b;
                    break;
                case Fuente.fArial20b:
                    selFont = fArial20b;
                    break;
                case Fuente.fArial36b:
                    selFont = fArial36b;
                    break;
                default:
                    break;
            }

            return selFont;
        }
        #endregion
    }

    public static class StringExtensions
    {
        public static string Left(this string s, int left)
        {
            if (s.Length > left)
                return s.Substring(0, left);

            return s;
        }
    }
}