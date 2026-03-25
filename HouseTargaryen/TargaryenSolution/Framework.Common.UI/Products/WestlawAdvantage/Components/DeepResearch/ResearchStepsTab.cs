namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Research Steps Tab
    /// </summary>
    public class ResearchStepsTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-researchstepstab']");
        private static readonly By JurisdictionResolutionMessageLocator = By.XPath("//saf-alert-v3[@data-testid='jurisdiction-message']");

        /// <summary>
        /// Jurisdiction Resolution Message label
        /// </summary>
        public ILabel JurisdictionResolutionMessageLabel => new Label(JurisdictionResolutionMessageLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Research Steps";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}


