namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Super Browse Page
    /// </summary>
    public abstract class BaseSuperBrowsePage : BaseModuleRegressionPage
    {
        private static readonly By ChapterTitleLocator = By.ClassName("co_search_header");

        private static readonly By RegulatorySectionLocator = By.ClassName("topicTitle");

        private static readonly By SelectAllCheckboxLocator = By.Id("coid_superBrowseSelectAllCheckboxInput");

        private static readonly By SubchapterTitleLocator = By.Id("co_browsePageLabel");

        private static readonly By SuperbrowseCheckboxLocator = By.XPath("//li[@class='co_section']/input[@type='checkbox']");

        private static readonly By SuperbrowseCheckedItemsCountLocator = By.XPath("//span[@id='co_itemsSelected'] /strong");

        private static readonly By SuperBrowseContentLocator = By.Id("co_contentColumn");

        private static readonly By SuperbrowseTitlesLocator = By.XPath("//h3[@class='topicTitle']");

        /// <summary>
        /// Chunk Navigation Component
        /// </summary>
        public ChunkNavigationComponent ChunkNavigation { get; } = new ChunkNavigationComponent();

        /// <summary>
        /// Check items on a page
        /// </summary>
        /// <param name="searchResultItems"> The search Result Items. </param>
        public void CheckItemsByIndex(Dictionary<int, bool> searchResultItems)
        {
            IList<IWebElement> checkboxes = DriverExtensions.GetElements(new[] { SuperbrowseCheckboxLocator });
            foreach (KeyValuePair<int, bool> item in searchResultItems)
            {
                checkboxes[item.Key].SetCheckbox(item.Value);
            }
        }

        /// <summary>
        /// Check 'Select all' checkbox
        /// </summary>
        public void CheckSelectAllCheckbox() => DriverExtensions.WaitForElement(SelectAllCheckboxLocator).SetCheckbox(true);

        /// <summary>
        /// Get SB Delivery One Item Title
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>title</returns>
        public string GetDeliveryItemTitleByIndex(int index)
        {
            string text = DriverExtensions.GetElements(new[] { SuperbrowseTitlesLocator })[index].Text;
            string title =
                $"{text.Replace("&", "And").Replace(".", string.Empty).Replace("§", string.Empty).Replace(":", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Trim()}.pdf";
            return Regex.Replace(title, @"\s+", " ");
        }

        /// <summary>
        /// Get list of subchapter title
        /// </summary>
        /// <returns>List of subchapter title</returns>
        public List<string> GetListOfSubChapterTitle()
            => DriverExtensions.GetElements(SubchapterTitleLocator).ToList().Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get list of subchapter title
        /// </summary>
        /// <returns>List of subchapter title</returns>
        public List<string> GetListOfChapterTitle()
            => DriverExtensions.GetElements(ChapterTitleLocator).ToList().Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get list of regulatory sections
        /// </summary>
        /// <returns>List of regulatory sections</returns>
        public List<string> GetListOfRegulatorySections()
            => DriverExtensions.GetElements(RegulatorySectionLocator).ToList().Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get list of selected items.
        /// </summary>
        /// <param name="indexes">
        /// The indexes.
        /// </param>
        /// <returns>
        /// The list of selected sections.
        /// </returns>
        public List<string> GetListOfSelectedSections(List<int> indexes)
        {
            IReadOnlyCollection<IWebElement> regulatorySections = DriverExtensions.GetElements(RegulatorySectionLocator);
            return indexes.Select(x => regulatorySections.ElementAt(x).Text).ToList();
        }

        /// <summary>
        /// Checks if the Super Browse page has loaded
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsSuperBrowseLoaded() => DriverExtensions.IsDisplayed(SuperBrowseContentLocator);

        /// <summary>
        /// Get Checked Items Count
        /// </summary>
        /// <returns>checked items count</returns>
        public int GetCheckedItemsCount() =>
            DriverExtensions.GetText(SuperbrowseCheckedItemsCountLocator).ConvertCountToInt();
    }
}