namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The BaseWebElement interface.
    /// </summary>
    public interface IBaseWebElement
    {
        /// <summary>
        /// Gets a value indicating whether displayed.
        /// </summary>
        bool Displayed { get; }

        /// <summary>
        /// Gets a value indicating whether enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets a value indicating whether present.
        /// </summary>
        bool Present { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets a value indicating whether is in view.
        /// </summary>
        bool IsInView { get; }

        /// <summary>
        /// The get attribute.
        /// </summary>
        /// <param name="attribute">
        /// The attribute.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetAttribute(string attribute);

        /// <summary>
        /// The get css value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetCssValue(string propertyName);

        /// <summary>
        /// The send keys.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        void SendKeys(string text);

        /// <summary>
        /// The hover.
        /// </summary>
        void Hover();

        /// <summary>
        /// The hover out.
        /// </summary>
        void HoverOut();

        /// <summary>
        /// The scroll to element.
        /// </summary>
        void ScrollToElement();

        /// <summary>
        /// The wait for displayed.
        /// </summary>
        /// <param name="milliseconds">
        /// The seconds.
        /// </param>
        void WaitDisplayed(int milliseconds);

        /// <summary>
        /// The wait for not displayed.
        /// </summary>
        void WaitNotDisplayed();

        /// <summary>
        /// The wait for enabled.
        /// </summary>
        /// <param name="milliseconds">
        /// The seconds.
        /// </param>
        void WaitEnabled(int milliseconds);
    }
}