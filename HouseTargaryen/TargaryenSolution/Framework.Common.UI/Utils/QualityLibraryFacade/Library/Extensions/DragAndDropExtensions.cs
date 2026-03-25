namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Contains extensions related dragging and dropping elements
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Drags the element specified by drag element Bys to the element specified by drop element By and releases
        /// </summary>
        /// <param name="dropTargetBy">By representing the element where you want to release the element specified by drag element Bys</param>
        /// <param name="dragElementBys">Bys representing the element that you want to drag and drop</param>
        public static void DragAndDrop(By dropTargetBy, params By[] dragElementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndDrop(dropTargetBy, dragElementBys));
        }

        /// <summary>
        /// drag and drop element
        /// </summary>
        /// <param name="dragElement">drag Element</param>
        /// <param name="dropTarget">drop Target</param>
        public static void CustomDragAndDrop(IWebElement dragElement, IWebElement dropTarget)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.CustomDragAndDrop(dragElement, dropTarget));
        }

        /// <summary>
        /// Drags the specified drag element to the specified drop element and releases
        /// </summary>
        /// <param name="dropTarget">The target IWebElement where you want to release the specified draggable element</param>
        /// <param name="dragElement">The draggable IWebElement that you want to drag and drop</param>
        public static void DragAndDrop(IWebElement dropTarget, IWebElement dragElement)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndDrop(dropTarget, dragElement));
        }

        /// <summary>
        /// Drags the specified drag element to the specified drop element and releases
        /// </summary>
        /// <param name="target">The target IWebElement where you want to drag the specified element</param>
        /// <param name="element">The draggable IWebElement that you want to drag</param>
        public static void DragAndHold(IWebElement target, IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndHold(target, element));
        }

        /// <summary>
        /// Drags the specified drag element to the specified drop element without wait for javascript and releases
        /// </summary>
        /// <param name="target">The target IWebElement where you want to drag the specified element</param>
        /// <param name="element">The draggable IWebElement that you want to drag</param>
        public static void DragAndDropWithoutWaitTime(IWebElement target, IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndDropWithOutWait(target, element));
        }
        /// <summary>
        /// Drags the specified drag element to the specified drop element, wait for javascript and releases
        /// </summary>
        /// <param name="target">The target IWebElement where you want to drag the specified element</param>
        /// <param name="element">The draggable IWebElement that you want to drag</param>
        public static void DragAndDropWithWait(IWebElement target, IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndDropWithWait(target, element));
        }

        /// <summary>
        /// Drags the specified drag element to the specified drop element with offset
        /// </summary>
        /// <param name="target">The target IWebElement where you want to drag the specified element</param>
        /// <param name="element">The draggable IWebElement that you want to drag</param>
        /// <param name="x">offsetX</param>
        /// <param name="y">offsetY</param>
        public static void DragAndDropWithOffset(IWebElement target, IWebElement element, int x = 0, int y = 0)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DragAndDropWithOffset(target, element, x, y));
        }
    }
}