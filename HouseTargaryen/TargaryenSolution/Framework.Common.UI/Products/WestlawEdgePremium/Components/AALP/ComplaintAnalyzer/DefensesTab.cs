namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Defenses Tab
    /// </summary>
    public class DefensesTab : BaseComplaintAnalyzerSkillLandingPageTab
    {
        private static readonly By TabContainerLocator = By.XPath(".//saf-tab-panel-v3[@data-testid='results-defenses-content-panel'] | .//saf-tab-panel[@data-testid='results-defenses-content-panel']");

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public ComplaintAnalyserDefensesResultListComponent ResultList => new ComplaintAnalyserDefensesResultListComponent(DriverExtensions.GetElement(TabContainerLocator));

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Defenses";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
