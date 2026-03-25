namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.CitedWith
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The CoCitations item
    /// </summary>
    public class CoCitationsItem : BaseItem
    {
        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");
        private static readonly By CitationLocator = By.XPath(".//span[@class='ResultItem-metadataItem']");
        private static readonly By SnippetLocator = By.XPath(".//a[@class='ResultItem-snippetLink']");
        private static readonly By CitationProximityLocator = By.XPath(".//div[@class='ResultItem-citeProximity']");
        private static readonly By CoCitingCasesButtonLocator = By.XPath(".//button[@class='ResultItem-button']");
        private static readonly By SummaryLocator = By.XPath(".//span[@class='ResultItem-Intro']");
        private static readonly By SynopsisLocator = By.XPath(".//div[contains(@class,'ResultItem-showHideCollapsibleWrapper')]");
        private static readonly By TitleLinkLocator = By.XPath(".//h3[@class='ResultItem-title']/a");
        private static readonly By ToggleLocator = By.XPath(".//button[@class='ResultItem-showHideToggle']");
        private static readonly By PuiIconLocator = By.XPath(".//button[contains(@class, 'PUI-button')]/span");
        private static readonly By ItemCountLocator = By.XPath(".//div[contains(@id,'CitedWithResultItem')]");
        private static readonly By SearchWithinTermsLocator = By.XPath(".//span[contains(@class,'co_locateTerm')]");

        /// <summary>
        /// Initializes a new instance of the CoCitationsItem class.
        /// Search result item constructor
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public CoCitationsItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckBoxLocator);

        /// <summary>
        /// Title
        /// </summary>
        public ILink Title => new Link(this.Container, TitleLinkLocator);

        /// <summary>
        /// Citations list
        /// </summary>
        public IReadOnlyCollection<ILabel> Citations =>
            new ElementsCollection<Label>(this.Container, CitationLocator);

        /// <summary>
        /// List of PUI icons
        /// </summary>
        public IReadOnlyCollection<IButton> PuiIconsList =>
            new ElementsCollection<Button>(this.Container, PuiIconLocator);

        /// <summary>
        /// [N] Co-citing cases button
        /// </summary>
        public IButton CoCitingCasesButton => new Button(this.Container, CoCitingCasesButtonLocator);

        /// <summary>
        /// Document Summary
        /// </summary>
        public ILabel DocSummary => new Label(this.Container, SummaryLocator);

        /// <summary>
        /// Synopsis toggle
        /// </summary>
        public IToggle SynopsisToggle => new ToggleWithText(this.Container, ToggleLocator, "Hide synopsis");

        /// <summary>
        /// Synopsis
        /// </summary>
        public ILabel Synopsis => new Label(this.Container, SynopsisLocator);

        /// <summary>
        /// Citation Proximity label
        /// </summary>
        public ILabel CitationProximityLabel => new Label(this.Container, CitationProximityLocator);

        /// <summary>
        /// List of snippets
        /// </summary>
        public ItemsCollection<CoCitingSnippetItem> SnippetList =>
            new ItemsCollection<CoCitingSnippetItem>(this.Container, SnippetLocator);

        /// <summary>
        /// Search within terms
        /// </summary>
        public IReadOnlyCollection<ILabel> SearchWithinTermsCollection =>
            new ElementsCollection<Label>(this.Container, SearchWithinTermsLocator);
        
       /// <summary>
        /// Get item count
        /// </summary>
        /// <returns>the count of item</returns>
        public int GetItemCount() =>
            DriverExtensions.GetElement(this.Container, ItemCountLocator).Text.ConvertCountToInt();
    }
}