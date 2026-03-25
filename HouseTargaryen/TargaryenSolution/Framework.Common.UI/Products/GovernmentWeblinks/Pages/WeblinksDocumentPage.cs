namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks Document Page
    /// </summary>
    public class WeblinksDocumentPage : BaseGovernmentWeblinksPage
    {
        private static readonly By CitationLocator = By.XPath("//div[@id='co_document']//div[@class='co_cites']");

        private static readonly By CiteListLinkLocator = By.XPath("//span[@id='citeList']//a");

        private static readonly By CiteListLocator = By.Id("citeList");

        private static readonly By DocumentHeaderLocator = By.XPath("//div[@id='co_docHeaderTitle']");

        private static readonly By DocumentLocator = By.Id("co_document");

        private static readonly By DocumentsInSequenceLinkLocator = By.XPath("//div[@id='docsInSequence']/a");

        private static readonly By HelpLinkLocator = By.XPath("//a[contains(@class, 'co_floatRight co_helpLink')]");

        private static readonly By LeftNavigationLocator = By.Id("leftNavButtonBottom");

        private static readonly By NextDocumentButtonLocator = By.XPath("//a[@id='nextDocLink']");

        private static readonly By NextTermsLocator = By.XPath("//a[@class='co_linkBtn wl_icon icon_single_right_arrow_blue']");

        private static readonly By PreviousDocumentButtonLocator = By.XPath("//a[@id='prevDocLink']");

        private static readonly By PreviousTermsLocator =
            By.XPath("//a[@class='co_linkBtn wl_icon icon_single_left_arrow_blue']");

        private static readonly By RightNavigationLocator = By.Id("rightNavButtonBottom");

        private static readonly By RightNavigationTopLocator = By.Id("rightNavButtonTop");

        private static readonly By HighlightedTermsLocator = By.ClassName("co_searchTerm");

        /// <summary>
        /// Gets the next terms.
        /// </summary>
        /// <returns>IEnumerable of elements</returns>
        private IEnumerable<IWebElement> NextTerms => DriverExtensions.GetElements(NextTermsLocator);

        /// <summary>
        /// Gets the previous terms.
        /// </summary>
        /// <returns>IEnumerable of elements</returns>
        private IEnumerable<IWebElement> PreviousTerms => DriverExtensions.GetElements(PreviousTermsLocator);

        /// <summary>
        /// Click on the CiteListLink
        /// </summary>
        /// <typeparam name="T">The type of the param</typeparam>
        /// <returns>
        /// The <see cref="SearchResultsPage"/>.
        /// </returns>
        public T ClickCiteList<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CiteListLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks document in sequence link
        /// </summary>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickDocumentInSequenceLink()
        {
            DriverExtensions.WaitForElement(DocumentsInSequenceLinkLocator).Click();
            return new WeblinksDocumentPage();
        } 

        /// <summary>
        /// Clicks next document
        /// </summary>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickNextDocument()
        {
            DriverExtensions.WaitForElement(NextDocumentButtonLocator).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks next document bottom
        /// </summary>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickNextTermBottom()
        {
            DriverExtensions.WaitForElement(RightNavigationLocator).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks next document top
        /// </summary>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickNextTermTop()
        {
            DriverExtensions.WaitForElement(RightNavigationTopLocator).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks next term of the document
        /// </summary>
        /// <param name="index">The index of the document</param>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickNextTermByIndex(int index)
        {
            this.NextTerms.ElementAt(index).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks previous document
        /// </summary>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickPreviousDocument()
        {
            DriverExtensions.WaitForElement(PreviousDocumentButtonLocator).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks previous term of the document
        /// </summary>
        /// <param name="index">The index of the document</param>
        /// <returns>The <see cref="WeblinksDocumentPage"/>.</returns>
        public WeblinksDocumentPage ClickPreviousTermByIndex(int index)
        {
            this.PreviousTerms.ElementAt(index).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Gets citation list
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCitationText() => DriverExtensions.GetText(CitationLocator);

        /// <summary>
        /// Gets highlighted terms
        /// </summary>
        /// <returns>The list of string</returns>
        public List<string> GetHighlightedTerms() => DriverExtensions.GetElements(HighlightedTermsLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Gets document header
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDocumentTitle() => DriverExtensions.GetText(DocumentHeaderLocator);

        /// <summary>
        /// Gets next terms count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetNextTermsCount() => this.NextTerms.Count();

        /// <summary>
        /// Gets previous terms count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetPreviousTermsCount() => this.PreviousTerms.Count();

        /// <summary>
        /// Verifies is cite list displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsCiteListDisplayed() => DriverExtensions.IsDisplayed(CiteListLocator);

        /// <summary>
        /// Verifies is document displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDocumentDisplayed() => DriverExtensions.IsDisplayed(DocumentLocator);

        /// <summary>
        /// Verifies is document header displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDocumentHeaderDisplayed() => DriverExtensions.IsDisplayed(DocumentHeaderLocator);

        /// <summary>
        /// Verifies is document in sequence link displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDocumentInSequenceLinkDisplayed() => DriverExtensions.IsDisplayed(DocumentsInSequenceLinkLocator);

        /// <summary>
        /// Verifies is right term navigation button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsHelpLinkDisplayed() => DriverExtensions.IsDisplayed(HelpLinkLocator, 5);

        /// <summary>
        /// Verifies is next document button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsNextDocumentButtonDisplayed() => DriverExtensions.IsDisplayed(NextDocumentButtonLocator, 5);

        /// <summary>
        /// Verifies is previous document button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsPreviousDocumentButtonDisplayed() => DriverExtensions.IsDisplayed(PreviousDocumentButtonLocator, 5);

        /// <summary>
        /// Verifies is term component button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsNavigationTermComponentDisplayed() => this.IsRightNavigationButtonDisplayed() & this.IsLeftNavigationButtonDisplayed();

        /// <summary>
        /// Verifies is right term navigation top button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsRightNavigationTopButtonDisplayed() => DriverExtensions.IsDisplayed(RightNavigationTopLocator, 5);

        /// <summary>
        /// Verifies is left term navigation button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        private bool IsLeftNavigationButtonDisplayed() => DriverExtensions.IsDisplayed(LeftNavigationLocator, 5);

        /// <summary>
        /// Verifies is right term navigation button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        private bool IsRightNavigationButtonDisplayed() => DriverExtensions.IsDisplayed(RightNavigationLocator, 5);
    }
}
