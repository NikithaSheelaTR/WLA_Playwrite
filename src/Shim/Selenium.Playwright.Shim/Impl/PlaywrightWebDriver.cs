using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Playwright.Shim.Impl
{
    public class PlaywrightWebDriver : IWebDriver, IJavaScriptExecutor, ITakesScreenshot
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        private readonly PlaywrightOptions _options;
        private readonly PlaywrightTargetLocator _targetLocator;
        private bool _disposed;

        // Track the "current" page for frame/window switching
        private IPage _activePage;

        // Track frame state for SwitchTo().Frame() / DefaultContent()
        internal IFrameLocator CurrentFrameLocator { get; set; }
        internal IFrame CurrentFrame { get; set; }

        public IPage Page => _activePage ?? _page;

        public IBrowserContext Context => _context;

        public PlaywrightWebDriver(ChromeOptions chromeOptions = null)
        {
            EnsurePlaywrightDriver();
            _playwright = SyncHelper.RunSync(() => Microsoft.Playwright.Playwright.CreateAsync());

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chromium"
            };

            // Map ChromeOptions arguments to Playwright launch options
            if (chromeOptions != null)
            {
                var args = new List<string>();
                foreach (var arg in chromeOptions.Arguments)
                {
                    // Skip Chrome-specific args that don't apply to Playwright
                    if (arg.StartsWith("force-device-scale-factor") ||
                        arg.StartsWith("high-dpi-support") ||
                        arg == "--disable-infobars" ||
                        arg == "--test-type")
                        continue;

                    args.Add(arg);
                }
                if (args.Count > 0)
                    launchOptions.Args = args;

                if (!string.IsNullOrEmpty(chromeOptions.BinaryLocation))
                    launchOptions.ExecutablePath = chromeOptions.BinaryLocation;
            }

            _browser = SyncHelper.RunSync(() => _playwright.Chromium.LaunchAsync(launchOptions));

            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                IgnoreHTTPSErrors = true
            };

            // Apply device scale factor if specified
            if (chromeOptions != null)
            {
                var scaleFactor = chromeOptions.Arguments
                    .FirstOrDefault(a => a.StartsWith("force-device-scale-factor="));
                if (scaleFactor != null)
                {
                    var parts = scaleFactor.Split('=');
                    if (parts.Length == 2 && float.TryParse(parts[1], out float scale))
                    {
                        contextOptions.DeviceScaleFactor = scale;
                    }
                }
            }

            _context = SyncHelper.RunSync(() => _browser.NewContextAsync(contextOptions));
            _page = SyncHelper.RunSync(() => _context.NewPageAsync());
            _activePage = _page;

            _options = new PlaywrightOptions(this);
            _targetLocator = new PlaywrightTargetLocator(this);
        }

        public string Url
        {
            get => Page.Url;
            set => SyncHelper.RunSync(() => Page.GotoAsync(value));
        }

        public string Title => SyncHelper.RunSync(() => Page.TitleAsync());

        public string PageSource => SyncHelper.RunSync(() => Page.ContentAsync());

        public string CurrentWindowHandle
        {
            get
            {
                var pages = _context.Pages;
                for (int i = 0; i < pages.Count; i++)
                {
                    if (pages[i] == _activePage)
                        return i.ToString();
                }
                return "0";
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                var handles = new List<string>();
                for (int i = 0; i < _context.Pages.Count; i++)
                {
                    handles.Add(i.ToString());
                }
                return handles.AsReadOnly();
            }
        }

        public IWebElement FindElement(By by)
        {
            var locatorStr = ByConverter.ToPlaywrightLocator(by);
            ILocator locator;

            if (CurrentFrame != null)
            {
                locator = CurrentFrame.Locator(locatorStr);
            }
            else
            {
                locator = Page.Locator(locatorStr);
            }

            // Wait for the element with implicit wait timeout
            var timeout = _options.GetImplicitWaitMs();
            try
            {
                SyncHelper.RunSync(() => locator.First.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = timeout
                }));
            }
            catch (PlaywrightException)
            {
                throw new NoSuchElementException($"Unable to locate element: {by}");
            }

            var count = SyncHelper.RunSync(() => locator.CountAsync());
            if (count == 0)
                throw new NoSuchElementException($"Unable to locate element: {by}");

            return new PlaywrightWebElement(locator.First, this);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var locatorStr = ByConverter.ToPlaywrightLocator(by);
            ILocator locator;

            if (CurrentFrame != null)
            {
                locator = CurrentFrame.Locator(locatorStr);
            }
            else
            {
                locator = Page.Locator(locatorStr);
            }

            var timeout = _options.GetImplicitWaitMs();
            try
            {
                SyncHelper.RunSync(() => locator.First.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = Math.Max(timeout, 1000)
                }));
            }
            catch (PlaywrightException)
            {
                return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            }

            var count = SyncHelper.RunSync(() => locator.CountAsync());
            var elements = new List<IWebElement>();
            for (int i = 0; i < count; i++)
            {
                elements.Add(new PlaywrightWebElement(locator.Nth(i), this));
            }
            return elements.AsReadOnly();
        }

        public object ExecuteScript(string script, params object[] args)
        {
            // Map IWebElement args to Playwright element handles
            var mappedArgs = MapArguments(args);

            if (args.Length > 0)
            {
                return SyncHelper.RunSync(() => Page.EvaluateAsync<object>(
                    WrapScriptForArgs(script, args.Length), mappedArgs));
            }
            return SyncHelper.RunSync(() => Page.EvaluateAsync<object>(script));
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            var mappedArgs = MapArguments(args);
            if (args.Length > 0)
            {
                return SyncHelper.RunSync(() => Page.EvaluateAsync<object>(
                    WrapAsyncScriptForArgs(script, args.Length), mappedArgs));
            }
            return SyncHelper.RunSync(() => Page.EvaluateAsync<object>(
                $"() => new Promise((resolve) => {{ var callback = resolve; {script} }})"));
        }

        public Screenshot GetScreenshot()
        {
            var bytes = SyncHelper.RunSync(() => Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Type = ScreenshotType.Png,
                FullPage = false
            }));
            return new Screenshot(bytes);
        }

        public void Close()
        {
            SyncHelper.RunSync(() => Page.CloseAsync());
            // Switch to another open page if available
            if (_context.Pages.Count > 0)
                _activePage = _context.Pages.Last();
        }

        public void Quit()
        {
            Dispose();
        }

        public IOptions Manage() => _options;

        public INavigation Navigate() => new PlaywrightNavigation(this);

        public ITargetLocator SwitchTo() => _targetLocator;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                try
                {
                    SyncHelper.RunSync(() => _context?.CloseAsync());
                    SyncHelper.RunSync(() => _browser?.CloseAsync());
                    _playwright?.Dispose();
                }
                catch { /* cleanup best effort */ }
            }
        }

        internal void SetActivePage(IPage page)
        {
            _activePage = page;
            CurrentFrame = null;
            CurrentFrameLocator = null;
        }

        private object[] MapArguments(object[] args)
        {
            if (args == null || args.Length == 0) return args;

            var mapped = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is PlaywrightWebElement pwe)
                {
                    mapped[i] = SyncHelper.RunSync(() => pwe.Locator.ElementHandleAsync());
                }
                else
                {
                    mapped[i] = args[i];
                }
            }
            return mapped;
        }

        private string WrapScriptForArgs(string script, int argCount)
        {
            // Convert Selenium-style "arguments[n]" to a function with parameters
            var paramNames = new List<string>();
            for (int i = 0; i < argCount; i++)
                paramNames.Add($"arg{i}");

            var modifiedScript = script;
            for (int i = 0; i < argCount; i++)
                modifiedScript = modifiedScript.Replace($"arguments[{i}]", $"arg{i}");

            // Handle generic "arguments" reference
            if (modifiedScript.Contains("arguments"))
            {
                modifiedScript = modifiedScript.Replace("arguments", $"[{string.Join(",", paramNames)}]");
            }

            return $"({string.Join(", ", paramNames)}) => {{ {modifiedScript} }}";
        }

        private string WrapAsyncScriptForArgs(string script, int argCount)
        {
            var paramNames = new List<string>();
            for (int i = 0; i < argCount; i++)
                paramNames.Add($"arg{i}");

            var modifiedScript = script;
            for (int i = 0; i < argCount; i++)
                modifiedScript = modifiedScript.Replace($"arguments[{i}]", $"arg{i}");

            return $"({string.Join(", ", paramNames)}) => new Promise((resolve) => {{ var callback = resolve; {modifiedScript} }})";
        }

        /// <summary>
        /// Ensures the .playwright driver folder is available at AppContext.BaseDirectory
        /// so that Microsoft.Playwright can find its Node.js driver at runtime.
        /// </summary>
        private static void EnsurePlaywrightDriver()
        {
            var baseDir = AppContext.BaseDirectory;
            var targetPlaywright = System.IO.Path.Combine(baseDir, ".playwright");
            if (System.IO.Directory.Exists(targetPlaywright))
                return;

            // Search for .playwright in several candidate locations
            string sourcePlaywright = null;
            var candidates = new List<string>();

            // 1. Next to the shim DLL
            var shimDir = System.IO.Path.GetDirectoryName(typeof(PlaywrightWebDriver).Assembly.Location);
            if (!string.IsNullOrEmpty(shimDir))
                candidates.Add(System.IO.Path.Combine(shimDir, ".playwright"));

            // 2. Next to the Microsoft.Playwright DLL
            var pwDir = System.IO.Path.GetDirectoryName(typeof(Microsoft.Playwright.IPlaywright).Assembly.Location);
            if (!string.IsNullOrEmpty(pwDir))
                candidates.Add(System.IO.Path.Combine(pwDir, ".playwright"));

            // 3. Walk up from the working directory looking for src\Shim\publish\.playwright
            var cur = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 10 && cur != null; i++)
            {
                candidates.Add(System.IO.Path.Combine(cur, "src", "Shim", "publish", ".playwright"));
                candidates.Add(System.IO.Path.Combine(cur, ".playwright"));
                cur = System.IO.Directory.GetParent(cur)?.FullName;
            }

            // 4. Walk up from AppContext.BaseDirectory
            cur = baseDir;
            for (int i = 0; i < 10 && cur != null; i++)
            {
                candidates.Add(System.IO.Path.Combine(cur, "src", "Shim", "publish", ".playwright"));
                cur = System.IO.Directory.GetParent(cur)?.FullName;
            }

            foreach (var candidate in candidates)
            {
                if (System.IO.Directory.Exists(candidate))
                {
                    sourcePlaywright = candidate;
                    break;
                }
            }

            if (sourcePlaywright != null && !System.IO.Directory.Exists(targetPlaywright))
            {
                try
                {
                    // Create a directory junction (symlink) to avoid copying large files
                    var psi = new System.Diagnostics.ProcessStartInfo("cmd.exe",
                        $"/c mklink /J \"{targetPlaywright}\" \"{sourcePlaywright}\"")
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    var proc = System.Diagnostics.Process.Start(psi);
                    proc.WaitForExit(5000);

                    if (!System.IO.Directory.Exists(targetPlaywright))
                    {
                        CopyDirectory(sourcePlaywright, targetPlaywright);
                    }
                }
                catch
                {
                    CopyDirectory(sourcePlaywright, targetPlaywright);
                }
            }
        }

        private static void CopyDirectory(string sourceDir, string destDir)
        {
            System.IO.Directory.CreateDirectory(destDir);
            foreach (var file in System.IO.Directory.GetFiles(sourceDir))
            {
                var destFile = System.IO.Path.Combine(destDir, System.IO.Path.GetFileName(file));
                System.IO.File.Copy(file, destFile, true);
            }
            foreach (var dir in System.IO.Directory.GetDirectories(sourceDir))
            {
                var destSubDir = System.IO.Path.Combine(destDir, System.IO.Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
    }
}
