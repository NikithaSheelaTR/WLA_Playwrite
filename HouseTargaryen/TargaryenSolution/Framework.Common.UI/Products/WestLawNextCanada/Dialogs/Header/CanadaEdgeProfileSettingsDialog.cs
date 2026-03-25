namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Header
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using OpenQA.Selenium;

    /// <summary>
    /// The Indigo "Profile" pop-up dialog (Profile and Sign out") that is displayed when the User icon is clicked (right top corner)
    /// </summary>
    public class CanadaEdgeProfileSettingsDialog : EdgeProfileSettingsDialog
    {
        private static readonly By UpdateOnepassProfileLocator = By.CssSelector("a.co_signOff_updateProfile");

        /// <summary>
        /// Training and Support Link
        /// </summary>
        public ILink UpdateOnepassProfileLink => new Link(UpdateOnepassProfileLocator);
    }
}
