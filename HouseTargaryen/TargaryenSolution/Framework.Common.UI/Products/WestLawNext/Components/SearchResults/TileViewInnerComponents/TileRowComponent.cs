namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TileViewInnerComponents
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Tile Row Component
	/// </summary>
	public class TileRowComponent : BaseModuleRegressionComponent
	{
		private static readonly By CellLocator = By.XPath("./td");

		private static readonly By ContainerLocator = By.XPath("./.");

		/// <inheritdoc />
		public TileRowComponent(IWebElement containerWebElement)
		{
			this.TileCellList = DriverExtensions.GetElements(containerWebElement, CellLocator)
			                                    ?.Select(webEl => new TileCellItem(webEl)).ToList();
		}

		/// <summary>
		/// Tile Cell List
		/// </summary>
		public List<TileCellItem> TileCellList { get; }

		/// <inheritdoc />
		protected override By ComponentLocator { get; } = ContainerLocator;
	}
}