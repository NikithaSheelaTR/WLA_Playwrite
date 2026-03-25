namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Contains all elements and methods pertaining to the PreviewResultsDialog
    /// </summary>
    public class PreviewResultsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ResultsListItemLocator = By.XPath("//div[@class='co_searchContent']");

        private static readonly By CloseButtonLocator = By.Id("co_search_alertPreviewClose");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Verify if Preview Results Items are sorted by date in descending order
        /// </summary>
        /// <returns>true if Preview Results Items are sorted by date in descending order, false otherwise</returns>
        public bool AreItemsSortedByDate()
        {
            List<PreviewResultsItem> resultItems = this.GetAllPreviewResultsItems();
            List<DateTime> dateList = resultItems.Select(s => DateTime.Parse(s.Date)).ToList();
            return dateList.SequenceEqual(dateList.OrderByDescending(date => date));
        }

        /// <summary>
        /// Gets All Preview Results Items
        /// </summary>
        /// <returns>
        /// The List
        /// </returns>
        public List<PreviewResultsItem> GetAllPreviewResultsItems() =>
            DriverExtensions.GetElements(ResultsListItemLocator).ToList().Select(item => new PreviewResultsItem(item)).ToList();
    }
}