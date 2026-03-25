namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Litigation Analytics Opportunity Finder Entity Tab Component
    /// </summary>
    public class LitigationAnalyticsOpportunityFinderEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.TagName("la-opportunity-finder-home-page-tab");
        private static readonly By CompanyMenuButtonLocator = By.XPath("//div[contains(@id,'dropdown')]/button");
        private static readonly By ContinueButtonLocator = By.XPath("//button[contains(@class, 'la-OpFinder-nextButton')]");
        private static string CompanyMenuItemLocatorMask = "//li[span[text()='{0}']]";

        /// <summary>
        /// Gets the Opportunity Finder Include Activity Within The Last to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<OpportunityFinderCompanydropdownItems, WebElementInfo> CompanyTypeSelection =>
            EnumPropertyModelCache.GetMap<OpportunityFinderCompanydropdownItems, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Company menu button
        /// </summary>
        public IButton CompanyMenuButton => new Button(CompanyMenuButtonLocator);

        /// <summary>
        /// Company menu button
        /// </summary>
        public IButton CompanyMenuItem(OpportunityFinderCompanydropdownItems parameter) => new Button(By.XPath(string.Format(CompanyMenuItemLocatorMask, this.CompanyTypeSelection[parameter].Text)));

        /// <summary>   
        /// Continue button
        /// </summary>
        public IButton ContinueButton => new Button(ComponentLocator, ContinueButtonLocator);

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Opportunity Finder";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}