using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for dropdown operations via IWebDriver.
    /// </summary>
    public static class DropdownExtensions
    {
        public static IList<IWebElement> GetDropdownOptionElements(this IWebDriver driver, params By[] elementBys)
        {
            var selectElement = driver.GetElementByChainedBys(elementBys);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            return select.Options.ToList();
        }

        public static IList<string> GetDropdownOptionTexts(this IWebDriver driver, params By[] elementBys)
        {
            var selectElement = driver.GetElementByChainedBys(elementBys);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            return select.Options.Select(o => o.Text).ToList();
        }

        public static IList<string> GetDropdownOptionValues(this IWebDriver driver, params By[] elementBys)
        {
            var selectElement = driver.GetElementByChainedBys(elementBys);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            return select.Options.Select(o => o.GetAttribute("value")).ToList();
        }

        public static string GetSelectedDropdownOptionText(this IWebDriver driver, params By[] elementBys)
        {
            var selectElement = driver.GetElementByChainedBys(elementBys);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            return select.SelectedOption.Text;
        }

        public static string GetSelectElementSelectedText(this IWebDriver driver, By by)
        {
            var selectElement = driver.FindElement(by);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            return select.SelectedOption.Text;
        }

        public static void SelectElementInListByText(this IWebDriver driver, By by, string textToSelect)
        {
            var selectElement = driver.FindElement(by);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            select.SelectByText(textToSelect);
        }

        public static void SetDropdown(this IWebDriver driver, string option, params By[] elementBys)
        {
            var selectElement = driver.GetElementByChainedBys(elementBys);
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElement);
            select.SelectByText(option);
        }

        public static void SetDropdown(this IWebDriver driver, string option, IWebElement element)
        {
            var select = new OpenQA.Selenium.Support.UI.SelectElement(element);
            select.SelectByText(option);
        }
    }
}
