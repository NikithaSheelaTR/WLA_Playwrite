namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items.Facets;
    using Framework.Common.UI.Products.Shared.Models;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using Button = Elements.Buttons.Button;

    /// <summary>
    /// Describes Dialogs with available and selected options lists and which appear after clicking on the 'Select' button
    /// near the Law Firm, Attorney, Judge, Party, Topic on the Narrow pane
    /// </summary>
    public abstract class BaseAvailableAndSelectedOptionsListsDialog : BaseModuleRegressionDialog
    {
        private const string SelectedOptionsItemMask = ".//ul[contains(@id,'selected')]//div[@class='co_listItem']//*[contains(text(),{0})]";

        private const string AvailableItemLctMask = ".//*[contains(text(),{0})]";

        private static readonly By AvailableItemsLocator = By.XPath(".//li");

        private static readonly By AvailableItemsContainerLocator = By.XPath(".//ul[contains(@id,'availableOptions')]");

        private static readonly By TitleLocator = By.XPath(".//h3 | .//h2");

        private static readonly By SearchInputLocator = By.Id("co_facet_searchBoxInput");

        private static readonly By CloseButtonLocator = By.XPath(".//button[contains(@class,'co_overlayBox_closeButton')]");

        private static readonly By CancelLinkLocator = By.XPath(".//*[contains(@class,'Cancel')]");

        private static readonly By SelectedItemsContainerLocator = By.XPath("//div[@class='co_overlayBox_rightContent']/div/ul");

        private static readonly By DialogLabelLocator = By.XPath("(//div[@class='co_overlayBox_left' or @class='co_overlayBox_leftContent']//h3)[1]");

        private static readonly By DialogYourSelectionLabelLocator = By.XPath("(//div[@class='co_overlayBox_left' or @class='co_overlayBox_leftContent']//h3)[2]");

        private static readonly By SearchLinkLocator = By.Id("co_lpaSearchLink");

        private static readonly By SelectedItemCountLocator = By.XPath("./span[@class='count']");

        private static readonly By ContinueButtonLocator = By.XPath(".//*[contains(@id,'continueButton')]");

        private static readonly By FilterButtonLocator = By.XPath(".//*[contains(@id,'co_facet_') and contains(@id,'_filterButton')]");

        private static readonly By PrimaryButtonLocator = By.XPath(".//*[@class='co_primaryBtn']");

        /// <summary>
        /// Element Container of dialog
        /// </summary>
        protected abstract IWebElement Container { get; }

        /// <summary>
        /// Filter Button 
        /// </summary>
        public Button FilterButton => new Button(FilterButtonLocator);

        /// <summary>
        /// Click on the Continue button in dialog
        /// </summary>
        /// <typeparam name="T"> Page type</typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(this.Container, ContinueButtonLocator);

        /// <summary>
        /// Click on the Filter button in NotesOfDecisionsTopics dialog
        /// </summary>
        /// <typeparam name="T"> Page type</typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickFilterButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(this.Container, FilterButtonLocator);

        /// <summary>
        /// Primary button in NotesOfDecisionsTopics dialog
        /// </summary>
        public IButton PrimaryButton => new Button(this.Container, PrimaryButtonLocator);

        /// <summary>
        /// The select item.
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="itemName">name of item</param>
        /// <returns>new T page instance</returns>
        public virtual T SearchAndSelectItemByName<T>(string itemName) where T : BaseAvailableAndSelectedOptionsListsDialog
        {
            this.SetTextToSearchInput<T>(itemName);
            DriverExtensions.WaitForElementDisplayed(
                this.Container,
                SafeXpath.BySafeXpath(AvailableItemLctMask, itemName));
            DriverExtensions.GetElement(this.Container, SafeXpath.BySafeXpath(AvailableItemLctMask, itemName))
                             .Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Selects a item
        /// </summary>
        /// <param name="item"> item to select </param>
        public void SelectItem(string item)
        {
            DriverExtensions.GetElement(this.Container, SafeXpath.BySafeXpath(AvailableItemLctMask, item)).Click();
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SelectedOptionsItemMask, item));
        }

        /// <summary>
        /// The get available items list.
        /// </summary>
        /// <returns>
        /// T
        /// </returns>
        public virtual List<string> GetAvailableItemsTextList() =>
            DriverExtensions.GetElements(AvailableItemsContainerLocator, AvailableItemsLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get dialog option item with specific name
        /// </summary>
        /// <param name="name">
        /// specific name
        /// </param>
        /// <returns>
        /// The <see cref="DialogOptionItemModel"/>.
        /// </returns>
        public DialogOptionItemModel GetOptionByName(string name) => this.GetDialogByNameItem(name).ToModel<DialogOptionItemModel>();

        /// <summary>
        /// The is continue button enabled.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsContinueButtonEnabled() => DriverExtensions.WaitForElement(this.Container, ContinueButtonLocator).Enabled;

        /// <summary>
        /// The is filter button enabled.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFilterButtonEnabled() => DriverExtensions.WaitForElement(this.Container, FilterButtonLocator).Enabled;

        /// <summary>
        /// The get title.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTitle() => DriverExtensions.GetElement(this.Container, TitleLocator).Text;

        /// <summary>
        /// The set text to search input.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="text">text</param>
        /// <returns>new T page instance</returns>
        public T SetTextToSearchInput<T>(string text) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(this.Container, SearchInputLocator).SetTextField(text);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///  The click cancel link.
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <returns>new T page instance</returns>
        public T ClickCancelLink<T>() where T : ICreatablePageObject
        {
            this.ClickElement(DriverExtensions.WaitForElement(this.Container, CancelLinkLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click close button.
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <returns>new T page instance</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject
        {
            this.ClickElement(DriverExtensions.WaitForElement(this.Container, CloseButtonLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The unselect item.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="itemName">name of item that need unselect</param>
        /// <returns>page instance</returns>
        public virtual T UnselectItem<T>(string itemName) where T : BaseAvailableAndSelectedOptionsListsDialog
        {
            DriverExtensions.WaitForElement(this.Container, SafeXpath.BySafeXpath(SelectedOptionsItemMask, itemName))
                            .Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that available items are displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise</returns>
        public bool AreAvailableItemsDisplayed()
            => DriverExtensions.IsDisplayed(AvailableItemsContainerLocator, 5);

        /// <summary>
        /// Verify that available items are enabled
        /// </summary>
        /// <returns> True if available options component is displayed, false otherwise </returns>
        public bool AreAvailableItemsEnabled() => DriverExtensions.IsEnabled(AvailableItemsContainerLocator);

        /// <summary>
        /// Determines if the item is selected
        /// </summary>
        /// <param name="publication"> Publication to look for </param>
        /// <returns> True if it selected, false otherwise </returns>
        public bool IsItemSelected(string publication)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(SelectedOptionsItemMask, publication));

        /// <summary>
        /// Verify dialog search text box is displayed
        /// </summary>
        /// <returns> True if search text box is displayed, false otherwise</returns>
        public bool IsDialogSearchInputDisplayed()
            => DriverExtensions.IsDisplayed(SearchInputLocator, 5);

        /// <summary>
        /// Verify if Selected items are displayed
        /// </summary>
        /// <returns> True if selected options topic is displayed, false otherwise </returns>
        public bool IsSelectedItemsDisplayed()
            => DriverExtensions.IsDisplayed(SelectedItemsContainerLocator, 5);

        /// <summary>
        /// Get count of selected item
        /// </summary>
        /// <param name="itemName">name of selected item</param>
        /// <returns>count og selected item</returns>
        public int GetSelectedItemCount(string itemName)
        {
            IWebElement selectedItemElement =
                DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(SelectedOptionsItemMask, itemName));
            return DriverExtensions.GetElement(selectedItemElement, SelectedItemCountLocator).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Get all selected items in dialog
        /// </summary>
        /// <returns>List of elements for each link in the selected options section.</returns>
        public List<string> GetAllSelectedItems() =>
            DriverExtensions.WaitForElementDisplayed(SelectedItemsContainerLocator).FindElements(By.XPath(".//li"))
                            .Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get all the options in the dialog
        /// </summary>
        /// <returns> Dialog items list </returns>
        public List<DialogOptionItem> GetAllDialogItems() => DriverExtensions.WaitForElement(this.Container, AvailableItemsContainerLocator).FindElements(By.XPath(".//li")).Select(elem => new DialogOptionItem(elem)).ToList();

        /// <summary>
        /// Is continue Button displayed
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.WaitForElement(this.Container, ContinueButtonLocator).IsDisplayed();

        /// <summary>
        /// Is Filter Button displayed
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsFilterButtonDisplayed() => DriverExtensions.WaitForElement(this.Container, FilterButtonLocator).IsDisplayed();

        /// <summary>
        /// Is Cancel button/link displayed
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsCancelLinkDisplayed() => DriverExtensions.WaitForElement(this.Container, CancelLinkLocator).IsDisplayed();

        /// <summary>
        /// Is Close Button displayed
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.WaitForElement(this.Container, CloseButtonLocator).IsDisplayed();

        /// <summary>
        /// Verify dialog label is displayed
        /// </summary>
        /// <returns> True if label is displayed, false otherwise</returns>
        public bool IsLabelDisplayed()
            => DriverExtensions.IsDisplayed(DialogLabelLocator, 5);

        /// <summary>
        /// Verify dialog 'Your Selection' label is displayed
        /// </summary>
        /// <returns> True if 'Your Selection' label is displayed, false otherwise</returns>
        public bool IsYourSelectionLabelDisplayed()
            => DriverExtensions.IsDisplayed(DialogYourSelectionLabelLocator, 5);

        /// <summary>
        /// Verify dialog title is displayed
        /// </summary>
        /// <returns> True if title is displayed, false otherwise</returns>
        public bool IsTitleDisplayed()
            => DriverExtensions.GetElement(this.Container, TitleLocator).Displayed;

        /// <summary>
        /// Enter text and click facet search link.
        /// </summary>
        /// <typeparam name="T">T page</typeparam>
        /// <param name="itemName">name of item</param>
        /// <returns>new T page instance</returns>
        protected T EnterTextAndClickFacetSearchLink<T>(string itemName) where T : ICreatablePageObject
        {
            this.SetTextToSearchInput<T>(itemName);
            DriverExtensions.Click(SearchLinkLocator, By.TagName("a"));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get dialog option item with specific name
        /// </summary>
        /// <param name="name">
        /// specific name
        /// </param>
        /// <returns>
        /// The <see cref="DialogOptionItem"/>.
        /// </returns>
        private DialogOptionItem GetDialogByNameItem(string name) => this.GetAllDialogItems().Find(o => o.Name.Equals(name));
    }
}