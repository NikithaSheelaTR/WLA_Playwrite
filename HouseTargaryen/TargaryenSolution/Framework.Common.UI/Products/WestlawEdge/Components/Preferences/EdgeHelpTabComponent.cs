namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Help And Training Tab Component
    /// </summary>
    public class EdgeHelpTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_Help");

        private EnumPropertyMapper<EdgeHelpTab, WebElementInfo> helpTabMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Help and Training";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the EdgePreferencesDialogTabs enumeration
        /// </summary>
        private EnumPropertyMapper<EdgeHelpTab, WebElementInfo> EdgeHelpTabMap =>
            this.helpTabMap = this.helpTabMap ?? EnumPropertyModelCache.GetMap<EdgeHelpTab, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Verify is checkbox selected
        /// </summary>
        /// <param name="tour">Tour</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsChekboxSelected(EdgeHelpTab tour) =>
            DriverExtensions.IsCheckboxSelected(By.XPath(this.EdgeHelpTabMap[tour].LocatorString));

        /// <summary>
        /// Select tour
        /// </summary>
        /// <param name="tour">Tour</param>
        /// <param name="value">Selecting value</param>
        public void SelectTour(EdgeHelpTab tour, bool value) => DriverExtensions.WaitForElementDisplayed(By.XPath(this.EdgeHelpTabMap[tour].LocatorString)).SetCheckbox(value);

        /// <summary>
        /// Is tour displayed
        /// </summary>
        /// <param name="tour">Type of tour</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTourDisplayed(EdgeHelpTab tour) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeHelpTabMap[tour].LocatorString));
    }
}