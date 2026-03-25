namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents
{
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Title row component
	/// </summary>
	public sealed class TableRowComponent : BaseInnerRowComponent
	{
		/// <inheritdoc />
		public TableRowComponent(IWebElement rootComponentWebElement)
			: base(rootComponentWebElement)
		{
			this.TitleComponent = new TitleComponent(this.BaseComponentWebElement);
			this.Image = new ImageComponent(this.BaseComponentWebElement);
		}

		/// <summary>
		/// Title component
		/// </summary>
		public TitleComponent TitleComponent { get; }

		/// <summary>
		/// Trademark image component
		/// </summary>
		public ImageComponent Image { get; }

		/// <inheritdoc />
		protected override sealed By ComponentLocator { get; } = By.XPath(".");

		/// <summary>
		/// Retrieve Cell inner text
		/// </summary>
		/// <param name="index">index of the row. (Corresponds to the header title)</param>
		/// <returns>Inner Text</returns>
		public string GetCellInnerTextByIndex(int index) => DriverExtensions.GetHiddenText(
			DriverExtensions.GetElement(this.RowLineWebElementsList[index], By.XPath("./div")));

		/// <summary>
		/// Retrieve display state
		/// </summary>
		/// <param name="index">Cell index</param>
		/// <returns>True if displayed</returns>
		public bool IsCellDisplayed(int index) => this.RowLineWebElementsList[index].Displayed;
	}
}