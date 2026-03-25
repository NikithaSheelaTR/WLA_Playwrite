namespace Framework.Common.UI.Products.WestLawAnalytics.Dropdowns.Analytics
{
    using System;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Graph by dropdown on the Analytics page
    /// </summary>
    public class GraphByDropdown : Dropdown<GraphByOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphByDropdown"/> class.
        /// </summary>
        /// <param name="locatorBys">
        /// The locator Bys.
        /// </param>
        public GraphByDropdown(params By[] locatorBys) : base(locatorBys)
        {
        }
        
        /// <summary>
        /// The get option text.
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> option text</returns>
        protected override string GetOptionText(GraphByOptions option) => option.ToString();

        /// <summary>
        /// The get option by text.
        /// </summary>
        /// <param name="text"> The text. </param>
        /// <returns> The <see cref="GraphByOptions"/>. </returns>
        protected override GraphByOptions GetOptionByText(string text)
            => (GraphByOptions)Enum.Parse(typeof(GraphByOptions), text);
    }
}
