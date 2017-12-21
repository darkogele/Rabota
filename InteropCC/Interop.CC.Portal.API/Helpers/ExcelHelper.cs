using System;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Interop.CC.Portal.API.Helpers
{
    public class ExcelHelper
    {
        public static void SetBordersAndHeaderBckColor(ExcelRange cell, bool setBckColor)
        {
            if (setBckColor)
            {
                Color headersBckColor = Color.FromArgb(141, 180, 226);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(headersBckColor);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, headersBckColor);
            }
            Color bordersColor = Color.FromArgb(0, 0, 0);

            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, bordersColor);
        }

        public static void SetExcelDescription(ExcelWorksheet worksheet, DateTime? fromDate, DateTime? toDate)
        {
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            Color fontColorH = Color.FromArgb(173, 128, 255);
            worksheet.Cells["A1:G2"].Merge = true;
            if (fromDate != null)
                if (toDate != null)
                    worksheet.Cells["A1"].Value = "Повикување на сервиси во период " + fromDate.Value.ToString("dd.MM.yyyy", macCultureInfo) + " - " + toDate.Value.ToString("dd.MM.yyyy", macCultureInfo);
            worksheet.Cells["A1"].Style.Font.Color.SetColor(fontColorH);
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
        }

        public static void SetExcelHeader(ExcelWorksheet worksheet, int headerPosition)
        {
            Color fontColorO = Color.FromArgb(22, 54, 92);
            string[] headerStrings = { "Идентификациски број", "Институција корисник", "Институција провајдер", "Сервис", "Има одговор", "Време на повик", "Време на одговор" };
            string[] cells = { "A", "B", "C", "D", "E", "F", "G" };

            for (int i = 0; i < 7; i++)
            {
                worksheet.Cells[cells[i] + headerPosition].Value = headerStrings[i];
                worksheet.Cells[cells[i] + headerPosition].Style.Font.Color.SetColor(fontColorO);
                worksheet.Cells[cells[i] + headerPosition].Style.Font.Size = InchesToPoints(0.17f);
                worksheet.Cells[cells[i] + headerPosition].Style.Font.Bold = true;
                worksheet.Cells[cells[i] + headerPosition].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                SetBordersAndHeaderBckColor(worksheet.Cells[cells[i] + headerPosition], true);
            }
        }
        public static float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }
    }
}