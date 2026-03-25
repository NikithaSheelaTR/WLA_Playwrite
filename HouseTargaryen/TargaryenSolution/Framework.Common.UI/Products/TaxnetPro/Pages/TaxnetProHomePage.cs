namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.TaxnetPro.Components.Header;
    using Framework.Common.UI.Products.TaxnetPro.Components.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Home Page
    /// </summary>
    public class TaxnetProHomePage : EdgeHomePage
    {
        /// <summary>
        /// Taxnet Pro Typeahead window locator
        /// </summary>
        private static readonly By TypeaheadDialogWindowLocator = By.Id("typeaheadcontainer");

        /// <summary>
        /// Gets the Taxnet Pro header.
        /// </summary>
        public new TaxnetProHeaderComponent Header { get; } = new TaxnetProHeaderComponent();

        /// <summary>
        /// Gets the Pinpoint search
        /// </summary>
        public PinpointSearchComponent Pinpoint { get; } = new PinpointSearchComponent();

        /// <summary>
        /// Gets the HomePage Components
        /// </summary>
        public HomePageComponent HomePage { get; } = new HomePageComponent();

        /// <summary>
        /// Typeahead dialog window is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsTypeaheadDialogWindowDisplayed() =>
            DriverExtensions.IsDisplayed(TypeaheadDialogWindowLocator);
    }
}