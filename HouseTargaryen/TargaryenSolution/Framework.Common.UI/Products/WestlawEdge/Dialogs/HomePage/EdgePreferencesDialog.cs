namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using OpenQA.Selenium;

    /// <summary>
    /// Base Preferences Dialog
    /// </summary>
    public class EdgePreferencesDialog : PreferencesDialog
    {
        private static readonly By HeaderTitleLocator = By.XPath("//*[contains(@id, 'coid_lightboxAriaLabel')]");

        /// <summary>
        /// Header title
        /// </summary>
        public new ILabel HeaderTitleLabel => new Label(HeaderTitleLocator);

        /// <summary>
        /// Preferences Tab Panel
        /// </summary>
        public new EdgePreferencesTabPanel TabPanel { get; set; } = new EdgePreferencesTabPanel();
    }
}
