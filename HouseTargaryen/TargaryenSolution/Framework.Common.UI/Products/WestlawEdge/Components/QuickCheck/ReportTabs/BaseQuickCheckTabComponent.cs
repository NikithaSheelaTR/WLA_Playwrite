namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Doc analyzer base tab component
    /// </summary>
    public abstract class BaseQuickCheckTabComponent : BaseTabComponent
    {
        private static readonly By ResultsListPageTitleContainer = By.XPath("//*[@class='Error'] | .//div[@class='DA-HeaderToolbar']/ *[self::h1 or self::h2] | //*[contains(@class, 'co_issueContentContainer')]");
        private static readonly By SelectAllComponentLocator = By.XPath("//ul[@class='co_navOptions']");
        private static readonly By ToolbarLocator = By.XPath("//div[@class='co_navTools DA-recommendationToolbar'] | //ul[@class='co_navOptions'] | //ul[contains(@class,'contentNav')]");
        private static readonly By ZeroStateMessageLocator = By.ClassName("Error");
        private static readonly By DescriptionMessageLocator = By.XPath("//div[@class='DescriptionMessage']");
        private static readonly By InfoBoxContainerLocator = By.XPath("//div[@class='TabDescription']");
        private static readonly By CitationIssuesButtonLocator = By.Id("potential-citation-errors-button");
        private static readonly By CitationLinkLocator = By.XPath("//saf-anchor[contains(@class,'citationLink')]");
        
        private static readonly By CitationIssuesComponentLocator = By.XPath("//div[@id='DA-Content']//span[contains(@class,'buttonCitation')]//following-sibling::div"); 

        /// <summary>
        /// Potential citation button
        /// </summary>
        public IButton CitationIssuesButton => new Button(CitationIssuesButtonLocator);

        /// <summary>
        /// Citation Issues Component
        /// </summary>
        public CitationIssuesComponent CitationIssuesComponent => new CitationIssuesComponent(CitationIssuesComponentLocator);

        /// <summary>
        /// Gets the Select all Component 
        /// </summary>
        public SelectAllComponent SelectAllComponent { get; } = new SelectAllComponent(SelectAllComponentLocator);

        /// <summary>
        /// Tab description info box
        /// </summary>
        public IInfoBox TabInfoBox => new InfoBox(InfoBoxContainerLocator);

        /// <summary>
        /// Result list title label
        /// </summary>
        public ILabel ResultsListTitleLabel => new Label(ResultsListPageTitleContainer);

        /// <summary>
        /// Citation Link
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLink => new ElementsCollection<Link>(CitationLinkLocator);

        /// <summary>
        /// Description messages
        /// </summary>
        public IReadOnlyCollection<ILabel> DescriptionMessageLabels => new ElementsCollection<Label>(DescriptionMessageLocator);

        /// <summary>
        /// Zero state message label
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(ZeroStateMessageLocator);

        /// <summary>
        /// The toolbar.
        /// </summary>
        public QuickCheckToolbar Toolbar => new QuickCheckToolbar(DriverExtensions.WaitForElement(ToolbarLocator));

        /// <summary>
        /// Gets the tab name.
        /// </summary>
        protected override string TabName => DriverExtensions.GetElement(this.ComponentLocator).Text;        
    }
}