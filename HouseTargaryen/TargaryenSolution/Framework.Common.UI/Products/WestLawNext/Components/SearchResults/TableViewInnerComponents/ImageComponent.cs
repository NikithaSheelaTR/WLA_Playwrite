namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents
{
	using System.Diagnostics.CodeAnalysis;

	using Framework.Common.UI.Interfaces;
	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Trademark image component
	/// </summary>
	public sealed class ImageComponent : BaseModuleRegressionComponent
	{
		/// <summary>
		/// Provides if usage of this component is applicable
		/// </summary>
		public bool IsApplicable;

		/// <summary>
		/// The image locator.
		/// </summary>
		private static readonly By ImageLocator = By.XPath(".//img[contains(@class,'co_trademarkScanImage') or parent::a[@class='co_imageLink']]");

		private IWebElement componentContainer;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="componentContainer"></param>
		public ImageComponent(IWebElement componentContainer)
		{
			this.componentContainer = componentContainer;
		}

		/// <inheritdoc />
		protected override By ComponentLocator { get; } = By.XPath(".//a[@class='co_ip_table_image']");

		/// <summary>
		/// Get image alternative text
		/// </summary>
		/// <returns>Image alternative text</returns>
		public string GetImageAlternativeText() => DriverExtensions.GetAttribute("alt", this.componentContainer, ImageLocator);

		/// <summary>
		/// Click on the image
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns><see cref="ICreatablePageObject"/>A Page Object</returns>
		public T ClickOnImage<T>() where T : ICreatablePageObject
		{
			DriverExtensions.Click(this.componentContainer, ImageLocator);
			return DriverExtensions.CreatePageInstance<T>();
		}
	}
}