namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Related Topics Component
    /// </summary>
    public class RelatedTopicsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ClusterTopicHeaderLocator = By.Id("coid_website_searchAvailableFacets");

        private static readonly By LoadingSpinnerLocator = By.CssSelector(".co_search_ajaxLoading");

        private static readonly By PrimaryTabNavigationLocator = By.Id("co_docPrimaryTabNavigation");

        private static readonly By SearchContentLocator = By.Id("co_fermiSearchResult_data");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.Id(Constants.TopicContainerId);

        /// <summary>
        /// Open a single cluster and get text of the content
        /// </summary>
        /// <param name="clusterId">Cluster id</param>
        /// <returns>Text of search content</returns>
        public string ClickOnSingleClusterAndGetContentText(string clusterId)
        {
            DriverExtensions.WaitForElement(By.Id(clusterId)).Click();
            return DriverExtensions.WaitForElement(SearchContentLocator).GetText();
        }

        /// <summary>
        /// Click related topic link. 
        /// </summary>
        /// <param name="linkToClick"> Related topic link  </param>
        /// <returns> The <see cref="RelatedTopicsResultsPage"/>. </returns>
        public RelatedTopicsResultsPage ClickRelatedTopicLink(string linkToClick)
        {
            DriverExtensions.Click(this.ComponentLocator, By.PartialLinkText(linkToClick));
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementNotDisplayed(LoadingSpinnerLocator);
            return DriverExtensions.CreatePageInstance<RelatedTopicsResultsPage>();
        }

        /// <summary>
        /// Get clusters id list from the page.
        /// </summary>
        /// <returns> List of clusters id </returns>
        public List<string> GetClustersIdText()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(By.Id(Constants.TopicContainerId)), By.TagName("a"))
                               .Select(c => c.GetAttribute("id")).ToList();

        /// <summary>
        /// Return the text for the given cluster
        /// </summary>
        /// <param name="clusterId"> Cluster id </param>
        /// <returns> Text for the given cluster </returns>
        public string GetClusterText(string clusterId) => DriverExtensions.GetElement(By.Id(clusterId)).Text;

        /// <summary>
        /// Get cluster single topic page header
        /// </summary> 
        /// <param name="text"> Text to compare </param>
        /// <returns> True if text is correct, false otherwise </returns>
        public bool IsClusterTopicHeaderTextCorrect(string text)
            => DriverExtensions.WaitForElementDisplayed(ClusterTopicHeaderLocator).Text.Contains(text);

        /// <summary>
        /// Verify the related topics component is empty
        /// </summary> 
        /// <returns> True if empty, false otherwise </returns>
        public bool IsRelatedTopicsComponentEmpty()
            => DriverExtensions.WaitForElement(this.ComponentLocator, 120000).Text == string.Empty;

        /// <summary>
        /// Verify RI Navigation Tab is displayed
        /// </summary>
        /// <returns> True if RI Navigation Tab is displayed, false otherwise s</returns>
        public bool IsRiTabNavigationDisplayed() => DriverExtensions.IsDisplayed(PrimaryTabNavigationLocator);

        /// <summary>
        /// Wait for the given cluster
        /// </summary>
        /// <param name="clusterId"> Id of the cluster to wait for </param>
        public void WaitForCluster(string clusterId) => DriverExtensions.WaitForElement(By.Id(clusterId));

        /// <summary>
        /// Wait for related topics component
        /// </summary>
        public void WaitForRelatedTopicsComponent() => DriverExtensions.WaitForElement(this.ComponentLocator);
    }
}