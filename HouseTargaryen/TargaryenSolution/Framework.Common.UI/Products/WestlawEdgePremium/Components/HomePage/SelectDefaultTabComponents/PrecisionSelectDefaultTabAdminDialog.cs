namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.SelectDefaultTabComponents
{
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;

    /// <summary>
    /// Select Default Tab admin Dialog
    /// </summary>
    public class PrecisionSelectDefaultTabAdminDialog : SelectDefaultTabDialog
    {
        /// <summary>
        /// Tab panel
        /// </summary>
        public PrecisionSelectDefaultTabPanel TabPanel { get; } = new PrecisionSelectDefaultTabPanel();
    }
}
