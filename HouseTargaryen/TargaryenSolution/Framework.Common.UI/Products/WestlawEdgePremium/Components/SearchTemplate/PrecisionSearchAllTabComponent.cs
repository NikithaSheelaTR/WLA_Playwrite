namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;

    /// <summary>
    /// Precision Search all tab component
    /// </summary>
    public class PrecisionSearchAllTabComponent : BaseTabComponent
    {
        private static readonly By AreaOfLawButtonLocator = By.XPath(".//button[@id='coid_AreaOfLawDropdown']");
        private static readonly By BackToTopButtonLocator = By.XPath(".//div[@class='BackToTopWrapper']/button");
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel_quickSearch']");
        private static readonly By ChangeJurisdictionButtonLocator = By.XPath(".//div[@class='PrecisionSearchModal-zeroState']//button");
        private static readonly By FiltersSectionLocator = By.XPath(".//*[@class='PrecisionSearchModal-contentList']");
        private static readonly By LearnMoreExternalLinkLocator = By.XPath(".//button[@class='co_linkBlue Button--medium']");
        private static readonly By LegalIssueInfoColumnLocator = By.XPath(".//*[@id='coid_legalIssue']");
        private static readonly By NewSearchWayInfoColumnLocator = By.XPath(".//*[@class='PrecisionSearchModal-infoColumns' and not(contains(@id, 'coid'))]");
        private static readonly By NoResultsForJurisdictionMessageLocator = By.XPath(".//div[@class='PrecisionSearchModal-zeroState']");
        private static readonly By SearchTemplateInfoBoxLocator = By.XPath(".//div[contains(@class, 'PrecisionSearchModal-moreInfo')]");
        private static readonly By SearchTextboxLocator = By.XPath(".//*[@id='co_facet_searchBox']");
        private static readonly By TabNameLocator = By.XPath("//*[@id='tab_quickSearch']");       
        private static readonly By TextboxLabelLocator = By.XPath(".//*[@class='PrecisionSearchModal-contentHeadingInner']//b");
        private static readonly By XClearButtonLocator = By.XPath("./following-sibling::*[@class='Typeahead-clearButton']");          
        private static readonly By ZeroStateMessageLocator = By.XPath(".//*[@class='PrecisionSearch-noResults']");

        /// <summary>
        /// Message when there are no results for the selected jurisdiction
        /// </summary>
        public ILabel NoResultsForJurisdictionMessageLabel => new Label(this.ComponentLocator, NoResultsForJurisdictionMessageLocator);

        /// <summary>
        /// Textbox label
        /// </summary>
        public ILabel TextboxLabel => new Label(this.ComponentLocator, TextboxLabelLocator);

        /// <summary>
        /// Tab name label
        /// </summary>
        public ILabel TabNameLabel => new Label(TabNameLocator);

        /// <summary>
        /// Zero state message label
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(this.ComponentLocator, ZeroStateMessageLocator);

        /// <summary>
        /// Area of law button
        /// </summary>
        public IButton AreaOfLawButton => new Button(this.ComponentLocator, AreaOfLawButtonLocator);

        /// <summary>
        /// Back to top button
        /// </summary>
        public IButton BackToTopButton => new Button(this.ComponentLocator, BackToTopButtonLocator);

        /// <summary>
        /// Change jurisdiction button
        /// </summary>
        public IButton ChangeJurisdictionButton => new Button(this.ComponentLocator, ChangeJurisdictionButtonLocator);

        /// <summary>
        /// Learn more link
        /// </summary>
        public ILink LearnMoreLink => new Link(this.ComponentLocator, LearnMoreExternalLinkLocator);

        /// <summary>
        /// Search textbox
        /// </summary>
        public ITextbox SearchTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, SearchTextboxLocator), XClearButtonLocator);

        /// <summary>
        /// Infobox
        /// </summary>
        public IInfoBoxWithLink SearchTemplateInfoBox => new InfoBoxWithLink(SearchTemplateInfoBoxLocator);
        
        /// <summary>
        /// Matches
        /// </summary>
        public PrecisionSingleTypeaheadSearchTemplateMatchesComponent Matches => new PrecisionSingleTypeaheadSearchTemplateMatchesComponent(new ByChained(ContainerLocator, FiltersSectionLocator));

        /// <summary>
        /// New search way info column
        /// </summary>
        public PrecisionSearchAllTabTopInfoColumnComponent NewSearchWayInfoColumn => new PrecisionSearchAllTabTopInfoColumnComponent(new ByChained(ContainerLocator, NewSearchWayInfoColumnLocator));

        /// <summary>
        /// Legal issue info column
        /// </summary>
        public PrecisionBaseSearchAllTabInfoColumnComponent LegalIssueInfoColumn => new PrecisionBaseSearchAllTabInfoColumnComponent(new ByChained(ContainerLocator, LegalIssueInfoColumnLocator));

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => TabNameLabel.Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
