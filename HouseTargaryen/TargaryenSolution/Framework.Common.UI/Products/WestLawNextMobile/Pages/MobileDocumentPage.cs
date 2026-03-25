namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNextMobile.Components;
    using Framework.Common.UI.Products.WestLawNextMobile.Dropdowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DocumentPage
    /// </summary>
    public class MobileDocumentPage : MobileBasePage
    {
        private static readonly By BackButtonLocator = By.XPath("//img[@alt = 'Back to list']");

        private static readonly By DocumentNoteLocator = By.XPath("//a[@class='icn i7b']");

        private static readonly By DocumentFlagIconLocator = By.XPath("//div[@class='snt']/a/img");

        private static readonly By NextDocumentButtonLocator = By.XPath("//img[(@alt='Next Document >')]");

        private static readonly By DocumentTitleLocator = By.ClassName("title");

        private static readonly By DocumentCitationLocator = By.ClassName("co_cites");

        private static readonly By CitedCasesLinkLocator =
            By.XPath("//div[contains(@class, 'co_headnoteCitedCaseRef')]/a[text()='Cases that cite this headnote']");

        private static readonly By DocumentBodyLocator = By.Id("co_document");

        private static readonly By NextPartOfDocumentLinkLocator = By.XPath("//a[contains(text(), 'Next Part >')]");

        private static readonly By SkipToButtonLocator = By.Id("coid_website_navigationExpandCollapse");

        /// <summary>
        /// The KeyCite map.
        /// </summary>
        private EnumPropertyMapper<KeyCiteFlag, WebElementInfo> keyCiteMap;

        /// <summary>
        /// Gets the component for the Related Info section at the bottom of the page
        /// </summary>
        public RelatedInfoSectionComponent RiSection { get; } = new RelatedInfoSectionComponent();

        /// <summary>
        /// Tools dropdown
        /// </summary>
        public ToolsDropdown TopTools { get; } = new ToolsDropdown();

        /// <summary>
        /// Bottom Tools component
        /// </summary>
        public BottomToolsComponent BottomTools { get; } = new BottomToolsComponent();

        /// <summary>
        /// Gets the KeyCite enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteMap
            => this.keyCiteMap = this.keyCiteMap ?? EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>("Mobile");

        /// <summary>
        /// Clicks the Back button to get back to search results
        /// </summary>
        /// <typeparam name="TPage"> Page object type  </typeparam>
        /// <returns> Page object </returns>
        public TPage ClickBackButton<TPage>() where TPage : BaseModuleRegressionPage
            => this.ClickElement<TPage>(BackButtonLocator);

        /// <summary>
        /// Checks if the Back button is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBackButtonDisplayed() => DriverExtensions.IsDisplayed(BackButtonLocator, 5);

        /// <summary>
        /// Clicks the document note link
        /// </summary>
        /// <typeparam name="TPage"> Page object type  </typeparam>
        /// <returns> Page object </returns>
        public TPage ClickDocNoteLink<TPage>() where TPage : BaseModuleRegressionPage
            => this.ClickElement<TPage>(DocumentNoteLocator);

        /// <summary>
        /// Checks if the Document Note is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDocNoteDisplayed() => DriverExtensions.IsDisplayed(DocumentNoteLocator, 5);

        /// <summary>
        /// Clicks the 'Cases that cite this headnote' link on the page
        /// </summary>
        /// <typeparam name="TPage"> Page object type </typeparam>
        /// <param name="index"> The index of the link to click </param>
        /// <returns> Page object </returns>
        public TPage ClickHeadnoteCitingReferencesLink<TPage>(int index = 0) where TPage : BaseModuleRegressionPage
        {
            DriverExtensions.GetElements(CitedCasesLinkLocator).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Clicks the key Negative Treatment link
        /// </summary>
        /// <param name="flag"> The flag. </param>
        /// <typeparam name="TPage"> Page object type  </typeparam>
        /// <returns> Page object </returns>
        public TPage ClickKeyCiteLink<TPage>(KeyCiteFlag flag = KeyCiteFlag.NoFlag)
            where TPage : BaseModuleRegressionPage
        {
            this.GetFlagElement(flag).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Determine if the flag is displayed.
        /// </summary>
        /// <param name="flag"> The flag. </param>
        /// <returns> True if displayed. </returns>
        public bool IsFlagDisplayed(KeyCiteFlag flag) => this.GetFlagElement(flag).Displayed;

        /// <summary>
        /// Clicks the 'Next' button to go to next document from search result
        /// </summary>
        /// <typeparam name="TPage"> Page object type  </typeparam>
        /// <returns> Page object </returns>
        public TPage ClickNextDocumentButton<TPage>() where TPage : BaseModuleRegressionPage
            => this.ClickElement<TPage>(NextDocumentButtonLocator);

        /// <summary>
        /// Checks if the Next document button is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsNextDocumentButtonDisplayed() => DriverExtensions.IsDisplayed(NextDocumentButtonLocator, 5);

        /// <summary>
        /// Returns the title of the document
        /// </summary>
        /// <returns>Title of the document</returns>
        public string GetDocumentTitle() => DriverExtensions.GetImmediateText(DocumentTitleLocator).Trim();

        /// <summary>
        /// Get the text of the primary citation for the document
        /// </summary>
        /// <returns>The text</returns>
        public string GetPrimaryCitation() => DriverExtensions.GetText(DocumentCitationLocator);

        /// <summary>
        /// Checks if the document body is displayed.
        /// </summary>
        /// <returns>If the body is displayed.</returns>
        public bool IsDocumentDisplayed() => DriverExtensions.IsDisplayed(DocumentBodyLocator, 5);

        /// <summary>
        /// Checks if the next part of the document link is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsNextPartLinkDisplayed() => DriverExtensions.IsDisplayed(NextPartOfDocumentLinkLocator, 5);

        /// <summary>
        /// Checks if the Skip To button is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSkipToButtonDisplayed() => DriverExtensions.IsDisplayed(SkipToButtonLocator, 5);

        private IWebElement GetFlagElement(KeyCiteFlag flag)
        {
            DriverExtensions.WaitForElement(DocumentFlagIconLocator, 10000);
            return DriverExtensions.GetElements(DocumentFlagIconLocator)
                            .First(el => el.GetAttribute("src").Contains(this.KeyCiteMap[flag].SourceFile));
        }
    }
}