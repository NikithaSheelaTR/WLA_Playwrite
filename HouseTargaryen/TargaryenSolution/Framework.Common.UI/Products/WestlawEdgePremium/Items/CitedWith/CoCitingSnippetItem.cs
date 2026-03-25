namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.CitedWith
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Co-Citing snippet item
    /// </summary>
    public class CoCitingSnippetItem : BaseItem
    {
        private static readonly By HighlightedTextLocator = By.XPath(".//span[contains(@class,'ResultItem')]");
        private static readonly By SearchWithinTermsLocator = By.XPath(".//span[contains(@class,'co_locateTerm')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitingSnippetItem"/> class. 
        ///  </summary>
        /// <param name="container">
        /// Snippet container</param>
        public CoCitingSnippetItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Snippet link
        /// </summary>
        public ILink Snippet => new Link(this.Container);

        /// <summary>
        /// Highlighted text
        /// </summary>
        public ILabel HighlightedText => new Label(this.Container, HighlightedTextLocator);

        /// <summary>
        /// Get Term
        /// </summary>
        public IReadOnlyCollection<IWebElement> SearchWithinTermsCollection => DriverExtensions.GetElements(this.Container,SearchWithinTermsLocator).ToList();

        /// <summary>
        /// Get color of highlighted text
        /// </summary>
        /// <returns>the code of background color</returns>
        public string GetHighlightedTextColor() => this.HighlightedText.GetCssValue("background-color");
    }
}