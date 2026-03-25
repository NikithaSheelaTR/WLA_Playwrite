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
                    return SyncHelper.RunSync(() => Locator.InnerTextAsync());
                }
                catch (PlaywrightException)
                {
                    return string.Empty;
                }
            }
        }

        public bool Enabled => SyncHelper.RunSync(() => Locator.IsEnabledAsync());

        public bool Selected => SyncHelper.RunSync(() => Locator.IsCheckedAsync());

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
            SyncHelper.RunSync(() => Locator.ClearAsync());
        }

        public void Click()
        {
            SyncHelper.RunSync(() => Locator.ClickAsync());
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
            else
            {
                // For regular text input, use type (which types character by character)
                // But first check if element is a file input
                var inputType = SyncHelper.RunSync(() => Locator.EvaluateAsync<string>(
                    "el => el.tagName === 'INPUT' ? el.type : ''"));

                if (inputType == "file")
                {
                    SyncHelper.RunSync(() => Locator.SetInputFilesAsync(text));
                }
                else
                {
                    SyncHelper.RunSync(() => Locator.FillAsync(text));
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
            var locatorStr = ByConverter.ToPlaywrightLocator(by);
            var scoped = Locator.Locator(locatorStr);
            var count = SyncHelper.RunSync(() => scoped.CountAsync());
            if (count == 0)
                throw new NoSuchElementException($"Unable to locate element: {by}");
            return new PlaywrightWebElement(scoped.First, _driver);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
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
