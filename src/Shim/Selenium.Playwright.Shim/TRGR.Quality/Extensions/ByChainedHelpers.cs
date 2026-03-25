using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Internal helper extension methods for chaining By locators.
    /// </summary>
    public static class ByChainedHelpers
    {
        /// <summary>
        /// Finds an element by navigating through a chain of By locators.
        /// Each By in the chain narrows the search scope.
        /// </summary>
        public static IWebElement GetElementByChainedBys(this ISearchContext context, params By[] bys)
        {
            if (bys == null || bys.Length == 0)
                throw new NoSuchElementException("No By locators provided");

            ISearchContext current = context;
            for (int i = 0; i < bys.Length; i++)
            {
                current = current.FindElement(bys[i]);
            }
            return (IWebElement)current;
        }

        /// <summary>
        /// Finds an element within a container by navigating through a chain of By locators.
        /// </summary>
        public static IWebElement FindElementByChainedBys(this IWebElement container, params By[] bys)
        {
            if (bys == null || bys.Length == 0)
                throw new NoSuchElementException("No By locators provided");

            ISearchContext current = container;
            for (int i = 0; i < bys.Length; i++)
            {
                current = current.FindElement(bys[i]);
            }
            return (IWebElement)current;
        }
    }
}
