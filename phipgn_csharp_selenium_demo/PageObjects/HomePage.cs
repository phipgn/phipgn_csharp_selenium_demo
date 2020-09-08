using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace phipgn_csharp_selenium_demo.PageObjects
{
    class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='searchDropdownBox']")]
        [CacheLookup]
        private IWebElement searchDropdownBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='twotabsearchtextbox']")]
        [CacheLookup]
        private IWebElement searchBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Go']")]
        [CacheLookup]
        private IWebElement btnSearch { get; set; }

        public void SearchBook(string query)
        {
            var selectElement = new SelectElement(searchDropdownBox);
            selectElement.SelectByText("Books");
            searchBox.SendKeys(query);
            btnSearch.Click();
        }
    }
}
