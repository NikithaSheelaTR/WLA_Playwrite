namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Research Recommendations result grid.
    /// </summary>
    /// <typeparam name="TRrResultItem">
    /// The type of search result item
    /// </typeparam>
    public class ResearchRecommendationsResultList<TRrResultItem> : SearchResultList<TRrResultItem> where TRrResultItem : ResearchRecommendationsResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly By ClearSelectedLocator = By.XPath("//span[@id='co_raHeader_dockItemsClear']/a");

        private static readonly By ItemSelectedLocator = By.XPath(".//span[@id='co_raHeader_dockItemsSelected']");

        private static readonly By ResultItemLocator = By.XPath(".//li[contains(@id, 'cobalt_research_results_')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchRecommendationsResultList{TRrResultItem}"/> class. 
        /// </summary>
        /// <param name="container">
        /// container 
        /// </param>
        public ResearchRecommendationsResultList(IWebElement container) : base(container, ResultItemLocator)
        {
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <returns>
        /// True if result list contains at least one item
        /// </returns>
        public override bool Any() =>
            DriverExtensions.GetElements(this.Container, this.ItemLocator).Any()
            && DriverExtensions.IsDisplayed(this.Container, this.ItemLocator);

        /// <summary>
        /// Click link 'clear selected'
        /// </summary>
        public void ClickClearSelectedLink() =>
            DriverExtensions.WaitForElementDisplayed(ClearSelectedLocator).Click();

        /// <summary>
        /// Gets the text in the research recommendations toolbar related to selected items
        /// </summary>
        /// <returns>Get the text of the selected item</returns>
        public string GetItemSelectedText() =>
            DriverExtensions.WaitForElement(this.Container, ItemSelectedLocator).Text;

        /// <summary>
        /// Verify link 'clear selected' is displayed
        /// </summary>
        /// <returns>true if link 'clear selected' is displayed, false otherwise</returns>
        public bool IsClearSelectedLinkDisplayed() =>
            DriverExtensions.IsDisplayed(ClearSelectedLocator, 5);
    }
}
