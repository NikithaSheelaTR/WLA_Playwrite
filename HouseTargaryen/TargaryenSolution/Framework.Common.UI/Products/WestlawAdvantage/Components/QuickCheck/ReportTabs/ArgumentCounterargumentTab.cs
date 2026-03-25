namespace Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Collections.Generic;

    /// <summary>
    /// The Arguments and counter arguments tab.
    /// </summary>
    public class ArgumentCounterargumentTab : BaseQuickCheckTabComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@id = 'co_contentColumn']");
        private static readonly By ArgumentResultListLocator = By.XPath(".//h3[contains(text(), 'Arguments and counterarguments')]/following-sibling::ul");
        private static readonly By RelatedArgumentResultListLocator = By.XPath(".//h3[contains(text(), 'Related arguments')]//following-sibling::ul");
        private static readonly By RelatedDefensesResultListLocator = By.XPath(".//h3[contains(text(), 'Related defenses')]//following-sibling::ul");
        private static readonly By ArgumentResultItemLocator = By.XPath("//li[contains(@class, 'argumentBox') and not(contains(@id, 'relatedArg')) and not(contains(@id, 'relatedDef'))]");
        private static readonly By RelatedArgumentResultItemLocator = By.XPath("//li[contains(@id, 'relatedArg')]");
        private static readonly By RelatedDefensesResultItemLocator = By.XPath("//li[contains(@id, 'relatedDef')]");
        private static readonly By ZeroStateArgumentsMessageLabelLocator = By.XPath("//div[@class = 'Error']");
        private static readonly By ArgumentHeaderLabelLocator = By.XPath(".//h3[contains(@class,'argumentHeader')]");
        private static readonly By ArgumentSubHeaderLabelLocator = By.XPath(".//p[contains(@class,'argumentSubHeader')]");

        /// <summary>
        /// Zero State Message Label
        /// </summary>
        public ILabel ZeroStateArgumentsMessageLabel => new Label(this.ComponentLocator, ZeroStateArgumentsMessageLabelLocator);

        /// <summary>
        /// Argument header labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ArgumentHeaderLabels => new ElementsCollection<Label>(this.ComponentLocator, ArgumentHeaderLabelLocator);

        /// <summary>
        /// Argument subheader labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ArgumentSubHeaderLabels => new ElementsCollection<Label>(this.ComponentLocator, ArgumentSubHeaderLabelLocator);

        /// <summary>
        /// Document summary
        /// </summary>
        public WestlawAdvantageDocumentSummaryComponent DocumentSummary { get; } = new WestlawAdvantageDocumentSummaryComponent();

        /// <summary>
        /// The narrow pane.
        /// </summary>
        public ArgumentCounterargumentNarrowPaneComponent NarrowPane { get; } = new ArgumentCounterargumentNarrowPaneComponent();

        /// <summary>
        /// The Argument result list.
        /// </summary>
        public virtual QuickCheckItemsCollection<ArgumentCounterargumentItem> ArgumentResultList =>
            new QuickCheckItemsCollection<ArgumentCounterargumentItem>(DriverExtensions.GetElement(this.ComponentLocator, ArgumentResultListLocator), ArgumentResultItemLocator);

        /// <summary>
        /// The Related Argument result list.
        /// </summary>
        public virtual QuickCheckItemsCollection<ArgumentCounterargumentItem> RelatedArgumentResultList =>
            new QuickCheckItemsCollection<ArgumentCounterargumentItem>(DriverExtensions.GetElement(this.ComponentLocator, RelatedArgumentResultListLocator), RelatedArgumentResultItemLocator);

        /// <summary>
        /// The Related Defenses result list.
        /// </summary>
        public virtual QuickCheckItemsCollection<ArgumentCounterargumentItem> RelatedDefensesResultList =>
            new QuickCheckItemsCollection<ArgumentCounterargumentItem>(DriverExtensions.GetElement(this.ComponentLocator, RelatedDefensesResultListLocator), RelatedDefensesResultListLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}