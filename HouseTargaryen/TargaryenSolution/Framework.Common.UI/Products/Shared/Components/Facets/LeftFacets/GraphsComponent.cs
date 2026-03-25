namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.GraphDialog;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Ip graph facet
    /// </summary>
    public class GraphsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_ip_facetGraph_external");

        private static readonly By HighChartsContainerLocator = By.ClassName("highcharts-container");

        private static readonly By IpGraphsFacetContentBoxLocator = By.Id("co_searchResults_ip_facetGraph");

        private static readonly By OpenGraphsDialogButtonLocator = By.Id("co_searchResults_ip_facetGraph_popup");

        private static readonly By ShowOrHideGraphsLinkLocator = By.Id("co_searchResults_ip_facetGraph_genericBoxHeader");

        private static readonly By GraphChartLocator = By.XPath(".//*[contains(@class,'highcharts-tracker')]/*[name()='rect' or name() = 'path']");

        /// <summary>
        /// Graphs type dropdown
        /// </summary>
        public GraphsTypesDropDown GraphsDropDown { get; } = new GraphsTypesDropDown();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Expands graphs facet if it is collapsed
        /// </summary>
        public void ExpandGraphsFacet()
        {
            if (!this.IsGraphsFacetExpanded())
            {
                this.ClickShowOrHideGraphsLink();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Click Show/Hide graphs link
        /// </summary>
        public void ClickShowOrHideGraphsLink() => DriverExtensions.GetElement(ShowOrHideGraphsLinkLocator).Click();

        /// <summary>
        /// Get show graphs/hide graphs link text
        /// </summary>
        /// <returns>  Get show graphs/hide graphs link text </returns>
        public string GetShowOrHideGraphsLinkText() => DriverExtensions.GetText(ShowOrHideGraphsLinkLocator);

        /// <summary>
        /// Verify if chart is displayed
        /// </summary>
        /// <returns>  true if the chart is displayed otherwise returns false </returns>
        public bool IsChartDisplayed() => DriverExtensions.IsDisplayed(HighChartsContainerLocator, 5);

        /// <summary>
        /// Checks if Graphs facet is displayed
        /// </summary>
        /// <returns> true if displayed and false if not </returns>
        public bool IsGraphsFacetExpanded() => DriverExtensions.IsDisplayed(IpGraphsFacetContentBoxLocator);

        /// <summary>
        /// Checks if Graphs facet is collapsed
        /// </summary>
        /// <returns> true if collapsed and false if expanded </returns>
        public bool IsGraphsFacetCollapsed() => DriverExtensions.GetElement(IpGraphsFacetContentBoxLocator).GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// Hovers chart
        /// </summary>
        /// <returns> Pop Up dialog </returns>
        public ChartsPopUpDialog HoverChart() 
        {
            DriverExtensions.GetElement(ContainerLocator, GraphChartLocator).SeleniumHover();
            return new ChartsPopUpDialog();
        }

        /// <summary>
        /// Click open graphs dialog link.
        /// </summary>
        /// <returns> New page Graphs dialog </returns>
        public GraphsDialog OpenGraphsDialog()
        {
            DriverExtensions.GetElement(OpenGraphsDialogButtonLocator).Click();
            return new GraphsDialog();
        }
    }
}