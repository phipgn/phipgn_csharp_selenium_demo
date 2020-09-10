using AventStack.ExtentReports;
using NUnit.Framework;
using phipgn_csharp_selenium_demo.PageObjects;
using phipgn_csharp_selenium_demo.TestDataAccess;
using System;

namespace phipgn_csharp_selenium_demo.TestCases
{
    [TestFixture]
    class HomePageTest : BaseTest
    {

        private static object[] DataSet = JsonTestData.GetDataSet();

        [Test, TestCaseSource("DataSet")]
        public void Test01(string query, string expected)
        {            
            SelectSearchDropDownBox(Page.Home.SearchDropdownBox, "Books");
            SetText(Page.Home.SearchBox, query);

            ClickToElement(Page.Home.BtnSearch);
            ClickToElement(Page.SearchResult.FirstResult);

            WaitForElementDisplayed(Page.ProductDetail.ProductTitle);
 
            string actual = Page.ProductDetail.ProductTitle.Text;
            Console.WriteLine($"{query} : {actual}");
            Assert.IsTrue(actual.Equals(expected));
        }
    }
}
