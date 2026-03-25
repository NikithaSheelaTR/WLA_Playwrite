namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Custom Action extensions
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Press key
        /// </summary>
        /// <param name="driver">IwebDriver</param>
        /// <param name="key">Key</param>
        public static void PressKey(this IWebDriver driver, string key)
        {
            var builder = new Actions(driver);
            builder.SendKeys(key).Build().Perform();
        }

        /// <summary>
        /// DragAndHold item
        /// </summary>
        /// <param name="driver">IwebDriver</param>
        /// <param name="target">IwebElement</param>
        /// <param name="element">IwebElement</param>
        public static void DragAndHold(this IWebDriver driver, IWebElement target, IWebElement element)
            => new Actions(driver).ClickAndHold(element).MoveByOffset(1, 0)
                .MoveToElement(target).Build().Perform();

        /// <summary>
        /// DragAndHold item, wait for javascript and release item
        /// </summary>
        /// <param name="driver">IwebDriver</param>
        /// <param name="target">IwebElement</param>
        /// <param name="element">IwebElement</param>
        public static void DragAndDropWithWait(this IWebDriver driver, IWebElement target, IWebElement element)
        {
            var action = new Actions(driver);
            action.ClickAndHold(element).MoveToElement(target).Build().Perform();
            driver.WaitForJavaScript();
            action.Release(target).Build().Perform();
        }

        /// <summary>
        /// DragAndDrop item with offset
        /// </summary>
        /// <param name="driver">IwebDriver</param>
        /// <param name="target">IwebElement</param>
        /// <param name="element">IwebElement</param>
        /// <param name="x">offset X</param>
        /// <param name="y">offset Y</param>
        public static void DragAndDropWithOffset(this IWebDriver driver, IWebElement target, IWebElement element, int x, int y)
        {
            var action = new Actions(driver);
            action.ClickAndHold(element).MoveToElement(target).MoveByOffset(x, y).Build().Perform();
            action.Release(target).Build().Perform();
        }

        /// <summary>
        /// Double click on an element
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void DoubleClick(this IWebDriver driver, IWebElement element)
        {
            var action = new Actions(driver);
            action.DoubleClick(element).Build().Perform();
        }

        /// <summary>
        /// Custom drag and drop for Judicial upload page
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="element">IWebDriver</param>
        /// <param name="target">IWebElement</param>
        public static void CustomDragAndDrop(this IWebDriver driver, IWebElement element, IWebElement target)
        {
            string script = @"function createEvent(typeOfEvent) 
        {
            var event =document.createEvent('CustomEvent');
                event.initCustomEvent(typeOfEvent,true, true, null);
                event.dataTransfer = {
                data: { },
                setData: function(key, value) {
                    this.data[key] = value;
                },
                getData: function(key) {
                    return this.data[key];
                }
            };
            return event;
        }

        function dispatchEvent(element, event,transferData) {
            if (transferData !== undefined)
            {
                event.dataTransfer = transferData;
            }
            if (element.dispatchEvent) {
                element.dispatchEvent(event);
            } else if (element.fireEvent) {
                element.fireEvent('on' + event.type, event);
            }
        }

        function simulateHTML5DragAndDrop(element, destination)
        {
            var dragStartEvent = createEvent('dragstart');
            dispatchEvent(element, dragStartEvent);
            var dropEvent = createEvent('drop');
            dispatchEvent(destination, dropEvent, dragStartEvent.dataTransfer);
            var dragEndEvent = createEvent('dragend');
            dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);
        }

        var source = arguments[0];
        var destination = arguments[1];
        simulateHTML5DragAndDrop(source, destination);";

            driver.ExecuteScript(script, element, target);
        }

        /// <summary>
        /// DragAndDrop item without waiting for javascript
        /// </summary>
        /// <param name="driver">IwebDriver</param>
        /// <param name="target">IwebElement</param>
        /// <param name="element">IwebElement</param>
        public static void DragAndDropWithOutWait(this IWebDriver driver, IWebElement target, IWebElement element)
        {
            new Actions(driver).DragAndDrop(element, target).Build()
            .Perform();
        }
    }
}