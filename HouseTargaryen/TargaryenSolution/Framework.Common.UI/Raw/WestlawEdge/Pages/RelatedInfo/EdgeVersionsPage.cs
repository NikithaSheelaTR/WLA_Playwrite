namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages;
    using Framework.Common.UI.Products.WestlawEdge.Components.StatutesCompare;
    using Framework.Common.UI.Products.WestlawEdge.Components.StatutesHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Raw.WestlawEdge.Items.StatutesCompare;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Enums;

    /// <summary>
    /// The indigo versions page.
    /// </summary>
    public class EdgeVersionsPage : VersionsPage
    {
        private const string DocItemLocator = "//li[.//a[@guid={0}]]//button[@class='co_statuteCompare_addToCompareButton']";
        private static readonly By AddToCompareButtonLatestVersionLocator = By.XPath("//ul[@class='co_relatedInfo_orderedList']//button");
        private static readonly By PriorVersionsItemLocator = By.XPath("//div[h2[span[contains(text(),'Prior Version')]]]/ul/li");
        private static readonly By DocItemTitleLocator = By.XPath("//ul[contains(@class,'co_versionsList')]//a[contains(@id, 'documentLink')]");

        /// <summary>
        /// Compare Blackline Edits Widget
        /// </summary>
        public CompareBlacklineEditsComponent CompareBlacklineEditsComponent { get; set; } = new CompareBlacklineEditsComponent();
       
        /// <summary>
        /// Edge header component
        /// </summary>
        public new EdgeHeaderComponent Header { get; protected set; } = new EdgeHeaderComponent();

        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new StatuteHistoryToolbarComponent Toolbar { get; set; } = new StatuteHistoryToolbarComponent();

        /// <summary>
        /// Statute History Content Types Navigation Component
        /// </summary>
        public StatuteHistoryContentTypesNavigationComponent ContentType { get; set; } = new StatuteHistoryContentTypesNavigationComponent();

        /// <summary>
        /// Clicks Add To Compare button for the latest version.
        /// </summary>
        /// <returns> The <see cref="EdgeVersionsPage"/>.  </returns>
        public EdgeVersionsPage ClickAddToCompareButtonForLatestVersion()
        {
            DriverExtensions.WaitForElement(AddToCompareButtonLatestVersionLocator).Click();
            return this;
        }

        /// <summary>
        /// Clicks Add To Compare button for the prior version.
        /// </summary>
        /// <param name="itemNumber"> The button number. The first by default </param>
        /// <returns> The <see cref="EdgeVersionsPage"/>.  </returns>
        public EdgeVersionsPage ClickAddToCompareButtonForPriorVersion(int itemNumber = 0)
            => this.GetItemByNumber(itemNumber).AddToCompareButton.Click<EdgeVersionsPage>();

        /// <summary>
        /// Clicks Add To Compare button for doc.
        /// </summary>
        /// <param name="docGuid"> The doc number. The first by default </param>
        /// <returns> The <see cref="EdgeVersionsPage"/>.  </returns>
        public EdgeVersionsPage ClickAddToCompareButtonByDocIdForPriorVersion(string docGuid)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(DocItemLocator, docGuid)).Click();
            return this;
        }

        /// <summary>
        /// Verifies that Remove from compare button displayed for an item
        /// </summary>
        /// <param name="itemNumber">item number</param>
        /// <returns> True if displayed</returns>
        public bool IsRemoveFromCompareButtonDisplayed(int itemNumber) =>
            this.GetItemByNumber(itemNumber).RemoveFromCompareButton.Displayed;

        /// <summary>
        /// Verifies that all add to compare buttons are displayed. 
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all add to compare buttons are displayed. </returns>
        public bool IsAllAddToCompareButtonDisplayed()
            => this.GetItems().ToList().TrueForAll(item => item.AddToCompareButton.Displayed);

        /// <summary>
        /// Verifies that all add to compare buttons are enabled. 
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all add to compare buttons are enabled. </returns>
        public bool IsAllAddToCompareButtonEnabled() =>
            this.GetItems().ToList().Where(i => i.AddToCompareButton.Displayed).ToList()
                .TrueForAll(item => item.AddToCompareButton.Enabled);
        
        /// <summary>
        /// Clicks prior version document link.
        /// </summary>
        /// <param name="itemNumber"> The button number. The first by default </param>
        /// <returns> The <see cref="EdgeCommonDocumentPage"/>. </returns>
        public EdgeCommonDocumentPage ClickPriorVersionDocumentLink(int itemNumber = 0)
            => this.GetItemByNumber(itemNumber).PriorVersionsDocumentLink.Click<EdgeCommonDocumentPage>();

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="resultListItemNumber">The element to drag.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string DragAndDropTitleElementToRecentFolder(
            string targetFolder,
            int resultListItemNumber)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
              this.GetItemTitleElements().ElementAt(resultListItemNumber));

           this.DragAndDropToFolder(
                new EdgeRecentFoldersDialog().GetFolderElement(targetFolder),
                 this.GetItemTitleElements().ElementAt(resultListItemNumber), CopyOrMoveEnum.Move);

            return this.Header.GetInfoMessage();
        }

        /// <summary>
        /// Get item title elements
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private List<IWebElement> GetItemTitleElements()
            => DriverExtensions.GetElements(DocItemTitleLocator).ToList();

        /// <summary>
        /// The get items.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<PriorVersionsItem> GetItems()
            => DriverExtensions.GetElements(PriorVersionsItemLocator).Select(item => new PriorVersionsItem(item));

        /// <summary>
        /// Gets item by number.
        /// </summary>
        /// <param name="itemNumber"> The item Number. </param>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        private PriorVersionsItem GetItemByNumber(int itemNumber)
            => new PriorVersionsItem(DriverExtensions.GetElements(PriorVersionsItemLocator).ToList()[itemNumber]);
    }
}