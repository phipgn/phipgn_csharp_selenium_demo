using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using phipgn_csharp_selenium_demo.Utilities;
using System;

namespace phipgn_csharp_selenium_demo.TestCases_KeywordDriven
{
    class ActionKeywords
    {
        public static void OpenBrowser(string browserName)
        {
            try
            {
                BrowserFactory.InitBrowser(browserName);
                LogUtil.LogInfo($"Open browswer {browserName} successfully!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed to open browser: {e.Message}");
            }
        }

        public static void Navigate(string url)
        {
            try
            {
                BrowserFactory.GoToURL(url);
                LogUtil.LogInfo($"Navigated to {url} successfully!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed navigating to {url}: {e.Message}");
            }
        }

        public static void CloseBrowser(string data)
        {
            try
            {
                BrowserFactory.Close();
                LogUtil.LogInfo($"Browser has been closed successfully!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed closing browser: {e.Message}");
            }
        }

        public static void SelectOption(string locatorId, string option)
        {
            try
            {
                string xpath = GetXpath(locatorId);
                IWebElement e = BrowserFactory.GetDriver().FindElement(By.XPath(xpath));
                var selectElement = new SelectElement(e);
                selectElement.SelectByText(option);
                LogUtil.LogInfo($"Option '{option}' has been selected!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed selecting option {locatorId}-'{option}': {e.Message}");
            }
        }

        public static void ClickElement(string locatorId)
        {
            try
            {
                string xpath = GetXpath(locatorId);
                IWebElement e = BrowserFactory.GetDriver().FindElement(By.XPath(xpath));
                WaitForElementClickable(e);
                e.Click();
                LogUtil.LogInfo($"Element '{xpath}' has been clicked!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed to click element {locatorId}: {e.Message}");
            }
        }

        public static void InputText(string locatorId, string text)
        {
            try
            {
                string xpath = GetXpath(locatorId);
                IWebElement e = BrowserFactory.GetDriver().FindElement(By.XPath(xpath));
                e.Clear();
                e.SendKeys(text);
                LogUtil.LogInfo($"Text '{text}' has been set for {locatorId}!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed setting text for {locatorId}: {e.Message}");
            }
        }

        public static void VerifyProductTitle(string locatorId, string text)
        {
            try
            {
                string xpath = GetXpath(locatorId);
                IWebElement e = BrowserFactory.GetDriver().FindElement(By.XPath(xpath));
                if (e.Text.Equals(text))
                    LogUtil.LogInfo($"{locatorId} has text '{text}' as expected!");
                else
                    LogUtil.LogFail($"{locatorId} does not have text '{text}' as expected!");
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed setting text for {locatorId}: {e.Message}");
            }
        }

        private static void WaitForElementClickable(IWebElement e)
        {
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetDriver(), TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(e));
        }

        private static string GetXpath(string obj)
        {
            return Locators.Default.Properties[obj].DefaultValue as string;
        }
    }
}
