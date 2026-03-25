namespace Framework.Common.UI.Products.WestlawEdge.Components.HomePage
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items.HotDocuments;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Hot documents component on home page 
    /// </summary>
    public class HotDocumentsComponent : BaseModuleRegressionComponent
    {
        private static readonly By HotDocItemLocator = By.XPath(
            "//div[contains(@class,'co_genericBox co_expandedState co_dataFeedWidget styled')]//div[@class='co_genericBoxContent']//li");
            
        private static readonly By HotDocumentsComponentNameLocator = By.XPath("//div[contains(@class,'co_hotDocuments')]/h3");

        private static readonly By HotDocumentsComponentLocator = By.XPath("//div[@id='co_widgets']/div[contains(@class,'co_hotDocuments')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => HotDocumentsComponentLocator;

        /// <summary>
        /// Verifies that all hot documents have links
        /// </summary>
        /// <returns> True if all hot documents have links </returns>
        public bool IsHotDocsLinkDisplayedForAllDocuments()
            => this.GetHotDocsItems().TrueForAll(item => item.DocLink.IsDisplayed());

        /// <summary>
        /// Verifies that all hot documents have not metadata
        /// </summary>
        /// <returns> True if all hot documents have metadata </returns>
        public bool IsHotDocsMetadataEmptyForAllDocuments()
            => this.GetHotDocsItems().TrueForAll(item => string.IsNullOrEmpty(item.Metadata));

        /// <summary>
        /// Verify that the Hot docs widget can display 1-3 Hot Docs items.
        /// </summary>
        /// <param name="maxAmountOfHotDocs"> The max Amount Of Hot Docs. </param>
        /// <returns> True if  Hot docs widget displays 1-3 Hot Docs items. </returns>
        public bool IsAmountOfHotDocsNotMoreThanLimit(int maxAmountOfHotDocs)
            => DriverExtensions.GetElements(HotDocItemLocator).Count <= maxAmountOfHotDocs;

        /// <summary>
        /// Gets hot doc link name. 
        /// </summary> 
        /// <param name="docNumber"> The doc number(from 1 to 3)</param>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetHotDocLinkName(int docNumber = 1)
            => this.GetHotDocsItems()[docNumber - 1].DocLink.GetText();

        /// <summary>
        /// Verifies that the each Hot Docs item has a description /excerpt text with a 250 character limit.
        /// </summary>
        /// <param name="characterLimit"> The character Limit. </param>
        /// <returns> True if the each Hot Docs item has a description /excerpt text with a 250 character limit. </returns>
        public bool IsHotDocsDescriptionLengthNotMoreThanLimit(int characterLimit)
            => this.GetHotDocsItems().TrueForAll(item => item.Metadata.Length <= characterLimit);

        /// <summary>
        /// Clicks hot docs link.
        /// </summary>
        /// <param name="docLinkName"> The doc number. </param>
        /// <returns> The <see cref="CommonDocumentPage"/>. </returns>
        public CommonDocumentPage ClickHotDocsLink(string docLinkName)
        {
            DriverExtensions.Click(this.GetHotDocsItems().Find(item => item.DocLink.Text.Equals(docLinkName)).DocLink);
            return new CommonDocumentPage();
        }

        /// <summary>
        /// Gets Hot Doc Component Name
        /// </summary>
        /// <returns> Hot Doc Component Name </returns>
        public string GetHotDocComponentName() => DriverExtensions.GetText(HotDocumentsComponentNameLocator);

        /// <summary>
        /// Gets list of Hot Documents 
        /// </summary>
        /// <returns> List list of Hot Documents </returns>
        private List<HotDocumentsItem> GetHotDocsItems()
        {
            var item = new HotDocumentsItem();
            return item.GetHotDocsItems();
        }
    }
}
