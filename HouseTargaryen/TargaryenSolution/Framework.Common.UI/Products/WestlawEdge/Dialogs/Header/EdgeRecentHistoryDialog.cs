namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// The Indigo Recent History Dialog is expanded in Header.
    /// </summary>
    public class EdgeRecentHistoryDialog : BaseEdgeHeaderDialog
    {
        private const string DocumentItemTitleLctMask = "//*[@class='historyItem']/a[contains(@href,'{0}')]";
        private const string CitationsLctMask = "//li[@class='hasIcon' and .//a[contains(@href,'{0}')]]/div/div/span";
        private const string CitationsDateLctMask = "//li[@class='hasIcon' and .//a[contains(@href,'{0}')]]/div/span";

        private static readonly By RecentHistoryListLocator = By.XPath(".//li[@class='hasIcon'] | .//li[@class='co_recentResearchItemSearch']");
        private static readonly By ItemContainerLocator = By.CssSelector(".Home-flex-content li");
        private static readonly By ContainerLocator = By.XPath("//li[@id='co_recentHistoryContainer']//div[@class='co_dropdownBoxExpanded']");
        private static readonly By ViewThisHistoryButtonLocator = By.XPath("//a[contains(text(), 'View this history') or contains(text(),'Afficher l’historique')]");
        private static readonly By RecentHistoryLinkLocator = By.XPath(".//div[@class='historyItem']//a");
        private static readonly By ViewAllHistoryLinkLocator = By.XPath(".//a[text()='View all']");

        private static readonly string ImpliedOverrulingIconLctMask = ".//*[@id='co_recentHistoryContainer']//li[{0}]//a[@class='co_impliedOverrulingsFlagSm']";
        private static readonly string SelectedTypeOfHistoryLctMask = "//ul[@class='Tab-list']//*[contains(text(),'{0}')]";

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Click view this history button
        /// </summary>
        public T ClickViewThisHistoryButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(ViewThisHistoryButtonLocator);

        /// <summary>
        /// Get recent search models
        /// </summary>
        /// <returns>List with Searches Recent History Items</returns>
        public List<SearchesRecentHistoryItem> GetRecentSearchesItems() => DriverExtensions
            .GetElements(ContainerLocator, RecentHistoryListLocator).Select(
                item => new SearchesRecentHistoryItem(item)).ToList();

        /// <summary>
        /// View all history link
        /// </summary>
        public ILink ViewAllHistoryLink => new Link(this.Container, ViewAllHistoryLinkLocator);

        /// <summary>
        /// Verify that row contains implied overruling icon
        /// </summary>
        /// <param name="rowIndex"> Row index in the results grid </param>
        /// <returns> True if icon is displayed, false otherwise </returns>
        public bool IsHistoryRowContainsImpliedOverrulingFlag(int rowIndex) => DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(ImpliedOverrulingIconLctMask, rowIndex)) != null;

        /// <summary>
        /// Select type of history
        /// </summary>
        /// <param name="typeOfHistory">
        /// The type Of History.
        /// </param>
        /// <returns>
        /// The recent history dialog <see cref="EdgeRecentHistoryDialog"/>.
        /// </returns>
        public EdgeRecentHistoryDialog SelectTypeOfHistory(string typeOfHistory) =>
            this.ClickElement<EdgeRecentHistoryDialog>(
                By.XPath(string.Format(SelectedTypeOfHistoryLctMask, typeOfHistory)));

        /// <summary>
        /// Click on History Event By Number
        /// </summary>
        public T ClickHistoryEventByNumber<T>(int number) where T : ICreatablePageObject =>
             this.ClickElement<T>(DriverExtensions.GetElements(ContainerLocator, RecentHistoryLinkLocator).Where(e => e.Displayed).ToList().ElementAt(number));

        #region By Guid
        /// <summary>
        /// Gets item title by doc guid
        /// </summary>
        /// <param name="guid">document guid</param>
        /// <returns>item title</returns>
        public string GetItemTitleByGuid(string guid)
            => DriverExtensions.GetElement(this.Container, ItemContainerLocator, By.XPath(string.Format(DocumentItemTitleLctMask, guid))).Text;
      
        /// <summary>
        /// Gets citations list by doc guid
        /// </summary>
        /// <param name="guid">document guid</param>
        /// <returns>list of citations</returns>
        public List<string> GetItemCitationsByGuid(string guid) =>
            DriverExtensions.GetElements(this.Container, ItemContainerLocator, By.XPath(string.Format(CitationsLctMask, guid)))
                            .Concat(DriverExtensions.GetElements(this.Container, ItemContainerLocator, By.XPath(string.Format(CitationsDateLctMask, guid))))
                            .Select(e => e.Text)
                            .ToList();
        #endregion
    }
}