namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Damages Search Tab Component
    /// </summary>
    public class LitigationAnalyticsDamagesEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By SelectCaseTypeButtonLocator = By.Id("selectCaseType");
        private static readonly By SelectJurisdictionButtonLocator = By.ClassName("jurisdiction_button");
        private static readonly By ContainerLocator = By.Id("tab-Damages");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Damages";

        /// <summary>
        /// Select case type(s) button
        /// </summary>
        public IButton CaseTypeButton => new Button(SelectCaseTypeButtonLocator);

        /// <summary>
        /// Jurisdiction button 
        /// </summary>
        public IButton JurisdictionButton => new Button(SelectJurisdictionButtonLocator);

        ///<summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}