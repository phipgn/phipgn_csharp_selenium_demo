using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using phipgn_csharp_selenium_demo.TestCases_KeywordDriven;
using phipgn_csharp_selenium_demo.TestDataAccess;
using phipgn_csharp_selenium_demo.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace phipgn_csharp_selenium_demo.TestCases_KD
{
    class TestExecutor
    {
        private ActionKeywords actionKeywords;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            LogUtil._extent.AttachReporter(htmlReporter);
            LogUtil._extent.AddSystemInfo("Hostname", "localhost");
            LogUtil._extent.AddSystemInfo("Environment", "QA");
            LogUtil._extent.AddSystemInfo("Username", "PhiPGN");

            actionKeywords = new ActionKeywords();
            ExcelUtil.SetExcelFile(ConfigurationManager.AppSettings["ExcelDataFile"]);
        }

        [OneTimeTearDown]
        public void TearDownOnce()
        {
            ExcelUtil.ExcelWBook.Save();
            ExcelUtil.ExcelWBook.Close(0);
            ExcelUtil.ExcelApp.Quit();
            LogUtil._extent.Flush();
        }

        [Test]
        public void TestMain()
        {
            ExecuteTestCase();
        }

        private void ExecuteTestCase()
        {
            string SHEET_TESTCASES = "Testcases";
            string SHEET_STEPS = "Teststeps";
            int COL_TC_ID = 0;
            int COL_RUN = 2;

            int COL_LOCATOR_ID = 2;
            int COL_KEYWORD = 3;
            int COL_DATASET = 4;

            int nTestCases = ExcelUtil.GetRowCount(SHEET_TESTCASES) - 1;    // number of test cases
            for (int i = 1; i <= nTestCases; i++)
            {
                string testcaseId = ExcelUtil.GetCellData(i, COL_TC_ID, SHEET_TESTCASES);

                bool run = Boolean.Parse(ExcelUtil.GetCellData(i, COL_RUN, SHEET_TESTCASES));   // run test case or not
                if (run)
                {
                    LogUtil.StartTestCase(testcaseId);
                    for (int j = 1; j < ExcelUtil.GetRowCount(SHEET_STEPS); j++)
                    {
                        string testcaseId2 = ExcelUtil.GetCellData(j, COL_TC_ID, SHEET_STEPS);
                        if (testcaseId.Equals(testcaseId2))
                        {
                            string locatorId = ExcelUtil.GetCellData(j, COL_LOCATOR_ID, SHEET_STEPS);
                            string actionKeyword = ExcelUtil.GetCellData(j, COL_KEYWORD, SHEET_STEPS);
                            string dataset = ExcelUtil.GetCellData(j, COL_DATASET, SHEET_STEPS);
                            ExecuteKeyword(actionKeyword, locatorId, dataset);
                        }
                    }
                    LogUtil.EndTestCase();
                }
            }
        }

        private void ExecuteKeyword(string actionKeyword, string locatorId, string dataset)
        {
            if (actionKeyword == null || actionKeyword.Equals(""))
                throw new Exception("Action Keyword is NULL or empty!");

            MethodInfo mi = actionKeywords.GetType().GetMethod(actionKeyword);

            List<string> args = new List<string>();
            switch (actionKeyword)
            {
                case "OpenBrowser":
                case "CloseBrowser":
                case "Navigate":
                    args.Add(dataset);
                    break;
                case "ClickElement":
                    args.Add(locatorId);
                    break;
                case "InputText":
                case "VerifyProductTitle":
                case "SelectOption":
                    args.Add(locatorId);
                    args.Add(dataset);
                    break;
            }

            try
            {
                mi.Invoke(actionKeyword, args.ToArray());
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed at '{actionKeyword}': {e.Message}");
            }
        }
    }
}
