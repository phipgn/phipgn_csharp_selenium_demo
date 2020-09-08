using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;

namespace phipgn_csharp_selenium_demo.TestCases
{
    class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        protected void setUp()
        {
            driver = new ChromeDriver();
            Console.WriteLine("url=" + ConfigurationManager.AppSettings["URL"]);
            GoToURL(ConfigurationManager.AppSettings["URL"]);
            //GoToURL("https://www.amazon.com");
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        protected void tearDown()
        {
            driver.Quit();
        }

        protected void WaitForElementPresent()
        {

        }

        protected void WaitForElementDisplayed(IWebElement e)
        {
            int retry = 0;
            do
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Element is displayed : {e.Displayed} : {retry}");
                retry++;
            } while (!e.Displayed && retry < 3);
        }

        protected void WaitForElementClickable(IWebElement e)
        {

        }

        protected void ClickToElement(IWebElement e)
        {
            e.Click();
        }

        protected void GoToURL(String url)
        {
            driver.Navigate().GoToUrl(url);
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
    }
}
