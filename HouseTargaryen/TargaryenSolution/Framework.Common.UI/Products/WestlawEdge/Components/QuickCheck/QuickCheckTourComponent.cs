namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.TourComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Tours;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document Analyzer Tour Component 
    /// It consist of 2 tours and "Take the tour" card
    /// 2 tours: in Check your work mode, in Analyze an opponent's work mode
    /// </summary>
    public class QuickCheckTourComponent : BaseModuleRegressionComponent
    {
        private static readonly By TourCardLocator = By.XPath("//*[@class='co_overlayBox_container' or contains(@class,'DefaultDocumentAnalyzerStep')]");

        /// <summary>
        /// Tour notification: "Document Analyzer - Check your work" mode
        /// </summary>
        public TourCardComponent<QuickCheckCheckYourWorkTourCards> TourCardCheckYourWorkModeComponent { get; set; }
            = new TourCardComponent<QuickCheckCheckYourWorkTourCards>();

        /// <summary>
        /// Tour notification: "Document Analyzer - Analyze an opponent's work" mode
        /// </summary>
        public TourCardComponent<QuickCheckCheckOpponentsWorkTourCards> TourCardCheckOpponentsWorkModeComponent { get; set; }
            = new TourCardComponent<QuickCheckCheckOpponentsWorkTourCards>();

        /// <summary>
        /// tour for Judicial
        /// </summary>
        public TourCardComponent<JudicialTourCards> TourCardJudicialModeComponent { get; set; } =
            new TourCardComponent<JudicialTourCards>();

        /// <summary>
        /// Tour notification: "Document Analyzer - Selected Text" mode
        /// </summary>
        public TourCardComponent<QuickCheckSelectedTextTourCards> TourCardSelectedTextModeComponent { get; set; }
            = new TourCardComponent<QuickCheckSelectedTextTourCards>();

        /// <summary>
        /// Tour notification: "Document Analyzer - Selected Text" mode
        /// </summary>
        public TourCardComponent<QuickCheckOverviewTourCards> TourCardQuickCheckOverviewComponent { get; set; }
            = new TourCardComponent<QuickCheckOverviewTourCards>();

        /// <summary>
        /// DocAnalyzer tour options: Take the tour, Maybe later, Don't show me this
        /// </summary>
        public TakeTheTourComponent TakeTheTourComponent { get; set; } = new TakeTheTourComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TourCardLocator;

        /// <summary>
        /// Is it Tour card displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 30);
    }
}