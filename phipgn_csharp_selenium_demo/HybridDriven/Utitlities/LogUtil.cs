using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;

namespace phipgn_csharp_selenium_demo.Utilities
{
    class LogUtil
    {
        public static ExtentReports _extent = new ExtentReports();
        public static ExtentTest _test;
        public static string ErrLog = "";

        public static void GetTest()
        {
            if (_test == null)
            {
                // init extent reports instance
                _test = _extent.CreateTest("Practice", "This is the Practice");
            }
        }

        public static void StartTestCase(String sTestCaseName)
        {
            _test = _extent.CreateTest(sTestCaseName);
            _test.Log(Status.Info, "|---S---T---A---R---T-");
            _test.Log(Status.Info, "|---Test ID: " + sTestCaseName);
        }

        public static void EndTestCase()
        {
            _test.Log(Status.Info, "|---E---N---D-");
        }

        public static void LogInfo(string message)
        {
            _test.Log(Status.Info, message);
        }

        public static void LogFail(string message)
        {
            _test.Log(Status.Fail, message);
        }

        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            if (screenshotDriver == null)
            {
                _test.Log(Status.Fail, "Error in taking screenshot!");
            }
            else
            {
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(fileName, OpenQA.Selenium.ScreenshotImageFormat.Png);
            }
        }
    }
}
