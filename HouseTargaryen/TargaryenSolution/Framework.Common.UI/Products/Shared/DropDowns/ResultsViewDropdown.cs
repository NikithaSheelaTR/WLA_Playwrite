namespace Framework.Common.UI.Products.Shared.DropDowns
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Products.Shared.Enums.Toolbars;
	using Framework.Common.UI.Products.Shared.Models.EnumProperties;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.CommonTypes.Extensions;
	using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

	/// <summary>
	/// Results View dropdown
	/// </summary>
	public class ResultsViewDropdown : BaseModuleRegressionCustomDropdown<ResultsViewOptions>
	{
		private static readonly By DropdownLocator = By.XPath("//div[@id='ipViewDropdown_a11y']");

		private static readonly By SelectLocator = By.XPath(".//button[@class='a11yDropdown-button']/span[@class='icon25 icon_downMenu-gray']");

		private static readonly By OptionsLocator = By.XPath(".//ul/li");

		/// <inheritdoc />
        public override ResultsViewOptions SelectedOption
        {
            get
            {
                ExpandIfNotExpanded();
				IWebElement checkedElement = DriverExtensions.GetElements(DropdownLocator, OptionsLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Equals("true")
                            || elem.GetAttribute("class")
                                   .Contains("selected"));
                string checkedElementText = DriverExtensions.GetElement(checkedElement, By.TagName("span")).Text;

                return checkedElementText.GetEnumValueByText<ResultsViewOptions>();
            }
        }

		/// <inheritdoc />
		protected override IEnumerable<ResultsViewOptions> OptionsFromExpandedDropdown  => DriverExtensions
			.GetElements(DropdownLocator, OptionsLocator, By.TagName("span"))?.Select(
				webElement =>
					webElement.Text.GetEnumValueByText<ResultsViewOptions>())
			.ToList();

		/// <inheritdoc />
		protected override IWebElement Dropdown { get; } = DriverExtensions.GetElements(DropdownLocator, By.TagName("button")).FirstOrDefault();

        /// <inheritdoc />
		protected override bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(this.Dropdown).GetAttribute("aria-expanded");
            dropdownClass = dropdownClass == null ? "false" : dropdownClass;
            return dropdownClass.Equals("true", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Gets the dropdown map.
		/// </summary>
		private EnumPropertyMapper<ResultsViewOptions, WebElementInfo> DropdownMap { get; } =
			EnumPropertyModelCache.GetMap<ResultsViewOptions, WebElementInfo>();

		/// <inheritdoc />
		public override bool IsSelected(ResultsViewOptions option) => DriverExtensions
			.GetElement(DropdownLocator, By.XPath(this.DropdownMap[option].LocatorString)).GetAttribute("class").Contains("selected");

		/// <inheritdoc />
		protected override void SelectOptionFromExpandedDropdown(ResultsViewOptions option) => DriverExtensions.GetElement(DropdownLocator, By.XPath(this.DropdownMap[option].LocatorString)).Click();

		/// <summary>
		/// ClickDropdownArrow
		/// </summary>
		protected override void ClickDropdownArrow() => DriverExtensions.GetElement(DropdownLocator, SelectLocator).Click();
	}
}