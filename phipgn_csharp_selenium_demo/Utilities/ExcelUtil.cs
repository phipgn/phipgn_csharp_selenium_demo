using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace phipgn_csharp_selenium_demo.Utilities
{
    class ExcelUtil
    {
        public static Excel.Application ExcelApp;
        public static Excel.Workbook ExcelWBook;
        private static Excel.Worksheet ExcelWSheet;
        private const int COL_TESTCASE_ID = 0;

        //This method is to set the File path and to open the Excel file
        //Pass Excel Path and SheetName as Arguments to this method
        public static void SetExcelFile(string path)
        {
            try
            {
                ExcelApp = new Excel.Application();
                ExcelApp.Visible = false;

                // Opening Excel file
                ExcelWBook = ExcelApp.Workbooks.Open(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method SetExcelFile | Exception desc: {e.Message}");
            }
        }

        //This method is to read the test data from the Excel cell
        //In this we are passing parameters/arguments as Row Num and Col Num & Sheet Name
        public static string GetCellData(int rowNum, int colNum, string sheetName)
        {
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                var cellValue = (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Value as string;
                return cellValue;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method GetCellData | Exception desc: {e.Message}");
                return null;
            }
        }

        // This method to return number of rows in that sheet
        public static int GetRowCount(string sheetName)
        {
            int number = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                number = ExcelWSheet.UsedRange.Rows.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method GetRowCount | Exception desc: {e.Message}");
            }
            return number;
        }

        // This method is to get the Row number of the test case
        // This method takes three arguments (Test case name, Column Number & Sheet Name)
        public static int GetRowContains(string testcaseId, int colNum, string sheetName)
        {
            int count = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                int n = GetRowCount(sheetName);
                for (int i = 0; i <= n; i++)
                    if (GetCellData(i, colNum, sheetName).Equals(testcaseId))
                        count++;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method GetRowContains | Exception desc: {e.Message}");
            }
            return count;
        }

        // This method is to get the count of the test steps of test case
        // This method takes three arguments (Sheet name, Test Case Id & Test case row number)
        public static int GetTestStepsCount(string testCaseID, int testCaseStart, string sheetName)
        {
            int number = 0;
            try
            {
                for (int i = testCaseStart; i <= GetRowCount(sheetName); i++)
                    if (!testCaseID.Equals(GetCellData(i, COL_TESTCASE_ID, sheetName)))
                        number = i;
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                number = ExcelWSheet.UsedRange.Rows.Count + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method GetTestStepsCount | Exception desc: {e.Message}");
                number = 0;
            }
            return number;
        }

        // This method is used to write value in excel cell
        // Four arguments are accepted (Result, Row Number, Column Number & Sheet Name)
        public static void SetCellData(string Result, int rowNum, int colNum, string sheetName)
        {
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                string vv = (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Value as string;
                (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Value = Result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Class ExcelUtil | Method SetCellData | Exception desc: {e.Message}");
            }
        }
    }
}
