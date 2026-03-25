using System;
using System.Collections.Generic;
using Selenium.Playwright.Shim.Impl;

namespace OpenQA.Selenium.Interactions
{
    public class Actions
    {
        private readonly IWebDriver _driver;
        private readonly List<Action> _actionQueue = new List<Action>();

        public Actions(IWebDriver driver)
        {
            _driver = driver;
        }

        public Actions Click()
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                SyncHelper.RunSync(() => pwd?.Page?.Mouse.ClickAsync(0, 0));
            });
            return this;
        }

        public Actions Click(IWebElement element)
        {
            _actionQueue.Add(() => element.Click());
            return this;
        }

        public Actions DoubleClick(IWebElement element)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                    SyncHelper.RunSync(() => pwe.Locator.DblClickAsync());
                else
                    element.Click();
            });
            return this;
        }

        public Actions ContextClick(IWebElement element)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                    SyncHelper.RunSync(() => pwe.Locator.ClickAsync(new Microsoft.Playwright.LocatorClickOptions
                    {
                        Button = Microsoft.Playwright.MouseButton.Right
                    }));
            });
            return this;
        }

        public Actions MoveToElement(IWebElement element)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                    SyncHelper.RunSync(() => pwe.Locator.HoverAsync());
            });
            return this;
        }

        public Actions MoveToElement(IWebElement element, int offsetX, int offsetY)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                {
                    var box = SyncHelper.RunSync(() => pwe.Locator.BoundingBoxAsync());
                    if (box != null)
                    {
                        var pwd = _driver as PlaywrightWebDriver;
                        SyncHelper.RunSync(() => pwd?.Page?.Mouse.MoveAsync(
                            box.X + box.Width / 2 + offsetX,
                            box.Y + box.Height / 2 + offsetY));
                    }
                }
            });
            return this;
        }

        public Actions MoveByOffset(int offsetX, int offsetY)
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                SyncHelper.RunSync(() => pwd?.Page?.Mouse.MoveAsync(offsetX, offsetY));
            });
            return this;
        }

        public Actions ClickAndHold(IWebElement element)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                {
                    var box = SyncHelper.RunSync(() => pwe.Locator.BoundingBoxAsync());
                    if (box != null)
                    {
                        var pwd = _driver as PlaywrightWebDriver;
                        SyncHelper.RunSync(() => pwd?.Page?.Mouse.MoveAsync(
                            box.X + box.Width / 2, box.Y + box.Height / 2));
                        SyncHelper.RunSync(() => pwd?.Page?.Mouse.DownAsync());
                    }
                }
            });
            return this;
        }

        public Actions ClickAndHold()
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                SyncHelper.RunSync(() => pwd?.Page?.Mouse.DownAsync());
            });
            return this;
        }

        public Actions Release(IWebElement element)
        {
            _actionQueue.Add(() =>
            {
                if (element is PlaywrightWebElement pwe)
                {
                    var box = SyncHelper.RunSync(() => pwe.Locator.BoundingBoxAsync());
                    if (box != null)
                    {
                        var pwd = _driver as PlaywrightWebDriver;
                        SyncHelper.RunSync(() => pwd?.Page?.Mouse.MoveAsync(
                            box.X + box.Width / 2, box.Y + box.Height / 2));
                        SyncHelper.RunSync(() => pwd?.Page?.Mouse.UpAsync());
                    }
                }
            });
            return this;
        }

        public Actions Release()
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                SyncHelper.RunSync(() => pwd?.Page?.Mouse.UpAsync());
            });
            return this;
        }

        public Actions DragAndDrop(IWebElement source, IWebElement target)
        {
            _actionQueue.Add(() =>
            {
                if (source is PlaywrightWebElement pweSrc && target is PlaywrightWebElement pweTgt)
                    SyncHelper.RunSync(() => pweSrc.Locator.DragToAsync(pweTgt.Locator));
            });
            return this;
        }

        public Actions SendKeys(string keysToSend)
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                var mapped = KeyMapper.MapKeys(keysToSend);
                SyncHelper.RunSync(() => pwd?.Page?.Keyboard.PressAsync(mapped));
            });
            return this;
        }

        public Actions SendKeys(IWebElement element, string keysToSend)
        {
            _actionQueue.Add(() => element.SendKeys(keysToSend));
            return this;
        }

        public Actions KeyDown(string theKey)
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                var mapped = KeyMapper.MapKeys(theKey);
                SyncHelper.RunSync(() => pwd?.Page?.Keyboard.DownAsync(mapped));
            });
            return this;
        }

        public Actions KeyUp(string theKey)
        {
            _actionQueue.Add(() =>
            {
                var pwd = _driver as PlaywrightWebDriver;
                var mapped = KeyMapper.MapKeys(theKey);
                SyncHelper.RunSync(() => pwd?.Page?.Keyboard.UpAsync(mapped));
            });
            return this;
        }

        public Actions Build()
        {
            // No-op: actions are already queued
            return this;
        }

        public void Perform()
        {
            foreach (var action in _actionQueue)
            {
                action();
            }
            _actionQueue.Clear();
        }
    }
}
