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
            SyncHelper.RunSync(() => _driver.Page.GoBackAsync());
        }

        public void Forward()
        {
            SyncHelper.RunSync(() => _driver.Page.GoForwardAsync());
        }

        public void GoToUrl(string url)
        {
            SyncHelper.RunSync(() => _driver.Page.GotoAsync(url, new Microsoft.Playwright.PageGotoOptions
            {
                WaitUntil = Microsoft.Playwright.WaitUntilState.Load
            }));
        }

        public void GoToUrl(Uri url)
        {
            GoToUrl(url.ToString());
        }

        public void Refresh()
        {
            SyncHelper.RunSync(() => _driver.Page.ReloadAsync());
        }
    }
}
