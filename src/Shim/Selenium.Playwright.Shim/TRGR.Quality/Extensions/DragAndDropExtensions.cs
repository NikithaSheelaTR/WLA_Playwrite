using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for drag and drop operations via IWebDriver.
    /// </summary>
    public static class DragAndDropExtensions
    {
        public static void DragAndDrop(this IWebDriver driver, By dropTargetBy, params By[] dragElementBys)
        {
            var dragElement = driver.GetElementByChainedBys(dragElementBys);
            var dropTarget = driver.FindElement(dropTargetBy);
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.DragAndDrop(dragElement, dropTarget).Perform();
        }

        public static void DragAndDrop(this IWebDriver driver, IWebElement dropTarget, IWebElement dragElement)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.DragAndDrop(dragElement, dropTarget).Perform();
        }

    }
}
