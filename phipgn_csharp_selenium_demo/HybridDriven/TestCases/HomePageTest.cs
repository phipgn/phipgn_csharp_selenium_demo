using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using phipgn_csharp_selenium_demo.TestCases_HybridDriven;
using phipgn_csharp_selenium_demo.Utilities;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace phipgn_csharp_selenium_demo.TestCases_KD
{
    class HomePageTest : BaseTest
    {
        private static object[] DataTest01 = ExcelUtil.GetDataSet(ConfigurationManager.AppSettings[typeof(HomePageTest).Name + "Data"], "Test01");

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var path = Assembly.GetCallingAssembly().CodeBase;
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
            //string keyConfig = this.GetType().Name + "Data";
            //ExcelUtil.SetExcelFile(ConfigurationManager.AppSettings[keyConfig]);
        }

        [OneTimeTearDown]
        public void TearDownOnce()
        {
            ExcelUtil.ExcelWBook.Save();
            ExcelUtil.ExcelWBook.Close(0);
            ExcelUtil.ExcelApp.Quit();
            LogUtil._extent.Flush();
        }

        [TearDown]
        public void TearDown()
        {
            LogUtil.ErrLog = "";
        }

        [Test, TestCaseSource("DataTest01")]
        public void Test01(params object[] args)
        {
            ExecuteTestCase(args);
            LogUtil.LogInfo(LogUtil.ErrLog);
            Assert.IsTrue(LogUtil.ErrLog.Equals(""));
        }
 
    }
}
