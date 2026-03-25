namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Judge dialog which appears after clicking on the 'Select' button near the Judge on the Narrow pane
    /// </summary>
    public class EdgeProceduralPostureFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string AvailableOptionsItemLctMask =
            "//ul[@id='co_facet_proceduralPosture_availableOptions']//div[contains(@class, 'co_listItem')]//span[contains(@class, 'name')]";

        private static readonly By ContainerLocator = By.Id("co_facet_proceduralPosture_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Search and select item
        /// </summary>
        /// <param name="itemName">The item name</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public override T SearchAndSelectItemByName<T>(string itemName)
        {
            this.SetTextToSearchInput<T>(itemName);
            DriverExtensions.WaitForElementDisplayed(this.Container, SafeXpath.BySafeXpath(AvailableOptionsItemLctMask));
            DriverExtensions.GetElements(this.Container, SafeXpath.BySafeXpath(AvailableOptionsItemLctMask)).First().Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}