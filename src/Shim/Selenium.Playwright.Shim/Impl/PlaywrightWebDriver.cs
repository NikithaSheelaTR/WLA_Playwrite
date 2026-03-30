using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
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
        private string _downloadDirectory;
        private static string _shimDirectory;
        private static string _publishDirectory;
        private static readonly string _traceFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "playwright_shim_trace.log");

        internal static void Trace(string message)
        {
            try
            {
                var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
                System.IO.File.AppendAllText(_traceFile, line + Environment.NewLine);
                System.Console.WriteLine(line);
            }
            catch { /* best effort */ }
        }

        // Track the "current" page for frame/window switching
        private IPage _activePage;

        // Track frame state for SwitchTo().Frame() / DefaultContent()
        internal IFrameLocator CurrentFrameLocator { get; set; }
        internal IFrame CurrentFrame { get; set; }

        public IPage Page => _activePage ?? _page;

        public IBrowserContext Context => _context;

        /// <summary>
        /// Static constructor: runs once before any instance is created.
        /// Sets up assembly resolution so all Playwright BCL dependencies 
        /// (System.ComponentModel.Annotations, System.Text.Json, etc.) 
        /// can be found even under vstest deployment.
        /// </summary>
        static PlaywrightWebDriver()
        {
            _shimDirectory = System.IO.Path.GetDirectoryName(
                typeof(PlaywrightWebDriver).Assembly.Location);

            // Find the publish folder (where ALL dependencies live)
            _publishDirectory = FindPublishDirectory();

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var name = new System.Reflection.AssemblyName(args.Name).Name;

                // Check where the shim DLL is loaded from (bin\Debug or vstest deploy)
                var dll = System.IO.Path.Combine(_shimDirectory, name + ".dll");
                if (System.IO.File.Exists(dll))
                    return System.Reflection.Assembly.LoadFrom(dll);

                // Fall back to publish folder (has all BCL deps)
                if (_publishDirectory != null)
                {
                    dll = System.IO.Path.Combine(_publishDirectory, name + ".dll");
                    if (System.IO.File.Exists(dll))
                        return System.Reflection.Assembly.LoadFrom(dll);
                }

                return null;
            };
        }

        /// <summary>
        /// Walks up from the shim DLL directory looking for src\Shim\publish\.
        /// Also checks from the Playwright DLL location, AppContext.BaseDirectory,
        /// and the original source path (from PDB) to handle vstest deploy scenarios
        /// where DLLs are copied to a different tree.
        /// </summary>
        private static string FindPublishDirectory()
        {
            var startDirs = new List<string>
            {
                _shimDirectory,
                System.IO.Path.GetDirectoryName(typeof(Microsoft.Playwright.Playwright).Assembly.Location),
                AppContext.BaseDirectory
            };

            // Try to find the original source path from the PDB/debug info
            // The shim DLL was compiled from src\Shim\Selenium.Playwright.Shim\
            // so the PDB should contain the original path
            try
            {
                var shimAssemblyPath = typeof(PlaywrightWebDriver).Assembly.Location;
                var pdbPath = System.IO.Path.ChangeExtension(shimAssemblyPath, ".pdb");
                if (System.IO.File.Exists(pdbPath))
                {
                    // Read first 4KB of PDB to find source path references
                    var pdbBytes = System.IO.File.ReadAllBytes(pdbPath);
                    var pdbText = System.Text.Encoding.UTF8.GetString(pdbBytes);
                    // Look for paths like C:\Github\WLA_Playwrite\src\Shim\...
                    var match = System.Text.RegularExpressions.Regex.Match(
                        pdbText, @"([A-Za-z]:\\[^\0]*?\\src\\Shim\\)");
                    if (match.Success)
                    {
                        var srcShimDir = match.Groups[1].Value;
                        var publishFromPdb = System.IO.Path.Combine(srcShimDir, "publish");
                        if (System.IO.Directory.Exists(publishFromPdb))
                            return publishFromPdb;
                        // Also try the parent dir
                        startDirs.Add(System.IO.Path.GetDirectoryName(srcShimDir.TrimEnd('\\')));
                    }
                }
            }
            catch { /* best effort */ }

            // Also scan common Git repo locations
            var drives = new[] { "C" };
            var githubDirs = new[] { "Github", "git", "repos", "source" };
            foreach (var drive in drives)
            {
                foreach (var gitDir in githubDirs)
                {
                    var gitRoot = $@"{drive}:\{gitDir}";
                    if (System.IO.Directory.Exists(gitRoot))
                    {
                        try
                        {
                            foreach (var repoDir in System.IO.Directory.GetDirectories(gitRoot))
                            {
                                var candidate = System.IO.Path.Combine(repoDir, "src", "Shim", "publish");
                                if (System.IO.Directory.Exists(candidate) &&
                                    System.IO.File.Exists(System.IO.Path.Combine(candidate, "Selenium.Playwright.Shim.dll")))
                                    return candidate;
                            }
                        }
                        catch { /* access denied, etc. */ }
                    }
                }
            }

            foreach (var startDir in startDirs)
            {
                if (string.IsNullOrEmpty(startDir)) continue;
                var dir = startDir;
                for (int i = 0; i < 15 && dir != null; i++)
                {
                    var candidate = System.IO.Path.Combine(dir, "src", "Shim", "publish");
                    if (System.IO.Directory.Exists(candidate) &&
                        System.IO.File.Exists(System.IO.Path.Combine(candidate, "Selenium.Playwright.Shim.dll")))
                        return candidate;
                    dir = System.IO.Directory.GetParent(dir)?.FullName;
                }
            }
            return null;
        }

        public PlaywrightWebDriver(ChromeOptions chromeOptions = null)
        {
            Trace($"Constructor starting. ShimDir={_shimDirectory}, PublishDir={_publishDirectory}");
            EnsurePlaywrightDriver();
            Trace("EnsurePlaywrightDriver done. Creating Playwright...");
            _playwright = SyncHelper.RunSync(() => Microsoft.Playwright.Playwright.CreateAsync());
            Trace("Playwright created. Launching browser...");

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chrome"  // Use system-installed Chrome (has corporate proxy/certs)
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
            Trace("Browser launched. Creating context...");

            // Check if --start-maximized is in the args
            bool startMaximized = chromeOptions != null &&
                chromeOptions.Arguments.Any(a => a == "--start-maximized");

            // Extract download directory from ChromeOptions user profile preferences.
            // WlaBaseTest sets "download.default_directory" via AddUserProfilePreference;
            // we must honour that so file-download tests can find their files.
            if (chromeOptions != null &&
                chromeOptions.UserProfilePreferences.TryGetValue("download.default_directory", out var rawDownloadDir))
            {
                _downloadDirectory = rawDownloadDir as string;
                if (!string.IsNullOrEmpty(_downloadDirectory) && !System.IO.Directory.Exists(_downloadDirectory))
                    System.IO.Directory.CreateDirectory(_downloadDirectory);
            }

            var contextOptions = new BrowserNewContextOptions
            {
                IgnoreHTTPSErrors = true,
                Permissions = new[] { "clipboard-read", "clipboard-write" },
                AcceptDownloads = true
            };

            // When --start-maximized is used, set ViewportSize to No Viewport
            // so the page content area matches the actual maximized window.
            // ViewportSize.NoViewport tells Playwright not to override the viewport.
            if (startMaximized)
            {
                contextOptions.ViewportSize = ViewportSize.NoViewport;
            }
            else
            {
                contextOptions.ViewportSize = new ViewportSize { Width = 1920, Height = 1080 };
            }

            // Apply device scale factor if specified (only when viewport is set,
            // Playwright does not allow deviceScaleFactor with null viewport)
            if (chromeOptions != null && !startMaximized)
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

            // Intercept clipboard writes so readText() works reliably without
            // page focus (which automated browsers often lack).
            SyncHelper.RunSync(() => _context.AddInitScriptAsync(@"
                (function() {
                    window.__shimClipboardData = '';
                    const orig = navigator.clipboard.writeText.bind(navigator.clipboard);
                    navigator.clipboard.writeText = function(text) {
                        window.__shimClipboardData = text;
                        return orig(text);
                    };
                    const origRead = navigator.clipboard.readText.bind(navigator.clipboard);
                    navigator.clipboard.readText = function() {
                        if (window.__shimClipboardData) {
                            return Promise.resolve(window.__shimClipboardData);
                        }
                        return origRead();
                    };
                    // Also capture execCommand('copy') which copies selected text
                    document.addEventListener('copy', function(e) {
                        var sel = window.getSelection();
                        if (sel && sel.toString()) {
                            window.__shimClipboardData = sel.toString();
                        }
                    });
                })();
            "));

            // Reduce Playwright's default auto-wait timeout from 30s to 5s.
            // Selenium does NOT auto-wait on element operations (Click, Text,
            // GetAttribute, etc.) — they return or throw immediately. Playwright
            // auto-waits on every action. 5s is a compromise: short enough so the
            // test framework's WebDriverWait can actually retry, long enough for
            // normal scroll/animation delays.
            _context.SetDefaultTimeout(5000);
            _context.SetDefaultNavigationTimeout(60000);

            Trace("Context created. Creating page...");
            _page = SyncHelper.RunSync(() => _context.NewPageAsync());
            _activePage = _page;
            AttachPopupHandler(_page);
            Trace("Page created. Initializing options...");

            _options = new PlaywrightOptions(this);
            _targetLocator = new PlaywrightTargetLocator(this);
            Trace("Constructor completed successfully.");
        }

        public string Url
        {
            get => Page.Url;
            set
            {
                try
                {
                    SyncHelper.RunSync(() => Page.GotoAsync(value, new PageGotoOptions
                    {
                        WaitUntil = WaitUntilState.Load,
                        Timeout = 60000
                    }));
                    CurrentFrame = null;
                    CurrentFrameLocator = null;
                    Trace($"Url setter completed: {value} (actual: {Page.Url})");
                }
                catch (PlaywrightException ex)
                {
                    Trace($"Url setter failed: {ex.Message}");
                    throw new OpenQA.Selenium.WebDriverException($"Navigation to '{value}' failed: {ex.Message}", ex);
                }
            }
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
                // After a click that opens a popup (target="_blank", window.open),
                // the new page may not yet be in Context.Pages when this is called.
                // Wait briefly for the popup to be registered.
                if (_expectPopup)
                {
                    var deadline = DateTime.UtcNow.AddMilliseconds(3000);
                    var initialCount = _context.Pages.Count;
                    while (_context.Pages.Count == initialCount && DateTime.UtcNow < deadline)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    _expectPopup = false;
                }

                var handles = new List<string>();
                for (int i = 0; i < _context.Pages.Count; i++)
                {
                    handles.Add(i.ToString());
                }
                Trace($"WindowHandles: count={handles.Count}, pages=[{string.Join(", ", _context.Pages.Select((p, i) => i + ":" + p.Url.Substring(0, Math.Min(60, p.Url.Length))))}]");
                return handles.AsReadOnly();
            }
        }

        public IWebElement FindElement(By by)
        {
            Trace($"FindElement [{Page.Url}]: {by}");

            // Handle custom By subclasses (e.g. ByChained) that override FindElement
            if (by.Mechanism == null)
                return by.FindElement(this);

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

            // Wait for the element with implicit wait timeout.
            // IMPORTANT: Use a short timeout here so that WebDriverWait (the outer
            // polling loop in WaitForElement) can retry rapidly. If we block for the
            // full implicit wait inside FindElement, WebDriverWait never gets a
            // chance to re-poll and times out after a single attempt.
            var timeoutMs = _options.GetImplicitWaitMs();

            // Cap the per-attempt wait to 2 seconds. The outer WebDriverWait loop
            // (typically 30s) handles the overall timeout by calling FindElement
            // repeatedly, catching NoSuchElementException on each miss.
            var perAttemptTimeout = timeoutMs <= 0 ? 0 : Math.Min(timeoutMs, 2000);

            if (perAttemptTimeout <= 0)
            {
                // Selenium ImplicitWait=0 means "don't wait, fail immediately if not found".
                // Use CountAsync() which checks immediately without waiting.
                int count;
                try
                {
                    count = SyncHelper.RunSync(() => locator.CountAsync());
                }
                catch (Exception)
                {
                    // PlaywrightException (e.g. "Execution context was destroyed") can be thrown
                    // during navigation — convert to NoSuchElementException so WebDriverWait can
                    // catch it and retry on the next poll cycle.
                    throw new NoSuchElementException($"Unable to locate element: {by}");
                }
                if (count == 0)
                    throw new NoSuchElementException($"Unable to locate element: {by}");
                return new PlaywrightWebElement(locator.First, this);
            }

            try
            {
                SyncHelper.RunSync(() => locator.First.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = perAttemptTimeout
                }));
            }
            catch (Exception)
            {
                throw new NoSuchElementException($"Unable to locate element: {by}");
            }

            int count2;
            try
            {
                count2 = SyncHelper.RunSync(() => locator.CountAsync());
            }
            catch (Exception)
            {
                throw new NoSuchElementException($"Unable to locate element: {by}");
            }
            if (count2 == 0)
                throw new NoSuchElementException($"Unable to locate element: {by}");

            return new PlaywrightWebElement(locator.First, this);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            // Handle custom By subclasses (e.g. ByChained) that override FindElements
            if (by.Mechanism == null)
                return by.FindElements(this);

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
            if (timeout <= 0)
            {
                // Selenium ImplicitWait=0 means "don't wait, return immediately".
                int count;
                try
                {
                    count = SyncHelper.RunSync(() => locator.CountAsync());
                }
                catch (Exception)
                {
                    return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
                }
                if (count == 0)
                    return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
                var elements = new List<IWebElement>();
                for (int i = 0; i < count; i++)
                    elements.Add(new PlaywrightWebElement(locator.Nth(i), this));
                return elements.AsReadOnly();
            }

            try
            {
                SyncHelper.RunSync(() => locator.First.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = Math.Min(timeout, 2000)
                }));
            }
            catch (Exception)
            {
                return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            }

            {
                int count;
                try
                {
                    count = SyncHelper.RunSync(() => locator.CountAsync());
                }
                catch (Exception)
                {
                    return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
                }
                var elements = new List<IWebElement>();
                for (int i = 0; i < count; i++)
                    elements.Add(new PlaywrightWebElement(locator.Nth(i), this));
                return elements.AsReadOnly();
            }
        }

        public object ExecuteScript(string script, params object[] args)
        {
            Trace($"ExecuteScript: script='{script}', args.Length={args?.Length ?? 0}");

            // For scripts with a single element arg, evaluate via JavaScript on the locator
            // This exactly matches Selenium's IJavaScriptExecutor behavior (runs JS in browser context).
            if (args != null && args.Length == 1 && args[0] is PlaywrightWebElement singleElement)
            {
                var modScript = script.Replace("arguments[0]", "element");
                Trace($"ExecuteScript (single element): modScript='{modScript}'");
                var handle = SyncHelper.RunSync(() => singleElement.Locator.EvaluateHandleAsync(
                    $"(element) => {{ {modScript} }}"));
                var elementHandle = handle.AsElement();
                if (elementHandle != null)
                {
                    // Script returned a DOM element — stamp with unique attr and wrap as IWebElement
                    var uid = "shim-" + Guid.NewGuid().ToString("N");
                    SyncHelper.RunSync(() => elementHandle.EvaluateAsync(
                        "(el, id) => el.setAttribute('data-shim-id', id)", uid));
                    var locator = Page.Locator($"[data-shim-id='{uid}']");
                    Trace($"ExecuteScript (single element) result: DOM element, shim-id={uid}");
                    return new PlaywrightWebElement(locator, this);
                }
                var result = SyncHelper.RunSync(() => handle.JsonValueAsync<object>());
                Trace($"ExecuteScript (single element) result: {result}, URL after: {Page.Url}");
                return result;
            }

            // No element args — evaluate script directly
            if (args == null || args.Length == 0)
            {
                // Selenium allows scripts starting with "return" — Playwright needs an expression
                var evalScript = script.TrimStart();
                if (evalScript.StartsWith("return ", StringComparison.Ordinal) || evalScript.StartsWith("return;", StringComparison.Ordinal))
                    evalScript = "() => { " + evalScript + " }";
                Trace($"ExecuteScript (no args): original='{script}', eval='{evalScript}'");
                try
                {
                    var result = SyncHelper.RunSync(() => Page.EvaluateAsync<object>(evalScript));
                    Trace($"ExecuteScript result: {result}");
                    return result;
                }
                catch (PlaywrightException ex)
                {
                    // Page is mid-navigation (execution context destroyed) — return null so callers
                    // like WaitForPageLoad treat this as "not ready yet" and keep polling.
                    Trace($"ExecuteScript (no args) PlaywrightException swallowed: {ex.Message}");
                    return null;
                }
            }

            // Multiple non-element args — pass as plain values
            var mappedArgs = MapNonElementArguments(args);
            try
            {
                return SyncHelper.RunSync(() => Page.EvaluateAsync<object>(
                    WrapScriptForArgs(script, args.Length), mappedArgs));
            }
            catch (PlaywrightException ex)
            {
                Trace($"ExecuteScript (multi-args) PlaywrightException swallowed: {ex.Message}");
                return null;
            }
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            var mappedArgs = MapNonElementArguments(args);
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
                FullPage = false,
                Timeout = 30000
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

        // Flag to indicate a recent click might have opened a popup
        private volatile bool _expectPopup;

        /// <summary>
        /// Called by PlaywrightWebElement.Click when the click might open a new page.
        /// </summary>
        internal void NotifyClickMayOpenPopup()
        {
            _expectPopup = true;
        }

        /// <summary>
        /// Attach a Popup handler to a page so that new tabs/windows opened by clicks
        /// are properly tracked and available via WindowHandles.
        /// Also attaches a Download handler so files are saved to the configured download directory.
        /// </summary>
        private void AttachPopupHandler(IPage page)
        {
            page.Popup += (_, popup) =>
            {
                Trace($"Popup detected: {popup.Url}");
                AttachPopupHandler(popup); // track nested popups
            };

            // Save downloads to the configured directory (mirrors Chrome's download.default_directory).
            page.Download += (_, download) =>
            {
                var dir = _downloadDirectory;
                if (string.IsNullOrEmpty(dir)) return;
                try
                {
                    var destPath = System.IO.Path.Combine(dir, download.SuggestedFilename);
                    Trace($"Download started: saving '{download.SuggestedFilename}' to '{destPath}'");
                    SyncHelper.RunSync(() => download.SaveAsAsync(destPath));
                    Trace($"Download saved: '{destPath}'");
                }
                catch (Exception ex)
                {
                    Trace($"Download save failed: {ex.Message}");
                }
            };
        }

        internal void SetActivePage(IPage page)
        {
            Trace($"SetActivePage: switching to page URL={page.Url}");
            _activePage = page;
            CurrentFrame = null;
            CurrentFrameLocator = null;
        }

        private static object[] MapNonElementArguments(object[] args)
        {
            if (args == null || args.Length == 0) return args;

            var mapped = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
                mapped[i] = args[i]; // only plain values — element args handled before this point
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
        /// Ensures Playwright can find its Node.js driver.
        /// Sets the PLAYWRIGHT_DRIVER_SEARCH_PATH environment variable to point
        /// to the .playwright folder in the publish directory.
        /// This is more reliable than junctions under vstest deploy scenarios.
        /// </summary>
        private static void EnsurePlaywrightDriver()
        {
            // Find where .playwright source lives
            string playwrightPath = null;

            // Check next to the shim DLL
            var shimCandidate = System.IO.Path.Combine(_shimDirectory, ".playwright");
            if (System.IO.Directory.Exists(shimCandidate))
                playwrightPath = shimCandidate;

            // Check in publish folder
            if (playwrightPath == null && _publishDirectory != null)
            {
                var pubCandidate = System.IO.Path.Combine(_publishDirectory, ".playwright");
                if (System.IO.Directory.Exists(pubCandidate))
                    playwrightPath = pubCandidate;
            }

            if (playwrightPath == null)
            {
                Trace("WARNING: .playwright driver folder not found!");
                return;
            }

            // Set env var so Playwright.CreateAsync() finds the driver
            Trace($"Setting PLAYWRIGHT_DRIVER_SEARCH_PATH={playwrightPath}");
            Environment.SetEnvironmentVariable("PLAYWRIGHT_DRIVER_SEARCH_PATH", playwrightPath);

            // Also create a junction at AppContext.BaseDirectory as fallback
            var baseDir = AppContext.BaseDirectory;
            var target = System.IO.Path.Combine(baseDir, ".playwright");
            if (!System.IO.Directory.Exists(target) && System.IO.Directory.Exists(baseDir))
            {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo("cmd.exe",
                        $"/c mklink /J \"{target}\" \"{playwrightPath}\"")
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    var proc = System.Diagnostics.Process.Start(psi);
                    proc.WaitForExit(5000);
                }
                catch { /* best effort */ }
            }
        }
    }
}
