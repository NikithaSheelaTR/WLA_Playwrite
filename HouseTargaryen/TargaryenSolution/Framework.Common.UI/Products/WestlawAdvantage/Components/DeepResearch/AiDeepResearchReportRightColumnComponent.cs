namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Products.Shared.Elements;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// AI Deep Research Report Tab Right column component
    /// </summary>
    public class AiDeepResearchReportRightColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By ReportFeedbackContainerLocator = By.XPath("//div[contains(@class, 'ReportPanel-module__feedback')]");
        private static readonly By RightColumnContainerLocator = By.XPath("//*[contains(@class,'ReportPanel-module__rightColumn')]");
        private static readonly By ExpandedReportButtonLocator = By.XPath(".//button[@data-testid='request-report-response-button']");
        private static readonly By SummarySectionHeadingLabelLocator = By.XPath(".//h3[starts-with(@id, 'summary')]/span");
        private const string ResultSectionTitleLctMask = ".//h3/span[contains(text(),'{0}')]";
        private static readonly By CitationLinksLocator = By.XPath(".//span[contains(@class,'ReportInlineCitationLink-module__citationLink')]");
        private static readonly By KeyCiteFlagLinksLocator = By.XPath(".//saf-button-v3[contains(@class,'ReportKeyCiteFlags-module__keyciteFlagButton')]");
        private static readonly By ResearchReportContentsLabelLocator = By.XPath(".//p");
        private static readonly By TabsDropdownLocator = By.XPath(".//saf-select-v3[contains(@class, 'ReportAnswer-module__responsiveSelect')]");
        private const string RootScript = "return(arguments[0].shadowRoot.querySelector('div[class=root]'));";

        /// <summary>
        /// Get the feedback component
        /// </summary>
        public AiDeepResearchFeedbackComponent Feedback { get; } = new AiDeepResearchFeedbackComponent(ReportFeedbackContainerLocator);

        /// <summary>
        /// Run an expanded Report Button
        /// </summary>
        public IButton ExpandedReportButton => new Button(ComponentLocator, ExpandedReportButtonLocator);

        /// <summary>
        /// Summary section heading label
        /// </summary>
        public ILabel SummarySectionHeadingLabel => new Label(ComponentLocator, SummarySectionHeadingLabelLocator);

        /// <summary>
        /// Verify if result section scrolled in to view
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>true if scrolled into view, false otherwise</returns>
        public bool IsSectionScrolledIntoView(string title) =>
            DriverExtensions.GetElement(By.XPath(string.Format(ResultSectionTitleLctMask, title))).IsElementInView();

        /// <summary>
        /// List of citation links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks => new ElementsCollection<Link>(this.ComponentLocator, CitationLinksLocator);

        /// <summary>
        /// List of keycite flag links
        /// </summary>
        public IReadOnlyCollection<ILink> KeyCiteFlagLinks => new ElementsCollection<Link>(this.ComponentLocator, KeyCiteFlagLinksLocator);

        /// <summary>
        /// Research Report Contents label
        /// </summary>
        public IReadOnlyCollection<ILabel> ResearchReportContentsLabel => new ElementsCollection<Label>(ComponentLocator, ResearchReportContentsLabelLocator);

        /// <summary>
        /// Is Tabs DropDown Displayed
        /// </summary>
        /// <returns>true if dropdown displayed, false otherwise</returns>
        public bool IsTabsDropDownDisplayed() 
        {
            IWebElement TabDropdown = DriverExtensions.GetElement(TabsDropdownLocator);
            IWebElement dropDownRoot = (IWebElement)DriverExtensions.ExecuteScript(RootScript, TabDropdown);
            return dropDownRoot.Displayed;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => RightColumnContainerLocator;
    }
}
