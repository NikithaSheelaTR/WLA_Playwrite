namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.PrecisionSearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using OpenQA.Selenium;

    /// <summary>
    /// Edge Preview Area of law items dialog
    /// </summary>
    public class EdgePreviewAreaOfLawItemsDialog : PrecisionAreaOfLawItemsDialog
    {
        private static readonly By AvailableOnlyLinkLocator = By.XPath("//*[@class='precisionSearchModal-multiSelectKey']//*[contains(@class, 'co_linkBlue')]//span");

        /// <summary>
        /// Available only link
        /// </summary>
        public ILink AvailableOnlyLink => new Link(AvailableOnlyLinkLocator);
    }
}
