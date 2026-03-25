using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for keyboard actions via IWebDriver.
    /// </summary>
    public static class ActionExtensions
    {
        public static void PressKey(this IWebDriver driver, string key)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.SendKeys(key).Perform();
        }
    }
}
