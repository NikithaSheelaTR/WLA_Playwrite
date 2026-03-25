namespace Framework.Common.UI.Products.Shared.Components.FolderHistory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The center research organizer section.
    /// </summary>
    public class FolderGridComponent : BaseGridComponent
    {
        // Purple color
        private const string ColorForSearchWithinTermString = "rgba(190, 190, 252, 1)";

        private const string ItemLctMask = "//tbody//tr//td[(@class='co_detailsTable_content'or @class='TableCell--Description') and .//*[contains(.,{0})]]";

        private static readonly By SearchWithinTermLocator = By.XPath("//span[@class='co_searchTerm co_keyword']");

        private static readonly By AddDescriptionLocator = By.XPath(".//button[@id = 'addDescLinkId'] | .//button[contains(@id,'addLink')]");

        private static readonly By ContentTypeSubHeaderLocator = By.ClassName("co_detailsTable_subHeader");

        private static readonly By CurrentFolderHeaderLocator = By.XPath("//div[contains(@class,'co_foldering_headerDetails')]");

        private static readonly By CurrentFolderTitleLocator = By.XPath(".//h1[contains(@class,'co_folderTitle')]");

        private static readonly By DescriptionLocator = By.XPath(".//div[contains(@class,'co_item_description_desc')]/span|.//div[contains(@class,'Foldering--NoteContainer')]/div");

        private static readonly By DialogLocator = By.ClassName("co_overlayBox_container");

        private static readonly By DocumentUrlLocator = By.XPath("//a[contains(@id,'cobalt_foldering_ro_item_name_')]");

        private static readonly By EditDescriptionItemLocator
            = By.XPath("//div[@class='co_item_description_desc co_showState']//button[@id='descriptionEditLinkId'] | .//button[contains(@id,'editLink')]");

        private static readonly By FolderingProgressLocator = By.Id("folderingProgress");

        private static readonly By KmExcludedLocator = By.Id("co_KmExcluded");

        private static readonly By NotificationMessageLocator = By.XPath("//*[@id='co_researchOrganizerNotification'] | (//div[@id='co_infoBox_message' and contains(@class,'success')]//div[contains(@class,'co_infoBox_message')])[last()]");

        private static readonly By InputDescriptionLocator = By.XPath(".//input[contains(@id,'descriptionWidget_')]");

        private static readonly By ItemsLocator = By.XPath("//tbody//tr[contains(@id, 'datatable-row') and .//td[not(contains(@class, 'co_detailsTable_subHeader'))]]");

        private static readonly By SaveDescriptionLocator = By.XPath(".//div[contains(@class, 'co_item_description_')]//button[text()='Save']");

        private static readonly By ContainerLocator = By.Id("co_researchOrg_detailsContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Add description to select folder via the header 
        /// </summary>
        /// <param name="description">
        /// Header description
        /// </param>
        public void AddDescriptionToFolderHeader(string description) =>
            this.AddOrEditDescription(DriverExtensions.WaitForElement(CurrentFolderHeaderLocator), AddDescriptionLocator, description);

        /// <summary>
        /// Add description to a grid Item indicated by it's name
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <param name="description">
        /// Text for description
        /// </param>
        public void AddDescriptionToItemByName(string itemName, string description) =>
            this.AddOrEditDescription(DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItemLctMask, itemName)), AddDescriptionLocator, description);

        /// <summary>
        /// Add description to a grid item indicated by it's name 
        /// Applied for Folders and Items
        /// </summary>
        /// <param name="itemName">
        /// The Item name.
        /// </param>
        /// <param name="description">
        /// Text for Item Description
        /// </param>
        public void EditDescriptionForItemByName(string itemName, string description) =>
            this.AddOrEditDescription(DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItemLctMask, itemName)), EditDescriptionItemLocator, description);

        /// <summary>
        /// Edit description to select folder via the header 
        /// </summary>
        /// <param name="description">
        /// Text for Item Description
        /// </param>
        public void EditDescriptionToFolderHeader(string description) =>
            this.AddOrEditDescription(DriverExtensions.WaitForElementDisplayed(CurrentFolderHeaderLocator), EditDescriptionItemLocator, description);

        /// <summary>
        /// Returns guid list of the current folder
        /// </summary>
        /// <returns>The list of guids</returns>
        public List<string> GetDocumentsGuids()
        {
            this.FooterToolbar.SelectPerPageDropDownValue(ResultItemsPerPage.OneHundred);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElements(DocumentUrlLocator).Select(document => Regex.Match(document.GetAttribute("href"), @".*/?(Document)/(?<searchId>.+?)/.*$", RegexOptions.IgnoreCase).Groups["searchId"].Value).ToList();
        }

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public List<FolderGridModel> GetGridDocuments() =>
            this.GetGridModels().Where(e => e.Type == GridType.Document).ToList();

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public virtual List<FolderGridModel> GetGridModels() =>
            this.IsGridEmpty()
                ? new List<FolderGridModel>()
                : DriverExtensions.GetElements(ItemsLocator).Select(item => new FolderGridItem(item).ToModel<FolderGridModel>()).ToList();

        /// <summary>
        /// Verify that search within term is highlighted
        /// </summary>
        /// <param name="searchTerm"> Term to search </param>
        /// <returns> True if term highlighted, false otherwise </returns>
        public bool IsSearchWithinTermHighlighted(string searchTerm)
            => DriverExtensions.GetElements(ItemsLocator, SearchWithinTermLocator).Where(elem => elem.Text.Contains(searchTerm))
                .Any(elem => elem.GetCssValue("background-color").Equals(ColorForSearchWithinTermString));

        /// <summary>
        /// Retrieve the description of the header
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHeaderDescription() =>
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(CurrentFolderHeaderLocator), DescriptionLocator).GetText();

        /// <summary>
        /// Retrieves grid item's description, the item is indicated by the it's name
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetItemDescriptionByName(string itemName) =>
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItemLctMask, itemName)), DescriptionLocator).Text;

        /// <summary>
        /// Get the message after search within is applied to a folder containing KM document(s)
        /// </summary>
        /// <returns> The <see cref="string"/></returns>
        public string GetKmExcludedSearchMessage() => DriverExtensions.GetText(KmExcludedLocator);

        /// <summary>
        /// Get the list of all content type sub headers displayed on the search within results grid
        /// </summary>
        /// <returns>list of sub headers</returns>
        public List<ContentType> GetListofContentTypeSubHeaders() =>
            DriverExtensions.GetElements(ContentTypeSubHeaderLocator).Select(row => row.Text.RetainText().GetEnumValueByText<ContentType>()).ToList();

        /// <summary>
        /// Gets the notification messages
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNotificationMessage()
        {
            DriverExtensions.WaitForElementNotDisplayed(FolderingProgressLocator);
            DriverExtensions.WaitForElementNotDisplayed(DialogLocator);
            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.WaitForElementDisplayed(NotificationMessageLocator).Text;
        }

        /// <summary>
        /// Get Selected folder name
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSelectedFolderName() =>
            DriverExtensions.WaitForElement(
                DriverExtensions.WaitForElement(CurrentFolderHeaderLocator),
                CurrentFolderTitleLocator).GetText();

        /// <summary>
        /// Check if the title of a folder or folder item is in the Folder Grid 
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <returns>
        /// true if the grid has the grid with given item name.
        /// </returns>
        public override bool IsItemDisplayed(string itemName)
            => this.GetGridModels().Any(item => item.Title.StartsWith(itemName));

        /// <summary>
        /// The is item listed in folder.
        /// </summary>
        /// <param name="itemName">
        /// The _sub heading.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsItemInSelectedFolder(string itemName) =>
            DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ItemLctMask, itemName));

        private void AddOrEditDescription(IWebElement element, By locator, string description)
        {
            element.Hover();
            DriverExtensions.Click(DriverExtensions.WaitForElement(element, locator));
            DriverExtensions.WaitForElement(element, InputDescriptionLocator).SetTextField(description);
            DriverExtensions.WaitForElement(element, SaveDescriptionLocator).Click();

            BrowserPool.CurrentBrowser.Refresh();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }
    }
}