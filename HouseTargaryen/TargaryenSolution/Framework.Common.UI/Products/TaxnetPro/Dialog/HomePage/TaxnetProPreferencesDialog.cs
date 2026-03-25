namespace Framework.Common.UI.Products.TaxnetPro.Dialog.HomePage
{
    using Framework.Common.UI.Products.TaxnetPro.Components.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;

    /// <summary>
    /// Taxnet Pro Preferences Dialog
    /// </summary>
    public class TaxnetProPreferencesDialog : EdgePreferencesDialog
    {
        /// <summary>
        /// Preferences Tab Panel
        /// </summary>
        public new TaxnetProPreferencesTabPanel TabPanel { get; set; } = new TaxnetProPreferencesTabPanel();
    }
}
