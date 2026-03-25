namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Find By Case Number page
    /// </summary>
    public class FindByCaseNumberPage : BaseModuleRegressionPage
    {
        private static readonly By CancelLinkLocator = By.Id("Cancel");

        private static readonly By CaseNumberErrorBoxLocator = By.Id("typeCaseNumberMessageBox");

        private static readonly By CaseNumberInputTextBoxLocator = By.Id("CaseNumber");

        private static readonly By CloseCaseNumberErrorBoxLocator = By.XPath("id('typeCaseNumberMessageBox')//a");

        private static readonly By CourtDropdownLocator = By.Id("co_documentOrderingInfoCourtSelectionOptionEight");

        private static readonly By CourtSelectionLocator = By.Id("co_documentOrderingInfoCourtSelectionOptionOne");

        private static readonly By FederalRadioButtonLocator = By.Id("FederalCourtSelection");

        private static readonly By FindBtnLocator = By.Id("Find");

        private static readonly By StateRadioBtnLocator = By.Id("StateCourtSelection");

        /// <summary>
        /// Initializes a new instance of the <see cref="FindByCaseNumberPage"/> class. 
        /// Constructs Find By Case Number page
        /// </summary>
        public FindByCaseNumberPage()
        {
            DriverExtensions.WaitForElement(FindBtnLocator);
        }

        /// <summary>
        /// Enter the case number in the case number text box.
        /// </summary>
        /// <param name="caseNumber">The case Number.</param>
        public void AddCaseNumber(string caseNumber)
            => DriverExtensions.WaitForElement(CaseNumberInputTextBoxLocator).SendKeysSlow(caseNumber);

        /// <summary>
        /// Clears the text in the Case Number input box
        /// </summary>
        public void ClearCaseNumber() => DriverExtensions.WaitForElement(CaseNumberInputTextBoxLocator).Clear();

        /// <summary>
        /// Click the Cancel button
        /// </summary>
        /// <returns>The <see cref="DocketsCategoryPage"/>.</returns>
        public DocketsCategoryPage ClickCancel()
        {
            DriverExtensions.WaitForElement(CancelLinkLocator).Click();
            return new DocketsCategoryPage();
        }

        /// <summary>
        /// Select the federal radio button
        /// </summary>
        public void ClickFederalRadioButton()
        {
            DriverExtensions.WaitForElement(FederalRadioButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The click find.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of page T</returns>
        public T ClickFind<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(FindBtnLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select the state radio button
        /// </summary>
        public void ClickStateRadioButton()
        {
            DriverExtensions.WaitForElement(StateRadioBtnLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Close the Case Number error box
        /// </summary>
        public void CloseCaseNumberErrorBox() => DriverExtensions.WaitForElement(CloseCaseNumberErrorBoxLocator).Click();

        /// <summary>
        /// Returns true if the case number error box is displayed
        /// </summary>
        /// <returns>True if Case Number Error Box is displayed, otherwise - false</returns>
        public bool IsCaseNumberErrorBoxDisplayed()
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.IsDisplayed(CaseNumberErrorBoxLocator, 15);
        }

        /// <summary>
        /// Select Court
        /// </summary>
        /// <param name="courtName">Court name</param>
        public void SelectCourt(string courtName)
        {
            DriverExtensions.WaitForElement(CourtDropdownLocator).WaitForElementDisplayed();
            DriverExtensions.WaitForElement(CourtDropdownLocator).Click();
            DriverExtensions.SelectElementInListByText(CourtDropdownLocator, courtName);
        }

        /// <summary>
        /// Select the court 
        /// </summary>
        /// <param name="court">The court.</param>
        public void SelectCourtOption(string court)
        {
            DriverExtensions.WaitForElement(CourtSelectionLocator).Click();
            DriverExtensions.SelectElementInListByText(CourtSelectionLocator, court);
        }
    }
}