namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WL Analytics Practice Area page
    /// </summary>
    public class PracticeAreaPage : BaseModuleRegressionPage
    {
        private static readonly By NameTextBoxLocator = By.XPath("//th[@class='wa_practiceAreaName']/input");
        private static readonly By DescriptionTextBoxLocator = By.XPath("//th[@class='wa_practiceAreaDescription']/input");
        private static readonly By AddButtonLocator = By.Id("wa_addPracticeAreaSubmit");
        private static readonly By MessageBoxLocator = By.XPath("//div[@class = 'co_infoBox_message']");
        private static readonly By PracticeAreaRowLocator = By.XPath("//tr[contains(@class,'wa_practiceAreaStatus')]");
        private static readonly By HeaderLocator = By.XPath("//table[@class='co_formTextSelect']//td");
        private static readonly By ReturnToListLinkLocator = By.XPath("//*[@id='cancel']");

        /// <summary>
        /// Get model by index
        /// </summary>
        /// <param name="itemIndex">Number of the row in the Practice Area table</param>
        /// <typeparam name="TModel">Model</typeparam>
        /// <returns>Model</returns>
        public TModel GetPracticeAreaModelByIndex<TModel>(int itemIndex) =>
            this.GetPracticeAreaGridItemByIndex(itemIndex).ToModel<TModel>();

        /// <summary>
        /// Enter Practice Area 'Description'
        /// </summary>
        /// <param name="text">
        /// Description of Practice area
        /// </param>
        /// <param name="clearFirst">
        /// The clear First.
        /// </param>
        public void SentTextToDescriptionTextBox(string text, bool clearFirst = false) =>
            this.SentTextToTextBox(DescriptionTextBoxLocator, text, clearFirst);

        /// <summary>
        /// Enter Practice Area 'Name'
        /// </summary>
        /// <param name="text">
        /// Name of Practice area
        /// </param>
        /// <param name="clearFirst">
        /// The clear First.
        /// </param>
        public void SentTextToNameTextBox(string text, bool clearFirst = false) =>
            this.SentTextToTextBox(NameTextBoxLocator, text, clearFirst);

        #region Edit Practice area mode
        /// <summary>
        /// Edit Practice Area name
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <param name="practiceAreaName">Name of Practice area</param>
        /// <param name="clearFirst">Delete text in field</param>
        public void SentTextToItemName(int itemIndex, string practiceAreaName, bool clearFirst = false) => this
            .GetPracticeAreaGridItemByIndex(itemIndex).SentTextToNameField(practiceAreaName, clearFirst);

        /// <summary>
        /// Edit Practice Area description
        /// </summary>
        /// <param name="itemIndex">
        /// Index of item
        /// </param>
        /// <param name="practiceAreaDescription">
        /// Description of Practice area
        /// </param>
        /// <param name="clearFirst">
        /// Delete text in field
        /// </param>
        public void SentTextToItemDescription(int itemIndex, string practiceAreaDescription, bool clearFirst = false) =>
            this.GetPracticeAreaGridItemByIndex(itemIndex).SentTextToDescriptionField(practiceAreaDescription, clearFirst);
        #endregion Edit Practice area mode

        #region Click
        /// <summary>
        /// Click Add button
        /// If after clicking "Add" button, alert will be displayed
        /// Click "OK" button in the Alert window
        /// </summary>
        public void ClickAddButton() => DriverExtensions.WaitForElement(AddButtonLocator).Click();

        /// <summary>
        /// Click Edit button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemEditButton(int itemIndex) => this.GetPracticeAreaGridItemByIndex(itemIndex).ClickEditButton();

        /// <summary>
        /// Click Delete button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemDeleteButton(int itemIndex) => this.GetPracticeAreaGridItemByIndex(itemIndex).ClickDeleteButton();

        /// <summary>
        /// Click Cancel button
        /// </summary>
        /// <param name="itemIndex">
        /// The Index of item
        /// </param>
        public void ClickItemCancelButton(int itemIndex) => this.GetPracticeAreaGridItemByIndex(itemIndex).ClickCancelButton();

        /// <summary>
        /// Click Submit button
        /// </summary>
        /// <param name="itemIndex">
        /// The item Index.
        /// </param>
        public void ClickItemSubmitButton(int itemIndex) =>
            this.GetPracticeAreaGridItemByIndex(itemIndex).ClickSubmitButton();
        #endregion Click

        #region Get
        /// <summary>
        /// Get Header text
        /// </summary>
        /// <returns>
        /// Text of the Header <see cref="string"/>.
        /// </returns>
        public string GetHeaderText() => DriverExtensions.GetText(HeaderLocator);

        /// <summary>
        /// Get info message text
        /// </summary>
        /// <returns>Message's text</returns>
        public string GetInfoMessageText() =>
            DriverExtensions.WaitForElementDisplayed(MessageBoxLocator).Text;

        /// <summary>
        /// Get Practice Area Count
        /// </summary>
        /// <returns>Count of rows in the table</returns>
        public int GetPracticeAreaCount() => DriverExtensions.GetElements(PracticeAreaRowLocator).Count;

        /// <summary>
        /// Get "Return to Account List" link count
        /// </summary>
        /// <returns>Count of elements on the page</returns>
        public int GetReturnToListLinksCount() => DriverExtensions.GetElements(ReturnToListLinkLocator).Count;
        #endregion Get

        #region IsDisplayed
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
        public bool IsItemEditButtonDisplayed(int itemIndex) => this.GetPracticeAreaGridItemByIndex(itemIndex).IsEditButtonDisplayed();

        /// <summary>
        /// Is 'Delete' button displayed for item
        /// </summary>
        /// <param name="itemIndex">
        /// The item Index.
        /// </param>
        /// <returns>
        /// True if 'Edits' button is displayed<see cref="bool"/>.
        /// </returns>
        public bool IsItemDeleteButtonDisplayed(int itemIndex) => this.GetPracticeAreaGridItemByIndex(itemIndex).IsDeleteButtonDisplayed();
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
        /// Get Practice Area Grid Item
        /// </summary>
        /// <param name="itemIndex">Number of the row in the Practice Area table</param>
        /// <returns>Grid items</returns>
        private PracticeAreaGridItem GetPracticeAreaGridItemByIndex(int itemIndex) => new PracticeAreaGridItem(itemIndex);
    }
}