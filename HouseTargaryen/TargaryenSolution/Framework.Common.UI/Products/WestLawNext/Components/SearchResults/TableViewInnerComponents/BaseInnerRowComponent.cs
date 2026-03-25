namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.Utils.Execution;

	using OpenQA.Selenium;

	/// <summary>
	/// Abstract Inner component 
	/// </summary>
	public abstract class BaseInnerRowComponent : BaseModuleRegressionComponent
	{
		/// <summary>
		/// Table View result row guid
		/// </summary>
		public string Guid { get; private set; }

		/// <summary>
		/// Base container WebElement
		/// </summary>
		protected IWebElement BaseComponentWebElement;

		/// <summary>
		/// Row line WebElements
		/// </summary>
		protected List<IWebElement> RowLineWebElementsList;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseInnerRowComponent"/> class. 
		/// Constructor for all InnerComponent classes
		/// </summary>
		/// <param name="rootComponentWebElement"></param>
		protected BaseInnerRowComponent(IWebElement rootComponentWebElement)
		{
			this.BaseComponentWebElement = DriverExtensions.SafeGetElement(rootComponentWebElement, this.ComponentLocator);
			SafeMethodExecutor.Execute(() => { this.Guid = this.BaseComponentWebElement.GetAttribute("data-guid"); });
			this.RowLineWebElementsList = DriverExtensions.GetElements(this.BaseComponentWebElement, By.XPath("./*")).ToList();
		}
	}
}