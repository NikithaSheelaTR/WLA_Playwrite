namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo
{
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar;

    /// <summary>
    /// MoreLikeThisPage that can be opened from the document page
    /// </summary>
    public class PrecisionMoreLikeThisPage : EdgeTabPage
    {
        private static readonly By ViewCasesButtonLocator = By.XPath("//button[contains(@class, 'co_primaryBtn Button--large')]");

        /// <summary>
        /// Tab panel
        /// </summary>
        public PrecisionFiltersTabPanel TabPanel { get; } = new PrecisionFiltersTabPanel();

        /// <summary>
        /// Selections
        /// </summary>
        public PrecisionSelectionsComponent  Selections => new PrecisionSelectionsComponent ();

        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new PrecisionMoreLikeThisTabToolbarComponent Toolbar { get; set; } = new PrecisionMoreLikeThisTabToolbarComponent();

        /// <summary>
        /// View cases button
        /// </summary>
        public IButton ViewCasesButton => new Button(ViewCasesButtonLocator);
    }
}
