namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Research report Tab
    /// </summary>
    public class ResearchReportTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-tab-panel-content']");
        private static readonly By SectionHeadersLocator = By.XPath(".//div[contains(@class,'RightColumnContent-module__results')]//h3");
        private static readonly By CitationLinksLocator = By.XPath(".//span[contains(@class,'ReportInlineCitationLink-module__citationLink')]");
        private static readonly By KeyCiteFlagLinksLocator = By.XPath(".//saf-button-v3[contains(@class,'ReportKeyCiteFlags-module__keyciteFlagButton')]");
        private const string ResultSectionTitleLctMask = "//div[contains(@class,'RightColumnContent-module__results')]/h3/span[contains(text(),'{0}')]";
        private static readonly By DeeperReportButtonLocator = By.XPath(".//button[@data-testid='request-report-response-button']");

        /// <summary>
        /// Deeper Report Button
        /// </summary>
        public IButton DeeperReportButton => new Button(ComponentLocator, DeeperReportButtonLocator);

        /// <summary>
        /// Gets a list of section headers in the Research Report tab
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetSectionHeaders()
            => DriverExtensions.GetElements(SectionHeadersLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// List of citation links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks => new ElementsCollection<Link>(this.ComponentLocator, CitationLinksLocator);

        /// <summary>
        /// List of keycite flag links
        /// </summary>
        public IReadOnlyCollection<ILink> KeyCiteFlagLinks => new ElementsCollection<Link>(this.ComponentLocator, KeyCiteFlagLinksLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Research report";

        /// <summary>
        /// Verify if result section scrolled in to view
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>true if scrolled into view, false otherwise</returns>
        public bool IsSectionScrolledIntoView(string title) =>
            DriverExtensions.GetElement(By.XPath(string.Format(ResultSectionTitleLctMask, title))).IsElementInView();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}

