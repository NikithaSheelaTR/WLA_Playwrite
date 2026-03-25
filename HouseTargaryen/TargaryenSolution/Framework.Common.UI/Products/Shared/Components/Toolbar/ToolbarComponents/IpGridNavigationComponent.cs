namespace Framework.Common.UI.Products.Shared.Components.Toolbar.ToolbarComponents
{
	using Framework.Common.UI.Interfaces;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Ip Grid Navigation Component
	/// </summary>
	public class IpGridNavigationComponent : BaseModuleRegressionComponent
	{
		private static readonly By ColumnsLinkLocator = By.XPath("./a[@id='ipColumnChooserWidget']");

		private static readonly By PreviousColumnButtonLocator = By.XPath("./a[contains(@class,'co_prev')]");

		private static readonly By NextColumnButtonLocator = By.XPath("./a[contains(@class,'co_next')]");

		/// <inheritdoc />
		protected override By ComponentLocator { get; } = By.XPath("//ul[@class='co_navOptions']/li[contains(@class,'co_navColumns co_ipGridPagination')]");

		/// <summary>
		/// Click on Next button locator
		/// </summary>
		public void ClickOnNextColumnButton() => DriverExtensions.Click(
			this.ComponentLocator,
			NextColumnButtonLocator);

		/// <summary>
		/// Click on previous button locator
		/// </summary>
		public void ClickOnPreviousColumnButton() => DriverExtensions.Click(
			this.ComponentLocator,
			PreviousColumnButtonLocator);

		/// <summary>
		/// Is Next button enabled
		/// </summary>
		/// <returns>True if enabled</returns>
		public bool IsNextColumnButtonEnabled() => !DriverExtensions
			                                           .GetAttribute(
				                                           "class",
				                                           this.ComponentLocator,
				                                           NextColumnButtonLocator).Contains("disabled");

		/// <summary>
		/// Is Previous button enabled
		/// </summary>
		/// <returns>True if enabled</returns>
		public bool IsPreviousColumnButtonEnabled() => !DriverExtensions.GetAttribute(
			                                               "class",
			                                               this.ComponentLocator,
			                                               PreviousColumnButtonLocator).Contains("disabled");

		/// <summary>
		/// ToDo: ColumnSelectionDialog
		/// </summary>
		/// <typeparam name="T">Dialog instance</typeparam>
		/// <returns>Dialog with columns selections</returns>
		public T ClickOnColumnsLink<T>() where T : ICreatablePageObject
		{
			DriverExtensions.Click(this.ComponentLocator, ColumnsLinkLocator);
			return DriverExtensions.CreatePageInstance<T>();
		}
	}
}