namespace Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// represents the page that displays a list of multiple documents in CE
    /// </summary>
    public class ReportDocumentListPage : CommonAuthenticatedWestlawNextPage
    {
        private const string DocumentByRowNumberLctMask = "//a[@id='cobalt_result_docket_title{0}']";

        private static readonly By HeaderLocator = By.XPath("//table[contains(@class, 'co_documentReportTable')]/thead//th/strong");

        private static readonly By CaseEvalDocListTitleLocator =
            By.XPath("//h2[@id='co_docHeaderTitleLine' and text()='Case Evaluator Report Document List']");

        private static readonly By FooterBreadCrambsLocator = By.XPath("//div[@id='co_headerReturnToWidget']|//div[@id='co_documentFooterBreadcrumb']");

        private static readonly By NumberPerPageDropdownListLocator = By.Id("resultsPerPage");

        private static readonly By NumDocsByPaginationValueLocator =
            By.XPath("//div[@id='co_paginationListItem_id']/span[@class='co_navigationText']/strong[3]");

        private static readonly By ReturnToReportLocator = By.XPath("//a[@title='Return to report']|//a[text()='Return to Report']");

        private static readonly By SortBySelectElementLocator = By.XPath("//select[@id='co_SortByCourtDocs']");

        private EnumPropertyMapper<ResultItemsPerPage, WebElementInfo> perPageMap;

        private EnumPropertyMapper<DocumentListHeaders, WebElementInfo> documentListHeadersMap;

        /// <summary>
        /// Gets the SortMenuDocumentListDropdown
        /// </summary>
        public IDropdown<SortMenuDocumentList> Dropdown { get; } = new Dropdown<SortMenuDocumentList>(SortBySelectElementLocator);

        /// <summary>
        /// PerPageMap
        /// </summary>
        protected EnumPropertyMapper<ResultItemsPerPage, WebElementInfo> PerPageMap
            => this.perPageMap = this.perPageMap ?? EnumPropertyModelCache.GetMap<ResultItemsPerPage, WebElementInfo>();

        /// <summary>
        /// Gets the FullDocumentListHeaders Map
        /// </summary>
        private EnumPropertyMapper<DocumentListHeaders, WebElementInfo> DocumentListHeadersMap
            =>
                this.documentListHeadersMap =
                    this.documentListHeadersMap ?? EnumPropertyModelCache.GetMap<DocumentListHeaders, WebElementInfo>();

        /// <summary>
        /// click on a document link by absolute row number (ignoring pagination)
        /// </summary>
        /// <param name="rowNumber">Int row number</param>
        /// <returns>The <see cref="CaseEvaluatorDocumentPage"/>.</returns>
        public T ClickDocumentByRowNumber<T>(int rowNumber) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(DocumentByRowNumberLctMask, rowNumber))).CustomClick();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// click on the passed header
        /// </summary>
        /// <param name="header">constant enum of header names</param>
        public void ClickHeader(DocumentListHeaders header) => 
            DriverExtensions.Click(DriverExtensions.GetElements(HeaderLocator).First(e => e.Text == this.DocumentListHeadersMap[header].Text));

        /// <summary>
        /// Return to the CE report page
        /// </summary>
        /// <returns>new reportDocumentListPage</returns>
        public T ClickReturnToReport<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnToReportLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// get the passed column as a list of strings
        /// this overrides the method above, passing in an enum of fulldocument list headers
        /// in this case, it specifies the document list page being viewed is the full document list
        /// and not the standard document list
        /// important:
        /// standard doc list page - 5 columns
        /// full doc list page - 4 columns
        /// </summary>
        /// <param name="header">constant enum of header names</param>
        /// <returns>list of strings</returns>
        public List<string> GetColumnAsStringList(DocumentListHeaders header)
        {
            DriverExtensions.WaitForElement(NumberPerPageDropdownListLocator).SendKeys(this.PerPageMap[ResultItemsPerPage.OneHundred].Text);
            DriverExtensions.WaitForJavaScript();

            int numResults = this.GetNumDocsByPaginationValue();
            if (numResults > 100)
            {
                throw new NotImplementedException(
                    "Getting document lists over 100 records long not implemented for testing");
            }

            return DriverExtensions.GetElements(By.XPath(this.DocumentListHeadersMap[header].LocatorString))
                .Select(el => el.Text)
                .ToList();
        }

        /// <summary>
        /// get the value of total num docs from pagination summary
        /// </summary>
        /// <returns>The number of docs by pagination value</returns>
        public int GetNumDocsByPaginationValue()
            => DriverExtensions.GetImmediateText(DriverExtensions.WaitForElement(NumDocsByPaginationValueLocator)).ConvertCountToInt();

        /// <summary>
        /// is current page case eval doc list
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsCaseEvalDocListPageDisplayed()
            => DriverExtensions.IsDisplayed(CaseEvalDocListTitleLocator, 5) && DriverExtensions.IsDisplayed(FooterBreadCrambsLocator, 5);
    }
}