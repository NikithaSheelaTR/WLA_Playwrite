namespace Framework.Common.UI.Products.WestLawAnalytics.Items
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analytics Reason Code page is displayed when West-Hosted type validation is choosen
    /// </summary>
    public class ReasonCodeGridItem : BaseItem
    {
        private const string ContainerLctMask = "//tr[contains(@class,'wa_reasonCodeStatus')][{0}]";

        private static readonly By NameLocator = By.XPath(".//*[@class='wa_reasonCodeName']");
        private static readonly By DescriptionLocator = By.XPath(".//*[@class='wa_reasonCodeDescription']");
        private static readonly By StatusLocator = By.ClassName("wa_practiceAreaStatus");
        private static readonly By EditButtonLocator = By.XPath(".//td[@class='wa_editReasonCodeEnableColumn']/button");
        private static readonly By DeleteButtonLocator = By.XPath(".//td[@class='wa_editReasonCodeDeleteColumn']/button");
        private static readonly By SubmitButtonLocator = By.XPath(".//td[@class='wa_editReasonCodeSubmitColumn']/button");
        private static readonly By CancelButtonLocator = By.XPath(".//td[@class='wa_editReasonCodeCancelColumn']/button");
        private static readonly By PracticeAreaNameInputLocator = By.XPath(".//*[@class='wa_reasonCodeInputName']");
        private static readonly By PracticeAreaDescriptionInputLocator = By.XPath(".//*[@class='wa_reasonCodeInputDescription']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonCodeGridItem"/> class. 
        /// </summary>
        /// <param name="itemIndex">
        /// Index of the row
        /// </param>
        public ReasonCodeGridItem(int itemIndex)
            : base(DriverExtensions.WaitForElement(By.XPath(string.Format(ContainerLctMask, itemIndex))))
        {
        }

        /// <summary>
        /// Reason Code name
        /// </summary>
        public string Name => DriverExtensions.GetElement(this.Container, NameLocator).GetText();

        /// <summary>
        /// Reason Code description
        /// </summary>
        public string Description => DriverExtensions.GetElement(this.Container, DescriptionLocator).GetText();

        /// <summary>
        /// Reason Code status
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Status =>
            this.IsStatusFieldDisplayed() ? DriverExtensions.GetElement(this.Container, StatusLocator).GetText() : string.Empty;

        /// <summary>
        /// Is Status Field displayed
        /// </summary>
        /// <returns>True</returns>
        public bool IsStatusFieldDisplayed() => DriverExtensions.IsDisplayed(this.Container, StatusLocator);

        #region Click
        /// <summary>
        /// Click Edit button
        /// </summary>
        public void ClickEditButton() =>
            DriverExtensions.GetElement(this.Container, EditButtonLocator).Click();

        /// <summary>
        /// Click Delete button
        /// </summary>
        public void ClickDeleteButton() =>
            DriverExtensions.GetElement(this.Container, DeleteButtonLocator).Click();

        /// <summary>
        /// Click Submit button
        /// </summary>
        public void ClickSubmitButton() =>
            DriverExtensions.GetElement(this.Container, SubmitButtonLocator).Click();

        /// <summary>
        /// Click Cancel button
        /// </summary>
        public void ClickCancelButton() =>
            DriverExtensions.GetElement(this.Container, CancelButtonLocator).Click();
        #endregion Click

        #region Edit mode
        /// <summary>
        /// Enter Reason Code Name for item
        /// </summary>
        /// <param name="text">
        /// Name of item
        /// </param>
        /// <param name="clearFirst">
        /// The clear First
        /// </param>
        public void SentTextToNameField(string text, bool clearFirst)
        {
            IWebElement textbox = DriverExtensions.GetElement(this.Container, PracticeAreaNameInputLocator);
            this.SentTextToTextField(textbox, text, clearFirst);
        }

        /// <summary>
        /// Enter Reason Code 'Description' for item
        /// </summary>
        /// <param name="text">
        /// Description of item
        /// </param>
        /// <param name="clearFirst">
        /// The clear First.
        /// </param>
        public void SentTextToDescriptionField(string text, bool clearFirst)
        {
            IWebElement textbox = DriverExtensions.GetElement(this.Container, PracticeAreaDescriptionInputLocator);
            this.SentTextToTextField(textbox, text, clearFirst);
        }
        #endregion Edit mode

        #region IsDisplayed
        /// <summary>
        /// Is 'Edit' button displayed for item
        /// </summary>
        /// <returns>True if 'Edits' button is displayed, false otherwise.</returns>
        public bool IsEditButtonDisplayed() => DriverExtensions.IsDisplayed(this.Container, EditButtonLocator);

        /// <summary>
        /// Is 'Delete' button displayed for item
        /// </summary>
        /// <returns>True if 'Edits' button is displayed, false otherwise.</returns>
        public bool IsDeleteButtonDisplayed() => DriverExtensions.IsDisplayed(this.Container, DeleteButtonLocator);
        #endregion IsDisplayed

        private void SentTextToTextField(IWebElement textbox, string text, bool clearFirst)
        {
            if (clearFirst)
            {
                textbox.Clear();
            }

            textbox.SendKeys(text);
        }
    }
}
