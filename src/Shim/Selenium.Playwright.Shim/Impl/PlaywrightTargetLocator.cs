using System;
using System.Linq;
using Microsoft.Playwright;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal class PlaywrightTargetLocator : ITargetLocator
    {
        private readonly PlaywrightWebDriver _driver;

        public PlaywrightTargetLocator(PlaywrightWebDriver driver)
        {
            _driver = driver;
        }

        public IWebDriver Frame(int frameIndex)
        {
            var frames = _driver.Page.Frames;
            if (frameIndex < 0 || frameIndex >= frames.Count)
                throw new NoSuchFrameException($"No frame found at index {frameIndex}");

            // Skip main frame (index 0 is main)
            var childFrames = frames.Where(f => f.ParentFrame != null).ToList();
            if (frameIndex >= childFrames.Count)
                throw new NoSuchFrameException($"No frame found at index {frameIndex}");

            _driver.CurrentFrame = childFrames[frameIndex];
            _driver.CurrentFrameLocator = null;
            return _driver;
        }

        public IWebDriver Frame(string frameName)
        {
            var frame = _driver.Page.Frame(frameName);
            if (frame == null)
            {
                // Try finding by name attribute as selector
                try
                {
                    var frameElement = _driver.Page.Locator($"frame[name='{frameName}'], iframe[name='{frameName}']");
                    var count = SyncHelper.RunSync(() => frameElement.CountAsync());
                    if (count > 0)
                    {
                        var handle = SyncHelper.RunSync(() => frameElement.First.ElementHandleAsync());
                        var contentFrame = SyncHelper.RunSync(() => handle.ContentFrameAsync());
                        if (contentFrame != null)
                        {
                            _driver.CurrentFrame = contentFrame;
                            _driver.CurrentFrameLocator = null;
                            return _driver;
                        }
                    }
                }
                catch { /* fall through */ }
                throw new NoSuchFrameException($"No frame found with name: {frameName}");
            }
            _driver.CurrentFrame = frame;
            _driver.CurrentFrameLocator = null;
            return _driver;
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            if (frameElement is PlaywrightWebElement pwe)
            {
                var handle = SyncHelper.RunSync(() => pwe.Locator.ElementHandleAsync());
                var frame = SyncHelper.RunSync(() => handle.ContentFrameAsync());
                if (frame != null)
                {
                    _driver.CurrentFrame = frame;
                    _driver.CurrentFrameLocator = null;
                    return _driver;
                }
            }
            throw new NoSuchFrameException("Unable to switch to frame");
        }

        public IWebDriver ParentFrame()
        {
            if (_driver.CurrentFrame?.ParentFrame != null)
            {
                _driver.CurrentFrame = _driver.CurrentFrame.ParentFrame;
                if (_driver.CurrentFrame == _driver.Page.MainFrame)
                {
                    _driver.CurrentFrame = null;
                }
            }
            else
            {
                _driver.CurrentFrame = null;
            }
            _driver.CurrentFrameLocator = null;
            return _driver;
        }

        public IWebDriver DefaultContent()
        {
            _driver.CurrentFrame = null;
            _driver.CurrentFrameLocator = null;
            return _driver;
        }

        public IWebDriver Window(string windowName)
        {
            PlaywrightWebDriver.Trace($"SwitchTo.Window: windowName='{windowName}', pages.Count={_driver.Context.Pages.Count}");
            var pages = _driver.Context.Pages;

            // Try as index
            if (int.TryParse(windowName, out int index) && index >= 0 && index < pages.Count)
            {
                PlaywrightWebDriver.Trace($"SwitchTo.Window: switching to page index {index}, URL={pages[index].Url}");
                _driver.SetActivePage(pages[index]);
                return _driver;
            }

            // Try matching by title or URL
            foreach (var page in pages)
            {
                var title = SyncHelper.RunSync(() => page.TitleAsync());
                if (title == windowName || page.Url.Contains(windowName))
                {
                    PlaywrightWebDriver.Trace($"SwitchTo.Window: matched by title/URL, URL={page.Url}");
                    _driver.SetActivePage(page);
                    return _driver;
                }
            }

            throw new NoSuchWindowException($"No window found: {windowName}");
        }

        public IAlert Alert()
        {
            return new PlaywrightAlert(_driver);
        }

        public IWebElement ActiveElement()
        {
            var locator = _driver.Page.Locator("*:focus");
            var count = SyncHelper.RunSync(() => locator.CountAsync());
            if (count == 0)
            {
                // Return body if no focused element
                return new PlaywrightWebElement(_driver.Page.Locator("body"), _driver);
            }
            return new PlaywrightWebElement(locator.First, _driver);
        }
    }
}
