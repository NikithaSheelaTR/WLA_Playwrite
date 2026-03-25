namespace Framework.Common.UI.Products.Shared.Pages.CustomPages
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Pages Page
    /// </summary>
    public class ManageCustomPagesPage : CommonAuthenticatedWestlawNextPage
    {
        private const string CustomPageLinkLctMask = "//*[contains(@class, 'all_customPages_list') or contains(@class, 'CP-card-list')]//a[contains(text(),{0})]";
        private static readonly By OrganizeButtonLocator = By.CssSelector("#coid_customPageViewAll_organizeBtn, #coid_customPageViewAll_organizeButton");
        private static readonly By ContentListItemLocator = By.XPath("//ul[contains(@class, 'all_customPages_list')]/li");
        private static readonly By ContentListItemAlternatelocator = By.CssSelector("li > a[href*='CustomPages']:not(#co_frequentFavoritesViewCustomPagesLink):not(#coid_customPageViewAll_editAssignedPages)");
        private static readonly By EditAssignedPagesButtonLocator = By.Id("coid_customPageViewAll_editAssignedPages");
        private static readonly By HoverMessageLocator = By.XPath("//*[@class='co_defaultBtn co_disabled']");
        private static readonly By CreateNewCustomPageButtonLocator
            = By.XPath("//*[@id='coid_website_customPageViewAll_container']//*[contains(@id,'_createNew') and @class='co_defaultBtn']");

        /// <summary>
        /// Custom Page link
        /// </summary>
        public ILink CustomPageLink(string linkName) => new Link(By.XPath(string.Format(CustomPageLinkLctMask, linkName)));

        /// <summary>
        /// Click Organize button
        /// </summary>
        /// <returns>The <see cref="ManageCustomPagesPage"/>.</returns>
        public ManageCustomPagesPage ClickOrganizeButton()
        {
            DriverExtensions.WaitForElement(OrganizeButtonLocator).Click();
            return new ManageCustomPagesPage();
        }

        /// <summary>
        /// Click Create New Custom Page button
        /// </summary>
        /// <returns>The <see cref="CreateCustomPageDialog"/>.</returns>
        public CreateCustomPageDialog ClickCreateNewCustomPageButton()
        {
            DriverExtensions.WaitForElement(CreateNewCustomPageButtonLocator).Click();
            return new CreateCustomPageDialog();
        }

        /// <summary>
        /// Drag And Drop CustomPage
        /// </summary>
        /// <param name="dropTargetCustomPageName">
        /// Drop target Custom Page name
        /// </param>
        /// <param name="dragElementCustomPageName">
        /// Drag target Custom Page name
        /// </param>
        /// <returns>
        /// The <see cref="ManageCustomPagesPage"/>.
        /// </returns>
        public ManageCustomPagesPage DragAndDropCustomPage(string dropTargetCustomPageName, string dragElementCustomPageName)
        {
            DriverExtensions.DragAndDropWithOffset(
                DriverExtensions.GetElement(SafeXpath.BySafeXpath(CustomPageLinkLctMask, dropTargetCustomPageName)),
                DriverExtensions.GetElement(SafeXpath.BySafeXpath(CustomPageLinkLctMask, dragElementCustomPageName)),
                y: 10);

            return this;
        }

        /// <summary>
        /// Get List of Custom Pages Names 
        /// </summary>
        /// <returns> List of Names </returns>
        public List<string> GetCustomPagesNames()
            => DriverExtensions.GetElements(ContentListItemAlternatelocator).Select(item => item.Text).ToList();

        /// <summary>
        /// Click on Edit Assigned pages button
        /// </summary>
        /// <returns>
        /// The <see cref="EditAssignedPagesPage"/>.
        /// </returns>
        public EditAssignedPagesPage ClickEditAssignedPagesButton()
        {
            DriverExtensions.WaitForElement(EditAssignedPagesButtonLocator).Click();
            return new EditAssignedPagesPage();
        }

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(EditAssignedPagesButtonLocator);
            return DriverExtensions.IsDisplayed(HoverMessageLocator);
        }

        /// <summary>
        /// Get text from hover message
        /// </summary>
        /// <returns>Text from hover message</returns>
        public string GetHoverMessageText() => DriverExtensions.GetAttribute(
            "oldtitle",
            DriverExtensions.WaitForElement(HoverMessageLocator));

        /// <summary>
        /// Check is "Edit assigned page" button enabled
        /// </summary>
        /// <returns>
        /// True - if "Edit assigned page" button is enabled
        /// </returns>
        public bool IsEditAssignedPagesButtonEnabled() => !DriverExtensions.GetAttribute("class", DriverExtensions.WaitForElement(EditAssignedPagesButtonLocator)).Contains("co_disabled");
    }
}
