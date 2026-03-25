namespace Framework.Common.UI.Utils.Browser
{
    using System;
    using System.Drawing;
    using System.Threading;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Verification;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Safari;
    using Selenium.Playwright.Shim.Impl;

    /// <summary>
    /// DriverFactory has methods to create specific driver objects for a given browser
    /// </summary>
    internal static class DriverFactory
    {
        /// <summary>
        /// Retrieves browser options for the specified version of a browser
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        internal static object GetBrowserOptions(TestClientInfo browser)
        {
            object browserOptions = null;

            switch (browser.Id)
            {
                case TestClientId.Chrome:
                    browserOptions = DriverFactory.GetChromeOptions(browser.PathToExecutable);
                    break;
                case TestClientId.ChromeCanary:
                    browserOptions = DriverFactory.GetChromeCanaryOptions(browser.PathToExecutable);
                    break;
                case TestClientId.Firefox:
                case TestClientId.FirefoxAurora:
                case TestClientId.FirefoxBeta:
                    browserOptions = DriverFactory.GetFirefoxOptions(browser.PathToExecutable);
                    break;
                case TestClientId.RemoteChromeBrowser:
                    browserOptions = DriverFactory.GetChromeOptions(browser.PathToExecutable);
                    break;
            }

            return browserOptions;
        }

        /// <summary>
        /// Retrieves Google Chrome Canary browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>
        /// Google Chrome Canary browser options
        /// </returns>
        internal static ChromeOptions GetChromeCanaryOptions(string pathToBrowserExecutable)
        {
            var browserOptions = new ChromeOptions();

            if (!string.IsNullOrEmpty(pathToBrowserExecutable))
            {
                Assertion.FileExists(pathToBrowserExecutable);
                browserOptions.BinaryLocation = pathToBrowserExecutable;
            }

            browserOptions.AddArgument("--start-maximized");
            return browserOptions;
        }

        /// <summary>
        /// Retrieves Google Chrome Canary browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome browser options.</returns>
        internal static ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            var browserOptions = new ChromeOptions();

            browserOptions.AddArguments(
                "--start-maximized",
                "--allow-running-insecure-content",
                "--test-type",
                "--disable-gpu",
                "--disable-backgrounding-occluded-windows",
                "--disable-infobars"); // disable "Chrome is being controlled by automated test software" InfoBar

            // disable "Do you want to save password for this site" dialog
            browserOptions.AddUserProfilePreference("credentials_enable_service", false);
            browserOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            browserOptions.AddArgument("force-device-scale-factor=0.75");
            browserOptions.AddArgument("high-dpi-support=0.75");
           
            if (!string.IsNullOrEmpty(pathToBrowserExecutable))
            {
                Assertion.FileExists(pathToBrowserExecutable);
                browserOptions.BinaryLocation = pathToBrowserExecutable;
            }

            return browserOptions;
        }

        /// <summary>
        /// The get Firefox options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>
        /// The <see cref="FirefoxOptions"/> for released and unreleased versions of Firefox.
        /// </returns>
        internal static FirefoxOptions GetFirefoxOptions(string pathToBrowserExecutable)
        {
            var browserOptions = new FirefoxOptions();
            var profile = new FirefoxProfile();

            SafeMethodExecutor.Execute(() => profile.SetPreference("javascript.enabled", true));
            browserOptions.Profile = profile;

            if (!string.IsNullOrEmpty(pathToBrowserExecutable))
            {
                Assertion.FileExists(pathToBrowserExecutable);
                browserOptions.BrowserExecutableLocation = pathToBrowserExecutable;
            }

            return browserOptions;
        }

        /// <summary>
        /// Base initialize for tests, gets the desired browser and sets the IWebDriver object accordingly
        /// </summary>
        /// <param name="supportedBrowser">Type of browser</param>
        /// <param name="browserOptions">Browser options</param>
        /// <param name="remoteDriverUri">Remote Driver Uri</param>
        /// <param name="driverLocation"> Driver Location </param>
        /// <returns>The <see cref="IWebDriver"/>.</returns>
        internal static IWebDriver GetWebDriver(TestClientId supportedBrowser, object browserOptions, Uri remoteDriverUri, string driverLocation)
        {
            IWebDriver driver;

            // Initialize the Selenium WebDriver
            switch (supportedBrowser)
            {
                case TestClientId.InternetExplorer9:
                case TestClientId.InternetExplorer10:
                case TestClientId.InternetExplorer11:
                    driver = DriverFactory.InitializeInternetExplorerDriver(driverLocation);
                    break;
                case TestClientId.FirefoxAurora:
                case TestClientId.FirefoxBeta:
                case TestClientId.Firefox:
                    driver = DriverFactory.InitializeFirefoxDriver(browserOptions, driverLocation);
                    break;
                case TestClientId.Chrome:
                    driver = DriverFactory.InitializeChromeDriver(browserOptions, driverLocation);
                    break;
                case TestClientId.ChromeCanary:
                    driver = DriverFactory.InitializeChromeDriver(browserOptions, driverLocation);
                    break;
                case TestClientId.Safari:
                    driver = DriverFactory.InitializeSafariDriver(driverLocation);
                    break;
                case TestClientId.Edge:
                    driver = DriverFactory.InitializeEdgeDriver(driverLocation);
                    break;
                case TestClientId.RemoteChromeBrowser:
                    driver = DriverFactory.InitializeRemoteDriver(remoteDriverUri, browserOptions);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser was detected: " + supportedBrowser);
            }

            return driver;
        }

        /// <summary>
        /// Gets the desired browser and sets the IWebDriver object accordingly.
        /// </summary>
        /// <param name="supportedBrowser">The browser.</param>
        /// <param name="driverLocation"></param>
        /// <returns>The <see cref="IWebDriver"/> object.</returns>
        internal static IWebDriver GetWebDriver(TestClientInfo supportedBrowser, string driverLocation)
        {
            return DriverFactory.GetWebDriver(supportedBrowser.Id, DriverFactory.GetBrowserOptions(supportedBrowser), supportedBrowser.RemoteDriverUri, driverLocation);
        }

        /// <summary>
        /// Initializes a WebDriver for use with Chrome
        /// </summary>
        /// <param name="browserOptions">
        /// An initialized or NULL ChromeOptions object.
        /// </param>
        /// <param name="driverLocation">
        /// The driver Location.
        /// </param>
        /// <returns>
        /// An instance of the Chrome driver
        /// </returns>
        private static IWebDriver InitializeChromeDriver(object browserOptions, string driverLocation)
        {
            var chromeOptions = browserOptions as ChromeOptions;
            IWebDriver driver = new PlaywrightWebDriver(chromeOptions);

            Logger.LogInfo("Playwright-backed Chromium driver initialized.");
            return driver;
        }

        /// <summary>
        /// Initializes a WebDriver for use with Edge
        /// </summary>
        /// <param name="driverLocation"> The driver Location. </param>
        /// <returns> IWebDriver object. </returns>
        private static IWebDriver InitializeEdgeDriver(string driverLocation)
        {
            IWebDriver driver = new EdgeDriver(driverLocation);
            driver.Manage().Window.Maximize();

            return driver;
        }

        /// <summary>
        /// Creates a Firefox Driver
        /// </summary>
        /// <param name="browserOptions">The browser Options.</param>
        /// <param name="driverLocation"> The driver location. </param>
        /// <returns>IWebDriver object.</returns>
        private static IWebDriver InitializeFirefoxDriver(object browserOptions, string driverLocation)
        {
            var firefoxOptions = browserOptions as FirefoxOptions;
            FirefoxDriverService firefoxDriverService =
                FirefoxDriverService.CreateDefaultService(driverLocation);
            IWebDriver driver = new FirefoxDriver(firefoxDriverService, firefoxOptions, TimeSpan.FromSeconds(60));

            driver.Manage().Window.Maximize();
            return driver;
        }

        /// <summary>
        /// Initializes a WebDriver for use with Internet Explorer
        /// </summary>
        /// <param name="driverLocation"> The driver Location. </param>
        /// <returns> An instance of the Internet Explorer driver </returns>
        private static IWebDriver InitializeInternetExplorerDriver(string driverLocation)
        {
            IWebDriver driver = new InternetExplorerDriver(driverLocation);

            driver.Manage().Window.Maximize();
            return driver;
        }

        /// <summary>
        /// Creates a Remote Driver
        /// </summary>
        /// <param name="remoteDriverUri"> The remote Driver Uri.  </param>
        /// <param name="browserOptions"> The browser Options. </param>
        /// <returns> IWebDriver object. </returns>
        private static IWebDriver InitializeRemoteDriver(Uri remoteDriverUri, object browserOptions) =>
            new RemoteWebDriver(remoteDriverUri, (DriverOptions)browserOptions);

        /// <summary>
        /// Creates a Safari Driver
        /// </summary>
        /// <param name="remoteSafariUri">URI to the remote safari location.</param>
        /// <returns>IWebDriver object.</returns>
        private static IWebDriver InitializeSafariDriver(string remoteSafariUri = "http://10.30.145.54:4444/wd/hub")
        {
            var capabilities = new SafariOptions();

            // if you wish safari to forget session every time.
            capabilities.AddAdditionalCapability("cleanSession", true);
            return new RemoteWebDriver(new Uri(remoteSafariUri), capabilities);
        }
    }
}