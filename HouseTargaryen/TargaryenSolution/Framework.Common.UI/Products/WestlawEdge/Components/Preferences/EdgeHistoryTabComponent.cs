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
    /// History Tab Component
    /// </summary>
    public class EdgeHistoryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_DeliveryHistory");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "History";

        private EnumPropertyMapper<EdgeHistoryTab, WebElementInfo> historyTabMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the HistoryTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeHistoryTab, WebElementInfo> HistoryTabMap =>
            this.historyTabMap = this.historyTabMap ?? EnumPropertyModelCache.GetMap<EdgeHistoryTab, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Returns true if the specified element on the history tab is displayed
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>If the option is visible</returns>
        public bool IsHistoryTabOptionDisplayed(EdgeHistoryTab tabOption) => DriverExtensions.IsDisplayed(By.XPath(this.HistoryTabMap[tabOption].LocatorString));

        /// <summary>
        /// Returns true if the EmailDetailedSessionSummaryCheckbox element on the history tab is selected 
        /// </summary>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsEmailDetailedSessionSummarySelected()
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.WaitForElement(By.XPath(this.HistoryTabMap[EdgeHistoryTab.EmailDetailedSessionSummaryCheckbox].LocatorString)));

        /// <summary>
        /// Sets the specified checkbox option on the history tab to the specified value.
        /// </summary>
        /// <param name="state"> state of the checkbox </param>
        /// <returns> The <see cref="EdgeHistoryTabComponent"/>HistoryTabComponent</returns>
        public EdgeHistoryTabComponent SetEmailDetailedSessionSummaryCheckbox(bool state)
        {
            DriverExtensions.WaitForElement(By.XPath(this.HistoryTabMap[EdgeHistoryTab.EmailDetailedSessionSummaryCheckbox].LocatorString)).SetCheckbox(state);
            return this;
        }
    }
}
