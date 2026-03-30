using System;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal class PlaywrightNavigation : INavigation
    {
        private readonly PlaywrightWebDriver _driver;

        public PlaywrightNavigation(PlaywrightWebDriver driver)
        {
            _driver = driver;
        }

        public void Back()
        {
            SyncHelper.RunSync(() => _driver.Page.GoBackAsync(new Microsoft.Playwright.PageGoBackOptions
            {
                WaitUntil = Microsoft.Playwright.WaitUntilState.Load,
                Timeout = 60000
            }));
        }

        public void Forward()
        {
            SyncHelper.RunSync(() => _driver.Page.GoForwardAsync(new Microsoft.Playwright.PageGoForwardOptions
            {
                WaitUntil = Microsoft.Playwright.WaitUntilState.Load,
                Timeout = 60000
            }));
        }

        public void GoToUrl(string url)
        {
            try
            {
                PlaywrightWebDriver.Trace($"GoToUrl starting: {url}");
                SyncHelper.RunSync(() => _driver.Page.GotoAsync(url, new Microsoft.Playwright.PageGotoOptions
                {
                    WaitUntil = Microsoft.Playwright.WaitUntilState.Load,
                    Timeout = 60000
                }));
                PlaywrightWebDriver.Trace($"GoToUrl completed: {url} (actual: {_driver.Page.Url})");
            }
            catch (Microsoft.Playwright.PlaywrightException ex)
            {
                PlaywrightWebDriver.Trace($"GoToUrl failed ({ex.GetType().Name}): {ex.Message}");
                throw new OpenQA.Selenium.WebDriverException($"Navigation to '{url}' failed: {ex.Message}", ex);
            }
        }

        public void GoToUrl(Uri url)
        {
            GoToUrl(url.ToString());
        }

        public void Refresh()
        {
            SyncHelper.RunSync(() => _driver.Page.ReloadAsync(new Microsoft.Playwright.PageReloadOptions
            {
                WaitUntil = Microsoft.Playwright.WaitUntilState.Load,
                Timeout = 60000
            }));
        }
    }
}
