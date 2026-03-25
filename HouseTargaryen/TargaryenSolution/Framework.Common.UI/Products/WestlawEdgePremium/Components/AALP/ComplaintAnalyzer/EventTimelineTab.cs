namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Event Time line Tab
    /// </summary>
    public class EventTimelineTab : BaseComplaintAnalyzerSkillLandingPageTab
    {
        private static readonly By TabContainerLocator = By.XPath("//saf-tab-panel-v3[@data-testid='results-eventtimeline-content-panel'] | //saf-tab-panel[@data-testid='results-eventtimeline-content-panel']");

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public ComplaintAnalyserEventsResultListComponent ResultList => new ComplaintAnalyserEventsResultListComponent(DriverExtensions.GetElement(this.ComponentLocator, ResultListContainerLocator));

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Event timeline";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
