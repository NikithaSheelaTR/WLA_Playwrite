namespace WestlawPrecision.Tests
{
    using Framework.Common.UI.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Chrome;
    using System;

    /// <summary>
    /// Westlaw base test.
    /// </summary>
    [DeploymentItem(@"Resources\RP.config.json")]
    [TestClass]
    public class BaseWestlawTest : BaseWebUiTest
    {
        protected virtual string FolderToSave { get; set; }

        /// <summary>
        /// Retrieves Google Chrome browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome browser options.</returns>
        protected override ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = base.GetChromeOptions(pathToBrowserExecutable);

            // disable "Do you want to save password for this site" dialog
            browserOptions.AddUserProfilePreference("credentials_enable_service", false);
            browserOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            browserOptions.AddUserProfilePreference("download.default_directory", FolderToSave);
            browserOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            browserOptions.AddArguments(
                "--start-maximized",
                "--allow-running-insecure-content",
                "--test-type",
                "--disable-gpu",
                "--disable-backgrounding-occluded-windows",
                "--disable-infobars");

            var jenkinsUrl = Environment.GetEnvironmentVariable("JENKINS_URL");

            if (!string.IsNullOrEmpty(jenkinsUrl))
            {
                browserOptions.AddArguments(
                    "--headless=new",
                    "--window-size=1916,1000");
            }

            return browserOptions;
        }
    }
}
