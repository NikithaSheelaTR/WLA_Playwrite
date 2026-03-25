using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for radio button operations via IWebDriver.
    /// </summary>
    public static class RadiobuttonExtensions
    {
        public static bool IsRadioButtonSelected(this IWebDriver driver, params By[] elementBys)
        {
            var element = driver.GetElementByChainedBys(elementBys);
            return element.Selected;
        }

        public static bool IsRadioButtonSelected(this IWebDriver driver, IWebElement container, params By[] elementBys)
        {
            var element = container.FindElementByChainedBys(elementBys);
            return element.Selected;
        }
    }
}
