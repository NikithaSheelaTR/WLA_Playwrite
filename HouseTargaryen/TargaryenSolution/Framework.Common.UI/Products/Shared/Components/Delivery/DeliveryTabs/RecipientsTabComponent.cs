namespace Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Recipients Tab Component
    /// </summary>
    public class RecipientsTabComponent : BaseTabComponent
    {
        private static readonly By EmailNoteTextboxLocator = By.Id("co_delivery_note");

        private static readonly By EmailSubjectTextboxLocator = By.Id("co_delivery_subject");

        private static readonly By ToTextBoxLocator = By.Id("co_delivery_emailAddress");

        private static readonly By ContainerLocator = By.Id("co_deliveryOptionsTabPanel1");

        private static readonly By DeliveryAsDropDownLocator = By.XPath("//select[@id= 'co_delivery_fileContainer' or @name = 'co_delivery_fileContainer']");

        private static readonly By DeliveryFormatDropdownLocator
            = By.XPath("//div[not(contains(@style, 'display: none'))]/select[contains(@id, 'co_delivery_format')]");

        private static readonly EnumPropertyMapper<RecipientsTabOptions, WebElementInfo> RecipientsTabOptionsMap =
            EnumPropertyModelCache.GetMap<RecipientsTabOptions, WebElementInfo>();

        /// <summary>
        /// What To Deliver Component
        /// </summary>
        public WhatToDeliverComponent WhatToDeliver { get; } = new WhatToDeliverComponent();

        /// <summary>
        /// Deliver as dropdown
        /// </summary>
        public IDropdown<string> DeliveryAsDropdown { get; } = new Dropdown(DeliveryAsDropDownLocator);

        /// <summary>
        /// Gets the format dropdown.
        /// </summary>
        public IDropdown<DeliveryFormat> FormatDropdown { get; } = new Dropdown<DeliveryFormat>(DeliveryFormatDropdownLocator);

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Recipients";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clear the subject textbox
        /// </summary>
        public void ClearEmailSubjectText() => DriverExtensions.GetElement(EmailSubjectTextboxLocator).Clear();

        /// <summary>
        /// Get Email Subject text
        /// </summary>
        /// <returns> Subject text</returns>
        public string GetEmailSubjectText() => DriverExtensions.GetElement(EmailSubjectTextboxLocator).GetAttribute("value");

        /// <summary>
        /// Get note text
        /// </summary>
        /// <returns> Text from the note </returns>
        public string GetNoteTextboxText() => DriverExtensions.GetText(EmailNoteTextboxLocator);

        /// <summary>
        /// Verify Email Note textbox is present
        /// </summary>
        /// <returns> True if Email Note textbox is displayed, false otherwise</returns>
        public bool IsEmailNoteTextboxDisplayed() => DriverExtensions.IsDisplayed(EmailNoteTextboxLocator);

        /// <summary>
        /// Verify 'email subject' textbox is displayed
        /// </summary>
        /// <returns> True if 'email subject' textbox is displayed, false otherwise </returns>
        public bool IsEmailSubjectTextboxDisplayed() => DriverExtensions.IsDisplayed(EmailSubjectTextboxLocator);

        /// <summary>
        /// Verify 'email to' textbox is displayed
        /// </summary>
        /// <returns> True if 'email to' textbox is displayed, false otherwise </returns>
        public bool IsEmailToTextboxDisplayed() => DriverExtensions.IsDisplayed(ToTextBoxLocator);

        /// <summary>
        /// Verify specified option on the Recipients tab is displayed
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if tab option is displayed, false otherwise </returns>
        public bool IsTabOptionDisplayed(RecipientsTabOptions expectedTabOption)
            => DriverExtensions.IsDisplayed(By.Id(RecipientsTabOptionsMap[expectedTabOption].Id));

        /// <summary>
        /// Verify specified option on the Recipients tab is presented
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if tab option is displayed, false otherwise </returns>
        public bool IsTabOptionPresent(RecipientsTabOptions expectedTabOption)
            => DriverExtensions.IsElementPresent(By.Id(RecipientsTabOptionsMap[expectedTabOption].Id));


        /// <summary>
        /// Selects the preferences for emailing the documents on the delivery widget
        /// </summary>
        /// <param name="emailId">email id of the user to email the documents</param>
        /// <param name="deliveryFormat">format to delivery ex-PDF, DOC etc</param>
        /// <param name="whatToDeliver">List of items to delivery format</param>
        public void SetDeliveryPreferences(string emailId, DeliveryFormat deliveryFormat, WhatToDeliver whatToDeliver)
        {
            this.FormatDropdown.SelectOption(deliveryFormat);
            this.WhatToDeliver.SelectOption(whatToDeliver);
            DriverExtensions.SetTextField(emailId, ToTextBoxLocator);
        }

        /// <summary>
        /// Sets the delivery Subject text
        /// </summary>
        /// <param name="inputText"> Subject </param>
        public void SetEmailSubjectText(string inputText) => DriverExtensions.SetTextField(inputText, EmailSubjectTextboxLocator);

        /// <summary>
        /// Enters Information In Email Text Box
        /// </summary>
        /// <param name="emailInfo"> Email </param>
        public void SetEmailToText(string emailInfo) => DriverExtensions.SetTextField(emailInfo, ToTextBoxLocator);

        /// <summary>
        /// Clear the box and enter the given note text
        /// </summary>
        /// <param name="noteText"> Note text </param>
        public void SetNoteText(string noteText) => DriverExtensions.SetTextField(noteText, EmailNoteTextboxLocator);
    }
}