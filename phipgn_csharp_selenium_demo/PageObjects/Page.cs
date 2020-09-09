using phipgn_csharp_selenium_demo.WrapperFactory;
using SeleniumExtras.PageObjects;

namespace phipgn_csharp_selenium_demo.PageObjects
{
    // Page Generator
    class Page
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(BrowserFactory.GetDriver(), page);
            return page;
        }

        public static HomePage Home
        {
            get { return GetPage<HomePage>(); }
        }

        public static SearchResultPage SearchResult
        {
            get { return GetPage<SearchResultPage>(); }
        }

        public static ProductDetailPage ProductDetail
        {
            get { return GetPage<ProductDetailPage>(); }
        }
    }
}
