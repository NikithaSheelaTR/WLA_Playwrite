namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Dialogs.Favorites;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common Favorites Page
    /// </summary>
    public class EdgeCommonFavoritesPage : CommonFavoritesPage
    {
        private const string CheckboxesLctMask =
                   "//*[text()='{0}']/preceding-sibling::*[starts-with(@type,'checkbox')]";

        private const string FolderMask =
            "//h3[@class='co_favoritesGroupHeader co_genericBoxHeader'] /label /span[text()='{0}'] | //span[text()='{0}']";

        private static readonly By FavouritesLinkLocator = By.XPath(
            "//div[contains(@aria-label,'My Favourites')]//a");

        private static readonly By AddGroupButtonLocator = By.Id("addGroupButtonId");

        private static readonly By MyThesaurusMenuButtonLocator = By.XPath("//button[@aria-label='ThesaurusTest']");

        private static readonly By MyThesaurusDeleteLocator = By.XPath("//span[text()='ThesaurusTest']/ancestor::li[contains(@class, 'organizer_group')]//li[@id='coid_kebabMenu_delete']");


        /// <summary>
        /// List of Favourites Links in favourites page
        /// </summary>
        public IReadOnlyCollection<ILink> FavouriteLinks => new ElementsCollection<Link>(FavouritesLinkLocator);

        /// <summary>
        /// Favorites Widget
        /// </summary>
        public FavoritesComponent FavoritesWidget { get; set; } = new FavoritesComponent();        

        /// <summary>
        /// Gets Manage Page Drop Down
        /// </summary>
        public ContentSectionMenuDropdown ContentSectionMenuDropdown => new ContentSectionMenuDropdown();

        /// <summary>
        ///Menu button
        /// </summary>
        public IButton MyThreasusMenuButton => new Button(MyThesaurusMenuButtonLocator);

        /// <summary>
        ///FavouritesDelete button
        /// </summary>
        public IButton MyTherasusDeleteButton => new Button(MyThesaurusDeleteLocator);

        /// <summary>
        /// Select Item To Search
        /// </summary>
        /// <param name="optionName"> The item Name.  </param>
        /// <param name="state"> The state.  </param>
        /// <returns> The <see cref="EdgeCommonFavoritesPage"/>. </returns>
        public EdgeCommonFavoritesPage SelectItemToSearch(string optionName, bool state)
        {
            string optionString = string.Format(CheckboxesLctMask, optionName);
            DriverExtensions.SetCheckbox(By.XPath(optionString), state);
            return this;
        }

        /// <summary>
        /// Is Folder Created
        /// </summary>
        /// <param name="folderName">folder Name</param>
        /// <returns>true if present</returns>
        public bool IsFolderCreated(string folderName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(FolderMask, folderName)), 5);

        /// <summary>
        /// Click Add Group Button
        /// </summary>
        /// <returns> The <see cref="CreateFavoritesGroupDialog"/>. </returns>
        public CreateFavoritesGroupDialog ClickAddGroupButton()
        {
            DriverExtensions.GetElement(AddGroupButtonLocator).Click();
            return new CreateFavoritesGroupDialog();
        }
    }
}
