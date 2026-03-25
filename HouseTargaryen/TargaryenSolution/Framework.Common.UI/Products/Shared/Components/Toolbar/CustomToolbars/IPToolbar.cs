namespace Framework.Common.UI.Products.Shared.Components.Toolbar.CustomToolbars
{
	using Framework.Common.UI.Products.Shared.Components.Toolbar.ToolbarComponents;
	using Framework.Common.UI.Products.Shared.DropDowns;

	using OpenQA.Selenium;

	/// <summary>
	/// Patent toolbar component
	/// </summary>
	public class IpToolbar : Toolbar
	{
		/// <summary>
		/// Results view  dropdown
		/// Used => instead of {get;} = to resolve incorrect components initialization order issue
		/// </summary>
		public ResultsViewDropdown ResultsViewDropdown => new ResultsViewDropdown();

		/// <summary>
		/// patent Sort by dropdown
		/// Used => instead of {get;} = to resolve incorrect components initialization order issue
		/// </summary>
		public new IpSortByDropdown SortBy => new IpSortByDropdown();

		/// <summary>
		/// Toolbar Navigation Component
		/// Used => instead of {get;} = to resolve incorrect components initialization order issue
		/// </summary>
		public IpGridNavigationComponent GridNavigationComponent => new IpGridNavigationComponent();

		/// <summary>
		/// Component locator
		/// </summary>
		protected override By ComponentLocator { get; } = By.XPath("//div[@class='co_searchToolbar']");
	}
}