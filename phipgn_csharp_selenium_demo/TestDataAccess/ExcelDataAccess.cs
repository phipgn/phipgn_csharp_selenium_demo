using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using Dapper;
using System.Collections.Generic;

namespace phipgn_csharp_selenium_demo.TestDataAccess
{
    class ExcelDataAccess
    {
        public static string TestDataFileConnection()
        {
            var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", fileName);
            return con;
        }

        public static ExcelTestData GetTestData(string sheetName, string keyName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format("select * from [{0}$] where key='{1}'", sheetName, keyName);
                var value = connection.Query<ExcelTestData>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }

        public static List<ExcelTestData> GetAllTestData(string sheetName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format($"select * from [{sheetName}$]");
                var value = connection.Query<ExcelTestData>(query).AsList();
                connection.Close();
                return value;
            }
        }
    }
}
