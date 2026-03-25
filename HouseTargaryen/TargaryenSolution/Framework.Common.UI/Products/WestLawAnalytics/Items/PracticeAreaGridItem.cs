namespace Framework.Common.UI.Products.WestLawAnalytics.Items
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Practice Area Grid Item
    /// </summary>
    public class PracticeAreaGridItem : BaseItem
    {
        private const string ContainerLctMask = "//tr[contains(@class,'wa_practiceAreaStatus')][{0}]";

        private static readonly By NameLocator = By.XPath(".//*[@class='wa_practiceAreaName']");
        private static readonly By DescriptionLocator = By.XPath(".//*[@class='wa_practiceAreaDescription']");
        private static readonly By StatusLocator = By.ClassName("wa_practiceAreaStatus");
        private static readonly By EditButtonLocator = By.XPath(".//td[@class='wa_editPracticeAreaEnableColumn']/button");
        private static readonly By DeleteButtonLocator = By.XPath(".//td[@class='wa_editPracticeAreaDeleteColumn']/button");
        private static readonly By SubmitButtonLocator = By.XPath(".//td[@class='wa_editPracticeAreaSubmitColumn']/button");
        private static readonly By CancelButtonLocator = By.XPath(".//td[@class='wa_editPracticeAreaCancelColumn']/button");
        private static readonly By PracticeAreaNameInputLocator = By.XPath(".//td[@class='wa_practiceAreaName']/input");
        private static readonly By PracticeAreaDescriptionInputLocator = By.XPath(".//td[@class='wa_practiceAreaDescription']/input");

        /// <summary>
        /// Initializes a new instance of the <see cref="PracticeAreaGridItem"/> class. 
        /// Practice Area Grid Item constructor
        /// </summary>
        /// <param name="itemIndex">
        /// Index of item
        /// </param>
        public PracticeAreaGridItem(int itemIndex)
            : base(DriverExtensions.WaitForElement(By.XPath(string.Format(ContainerLctMask, itemIndex))))
        {
        }

        /// <summary>
        /// Practice Area name
        /// </summary>
        public string Name => DriverExtensions.GetElement(this.Container, NameLocator).GetText();

        /// <summary>
        /// Practice Area description
        /// </summary>
        public string Description => DriverExtensions.GetElement(this.Container, DescriptionLocator).GetText();

        /// <summary>
        /// Practice Area status
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Status => this.IsStatusFieldDisplayed() ? DriverExtensions.GetElement(this.Container, StatusLocator).GetText() : string.Empty;

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
        /// Enter Practice Area 'Name' for item
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
        /// Enter Practice Area 'Description' for item
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
