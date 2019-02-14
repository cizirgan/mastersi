using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Mastersi.MineriaWeb
{
    public static class ExcelFile
    {
        public static void CreateFile(string file, JToken data)
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");


                IRow row = excelSheet.CreateRow(0);
                SetHeaderColumns(row);

                for (int i = 1; i < 100; i++)
                {
                    row = excelSheet.CreateRow(i);

                    try
                    {
                        row.CreateCell(0).SetCellValue(data[i]["created_time"].ToString());
                        row.CreateCell(1).SetCellValue(data[i]["message"].ToString());
                        row.CreateCell(2).SetCellValue(data[i]["comments"]["summary"]["total_count"].ToString());
                        row.CreateCell(3).SetCellValue(data[i]["likes"]["summary"]["total_count"].ToString());
                    }
                    catch (System.Exception e)
                    {
                        System.Console.WriteLine(e.StackTrace + " The PostID: " + i);
                    }


                }


                workbook.Write(fs);
            }
        }

        private static void SetHeaderColumns(IRow row)
        {
            row.CreateCell(0).SetCellValue("CreatedTime");
            row.CreateCell(1).SetCellValue("Message");
            row.CreateCell(2).SetCellValue("Comments");
            row.CreateCell(3).SetCellValue("Likes");

        }

        private static void SetHeaderComments(IRow row)
        {
            row.CreateCell(0).SetCellValue("CreatedTime");
            row.CreateCell(1).SetCellValue("Message");

        }
        public static void CreateFileForComments(string file, JToken data)
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");


                IRow row = excelSheet.CreateRow(0);
                SetHeaderComments(row);

                int a = 1;
                foreach (var item in data)
                {
                    row = excelSheet.CreateRow(a);

                    try
                    {
                        System.Console.WriteLine(item["message"].ToString());
                        row.CreateCell(0).SetCellValue(item["created_time"].ToString());
                        row.CreateCell(1).SetCellValue(item["message"].ToString());
                    }
                    catch (System.Exception)
                    {

                    }
                    a++;
                }


                workbook.Write(fs);
            }
        }

    }
}
