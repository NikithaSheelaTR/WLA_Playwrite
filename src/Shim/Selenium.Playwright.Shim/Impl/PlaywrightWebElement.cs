using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Playwright;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    public class PlaywrightWebElement : IWebElement
    {
        private readonly PlaywrightWebDriver _driver;

        public PlaywrightWebElement(ILocator locator, PlaywrightWebDriver driver)
        {
            this.Locator = locator;
            _driver = driver;
        }

        public ILocator Locator { get; }

        public string TagName
        {
            get
            {
                var tag = SyncHelper.RunSync(() => Locator.EvaluateAsync<string>("el => el.tagName.toLowerCase()"));
                return tag;
            }
        }

        public string Text
        {
            get
            {
                try
                {
                    // Use JavaScript to get innerText (matches Selenium behavior)
                    // but fall back gracefully if the element is detached or not yet rendered.
                    // innerText respects CSS visibility and collapses whitespace like Selenium.
                    // textContent does NOT � it includes hidden elements and raw whitespace.
                    return SyncHelper.RunSync(() => Locator.EvaluateAsync<string>(
                        "el => el.innerText != null ? el.innerText : (el.textContent || '')"));
                }
                catch (PlaywrightException)
                {
                    return string.Empty;
                }
            }
        }

        public bool Enabled => SyncHelper.RunSync(() => Locator.IsEnabledAsync());

        public bool Selected => SyncHelper.RunSync(() => Locator.EvaluateAsync<bool>("el => !!(el.selected || el.checked)"));

        public Point Location
        {
            get
            {
                var box = SyncHelper.RunSync(() => Locator.BoundingBoxAsync());
                return box != null ? new Point((int)box.X, (int)box.Y) : Point.Empty;
            }
        }

        public Size Size
        {
            get
            {
                var box = SyncHelper.RunSync(() => Locator.BoundingBoxAsync());
                return box != null ? new Size((int)box.Width, (int)box.Height) : Size.Empty;
            }
        }

        public bool Displayed
        {
            get
            {
                try
                {
                    return SyncHelper.RunSync(() => Locator.IsVisibleAsync());
                }
                catch (PlaywrightException)
                {
                    return false;
                }
            }
        }

        public void Clear()
        {
            SyncHelper.RunSync(() => Locator.ClearAsync(new Microsoft.Playwright.LocatorClearOptions
            {
                Timeout = 5000
            }));
        }

        public void Click()
        {
            PlaywrightWebDriver.Trace($"Click: locator={Locator}");

            // Check if the element (or its closest <a> ancestor) has target="_blank"
            // which would open a new tab/popup. Notify the driver so WindowHandles
            // can wait for the new page to appear.
            bool mightOpenPopup = false;
            try
            {
                mightOpenPopup = SyncHelper.RunSync(() => Locator.EvaluateAsync<bool>(
                    "el => { var a = el.closest ? el.closest('a') : null; return !!(a && a.target === '_blank'); }"));
            }
            catch { /* element might not support closest, proceed with normal click */ }

            if (mightOpenPopup)
            {
                _driver.NotifyClickMayOpenPopup();
                PlaywrightWebDriver.Trace("Click: target=_blank detected, using RunAndWaitForPopup");
                try
                {
                    SyncHelper.RunSync(async () =>
                    {
                        var popupTask = _driver.Page.WaitForPopupAsync(
                            new Microsoft.Playwright.PageWaitForPopupOptions { Timeout = 10000 });
                        await Locator.ClickAsync(new Microsoft.Playwright.LocatorClickOptions { Timeout = 5000 });
                        await popupTask;
                    });
                }
                catch (TimeoutException)
                {
                    PlaywrightWebDriver.Trace("Click: popup wait timed out, proceeding anyway");
                }
            }
            else
            {
                // Selenium Click() does not auto-wait — it clicks immediately or
                // throws. Playwright auto-waits for visibility, stability, and
                // actionability.  Use the context default (5s) rather than
                // Playwright's built-in 30s so the test framework can retry quickly.
                try
                {
                    SyncHelper.RunSync(() => Locator.ClickAsync(new Microsoft.Playwright.LocatorClickOptions
                    {
                        Timeout = 5000
                    }));
                }
                catch (Exception ex) when (ex.Message.Contains("element is not enabled"))
                {
                    // Selenium allows clicking disabled elements (no exception,
                    // click dispatched but has no effect in the browser — same
                    // as normal user click on a disabled button).  Use Force to
                    // bypass Playwright's actionability check.
                    PlaywrightWebDriver.Trace("Click: element disabled, retrying with Force=true (Selenium compat)");
                    SyncHelper.RunSync(() => Locator.ClickAsync(new Microsoft.Playwright.LocatorClickOptions
                    {
                        Timeout = 5000,
                        Force = true
                    }));
                }
                catch (Exception ex) when (ex.Message.Contains("waiting for scheduled navigations to finish"))
                {
                    // Selenium click returns immediately after dispatching the
                    // click event. Playwright additionally waits for navigations
                    // triggered by the click. Swallow the navigation timeout to
                    // match Selenium behaviour.
                    PlaywrightWebDriver.Trace("Click: navigation wait timed out after click, proceeding (Selenium compat)");
                }
            }
        }

        public string GetAttribute(string attributeName)
        {
            return SyncHelper.RunSync(() => Locator.GetAttributeAsync(attributeName));
        }

        public string GetProperty(string propertyName)
        {
            return SyncHelper.RunSync(() => Locator.EvaluateAsync<string>(
                $"(el) => {{ var v = el['{propertyName}']; return v == null ? null : String(v); }}"));
        }

        public string GetCssValue(string propertyName)
        {
            return SyncHelper.RunSync(() => Locator.EvaluateAsync<string>(
                $"(el) => window.getComputedStyle(el).getPropertyValue('{propertyName}')"));
        }

        public void SendKeys(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            // Check if text contains special keys
            bool hasSpecialKeys = false;
            foreach (char c in text)
            {
                if (c >= '\ue000' && c <= '\ue03d')
                {
                    hasSpecialKeys = true;
                    break;
                }
            }

            if (hasSpecialKeys)
            {
                // Handle special key sequences
                var mapped = KeyMapper.MapKeys(text);
                SyncHelper.RunSync(() => Locator.PressAsync(mapped));
            }
            else if (text.Length == 1)
            {
                // Single character (e.g., from SendKeysSlow): focus the element once
                // then type via page-level keyboard. This avoids the overhead of
                // PressSequentiallyAsync's full actionability checks per character,
                // which caused 2-5 second delays per keystroke on CIAM password fields.
                SyncHelper.RunSync(() => Locator.FocusAsync());
                SyncHelper.RunSync(() => _driver.Page.Keyboard.TypeAsync(text));
            }
            else
            {
                // Multi-character text: check if it's a file input first
                var inputType = SyncHelper.RunSync(() => Locator.EvaluateAsync<string>(
                    "el => el.tagName === 'INPUT' ? el.type : ''"));

                if (inputType == "file")
                {
                    SyncHelper.RunSync(() => Locator.SetInputFilesAsync(text));
                }
                else
                {
                    // Use PressSequentiallyAsync to match Selenium SendKeys behavior.
                    // Selenium fires keyDown/keyPress/keyUp for each character.
                    SyncHelper.RunSync(() => Locator.PressSequentiallyAsync(text, new Microsoft.Playwright.LocatorPressSequentiallyOptions { Delay = 50, Timeout = 5000 }));
                }
            }
        }

        public void Submit()
        {
            SyncHelper.RunSync(() => Locator.EvaluateAsync(
                "el => { var form = el.closest('form'); if (form) form.submit(); else el.submit(); }"));
        }

        public IWebElement FindElement(By by)
        {
            // Handle custom By subclasses (e.g. ByChained) that override FindElement
            if (by.Mechanism == null)
            {
                PlaywrightWebDriver.Trace($"WebElement.FindElement delegating to custom By: {by}");
                return by.FindElement(this);
            }

            var locatorStr = ByConverter.ToPlaywrightLocator(by);
            var scoped = Locator.Locator(locatorStr);
            var count = SyncHelper.RunSync(() => scoped.CountAsync());
            PlaywrightWebDriver.Trace($"WebElement.FindElement: by={by}, locator='{locatorStr}', count={count}");
            if (count == 0)
                throw new NoSuchElementException($"Unable to locate element: {by}");
            return new PlaywrightWebElement(scoped.First, _driver);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            // Handle custom By subclasses (e.g. ByChained) that override FindElements
            if (by.Mechanism == null)
                return by.FindElements(this);

            var locatorStr = ByConverter.ToPlaywrightLocator(by);
            var scoped = Locator.Locator(locatorStr);
            var count = SyncHelper.RunSync(() => scoped.CountAsync());
            var elements = new List<IWebElement>();
            for (int i = 0; i < count; i++)
            {
                elements.Add(new PlaywrightWebElement(scoped.Nth(i), _driver));
            }
            return elements.AsReadOnly();
        }
    }
}
