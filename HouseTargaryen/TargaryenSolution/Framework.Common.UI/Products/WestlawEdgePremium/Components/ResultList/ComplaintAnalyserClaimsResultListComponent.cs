namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyser Claims Tab result list
    /// </summary>
    public class ComplaintAnalyserClaimsResultListComponent : EdgeLegacyResultListComponent
    {
        private static readonly By ClaimsCardLocator = By.XPath(".//saf-card-v3[@data-testid='claims-section-container'] | .//saf-card[@data-testid='claims-section-container']");
        private static readonly By ClaimsCardSectionTitleLocator = By.XPath(".//saf-card-v3[@data-testid='claims-section-container']//div[contains(@class,'ClaimsSection-module__cardSection')]//h4 | .//saf-card[@data-testid='claims-section-container']//div[contains(@class,'ClaimsSection-module__cardSection')]//h4 | .//div[@data-testid='claims-content']//div[contains(@class,'ClaimsSection-module__section')]//h5");
        private static readonly By ClaimsCardPlantiffLocator = By.XPath(".//li[contains(@data-testid,'plaintiff-name')]");
        private static readonly By ClaimsCardDefendantLocator = By.XPath(".//li[contains(@data-testid,'defendant-name')]");
        private static readonly By RelatedFactsShortDescriptionLocator = By.XPath(".//div[@data-testid ='related-facts-title']//li/strong");
        private static readonly By RelatedFactsLongDescriptionLocator = By.XPath(".//div[@data-testid ='related-facts-title']//li/span");
        private static readonly By ReliefSoughtLocator = By.XPath(".//div[@data-testid ='relief-sought-title']//li");

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public ComplaintAnalyserClaimsResultListComponent(IWebElement container) : base(container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Claims card section locator
        /// </summary>
        public IReadOnlyCollection<ILabel> ClaimsCardSectionLabels => new ElementsCollection<Label>(this.Container, ClaimsCardSectionTitleLocator);

        /// <summary>
        /// Claims card plantif list locator
        /// </summary>
        public IReadOnlyCollection<ILabel> ClaimsCardPlantiffLabels => new ElementsCollection<Label>(this.Container, ClaimsCardPlantiffLocator);

        /// <summary>
        /// Claims card Defendant list locator
        /// </summary>
        public IReadOnlyCollection<ILabel> ClaimsCardDefendantLabels => new ElementsCollection<Label>(this.Container, ClaimsCardDefendantLocator);

        /// <summary>
        /// Claims card locator
        /// </summary>
        public IReadOnlyCollection<ILabel> ClaimsCardLabels => new ElementsCollection<Label>(this.Container, ClaimsCardLocator);

        /// <summary>
        /// Related Facts Short Description Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> RelatedFactsShortDescriptionLabels => new ElementsCollection<Label>(this.Container, RelatedFactsShortDescriptionLocator);

        /// <summary>
        /// Related Facts Long Description Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> RelatedFactsLongDescriptionLabels => new ElementsCollection<Label>(this.Container, RelatedFactsLongDescriptionLocator);

        /// <summary>
        /// Relief Sought Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ReliefSoughtLabels => new ElementsCollection<Label>(this.Container, ReliefSoughtLocator);

        private IWebElement Container { get; }
    }
}
