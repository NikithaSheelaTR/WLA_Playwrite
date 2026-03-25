namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Models;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Order Document page
    /// </summary>
    public class OrderDocumentFormPage : BaseModuleRegressionPage
    {
        private static readonly By AddressLine1Locator = By.Id("Address1");

        private static readonly By AddressLine12Locator = By.Id("Address12");

        private static readonly By AddressLine2Locator = By.Id("Address2");

        private static readonly By AddressLine22Locator = By.Id("Address22");

        private static readonly By BillingAddressCheckboxLocator = By.XPath("//input[@value='BillingAddress']");

        private static readonly By BillingAddressMessageLocator =
            By.XPath("id('BillingAddress')//div[@class='co_infoBox_message']");

        private static readonly By CaseCaptionLocator = By.Id("CaseCaption");

        private static readonly By CaseNumberLocator = By.Id("CaseNumber");

        private static readonly By CityLocator = By.Id("City");

        private static readonly By City2Locator = By.Id("City2");

        private static readonly By ClearAllLinkLocator = By.XPath("id('CourtSection')");

        private static readonly By ClientIdLocator = By.Id("ClientId");

        private static readonly By CommentsLocator = By.Id("Comments");

        private static readonly By CompanyFirmLocator = By.Id("Company");

        private static readonly By CompanyFirm2Locator = By.Id("Company2");

        private static readonly By ContactB4FullfillmentCheckboxLocator = By.Name("ContactSubmitterWithTimeCostEstimates");

        private static readonly By CountryLocator = By.Id("Country");

        private static readonly By Country2Locator = By.Id("Country2");

        private static readonly By CourtSelectionLocator = By.Id("co_documentOrderingInfoCourtSelectionOptionOne");

        private static readonly By DeliveryEmailCheckboxLocator = By.XPath("id('DeliveryMedium')//input[@name='Email']");

        private static readonly By DeliveryFaxCheckboxLocator = By.XPath("id('DeliveryMedium')//input[@name='Fax']");

        private static readonly By DeliveryFirstClassMailCheckboxLocator =
            By.XPath("id('DeliveryMedium')//input[@name='FirstClassMail']");

        private static readonly By DeliveryOvernightCarrierCheckboxLocator =
            By.XPath("id('DeliveryMedium')//input[@name='OvernightCarrier']");

        private static readonly By DifferentAddressCheckboxLocator = By.XPath("//input[@value='DifferentAddress']");

        private static readonly By DocumentInformationMessageLocator =
            By.XPath("id('DocumentInformation')//div[@class='co_infoBox_message']");

        private static readonly By DocumentInformationMessage2Locator =
            By.XPath("id('DifferentAddress')//div[@class='co_infoBox_message']");

        private static readonly By EmailAddressLocator = By.Id("EmailAddress");

        private static readonly By EmailAddress2Locator = By.Id("EmailAddress2");

        private static readonly By FaxNumberLocator = By.Id("FaxNumber");

        private static readonly By FaxNumber2Locator = By.Id("FaxNumber2");

        private static readonly By FullNameLocator = By.Id("FullName");

        private static readonly By FullName2Locator = By.Id("FullName2");

        private static readonly By IncludeAllExhibitsCheckboxLocator =
            By.XPath(".//*[@id='co_documentOrderingOrderInformation']/div[4]/label/input");

        private static readonly By ParticipantInformationLocator = By.Id("ParticipantInformation");

        private static readonly By PhoneNumberLocator = By.Id("PhoneNumber");

        private static readonly By PhoneNumber2Locator = By.Id("PhoneNumber2");

        private static readonly By PostalcodeLocator = By.Id("PostalCode");

        private static readonly By Postalcode2Locator = By.Id("PostalCode2");

        private static readonly By RequestedDocumentsLocator = By.Id("RequestedDocuments");

        private static readonly By StateProvinceLocator = By.Id("State");

        private static readonly By StateProvince2Locator = By.Id("State2");

        private static readonly By SubmitButtonLocator = By.Id("Submit");

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDocumentFormPage"/> class. 
        /// Construct Order Document page
        /// </summary>
        public OrderDocumentFormPage()
        {
            DriverExtensions.WaitForElement(SubmitButtonLocator);
        }

        /// <summary>
        /// Click the clear all link
        /// </summary>
        public void ClickClearAll()
        {
            DriverExtensions.ScrollTo(ClearAllLinkLocator);
            DriverExtensions.WaitForElement(ClearAllLinkLocator).Click();
        }

        /// <summary>
        /// The click submit.
        /// </summary>
        /// <typeparam name="T"> PageObject</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickSubmit<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(SubmitButtonLocator));
            DriverExtensions.WaitForJavaScript(9999);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Fill 'Order Document Form' from model object
        /// </summary>
        /// <param name="formModel"><see cref="FullOrderDocumentFormModel"/> form model object</param>
        public void FillFormFromModel(FullOrderDocumentFormModel formModel)
        {
            if (formModel != null)
            {
                if (!string.IsNullOrEmpty(formModel.Court) && formModel.Court != "Select")
                {
                    DriverExtensions.SelectElementInListByText(CourtSelectionLocator, formModel.Court);
                }

                DriverExtensions.WaitForElementDisplayed(ParticipantInformationLocator)
                                .SetTextField(formModel.ParticipantInformation ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(RequestedDocumentsLocator)
                                .SetTextField(formModel.RequestedDocuments ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(IncludeAllExhibitsCheckboxLocator)
                                .SetCheckbox(formModel.IncludeAllExhibits);
                DriverExtensions.WaitForElementDisplayed(CaseCaptionLocator)
                                .SetTextField(formModel.CaseCaption ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(CaseNumberLocator)
                                .SetTextField(formModel.CaseNumber ?? string.Empty);

                // for clientID we use opposite behavoior - we get value from DOM-element and then put this value to the according form model object property
                formModel.ClientID = DriverExtensions.WaitForElementDisplayed(ClientIdLocator).GetAttribute("value");

                DriverExtensions.WaitForElementDisplayed(CommentsLocator)
                                .SetTextField(formModel.Comments ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(ContactB4FullfillmentCheckboxLocator)
                                .SetCheckbox(formModel.ContactMe);
                DriverExtensions.WaitForElementDisplayed(FullNameLocator)
                                .SetTextField(formModel.FullName ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(EmailAddressLocator)
                                .SetTextField(formModel.EmailAddress ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(PhoneNumberLocator)
                                .SetTextField(formModel.PhoneNumber ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(FaxNumberLocator)
                                .SetTextField(formModel.FaxNumber ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(CompanyFirmLocator)
                                .SetTextField(formModel.CompanyFirm ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(AddressLine1Locator)
                                .SetTextField(formModel.AddressLine1 ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(AddressLine2Locator)
                                .SetTextField(formModel.AddressLine2 ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(CityLocator).SetTextField(formModel.City ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(StateProvinceLocator)
                                .SetTextField(formModel.StateProvince ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(PostalcodeLocator)
                                .SetTextField(formModel.Postalcode ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(CountryLocator).SetTextField(formModel.Country ?? string.Empty);
                DriverExtensions.WaitForElementDisplayed(DeliveryEmailCheckboxLocator)
                                .SetCheckbox(formModel.DeliveryEmailCheckbox);
                DriverExtensions.WaitForElementDisplayed(DeliveryOvernightCarrierCheckboxLocator)
                                .SetCheckbox(formModel.DeliveryOvernightCarrierCheckbox);
                DriverExtensions.WaitForElementDisplayed(DeliveryFaxCheckboxLocator)
                                .SetCheckbox(formModel.DeliveryFaxCheckbox);
                DriverExtensions.WaitForElementDisplayed(DeliveryFirstClassMailCheckboxLocator)
                                .SetCheckbox(formModel.DeliveryFirstClassMailCheckbox);
                DriverExtensions.WaitForElementDisplayed(BillingAddressCheckboxLocator)
                                .SetCheckbox(formModel.BillingAddressRadio);
                DriverExtensions.WaitForElementDisplayed(DifferentAddressCheckboxLocator)
                                .SetCheckbox(formModel.DifferentAddressRadio);

                if (formModel.DifferentAddressRadio
                    && DriverExtensions.WaitForElementDisplayed(DifferentAddressCheckboxLocator).Selected)
                {
                    DriverExtensions.WaitForElementDisplayed(FullName2Locator)
                                    .SetTextField(formModel.FullName2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(EmailAddress2Locator)
                                    .SetTextField(formModel.EmailAddress2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(PhoneNumber2Locator)
                                    .SetTextField(formModel.PhoneNumber2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(FaxNumber2Locator)
                                    .SetTextField(formModel.FaxNumber2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(CompanyFirm2Locator)
                                    .SetTextField(formModel.CompanyFirm2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(AddressLine12Locator)
                                    .SetTextField(formModel.AddressLine12 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(AddressLine22Locator)
                                    .SetTextField(formModel.AddressLine22 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(City2Locator).SetTextField(formModel.City2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(StateProvince2Locator).SetTextField(
                        formModel.StateProvince2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(Postalcode2Locator)
                                    .SetTextField(formModel.Postalcode2 ?? string.Empty);
                    DriverExtensions.WaitForElementDisplayed(Country2Locator)
                                    .SetTextField(formModel.Country2 ?? string.Empty);
                }
            }
        }

        /// <summary>
        /// Creates the <see cref="FullOrderDocumentFormModel"/> object from html form
        /// </summary>
        /// <returns><see cref="FullOrderDocumentFormModel"/> object. Returns 'null' if form is empty</returns>
        public FullOrderDocumentFormModel GetFormModelFromPage()
        {
            var resultFormModel = new FullOrderDocumentFormModel();

            DriverExtensions.WaitForElementDisplayed(CourtSelectionLocator);
            resultFormModel.Court = DriverExtensions.GetSelectElementSelectedText(CourtSelectionLocator);
            resultFormModel.ParticipantInformation =
                DriverExtensions.WaitForElementDisplayed(ParticipantInformationLocator).GetAttribute("value");
            resultFormModel.RequestedDocuments =
                DriverExtensions.WaitForElementDisplayed(RequestedDocumentsLocator).GetAttribute("value");
            resultFormModel.IncludeAllExhibits =
                DriverExtensions.WaitForElementDisplayed(IncludeAllExhibitsCheckboxLocator).Selected;
            resultFormModel.CaseCaption = DriverExtensions.WaitForElementDisplayed(CaseCaptionLocator).GetAttribute("value");
            resultFormModel.CaseNumber = DriverExtensions.WaitForElementDisplayed(CaseNumberLocator).GetAttribute("value");
            resultFormModel.ClientID = DriverExtensions.WaitForElementDisplayed(ClientIdLocator).GetAttribute("value");
            resultFormModel.Comments = DriverExtensions.WaitForElementDisplayed(CommentsLocator).GetAttribute("value");

            resultFormModel.ContactMe = DriverExtensions.IsCheckboxSelected(ContactB4FullfillmentCheckboxLocator);
            resultFormModel.FullName = DriverExtensions.WaitForElementDisplayed(FullNameLocator).GetAttribute("value");
            resultFormModel.EmailAddress = DriverExtensions.WaitForElementDisplayed(EmailAddressLocator).GetAttribute("value");
            resultFormModel.PhoneNumber = DriverExtensions.WaitForElementDisplayed(PhoneNumberLocator).GetAttribute("value");
            resultFormModel.FaxNumber = DriverExtensions.WaitForElementDisplayed(FaxNumberLocator).GetAttribute("value");
            resultFormModel.CompanyFirm = DriverExtensions.WaitForElementDisplayed(CompanyFirmLocator).GetAttribute("value");
            resultFormModel.AddressLine1 = DriverExtensions.WaitForElementDisplayed(AddressLine1Locator).GetAttribute("value");
            resultFormModel.AddressLine2 = DriverExtensions.WaitForElementDisplayed(AddressLine2Locator).GetAttribute("value");
            resultFormModel.City = DriverExtensions.WaitForElementDisplayed(CityLocator).GetAttribute("value");
            resultFormModel.StateProvince = DriverExtensions.WaitForElementDisplayed(StateProvinceLocator)
                                                            .GetAttribute("value");
            resultFormModel.Postalcode = DriverExtensions.WaitForElementDisplayed(PostalcodeLocator).GetAttribute("value");
            resultFormModel.Country = DriverExtensions.WaitForElementDisplayed(CountryLocator).GetAttribute("value");
            resultFormModel.DeliveryEmailCheckbox =
                DriverExtensions.WaitForElementDisplayed(DeliveryEmailCheckboxLocator).Selected;
            resultFormModel.DeliveryOvernightCarrierCheckbox =
                DriverExtensions.WaitForElementDisplayed(DeliveryOvernightCarrierCheckboxLocator).Selected;
            resultFormModel.DeliveryFaxCheckbox = DriverExtensions.WaitForElementDisplayed(DeliveryFaxCheckboxLocator).Selected;
            resultFormModel.DeliveryFirstClassMailCheckbox =
                DriverExtensions.WaitForElementDisplayed(DeliveryFirstClassMailCheckboxLocator).Selected;
            resultFormModel.BillingAddressRadio =
                DriverExtensions.WaitForElementDisplayed(BillingAddressCheckboxLocator).Selected;
            resultFormModel.DifferentAddressRadio =
                DriverExtensions.WaitForElementDisplayed(DifferentAddressCheckboxLocator).Selected;

            if (DriverExtensions.WaitForElementDisplayed(DifferentAddressCheckboxLocator).Selected
                && resultFormModel.DifferentAddressRadio)
            {
                resultFormModel.FullName2 = DriverExtensions.WaitForElementDisplayed(FullName2Locator).GetAttribute("value");
                resultFormModel.EmailAddress2 =
                    DriverExtensions.WaitForElementDisplayed(EmailAddress2Locator).GetAttribute("value");
                resultFormModel.PhoneNumber2 =
                    DriverExtensions.WaitForElementDisplayed(PhoneNumber2Locator).GetAttribute("value");
                resultFormModel.FaxNumber2 = DriverExtensions.WaitForElementDisplayed(FaxNumber2Locator).GetAttribute("value");
                resultFormModel.CompanyFirm2 =
                    DriverExtensions.WaitForElementDisplayed(CompanyFirm2Locator).GetAttribute("value");
                resultFormModel.AddressLine12 =
                    DriverExtensions.WaitForElementDisplayed(AddressLine12Locator).GetAttribute("value");
                resultFormModel.AddressLine22 =
                    DriverExtensions.WaitForElementDisplayed(AddressLine22Locator).GetAttribute("value");
                resultFormModel.City2 = DriverExtensions.WaitForElementDisplayed(City2Locator).GetAttribute("value");
                resultFormModel.StateProvince2 =
                    DriverExtensions.WaitForElementDisplayed(StateProvince2Locator).GetAttribute("value");
                resultFormModel.Postalcode2 = DriverExtensions.WaitForElementDisplayed(Postalcode2Locator)
                                                              .GetAttribute("value");
                resultFormModel.Country2 = DriverExtensions.WaitForElementDisplayed(Country2Locator).GetAttribute("value");
            }

            return resultFormModel;
        }

        /// <summary>
        /// Return true if required field is not filled
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRequiredFieldMessageDisplayed()
        {
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.IsDisplayed(DocumentInformationMessageLocator)
                   || DriverExtensions.IsDisplayed(BillingAddressMessageLocator)
                   || DriverExtensions.IsDisplayed(DocumentInformationMessage2Locator);
        }

        // This section is mandatory one, is always availableon the form

        // This section is optional one, is available only if 'Different Address' radio button is selected
    }
}