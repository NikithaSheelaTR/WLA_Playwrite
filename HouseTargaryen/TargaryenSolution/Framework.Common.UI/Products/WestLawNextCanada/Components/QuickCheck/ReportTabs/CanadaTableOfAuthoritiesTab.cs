namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Table of Authorities Tab for Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class CanadaTableOfAuthoritiesTab : TableOfAuthoritiesTab
    {
        private static readonly By ToolbarLocator = By.ClassName("co_navTools");

        /// <summary>
        /// Toolbar component.
        /// </summary>
        public new CanadaQuickCheckToolbar Toolbar => new CanadaQuickCheckToolbar(DriverExtensions.WaitForElement(ToolbarLocator));
    }
}