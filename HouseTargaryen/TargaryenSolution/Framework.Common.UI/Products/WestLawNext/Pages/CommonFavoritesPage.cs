namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;   

    /// <summary>
    /// CommonFavoritesPage
    /// </summary>
    public class CommonFavoritesPage : CommonAuthenticatedWestlawNextPage, ICommonFavoritesPage
    {
        private const string DeleteGroupButtonLctMask = "//a[contains(@class, 'co_favoriteDel') and contains(@class, 'co_secondaryBtn')][./span[text()=\"{0}\"] | //span[contains(@class, '__groupTitle') and text()=\"{0}\"]";

        private static readonly By DoneButtonLocator = By.XPath("//*[@id='coid_website_favoritesWidget']//*[text()='Done']");

        private static readonly By OrganizeButtonLocator = By.XPath("//*[@id='coid_website_favoritesWidget']//button[text()='Organize']");

        private static readonly By ManageButtonLocator = By.XPath("//*[@id='coid_website_favoritesWidget']//*[text()='Manage page']");

        private static readonly By CopyLinkLocator = By.Id("co_linkBuilder");

        private static readonly By MyFavoritesMenuButtonLocator = By.XPath("//*[@class='a11yDropdown']/button[@aria-label='My Favorites']");

        private static readonly By RegFavoritesMenuButtonLocator = By.XPath("//*[@class='a11yDropdown']/button[@aria-label='RegFavorites']");

        private static readonly By ConfirmButtonLocator = By.XPath("//button[text()='Confirm']");
        private static readonly By MyFavoritesDeleteLocator = By.XPath("//span[text()='My Favorites']/ancestor::li[contains(@class, 'organizer_group')]//li[@id='coid_kebabMenu_delete']");

        /// <summary>
        /// The click done button.
        /// </summary>
        public void ClickDoneButton() => DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(DoneButtonLocator));

        /// <summary>
        /// The click organize button.
        /// </summary>
        public void ClickOrganizeButton() => DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(OrganizeButtonLocator));

        /// <summary>
        /// The click Manage button.
        /// </summary>        
        public IButton ManageButton => new Button(ManageButtonLocator);

        /// <summary>
        ///Confirm button
        /// </summary>
        public IButton ConfirmButton => new Button(ConfirmButtonLocator);

        /// <summary>
        ///Menu button
        /// </summary>
        public IButton MyFavouritesMenuButton => new Button(MyFavoritesMenuButtonLocator);

        /// <summary>
        ///RegFavorites button
        /// </summary>
        public IButton RegFavoritesMenuButton => new Button(RegFavoritesMenuButtonLocator);

        /// <summary>
        ///FavouritesDelete button
        /// </summary>
        public IButton MyFavouritesDeleteButton => new Button(MyFavoritesDeleteLocator);

        /// <summary>
        /// The delete group from favorites.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        public void DeleteGroupFromFavourites(string groupName)
        {
            By groupXpath = By.XPath(string.Format(DeleteGroupButtonLctMask, groupName));

            if (DriverExtensions.IsElementPresent(groupXpath, 3))
            {
                DriverExtensions.Click(groupXpath);
            }
        }        
    }
  }