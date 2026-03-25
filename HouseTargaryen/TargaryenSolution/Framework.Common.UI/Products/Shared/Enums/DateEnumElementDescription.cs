namespace Framework.Common.UI.Products.Shared.Enums
{
    using OpenQA.Selenium;

    /// <summary>
    /// DateEnumElementDescription
    /// </summary>
    public class DateEnumElementDescription
    {
        /// <summary>
        /// CountLocator
        /// </summary>
        public By CountLocator { get; set; }

        /// <summary>
        /// DateFromInputLocator
        /// </summary>
        public By DateFromInputLocator { get; set; }

        /// <summary>
        /// DateUntilInputLocator
        /// </summary>
        public By DateUntilInputLocator { get; set; }

        /// <summary>
        /// Locator
        /// </summary>
        public By Locator { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
    }
}