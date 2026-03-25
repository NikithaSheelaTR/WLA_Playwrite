namespace Framework.Common.UI.Utils.Browser
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// Encapsulates a WebDriver object and provides convenient means of manipulation with the driver.
    /// </summary>
    public sealed class Browser : IDisposable
    {
        private readonly Dictionary<string, string> tabs = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class. 
        /// </summary>
        /// <param name="supportedBrowser"> The browser </param>
        /// <param name="driverLocation"> Driver Location </param>
        public Browser(TestClientInfo supportedBrowser, string driverLocation)
        {
            this.IsDisposed = true;
            this.BrowserInfo = supportedBrowser;
            this.Driver = DriverFactory.GetWebDriver(supportedBrowser, driverLocation);
            this.CreateTab();
            this.IsDisposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class. 
        /// </summary>
        /// <param name="supportedBrowser"> Type of Browser </param>
        /// <param name="browserOptions"> Browser options </param>
        /// <param name="driverLocation"> Driver Location </param>
        public Browser(TestClientInfo supportedBrowser, object browserOptions, string driverLocation)
        {
            this.IsDisposed = true;
            this.BrowserInfo = supportedBrowser;
            this.Driver = DriverFactory.GetWebDriver(supportedBrowser.Id, browserOptions, supportedBrowser.RemoteDriverUri, driverLocation);
            this.CreateTab();
            this.IsDisposed = false;
        }

        /// <summary>
        /// The type of a browser.
        /// </summary>
        public TestClientInfo BrowserInfo { get; set; }

        /// <summary>
        /// The current window handle.
        /// </summary>
        public string CurrentWindowHandle => this.Driver.CurrentWindowHandle;

        /// <summary>
        /// Reference to the encompassed WedDriver object
        /// TODO Remove after moving to new QualityLibrary was completed
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// Indicates if a browser object can be used.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Returns Action instance
        /// </summary>
        /// <returns> The <see cref="Actions"/>. </returns>
        public Actions ActionInstance => new Actions(this.Driver);

        /// <summary>
        /// Tab Handles
        /// </summary>
        public ReadOnlyCollection<string> TabHandles => this.Driver.WindowHandles;

        /// <summary>
        /// Browser Tab Names collection
        /// </summary>
        public List<string> TabNames => this.tabs.Values.ToList();

        /// <summary>
        /// Browser title
        /// </summary>
        public string Title => this.Driver.Title;

        /// <summary>
        /// URL
        /// </summary>
        public string Url => this.Driver.Url;

        /// <summary>
        /// Activate browser tab by its name.
        /// </summary>
        /// <param name="tabName">The tab to activate.</param>
        public void ActivateTab(string tabName)
        {
            this.Driver.SwitchTo()
                .Window(
                    this.tabs.ContainsValue(tabName)
                        ? this.tabs.First(x => x.Value == tabName).Key
                        : this.tabs.Keys.Last());
        }

        /// <summary>
        /// Close browser tab
        /// </summary>
        /// <param name="tabName">tab name</param>
        public void CloseTab(string tabName)
        {
            this.Driver.SwitchTo().Window(this.tabs.First(x => x.Value == tabName).Key).Close();
            this.DeleteTab(tabName);
        }

        /// <summary>
        /// Create new browser tab
        /// </summary>
        /// <param name="tabName">The name of a tab to register in the internal tab pool.</param>
        public void CreateTab(string tabName = null)
        {
            string lastTabHandle = this.Driver.WindowHandles.Last();

            if (string.IsNullOrEmpty(tabName))
            {
                tabName = this.Driver.CurrentWindowHandle;
            }

            if (!this.tabs.ContainsKey(lastTabHandle))
            {
                if (!this.tabs.ContainsValue(tabName))
                {
                    this.tabs.Add(lastTabHandle, tabName);
                }
                else
                {
                    throw new ArgumentException("Tab name is not unique");
                }
            }
        }

        /// <summary>
        /// Disposes of the Browser object.
        /// </summary>
        public void Dispose()
        {
            this.DisposeInternal();
        }

		/// <summary>
		/// Switch Browser to fullscreen mode
		/// </summary>
	    public void SwitchToFullScreenMode()
	    {
			this.Driver.Manage().Window.FullScreen();
	    }

	    /// <summary>
        /// Gets the collection of cookies.
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection{T}"/> of cookies.
        /// </returns>
        public ICollection<Cookie> GetCookies()
        {
            this.VerifyIsDisposed();
            return this.Driver.Manage().Cookies.AllCookies;
        }

        /// <summary>
        /// Return the value of the cookie for the passed in cookie name.
        /// </summary>
        /// <param name="cookieName">The name of the cookie whose value will be returned.</param>
        /// <returns>The value of the cookie or NULL.</returns>
        public string GetCookieValue(string cookieName)
        {
            Cookie cookie = null;

            var result =
                SafeMethodExecutor.Execute(
                    () =>
                        {
                            cookie =
                                this.GetCookies()
                                    .FirstOrDefault(
                                        c =>
                                            string.Equals(
                                                c.Name,
                                                cookieName,
                                                StringComparison.InvariantCultureIgnoreCase));
                        });
            result.LogDetails();

            return result.ResultType == ExecutionResultType.Success && cookie != null ? cookie.Value : null;
        }

        /// <summary>
        /// Add cookie to Browser
        /// </summary>
        /// <param name="name">Cookie name</param>
        /// <param name="value">Cookie value</param>
        /// <param name="domain">Domain</param>
        /// <param name="path">Path</param>
        /// <param name="expiry">Expiration datetime</param>
        public void AddCookie(string name, string value, string domain="", string path="", DateTime? expiry=null)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                throw new Exception("Provided cookie is not valid");
            }

            Cookie cookie;
            if (domain.Equals(string.Empty) && path.Equals(string.Empty) && expiry==null)
            {
                cookie = new Cookie(name, value);
            }
            else if (domain.Equals(string.Empty) && expiry == null)
            {
                cookie = new Cookie(name, value, path);
            }
            else if (domain.Equals(string.Empty))
            {
                cookie = new Cookie(name, value, path, expiry);
            }
            else
            {
                cookie = new Cookie(name, value, domain, path, expiry);
            }

            this.InvokeAction(wd=> wd.Manage().Cookies.AddCookie(cookie));
        }

        /// <summary>
        /// Navigate back
        /// </summary>
        public void GoBack()
        {
            this.InvokeAction(wd => wd.Navigate().Back());
        }

        /// <summary>
        /// Navigate back
        /// </summary>
        /// <typeparam name="T">Page Class to return</typeparam>
        /// <returns>Page class of type T</returns>
        public T GoBack<T>() where T : ICreatablePageObject
        {
            this.GoBack();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigate forward
        /// </summary>
        public void GoForward()
        {
            this.InvokeAction(wd => wd.Navigate().Forward());
        }

        /// <summary>
        /// Navigate forward
        /// </summary>
        /// <typeparam name="T">Page Class to return</typeparam>
        /// <returns>Page class of type T</returns>
        public T GoForward<T>() where T : ICreatablePageObject
        {
            this.GoForward();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigates directly to the path for the given host
        /// </summary>
        /// <param name="path">path to navigate to</param>
        /// <typeparam name="T">Page Class to return</typeparam>
        /// <returns>Page class of type T</returns>
        public T GoToPath<T>(string path) where T : ICreatablePageObject
        {
            string fullUrl = new Uri(new Uri(this.Url), path).ToString();

            // use GoToUrl with the provided path and domain
            return BrowserPool.CurrentBrowser.GoToUrl<T>(fullUrl);
        }

        /// <summary>
        /// Navigates to specified URL.
        /// </summary>
        /// <param name="url">URL</param>
        public void GoToUrl(string url)
        {
            this.InvokeAction(wd => wd.Navigate().GoToUrl(url));
        }

        /// <summary>
        /// Navigates to specified URL.
        /// </summary>
        /// <param name="url">
        /// URL to navigate to
        /// </param>
        /// <typeparam name="T">Page Class to return</typeparam>
        /// <returns>Page class of type T</returns>
        public T GoToUrl<T>(string url) where T : ICreatablePageObject
        {
            this.GoToUrl(url);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Invokes a WebDriver extension that does not return the result of execution.
        /// </summary>
        /// <param name="action">WebWeb driver extension that does not return a result.</param>
        public void InvokeAction(Action<IWebDriver> action)
        {
            this.VerifyIsDisposed();

            action?.Invoke(this.Driver);
        }

        /// <summary>
        /// Invokes a WebDriver extension that returns the result of execution.
        /// </summary>
        /// <param name="func">Web driver extension that returns a result.</param>
        /// <typeparam name="T">The type of the returned result.</typeparam>
        /// <returns>The result of execution of WebDriver extension.</returns>
        public T InvokeFunc<T>(Func<IWebDriver, T> func)
        {
            this.VerifyIsDisposed();

            return func != null ? func(this.Driver) : default(T);
        }

        /// <summary>
        /// Maximizes the current browser window.
        /// </summary>
        public void Maximize()
        {
            this.InvokeAction(wd => wd.Manage().Window.Maximize());
        }

        /// <summary>
        /// Reloading web page to view the updated data for working with regression tests
        /// </summary>
        public void Refresh()
        {
            this.InvokeAction(wd => wd.Navigate().Refresh());
        }

        /// <summary>
        /// Reloading web page to view the updated data for working with regression tests
        /// </summary>
        /// <typeparam name="T">Page Class to return</typeparam>
        /// <returns>Page class of type T</returns>
        public T Refresh<T>() where T : ICreatablePageObject
        {
            this.Refresh();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Takes a screenshot and saves it in the file in the specified format.
        /// </summary>
        /// <param name="fileName">The name of the file with a screenshot.</param>
        /// <param name="imgFormat">The format to save an image in.</param>
        public void TakeScreenshot(string fileName, ScreenshotImageFormat imgFormat) => this.GetScreenshot().SaveAsFile(fileName, imgFormat);


	    /// <summary>
	    /// Get screenshot.
	    /// </summary>
	    /// <returns> The <see cref="Screenshot"/> ScreenShot </returns>
	    public Screenshot GetScreenshot() => ((ITakesScreenshot)this.Driver).GetScreenshot();

		/// <summary>
		/// SwitchToFrame. If locator is null switch to default content
		/// </summary>
		/// <param name="elementLocator"> Element Locator </param>
		public void SwitchToFrame(By elementLocator = null)
        {
            if (elementLocator != null)
            {
                this.Driver.SwitchTo().Frame(DriverExtensions.WaitForElement(elementLocator));
            }
            else
            {
                this.Driver.SwitchTo().DefaultContent();
            }
        }
        
        /// <summary>
        /// Set window size
        /// </summary>
        public void SetWindowSize(int width, int height)
        {
            this.InvokeAction(wd => wd.Manage().Window.Size = new Size(width, height));
        }

        private void DeleteTab(string tabName)
        {
            if (this.tabs.ContainsValue(tabName))
            {
                this.tabs.Remove(this.tabs.First(x => x.Value == tabName).Key);
            }
        }

        private void DisposeInternal()
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.Driver != null)
            {
                this.Driver.Quit();
                this.Driver = null;
            }

            if (this.tabs != null)
            {
                this.tabs.Clear();
            }

            this.IsDisposed = true;
        }

        private void VerifyIsDisposed()
        {
            if (this.IsDisposed)
            {
                throw new InvalidOperationException("Browser is in disposed state");
            }
        }
    }
}