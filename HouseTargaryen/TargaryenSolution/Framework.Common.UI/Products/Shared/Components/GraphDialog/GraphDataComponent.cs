namespace Framework.Common.UI.Products.Shared.Components.GraphDialog
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Products.Shared.Items.GraphData;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Graphs Data component
    /// </summary>
    public class GraphDataComponent : BaseModuleRegressionComponent
    {
        private const string AllCheckBoxSelectionModeLctMask = ".//a[text() = '{0}']";
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_graphData']");
        private static readonly By GraphDataItemLocator = By.XPath(".//li[contains(@id, 'co_facet')]");
        private static readonly By GraphDataDropdownLocator = By.XPath("//select[@id='coid_graphDataDropDown']");

        /// <summary>
        /// Graph data dropdown
        /// </summary>
        public IDropdown<GraphsTypes> DropDown => new Dropdown<GraphsTypes>(GraphDataDropdownLocator);

        /// <summary>
        /// Gets Graph data items
        /// </summary>
        public List<GraphDataItem> GetGraphDataItems() => DriverExtensions.GetElements(ContainerLocator, GraphDataItemLocator)
                                                                          .Select(elem => new GraphDataItem(elem)).ToList();
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Sets options Select All or Deselect All 
        /// </summary>
        public void SetAllSelectOption(string option) => DriverExtensions.GetElement(
            this.ComponentLocator,
            By.XPath(string.Format(AllCheckBoxSelectionModeLctMask, option))).Click();

        /// <summary>
        /// Verifies if the option enabled 
        /// </summary>
        public bool IsOptionEnabled (string option) => !DriverExtensions.GetElement(
                                                                         this.ComponentLocator,
                                                                         By.XPath(
                                                                             string.Format(
                                                                                 AllCheckBoxSelectionModeLctMask,
                                                                                 option))).GetAttribute("class")
                                                                     .Contains("co_disabled");
    }
}
