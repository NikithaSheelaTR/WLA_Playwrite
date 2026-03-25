namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Dropdowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents dialog appeared after Edit button had been clicked.
    /// </summary>
    public class EditBillingInformationDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.Id("wa_editSession_cancel");
        
        private static readonly By ClientIdFieldLocator =
            By.XPath("//div[@id='co_editSessionView_ClientMattersComboBox']/.//input[contains(@class,'co_fetchableComboBox')]");

        private static readonly By ErrorMessageLocator =
            By.XPath("//div[@class='co_infoBox_inner']/div[contains(text(),'The Client ID field cannot be empty.')]");

        private static readonly By ReasonDescriptionLocator =
            By.XPath("//div[@id='co_researchDescriptionTextAreaContainer']/.//textarea");

        private static readonly By SubmitButtonLocator = By.Id("wa_editSession_submit");
        private static readonly By ChargeableDropdownLocator = By.XPath("//select[@id='wa_editSession_nonChargeableDropdown']");

        /// <summary>
        /// Chargeable dropdown
        /// </summary>
        public IDropdown<AnalyticsChargeableOptions> ChargeableDropdown { get; } = new Dropdown<AnalyticsChargeableOptions>(ChargeableDropdownLocator);

        /// <summary>
        /// Click on cancel button
        /// </summary>
        /// <returns><see cref="BillingInvestigationResultsPage"/></returns>
        public BillingInvestigationResultsPage ClickCancelButton()
            => this.ClickElement<BillingInvestigationResultsPage>(CancelButtonLocator);

        /// <summary>
        /// Click on submit button
        /// </summary>
        /// <returns><see cref="EditBillingInformationSubmittedChangesDialog"/></returns>
        public EditBillingInformationSubmittedChangesDialog ClickSubmitButton()
            => this.ClickElement<EditBillingInformationSubmittedChangesDialog>(SubmitButtonLocator);

        /// <summary>
        /// Checks whether error message appears
        /// </summary>
        /// <returns>True, if message appeared.</returns>
        public bool HasError() => DriverExtensions.IsElementPresent(ErrorMessageLocator, 2000)
                                  && DriverExtensions.IsDisplayed(ErrorMessageLocator);

        /// <summary>
        /// Clears Client Id field
        /// </summary>
        /// <param name="id"> Client id to set</param>
        public void SetClientId(string id)
        {
            DriverExtensions.SetTextField(id, ClientIdFieldLocator);
            DriverExtensions.WaitForTextInElement(id, ClientIdFieldLocator);
        }

        /// <summary>
        /// Sets corresponding text into text area
        /// </summary>
        /// <param name="value"> text to be filled </param>
        public void SetResearchDescription(string value) => DriverExtensions.SetTextField(value, ReasonDescriptionLocator);
    }
}