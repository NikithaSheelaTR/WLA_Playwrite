namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd dialog with available and selected options lists.
    /// </summary>
    public abstract class BaseAvailableAndSelectedHierarchicalOptionsListsDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string AvailableOptionsItemLctMask = ".//ul[contains(@class,'co_facet_tree')]//li/*[contains(@id,'facet_hierarchy_facet_div') or contains(@for,'facet_hierarchy_facet_div')][contains(.,{0})]";

        private const string AvailableOptionsItemExpandLctMask = ".//button[contains(@id,'facet_div_') and contains(@childlistid,'facet_expandable_facet') and contains(.,{0})]";

        private const string AvailableOptionsItemHierarchyIconLctMask =
            ".//ul[contains(@id,'selectedOptions')]//button[contains(.,{0})]/following-sibling::button[contains(@class,'HierarchyIcon')]";

        private static readonly By ItemsListContainerLocator = By.XPath("//div[@class='co_collectorScrollOne co_scrollWrapper']//ul[@class='co_facet_tree']");

        private static readonly By AvailableItemLocator = By.XPath(".//label");

        /// <summary>
        /// The options.
        /// </summary>
        protected IEnumerable<IWebElement> Options
            => DriverExtensions.GetElements(ItemsListContainerLocator, AvailableItemLocator);

        /// <summary>
        /// The select item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        public override T SearchAndSelectItemByName<T>(string itemName)
            => this.ClickElement<T>(this.Container, SafeXpath.BySafeXpath(AvailableOptionsItemLctMask, itemName));

        /// <summary>
        /// The is option has green icon.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsOptionHasGreenIcon(string itemName)
            => DriverExtensions.SafeGetElement(this.Container, SafeXpath.BySafeXpath(AvailableOptionsItemLctMask, itemName))
                              ?.GetAttribute("class") == "co_facetSelected";

        /// <summary>
        /// The expand item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        public T ExpandItem<T>(string itemName) where T : BaseAvailableAndSelectedHierarchicalOptionsListsDialog
        {
            DriverExtensions.WaitForElement(
                this.Container,
                SafeXpath.BySafeXpath(AvailableOptionsItemExpandLctMask, itemName)).JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is hierarchy icon displayed.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHierarchyIconDisplayed(string itemName)
            => DriverExtensions.IsDisplayed(
                this.Container,
                SafeXpath.BySafeXpath(AvailableOptionsItemHierarchyIconLctMask, itemName));


        /// <summary>
        /// The get available options list.
        /// </summary>
        public override List<string> GetAvailableItemsTextList() => this.Options.Select(e => e.Text).ToList();
    }
}