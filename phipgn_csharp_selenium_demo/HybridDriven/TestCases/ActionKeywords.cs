using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using phipgn_csharp_selenium_demo.Common;
using phipgn_csharp_selenium_demo.HybridDriven;
using phipgn_csharp_selenium_demo.Utilities;
using System;
using System.Threading;

namespace phipgn_csharp_selenium_demo.TestCases_HybridDriven
{
    class ActionKeywords
    {

        public static void OpenBrowser(string browserName)
        {
            try
            {
                BrowserFactory.InitBrowser(browserName);
                LogUtil.LogInfo($"{browserName} opened successfully!");
                BrowserFactory.MaximizeWindow();
                LogUtil.LogInfo($"{browserName} maximized successfully!");
            }
            catch (Exception e)
            {
                string msg = $"Failed to open browser: {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
            }
        }

        public static void Navigate(string url)
        {
            try
            {
                BrowserFactory.GoToURL(url);
                LogUtil.LogInfo($"Navigated to {url} successfully!\n");
            }
            catch (Exception e)
            {
                string msg = $"Failed navigating to {url}: {e.Message}";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
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
                string msg = $"Failed closing browser: {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
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
                string msg = $"Failed selecting option {locatorId}-'{option}': {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
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
                string msg = $"Failed to click element {locatorId}: {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
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
                string msg = $"Failed setting text for {locatorId}: {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
            }
        }

        public static void VerifyProductTitle(string locatorId, string text)
        {
            try
            {
                string xpath = GetXpath(locatorId);
                WaitForElementDisplayed(xpath);
                IWebElement e = BrowserFactory.GetDriver().FindElement(By.XPath(xpath));
                if (e.Text.Equals(text))
                    LogUtil.LogInfo($"{locatorId} has text '{text}' as expected!");
                else
                    LogUtil.LogFail($"{locatorId} does not have text '{text}' as expected!");
            }
            catch (Exception e)
            {
                string msg = $"Failed setting text for {locatorId}: {e.Message}\n";
                LogUtil.LogFail(msg);
                LogUtil.ErrLog += msg;
            }
        }

        private static void WaitForElementDisplayed(string xpath)
        {
            LogUtil.LogInfo($"Waiting for element {xpath} to be displayed!");
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetDriver(), TimeSpan.FromSeconds(15));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            LogUtil.LogInfo($"Element {xpath} is now displayed!");
        }

        private static void WaitForElementClickable(IWebElement e)
        {
            LogUtil.LogInfo($"Waiting for element {e} to be clickable!");
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetDriver(), TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(e));
            LogUtil.LogInfo($"Element {e} is now clickable!");
        }

        private static string GetXpath(string obj)
        {
            return Locators.Default.Properties[obj].DefaultValue as string;
        }
    }
}
