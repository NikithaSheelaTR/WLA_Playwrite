namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// The recommendations tab.
    /// </summary>
    public class RecommendationsTab : BaseQuickCheckTabComponent
    {
        /// <summary>
        /// The result list locator.
        /// </summary>
        protected static readonly By ResultListLocator = By.ClassName("co_issueList");
        private static readonly By TabContainerLocator = By.Id("DA-RecPage");
        private static readonly By CancelRerunButtonLocator = By.XPath("//button[@class ='co_secondaryBtn']");
        private static readonly By RerunProgressBarLocator = By.XPath("//div[@class='DA-ProgressBarBack']");
        private static readonly By JurisdictionsInfoLabelLocator = By.XPath("//div[contains(@class,'DA-jurisdictionInfo')]//*[@class='co_infoBox_message']");

        /// <summary>
        /// Rerun cancel button
        /// </summary>
        public IButton CancelRerunButton => new Button(CancelRerunButtonLocator);

        /// <summary>
        /// Selected Jurisdictions info box message label
        /// </summary>
        public ILabel JurisdictionsInfoLabel => new Label(JurisdictionsInfoLabelLocator);

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public RecommendationsResultsComponent ResultList => new RecommendationsResultsComponent(DriverExtensions.GetElement(ResultListLocator));
                
        /// <summary>
        /// Gets the document information.
        /// </summary>
        public RecommendationsDocumentInformationComponent DocumentInformation { get; } = new RecommendationsDocumentInformationComponent();

        /// <summary>
        /// Gets the highlighted text information.
        /// </summary>
        public RecommendationsHighlightedTextComponent HighlightedTextFromWestlawDoc { get; } = new RecommendationsHighlightedTextComponent();

        /// <summary>
        /// Gets the narrow pane.
        /// </summary>
        public RecommendationsNarrowPaneComponent NarrowPane { get; } = new RecommendationsNarrowPaneComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
        
        /// <summary>
        /// The get all cases recommendations.
        /// </summary>
        /// <returns>
        /// The recommendations items
        /// </returns>
        public IEnumerable<RecommendationItem> GetAllCasesRecommendations() =>
            this.ResultList.Headings.SelectMany(issue => issue.Cases);

        /// <summary>
        /// Verification that rerun progress bar is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRerunProgressBarDisplayed() => DriverExtensions.IsDisplayed(RerunProgressBarLocator);

        /// <summary>
        /// Wait for report rerunning
        /// Timeout is added due to the reason that in general rerun takes more than 1 minute
        /// </summary>
        /// <returns>
        /// The recommendations report
        /// </returns>
        public T WaitForReportRerunning<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForCondition(condition => DocumentInformation.IsDisplayed(), 150);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}