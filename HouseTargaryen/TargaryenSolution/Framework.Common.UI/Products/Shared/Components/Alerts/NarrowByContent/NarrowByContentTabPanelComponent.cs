namespace Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Narrow by Content tab panel
    /// </summary>
    public class NarrowByContentTabPanelComponent : TabPanel<NarrowByContentTab>
    {
        private static readonly By ActiveTabLocator = By.CssSelector(".co_leftColumn_activePage");

        /// <summary>
        /// Initializes a new instance of the <see cref="NarrowByContentTabPanelComponent"/> class. 
        /// </summary>
        public NarrowByContentTabPanelComponent()
        {
            this.ActiveTab = new KeyValuePair<NarrowByContentTab, BaseTabComponent>(NarrowByContentTab.ContentTypes, new ContentTypesTabComponent());
            this.AllPossibleTabOptions = new Dictionary<NarrowByContentTab, Type>
                                             {
                                                {
                                                    NarrowByContentTab.ContentTypes,
                                                     typeof(ContentTypesTabComponent)
                                                 },
                                                 {
                                                     NarrowByContentTab.Jurisdiction,
                                                     typeof(JurisdictionTabComponent)
                                                 },
                                                 {
                                                     NarrowByContentTab.SearchWithinResults,
                                                     typeof(SearchWithinResultsTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Is Tab active
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsActive(NarrowByContentTab tab)
            => DriverExtensions.WaitForElement(ActiveTabLocator).GetText().Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab displayed
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed(NarrowByContentTab tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}