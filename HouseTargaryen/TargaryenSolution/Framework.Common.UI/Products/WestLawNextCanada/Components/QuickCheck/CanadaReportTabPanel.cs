namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.ReportTabs;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Canada Report Tab Panel for Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class CanadaReportTabPanel : QuickCheckReportTabPanel
    {
        /// <summary>
        /// Canada Report Tab Panel constructor
        /// </summary>
        public CanadaReportTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<QuickCheckReportTabs, Type>
            {
                { QuickCheckReportTabs.Recommendations, typeof(CanadaRecommendationsTab) },
                { QuickCheckReportTabs.QuotationAnalysis, typeof(LanguageQuotationAnalysisTab) },
                { QuickCheckReportTabs.LanguageAnalysis, typeof(LanguageQuotationAnalysisTab) },
                { QuickCheckReportTabs.WarningsForCitedAuthority, typeof(CanadaWarningsForCitedAuthorityTab) },
                { QuickCheckReportTabs.TableOfAuthorities, typeof(CanadaTableOfAuthoritiesTab) }
            };
        }

        /// <summary>
        /// The get recommendations tab.
        /// </summary>
        /// <returns>
        /// The <see cref="CanadaRecommendationsTab"/>.
        /// </returns>
        public new CanadaRecommendationsTab GetRecommendationsTab() =>
            this.SetActiveTab<CanadaRecommendationsTab>(QuickCheckReportTabs.Recommendations);

        /// <summary>
        /// Canada Warnings for Cited Authority Tab.
        /// </summary>
        /// <returns>
        /// The <see cref="CanadaWarningsForCitedAuthorityTab"/>.
        /// </returns>
        public new CanadaWarningsForCitedAuthorityTab GetWarningsForCitedAuthorityTab() =>
            this.SetActiveTab<CanadaWarningsForCitedAuthorityTab>(QuickCheckReportTabs.WarningsForCitedAuthority);

        /// <summary>
        /// Canada Table of Authorities tab
        /// </summary>
        /// <returns>The <see cref="CanadaTableOfAuthoritiesTab"/>.</returns>
        public new CanadaTableOfAuthoritiesTab GetTableOfAuthoritiesTab() =>
          this.SetActiveTab<CanadaTableOfAuthoritiesTab>(QuickCheckReportTabs.TableOfAuthorities);
    }
}