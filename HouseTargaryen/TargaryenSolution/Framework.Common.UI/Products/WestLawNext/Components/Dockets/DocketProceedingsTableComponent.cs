namespace Framework.Common.UI.Products.WestLawNext.Components.Dockets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestLawNext.Pages.Dockets;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Docket Proceedings Table Component
    /// </summary>
    public class DocketProceedingsTableComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//table[@class='co_docketsTable'][last()]");

        private static readonly By TableEntryLocator = By.CssSelector("tbody tr td:first-child");

        private static readonly By AddToRequestButtonLocator = By.XPath(".//a[contains(text(),'Add to request')]");

        private static readonly By AddedToRequestButtonLocator = By.CssSelector(".co_active");

        private static readonly By PdfViewButtonLocator = By.XPath("*//a[contains(@class,'xslShowMultiPartPdfOnClick') and text()='View']");

        private static readonly By UnseenPdfViewButtonLocator = By.XPath("//td[not(div)]/a[contains(@class,'xslShowMultiPartPdfOnClick') and text()='View']");

        private static readonly By ProceedingDateLocator = By.CssSelector("td:nth-child(2)");

        private static readonly By ProceedingEntryLinkLocator = By.ClassName("co_blobLink");

        private static readonly By SendRunnerToCourtButtonLocator = By.XPath(".//a[text()='Send Runner to Court']");

        private static readonly By GreenCaratLocator = By.CssSelector(".co_docketStatus");

        private static readonly By NextArrowLocator = By.Id("co_bottomNextChunkButton");

        private static readonly By EntryPdfViewButtonLocator = By.XPath(".//a[contains(@class,'xslShowMultiPartPdfOnClick') and text()='View']");


        /// <summary>
        /// Next arrow button
        /// </summary>
        public IButton NextArrowButton => new Button(NextArrowLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Add to request the docket proceeding at the index
        /// </summary>
        /// <param name="indexArray">The indexes of the proceedings to add to request</param>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage AddToRequestDocketByIndex(params int[] indexArray)
        {
            foreach (int i in indexArray)
            {
                DriverExtensions.GetElement(this.GetProceedingRow(i), AddToRequestButtonLocator).ScrollToElement();
                DriverExtensions.GetElement(this.GetProceedingRow(i), AddToRequestButtonLocator).Click();
            }
            return new DocketDocumentPage();
        }

        /// <summary>
        /// Click on Add to request button by 'pdfindex' attribute
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="indexArray">The array of pdf indexes</param>
        /// <returns>The instance of the page</returns>
        public T ClickOnAddToRequestButtonByPdfIndex<T>(int[] indexArray)
            where T : ICreatablePageObject
        {
            this.ClickOnButtonGeneric(AddToRequestButtonLocator, indexArray);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Added to request button by the index
        /// </summary>
        /// <param name="indexArray">The indexes of the proceedings to added to request</param>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage ClickAddedToRequestButtonByIndex(params int[] indexArray)
        {
            foreach (int i in indexArray)
            {
                DriverExtensions.GetElement(this.GetProceedingRow(i), AddedToRequestButtonLocator).ScrollToElement();
                DriverExtensions.GetElement(this.GetProceedingRow(i), AddedToRequestButtonLocator).Click();
            }
            return new DocketDocumentPage();
        }

        /// <summary>
        /// Click on Add to request button by 'pdfindex' attribute
        /// </summary>
        /// <param name="indexArray">The array of pdf indexes</param>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage ClickOnAddedToRequestButtonByPdfIndex(int[] indexArray) =>
            this.ClickOnButtonGeneric(AddedToRequestButtonLocator, indexArray);

        /// <summary>
        /// Is Added To Request Button Displayed By Selected Docket
        /// </summary>
        /// <param name="index"> index of Selected Docket</param>
        /// <returns>true if button is displayed</returns>
        public bool IsAddedToRequestButtonDisplayedByIndex(int index) =>
            DriverExtensions.IsDisplayed(this.GetProceedingRow(index), AddedToRequestButtonLocator);

        /// <summary>
        /// Get Added To Request Buttons Count
        /// </summary>
        /// <returns>number of Added To Request buttons</returns>
        public int GetAddedToRequestButtonsCount() => DriverExtensions.GetElements(AddedToRequestButtonLocator).Count;

        /// <summary>
        /// Get Add To Request Buttons Count
        /// </summary>
        /// <returns>number of Add To Request buttons</returns>
        public int GetAddToRequestButtonsCount() => DriverExtensions.GetElements(AddToRequestButtonLocator).Count;

        /// <summary>
        /// Returns list of PDF indexes
        /// </summary>
        /// <returns>List of indexes</returns>
        public List<int> GetPdfIndexForAvailableToRequestEntries() => DriverExtensions
            .GetElements(AddToRequestButtonLocator).Select(x => int.Parse(x.GetAttribute("pdfindex"))).ToList();

        /// <summary>
        /// Is PDF Judgment View Button Displayed
        /// </summary>
        /// <returns>true if button is displayed</returns>
        public bool IsPdfJudgmentViewButtonDisplayed() => DriverExtensions.IsDisplayed(PdfViewButtonLocator);

        /// <summary>
        /// Opens the multi-PDF dialog by clicking on the PDF View button for the docket proceeding at the index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="index">The index of the proceeding</param>
        /// <returns>The instance of the page</returns>
        public T ClickMultiPdfDialogForProceedingByIndex<T>(int index) where T : ICreatablePageObject
        {
            this.GetProceedingRow(index).ScrollToElement();
            DriverExtensions.WaitForElement(this.GetProceedingRow(index), PdfViewButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Opens the multi PDF dialog for specified entry for the specified proceeding by clicking on the link 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="proceedingIndex"> The index of the proceeding </param>
        /// <param name="entryIndex"> The index of the entry link </param>
        /// <returns>The instance of the page</returns>
        public T ClickEntryPdfForProceedingByIndex<T>(int proceedingIndex, int entryIndex) where T : ICreatablePageObject
        {
            this.GetProceedingRow(proceedingIndex).ScrollToElement();
            DriverExtensions.Click(DriverExtensions.GetElements(this.GetProceedingRow(proceedingIndex), ProceedingEntryLinkLocator)[entryIndex]);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the date text for the docket proceeding at the index
        /// </summary>
        /// <param name="index">The index of the proceeding</param>
        /// <returns> string </returns>
        public string GetDateForProceedingByIndex(int index)
            => DriverExtensions.GetImmediateText(this.GetProceedingRow(index), ProceedingDateLocator);

        /// <summary>
        /// Opens the Send Runner to Court page for the specified proceeding by clicking on the button for that item
        /// </summary>
        /// <param name="index">The index of the proceeding</param>
        /// <returns>SendRunnerToCourtPage</returns>
        public SendRunnerToCourtPage ClickSendRunnerToCourtForProceedingByIndex(int index)
        {
            DriverExtensions.Click(this.GetProceedingRow(index), SendRunnerToCourtButtonLocator);
            return new SendRunnerToCourtPage();
        }

        /// <summary>
        /// Is Green Carat Displayed by proceeding index
        /// </summary>
        /// <param name="index">The index of the proceeding to add to request</param>
        /// <returns>true if displayed</returns>
        public bool IsGreenCaratDisplayedByIndex(int index) =>
            DriverExtensions.IsDisplayed(this.GetProceedingRow(index), GreenCaratLocator);

        /// <summary>
        /// Get Green Carat title by proceeding index
        /// </summary>
        /// <param name="index">The index of the proceeding to add to request</param>
        /// <returns>Green Carat title</returns>
        public string GetGreenCaratTitleByIndex(int index) =>
            DriverExtensions.GetAttribute("title", DriverExtensions.GetElement(this.GetProceedingRow(index), GreenCaratLocator));

        private IWebElement GetProceedingRow(int index)
            => DriverExtensions.GetElements(this.ComponentLocator, TableEntryLocator)[index].GetParentElement();

        private DocketDocumentPage ClickOnButtonGeneric(By elementToClick, int[] indexArray)
        {
            DriverExtensions.GetElements(elementToClick)
                            .Where(x => indexArray.Contains(int.Parse(x.GetAttribute("pdfindex"))))
                            .ToList()
                            .ForEach(DriverExtensions.Click);
            return new DocketDocumentPage();
        }

        /// <summary>
        /// Opens the multi-PDF dialog by clicking on the PDF View button for the docket proceeding at the entry number
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="entryNumber">The entry number of the proceeding</param>
        /// <returns>The instance of the page</returns>
        public T ClickMultiPdfDialogForProceedingByEntryNumber<T>(int entryNumber) where T : ICreatablePageObject
        {
            this.GetProceedingRowByEntryNumber(entryNumber);
            DriverExtensions.WaitForElement(this.GetProceedingRowByEntryNumber(entryNumber), EntryPdfViewButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Add to request the docket proceeding at the index
        /// </summary>
        /// <param name="entryArray">The entry numbers of the proceedings to add to request</param>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage AddToRequestDocketByEntryNumber(params int[] entryArray)
        {
            foreach (int i in entryArray)
            {
                DriverExtensions.GetElement(this.GetProceedingRowByEntryNumber(i), AddToRequestButtonLocator).ScrollToElement();
                DriverExtensions.GetElement(this.GetProceedingRowByEntryNumber(i), AddToRequestButtonLocator).Click();
            }
            return new DocketDocumentPage();
        }

        /// <summary>
        /// Is Added To Request Button Displayed By Selected Docket
        /// </summary>
        /// <param name="entryNumber"> entryNumber of Selected Docket</param>
        /// <returns>true if button is displayed</returns>
        public bool IsAddedToRequestButtonDisplayedByEntryNumber(int entryNumber) =>
            DriverExtensions.IsDisplayed(this.GetProceedingRowByEntryNumber(entryNumber), AddedToRequestButtonLocator);

        private IWebElement GetProceedingRowByEntryNumber(int entryNumber) =>
    //  DriverExtensions.GetElement(this.ComponentLocator, By.XPath("./tbody/tr/td[1][text()="+entryNumber+"]")).GetParentElement();
    DriverExtensions.GetElement(this.ComponentLocator, By.XPath("./tbody/tr/td[(position()=1 or position()=2)][text()=" + entryNumber + "]")).GetParentElement();
    }
}