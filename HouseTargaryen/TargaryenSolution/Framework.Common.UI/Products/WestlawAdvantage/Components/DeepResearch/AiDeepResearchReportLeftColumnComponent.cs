namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// AI Deep Research Report Tab left column component
    /// </summary>
    public class AiDeepResearchReportLeftColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By LeftColumnContainerLocator = By.XPath("//*[contains(@class,'leftColumn_')]");
        private static readonly By VerifyButtonLocator = By.XPath(".//saf-button-v3[@data-testid='verify-button']");
        private static readonly By LeftColumnContentLocator = By.XPath(".//saf-disclosure-v3//following-sibling::div[contains(@class, 'ReportPanel-module__leftColumnContent')]");
        private static readonly By JurisdictionLabelLocator = By.XPath(".//saf-metadata-item-v3[@data-testid='jurisdictions']");
        private static readonly By ReportTypeLabelLocator = By.XPath(".//saf-metadata-item-v3[@data-testid= 'report-type']");
        private static readonly By ReportContentHeaderLocator = By.XPath(".//div[contains(@class, 'ReportPanel-module__tocHeader')]/h3");
        private static readonly By ReportContentsListLocator = By.XPath(".//saf-list-v3[contains(@class, 'TableOfContent-module__tableOfContentList')]/saf-list-item-v3");
        private const string TocTitleLinkLctMask = ".//saf-list-item-v3[contains(@class,'TableOfContent-module__tableOfContentItem')]//span[text()='{0}']";
        private static readonly By ReportTimeLabelLocator = By.XPath(".//saf-metadata-item-v3[@data-testid='timestamp']/time");
        /// <summary>
        /// Verify Button
        /// </summary>
        public IButton VerifyButton => new Button(this.ComponentLocator, VerifyButtonLocator);

        /// <summary>
        /// Jurisdiction label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.ComponentLocator, LeftColumnContentLocator, JurisdictionLabelLocator);

        /// <summary>
        /// Report Type Label
        /// </summary>
        public ILabel ReportTypeLabel => new Label(this.ComponentLocator, LeftColumnContentLocator, ReportTypeLabelLocator);

        /// <summary>
        /// Report Time Label
        /// </summary>
        public ILabel ReportTimeLabel => new Label(this.ComponentLocator, LeftColumnContentLocator, ReportTimeLabelLocator);

        /// <summary>
        /// Report Content Header
        /// </summary>
        public ILabel ReportContentHeader => new Label(this.ComponentLocator, ReportContentHeaderLocator);

        /// <summary>
        /// Report Contents section: list of TOC title links
        /// </summary>
        public IList<IWebElement> ReportContentsList => (IList<IWebElement>)DriverExtensions.GetElements(LeftColumnContentLocator, ReportContentsListLocator);

        /// <summary>
        /// TOC title at index
        /// </summary>
        /// <param name="title">position of TOC title</param>
        public IReadOnlyCollection<ILink> TocTitleLink(string title) => new ElementsCollection<Link>(ComponentLocator, By.XPath(string.Format(TocTitleLinkLctMask, title)));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => LeftColumnContainerLocator;
    }
}
