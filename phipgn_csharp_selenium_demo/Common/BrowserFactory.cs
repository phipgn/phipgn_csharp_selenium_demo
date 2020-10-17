using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace phipgn_csharp_selenium_demo.Common
{
    class BrowserFactory
    {
        public const string CHROME = "Chrome";
        public const string FIREFOX = "Firefox";

        private static IWebDriver Driver { get; set; }

        public static void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case FIREFOX:
                    if (Driver == null)
                        Driver = new FirefoxDriver();
                    break;

                case CHROME:
                    if (Driver == null)
                        Driver = new ChromeDriver();
                    break;
            }
        }

        public static void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public static void GoToURL(string url)
        {
            Driver.Url = url;
        }

        public static void Close()
        {
            Driver.Quit();
            Driver = null;
        }

        public static IWebDriver GetDriver()
        {
            if (Driver == null)
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
            return Driver;
        }
    }
}
