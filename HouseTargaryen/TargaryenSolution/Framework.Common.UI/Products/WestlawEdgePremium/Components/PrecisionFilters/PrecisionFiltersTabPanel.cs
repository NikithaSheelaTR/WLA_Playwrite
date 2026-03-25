namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Filters Tab Panel
    /// </summary>
    public class PrecisionFiltersTabPanel: TabPanel<PrecisionFiltersTab>
    {
        private static readonly By CurrentActiveTabLocator =
           By.XPath("//div[@id='legalSimilaritySearchModal']//li[@class='Tab Tab--active']");

        private static readonly By TabLocator =
           By.XPath("//div[@id='legalSimilaritySearchModal']//li[contains(@class, 'Tab')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionFiltersTabPanel"/> class. 
        /// </summary>
        public PrecisionFiltersTabPanel()
        {
            this.AllPossibleTabOptions =
                new Dictionary<PrecisionFiltersTab, Type>
                    {
                        { PrecisionFiltersTab.LegalIssueAndOutcome, typeof(PrecisionLegalIssueAndOutcomeTabComponent ) },
                        { PrecisionFiltersTab.FactPattern, typeof(PrecisionFactPatternTabComponent ) },
                        { PrecisionFiltersTab.CauseOfAction, typeof(PrecisionCauseOfActionTabComponent ) },
                        { PrecisionFiltersTab.MotionTypeAndOutcome, typeof(PrecisionMotionTypeAndOutcomeTabComponent) },
                        { PrecisionFiltersTab.GoverningLaw, typeof(PrecisionGoverningLawTabComponent) },
                        { PrecisionFiltersTab.IndustryType, typeof(PrecisionIndustryTypeTabComponent) },
                        { PrecisionFiltersTab.PartyType, typeof(PrecisionPartyTypeTabComponent ) },
                        { PrecisionFiltersTab.AreaOfLaw, typeof(PrecisionAreaOfLawTabComponent ) },           
                    };
        }

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab to verify</param>
        /// <returns>True if tab is active, false otherwise</returns>
        public override bool IsActive(PrecisionFiltersTab tab)
        {
            var activeTab = DriverExtensions.SafeGetElement(CurrentActiveTabLocator);
            if (activeTab == null)
            {
                return false;
            }
            else
            {
                string activeTabText = activeTab.Text;
                string tabToCheckText = this.TabsMap[tab].Text;
                return activeTabText.Equals(tabToCheckText);
            }
        }

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab to verify</param>
        /// <returns>True if tab is displayed, false otherwise</returns>
        public override bool IsDisplayed(PrecisionFiltersTab tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);

        /// <summary>
        /// List of tabs
        /// </summary>
        public IReadOnlyCollection<ILabel> TabsLabels => new ElementsCollection<Label>(TabLocator);

        /// <summary>
        /// Precision filters tab map
        /// </summary>
        protected override EnumPropertyMapper<PrecisionFiltersTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<PrecisionFiltersTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium");
    }
}
