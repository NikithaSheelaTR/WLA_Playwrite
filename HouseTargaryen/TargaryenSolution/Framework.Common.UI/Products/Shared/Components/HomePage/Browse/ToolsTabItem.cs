namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Tools Item as a part of Browse Component - Tools List
    /// </summary>
    public class ToolsTabItem : BaseItem
    {
        private static readonly By LinkLocator = By.XPath(".//a[@data-link-type = 'category-page']");

        private static readonly By SummaryTextLocator = By.XPath(".//a/following-sibling::*");

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsTabItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public ToolsTabItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Get text from the link
        /// </summary>
        /// <returns> Link text </returns>
        public string GetLinkText() => DriverExtensions.GetElement(this.Container, LinkLocator).GetText();

        /// <summary>
        /// Get summary text
        /// </summary>
        /// <returns> Summary text </returns>
        public string GetSummaryText() => DriverExtensions.GetElement(this.Container, SummaryTextLocator).GetText();
    }
}