using System;
using System.Drawing;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal class PlaywrightOptions : IOptions
    {
        private readonly PlaywrightWebDriver _driver;
        private readonly PlaywrightCookieJar _cookies;
        private readonly PlaywrightWindow _window;
        private readonly PlaywrightTimeouts _timeouts;

        public PlaywrightOptions(PlaywrightWebDriver driver)
        {
            _driver = driver;
            _cookies = new PlaywrightCookieJar(driver);
            _window = new PlaywrightWindow(driver);
            _timeouts = new PlaywrightTimeouts();
        }

        public ICookieJar Cookies => _cookies;

        public ITimeouts Timeouts() => _timeouts;

        public IWindow Window => _window;

        internal float GetImplicitWaitMs() => (float)_timeouts.ImplicitWait.TotalMilliseconds;
    }

    internal class PlaywrightTimeouts : ITimeouts
    {
        public TimeSpan ImplicitWait { get; set; } = TimeSpan.FromSeconds(0);
        public TimeSpan PageLoad { get; set; } = TimeSpan.FromSeconds(300);
        public TimeSpan AsynchronousJavaScript { get; set; } = TimeSpan.FromSeconds(30);
    }

    internal class PlaywrightWindow : IWindow
    {
        private readonly PlaywrightWebDriver _driver;

        public PlaywrightWindow(PlaywrightWebDriver driver)
        {
            _driver = driver;
        }

        public Point Position
        {
            get => Point.Empty;
            set { /* Playwright doesn't support window positioning */ }
        }

        public Size Size
        {
            get
            {
                var size = SyncHelper.RunSync(() =>
                    _driver.Page.EvaluateAsync<int[]>("() => [window.innerWidth, window.innerHeight]"));
                return new Size(size[0], size[1]);
            }
            set
            {
                SyncHelper.RunSync(() => _driver.Page.SetViewportSizeAsync(value.Width, value.Height));
            }
        }

        public void Maximize()
        {
            SyncHelper.RunSync(() => _driver.Page.SetViewportSizeAsync(1920, 1080));
        }

        public void Minimize()
        {
            SyncHelper.RunSync(() => _driver.Page.SetViewportSizeAsync(800, 600));
        }

        public void FullScreen()
        {
            SyncHelper.RunSync(() => _driver.Page.SetViewportSizeAsync(1920, 1080));
        }
    }

    internal class PlaywrightCookieJar : ICookieJar
    {
        private readonly PlaywrightWebDriver _driver;

        public PlaywrightCookieJar(PlaywrightWebDriver driver)
        {
            _driver = driver;
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<Cookie> AllCookies
        {
            get
            {
                var pwCookies = SyncHelper.RunSync(() => _driver.Context.CookiesAsync());
                var cookies = new System.Collections.Generic.List<Cookie>();
                foreach (var c in pwCookies)
                {
                    var cookie = new Cookie(c.Name, c.Value, c.Domain, c.Path,
                        DateTimeOffset.FromUnixTimeSeconds((long)c.Expires).DateTime);
                    cookie.Secure = c.Secure;
                    cookie.IsHttpOnly = c.HttpOnly;
                    cookies.Add(cookie);
                }
                return cookies.AsReadOnly();
            }
        }

        public void AddCookie(Cookie cookie)
        {
            var pwCookie = new Microsoft.Playwright.Cookie
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Domain = string.IsNullOrEmpty(cookie.Domain) ? null : cookie.Domain,
                Path = cookie.Path ?? "/",
                Secure = cookie.Secure,
                HttpOnly = cookie.IsHttpOnly
            };

            if (cookie.Expiry.HasValue)
                pwCookie.Expires = new DateTimeOffset(cookie.Expiry.Value).ToUnixTimeSeconds();

            // If domain is not set, use current page domain
            if (string.IsNullOrEmpty(pwCookie.Domain))
            {
                var uri = new Uri(_driver.Page.Url);
                pwCookie.Domain = uri.Host;
            }

            SyncHelper.RunSync(() => _driver.Context.AddCookiesAsync(
                new[] { pwCookie }));
        }

        public Cookie GetCookieNamed(string name)
        {
            var cookies = AllCookies;
            foreach (var c in cookies)
            {
                if (c.Name == name) return c;
            }
            return null;
        }

        public void DeleteCookie(Cookie cookie)
        {
            DeleteCookieNamed(cookie.Name);
        }

        public void DeleteCookieNamed(string name)
        {
            // Playwright doesn't have delete-by-name; clear and re-add all except the target
            var all = SyncHelper.RunSync(() => _driver.Context.CookiesAsync());
            SyncHelper.RunSync(() => _driver.Context.ClearCookiesAsync());
            var remaining = new System.Collections.Generic.List<Microsoft.Playwright.Cookie>();
            foreach (var c in all)
            {
                if (c.Name != name)
                {
                    remaining.Add(new Microsoft.Playwright.Cookie
                    {
                        Name = c.Name,
                        Value = c.Value,
                        Domain = c.Domain,
                        Path = c.Path,
                        Secure = c.Secure,
                        HttpOnly = c.HttpOnly,
                        Expires = c.Expires
                    });
                }
            }
            if (remaining.Count > 0)
                SyncHelper.RunSync(() => _driver.Context.AddCookiesAsync(remaining));
        }

        public void DeleteAllCookies()
        {
            SyncHelper.RunSync(() => _driver.Context.ClearCookiesAsync());
        }
    }
}
