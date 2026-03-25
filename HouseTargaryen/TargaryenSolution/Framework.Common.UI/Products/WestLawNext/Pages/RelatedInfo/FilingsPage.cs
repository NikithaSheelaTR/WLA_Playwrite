namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FilingsPage
    /// </summary>
    public class FilingsPage : TabPage
    {
        private const string RelatedInfoGridLocator =
            "//div[@id='coid_relatedinfo_detailsContainer']/table[contains(@class,'co_detailsTable')]";

        private static readonly By HighlightedTermLocator = By.XPath("//span[@class='co_searchTerm co_locateTerm']");

        private static readonly By RelatedInfoGridElementLocator = By.Id("coid_relatedinfo_detailsContainer");

        private static readonly By PdfIconLocator = By.ClassName("co_relatedInfo_pdfIcon");

        /// <summary>
        /// Grid in the body of the page
        /// </summary>
        public ReferenceGridComponent RelatedInfoGrid { get; set; } = new ReferenceGridComponent(RelatedInfoGridLocator);

        /// <summary>
        /// RelatedInfoGridElement
        /// </summary>
        private IWebElement RelatedInfoGridElement => DriverExtensions.WaitForElement(RelatedInfoGridElementLocator);

        /// <summary>
        /// Clicks on the highlighted text link element
        /// </summary>
        /// <typeparam name="T"> T page</typeparam>
        /// <param name="index"> index </param>
        /// <returns> DocumentPage instance </returns>
        public T ClickHighlightedTermElement<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.GetAllHighlightedElements()[index]);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is RelatedInfoGridElement Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRelatedInfoGridElementDisplayed() => this.RelatedInfoGridElement.IsDisplayed();

        /// <summary>
        /// Get all the highlighted text.
        /// </summary>
        /// <returns>List of highlighted texts</returns>
        public List<string> GetAllHighlightedTerms() => this.GetAllHighlightedElements().Select(e => e.Text).ToList();

        /// <summary>
        /// Click PDF icon
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickPdfIcon<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PdfIconLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get all the highlighted elements.
        /// </summary>
        /// <returns>List of highlighted elements</returns>
        private List<IWebElement> GetAllHighlightedElements()
            => DriverExtensions.GetElements(this.RelatedInfoGridElement, HighlightedTermLocator).ToList();
    }
}