namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Additional cases search results page
    /// </summary>
    public class PrecisionAdditionalCasesSearchResultPage: PrecisionCommonSearchResultPage
    {
        private static readonly By BackToSearchResultsLinkLocator = By.Id("co_search_backToPrimaryResultsLink");
        private static readonly By HeaderInfoIconLocator = By.XPath("//div[@class='searchHeaderInfo-container']/button");
        private static readonly By InfoBoxLocator = By.XPath("//span[starts-with(@id, 'popover-panel-')]");

        /// <summary>
        /// Back to search results link
        /// </summary>
        public ILink BackToSearchResultsLink => new Link(BackToSearchResultsLinkLocator);

        /// <summary>
        /// Header info icon button
        /// </summary>
        public ILink HeaderInfoIconButton => new Link(HeaderInfoIconLocator);

        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(DriverExtensions.GetElement(InfoBoxLocator));
    }
}
