namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Attorneys Tab Search Component
    /// </summary>
    public class LitigationAnalyticsAttorneysEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("Tab-panel-container");
        private static readonly By ByNameRadiobuttonLocator = By.Id("attorneyName");
        private static readonly By ByExperienceRadiobuttonLocator = By.Id("attorneyExpertise");
        private static readonly By JurisdictionButtonLocator = By.ClassName("jurisdiction_button");

        /// <summary>
        /// By Name Radiobutton
        /// </summary>
        public IRadiobutton ByNameRadiobutton => new Radiobutton(ByNameRadiobuttonLocator);

        /// <summary>
        /// By Experience Radiobutton
        /// </summary>
        public IRadiobutton ByExperienceRadiobutton => new Radiobutton(ByExperienceRadiobuttonLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(JurisdictionButtonLocator);

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Attorneys";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}