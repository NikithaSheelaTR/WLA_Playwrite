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

        public static void CustomDragAndDrop(this IWebDriver driver, IWebElement dragElement, IWebElement dropTarget)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.ClickAndHold(dragElement)
                   .MoveToElement(dropTarget)
                   .Release(dropTarget)
                   .Perform();
        }

        public static void DragAndHold(this IWebDriver driver, IWebElement target, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.ClickAndHold(element)
                   .MoveToElement(target)
                   .Perform();
        }

        public static void DragAndDropWithOutWait(this IWebDriver driver, IWebElement target, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.DragAndDrop(element, target).Perform();
        }

        public static void DragAndDropWithWait(this IWebDriver driver, IWebElement target, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.ClickAndHold(element)
                   .MoveToElement(target)
                   .Release(target)
                   .Perform();
        }

        public static void DragAndDropWithOffset(this IWebDriver driver, IWebElement target, IWebElement element, int x = 0, int y = 0)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.ClickAndHold(element)
                   .MoveToElement(target, x, y)
                   .Release()
                   .Perform();
        }
    }
}
