using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using DataAccessLayer;
using Microsoft.Office.Interop.Word;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using System.IO;

namespace Core
{
    public class Helper
    {
        public void AddDataToFile(string uniqueId)
        {
            SurveyManager manager = new SurveyManager();
            var data = manager.GetSurveyData(uniqueId);
            var fileName = $"C:\\Users\\priyanka.yadav\\Desktop\\Priyanka\\Interview\\Survey\\SurveyResponseData\\" + data.FirstOrDefault().SurveyId+ ".xlsx";
            // Declare HSSFWorkbook object to create sheet  
            var workbook = new HSSFWorkbook();
            var worksheet = workbook.CreateSheet($"SurveyData {data.FirstOrDefault().UniqueResponseId} {DateTime.Today.ToString("MMM dd yyyy")}");
            var header = worksheet.CreateRow(0);
            var headerStyle = workbook.CreateCellStyle();
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);

            var cellStyle = workbook.CreateCellStyle();
            cellStyle.WrapText = true;

            var columns = new[] { "Question", "Answer"};
            var headerRow = worksheet.CreateRow(0);

            for (int i = 0; i < columns.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(columns[i]);
                cell.CellStyle = headerStyle;
                worksheet.AutoSizeColumn(i);
                worksheet.SetColumnWidth(i, 5120);//column width is always divided by 256
            }
            int rowIndex = 0;
            foreach (var d in data)
            {
                rowIndex = rowIndex + 1;
                var row = worksheet.CreateRow(rowIndex);
             
                var cell = row.CreateCell(0);
                cell.SetCellValue(d.SurveyQuestions.Question);
                cell.CellStyle = cellStyle;

                var cell1 = row.CreateCell(1);
                if (!string.IsNullOrEmpty(d.Answer))
                cell1.SetCellValue(d.Answer);
                else
                {
                    List<int> answerIds = d.AnswerId.Split(',').Select(int.Parse).ToList();
                    cell1.SetCellValue(string.Join(",", manager.GetAnswers(answerIds).Select(a => a.Text).ToArray()));
                }
                cell1.CellStyle = cellStyle;
            }

            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            workbook.Write(stream);
            stream.Close();
            File.WriteAllBytes(fileName, stream.ToByteArray().ToArray());
        }
    }
}
