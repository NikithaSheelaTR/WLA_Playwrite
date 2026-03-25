namespace Framework.Common.UI.Products.Shared.Dialogs.Favorites
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Dialog that pops up when you have the welcome screen preference on
    /// </summary>
    public class AddToFavoritesDialog : BaseModuleRegressionDialog
    {
        private const string CheckboxGroupNameLctMask =
            "//div[@class='co_favoritesOrganizer_list']//label[text() ={0}]/input[@type='checkbox']";

        private static readonly By CheckboxLocator =
            By.XPath("//div[@class='co_favoritesOrganizer_list']//input[@type='checkbox']");

        private static readonly By CreateGroupButtonLocator = By.Id("createGroupLink");

        private static readonly By GroupListItemLocator =
            By.XPath("//div[@class='co_favoritesOrganizer_list']//li[@class='co_listItem']");

        private static readonly By SaveButtonLocator =
            By.XPath("//div[@id='co_favoritesOrganizerLightbox']//input[@value='Save'] | //button[@class='co_primaryBtn co_overlayBox_buttonSave' and text()='Save'] | //*[@id='coid_fav508_save']");

        private static readonly By SaveNewFavoriteGroupButtonLocator = By.XPath("//button[@id='co_foldering_favorites_createGroup_saveButton']");

        /// <summary>
        ///FavouritesDelete button
        /// </summary>
        public IButton MyFavouriteSaveButton => new Button(SaveNewFavoriteGroupButtonLocator);

        /// <summary>
        /// Click on the 'Create new group' link
        /// </summary>
        /// <returns> The <see cref="CreateFavoritesGroupDialog"/>. </returns>
        public CreateFavoritesGroupDialog ClickCreateNewGroup()
            => this.ClickElement<CreateFavoritesGroupDialog>(CreateGroupButtonLocator);

        /// <summary>
        /// Clicks Save button
        /// </summary>
        /// <typeparam name="T"> T type Page </typeparam>
        /// <returns> New page object </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Creating favorites group from add to favorites widget
        /// </summary>
        /// <param name="favoritesGroupName"> group name to create </param>
        public void CreateFavoritesGroup(string favoritesGroupName)
            => this.ClickCreateNewGroup().CreateGroup<AddToFavoritesDialog>(favoritesGroupName);

        /// <summary>
        /// Verify that the group is exist
        /// </summary>
        /// <param name="expectedGroup"> The expected Group. </param>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsGroupExist(string expectedGroup)
            => DriverExtensions.GetElements(GroupListItemLocator).Any(group => group.Text.Equals(expectedGroup));

        /// <summary>
        /// Select specific group from group list on the favorites dialog
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="favoriteGroupName"> group name </param>
        /// <returns> New instance of the page </returns>
        public T SaveToSpecificFavoritesGroup<T>(string favoriteGroupName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(CheckboxGroupNameLctMask, favoriteGroupName)).Click();
            return this.ClickElement<T>(SaveButtonLocator);
        }

        /// <summary>
        /// Unselect specific group in the Favorites widget
        /// </summary> <param name="selected"> The selected.
        /// </param>
        /// <param name="favoritesGroupName"> The favorites Group Name. </param>
        /// <returns> The <see cref="AddToFavoritesDialog"/>. </returns>
        public AddToFavoritesDialog SelectGroupByName(bool selected, params string[] favoritesGroupName)
        {
            DriverExtensions.GetElements(GroupListItemLocator)
                            .ToList()
                            .FindAll(checkbox => favoritesGroupName.Contains(checkbox.Text))
                            .ToList()
                            .ForEach(
                                checkbox =>
                                    DriverExtensions.GetElement(checkbox, By.TagName("input")).SetCheckbox(selected));
            return this;
        }

        /// <summary>
        /// Unselect all groups
        /// </summary>
        /// <returns> The <see cref="AddToFavoritesDialog"/>. </returns>
        public AddToFavoritesDialog UnselectAllGroups()
        {
            DriverExtensions.GetElements(CheckboxLocator).ToList().ForEach(checkbox => checkbox.SetCheckbox(false));
            return this;
        }
    }
}