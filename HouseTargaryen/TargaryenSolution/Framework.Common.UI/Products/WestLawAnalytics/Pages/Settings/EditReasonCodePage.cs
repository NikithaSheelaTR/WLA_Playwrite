namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WL Analytics Edit Reason Code page
    /// This page is able only for West-Hosted type validation
    /// </summary>
    public class EditReasonCodePage : BaseModuleRegressionPage
    {
        private static readonly By NameTextBoxLocator = By.XPath("//th[@class='wa_reasonCodeName']/input");
        private static readonly By DescriptionTextBoxLocator = By.XPath("//th[@class='wa_reasonCodeDescription']/input");
        private static readonly By AddButtonLocator = By.XPath("//button[@id = 'wa_addReasonCodeSubmit']");
        private static readonly By MessageBoxLocator = By.XPath("//div[@class = 'co_infoBox_message']");
        private static readonly By ReasonCodeRowLocator = By.XPath("//tr[@class = 'wa_reasonCodeStatusActive']");
        private static readonly By HeaderLocator = By.XPath("//div/h3");
        private static readonly By InfoMessageLocator = By.XPath("//table[@class='co_formTextSelect']//td");
        private static readonly By ReturnToListLinkLocator = By.XPath("//*[@id='cancel']");

        /// <summary>
        /// Get model by index
        /// </summary>
        /// <param name="itemIndex">Number of the row in the table</param>
        /// <typeparam name="TModel">Model</typeparam>
        /// <returns>Model</returns>
        public TModel GetReasonCodeModelByIndex<TModel>(int itemIndex) =>
            this.GetReasonCodeGridItemByIndex(itemIndex).ToModel<TModel>();

        /// <summary>
        /// Enter Reason Code 'Description'
        /// </summary>
        /// <param name="text">
        /// Description of the Reason Code
        /// </param>
        /// <param name="clearFirst">
        /// The clear First.
        /// </param>
        public void SentTextToDescriptionTextBox(string text, bool clearFirst = false) =>
            this.SentTextToTextBox(DescriptionTextBoxLocator, text, clearFirst);

        /// <summary>
        /// Enter Reason Code 'Name'
        /// </summary>
        /// <param name="text">
        /// Name of Reason Code
        /// </param>
        /// <param name="clearFirst">
        /// The clear First.
        /// </param>
        public void SentTextToNameTextBox(string text, bool clearFirst = false) =>
            this.SentTextToTextBox(NameTextBoxLocator, text, clearFirst);

        #region Edit Reason Code mode
        /// <summary>
        /// Edit Reason Code name
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <param name="reasonCodeName">Name of Reason Code</param>
        /// <param name="clearFirst">Delete text in field</param>
        public void SentTextToItemName(int itemIndex, string reasonCodeName, bool clearFirst = false) => this
            .GetReasonCodeGridItemByIndex(itemIndex).SentTextToNameField(reasonCodeName, clearFirst);

        /// <summary>
        /// Edit Reason Code description
        /// </summary>
        /// <param name="itemIndex">
        /// Index of item
        /// </param>
        /// <param name="reasonCodeDescription">
        /// Description of Reason Code
        /// </param>
        /// <param name="clearFirst">
        /// Delete text in field
        /// </param>
        public void SentTextToItemDescription(int itemIndex, string reasonCodeDescription, bool clearFirst = false) =>
            this.GetReasonCodeGridItemByIndex(itemIndex).SentTextToDescriptionField(reasonCodeDescription, clearFirst);
        #endregion Edit Reason Code mode

        #region Click
        /// <summary>
        /// Click Add button
        /// </summary>
        public void ClickAddButton() => DriverExtensions.WaitForElement(AddButtonLocator).Click();

        /// <summary>
        /// Click Edit button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemEditButton(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).ClickEditButton();

        /// <summary>
        /// Click Delete button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemDeleteButton(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).ClickDeleteButton();

        /// <summary>
        /// Click Cancel button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemCancelButton(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).ClickCancelButton();

        /// <summary>
        /// Click Submit button
        /// </summary>
        /// <param name="itemIndex">
        /// The item Index.
        /// </param>
        public void ClickItemSubmitButton(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).ClickSubmitButton();
        #endregion Click

        #region Get
        /// <summary>
        /// Get Header text with account name
        /// </summary>
        /// <returns>
        /// Text of the Header <see cref="string"/>.
        /// </returns>
        public string GetAccountInfoText() => DriverExtensions.GetText(InfoMessageLocator);

        /// <summary>
        /// Get info message text
        /// </summary>
        /// <returns>Message's text</returns>
        public string GetInfoMessageText() => DriverExtensions.WaitForElementDisplayed(MessageBoxLocator).Text;

        /// <summary>
        /// Get Reason Code Count
        /// </summary>
        /// <returns>Count of rows in the table</returns>
        public int GetReasonCodeCount() => DriverExtensions.GetElements(ReasonCodeRowLocator).Count;

        /// <summary>
        /// Get "Return to Account List" link count
        /// </summary>
        /// <returns>Count of elements on the page</returns>
        public int GetReturnToListLinksCount() => DriverExtensions.GetElements(ReturnToListLinkLocator).Count;
        #endregion Get

        #region IsDisplayed

        /// <summary>
        /// Check is header "Edit Reason Code" displayed
        /// </summary>
        /// <returns>
        /// True - if the "Name" textbox is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsHeaderDisplayed() => DriverExtensions.IsDisplayed(HeaderLocator);
        
        /// <summary>
        /// Check is "Name" textbox displayed
        /// </summary>
        /// <returns>
        /// True - if the "Name" textbox is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsNameTextBoxDisplayed() => DriverExtensions.IsDisplayed(NameTextBoxLocator);

        /// <summary>
        /// Check is "Description" textbox displayed
        /// </summary>
        /// <returns>
        /// True - if the "Decription" textbox is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsDescriptionTextBoxDisplayed() => DriverExtensions.IsDisplayed(DescriptionTextBoxLocator);

        /// <summary>
        /// Check is "Add" button is displayed
        /// </summary>
        /// <returns>
        /// True - if the "Add" button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsAddButtonDisplayed() => DriverExtensions.IsDisplayed(AddButtonLocator);

        /// <summary>
        /// Is 'Edit' button displayed for item
        /// </summary>
        /// <param name="itemIndex">
        /// The item Index.
        /// </param>
        /// <returns>
        /// True if 'Edits' button is displayed<see cref="bool"/>.
        /// </returns>
        public bool IsItemEditButtonDisplayed(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).IsEditButtonDisplayed();

        /// <summary>
        /// Is 'Delete' button displayed for item
        /// </summary>
        /// <param name="itemIndex">
        /// The item Index.
        /// </param>
        /// <returns>
        /// True if 'Edits' button is displayed<see cref="bool"/>.
        /// </returns>
        public bool IsItemDeleteButtonDisplayed(int itemIndex) => this.GetReasonCodeGridItemByIndex(itemIndex).IsDeleteButtonDisplayed();
        #endregion IsDisplayed

        #region Actions with Alert
        /// <summary>
        /// Close Alert
        /// </summary>
        public void CloseAlert()
        {
            BrowserPool.CurrentBrowser.Driver.SwitchTo().Alert().Accept();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get text from Alert
        /// </summary>
        /// <returns>Get text of alert</returns>
        public string GetAlertText() => BrowserPool.CurrentBrowser.Driver.SwitchTo().Alert().Text;
        #endregion Actions with Alert

        private void SentTextToTextBox(By locator, string text, bool clearFirst = false)
        {
            if (clearFirst)
            {
                DriverExtensions.WaitForElement(locator).Clear();
            }

            DriverExtensions.WaitForElement(locator).SendKeys(text);
        }

        /// <summary>
        /// Get Reason Code Grid Item
        /// </summary>
        /// <param name="itemIndex">Number of the row in the Reason Code table</param>
        /// <returns>Grid items</returns>
        private ReasonCodeGridItem GetReasonCodeGridItemByIndex(int itemIndex) => new ReasonCodeGridItem(itemIndex);
    }
}
