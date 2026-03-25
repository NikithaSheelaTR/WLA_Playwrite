namespace Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial.ReportTabs;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The document analyzer report tab panel.
    /// </summary>
    public class WestlawAdvantageReportTabPanel : QuickCheckReportTabPanel, ICreatablePageObject
    {
        private const string TabTextLctMask = "//li//*[text()='{0}']";
        private static readonly By CurrentActiveTab = By.XPath("//li[contains(@class,'co_tabActive')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="WestlawAdvantageReportTabPanel"/> class
        /// </summary>
        public WestlawAdvantageReportTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<QuickCheckReportTabs, Type>
                                             {
                                                 { QuickCheckReportTabs.ArgumentsAndCounterArguments, typeof(ArgumentCounterargumentTab) },
                                                 { QuickCheckReportTabs.Recommendations, typeof(RecommendationsTab) },                                                
                                                 { QuickCheckReportTabs.TableOfAuthorities, typeof(TableOfAuthoritiesTab) },
                                                 { QuickCheckReportTabs.WarningsForCitedAuthority, typeof(WarningsForCitedAuthorityTab) },
                                                 { QuickCheckReportTabs.QuotationAnalysis, typeof(LanguageQuotationAnalysisTab) },
                                                 { QuickCheckReportTabs.LanguageAnalysis, typeof(LanguageQuotationAnalysisTab) },
                                                 { QuickCheckReportTabs.JudicialLanguageAnalysis, typeof(JudicialLanguageAnalysisTab) },
                                                 { QuickCheckReportTabs.JudicialCitedAuthority, typeof(JudicialCitedAuthorityTab)}
                                             };
        }

        /// <summary>
        /// Tab Map
        /// </summary>
        protected override EnumPropertyMapper<QuickCheckReportTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<QuickCheckReportTabs, WebElementInfo>(
                "",
                @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

         /// <summary>
        /// The is active.
        /// </summary>
        /// <param name="tab">
        /// The tab.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsActive(QuickCheckReportTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTab).Text.Replace("NEW", string.Empty);
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// The is displayed.
        /// </summary>
        /// <param name="tab">
        /// The tab.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed(QuickCheckReportTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(TabTextLctMask, this.TabsMap[tab].Text)), 5);    

        /// <summary>
        /// Set Active Tab
        /// </summary>
        /// <param name="tab"> Tab option </param>
        /// <returns> The <see cref="BaseTabComponent"/>Tab component </returns>
        public override TTab SetActiveTab<TTab>(QuickCheckReportTabs tab)
        {
            Type searchedType;
            if (!this.AllPossibleTabOptions.TryGetValue(tab, out searchedType))
            {
                throw new NotFoundException("Tab is not found");
            }

            if (this.ActiveTab.Key.Equals(tab) && this.ActiveTab.Value != null)
            {
                return (TTab)this.ActiveTab.Value;
            }

            this.ActiveTab = !this.IsActive(tab)
                                 ? new KeyValuePair<QuickCheckReportTabs, BaseTabComponent>(
                                     tab,
                                     this.ClickTab<TTab>(tab))
                                 : new KeyValuePair<QuickCheckReportTabs, BaseTabComponent>(
                                     tab,
                                     (BaseTabComponent)Activator.CreateInstance(this.AllPossibleTabOptions[tab]));

            return (TTab)this.ActiveTab.Value;
        }

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns>TAB object</returns>
        protected override TTab ClickTab<TTab>(QuickCheckReportTabs tab)
        {
            IWebElement element = DriverExtensions.WaitForElementDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
            element.CustomClick();
            return DriverExtensions.CreatePageInstance<TTab>();
        }

        #region Tabs
        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="ArgumentCounterargumentTab"/>.
        /// </returns>
        public ArgumentCounterargumentTab GetArgumentsAndCounterargumentsTab() => this.SetActiveTab<ArgumentCounterargumentTab>(QuickCheckReportTabs.ArgumentsAndCounterArguments);

        /// <summary>
        /// The judicial language analysis tab.
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialLanguageAnalysisTab"/>.
        /// </returns>
        public JudicialLanguageAnalysisTab GetJudicialLanguageAnalysisTabTab() => this.SetActiveTab<JudicialLanguageAnalysisTab>(QuickCheckReportTabs.JudicialLanguageAnalysis);
        #endregion
    }
}