namespace Framework.Common.UI.Products.WestLawNext.Pages.Dockets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Searches;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The Send Runner to Court page for dockets
    /// </summary>
    public class SendRunnerToCourtPage : CommonDocketsPage
    {
        private static readonly By AdditionalDocumentInformationTextboxLocator =
            By.Id("co_documentOrderingAdditionalInformation");

        private static readonly By NextLinkLocator = By.Id("co_documentOrderingNext");

        private static readonly By DocumentOrderingStepNumberLocator = By.Id("co_documentOrderingStepNumber");

        private static readonly By SubmitButtonLocator = By.LinkText("Submit");

        private static readonly By ReturnToDocketLinkLocator = By.LinkText("Return to Docket");

        private static readonly By PricingMessagingLabelLocator =
            By.XPath("//a[contains(@id,'co_courtExpressHelp')]/parent::p");

        private static readonly EnumPropertyMapper<ContactAndBillingInformation, WebElementInfo> ContactAndBillingInformationMap =
                EnumPropertyModelCache.GetMap<ContactAndBillingInformation, WebElementInfo>();

        private static readonly EnumPropertyMapper<DeliveryMethodOptions, WebElementInfo> DeliveryMethodOptionsMap =
            EnumPropertyModelCache.GetMap<DeliveryMethodOptions, WebElementInfo>();

        /// <summary>
        /// Pricing Message Label
        /// </summary>
        public ILabel PricingMessagingLabel => new Label(PricingMessagingLabelLocator);

        /// <summary>
        /// Enters text into the Additional Document information box
        /// </summary>
        /// <param name="text">The text to enter</param>
        public void EnterAdditionalDocumentInformation(string text) =>
            DriverExtensions.SetTextField(text, AdditionalDocumentInformationTextboxLocator);

        /// <summary>
        /// Checks if the Additional Document Information text area is displayed or not
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsAdditionalDocumentInformationTextboxDisplayed() =>
            DriverExtensions.IsDisplayed(AdditionalDocumentInformationTextboxLocator);

        /// <summary>
        /// Determines if the Send Runner to the Court page is open
        /// </summary>
        /// <returns> Document Ordering Step Number Text </returns>
        public string GetDocumentOrderingStepNumberText() =>
            DriverExtensions.GetText(DocumentOrderingStepNumberLocator);

        /// <summary>
        /// Click the Next link
        /// </summary>
        /// <returns>The Send Runner to Court Form page</returns>
        public SendRunnerToCourtPage ClickNextLink()
        {
            DriverExtensions.WaitForElement(NextLinkLocator).Click();
            return this;
        }

        /// <summary>
        /// The click submit.
        /// </summary>
        /// <typeparam name="T"> PageObject</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickSubmit<T>()
            where T : ICreatablePageObject
        {
            this.ScrollPageToBottom();
            DriverExtensions.GetElement(SubmitButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Return To Docket
        /// </summary>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage ClickReturnToDocket()
        {
            DriverExtensions.GetElement(ReturnToDocketLinkLocator).Click();
            return new DocketDocumentPage();
        }

        /// <summary>
        /// Set Exclude Document Type Checkbox
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <param name="state"> The state. </param>
        /// <returns> this component  </returns>
        public SendRunnerToCourtPage SetCheckbox(DeliveryMethodOptions option, bool state)
        {
            DriverExtensions.SetCheckbox(By.Id(DeliveryMethodOptionsMap[option].Id), state);
            return this;
        }

        /// <summary>
        /// Get Delivery Options States
        /// </summary>
        /// <param name="options"> The options. </param>
        /// <returns> option with its state (true if checked) </returns>
        public Dictionary<string, bool> GetDeliveryOptionsStates(params DeliveryMethodOptions[] options) =>
             options.ToDictionary(
                option => DeliveryMethodOptionsMap[option].Text,
                option => Convert.ToBoolean(
                    DriverExtensions.GetElement(By.Id(DeliveryMethodOptionsMap[option].Id)).GetAttribute("checked")));

        /// <summary>
        /// Enter Text In Field
        /// </summary>
        /// <param name="field"> field </param>
        /// <param name="query"> query </param>
        /// <returns> page </returns>
        public SendRunnerToCourtPage EnterTextInField(ContactAndBillingInformation field, string query)
        {
            IWebElement fieldElement = DriverExtensions.GetElement(By.Id(ContactAndBillingInformationMap[field].Id));
            DriverExtensions.Click(fieldElement);
            fieldElement.Clear();
            fieldElement.SendKeysSlow(query);
            return this;
        }

        /// <summary>
        /// Get Text In Field
        /// </summary>
        /// <param name="fields"> fields </param>
        /// <returns> dict with results </returns>
        public Dictionary<string, string> GetTextInField(params ContactAndBillingInformation[] fields) =>
            fields.ToDictionary(
                f => ContactAndBillingInformationMap[f].Text,
                f => DriverExtensions.GetText(By.Id(ContactAndBillingInformationMap[f].Id)));
    }
}