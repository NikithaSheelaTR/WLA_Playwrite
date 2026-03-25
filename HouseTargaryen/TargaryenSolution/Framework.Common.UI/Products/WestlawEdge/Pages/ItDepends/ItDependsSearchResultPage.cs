namespace Framework.Common.UI.Products.WestlawEdge.Pages.ItDepends
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// ItDependsSearchResultPage class
    /// </summary>
    public class ItDependsSearchResultPage : EdgeCommonSearchResultPage
    {
        private static readonly By MultiFactorTestLinkLocator = By.XPath("//div[@id='co_factorAnalysisContainer']/a");

        private static readonly By ShowMoreButtonLocator = By.XPath("//div[contains(@class,'co_qaDetails')]//*[contains(text(),'View more')]");

        /// <summary>
        /// It depends link
        /// </summary>
        public ILink ItDependsLink => new Link(MultiFactorTestLinkLocator);

        /// <summary>
        /// Show more button
        /// </summary>
        public IButton ShowMoreButton => new Button(ShowMoreButtonLocator);        
    }
}