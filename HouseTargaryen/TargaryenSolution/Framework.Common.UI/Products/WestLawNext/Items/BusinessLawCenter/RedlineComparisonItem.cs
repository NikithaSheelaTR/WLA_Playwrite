namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RedlineComparisonItem
    /// </summary>
    public class RedlineComparisonItem : BaseItem
    {
        private static readonly By TitleLocator = By.XPath(".//p[contains(@class, 'co_redlineLightbox_tabItem')]/*");
        private static readonly By CheckboxLocator = By.XPath(".//div[contains(@class,'co_redlineLightbox_tabItem_itemCheck')]/input");
        private static readonly By DateLocator = By.XPath(".//p[contains(@class,'savedDate')]");
        private static readonly By DeleteButtonLocator = By.XPath(".//*[contains(@class,'co_redlineLightbox_tabItem_deleteButton')]");
        private static readonly By EditButtonlocator = By.XPath(".//*[contains(@class,'co_redlineLightbox_tabItem_editButton')]");
        private static readonly By EditItemInputLocator = By.XPath(".//input[@name='newItemName']");
        private static readonly By CancelEditingLinkLocator = By.XPath(".//a[contains(@class,'co_redlineLightbox_tabItem_editCancelButton')]");
        private static readonly By SaveEditingLinkLocator = By.XPath(".//*[contains(@class,'co_redlineLightbox_tabItem_editSaveButton')]");
        private static readonly By MarkAsOriginalRadiobuttonLocator = By.XPath(".//input[@value='original']");
        private static readonly By MarkAsRevisedRadiobuttonLocator = By.XPath(".//input[@value='revised']");


        /// <summary>
        /// Initializes a new instance of the <see cref="RedlineComparisonItem"/> class. 
        /// </summary>
        /// <param name="container"> Container </param>
        public RedlineComparisonItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title => DriverExtensions.WaitForElement(this.Container, TitleLocator).Text;

        /// <summary>
        /// Date
        /// </summary>
        public string Date => DriverExtensions.WaitForElement(this.Container, DateLocator).Text;

        /// <summary>
        /// Name of edited item
        /// </summary>
        /// <returns> Name </returns>
        public string NewItemName => DriverExtensions.WaitForElement(this.Container, EditItemInputLocator).GetText();

        /// <summary>
        /// Verify that Edit Input is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool EditItemInputDisplayed => DriverExtensions.IsDisplayed(this.Container, EditItemInputLocator);

        /// <summary>
        /// Verify that Original radio button is selected
        /// </summary>
        /// <returns> True if selected, false otherwise </returns>
        public bool OriginalRadiobuttonSelected => DriverExtensions.IsRadioButtonSelected(this.Container, MarkAsOriginalRadiobuttonLocator);

        /// <summary>
        /// Verify that Revised radio button is selected
        /// </summary>
        /// <returns> True if selected, false otherwise </returns>
        public bool RevisedRadiobuttonSelected => DriverExtensions.IsRadioButtonSelected(this.Container, MarkAsRevisedRadiobuttonLocator);

        /// <summary>
        /// Set checkbox
        /// Should be used only for item from SelectToCompareTabComponent, Item on the ViewSavedComparisonsTabComponent doesn't contain checkboxes
        /// </summary>
        /// <param name="selected"> Check if true, uncheck otherwise. </param>
        public void SetCheckbox(bool selected)
            => DriverExtensions.WaitForElement(this.Container, CheckboxLocator).SetCheckbox(selected);

        /// <summary>
        /// Verify that checkbox is selected
        /// </summary>
        /// <returns>true if Checked</returns>
        public bool IsCheckboxSelected() => DriverExtensions.IsCheckboxSelected(CheckboxLocator);

        /// <summary>
        /// Click Delete Button
        /// </summary>
        public void ClickDeleteButton()
        {
            IWebElement deleteButton = DriverExtensions.WaitForElement(this.Container, DeleteButtonLocator);
            deleteButton.WaitForElementEnabled();
            deleteButton.Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Edit Button
        /// </summary>
        public void ClickEditButton() => DriverExtensions.WaitForElement(this.Container, EditButtonlocator).Click();

        /// <summary>
        /// Enter new name of the item
        /// </summary>
        /// <param name="newName"> Name </param>
        public void EnterNewItemName(string newName)
            => DriverExtensions.GetElement(this.Container, EditItemInputLocator).SetTextField(newName);

        /// <summary>
        /// Click cancel button
        /// </summary>
        public void ClickCancelEditingButton()
            => DriverExtensions.WaitForElement(this.Container, CancelEditingLinkLocator).Click();

        /// <summary>
        /// Click Save button
        /// </summary>
        public void ClickSaveEditingButton()
            => DriverExtensions.WaitForElement(this.Container, SaveEditingLinkLocator).Click();
    }
}
