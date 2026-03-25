namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.PreviousInteractions
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Items.FolderedObject;
    using Framework.Common.UI.Raw.WestlawEdge.Models.PreviousInteractions;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Previously Foldered Dialog
    /// </summary>
    public class PreviouslyFolderedDialog : BasePreviouslyInteractionsDialog
    {
        private static readonly By ListOfPreviouslyFolderedItemsLocator =
                By.XPath("//li[@class = 'co_headnoteWidget']/ul");

        private static readonly By ListOfViewInFolderLinksLocator = By.XPath("//a[contains(text(), 'View in Folder')]");

        /// <summary>
        /// Get All Previously Foldered items
        /// </summary>
        /// <returns>The List Foldered Item Object Model.</returns>
        public List<FolderedObjectModel> GetAllDialogModels() =>
            DriverExtensions.GetElements(ListOfPreviouslyFolderedItemsLocator).Select(x => new FolderedObjectItem(x))
                            .Select(x => x.ToModel<FolderedObjectModel>()).ToList();

        /// <summary>
        /// Does 'View in Folder' link exist at item index
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns> True if link exist, false otherwise </returns>
        public bool IsViewInFolderLinkDisplayedForItem(int itemIndex) => DriverExtensions.GetElements(ListOfViewInFolderLinksLocator).ElementAt(itemIndex).Displayed;

        /// <summary>
        /// Click on 'View in Folder' link by index
        /// </summary>
        /// <param name="index"></param>
        public EdgeResearchOrganizerPage ClickOnViewInFolderLinkByIndex(int index) =>
            this.ClickElement<EdgeResearchOrganizerPage>(
                DriverExtensions.GetElements(ListOfViewInFolderLinksLocator).ElementAt(index));
    }
}
