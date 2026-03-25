namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.Enum;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Business law center document page
    /// </summary>
    public class BusinessLawCenterDocumentPage : CommonDocumentPage
    {
        private const string ParagraphLctMask = "//*[@id='co_document_0']//div[{0}]";
        private static readonly By TableBodyLocator = By.XPath("./tbody");

        /// <summary>
        /// TocLeftPanelComponent
        /// </summary>
        public TableOfContentsLeftPanelComponent TocLeftPanelComponent => new TableOfContentsLeftPanelComponent(); 

        /// <summary>
        /// Select Multiple Paragraphs By Document Section
        /// </summary>
        /// <param name="docSection"> Document section </param>
        /// <param name="firstIndex"> First index of element to select </param>
        /// <param name="lastIndex"> Last index of element to select </param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectMultipleParagraphsByDocumentSection(DocumentSection docSection, int firstIndex, int lastIndex)
        {
            IReadOnlyCollection<IWebElement> sectionElements =
                DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[docSection].LocatorString));
            return this.SelectParagraphsElements(sectionElements.ElementAt(firstIndex), sectionElements.ElementAt(lastIndex));
        }

        /// <summary>
        /// Select Tables
        /// </summary>
        /// <param name="docSection"> Doc section </param>
        /// <param name="firstIndex"> First index of element to select </param>
        /// <param name="lastIndex"> Last index of element to select </param>
        /// <returns> EdgeHighlightMenuDialog </returns>
        public HighlightMenuDialog SelectTables(int firstIndex, int lastIndex, DocumentSection docSection = DocumentSection.Table)
        {
            var sectionElements = DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[docSection].LocatorString), TableBodyLocator);
            return this.SelectParagraphsElements(sectionElements.ElementAt(firstIndex), sectionElements.ElementAt(lastIndex));
        }

        /// <summary>
        /// Checks if document section is displayed by section id.
        /// </summary>
        /// <param name="id">Section id</param>
        /// <returns>Boolean value</returns>
        public bool IsDocumentSectionDisplayedById(string id) => DriverExtensions.IsDisplayed(By.Id(id));

        private HighlightMenuDialog SelectParagraphsElements(IWebElement firstElement, IWebElement lastElement)
        {
            firstElement.TriggerMouseEventByPoint(Pointer.PointerDown);
            DriverExtensions.HighlightMultipleNodes(firstElement, lastElement);
            lastElement.ScrollToElementCenter();
            lastElement.TriggerMouseEventByPoint(Pointer.PointerUp);
            DriverExtensions.WaitForJavaScript();
            return new HighlightMenuDialog();
        }
    }
}
