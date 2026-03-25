namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;

    using Framework.Common.UI.Products.Shared.Enums.Delivery;

    using OpenQA.Selenium;

    /// <summary>
    /// Links Dropdown inside Layouts And Limits Tab
    /// </summary>
    public class LinksDropdown : Dropdown<LayoutAndLimitsLinksOptions>
    {
        private static readonly By LinksDropdownLocator = By.XPath("//select[@id='co_delivery_linkColor']");

        /// <summary>
        /// Initializes a new instance of the <see cref="LinksDropdown"/> class.
        /// </summary>
        public LinksDropdown() : base(LinksDropdownLocator)
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
        protected override string GetOptionText(LayoutAndLimitsLinksOptions option) => option.ToString();

        /// <summary>
        /// The get option by text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="LayoutAndLimitsLinksOptions"/>.
        /// </returns>
        protected override LayoutAndLimitsLinksOptions GetOptionByText(string text)
            => (LayoutAndLimitsLinksOptions)Enum.Parse(typeof(LayoutAndLimitsLinksOptions), text);
    }
}