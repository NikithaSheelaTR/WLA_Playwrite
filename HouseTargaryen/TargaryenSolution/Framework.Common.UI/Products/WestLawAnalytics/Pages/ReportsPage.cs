namespace Framework.Common.UI.Products.WestLawAnalytics.Pages
{
    using Framework.Common.UI.Products.WestLawAnalytics.Components;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents report tab
    /// </summary>
    public class ReportsPage : BasePage
    {
        private static readonly By AnalyticsSignOffLocator = By.CssSelector("#coid_websiteHeader_signofflink");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsPage"/> class. 
        /// </summary>
        public ReportsPage()
        {
            DriverExtensions.WaitForPageLoad(120000); // 120 seconds to load, poor performance
        }

        /// <summary>
        /// Header navigation links for every page object
        /// </summary>
        public override HeaderComponent AnalyticsHeader
        {
            get
            {
                // In case driver was switched to inner frame (QuickView)
                if (!DriverExtensions.IsDisplayed(AnalyticsSignOffLocator))
                {
                    this.SwitchToParentFrame();
                }

                return new HeaderComponent();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Components.QuickView.QuickViewComponent"/> class.
        /// </summary>
        /// <returns><see cref="Components.QuickView.QuickViewComponent"/></returns>
        public QuickViewComponent QuickViewComponent { get; } = new QuickViewComponent();

        private void SwitchToParentFrame() => BrowserPool.CurrentBrowser.Driver.SwitchTo().ParentFrame();
    }
}