namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.PreviousInteractions
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.PreviousInteractions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Previously Viewed Dialog
    /// </summary>
    public class PreviouslyViewedDialog : BasePreviouslyInteractionsDialog
    {
        private static readonly By GoToHistoryLinkLocator = By.XPath("//*[contains(text(),'Go to History')]");

        private static readonly By ViewsTextLocator = By.XPath("//h4[text() = 'Views:']");

        private static readonly By ViewsItemsLocator =
            By.XPath("//li[@class='co_previousInteractions_view co_displayName_IRTX']");

        /// <summary>
        /// Click Go To History Link
        /// </summary>
        /// <returns>
        /// The <see cref="CommonHistoryPage"/>.
        /// </returns>
        public CommonHistoryPage ClickGoToHistoryLink() => this.ClickElement<CommonHistoryPage>(GoToHistoryLinkLocator);

        /// <summary>
        /// Get All Previously Viewed items
        /// </summary>
        /// <returns>The List of ViewItemObjectModel.</returns>
        public List<ViewedObjectModel> GetAllDialogItems() =>
            DriverExtensions.GetElements(ViewsItemsLocator).Select(x => new ViewedObjectItem(x))
                            .Select(x => x.ToModel<ViewedObjectModel>()).ToList();

        /// <summary>
        /// Is Views Text Displayed
        /// </summary>
        /// <returns>True if Views Text is displayed, otherwise - false</returns>
        public bool IsViewsTextDisplayed() => DriverExtensions.IsDisplayed(ViewsTextLocator);
    }
}