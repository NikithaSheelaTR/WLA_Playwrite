namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Products.Shared.Components.CustomPage;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    

    using OpenQA.Selenium;

    /// <summary>
    /// Edit Assigned pages Page
    /// </summary>
    public class EditAssignedPagesPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By PageTitleLocator = By.Id("co_browsePageLabel");

        /// <summary>
        /// Contacts Component
        /// </summary>
        public ContactsComponent ContactsComponent => new ContactsComponent();

        /// <summary>
        /// Custom Page List Dropdown
        /// </summary>
        public CustomPageListDropdown CustomPageListDropdown => new CustomPageListDropdown();

        /// <summary>
        /// Get Page title text
        /// </summary>
        /// <returns>string</returns>
        public string GetPageTitleText() => DriverExtensions.WaitForElement(PageTitleLocator).Text;
    }
}