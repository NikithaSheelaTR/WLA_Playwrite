namespace Framework.Common.UI.Products.WestLawNext.Dialogs.Header
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Recent History Dialog is expanded in Header.
    /// </summary>
    public class RecentHistoryDialog : BaseModuleRegressionDialog
    {
        private const string ItemByGuidContainerLctMask = "//div[@class='co_recentResearch_item' and ./a[contains(text(), '{0}')]]";

        /// <summary>
        /// Container Locator
        /// </summary>
        protected static readonly By ContainerLocator = By.XPath("//li[@id='co_recentHistoryContainer']//div[@class='co_dropdownBoxExpanded']");
              
        private static readonly By CitationsLocator = By.XPath("./div/span");

        private static readonly By ViewAllRecentSearchesLinkLocator = By.Id("co_recentSearchesLink");

        private static readonly By ViewAllRecentDocumentsLinkLocator = By.Id("co_recentDocumentsLink");

        private static readonly By RecentHistoryListLocator = By.XPath(".//ul[@id ='co_recentSearchesList']/li");

        private static readonly By RecentHistoryDocumentListLocator = By.XPath(".//ul[@id ='co_recentDocumentsList']/li");

        private static readonly By DisabledSearchQueryLocator = By.XPath(".//div[contains(@class,'disabled')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentHistoryDialog"/> class. 
        /// </summary>
        public RecentHistoryDialog()
        {
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);
        }

        private IWebElement Container => DriverExtensions.GetElement(ContainerLocator);

        /// <summary>
        /// Clicks 'View All' recent researches link
        /// </summary>
        /// <returns>New instance of ResearchOrganizerPage</returns>
        public CommonHistoryPage ClickViewAllRecentSearchesLink()
            => this.ClickElement<CommonHistoryPage>(DriverExtensions.WaitForElement(this.Container, ViewAllRecentSearchesLinkLocator));

        /// <summary>
        /// Click 'View All' recent documents link
        /// </summary>
        /// <returns>New instance of CommonHistoryPage</returns>
        public CommonHistoryPage ClickViewAllResentDocumentsLink()
            => this.ClickElement<CommonHistoryPage>(DriverExtensions.WaitForElement(this.Container, ViewAllRecentDocumentsLinkLocator));

        /// <summary>
        /// Get recent search models
        /// </summary>
        /// <returns>List with Searches Recent History Model</returns>
        public List<SearchesRecentHistoryModel> GetRecentSearchesModels() => DriverExtensions
            .GetElements(ContainerLocator, RecentHistoryListLocator).Select(
                item => new SearchesRecentHistoryItem(item).ToModel<SearchesRecentHistoryModel>()).ToList();

        /// <summary>
        /// Get recent document models
        /// </summary>
        /// <returns>List with Document Recent History Model</returns>
        public List<SearchesRecentHistoryModel> GetRecentDocumentModels() => DriverExtensions
            .GetElements(ContainerLocator, RecentHistoryDocumentListLocator).Select(
                item => new SearchesRecentHistoryItem(item).ToModel<SearchesRecentHistoryModel>()).ToList();


        /// <summary>
        /// Get Recent History Searches List
        /// </summary>
        /// <returns>list</returns>
        public List<string> GetRecentHistoryDisabledSearchesList() => DriverExtensions
            .GetElements(ContainerLocator, DisabledSearchQueryLocator).Select(x => x.Text).ToList();

        #region Item by guid
        /// <summary>
        /// Citations list
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The list of citations.
        /// </returns>
        public IList<string> GetItemCitationsByGuid(string guid)
            => DriverExtensions.GetElements(this.ItemByGuidConteiner(guid), CitationsLocator).Select(c => c.Text).ToList();

        /// <summary>
        /// Gets Title Link
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetItemTitleByGuid(string guid) => DriverExtensions.GetElement(this.ItemByGuidConteiner(guid), By.TagName("a")).Text;

        private IWebElement ItemByGuidConteiner(string guid) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(ItemByGuidContainerLctMask, guid)));
        #endregion
    }
}