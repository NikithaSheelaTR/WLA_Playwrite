namespace Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Presents QuickView Frame
    /// </summary>
    public class QuickViewComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='quickview']/.//*[@id='co_quickview_iframe']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickViewComponent"/> class.
        /// </summary>
        public QuickViewComponent()
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator);
            BrowserPool.CurrentBrowser.Driver.SwitchTo().Frame(DriverExtensions.WaitForElementDisplayed(this.ComponentLocator));
        }

        /// <summary>
        /// Represents Analytics Report view
        /// </summary>
        public AnalyticsReportComponent AnalyticsReport { get; set; } = new AnalyticsReportComponent();

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickViewComponent"/> class.
        /// </summary>
        /// <returns> <see cref="QuickViewHeaderComponent"/></returns>
        public QuickViewHeaderComponent QuickViewHeader { get; } = new QuickViewHeaderComponent();

        /// <summary>
        /// Presents create report functionality 
        /// </summary>
        /// <returns><see cref="ReportCreationComponent"/></returns>
        public ReportCreationComponent ReportCreationComponent { get; } = new ReportCreationComponent();

        /// <summary>
        /// Represents Special Pricing view
        /// </summary>
        public SpecialPricingReportComponent SpecialPricingComponent { get; set; } = new SpecialPricingReportComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}