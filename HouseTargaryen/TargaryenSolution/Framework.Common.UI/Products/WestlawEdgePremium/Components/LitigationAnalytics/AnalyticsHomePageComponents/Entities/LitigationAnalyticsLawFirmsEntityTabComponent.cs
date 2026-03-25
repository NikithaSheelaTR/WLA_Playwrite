namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using OpenQA.Selenium;

    /// <summary>
    /// Law firms tab
    /// </summary>
    public class LitigationAnalyticsLawFirmsEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-LawFirms");
        private static readonly By ByNameRadiobuttonLocator = By.XPath("//div[@class ='co_searchForm-radioGroupItem']//input[@value='ByName']");
        private static readonly By CompareRadiobuttonLocator = By.XPath("//div[@class ='co_searchForm-radioGroupItem']//input[@value='Compare']");

        /// <summary>
        /// By Name Radiobutton
        /// </summary>
        public IRadiobutton ByNameRadiobutton => new Radiobutton(ByNameRadiobuttonLocator);

        /// <summary>
        /// By Experience Radiobutton
        /// </summary>
        public IRadiobutton CompareRadiobutton => new Radiobutton(CompareRadiobuttonLocator);

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "LawFirms";

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}