
namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Graphs dropdown
    /// </summary>
    public class GraphsTypesDropDown : BaseModuleRegressionCustomDropdown<GraphsTypes> 
    {
        private static readonly By ContainerLocator = By.Id("co_searchResults_ip_facetGraph_dropdown");

        private static readonly By SelectedOptionLocator = By.Id("co_searchResults_ip_facetGraph_dropdown_selected");

        private static readonly By DropDownOptionsLocator = By.XPath(".//li");

        private EnumPropertyMapper<GraphsTypes, WebElementInfo> graphsTypesMap;

        /// <summary>
        /// Return Selected Option 
        /// </summary>
        /// <returns> Selected option</returns>
        public override GraphsTypes SelectedOption => this.GetSelectedOptionText().GetEnumValueByText<GraphsTypes>();
                                                                      
        /// <summary>
        /// Return Selected Option text
        /// </summary>
        /// <returns> Selected option text</returns>
        public string GetSelectedOptionText() => DriverExtensions.GetText(ContainerLocator, SelectedOptionLocator);

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        protected override IEnumerable<GraphsTypes> OptionsFromExpandedDropdown => 
            DriverExtensions.GetElements(this.Dropdown, DropDownOptionsLocator).Skip(1)
                            .Select(elem => elem.Text.GetEnumValueByText<GraphsTypes>());

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(ContainerLocator);

        /// <summary>
        /// Return if option is selected 
        /// </summary>
        /// <returns> true if is selected otherwise false </returns>
        public override bool IsSelected(GraphsTypes option) =>
            DriverExtensions.GetText(ContainerLocator, SelectedOptionLocator).Equals(option.GetEnumTextValue());

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        protected override void SelectOptionFromExpandedDropdown(GraphsTypes option)
        {
            DriverExtensions.GetElement(this.Dropdown, By.XPath(this.GraphsTypesMap[option].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Graphs types  Map
        /// </summary>
        protected EnumPropertyMapper<GraphsTypes, WebElementInfo> GraphsTypesMap  =>
            this.graphsTypesMap = this.graphsTypesMap ?? EnumPropertyModelCache.GetMap<GraphsTypes, WebElementInfo>();
    }
}
