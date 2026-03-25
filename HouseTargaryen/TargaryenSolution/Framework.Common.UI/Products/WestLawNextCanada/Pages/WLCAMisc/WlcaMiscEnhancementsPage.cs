namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.WLCAMisc
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
   
    /// <summary>
    /// Represents a page containing fields for CopyNeutralHyperlinks, LocalToggle and Citations.
    /// </summary>
    public class WlcaMiscEnhancementsPage : EdgeCommonDocumentPage
    {
        private static readonly By DocumentLanguageLocator = By.XPath(".//*[@id='crsw_languageToggleAnchor']");

        private static readonly By CopyNeutralItemLocator = By.XPath(".//*[@id='crsw_selectedTextMenuListItem_CopyNeutral']");

        private static readonly By HeadingItemLocator = By.XPath(".//*[@id='coid_website_searchAvailableFacets']/div/h1");

        private static readonly By ChildCitationHeadingLocator = By.XPath(".//*[@id='cobalt_result_search_hierarchy']");

        private static readonly By CitationLinkLoactor = By.XPath(".//*[@id='crsw_rightPaneRelatedAbridgmentDigestsLink']");

        private static readonly By LocalToggleLocator = By.XPath(".//*[@id='coid_websiteFooter_localeToggle']");

        /// <summary>
        /// Footer locale toggle link Text.
        /// </summary>
        public ILink LocalToggleByLink => new Link(LocalToggleLocator);

        /// <summary>
        /// DocumentLanguageButton
        /// </summary>
        public IButton DocumentLanguageButton => new Button(DocumentLanguageLocator);

        /// <summary>
        /// Is Copy Neutral option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public ILink CopyNeutralOptionLink => new Link(CopyNeutralItemLocator);

        /// <summary>
        /// HeadingItemByLink
        /// </summary>
        public ILink HeadingItemByLink => new Link(HeadingItemLocator);

        /// <summary>
        /// ChildCitationItemByLink
        /// </summary>
        public ILink ChildCitationHeadingByLink => new Link(ChildCitationHeadingLocator);

        /// <summary>
        /// CitationLink
        /// </summary>
        public ILink CitationItemByLink => new Link(CitationLinkLoactor);

    }
}
