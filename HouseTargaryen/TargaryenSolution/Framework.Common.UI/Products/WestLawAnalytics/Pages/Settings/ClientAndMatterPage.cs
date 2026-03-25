namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Client and Matter page
    /// </summary>
    public class ClientAndMatterPage : BaseModuleRegressionPage
    {
        private static readonly By MessageLocator = By.XPath("//div[@class='co_infoBox_message']");
        private static readonly By ExtractButtonLocator = By.Id("wa_extractAccountSettings");
        private static readonly By FileInputLocator = By.Id("wa_uploadFile");
        private static readonly By SelectButtonLocator = By.Id("wa_uploadFileFakeButton");
        private static readonly By SubmitButtonLocator = By.Id("wa_submitUploadButton");
        private static readonly By RefreshJobHistoryButtonLocator =
            By.XPath("//div[@id='wa_clientMattersHistory']//button");

        private static readonly By FileNameLocator = By.XPath("//div[@id='wa_fileName']//span");
        private static readonly By AccountsLocator = By.Id("wa_accounts");
        private static readonly By AccountNameLocator = By.XPath("//table[@class='wa_LocationsTable']//td[1]");
        private static readonly By TableLocator = By.Id("wa_clientMattersHistoryTable");
        private static readonly By SearchInputLocator = By.Id("cm_accountNumber");
        private static readonly By SearchButtonLocator = By.Id("cm_searchAccounts");
        private static readonly By SpinnerLocator = By.Id("upload_file");
        private static readonly By ReturnToLinkLocator = By.Id("cancel");
        private static readonly By ContainerLocator = By.Id("co_mainContainer");

        /// <summary>
        /// Get upload history table model by index
        /// </summary>
        /// <param name="itemIndex"> index </param>
        /// <typeparam name="TModel"> Model </typeparam>
        /// <returns> Model </returns>
        public TModel GetUploadHistoryTableModelByIndex<TModel>(int itemIndex) =>
            this.GetUploadHistoryTableItemByIndex(itemIndex).ToModel<TModel>();

        /// <summary>
        /// Enter account number
        /// </summary>
        /// <param name="accountNumber">Account Number</param>
        public void EnterAccountNumber(string accountNumber) =>
            DriverExtensions.WaitForElement(SearchInputLocator).SendKeys(accountNumber);

        #region Click
        /// <summary>
        /// Click upload button
        /// </summary>
        public void ClickUploadButton()
        {
            DriverExtensions.WaitForElement(SelectButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click submit button
        /// </summary>
        public void ClickSubmitButton()
        {
            DriverExtensions.Click(SubmitButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click refresh jon history button
        /// </summary>
        public void ClickRefreshJobHistoryButton()
        {
            DriverExtensions.Click(RefreshJobHistoryButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Search button
        /// </summary>
        public void ClickSearchButton()
        {
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Search button
        /// </summary>
        /// <returns>
        /// The <see cref="CtsPage"/>.
        /// </returns>
        public CtsPage ClickReturnToLink()
        {
            DriverExtensions.WaitForElement(ReturnToLinkLocator).Click();
            return new CtsPage();
        }

        /// <summary>
        /// Get Upload History Table Item
        /// </summary>
        /// <param name="itemIndex">
        /// index of item
        /// </param>
        /// <returns>
        /// The <see cref="UploadHistoryTableItem"/>.
        /// </returns>
        private UploadHistoryTableItem GetUploadHistoryTableItemByIndex(int itemIndex) => new UploadHistoryTableItem(itemIndex);
        #endregion

        #region Upload
        /// <summary>
        /// Drags the specified file to the page using JavaScript.
        /// </summary>
        /// <param name="path">Absolute path to the file</param>
        public ClientAndMatterPage DragAndDropFile(string path)
        {
            // Script creates 'input' element which
            // calls 'drop' event that is listened to by the param 'arguments[0]'
            // after uploading the file to 'input' form.
            const string Script =
                @"object = arguments[0], document = object.ownerDocument || document, window = document.defaultView || window;
                  var field = document.createElement('INPUT');
                  field.style.display = 'none';
                  field.type = 'file';
                  field.onchange = function() {
                      var rect = object.getBoundingClientRect(),
                      dataTransfer = {
                      files: this.files
                          };
                      ['dragenter', 'dragover', 'drop'].forEach(function(drag) {
                      var event = document.createEvent('MouseEvent');
                      event.initMouseEvent(drag, !0, !0, window, 0, 0, 0, rect.left, rect.top, !1, !1, !1, !1, 0, null);
                      event.dataTransfer = dataTransfer;
                      object.dispatchEvent(event);
                      });
                  };
                  document.body.appendChild(field);
                  return field;";
            var field = (IWebElement)DriverExtensions.ExecuteScript(
                Script,
                DriverExtensions.GetElement(ContainerLocator));
            field.SendKeys(path);
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
            return new ClientAndMatterPage();
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="path">Absolute path to the file</param>
        public ClientAndMatterPage UploadFile(string path)
        {
            DriverExtensions.WaitForElement(FileInputLocator).SendKeys(path);
            return new ClientAndMatterPage();
        }
        #endregion

        #region Get
        /// <summary>
        /// Get message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMessage() => DriverExtensions.GetText(MessageLocator);

        /// <summary>
        /// Get uploaded file name
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileName() => DriverExtensions.GetImmediateText(FileNameLocator);

        /// <summary>
        /// Get account name
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAccountName() => DriverExtensions.GetText(AccountNameLocator);
        #endregion

        #region Displaying
        /// <summary>
        /// Is message displayed
        /// </summary>
        /// <returns>
        /// True - if message is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsMessageDisplayed() => DriverExtensions.IsDisplayed(MessageLocator);

        /// <summary>
        /// Is history table displayed
        /// </summary>
        /// <returns>
        /// True - if table is displayed<see cref="bool"/>.
        /// </returns>
        public bool IsHistoryTableDisplayed() => DriverExtensions.IsDisplayed(TableLocator);

        /// <summary>
        /// Is Select File button displayed
        /// </summary>
        /// <returns>
        /// True - if button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsSelectFileButtonDisplayed() => DriverExtensions.IsDisplayed(SelectButtonLocator);

        /// <summary>
        /// Is Extract button displayed
        /// </summary>
        /// <returns>
        /// True - if button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsExtractButtonDisplayed() => DriverExtensions.IsDisplayed(ExtractButtonLocator);

        /// <summary>
        /// Is Refresh Job history button displayed
        /// </summary>
        /// <returns>
        /// True - if button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsRefreshJobHistoryButtonDisplayed() => DriverExtensions.IsDisplayed(RefreshJobHistoryButtonLocator);

        /// <summary>
        /// Is list of additional accounts displayed
        /// </summary>
        /// <returns>
        /// True - if list is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsAccountsListDisplayed() => DriverExtensions.IsDisplayed(AccountsLocator);

        /// <summary>
        /// Is Search input displayed
        /// </summary>
        /// <returns>
        /// True - if input is displayed<see cref="bool"/>.
        /// </returns>
        public bool IsSearchInputDisplayed() => DriverExtensions.IsDisplayed(SearchInputLocator);

        /// <summary>
        /// Is Search buuton displayed
        /// </summary>
        /// <returns>
        /// True - if button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsSearchButtonDisplayed() => DriverExtensions.IsDisplayed(SearchButtonLocator);
        #endregion
    }
}
