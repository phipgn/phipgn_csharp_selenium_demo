using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using phipgn_csharp_selenium_demo.WrapperFactory;
using System;
using System.Configuration;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;
using NUnit.Framework.Interfaces;

namespace phipgn_csharp_selenium_demo.TestCases
{
    class BaseTest
    {

        private ExtentReports extentReport;
        private ExtentTest extentTest;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
            extentReport.AddSystemInfo("Hostname", "localhost");
            extentReport.AddSystemInfo("Environment", "QA");
            extentReport.AddSystemInfo("Username", "PhiPGN");
        }

        [OneTimeTearDown]
        protected void TearDownOnce()
        {
            extentReport.Flush();
        }

        [SetUp]
        protected void SetUp()
        {
            extentTest = extentReport.CreateTest(TestContext.CurrentContext.Test.Name);
            BrowserFactory.InitBrowser(BrowserFactory.CHROME);
            GoToURL(ConfigurationManager.AppSettings["URL"]);
            BrowserFactory.MaximizeWindow();
        }

        [TearDown]
        protected void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    string fileName = "Screenshot_" + time.ToString("hh_mm_ss") + ".png";
                    string screenShotPath = Capture(BrowserFactory.GetDriver(), fileName);
                    Console.WriteLine($"Screenshot has been captured at {screenShotPath}");
                    extentTest.Log(Status.Fail, "Fail");
                    extentTest.Log(Status.Fail, "Snapshot below: " + extentTest.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            extentTest.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extentReport.Flush();
            BrowserFactory.Close();
        }

        private string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return finalpth;
        }

        protected void WaitForElementDisplayed(IWebElement e)
        {
            int attempt = 3;
            bool retry = true;
            do
            {
                try
                {
                    Thread.Sleep(1000);
                    if (e.Displayed)
                        retry = false;
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine(ex.Message);
                    attempt--;
                }
            } while (retry && attempt > 0);
        }

        protected void WaitForElementClickable(IWebElement e)
        {
            WebDriverWait wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(e));
        }

        protected void ClickToElement(IWebElement e)
        {
            WaitForElementClickable(e);
            e.Click();
        }

        protected void GoToURL(String url)
        {
            BrowserFactory.GoToURL(url);
        }

        public static IWebDriver GetDriver()
        {
            return BrowserFactory.GetDriver();
        }

        protected void SelectByText(IWebElement e, string text)
        {
            var selectElement = new SelectElement(e);
            selectElement.SelectByText(text);
        }

        protected void SetText(IWebElement e, string text)
        {
            e.Clear();
            e.SendKeys(text);
        }

        public void SelectSearchDropDownBox(IWebElement e, string text)
        {
            var selectElement = new SelectElement(e);
            selectElement.SelectByText(text);
        }
    }
}
