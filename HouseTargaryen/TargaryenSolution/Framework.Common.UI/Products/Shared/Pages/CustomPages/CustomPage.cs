namespace Framework.Common.UI.Products.Shared.Pages.CustomPages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.CustomPage;
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.HomePage;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Page
    /// </summary>
    public class CustomPage : CommonBrowsePage
    {
        private static readonly By AllItemsSelectedByDefaultCheckboxLocator = By.Id("cp_all_selected_checkbox");

        private static readonly By ClearAllLinkLocator = By.Id("cp_clearAll");

        private static readonly By ContentSectionsLocator =
            By.XPath("//div[contains(@class, 'co_column') and not(contains(@class, 'cp_column_last'))]/div[contains(@class,'cp_section')]");

        private static readonly By ContentSelectionCheckboxLocator = By.CssSelector("input.cp_search_selector");

        private static readonly By ContentListItemLocator = By.CssSelector("span.cp_linkText");

        private static readonly By EditNameLinkLocator = By.Id("cp_renamePage");

        private static readonly By EnterFreeZoneButtonLocator = By.Id("cp_enterFreeZone_button");

        private static readonly By ExitFreeZoneButtonLocator = By.Id("cp_exitFreeZone_button");

        private static readonly By SaveChangesButtonLocator = By.Id("cp_saveChanges_button");

        private static readonly By SelectAllLinkLocator = By.Id("cp_selectAll");

        private static readonly By BackToTestingCustomPageLocator = By.LinkText("Back to Testing custom page");

        private EnumPropertyMapper<CustomPageTools, WebElementInfo> toolsMap;

        /// <summary>
        /// Gets List of Content Section Widgets
        /// </summary>
        public IList<ContentSectionComponent> ContentSections
        {
            get
            {
                DriverExtensions.WaitForElementDisplayed(ContentSectionsLocator);
                return DriverExtensions.GetElements(ContentSectionsLocator).Select(elem => new ContentSectionComponent(elem.GetCssLocator())).ToList();
            }
        }

        /// <summary>
        /// Favorites Widget on the right hand side
        /// </summary>
        public FavoritesComponent FavoritesWidget => new FavoritesComponent();

        /// <summary>
        /// Code of Federal Regulations widget in Tools section
        /// </summary>
        public CodeOfFederalRegulationsComponent CodeOfFederalRegulationsComponent => new CodeOfFederalRegulationsComponent();

        /// <summary>
        /// Hot documents component to the right of the News and Insight container
        /// </summary>
        public HotDocumentsComponent HotDocumentsComponent => new HotDocumentsComponent();

        /// <summary>
        /// Gets Manage Page Drop Down
        /// </summary>
        public ManagePageDropdown ManagePageDropDown => new ManagePageDropdown();

        /// <summary>
        /// Gets Manage Page Drop Down
        /// </summary>
        public CustomPageListDropdown CustomPageListDropdown => new CustomPageListDropdown();

        /// <summary>
        /// Back To Testing Custom Page Link
        /// </summary>
        public ILink BackToTestingCustomPageLink => new Link(BackToTestingCustomPageLocator);

        /// <summary>
        /// Gets the CustomPageTools enumeration to Framework.Common.UI.Products.Shared.Models.EnumProperties.WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<CustomPageTools, WebElementInfo> ToolsMap
            => this.toolsMap = this.toolsMap ?? EnumPropertyModelCache.GetMap<CustomPageTools, WebElementInfo>();

        /// <summary>
        /// Click Activate Non Billable Zone Button
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ActivateNonBillableZone<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EnterFreeZoneButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Activate Non-Billable Zone Button Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsActivateNonBillableZoneButtonDisplayed()
            => DriverExtensions.IsDisplayed(EnterFreeZoneButtonLocator) || DriverExtensions.IsDisplayed(ExitFreeZoneButtonLocator);

        /// <summary>
        /// Click Toggle To Billable Client Id Button
        /// </summary>
        /// <returns>New instance of ChangeClientIdDialog</returns>
        public ChangeClientIdDialog ToggleToBillableClientId()
        {
            DriverExtensions.WaitForElement(ExitFreeZoneButtonLocator).Click();
            return new ChangeClientIdDialog();
        }

        /// <summary>
        /// Add Categories To Specified Content Section
        /// </summary>
        /// <param name="sectionName">The section Name.</param>
        /// <param name="pathToSelectCategories">The path To Select Categories.</param>
        /// <param name="categoriesToAdd">The categories To Add.</param>
        /// <returns>The <see cref="CustomPage"/>.</returns>
        public CustomPage AddCategoriesToContentSection(string sectionName, string pathToSelectCategories, params string[] categoriesToAdd)
            => this.GetContentSectionComponent(sectionName)?.EditContentByPath<CustomPage>(pathToSelectCategories, categoriesToAdd);

        /// <summary>
        /// Add Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="pathToSelectCategories">The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="categoriesList">Category Pages List</param>
        /// <returns>Custom Page</returns>
        public CustomPage AddContentSection(string sectionName, string pathToSelectCategories, IList<string> categoriesList)
            => this.GetAddedContentSectionComponent(sectionName)?.AddContentByPath<CustomPage>(pathToSelectCategories, categoriesList);

        /// <summary>
        /// Add Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <returns>Custom Page</returns>
        public CustomPage AddContentSection(string sectionName)
            => this.ManagePageDropDown.SelectOption<CreateNewContentSectionDialog>(ManagePageDropdownOptions.AddContentSection).AddSection(sectionName);

        /// <summary>
        /// Add Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <returns>Custom Page</returns>
        public AddContentToContentSectionDialog AddSectionAndClickAddContent(string sectionName)
            => this.GetAddedContentSectionComponent(sectionName).AddContentButton.Click<AddContentToContentSectionDialog>();

        /// <summary>
        /// Add Content Section
        /// The key in <see cref="KeyValuePair{TKey,TValue}"/> specifies whether current category should be checked or should be clicked as link.
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="path">The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="contentList">The Content List</param>
        /// <returns>The <see cref="CustomPage"/>.</returns>
        public CustomPage AddContentSection(string sectionName, string path, IList<KeyValuePair<WebElementType, string>> contentList)
            => this.GetAddedContentSectionComponent(sectionName)?.AddContent<CustomPage>(path, contentList);

        /// <summary>
        /// Add Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="pathToSelectCategories">The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="countOfCategoriesToSelect">Count of category pages to select</param>
        /// <returns>The <see cref="CustomPage"/>.</returns>
        public CustomPage AddContentSection(string sectionName, string pathToSelectCategories, int countOfCategoriesToSelect)
            => this.GetAddedContentSectionComponent(sectionName)?.AddContentByPath<CustomPage>(pathToSelectCategories, countOfCategoriesToSelect);

        /// <summary>
        /// Add Cui Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="url">URL with Category Pages</param>
        /// <returns>Custom Page</returns>
        public CustomPage AddCuiContentSection(string sectionName, string url)
        {
            this.GetAddedContentSectionComponent(sectionName)?.AddCuiContent(url);
            return new CustomPage();
        }

        /// <summary>
        /// Add Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="categoryPageName">Category Page Name</param>
        /// <returns>Custom Page</returns>
        public CustomPage AddContentSection(string sectionName, params string[] categoryPageName)
            => this.GetAddedContentSectionComponent(sectionName)?.AddContent<CustomPage>(categoryPageName);

        /// <summary>
        /// Check whether Content Pages are selected
        /// </summary>
        /// <returns>true if checkbox is checked</returns>
        public bool AreAllContentSectionsChecked()
            => DriverExtensions.GetElements(ContentSelectionCheckboxLocator).All(e => e.Selected);

        /// <summary>
        /// Returns Content Section with specified name
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <returns>The Content Section Component.</returns>
        public ContentSectionComponent GetContentSectionByName(string sectionName)
            => this.ContentSections.FirstOrDefault(sec => sec.Title == sectionName);

        /// <summary>
        /// Delete Custom Page
        /// </summary>
        /// <typeparam name="TPage">TPage</typeparam>
        /// <returns>The new instance of T page</returns>
        public TPage DeleteCustomPage<TPage>() where TPage : ICreatablePageObject
        {
            this.ScrollPageToTop();
            return this.ManagePageDropDown.SelectOption<DeleteCustomPageDialog>(ManagePageDropdownOptions.DeletePage).ClickYesButton<TPage>();
        }

        /// <summary>
        /// Edit Content Section
        /// </summary>
        /// <param name="sectionName"> Section Name </param>
        /// <param name="categoriesToDelete"> The categories To Delete. </param>
        /// <returns> Custom Page </returns>
        public CustomPage DeleteContentFromSection(string sectionName, params string[] categoriesToDelete)
            => this.GetContentSectionComponent(sectionName)?.RemoveContent<CustomPage>(categoriesToDelete);

        /// <summary>
        /// Edit Content Section
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <param name="pathToSelectCategories">Special content to choose category pages from</param>
        /// <param name="countOfCategoriesToSelect">Count of category pages to select</param>
        /// <param name="uncheckPreviouslySelected">Specify if previously selected items should be removed firstly</param>
        /// <returns>Custom Page</returns>
        public CustomPage EditContentSection(
            string sectionName,
            string pathToSelectCategories,
            int countOfCategoriesToSelect,
            bool uncheckPreviouslySelected = false)
            => this.GetContentSectionComponent(sectionName)?.EditContentByPath<CustomPage>(pathToSelectCategories, countOfCategoriesToSelect);

        /// <summary>
        /// Check if All Items Selected by Default checkbox is checked
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAllItemsSelectedByDefaultCheckboxChecked()
            => DriverExtensions.IsCheckboxSelected(AllItemsSelectedByDefaultCheckboxLocator);

        /// <summary>
        /// Check if All Items Selected by Default checkbox present
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreAllItemsSelectedByDefaultCheckboxDisplayed()
            => DriverExtensions.IsDisplayed(AllItemsSelectedByDefaultCheckboxLocator);

        /// <summary>
        /// Check/Uncheck All Items Selected by Default checkbox
        /// </summary>
        /// <param name="optionState">The option State</param>
        public void SetAllItemsSelectedByDefaultCheckbox(bool optionState = true)
            => DriverExtensions.SetCheckbox(AllItemsSelectedByDefaultCheckboxLocator, optionState);

        /// <summary>
        /// Click on Clear All link
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsClearAllLinkDisplayed() => DriverExtensions.IsDisplayed(ClearAllLinkLocator);

        /// <summary>
        /// Click on Clear All link
        /// </summary>
        /// <returns>The <see cref="CustomPage"/>.</returns>
        public CustomPage ClickClearAllLink()
        {
            DriverExtensions.WaitForElement(ClearAllLinkLocator).Click();
            return new CustomPage();
        }

        /// <summary>
        /// Click on Select All link
        /// </summary>
        /// <returns>The <see cref="CustomPage"/>.</returns>
        public CustomPage ClickSelectAllLink()
        {
            DriverExtensions.WaitForElement(SelectAllLinkLocator).Click();
            return new CustomPage();
        }

        /// <summary>
        /// Check content section checkbox
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="setTo">The set To.</param>
        public void SetContentSectionCheckboxByIndex(int index, bool setTo = true)
            => DriverExtensions.GetElements(ContentSelectionCheckboxLocator).ElementAt(index).SetCheckbox(true);

        /// <summary>
        /// Get a list of content items
        /// </summary>
        /// <returns>The List of content items.</returns>
        public IEnumerable<string> GetContentSectionItems()
            => DriverExtensions.GetElements(ContentListItemLocator).Select(el => el.Text);

        /// <summary>
        /// Click on Rename Page Link
        /// </summary>
        /// <returns>
        /// The <see cref="RenamePageDialog"/>.</returns>
        public RenamePageDialog ClickRenamePageLink()
        {
            DriverExtensions.WaitForElement(EditNameLinkLocator).Click();
            return new RenamePageDialog();
        }

        /// <summary>
        /// Click on Save Changes Button
        /// </summary>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public T ClickSaveChangesButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveChangesButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// This common method verifies that tools widgets are displayed on Custom Page
        /// </summary>
        /// <param name="tools">Tools for verifying on custom page</param>
        /// <returns>True if the widgets can be added and displayed on the custom page</returns>
        public bool IsToolsComponentDisplayed(params CustomPageTools[] tools)
            => tools.All(tool => DriverExtensions.IsDisplayed(By.XPath(this.ToolsMap[tool].LocatorString), 5));

        private ContentSectionComponent GetAddedContentSectionComponent(string sectionName)
            => this.AddContentSection(sectionName).GetContentSectionComponent(sectionName);

        private ContentSectionComponent GetContentSectionComponent(string sectionName)
            => this.ContentSections.FirstOrDefault(it => it.Title.Equals(sectionName));
    }
}