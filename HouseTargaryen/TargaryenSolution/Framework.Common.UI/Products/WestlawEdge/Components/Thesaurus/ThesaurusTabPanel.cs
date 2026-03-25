namespace Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Thesaurus;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The Tab panel in Thesaurus dialog
    /// </summary>
    public sealed class ThesaurusTabPanel : TabPanel<ThesaurusTabs>
    {
        private static readonly By CurrentActiveTab = By.XPath("//*[contains(@class,'Tab Tab--active') and contains(@id,'TabId')]");

        /// <summary> 
        /// Initializes a new instance of the <see cref="ThesaurusTabs"/> class. 
        /// </summary>
        public ThesaurusTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<ThesaurusTabs, Type>
                                             {
                                                 {
                                                     ThesaurusTabs.Synonyms,
                                                     typeof(SynonymsTabComponent)
                                                 },
                                                 {
                                                     ThesaurusTabs.RelatedConcepts,
                                                     typeof(RelatedConceptsTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Thesaurus Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ThesaurusTabs, WebElementInfo> TabsMap => 
            EnumPropertyModelCache.GetMap<ThesaurusTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/Thesaurus");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ThesaurusTabs tab) => 
            DriverExtensions.WaitForElement(CurrentActiveTab).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(ThesaurusTabs tab) =>
            DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Is Tab Disabled
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is disabled</returns>
        public bool IsDisabled(ThesaurusTabs tab) =>
            DriverExtensions.GetElement(By.XPath(this.TabsMap[tab].LocatorString)).GetAttribute("class").Contains("Tab--disabled");

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns>TAB object</returns>
        protected override TTab ClickTab<TTab>(ThesaurusTabs tab)
        {
            IWebElement element = DriverExtensions.WaitForElementDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
            element.CustomClick();
            return DriverExtensions.CreatePageInstance<TTab>();
        }
    }
}