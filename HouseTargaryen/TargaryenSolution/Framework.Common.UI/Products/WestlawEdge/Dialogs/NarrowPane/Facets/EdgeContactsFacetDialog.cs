namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    ///  Describe Contact dialog which appears after clicking on the 'Select' button near the Contact on the Narrow pane
    /// </summary>
    public class EdgeContactsFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string SelectedOptionsItemMask = ".//div[@role='listitem']//*[contains(text(),{0})]";
        
        private static readonly By AvailableItemsLocator = By.XPath(".//div[@role='list']//span");

        private static readonly By AvailableItemsContainerLocator = By.XPath("//div[contains(@class,'SearchFacet-body')]");
        
        private static readonly By ContainerLocator = By.Id("facet_div_notificationCenterContact");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Select item
        /// </summary>
        /// <param name="itemName"></param>
        public new void SelectItem(string itemName)
        {
            DriverExtensions.WaitForElement(this.Container, SafeXpath.BySafeXpath(SelectedOptionsItemMask, itemName))
                            .Click();
        }

        /// <summary>
        /// The get available items list.
        /// </summary>
        /// <returns>
        /// T
        /// </returns>
        public override List<string> GetAvailableItemsTextList() =>
            DriverExtensions.GetElements(AvailableItemsContainerLocator, AvailableItemsLocator).Select(elem => elem.Text).ToList();

    }
}