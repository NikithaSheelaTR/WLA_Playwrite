namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The related documents base tab components.
    /// </summary>
    public abstract class RelatedDocumentsBaseTabComponents : BaseTabComponent 
    {
        private static readonly By ShowMoreLessLinkLocator = By.ClassName("ShowMoreRelatedContent");

        private static readonly By PlusMinusIconLocator 
            = By.XPath("//a[@class='ShowMoreRelatedContent']/span[contains(@class,'Box-gray')]");


        /// <summary>
        /// Clicks show more/less link.
        /// </summary>
        /// <returns> The <see cref="EdgeCommonDocumentPage"/>. </returns>
        public EdgeCommonDocumentPage ClickShowMoreLessLink()
        {
            DriverExtensions.Click(ShowMoreLessLinkLocator);
            return new EdgeCommonDocumentPage();
        }

        /// <summary>
        /// Verifies that the show more link is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the show more link is displayed. </returns>
        public bool IsShowMoreLinkDisplayed() => DriverExtensions.GetText(ShowMoreLessLinkLocator).Equals("show more");

        /// <summary>
        /// Verifies that the show less link is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the show less link is displayed. </returns>
        public bool IsShowLessLinkDisplayed() => DriverExtensions.GetText(ShowMoreLessLinkLocator).Equals("show less");

        /// <summary>
        /// Verifies that the plus icon is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the plus icon is displayed. </returns>
        public bool IsPlusIconDisplayed() => DriverExtensions.GetAttribute("class", PlusMinusIconLocator).Contains("icon_addBox-gray");

        /// <summary>
        /// Verifies that the minus icon is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the minus icon is displayed. </returns>
        public bool IsMinusIconDisplayed() => DriverExtensions.GetAttribute("class", PlusMinusIconLocator).Contains("icon_removeBox-gray");
    }
}
