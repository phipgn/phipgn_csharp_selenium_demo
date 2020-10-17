using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace phipgn_csharp_selenium_demo.PageObjects
{
    class HomePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='searchDropdownBox']")]
        [CacheLookup]
        public IWebElement SearchDropdownBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='twotabsearchtextbox']")]
        [CacheLookup]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Go']")]
        [CacheLookup]
        public IWebElement BtnSearch { get; set; }
        
    }
}
