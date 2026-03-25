namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyser Defenses Result List Component
    /// </summary>
    public class ComplaintAnalyserDefensesResultListComponent : EdgeLegacyResultListComponent
    {
        private static readonly By DefensesCardLocator = By.XPath(".//saf-accordion-item-v3[contains(@data-testid,'defenses-section')] | .//saf-accordion-item[contains(@data-testid,'defenses-section')]");
        private static readonly By DefensesCardTableLocator = By.XPath(".//table[contains(@class,'ClaimsElements-module__overviewTable')]");
        private static readonly By DefensesCardOverviewSectionLocator = By.XPath(".//div[contains(@class,'DefensesSection-module__defensesOverview')] | .//div[contains(@class,'DefensesSection-module__section')]");

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public ComplaintAnalyserDefensesResultListComponent(IWebElement container) : base(container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Defenses results list
        /// </summary>
        public IReadOnlyCollection<ILabel> DefensesCardLabels => new ElementsCollection<Label>(this.Container, DefensesCardLocator);

        /// <summary>
        /// Defenses results table list
        /// </summary>
        public IReadOnlyCollection<ILabel> DefensesCardTableLabels => new ElementsCollection<Label>(this.Container, DefensesCardTableLocator);

        /// <summary>
        /// Defenses results Overview section list
        /// </summary>
        public IReadOnlyCollection<ILabel> DefensesCardOverviewSectionLabels => new ElementsCollection<Label>(this.Container, DefensesCardOverviewSectionLocator);

        private IWebElement Container { get; }
    }
}
