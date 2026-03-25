namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Base Landing Page Tab
    /// </summary>
    public abstract class BaseComplaintAnalyzerSkillLandingPageTab : BaseTabComponent
    {
        /// <summary>
        /// Result list container locator
        /// </summary>
        protected static readonly By ResultListContainerLocator = By.XPath(".//div[contains(@class,'ClaimsContent-module__contentCenter')] | .//saf-accordion-item-v3[@data-testid='claims-section-accordion-item'] | .//saf-accordion-item-v3[@data-testid='event-timeline-section-accordion-item']");
        private static readonly By ChatResultExpandButtonLocator = By.XPath(".//saf-accordion-v3//*[contains(@id, 'accordion')]");

        /// <summary>
        /// Complaint analyser filter reference
        /// </summary>
        public ComplaintAnalyserFilterComponent ComplaintAnalyserFilter => new ComplaintAnalyserFilterComponent(this.ComponentLocator);

        /// <summary>
        /// Chat Result Expand Buttons list
        /// </summary>
        public IReadOnlyCollection<IButton> ChatResultExpandButtons => new ElementsCollection<Button>(this.ComponentLocator, ChatResultExpandButtonLocator);

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Tab";
    }
}
