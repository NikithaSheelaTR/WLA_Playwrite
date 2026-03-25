namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents the RI results list page reached as a result of clicking a Related Topic link
    /// </summary>
    public class RelatedTopicsResultsPage : TabPage
    {
        private static readonly By ClusterTopicHeaderLocator = By.Id("coid_website_searchAvailableFacets");

        /// <summary>
        /// Get cluster single topic page header
        /// </summary> 
        /// <param name="text"> Text to compare </param>
        /// <returns> True if text is correct, false otherwise </returns>
        public bool IsClusterTopicHeaderTextCorrect(string text)
        {
            string pageHeaderText = DriverExtensions.WaitForElementDisplayed(ClusterTopicHeaderLocator).Text;
            return pageHeaderText.Contains(text);
        }
    }
}