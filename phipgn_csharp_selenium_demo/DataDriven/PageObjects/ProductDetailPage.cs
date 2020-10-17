using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace phipgn_csharp_selenium_demo.PageObjects
{
    class ProductDetailPage
    {   
        [FindsBy(How = How.XPath, Using = "//*[@id='productTitle']")]
        [CacheLookup]
        public IWebElement ProductTitle { get; set; }
    }
}
