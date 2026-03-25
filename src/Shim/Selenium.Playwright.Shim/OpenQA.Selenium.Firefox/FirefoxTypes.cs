using System;

namespace OpenQA.Selenium.Firefox
{
    /// <summary>
    /// Stub for FirefoxOptions. Not supported in Playwright shim.
    /// </summary>
    public class FirefoxOptions : Remote.DriverOptions
    {
        public FirefoxProfile Profile { get; set; }
        public string BrowserExecutableLocation { get; set; }

        public override void AddAdditionalCapability(string capabilityName, object capabilityValue) { }
    }

    /// <summary>
    /// Stub for FirefoxProfile.
    /// </summary>
    public class FirefoxProfile
    {
        public void SetPreference(string name, object value) { }
    }

    /// <summary>
    /// Stub for FirefoxDriverService.
    /// </summary>
    public class FirefoxDriverService
    {
        public static FirefoxDriverService CreateDefaultService(string driverLocation)
        {
            return new FirefoxDriverService();
        }
    }

    /// <summary>
    /// Stub for FirefoxDriver. Not supported in Playwright shim.
    /// </summary>
    public class FirefoxDriver : Remote.RemoteWebDriver
    {
        public FirefoxDriver(FirefoxDriverService service, FirefoxOptions options, TimeSpan timeout)
        {
        }
    }
}
