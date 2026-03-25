using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for clicking elements via IWebDriver.
    /// </summary>
    public static class ClickExtensions
    {
        public static void Click(this IWebDriver driver, params By[] elementBys)
        {
            var element = driver.GetElementByChainedBys(elementBys);
            element.Click();
        }

        public static void Click(this IWebDriver driver, IWebElement element)
        {
            element.Click();
        }

        public static void Click(this IWebDriver driver, IWebElement container, params By[] elementBys)
        {
            var element = container.FindElementByChainedBys(elementBys);
            element.Click();
        }

        public static void JavascriptClick(this IWebDriver driver, params By[] elementBys)
        {
            var element = driver.GetElementByChainedBys(elementBys);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        public static void JavascriptClick(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        public static void DoubleClick(this IWebDriver driver, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.DoubleClick(element).Perform();
        }
    }
}
