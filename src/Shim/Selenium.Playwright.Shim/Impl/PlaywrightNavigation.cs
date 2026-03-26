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
            try
            {
                PlaywrightWebDriver.Trace($"GoToUrl starting: {url}");
                SyncHelper.RunSync(() => _driver.Page.GotoAsync(url, new Microsoft.Playwright.PageGotoOptions
                {
                    WaitUntil = Microsoft.Playwright.WaitUntilState.Load,
                    Timeout = 60000
                }));
                PlaywrightWebDriver.Trace($"GoToUrl completed: {url} (actual: {_driver.Page.Url})");
                try
                {
                    var title = SyncHelper.RunSync(() => _driver.Page.TitleAsync());
                    var bodyText = SyncHelper.RunSync(() => _driver.Page.EvaluateAsync<string>("() => document.body ? document.body.innerText.substring(0, 500) : 'NO BODY'"));
                    PlaywrightWebDriver.Trace($"GoToUrl page title: '{title}', body(500): {bodyText}");
                    // Check for save button specifically
                    var btnInfo = SyncHelper.RunSync(() => _driver.Page.EvaluateAsync<string>(@"() => {
                        var btn = document.getElementById('coid_website_routingSaveButton');
                        if (btn) return 'FOUND id=coid_website_routingSaveButton tag=' + btn.tagName + ' displayed=' + (btn.offsetParent !== null) + ' text=' + btn.textContent;
                        var allBtns = document.querySelectorAll('button, input[type=submit], input[type=button]');
                        var info = 'NOT FOUND. All buttons(' + allBtns.length + '): ';
                        for (var i = 0; i < Math.min(allBtns.length, 10); i++) info += allBtns[i].id + '|' + allBtns[i].textContent.substring(0,30) + '; ';
                        return info;
                    }"));
                    PlaywrightWebDriver.Trace($"GoToUrl saveBtn check: {btnInfo}");
                }
                catch (Exception diagEx)
                {
                    PlaywrightWebDriver.Trace($"GoToUrl diagnostics failed: {diagEx.Message}");
                }
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
            SyncHelper.RunSync(() => _driver.Page.ReloadAsync());
        }
    }
}
