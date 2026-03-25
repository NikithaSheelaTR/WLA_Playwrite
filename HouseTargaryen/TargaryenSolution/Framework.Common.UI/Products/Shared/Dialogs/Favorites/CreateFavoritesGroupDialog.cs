namespace Framework.Common.UI.Products.Shared.Dialogs.Favorites
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Dialog that pops up when you have the welcome screen preference on
    /// </summary>
    public class CreateFavoritesGroupDialog : BaseModuleRegressionDialog
    {
        private static readonly By GroupNameInputLocator = By.XPath("//input[@type='text' and @maxlength='50' and preceding-sibling::label[1]='Group Name:']");

        private static readonly By SaveButtonLocator = By.XPath("//*[@aria-label='Save New Group'] | //button[contains(@id,'createGroup_saveButton')]");

        private static readonly By GroupListItemLocator =
           By.XPath("//div[@class='co_favoritesOrganizer_list']//li[@class='co_listItem']");

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
                return this.ClickElement<T>(SaveButtonLocator);
            }
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}