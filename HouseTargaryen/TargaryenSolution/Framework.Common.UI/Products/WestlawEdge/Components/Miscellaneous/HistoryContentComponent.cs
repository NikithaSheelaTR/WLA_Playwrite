namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// History Widget Content Component
    /// </summary>
    public class HistoryContentComponent : BaseModuleRegressionComponent
    {
        private static readonly string ImpliedOverrulingIconLctMask = "//*[@id='panel_HistoryPaneId']//li[{0}]//a[@class='co_impliedOverrulingsFlagSm']";

        private static readonly By DisabledSearchQueryLocator = By.XPath(".//h4/span");
        private static readonly By InfoIconLocator = By.XPath(".//span[contains(@class,'icon25 icon_help-blueOutline')]");
        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");
        private static readonly By ContainerLocator = By.Id("panel_HistoryPaneId");
        private static readonly By RecentHistoryListLocator = By.XPath(".//li[@class='hasIcon']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify that a row contains implied overruling icon
        /// </summary>
        /// <param name="rowIndex"> Row index in the results grid </param>
        /// <returns> True if icon is displayed, false otherwise </returns>
        public bool IsHistoryRowContainsImpliedOverrulingFlag(int rowIndex) => DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(ImpliedOverrulingIconLctMask, rowIndex)) != null;

        /// <summary>
        /// Get recent search models
        /// </summary>
        /// <returns>List with Searches Recent History Items</returns>
        public List<SearchesRecentHistoryItem> GetRecentSearchesItems() => DriverExtensions.GetElements(ContainerLocator, RecentHistoryListLocator).Select(
                                                                               item => new SearchesRecentHistoryItem(item)).ToList();
 
        #region Disabled query
        /// <summary>
        /// Get Recent History Searches List that are disabled
        /// </summary>
        /// <returns>list</returns>
        public List<string> GetRecentHistoryDisabledSearchesList() => DriverExtensions
            .GetElements(ContainerLocator, DisabledSearchQueryLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Is info icon (?) displayed
        /// </summary>
        /// <returns>
        /// Return true if the info icon is displayed
        /// </returns>
        public bool IsInfoIconDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, InfoIconLocator);

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// Return true if the hover message is displayed
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(InfoIconLocator);
            return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");
        }
        #endregion Disabled query
    }
}
