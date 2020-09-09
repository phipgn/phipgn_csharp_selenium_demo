using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace phipgn_csharp_selenium_demo.PageObjects
{
    class SearchResultPage
    {
        [FindsBy(How = How.XPath, Using = "((//span[@cel_widget_id='MAIN-SEARCH_RESULTS'])[1]//a)[2]")]
        [CacheLookup]
        public IWebElement FirstResult { get; set; }
    }
}
