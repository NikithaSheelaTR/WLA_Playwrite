namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Research Recommendations tab panel component
    /// </summary>
    public class RrTabPanelComponent : TabPanel<ContentType>
    {
        private static readonly By ActiveTabLocator = By.XPath("//ul[@class='co_tabs researchAcceleratorTabs']//li[contains(@class, 'co_tabActive')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="RrTabPanelComponent"/> class. 
        /// </summary>
        public RrTabPanelComponent()
        {
            this.ActiveTab = new KeyValuePair<ContentType, BaseTabComponent>(ContentType.Cases, new CasesTabComponent());
            this.AllPossibleTabOptions = new Dictionary<ContentType, Type>
                                             {
                                                 {
                                                     ContentType.Cases,
                                                     typeof(CasesTabComponent)
                                                 },
                                                 {
                                                     ContentType.StatutesAndCourtRules,
                                                     typeof(StatutesAndCourtRulesTabComponent)
                                                 },
                                                 {
                                                     ContentType.SecondarySources,
                                                     typeof(SecondarySourcesTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Gets the RrTabs enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<ContentType, WebElementInfo> TabsMap
            => EnumPropertyModelCache.GetMap<ContentType, WebElementInfo>("Rr");

        /// <summary>
        /// Is Tab active
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsActive(ContentType tab)
            => DriverExtensions.WaitForElement(ActiveTabLocator).GetText().Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab displayed
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDisabled(ContentType tab) => DriverExtensions.WaitForElement(By.XPath(this.TabsMap[tab].LocatorString)).GetAttribute("class") == "co_tabLeft  co_tabDisabled";

        /// <summary>
        /// Is Tab displayed
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed(ContentType tab) => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}
