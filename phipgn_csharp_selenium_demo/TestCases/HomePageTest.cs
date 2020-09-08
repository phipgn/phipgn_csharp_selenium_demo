using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using phipgn_csharp_selenium_demo.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace phipgn_csharp_selenium_demo.TestCases
{
    class HomePageTest : BaseTest
    {
        [Test]
        public void Test01()
        {
            var homePage = new HomePage(driver);
            /*
            SelectByText(homePage.searchDropdownBox, "Books");
            SetText(homePage.searchBox, "Selenium");
            ClickToElement(homePage.btnSearch);*/
            homePage.SearchBook("Selenium");
            Thread.Sleep(5000);
        }
    }
}
