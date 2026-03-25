namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Document Fixed Header Component
    /// </summary>
    public class DocumentFixedHeaderComponent : BaseModuleRegressionComponent
    {
        private const string DocumentIconLctMask = "//img[contains(@src,'{0}')]";

        private static readonly By FlagImageLocator = By.CssSelector("#co_docFixedHeader .co_citatorFlag img");

        private static readonly By FlagLinkLocator = By.CssSelector("#co_docFixedHeader .co_citatorFlag>a");

        private static readonly By DocumentStatusIconsContainerLocator = By.Id("co_documentStatusIcons");

        private static readonly By ParagraphCollectionWithSearchTerms =
            By.XPath("//div[@class='co_paragraphText'][.//a/span[@class='co_searchTerm'] and .//span[contains(@id, 'co_term_')]]");

        private static readonly By TitleLocator = By.XPath("//*[@id='co_docHeaderTitleLine'] | //a[contains(@id,'cobalt_result_regulation_title')]");

        private static readonly By CitationLocator = By.XPath("//*[@id='co_docHeaderCitation']/li");

        private static readonly By PageNumbersLocator = By.XPath("//*[@id='co_docHeaderCitation']");

        private static readonly By ContainerLocator = By.Id("co_docFixedHeader");

        private static readonly By HeaderCitationLocator = By.Id("co_docHeaderCitation");

        private static readonly By DocumentTitleLocator = By.XPath("//*[contains(@class,'co_documentHead')]");

        private static readonly By DocumentlinkTextLocator = By.PartialLinkText("CF/1988");

        private static readonly By DocumentLinkLocator = By.Id("co_link_i8FC430019F5711D8ABF3000102D1FD91");

        private static readonly By ParagraphTextLocator = By.XPath("//*[@class=' co_bold']");

        private static readonly By PdfLinkLocator = By.ClassName("co_pdfIcon");

        private static readonly By ClickHereLinkLocator = By.XPath("//div[@class='co_center co_versionUpdate']/a");

        private EnumPropertyMapper<KeyCiteFlag, WebElementInfo> keyCiteFlagsMap;

        private EnumPropertyMapper<DocumentIcon, WebElementInfo> docIconsMap;

        /// <summary>
        /// get paragraph text element
        /// </summary>
        public ILabel ParagraphTextElement => new Label(ParagraphTextLocator);

        /// <summary>
        /// get document link text locator
        /// </summary>
        public ILink DocumentLinkTextElement => new Link(DocumentlinkTextLocator);

        /// <summary>
        /// get Document link locator
        /// </summary>
        public ILink DocumentLinkElement => new Link(DocumentLinkLocator);

        /// <summary>
        /// Get Document title element
        /// </summary>
        public ILabel DocumentTitleElement => new Label(DocumentTitleLocator);

        /// <summary>
        /// Get header citation element
        /// </summary>
        public ILabel HeaderCitationElement => new Label(ComponentLocator, HeaderCitationLocator);

        /// <summary>
        /// Get Title Element to use in DragAndDropToFolder method
        /// </summary>
        public IWebElement TitleElement => DriverExtensions.WaitForElement(TitleLocator);

        /// <summary>
        /// Get Pdf Link element
        /// </summary>
        public ILink PdfLinkElement => new Link(PdfLinkLocator);

        /// <summary>
        /// Get Click Here Link element
        /// </summary>
        public ILink ClickHereLink => new Link(ComponentLocator,ClickHereLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap
            => this.keyCiteFlagsMap = this.keyCiteFlagsMap ?? EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>();

        /// <summary>
        /// Gets the DocumentIcon enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<DocumentIcon, WebElementInfo> DocIconsMap
            => this.docIconsMap = this.docIconsMap ?? EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

        /// <summary>
        /// Click KeyCite Flag in the document header
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickKeyCiteFlag<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(FlagLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// ToDo This logic need for Indigo. In the future should be removed
        /// Check if Document Status Icon Displayed
        /// (Document Right corner)
        /// </summary>
        /// <param name="iconType">
        /// Document icon Type.
        /// </param>
        /// <returns>
        /// true if icon present in the Document
        /// </returns>
        public bool IsStatusIconDisplayed(DocumentIcon iconType)
            => DriverExtensions.IsElementPresent(DocumentStatusIconsContainerLocator,
                    By.XPath(string.Format(DocumentIconLctMask, this.DocIconsMap[iconType].SourceFile)))
                && DriverExtensions.IsDisplayed(
                    DocumentStatusIconsContainerLocator,
                    By.XPath(string.Format(
                            this.DocIconsMap[iconType].LocatorMask,
                            string.Format(DocumentIconLctMask, this.DocIconsMap[iconType].SourceFile))));

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public KeyCiteFlag GetKeyCiteFlag()
        {
            if (DriverExtensions.IsDisplayed(FlagImageLocator))
            {
                string imageFilename = DriverExtensions.GetElement(FlagImageLocator).GetAttribute("src");
                var query = this.KeyCiteFlagsMap.Where(
                    pair =>
                        {
                            string sourceFileName = pair.Value.SourceFile;
                            return !string.IsNullOrEmpty(sourceFileName)
                                   && imageFilename.Contains(sourceFileName);
                        }).ToArray();
                return query.Any() ? query.Single().Key : KeyCiteFlag.NoFlag;
            }

            return KeyCiteFlag.NoFlag;
        }


        /// <summary>
        /// Clicks on the document status icon
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="iconType">IconType</param>
        /// <returns>The new instance of T page</returns>
        public T ClickStatusIcon<T>(DocumentIcon iconType) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.GetElement(
                                DocumentStatusIconsContainerLocator,
                                By.XPath(string.Format(DocumentIconLctMask, this.DocIconsMap[iconType].SourceFile))));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Document paragraph text for document with highlighted search terms
        /// </summary>
        /// <returns> The first Paragraph Text with matched search terms or empty string if text is not found </returns>
        public string GetTextForParagraphWithSearchTerms() => DriverExtensions.GetElements(ParagraphCollectionWithSearchTerms).FirstOrDefault()?.Text;

        /// <summary>
        /// Get Document Fixed Header Title Text
        /// </summary>
        /// <returns> Title text </returns>
        public string GetTitleText() => this.TitleElement.GetText();

        /// <summary>
        /// Get Document Fixed Header Citation text
        /// </summary>
        /// <returns>The List of strings </returns>
        public IList<string> GetCitationText() => DriverExtensions.GetElements(CitationLocator).Select(e => e.GetText()).ToList();

        /// <summary>
        /// Check if Key Cite Flag present in the document
        /// </summary>
        /// <returns>true if flag present</returns>
        public bool IsKeyCiteFlagDisplayed() => DriverExtensions.IsDisplayed(FlagImageLocator);

        /// <summary>
        /// Verify  Document Fixed Header Title is Displayed
        /// </summary>
        /// <returns>True if the title displayed, false otherwise </returns>
        public bool IsTitleDisplayed() => DriverExtensions.IsDisplayed(TitleLocator);

        /// <summary>
        /// Clicks document title link
        /// </summary>
        /// <typeparam name="T"> T pages </typeparam>
        /// <returns> T page.  </returns>
        public T ClickDocumentTitleLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(TitleLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get number of pages from citation block
        /// </summary>
        /// <returns> Number of pages </returns>
        public int GetNumberOfPages() => DriverExtensions.GetText(PageNumbersLocator).ConvertCountToInt();
    }
}