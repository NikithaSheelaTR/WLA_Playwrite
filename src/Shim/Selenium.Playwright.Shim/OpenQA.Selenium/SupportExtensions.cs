namespace OpenQA.Selenium.Support.Extensions
{
    /// <summary>
    /// Stub for Selenium Support Extensions WebDriverExtensions.
    /// </summary>
    public static class WebDriverExtensions
    {
        public static Screenshot TakeScreenshot(IWebDriver driver)
        {
            if (driver is ITakesScreenshot screenshotDriver)
            {
                return screenshotDriver.GetScreenshot();
            }
            return new Screenshot(new byte[0]);
        }
    }
}
