namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The document analyzer report tab panel.
    /// </summary>
    public class QuickCheckReportTabPanel : TabPanel<QuickCheckReportTabs>, ICreatablePageObject
    {
        private const string TabTextLctMask = "//li//*[text()='{0}']";
        private static readonly By CurrentActiveTab = By.XPath("//li[contains(@class,'co_tabActive')]");
        private static readonly By AllTabsLocator = By.ClassName("co_tabRight");
        private static readonly By ReportPageLocator = By.XPath("//div[@id='coid_website_documentAnalyzerReportPage']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckReportTabPanel"/> class
        /// </summary>
        public QuickCheckReportTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<QuickCheckReportTabs, Type>
                                             {
                                                 { QuickCheckReportTabs.Recommendations, typeof(RecommendationsTab) },
                                                 { QuickCheckReportTabs.OmittedAuthority, typeof(RecommendationsTab) },                                                
                                                 { QuickCheckReportTabs.TableOfAuthorities, typeof(TableOfAuthoritiesTab) },
                                                 { QuickCheckReportTabs.WarningsForCitedAuthority, typeof(WarningsForCitedAuthorityTab) },
                                                 { QuickCheckReportTabs.QuotationAnalysis, typeof(LanguageQuotationAnalysisTab) },
                                                 { QuickCheckReportTabs.LanguageAnalysis, typeof(LanguageQuotationAnalysisTab) },
                                                 { QuickCheckReportTabs.JudicialRecommendations, typeof(JudicialRecommendationsTab) },
                                                 { QuickCheckReportTabs.JudicialCitedAuthority, typeof(JudicialCitedAuthorityTab) },
                                                 { QuickCheckReportTabs.JudicialQuoteAlerts, typeof(JudicialQuotationsTab) }
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
        /// Available tabs
        /// </summary>
        public IReadOnlyCollection<ILabel> AvailableTabsLabels => new ElementsCollection<Label>(AllTabsLocator);

        /// <summary>
        /// Report page section locator
        /// </summary>
        public ILabel ReportPageLabel => new Label(ReportPageLocator);

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
        /// The get quotation analysis tab.
        /// </summary>
        /// <returns>
        /// The <see cref="LanguageQuotationAnalysisTab"/>.
        /// </returns>
        public LanguageQuotationAnalysisTab GetQuotationAnalysisTab() =>
            this.SetActiveTab<LanguageQuotationAnalysisTab>(QuickCheckReportTabs.QuotationAnalysis);

        /// <summary>
        /// The get language analysis tab.
        /// </summary>
        /// <returns>
        /// The <see cref="LanguageQuotationAnalysisTab"/>.
        /// </returns>
        public LanguageQuotationAnalysisTab GetLanguageAnalysisTab() =>
            this.SetActiveTab<LanguageQuotationAnalysisTab>(QuickCheckReportTabs.LanguageAnalysis);

        /// <summary>
        /// The get Judicial recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialRecommendationsTab"/>.
        /// </returns>
        public JudicialRecommendationsTab GetJudicialRecommendationsTab() =>
            this.SetActiveTab<JudicialRecommendationsTab>(QuickCheckReportTabs.JudicialRecommendations);

        /// <summary>
        /// The get Judicial cited authority tab.
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialCitedAuthorityTab"/>.
        /// </returns>
        public JudicialCitedAuthorityTab GetJudicialCitedAuthorityTab() =>
            this.SetActiveTab<JudicialCitedAuthorityTab>(QuickCheckReportTabs.JudicialCitedAuthority);

        /// <summary>
        /// The get Judicial quote alerts tab.
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialQuotationsTab"/>.
        /// </returns>
        public JudicialQuotationsTab GetJudicialQuoteAlertsTab() =>
            this.SetActiveTab<JudicialQuotationsTab>(QuickCheckReportTabs.JudicialQuoteAlerts);

        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="RecommendationsTab"/>.
        /// </returns>
        public RecommendationsTab GetRecommendationsTab() =>
            this.SetActiveTab<RecommendationsTab>(QuickCheckReportTabs.Recommendations);

        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="RecommendationsTab"/>.
        /// </returns>
        public RecommendationsTab GetOmittedAuthorityTab() =>
            this.SetActiveTab<RecommendationsTab>(QuickCheckReportTabs.OmittedAuthority);

        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="TableOfAuthoritiesTab"/>.
        /// </returns>
        public TableOfAuthoritiesTab GetTableOfAuthoritiesTab() =>
            this.SetActiveTab<TableOfAuthoritiesTab>(QuickCheckReportTabs.TableOfAuthorities);

        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="WarningsForCitedAuthorityTab"/>.
        /// </returns>
        public WarningsForCitedAuthorityTab GetWarningsForCitedAuthorityTab() =>
            this.SetActiveTab<WarningsForCitedAuthorityTab>(
                QuickCheckReportTabs.WarningsForCitedAuthority);
        #endregion
    }
}