namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// CommonFavoritesPage interface
    /// </summary>
    public interface ICommonFavoritesPage : ICreatablePageObject
    {
        /// <summary>
        /// Verify that Copy Link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        bool IsCopyLinkDisplayed();

        /// <summary>
        /// The click done button.
        /// </summary>
        void ClickDoneButton();

        /// <summary>
        /// The click organize button.
        /// </summary>
        void ClickOrganizeButton();

        /// <summary>
        /// The delete group from favorites.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        void DeleteGroupFromFavourites(string groupName);
    }
}