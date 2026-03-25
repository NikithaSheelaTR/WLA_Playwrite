namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.Judicial;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial Party Switcher Component
    /// </summary>
    /// <typeparam name="TTab">Tab type</typeparam>
    public sealed class JudicialRecommendationsTabPartySwitcherComponent<TTab> : TabPanel<JudicialParties>
        where TTab : BaseTabComponent
    {
        private static readonly By ActiveTabLocator =
            By.XPath("//*[@id='DA-Content']/ul[@class='co_tabs DA-partyTabs']//span");

        /// <summary>
        /// Party switcher tabs mapper 
        /// </summary>
        protected override EnumPropertyMapper<JudicialParties, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<JudicialParties, WebElementInfo>(
                "",
                @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// Set active party tab
        /// </summary>
        /// <typeparam name="TTabType">Tab</typeparam>
        /// <param name="tab">Party</param>
        /// <returns>TTab</returns>
        public override TTabType SetActiveTab<TTabType>(JudicialParties tab)
        {
            DriverExtensions.ScrollToTop();
            DriverExtensions.WaitForElement(By.XPath(this.TabsMap[tab].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<TTabType>();
        }

        /// <summary>
        /// Gets the first party
        /// </summary>
        /// <returns>JudicialTab</returns>
        public TTab GetFirstParty() => this.SetActiveTab<TTab>(JudicialParties.FirstParty);

        /// <summary>
        /// Gets the second party
        /// </summary>
        /// <returns>JudicialTab</returns>
        public TTab GetSecondParty() => this.SetActiveTab<TTab>(JudicialParties.SecondParty);

        /// <summary>
        /// The get omitted by both.
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialOmittedByBothPage"/>.
        /// </returns>
        public JudicialOmittedByBothPage GetOmittedByBoth()
        {
            DriverExtensions.ScrollToTop();
            DriverExtensions.GetElement(By.XPath(this.TabsMap[JudicialParties.OmittedByBoth].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<JudicialOmittedByBothPage>();
        }

        /// <summary>
        /// Verify is current tab active
        /// </summary>
        /// <param name="tab">Current tab</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(JudicialParties tab) =>
            DriverExtensions.GetElement(ActiveTabLocator).Text.Equals(
                DriverExtensions.GetText(By.XPath(this.TabsMap[tab].LocatorString)));

        /// <summary>
        /// Verify is current tab displayed
        /// </summary>
        /// <param name="tab">Current tab</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(JudicialParties tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}
