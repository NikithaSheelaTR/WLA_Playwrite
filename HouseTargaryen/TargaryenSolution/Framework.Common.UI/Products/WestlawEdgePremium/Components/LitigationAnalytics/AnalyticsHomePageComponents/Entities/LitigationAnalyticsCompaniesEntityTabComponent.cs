namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Companies entity tab component
    /// </summary>
    public class LitigationAnalyticsCompaniesEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-Companies");
        private static readonly By MessageLocator = By.XPath("//*[contains(text(),'Litigation Analytics for Companies on Westlaw')]");
        private static readonly By ByNameRadioButtonLocator = By.Id("companyName");
        private static readonly By ByIndustryRadioButtonLocator = By.Id("companyIndustry");
        private static readonly By SelectIndustryButtonLocator = By.Id("selectIndustry");

        /// <summary>
        /// By Name checkbox
        /// </summary>
        public IRadiobutton ByNameRadioButton => new Radiobutton(ByNameRadioButtonLocator);

        /// <summary>
        /// By Experience checkbox
        /// </summary>
        public IRadiobutton ByIndustryRadioButton => new Radiobutton(ByIndustryRadioButtonLocator);

        /// <summary>
        /// Select Industry button
        /// </summary>
        public IButton SelectIndustryButton => new Button(SelectIndustryButtonLocator);

        /// <summary>
        /// tabName
        /// </summary>
        protected override string TabName => "Companies";

        /// <summary>
        /// tabName
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// IsCompaniesNotAvaliableMessageDisplayed
        /// </summary>
        /// <returns></returns>
        public bool IsCompaniesNotAvaliableMessageDisplayed() => DriverExtensions.IsDisplayed(MessageLocator);

        /// <summary>
        /// Select Industry button
        /// </summary>
        public IButton IndustryButton => new Button(SelectIndustryButtonLocator);
    }
}