namespace Framework.Common.UI.Products.Shared.DropDowns
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Products.Shared.Enums.Toolbars;
	using Framework.Common.UI.Products.Shared.Models.EnumProperties;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.Utils.Enums;

	using OpenQA.Selenium;

	/// <inheritdoc />
	public class IpSortByDropdown : BaseModuleRegressionCustomDropdown<PatentSearchSortOptions>
	{
		private const string SearchOptionLctMask = "//ul[@id='co_search_sortOptions']//li[contains(., '{0}')]";

		private static readonly By AvailableOptionsLocator = By.XPath("//ul[@id='co_search_sortOptions']//li");

		private static readonly By SortByDropdownLocator = By.CssSelector("[id*='co_search_sortDropDownControl']>button");

		/// <summary>
		/// Retrieve selected option
		/// </summary>
		public override PatentSearchSortOptions SelectedOption
		{
			get
			{
				return this.DropdownMap
					.Where(x => x.Value.Text.Equals(DriverExtensions.GetText(SortByDropdownLocator))).Select(x => x.Key)
					.FirstOrDefault();
			}
		}

		/// <summary>
		/// IWebElement
		/// </summary>
		protected override IWebElement Dropdown { get; } = DriverExtensions.SafeGetElement(SortByDropdownLocator);

		/// <summary>
		/// Returns list of available options
		/// </summary>
		/// <returns> List of available options </returns>
		protected override IEnumerable<PatentSearchSortOptions> OptionsFromExpandedDropdown => DriverExtensions.GetElements(AvailableOptionsLocator)
		                                                                                                       .Select(x => this.DropdownMap.First(y => y.Value.Text.Equals(x.Text)).Key).ToList();

		private EnumPropertyMapper<PatentSearchSortOptions, WebElementInfo> DropdownMap { get; } = EnumPropertyModelCache.GetMap<PatentSearchSortOptions, WebElementInfo>();

		/// <inheritdoc />
		public override bool IsSelected(PatentSearchSortOptions option) => this.DropdownMap[option].Text?.Equals(DriverExtensions.GetText(SortByDropdownLocator)) ?? false;

		/// <summary>
		/// Verifies if drop-down is expanded
		/// </summary>
		/// <returns> True if expanded</returns>
		protected override bool IsDropdownExpanded() => DriverExtensions.GetElements(AvailableOptionsLocator).All(x => x.Displayed);

		/// <summary>
		/// Click Drop-down Arrow
		/// </summary>
		protected override void ClickDropdownArrow()
		{
			DriverExtensions.ClickUsingJavaScript(SortByDropdownLocator);
		}

		/// <summary>
		/// Select option from expanded drop-down
		/// </summary>
		/// <param name="option"> Option to select</param>
		protected override void SelectOptionFromExpandedDropdown(PatentSearchSortOptions option) =>
			DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, this.DropdownMap[option].Text)));
	}
}