namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;

    using Framework.Common.UI.Products.Shared.Enums.Delivery;

    using OpenQA.Selenium;

    /// <summary>
    /// Font Size Dropdown inside Layouts And Limits Tab
    /// </summary>
    public class FontSizeDropdown : Dropdown<LayoutAndLimitsFontSizeOptions>
    {
        private static readonly By FontSizeDropdownLocator = By.XPath("//select[@id='co_delivery_fontSize']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSizeDropdown"/> class.
        /// </summary>
        public FontSizeDropdown() : base(FontSizeDropdownLocator)
        {
        }

        /// <summary>
        /// The get option text.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected override string GetOptionText(LayoutAndLimitsFontSizeOptions option) => option.ToString();

        /// <summary>
        /// The get option by text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="LayoutAndLimitsFontSizeOptions"/>.
        /// </returns>
        protected override LayoutAndLimitsFontSizeOptions GetOptionByText(string text)
            => (LayoutAndLimitsFontSizeOptions)Enum.Parse(typeof(LayoutAndLimitsFontSizeOptions), text);
    }
}