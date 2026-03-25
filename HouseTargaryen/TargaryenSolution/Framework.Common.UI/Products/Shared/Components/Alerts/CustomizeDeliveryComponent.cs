namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Delivery;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Customize Delivery Section for Alert
    /// </summary>
    public class CustomizeDeliveryComponent : BaseAlertComponent
    {
        private static readonly By AddToExistingNewsletterButtonLocator = By.XPath(".//button[contains(@class,'co_addLink')]");

        private static readonly By AddToNewNewsletterButtonLocator = By.XPath(".//button[contains(@class,'co_newLink')]");

        private static readonly By DetailLevelTextLocator = By.Id("otherSummaryDetailLevel");

        private static readonly By DeliveryFormatTextLocator = By.Id("deliverFormat");

        private static readonly By DetailLevelDropdownLocator = By.Id("coid_chkDdcDetailLevel");

        private static readonly By NumberOfItemsTextLocator = By.Id("deliverMax");

        private static readonly By DeliverTypeTextLocator = By.Id("deliverType");

        private static readonly By OnlyPagesWithSearchTermsTextLocator = By.Id("onlyPagesWithSearchTermsContainer");

        private static readonly By RecipientsTextLocator = By.Id("summaryRecipients");

        private static readonly By DeliverySummaryLabelsLocator = By.CssSelector("#deliverySummaryDiv div div p strong");

        private static readonly By DeliverySummaryValuesLocator = By.CssSelector("#deliverySummaryDiv div div p span");

        private static readonly By EmailSettingsExpandButtonLocator =
            By.CssSelector("#co_emailDelivery #coid_subBellow_email_collapselink");

        private static readonly By EmailTextboxLocator = By.Id("coid_contacts_addedContactsInput_co_collaboratorWidget");

        private static readonly By FullDocketsLabelLocator = By.CssSelector("li#co_delivery_documents.co_formInline label");

        private static readonly By IncludeFullTextCheckboxLocator = By.Id("co_delivery_chkincludeFullTextNewDocs");

        private static readonly By IncludeOutOfPlanDocCheckboxLocator = By.Id("coid_chkAcceptCharges");

        private static readonly By InfoNoteTextboxLocator =
            By.XPath("//div[contains(@id, 'co_deliveryOptionsTabPanel1')]//div[contains(@class, 'co_deliveryNote')]");

        private static readonly By LayoutAndLimitsTabLocator =
            By.XPath("//button[contains(@class,'Tab') and text() = 'Layout and limits']");

        private static readonly By NewOrChangedEntriesRadioButtonLabelLocator =
            By.CssSelector("li#co_delivery_itemsList.co_formInline label");

        private static readonly By NewUserTextboxLocator = By.CssSelector(".co_contacts_collector_addNew input");

        private static readonly By NumberOfItemsLocator =
            By.XPath("//*[not(contains(@style,'display: none;'))]/input[contains(@id,'co_alerts_delivery_items_as')] | //label[contains(text(), 'up to a maximum of')]//following-sibling::input");

        private static readonly By OtherSectionNumberOfItemsTextboxLocator = By.Id("co_otherDeliveryNoOfItems");

        private static readonly By OtherSettingsDetailLevelDropdownLocator = By.Id("co_otherDeliveryDetailLevel");

        private static readonly By OtherSettingsExpandButtonLocator =
            By.CssSelector("#co_otherDelivery #coid_subBellow_other_collapselink");

        private static readonly By PreviewResultsButtonLocator = By.Id("co_button_previewResults_Delivery");

        private static readonly By RecipientsTabLocator = By.XPath("//button[contains(@class,'Tab') and text() = 'Recipients']");

        private static readonly By RemoveContactsLinkLocator =
            By.XPath(
                "//ul[contains(@id, 'coid_contacts_addedContactsInput_co_collaboratorWidget')]//a[contains(@oldtitle, 'remove')]");

        private static readonly By SelectedDeliveryFormatLocator = By.XPath("//select[@id='co_delivery_format_list']/option[@selected]");

        private static readonly By ContainerLocator = By.Id("deliverySection");

        private static readonly By CustomizeDeliveryHeaderLabelLocator = By.XPath(".//h2[@id='deliveryBellowHeader']/strong");

        private static readonly By NumberOfItemsLabelLocator = By.XPath(".//label[@for='co_alerts_delivery_items_as_list']");

        private static readonly By WhatToDeliverLabelLocator = By.XPath(".//*[@id='co_whatToDeliverTitle'] | .//div[@id='co_delivery_whatToDeliverContainer']/h3 | .//div[@id='co_delivery_whatToDeliverContainer']/span");

        private static readonly By DetailLevelLabelLocator = By.XPath(".//div[@id='co_delivery_DetailLevelContainer']//label");

        private static readonly By DeliveryFormat = By.XPath("//select[@id='co_delivery_format_list']/option[@value='InlineHtml']");

        private EnumPropertyMapper<AlertsDeliveryOption, WebElementInfo> alertsDeliveryOptionsMap;

        /// <summary>
        /// Add alerts to an existing newsletter button
        /// </summary>
        public IButton AddToExistingNewsletterButton = new Button(ContainerLocator, AddToExistingNewsletterButtonLocator);

        /// <summary>
        /// Add alerts to new newsletter button
        /// </summary>
        public IButton AddToNewNewsletterButton = new Button(ContainerLocator, AddToNewNewsletterButtonLocator);

        /// <summary>
        /// Layout And Limits Tab Component
        /// </summary>
        public LayoutAndLimitsTabComponent LayoutAndLimits { get; } = new LayoutAndLimitsTabComponent();

        /// <summary>
        /// Recipients Tab Component
        /// </summary>
        public RecipientsTabComponent Recipients { get; } = new RecipientsTabComponent();

        /// <summary>
        /// What To Deliver Component
        /// </summary>
        public WhatToDeliverComponent WhatToDeliver { get; } = new WhatToDeliverComponent();

        /// <summary>
        ///  Customize delivery header label
        /// </summary>
        public ILabel CustomizeDeliveryHeaderLabel => new Label(this.ComponentLocator, CustomizeDeliveryHeaderLabelLocator);

        /// <summary>
        ///  Number of items label
        /// </summary>
        public ILabel NumberOfItemsLabel => new Label(this.ComponentLocator, NumberOfItemsLabelLocator);

        /// <summary>
        ///  What to deliver label
        /// </summary>
        public ILabel WhatToDeliverLabel => new Label(this.ComponentLocator, WhatToDeliverLabelLocator);

        /// <summary>
        ///  Detail level label
        /// </summary>
        public ILabel DetailLevelLabel => new Label(this.ComponentLocator, DetailLevelLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AlertsDeliveryOption enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AlertsDeliveryOption, WebElementInfo> AlertsDeliveryOptionsMap
            => this.alertsDeliveryOptionsMap =
                    this.alertsDeliveryOptionsMap
                    ?? EnumPropertyModelCache.GetMap<AlertsDeliveryOption, WebElementInfo>();

        /// <summary>
        /// Clear the number of items textbox text
        /// </summary>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent ClearNumberOfItemsText()
        {
            DriverExtensions.WaitForElement(NumberOfItemsLocator).Clear();
            return this;
        }

        /// <summary>
        /// Clears the number of items textbox text
        /// </summary>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent ClearOtherNumberOfItemsText()
        {
            DriverExtensions.GetElement(OtherSectionNumberOfItemsTextboxLocator).Clear();
            return this;
        }

        /// <summary>
        /// Open Layout And Limits tab
        /// </summary>
        public void ClickLayoutAndLimitsTab() => DriverExtensions.Click(LayoutAndLimitsTabLocator);

        /// <summary>
        /// Click on the Preview Results Button
        /// </summary>
        /// <returns> The <see cref="PreviewResultsDialog"/>. </returns>
        public PreviewResultsDialog ClickPreviewResultsButton()
        {
            DriverExtensions.Click(PreviewResultsButtonLocator);
            return new PreviewResultsDialog();
        }

        /// <summary>
        /// Click Recipients Tab
        /// </summary>
        public void ClickRecipientsTab() => DriverExtensions.Click(RecipientsTabLocator);

        /// <summary>
        /// Deletes all current Email Recipients within an alert
        /// </summary>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent DeleteAllEmailRecipients()
        {
            DriverExtensions.GetElements(RemoveContactsLinkLocator).ToList().ForEach(DriverExtensions.Click);

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Delete All Recipients And Add New Recipient
        /// Note - Set Delivery Checkbox and Toggle Email Settings Section is also called
        /// </summary>
        /// <param name="email"> email </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public T DeleteAllRecipientsAndAddNewRecipient<T>(string email) where T : ICreatablePageObject
        {
            this.SetDeliveryCheckbox(true, AlertsDeliveryOption.Email);
            
            this.ExpandEmailSettingsSection();
            this.DeleteAllEmailRecipients();

            if (!DriverExtensions.IsDisplayed(NewUserTextboxLocator))
            {
                DriverExtensions.GetElement(EmailTextboxLocator).Click();
                DriverExtensions.WaitForPageLoad();
                DriverExtensions.WaitForJavaScript();
            }

            DriverExtensions.GetElement(NewUserTextboxLocator).SendKeys(email);

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get selected detail level text
        /// </summary>
        /// <returns> Detail Level text</returns>
        public string GetDetailLevelDropdownSelectedText() => DriverExtensions.GetSelectElementSelectedText(DetailLevelDropdownLocator);

        /// <summary>
        /// Get text from email textbox
        /// </summary>
        /// <returns> Text from email textbox </returns>
        public string GetEmailText() => DriverExtensions.GetText(EmailTextboxLocator);

        /// <summary>
        /// Get text from the 'Full Dockets' label
        /// </summary>
        /// <returns> Text from the 'Full Dockets' label </returns>
        public string GetFullDocketsLabelText() => DriverExtensions.GetText(FullDocketsLabelLocator);

        /// <summary>
        /// Get 'Include out-of-plan documents' label text
        /// </summary>
        /// <returns> 'Include out-of-plan documents' label text </returns>
        public string GetIncludeOutOfPlanCheckboxText() => DriverExtensions.GetText(IncludeOutOfPlanDocCheckboxLocator);

        /// <summary>
        /// Get text of the radio button 'Get New Or Changed Entries'
        /// </summary>
        /// <returns> Text of the radio button label </returns>
        public string GetNewOrChangedEntriesRadioButtonLabelText() => DriverExtensions.GetText(NewOrChangedEntriesRadioButtonLabelLocator);

        /// <summary>
        /// Get text from the newsletter note
        /// </summary>
        /// <returns> Newsletter note text </returns>
        public string GetNewsletterInfoNoteText() => DriverExtensions.GetText(InfoNoteTextboxLocator);

        /// <summary>
        /// Gets the number of items textbox text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNumberOfItemsText() => DriverExtensions.GetText(NumberOfItemsLocator);

        /// <summary>
        /// Get text from 'Detail Level' dropdown
        /// </summary>
        /// <returns> Selected 'Detail Level' option </returns>
        public string GetOtherDetailLevelDropdownSelectedText()
        {
            DriverExtensions.WaitForElementDisplayed(OtherSettingsDetailLevelDropdownLocator);
            return DriverExtensions.GetSelectElementSelectedText(OtherSettingsDetailLevelDropdownLocator);
        }

        /// <summary>
        /// Gets the other number of items textbox text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetOtherNumberOfItemsText() => DriverExtensions.GetText(OtherSectionNumberOfItemsTextboxLocator);
        
        /// <summary>
        /// Get if the specific checkbox is checked
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> True if checkbox is checked, false otherwise </returns>
        public bool IsDeliveryCheckboxChecked(AlertsDeliveryOption option)
            => DriverExtensions.IsCheckboxSelected(By.Id(this.AlertsDeliveryOptionsMap[option].Id));

        /// <summary>
        /// Detail Level Dropdown Is Displayed
        /// </summary>
        /// <returns> True if Detail Level Dropdown is displayed, false otherwise </returns>
        public bool IsDetailLevelDropdownDisplayed() => DriverExtensions.IsDisplayed(DetailLevelDropdownLocator);

        /// <summary>
        /// Email Textbox Is Displayed
        /// </summary>
        /// <returns> True if Email Textbox is displayed, false otherwise </returns>
        public bool IsEmailTextboxDisplayed() => DriverExtensions.IsDisplayed(EmailTextboxLocator);

        /// <summary>
        /// Verify that 'Include Full Text' checkbox is checked
        /// </summary>
        /// <returns> True if 'Include Full Text' checkbox is checked, false otherwise </returns>
        public bool IsIncludeFullTextCheckboxChecked() => DriverExtensions.IsCheckboxSelected(IncludeFullTextCheckboxLocator);

        /// <summary>
        /// Verify that 'Include out-of-plan documents' checkbox is checked
        /// </summary>
        /// <returns> True if 'Include out-of-plan documents' is checked, false otherwise </returns>
        public bool IsIncludeOutOfPlanCheckboxChecked() => DriverExtensions.GetElement(IncludeOutOfPlanDocCheckboxLocator).Selected;

        /// <summary>
        /// Include Out Of Plan Checkbox Is Displayed
        /// </summary>
        /// <returns> True if 'Out Of Plan' checkbox is displayed, false otherwise </returns>
        public bool IsIncludeOutOfPlanCheckboxDisplayed() => DriverExtensions.IsDisplayed(IncludeOutOfPlanDocCheckboxLocator);

        /// <summary>
        /// Set all delivery checkboxes to the given state
        /// </summary>
        /// <param name="check"> Select/Unselect </param>
        /// <param name="options"> Delivery options to select/unselect </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent SetDeliveryCheckbox(bool check, params AlertsDeliveryOption[] options)
        {
            options.ToList().ForEach(option => DriverExtensions.SetCheckbox(check, By.Id(this.AlertsDeliveryOptionsMap[option].Id)));

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Select Detail Level option
        /// </summary>
        /// <param name="selection"> Detail Level option </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent SetDetailLevelDropdown(string selection)
        {
            DriverExtensions.SetDropdown(selection, DetailLevelDropdownLocator);
            return this;
        }

        /// <summary>
        /// Select/Unselect 'Include Full Text' checkbox
        /// </summary>
        /// <param name="setTo"> true to select, false to unselect </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public T SetIncludeFullTextCheckbox<T>(bool setTo) where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(setTo, IncludeFullTextCheckboxLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Set Include Out Of Plan Checkbox
        /// </summary>
        /// <param name="setBox"> true to select, false to unselect  </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public T SetIncludeOutOfPlanCheckbox<T>(bool setBox) where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(setBox, IncludeOutOfPlanDocCheckboxLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Sets the max number textbox text
        /// </summary>
        /// <param name="inputText"> Number of items </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public T SetNumberOfItemsText<T>(string inputText) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(inputText, NumberOfItemsLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select Other Detail Level option
        /// </summary>
        /// <param name="selection"> Other detail Level option </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent SetOtherDetailLevelDropdown(string selection)
        {
            DriverExtensions.SetDropdown(selection, OtherSettingsDetailLevelDropdownLocator);
            return this;
        }

        /// <summary>
        /// Set Other Number Of Items Text
        /// </summary>
        /// <param name="inputText"> Other number of items </param>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent SetOtherNumberOfItemsText(string inputText)
        {
            DriverExtensions.SetTextField(inputText, OtherSectionNumberOfItemsTextboxLocator);
            return this;
        }

        /// <summary>
        /// Open or close the Email Settings section based on the given Boolean
        /// </summary>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent ExpandEmailSettingsSection() => this.ExpandSettingSection(true);

        /// <summary>
        /// Opens or closes the Other Settings section based on the given Boolean
        /// </summary>
        /// <returns> The <see cref="CustomizeDeliveryComponent"/>. </returns>
        public CustomizeDeliveryComponent ExpandOtherSettingsSection() => this.ExpandSettingSection(false);

        /// <summary> 
        /// Verify that 'Detail Level' information is displayed when Delivery section is collapsed 
        /// Verify that 'Detail Level' information is displayed when Delivery section is collapsed 
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDetailLevelInfoDisplayed() => DriverExtensions.IsDisplayed(DetailLevelTextLocator);

        /// <summary> 
        /// Verify that 'Format' information is displayed when Delivery section is collapsed 
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFormatInfoDisplayed() => DriverExtensions.IsDisplayed(DeliveryFormatTextLocator);

        /// <summary> 
        /// Get selected delivery Format 
        /// </summary>
        public string GeSelectedDeliveryFormat() => DriverExtensions.WaitForElement(SelectedDeliveryFormatLocator).GetAttribute("text");

        /// <summary> 
        /// Verify that 'Number Of Items' information is displayed when Delivery section is collapsed 
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsNumberOfItemsInfoDisplayed() => DriverExtensions.IsDisplayed(NumberOfItemsTextLocator);

        /// <summary> 
        /// Verify that 'Deliver' (type) information is displayed when Delivery section is collapsed 
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTypeInfoDisplayed() => DriverExtensions.IsDisplayed(DeliverTypeTextLocator);

        /// <summary>
        /// Get 'Only Page With Terms' text when Delivery section is collapsed 
        /// </summary>
        /// <returns> 'Only Page With Terms' text </returns>
        public bool IsOnlyPagesWithSearchTermsInfoDisplayed() => DriverExtensions.IsDisplayed(OnlyPagesWithSearchTermsTextLocator);

        /// <summary> Verify that 'Recipients' information is displayed </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsRecipientsInfoDisplayed() => DriverExtensions.IsDisplayed(RecipientsTextLocator);

        /// <summary>
        /// Get Delivery Summary
        /// </summary>
        /// <returns> Delivery Summary dictionary </returns>
        public Dictionary<string, string> GetDeliverySummarySettings()
        {
            IList<IWebElement> labelElements = DriverExtensions.GetElements(DeliverySummaryLabelsLocator).ToList();
            IList<IWebElement> valueElements = DriverExtensions.GetElements(DeliverySummaryValuesLocator).ToList();
            var settings = new Dictionary<string, string>();

            for (int settingIndex = 0; settingIndex < labelElements.Count; settingIndex++)
            {
                if (labelElements[settingIndex].Displayed)
                {
                    string label = labelElements[settingIndex].Text.Split(':')[0].Trim();
                    string value = valueElements[settingIndex].Text.Trim();
                    settings[label] = value;
                }
            }

            return settings;
        }

        /// <summary> 
        /// Get the delivery Format 
        /// </summary>
        public T GetDeliveryFormat<T>(string format) where T : ICreatablePageObject
        {
            DriverExtensions.GetText(DeliveryFormat);
            return DriverExtensions.CreatePageInstance<T>();
        }

        private CustomizeDeliveryComponent ExpandSettingSection(bool emailSection)
        {
            IWebElement expandButtonElement =
                DriverExtensions.WaitForElement(emailSection ? EmailSettingsExpandButtonLocator : OtherSettingsExpandButtonLocator);
   
            if (expandButtonElement.GetAttribute("class").Contains("Expand"))
            {
                 DriverExtensions.Click(expandButtonElement);
            }

            return this;
        }
    }
}