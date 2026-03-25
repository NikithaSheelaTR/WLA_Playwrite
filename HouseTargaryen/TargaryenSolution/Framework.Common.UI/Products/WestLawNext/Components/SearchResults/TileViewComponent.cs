namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TileViewInnerComponents;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Tile View Component
	/// </summary>
	public sealed class TileViewComponent : BaseModuleRegressionComponent
	{
		private static readonly By TileRowLocator = By.XPath(".//tr");

		private static readonly By ContainerLocator = By.XPath("//table[@class='co_ipTileTable']");

		/// <summary>
		/// Tile Row Component list
		/// </summary>
		public List<TileRowComponent> TileRowComponentList { get; } = DriverExtensions.GetElements(ContainerLocator, TileRowLocator)?.Select(webEl => new TileRowComponent(webEl))
		                                                                                                          .ToList();

		/// <summary>
		/// Component container locator
		/// </summary>
		protected override By ComponentLocator { get; } = ContainerLocator;
	}
}