namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// Precision Common Document page
    /// </summary>
    public class PrecisionCommonDocumentPage : EdgeCommonDocumentPage
    {
        private static readonly By CoCitesOriginalDocLocator = By.XPath("//span[contains(@class, 'co_searchTerm co_currentSearchTerm')]"); 
        private static readonly By CoCitedDocLocator = By.XPath("//span[contains(@class,'co_searchTerm co_coCitedTerm')]");
        private static readonly By FootnoteHighlightLabelLocator = By.XPath(".//div[contains(@class,'__highlighted')]");

        /// <summary>
        /// Current CoCites Original Term
        /// </summary>
        public IReadOnlyCollection<ILink> CoCitesOriginalDoc => new ElementsCollection<Link>(CoCitesOriginalDocLocator);

        /// <summary>
        /// Current CoCited Term
        /// </summary>
        public IReadOnlyCollection<ILink> CoCitedDocs => new ElementsCollection<Link>(CoCitedDocLocator);

        /// <summary>
        /// Highlighted Section Label
        /// </summary>
        public ILabel FootNotesHighlightedLabel => new Label(FootnoteHighlightLabelLocator);
    }
}
