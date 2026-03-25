namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Citation dialog upon clicking on the citation/flag link on report
    /// </summary>
    public class CitationDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class,'CitationPopover-module__citationPopover')]");
        private static readonly By CloseButtonLocator = By.XPath(".//saf-button-v3[contains(@class,'CitationPopover-module__citationPopoverCloseButton')]");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath(".//saf-anchor-v3[contains(@id,'popover-flags-report-flags') and contains(@href,'RelatedInformation')]");
        private static readonly By CitationTitleLinkLocator = By.XPath(".//saf-anchor-v3[contains(@data-testid,'document-title-link') and contains(@href,'Document')]");

        /// <summary>
        /// Close Button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Citation Title Link
        /// </summary>
        public IReadOnlyCollection<ILink> CitationTitleLink => new ElementsCollection<Link>(ContainerLocator, CitationTitleLinkLocator);

        /// <summary>
        /// KeyCite Flag Link
        /// </summary>
        public IReadOnlyCollection<ILink> KeyCiteFlagLink => new ElementsCollection<Link>(ContainerLocator, KeyCiteFlagLinkLocator);
    }
}


