namespace Framework.Common.UI.Products.Shared.DropDowns
{
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Enums.Toolbars;
using Framework.Common.UI.Products.Shared.Models.EnumProperties;
using Framework.Common.UI.Products.WestLawNextCanada.Enums;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Core.CommonTypes.Extensions;
using Framework.Core.Utils.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// This is AlertResultViewDropdown in the toolbar
    /// </summary>
    public class AlertResultViewDropdown : BaseModuleRegressionCustomDropdown<AlertResultsViewOptions>
    {    
        private static readonly By DropdownLocator = By.XPath("//a[@name='co_alertsViewSliderArrow'] | .//*[@id='co_viewSlider']");

        private static readonly By DropdownOptionLocator = By.XPath(".//li[./a[contains(@title,'View')]]");

        private static readonly By AlertViewIconLocator = By.XPath(".//li[@class='co_selected']//a");

        private By dropdownLocator;

        private EnumPropertyMapper<AlertResultsViewOptions, WebElementInfo> AlertResultViewOptionsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertResultViewDropdown"/> class.
        /// </summary>
        public AlertResultViewDropdown() => this.dropdownLocator = DropdownLocator;

        /// <summary>
        /// Detail Level Map
        /// </summary>
        protected EnumPropertyMapper<AlertResultsViewOptions, WebElementInfo> AlertResultViewMap =>
            this.AlertResultViewOptionsMap = this.AlertResultViewOptionsMap ?? EnumPropertyModelCache.GetMap<AlertResultsViewOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");


        /// <inheritdoc />
        /// <summary>
        /// Return Selected Option.
        /// </summary>
        public override AlertResultsViewOptions SelectedOption 
        {
            get
            {
                IWebElement checkedElement = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("class").Contains("checked")
                            || elem.GetAttribute("class")
                                   .Contains("selected"));
                string checkedElementText =
                    DriverExtensions.WaitForElement(checkedElement, By.TagName("a")).GetAttribute("title");

                return checkedElementText.GetEnumValueByText<AlertResultsViewOptions>();
            }
        }

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<AlertResultsViewOptions> OptionsFromExpandedDropdown
        {
            get
            {
                return DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                    elem => DriverExtensions
                        .GetElement(elem, By.TagName("a")).GetAttribute("title")
                        .GetEnumValueByText<AlertResultsViewOptions>()).ToList();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The dropdown container element 
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.dropdownLocator);

        /// <inheritdoc />
        /// <summary>
        /// Verify that alertResultView option is selected
        /// </summary>
        /// <param name="alertResultViewOptions">The desired view Level</param>
        /// <returns> True if selected, false otherwise </returns>
        public override bool IsSelected(AlertResultsViewOptions alertResultViewOptions)
        {
            this.ExpandIfNotExpanded();
            return string.Equals(
                   DriverExtensions.GetElement(this.Dropdown, AlertViewIconLocator).Text,
                   this.AlertResultViewMap[alertResultViewOptions].Text,
                   StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc />
        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">The desired option</param>
        protected override void SelectOptionFromExpandedDropdown(AlertResultsViewOptions option)
        {
            DriverExtensions.Click(
               DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.AlertResultViewMap[option].LocatorString)));
            DriverExtensions.WaitForJavaScript();
        }
      }
    }
