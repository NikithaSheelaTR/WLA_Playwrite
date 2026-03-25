namespace Framework.Common.UI.Products.WestLawNextCanada.Components.CompareText
{
    using Framework.Common.UI.Products.WestlawEdge.Components.CompareText;
    using Framework.Common.UI.Products.WestlawEdge.Components.TabPanel;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.CompareText;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Canada Compare text report tab
    /// </summary>
    public class CanadaCompareTextReportTabPanel : CompareTextReportTabPanel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTextReportTabPanel"/> class
        /// </summary>
        public CanadaCompareTextReportTabPanel()
        {
            this.ActiveTab = new KeyValuePair<CompareTextReportTabs, BaseTabComponent>(CompareTextReportTabs.SideBySideView, new CanadaSideBySideViewTab());
            this.AllPossibleTabOptions = new Dictionary<CompareTextReportTabs, Type>
                                             {
                                                 { CompareTextReportTabs.SideBySideView, typeof(CanadaSideBySideViewTab) },
                                                 { CompareTextReportTabs.CompareView, typeof(CompareViewTab) }
            };
        }
    }
}