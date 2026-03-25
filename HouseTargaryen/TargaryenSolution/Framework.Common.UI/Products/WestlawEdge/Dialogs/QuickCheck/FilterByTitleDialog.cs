namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Filter by title dialog
    /// </summary>
    public class FilterByTitleDialog : BaseAvailableAndSelectedHierarchicalOptionsListsDialog
    {
        private const string AvailableItemLctMask = ".//*[contains(text(),{0})]";
        private const string AvailableItemsForSpecifiedContentTypeLctMask = ".//span[@class='DA-SearchByTitle-contentType' and text()={0}]//ancestor::li//ul//li";

        private static readonly By CancelButtonLocator = By.XPath(".//button[text()='Cancel']");
        private static readonly By CitationTitleLocator = By.XPath(".//span[contains(@class, 'DA-SearchByTitle-title')]");
        private static readonly By ContainerLocator = By.XPath("//div[@id='DA-SearchByTitleLightbox']");
        private static readonly By ContinueButtonLocator = By.XPath(".//button[text()='Continue']");
        private static readonly By DisabledItemsLocator = By.XPath(".//li[@class='DA-SearchByTitle-item disabled']");
        private static readonly By ExpandContentTypeButtonLocator = By.XPath(".//button[@aria-expanded='false']");
        private static readonly By HighlightedTermLocator = By.XPath(".//span[@class='co_searchTerm']");
        private static readonly By SearchTextBoxLocator = By.XPath(".//input[@id='DA-SearchByTitle-input']");
        private static readonly By SelectedItemsLocator = By.XPath(".//*[contains(@class, 'rightContent')]//li[@class='DA-SearchByTitle-item']");
        private static readonly By SelectionsAreaLocator = By.XPath(".//ul[@class='DA-SearchByTitle-selectedContainer']");
        private static readonly By TitleAreaLocator = By.XPath(".//ul[@class='DA-SearchByTitle-resultsContainer']");
        private static readonly By XButtonLocator = By.XPath(".//span[@class='Icon-clear']");
        private static readonly By ExpandSelectedTitlesButtonLocator = By.XPath("//ul[@class='DA-SearchByTitle-selectedContainer']//button");

        /// <summary> Container </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
        
        /// <summary> Search text box </summary>
        public ITextbox SearchTextBox => new Textbox(this.Container, SearchTextBoxLocator);

        /// <summary> Continue button </summary>
        public IButton ContinueButton => new Button(this.Container, ContinueButtonLocator);

        /// <summary> Cancel button </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary> X button </summary>
        public IButton XButton => new Button(this.Container, XButtonLocator);

        /// <summary> Expand Selected Title button </summary>
        public IButton ExpandSelectedTitleButton => new Button(this.Container, ExpandSelectedTitlesButtonLocator);

        /// <summary>
        /// Expand content types for titles column
        /// </summary>
        /// <returns>Filter By Title Dialog with expanded content types</returns>
        public FilterByTitleDialog ExpandContentTypesForTitles()
        {
            DriverExtensions.GetElements(this.Container, TitleAreaLocator, ExpandContentTypeButtonLocator).ToList().ForEach(
                el => DriverExtensions.Click(this.Container, TitleAreaLocator, ExpandContentTypeButtonLocator));
            return new FilterByTitleDialog();
        }

        /// <summary>
        /// Search and select item by name
        /// </summary>
        /// <param name="itemName">Item to select</param>
        /// <returns> New instance of the page </returns>
        public override T SearchAndSelectItemByName<T>(string itemName)
        {
            this.SearchTextBox.Clear();
            this.SearchTextBox.SetText<T>(itemName);
            DriverExtensions.WaitForElementDisplayed(
                this.Container, TitleAreaLocator, 
                SafeXpath.BySafeXpath(AvailableItemLctMask, itemName));
            DriverExtensions.GetElement(
                this.Container,TitleAreaLocator, 
                SafeXpath.BySafeXpath(AvailableItemLctMask, itemName)).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The unselect item.
        /// </summary>
        /// <param name="itemName">name of item that need unselect</param>
        /// <returns>page instance</returns>
        public override T UnselectItem<T>(string itemName)
        {
            DriverExtensions.GetElement(this.Container,SelectionsAreaLocator, SafeXpath.BySafeXpath(AvailableItemLctMask, itemName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that selected items are displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise</returns>
        public bool AreSelectedItemsDisplayed() =>
            DriverExtensions.IsDisplayed(SelectedItemsLocator, 5);

        /// <summary>
        /// Get all selected titles for the specified content type from the Selections column
        /// </summary>
        /// <param name="contentType">Content type</param>
        /// <returns>List of selected titles</returns>
        public List<string> GetAllSelectedTitlesByContentType(string contentType) =>
            DriverExtensions.GetElements(this.Container,SelectionsAreaLocator,
                               SafeXpath.BySafeXpath(AvailableItemsForSpecifiedContentTypeLctMask, contentType),
                               CitationTitleLocator).Select(elem => elem.Text).ToList();
        

        /// <summary>
        /// Get highlighted terms text
        /// </summary>
        /// <returns>List of highlighted terms</returns>
        public List<string> GetHighlightedTermsText() =>
            DriverExtensions.GetElements(this.Container, HighlightedTermLocator)
                            .Select(t => t.Text).ToList();

        /// <summary>
        /// Get all disabled titles from the Titles column
        /// </summary>
        /// <returns>List of disabled titles</returns>
        public List<string> GetDisabledItems() =>
        DriverExtensions.GetElements(this.Container, TitleAreaLocator, DisabledItemsLocator)
                        .Select(elem => elem.Text).ToList();
    }
}