namespace Framework.Common.UI.Products.Shared.Components.CaseEvaluatorTab
{
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// component that represents the table of contents on the case evaluator report page
    /// </summary>
    public class ReportPageTableOfContentsComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// 'No Data' tab
        /// </summary>
        private static readonly By NoDataTabLink = By.XPath(".//li[@id='noDataTab']/a");

        private static readonly By ContainerLocator = By.ClassName("co_genericDocTocTabs");

        private EnumPropertyMapper<ReportPageTabs, WebElementInfo> tabsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportPageTableOfContentsComponent"/> class. 
        /// </summary>
        public ReportPageTableOfContentsComponent()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(60000, NoDataTabLink);
            this.IncludedTab = new IncludedTabComponent();
            this.ExcludedTab = new ExcludedTabComponent();
            this.NoDataTab = new NoDataTabComponent();
        }

        /// <summary>
        /// The excluded tab
        /// </summary>
        public ExcludedTabComponent ExcludedTab { get; protected set; }

        /// <summary>
        /// The included tab
        /// </summary>
        public IncludedTabComponent IncludedTab { get; protected set; }

        /// <summary>
        /// The no data tab
        /// </summary>
        public NoDataTabComponent NoDataTab { get; protected set; }

        /// <summary>
        /// Gets the Enums.ReportPageTabs enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<ReportPageTabs, WebElementInfo> TabsMap
            => this.tabsMap = this.tabsMap ?? EnumPropertyModelCache.GetMap<ReportPageTabs, WebElementInfo>();

        /// <summary>
        /// The click tab.
        /// </summary>
        /// <param name="tab">
        /// The tab.
        /// </param>
        public void ClickTab(ReportPageTabs tab)
            => DriverExtensions.WaitForElement(By.Id(this.TabsMap[tab].Id)).Click();
    }
}