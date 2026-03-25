namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.History;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Title;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ContentViewTabPanel
    /// </summary>
    public class LitigationAnalyticsContentViewTabPanel : TabPanel<LitigationAnalyticsViewToggleTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//div[@class ='la-Chart-header']//li[contains(@class, 'Tab--active')]");
        private static readonly By DisplayChartsIndividuallyToggleLocator = By.XPath("//div[contains(@class,'la-Layout-main')]//div[@class='SlideToggle-thumb-container']");
        private static readonly By SelectChartsToDisplayButtonLocator = By.XPath("//div[contains(@class, 'la-charts-selectToDisplay')]/button");
        private static readonly By ChartsComponentLocator = By.XPath("//div[contains(@class,  'la-Layout-subChartContainer')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPreviewTabPanel"/> class
        /// </summary>
        public LitigationAnalyticsContentViewTabPanel()
        {            
            SafeMethodExecutor.WaitUntil(() => DriverExtensions.IsDisplayed(By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-Success']")), 30);
        }

        ///<summary>
        /// Display charts individualy toggle
        /// </summary>
        public IToggle DisplayChartsIndividuallyToggle => new Toggle(DisplayChartsIndividuallyToggleLocator, DisplayChartsIndividuallyToggleLocator, "SlideToggle-thumb-container", "#218321");

        /// <summary>
        /// Select Charts To Display Button
        /// </summary>
        public IButton SelectChartsToDisplayButton => new Button(SelectChartsToDisplayButtonLocator);

        /// <summary>
        /// ChartComponent
        /// </summary>
        public TitleToolBarComponent TitleToolBarComponent => new TitleToolBarComponent();

        /// <summary>
        /// Header component
        /// </summary>
        public HeaderCategoryTabPanel HeaderTabPanel = new HeaderCategoryTabPanel();

        /// <summary>
        /// Chart View Container
        /// </summary>
        public ChartViewContainer ChartViewContainer => new ChartViewContainer();

        /// <summary>
        /// Content view tab panel.
        /// </summary>
        public List<ChartViewContainer> ChartViewContainerList => DriverExtensions.GetElements(ChartsComponentLocator).Select(itemContainer => new ChartViewContainer(itemContainer)).ToList();

        /// <summary>
        /// History tabs map
        /// </summary>
        protected override EnumPropertyMapper<LitigationAnalyticsViewToggleTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsViewToggleTab, WebElementInfo>(
                string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(LitigationAnalyticsViewToggleTab tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(LitigationAnalyticsViewToggleTab tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}