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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Core
{
    public class Helper
    {
        public void AddDataToFile(string uniqueId)
        {
            SurveyManager manager = new SurveyManager();
            var data = manager.GetSurveyData(uniqueId);
            var fileName = $"C:\\Users\\priyanka.yadav\\Desktop\\Priyanka\\Interview\\SurveyResponseData.xlsx";
            var workbook = new XSSFWorkbook();
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new XSSFWorkbook(file);
                file.Close();
            }
            
            //var header = worksheet.CreateRow(0);
            //var headerStyle = workbook.CreateCellStyle();
            //var headerFont = workbook.CreateFont();
            //headerFont.IsBold = true;
            //headerStyle.SetFont(headerFont);
            ISheet worksheet = workbook.GetSheet("SurveyData2021");//workbook.GetSheet(0)

            var cellStyle = workbook.CreateCellStyle();
            cellStyle.WrapText = true;
            var row = worksheet.CreateRow(worksheet.LastRowNum + 1);
            var cell = row.CreateCell(0);
            cell.SetCellValue(uniqueId);
            int colNum = 1;
            foreach (var d in data)
            {
                var cell1 = row.CreateCell(colNum);

                if (!string.IsNullOrEmpty(d.Answer))
                    cell1.SetCellValue(d.Answer);
                else if (!string.IsNullOrEmpty(d.AnswerId))
                {
                    List<int> answerIds = d.AnswerId.Split(',').Select(int.Parse).ToList();
                    cell1.SetCellValue(string.Join(",", manager.GetAnswers(answerIds).Select(a => a.Text).ToArray()));
                }
                else
                    cell1.SetCellValue("NA");

                cell1.CellStyle = cellStyle;
                colNum++;
            }
            using (var file2 = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file2);
                file2.Close();
            }
        }
    }
}
