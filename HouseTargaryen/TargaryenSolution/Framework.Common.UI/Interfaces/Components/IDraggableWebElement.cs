namespace Framework.Common.UI.Interfaces.Components
{
    using OpenQA.Selenium;

    /// <summary>
    /// The DraggableWebElement interface to use in Drag'n'Drop methods
    /// </summary>
    internal interface IDraggableWebElement
    {
        /// <summary>
        /// The get draggable web element.
        /// </summary>
        /// <returns>
        /// The IWebElement to be dragged or dropped on
        /// </returns>
        IWebElement GetDraggableElement();
    }
}