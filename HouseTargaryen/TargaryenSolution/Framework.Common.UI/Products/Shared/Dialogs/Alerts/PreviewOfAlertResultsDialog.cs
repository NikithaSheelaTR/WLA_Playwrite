namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Items.ResultList;

    using OpenQA.Selenium;

    /// <summary>
    /// Preview Of WestClip Results Component
    /// </summary>
    public class PreviewOfAlertResultsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("co_search_alertPreviewClose");
        private static readonly By ResultsCountLocator = By.XPath("//span[starts-with(@id,'co_search_alert_preview')]");
        private static readonly By WordCountLocator = By.XPath(".//span[starts-with(normalize-space(text()),'Word')]");
        private static readonly By ResultsListItemLocator = By.XPath("//li[contains(@id,'cobalt_search_results_case')]");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Get Results Count
        /// </summary>
        /// <returns>results count</returns>
        public int GetResultsCount()
        {
            string results = DriverExtensions.GetText(ResultsCountLocator);
            int index = results.LastIndexOf(' ');
            results = results.Substring(index + 1);

            return results.ConvertCountToInt();
        }

        /// <summary>
        /// Get Results Word Count
        /// </summary>
        /// <returns> words count</returns>
        public List<int> GetResultsWordCount()
        {
            var wordCountList = new List<int>();
            IReadOnlyCollection<IWebElement> elements = DriverExtensions.GetElements(WordCountLocator);

            foreach (IWebElement el in elements)
            {
                string text = el.Text.Substring(el.Text.IndexOf(":", StringComparison.Ordinal) + 1);
                wordCountList.Add(text.ConvertCountToInt());
            }

            return wordCountList;
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