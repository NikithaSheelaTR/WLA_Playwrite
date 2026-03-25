namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Claims Tab
    /// </summary>
    public class ClaimsTab : BaseComplaintAnalyzerSkillLandingPageTab
    {
        private static readonly By TabContainerLocator = By.XPath("//saf-tab-panel-v3[@data-testid='results-claims-content-panel'] | //saf-tab-panel[@data-testid='results-claims-content-panel']");

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public ComplaintAnalyserClaimsResultListComponent ResultList => new ComplaintAnalyserClaimsResultListComponent(DriverExtensions.GetElement(this.ComponentLocator, ResultListContainerLocator));

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Claims";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
