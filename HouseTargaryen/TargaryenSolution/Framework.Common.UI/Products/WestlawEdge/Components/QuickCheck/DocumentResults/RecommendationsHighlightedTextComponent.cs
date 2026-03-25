namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Elements;

    /// <summary>
    /// Highlighted text in Recommendation tab
    /// </summary>
    public class RecommendationsHighlightedTextComponent: BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_da_highlighted_text']");
        private static readonly By ExpandButtonLocator = By.XPath(".//button[@type='button']");
        private static readonly By MetadataTextLocator = By.XPath(".//div[@class='co_searchResults_citation DA-DocMetadata ']/span");
        private static readonly By SelectedTextLocator = By.XPath(".//div[@class='DA-highlightedText-content']");
        private static readonly By HighlightedTextHeadingLocator = By.XPath(".//div/span");
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//div/a[@class='DA-highlightedText-docLink']");
        private static readonly By DocumentKeyCiteFlagLinkLocator = By.XPath(".//div/a[contains(@href, 'Flag?')]");

        /// <summary>
        /// Expand button.
        /// </summary>
        public IButton ExpandButton => new Button(this.ComponentLocator, ExpandButtonLocator);

        /// <summary> 
        /// Selected text label. 
        /// </summary>
        public ILabel SelectedTextLabel => new Label(this.ComponentLocator, SelectedTextLocator);

        /// <summary>
        /// Highlighted text heading.
        /// </summary>
        public ILabel HighlightedTextHeadingLabel => new Label(this.ComponentLocator, HighlightedTextHeadingLocator);

        /// <summary>
        /// The title link.
        /// </summary>
        public ILink DocumentTitleLink => new Link(this.ComponentLocator, DocumentTitleLinkLocator);

        /// <summary>
        /// The KeyCite Flag link.
        /// </summary>
        public ILink DocumentKeyCiteFlagLink => new Link(this.ComponentLocator, DocumentKeyCiteFlagLinkLocator);

        /// <summary>
        /// Metadata labels
        /// </summary>
        public IReadOnlyCollection<ILabel> MetadataLabels => new ElementsCollection<Label>(this.ComponentLocator, MetadataTextLocator);
   
        /// <summary>
        /// Component locator.
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
