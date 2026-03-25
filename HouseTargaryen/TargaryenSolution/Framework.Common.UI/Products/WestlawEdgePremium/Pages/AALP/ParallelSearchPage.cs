namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Parallel Search page
    /// </summary>
    public class ParallelSearchPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By PageHeaderLocator = By.XPath("//h2[contains(@class, 'parallelSearchLandingHeader') and contains(text(),'Parallel Search')]");
        private static readonly By ProgressRingLabelLocator = By.XPath("//saf-progress-ring[@role='progressbar']");
        private static readonly By WarningMsgLocator = By.XPath("//div[contains(@class, '__validationMessagePhaseTwo')]/span");
                
        /// <summary>
        /// Query box Component
        /// </summary>
        public ParallelSearchQueryBoxComponent QueryBox { get; } = new ParallelSearchQueryBoxComponent();

        /// <summary>
        /// Search result Component
        /// </summary>
        public ParallelSearchResultComponent Results { get; } = new ParallelSearchResultComponent();

        /// <summary>
        /// Page header label
        /// </summary>
        public ILabel PageHeader => new Label(PageHeaderLocator);

        /// <summary>
        /// Progress ring label
        /// </summary>
        public ILabel ProgressRingLabel => new Label(ProgressRingLabelLocator);

        /// <summary>
        /// Warning Msg Label
        /// </summary>
        public ILabel WarningMsgLabel => new Label(WarningMsgLocator);
       
    }
}


