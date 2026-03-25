using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for checkbox operations via IWebDriver.
    /// </summary>
    public static class CheckboxExtensions
    {
        public static bool IsCheckboxSelected(this IWebDriver driver, IWebElement container, params By[] elementBys)
        {
            var element = container.FindElementByChainedBys(elementBys);
            return element.Selected;
        }

        public static bool IsCheckboxSelected(this IWebDriver driver, params By[] elementBys)
        {
            var element = driver.GetElementByChainedBys(elementBys);
            return element.Selected;
        }

        public static void SetCheckbox(this IWebDriver driver, bool selected, By elementBys)
        {
            var element = driver.FindElement(elementBys);
            if (element.Selected != selected)
                element.Click();
        }

        public static void SetCheckbox(this IWebDriver driver, bool selected, string value)
        {
            var element = driver.FindElement(By.CssSelector($"input[type='checkbox'][value='{value}']"));
            if (element.Selected != selected)
                element.Click();
        }

        public static void SetCheckbox(this IWebDriver driver, bool selected, params By[] elementBys)
        {
            var element = driver.GetElementByChainedBys(elementBys);
            if (element.Selected != selected)
                element.Click();
        }

        public static void SetCheckbox(this IWebDriver driver, bool selected, IWebElement container, params By[] elementBys)
        {
            var element = container.FindElementByChainedBys(elementBys);
            if (element.Selected != selected)
                element.Click();
        }

        public static void SetCheckbox(this IWebDriver driver, bool selected, IWebElement checkbox)
        {
            if (checkbox.Selected != selected)
                checkbox.Click();
        }
    }
}
