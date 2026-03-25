namespace Framework.Common.UI.Products.Shared.Components.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Favorites;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Favorites Widget on homepage
    /// </summary>
    public class FavoritesComponent : BaseModuleRegressionComponent
    {
        private const string CheckboxLctMask = "//input[@name='checkbox-{0}']";

        private const string FavoritePageLinkLctMask =
            "//ul[contains(@class, 'co_favorites_listSub')]/li/div/label/a[contains(@class, 'co_foldering_frontpage_favorite') and text()={0}]";

        private static readonly By AddNewGroupToFavoritesLocator = By.Id("co_favorites_addGroupLink");

        private static readonly By DeleteLinkLocator = By.CssSelector("a.co_favoriteDel");

        private static readonly By DoneOrganizingLinkLocator = By.Id("co_foldering_favorites_doneEditLink");

        private static readonly By EnterFavoriteGroupNameTextBoxLocator = By.Id("co_foldering_favorites_createGroup_groupName");

        private static readonly By FavoritesComponantLocator = By.XPath("//h2[contains(@class,'co_genericBoxHeaderText')][normalize-space(.)='Favorites']");

        private static readonly By FavoriteRootLocator = By.Id("co_favorites_listRoot");

        private static readonly By FrequentlyUsedItemLastitemLocator =
            By.XPath("//ul[@class='co_favorites_listSub co_frequently_used_list']/li[5]/label/a");

        private static readonly By FrequentlyUsedListLocator =
            By.XPath("//div[@id='co_frequentlyUsed_listRoot']//ul/li");

        private static readonly By ListRootSelectedLocator =
            By.XPath("//ul[@id='co_favorites_listRoot']/li/descendant::span");

        private static readonly By MyFavoritesLinkLocator =
            By.XPath(
                "//ul[contains(@class, 'co_favorites_connector')]//a[contains(@class, 'co_foldering_frontpage_favorite')]");

        private static readonly By OrganizeLinkLocator = By.Id("co_foldering_favorites_editLink");

        private static readonly By SaveNewFavoriteGroupButtonLocator = By.Id("co_foldering_favorites_createGroup_saveButton");

        private static readonly By SearchButtonLocator = By.Id("searchButtonFavorites");

        private static readonly By SearchInputLocator = By.Id("searchInputIdFavorites");

        private static readonly By SelectAllLinkLocator = By.ClassName("co_foldering_favorites_selectAll");

        private static readonly By AddToFavoritesLinkLocator = By.XPath("//*[@id = 'co_foldering_categoryPage' and contains(.,'Add to') or contains(@oldtitle,'Ajouter aux')]");

        private static readonly By ManageButtonLocator = By.XPath("//*[@id='coid_website_favoritesWidget']//*[text()='Manage page']");

        private static readonly By AddGroupButtonLocator = By.Id("addGroupButtonId");

        private static readonly By GroupListItemLocator =
           By.XPath("//div[@class='co_favoritesOrganizer_list']//li[@class='co_listItem']");

        private static readonly By GroupNameInputLocator = By.XPath("//input[@type='text' and @maxlength='50' and preceding-sibling::label[1]='Group Name:']");

        private static readonly By SaveButtonLocator = By.XPath("//*[@aria-label='Save New Group'] | //button[contains(@id,'createGroup_saveButton')]");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FavoritesComponantLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap =>
            this.contentTypeMap =
                this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Adding new group to favorites from home page
        /// </summary>
        /// <param name="groupName">
        /// group name to add
        /// </param>
        public void AddNewGroupToFavorite(string groupName)
        {

            this.ManageButton.Click();
            DriverExtensions.Click(AddGroupButtonLocator);

            this.CreateGroup<AddToFavoritesDialog>(groupName);

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Clear All Check boxes on the Favorites component
        /// </summary>
        public void ClearAllCheckboxes()
        {
            DriverExtensions.SetCheckbox(false, SelectAllLinkLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on the Favorite Page on the Favorites component
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="favoritePageName"> Page Name </param>
        /// <returns> New Page Object </returns>
        public T ClickFavoritePage<T>(string favoritePageName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(FavoritePageLinkLctMask, favoritePageName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///     Click on last item of the frequently list items
        /// </summary>
        public void ClickLastItemOfFrequentlyUsedItem()
        {
            DriverExtensions.WaitForElement(FrequentlyUsedItemLastitemLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Delete All Items In Favorite Widget
        /// </summary>
        /// <returns> Favorites Widget </returns>
        public FavoritesComponent DeleteAllItemsInFavoriteWidget()
        {
            this.ClickOrganizeLink();
            this.DeleteAllItemsFromMyFavoritesSection();
            this.ClickDoneOrganizing();
            return this;
        }

        /// <summary>
        /// Expand Favorites component (if collapsed)
        /// </summary>
        public void ExpandFavoritesComponent()
        {
            if (!this.IsFavoritesComponentExpanded())
            {
                DriverExtensions.WaitForElement(FavoritesComponantLocator).Click();
            }
        }

        /// <summary>
        /// Gets all favorites from the SearchHomePage Favorites component and returns them as strings
        /// </summary>
        /// <returns> List of Favorites links </returns>
        public List<string> GetFavoritesList()
        {
            DriverExtensions.WaitForPageLoad();
            this.ExpandFavoritesComponent();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElements(MyFavoritesLinkLocator).Select(item => item.Text).ToList();
        }

        /// <summary>
        /// Get the list of Frequently used items
        /// </summary>
        /// <returns> List </returns>
        public List<string> GetFrequentlyUsedList() =>
            DriverExtensions.GetElements(FrequentlyUsedListLocator, By.XPath(".//label/a")).Select(row => row.Text).ToList();

        /// <summary>
        /// checks to see if the specific favorite group added
        /// </summary>
        /// <param name="groupName"> Group Name</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFavoriteGroupCreationSuccessful(string groupName) =>
            DriverExtensions.GetElements(ListRootSelectedLocator).Any(g => g.Text.Contains(groupName));

        /// <summary>
        /// Checks for the given favorites root type on the favorites list
        /// </summary>
        /// <param name="favoritesRoot"> Name of root</param>
        /// <returns>
        /// returns true if the favorites root is present 
        /// </returns>
        public bool IsFavoritesRootDisplayed(ContentType favoritesRoot) =>
            DriverExtensions.WaitForElement(FavoriteRootLocator).Text.Contains(this.ContentTypeMap[favoritesRoot].Text);

        /// <summary>
        /// Is the group present
        /// </summary>
        /// <param name="expectedGroup">The <see cref="string"/></param>
        /// <returns>True if group is presented</returns>
        public bool IsGroupDisplayed(string expectedGroup) => this.GetFavoritesList().FirstOrDefault(item => item.Equals(expectedGroup)) != null;

        /// <summary>
        /// Search Favorites
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="query"> Query to search </param>
        /// <returns> New Page Object </returns>
        public T SearchFavorites<T>(string query) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            IWebElement favoritesSearchInput = DriverExtensions.WaitForElementDisplayed(SearchInputLocator);
            favoritesSearchInput.SetTextField(query);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.Click(SearchButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select Checkbox on the Favorites component
        /// </summary>
        /// <param name="favoritesGroupName"> Group name </param>
        public void SelectCheckbox(string favoritesGroupName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(CheckboxLctMask, favoritesGroupName)))
                            .SetCheckbox(true);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Done Organizing Link
        /// </summary>
        private void ClickDoneOrganizing() => DriverExtensions.Click(DriverExtensions.WaitForElement(DoneOrganizingLinkLocator));

        /// <summary>
        /// Click Organize Link
        /// </summary>
        private void ClickOrganizeLink() => DriverExtensions.Click(DriverExtensions.WaitForElement(OrganizeLinkLocator));

        /// <summary>
        /// Click AddToFavoritesLinkLocator Link
        /// </summary>
        private void ClickFavoritesLink() => DriverExtensions.Click(DriverExtensions.WaitForElement(AddToFavoritesLinkLocator));


        /// <summary>
        /// The click Manage button.
        /// </summary>        
        public IButton ManageButton => new Button(ManageButtonLocator);

        /// <summary>
        /// Click Add Group Button
        /// </summary>
        /// <returns> The <see cref="CreateFavoritesGroupDialog"/>. </returns>
        public CreateFavoritesGroupDialog ClickAddGroupButton()
        {
            DriverExtensions.GetElement(AddGroupButtonLocator).Click();
            return new CreateFavoritesGroupDialog();
        }

        /// <summary>
        /// Verify that the group is exist
        /// </summary>
        /// <param name="expectedGroup"> The expected Group. </param>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsGroupExist(string expectedGroup)
            => DriverExtensions.GetElements(GroupListItemLocator).Any(group => group.Text.Equals(expectedGroup));


        /// <summary>
        /// Create new group
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="groupName"> The group Name. </param>
        /// <returns> The <see cref="AddToFavoritesDialog"/>. </returns>
        public T CreateGroup<T>(string groupName) where T : ICreatablePageObject
        {
            if (!IsGroupExist(groupName))
            {
                DriverExtensions.SetTextField(groupName, GroupNameInputLocator);
                DriverExtensions.WaitForJavaScript();
                DriverExtensions.Click(SaveButtonLocator);
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        private void DeleteAllItemsFromMyFavoritesSection()
        {
            while (this.IsDeleteLinkExist())
            {
                DriverExtensions.Click(DriverExtensions.GetElements(DeleteLinkLocator).First());
            }
        }

        private bool IsDeleteLinkExist() => DriverExtensions.GetElements(DeleteLinkLocator).Any();

        private bool IsFavoritesComponentExpanded()
            => DriverExtensions.GetAttribute("class", FavoritesComponantLocator).Contains("co_expandedState");

        /// <summary>
        /// Favourite Widget
        /// </summary>
        public IWebElement FavouriteWidget => DriverExtensions.GetElement(this.ComponentLocator);
    }
}