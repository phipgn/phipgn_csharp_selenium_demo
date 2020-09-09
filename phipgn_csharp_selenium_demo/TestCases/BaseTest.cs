using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using phipgn_csharp_selenium_demo.WrapperFactory;
using System;
using System.Configuration;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace phipgn_csharp_selenium_demo.TestCases
{
    class BaseTest
    {
        [SetUp]
        protected void SetUp()
        {
            BrowserFactory.InitBrowser(BrowserFactory.CHROME);
            GoToURL(ConfigurationManager.AppSettings["URL"]);
            BrowserFactory.MaximizeWindow();
        }

        [TearDown]
        protected void TearDown()
        {
            BrowserFactory.Close();
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
            } while(retry && attempt > 0);
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
            e.SendKeys(text);
        }

        public void SelectSearchDropDownBox(IWebElement e, string text)
        {
            var selectElement = new SelectElement(e);
            selectElement.SelectByText(text);
        }
    }
}
