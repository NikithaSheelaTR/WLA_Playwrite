namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Interfaces.Elements;
	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Products.Shared.Elements.Buttons;
	using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
	using Framework.Common.UI.Products.Shared.Elements.Labels;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Edge Checkbox Filter component for EdgeIpCheckboxTreeFacet
	/// </summary>
	public class EdgeCheckboxFilterFacetComponent : BaseModuleRegressionComponent
	{
		private static readonly By LabelLocator = By.XPath(".//div[@class='SearchFacet-listItemGroup SearchFacet-treeItemGroup']//span[contains(text(),'U.S. Federal')]");

		private static readonly By LabelCountLocator = By.XPath(".//div[@class='SearchFacet-listItemGroup SearchFacet-treeItemGroup']");

        private static readonly By ExpandCollapseButtonLocator =
			By.XPath("./div[@class='SearchFacet-listItemGroup']//button");

		private static readonly By CheckBoxLocator = By.XPath(".//div[@class='SearchFacet-listItemGroup SearchFacet-treeItemGroup']//input[@type='checkbox']");

		private static readonly By ChildrenFiltersLocator = By.XPath(".//div[contains(@class,'--secondary')]");

		private readonly By containerLocator;

		private readonly IWebElement containerWebElement;

		/// <summary>
		/// Initializes a new instance of the <see cref="EdgeCheckboxFilterFacetComponent"/> class. 
		/// The Constructor
		/// </summary>
		/// <param name="locatorBy"> Locator to initiate the component </param>
		public EdgeCheckboxFilterFacetComponent(By locatorBy)
		{
			this.containerLocator = locatorBy;
			this.containerWebElement = DriverExtensions.GetElement(locatorBy);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EdgeCheckboxFilterFacetComponent"/> class. 
		/// The Constructor
		/// </summary>
		/// <param name="webElement"> WebElement to initiate the component </param>
		public EdgeCheckboxFilterFacetComponent(IWebElement webElement)
		{
			this.containerWebElement = webElement;
		}

		/// <summary>
		/// Expand/collapse children filters button
		/// </summary>
		public IButton ExpandCollapseButton => this.containerWebElement.GetAttribute("class").Contains("has-children")
			                                       ? new Button(this.containerWebElement, ExpandCollapseButtonLocator)
			                                       : null;

		/// <summary>
		/// Filter Label
		/// </summary>
		public ILabel FilterLabel => new Label(this.containerWebElement, LabelLocator);

		/// <summary>
		/// Document count for Filter
		/// </summary>
		public ILabel FilterCountLabel => new Label(this.containerWebElement, LabelCountLocator);

		/// <summary>
		/// Checkbox to apply Filter
		/// </summary>
		public ICheckBox FilterCheckBox => new CheckBox(this.containerWebElement, CheckBoxLocator);

		/// <summary>
		/// Return children Filter components list
		/// </summary>
		public List<EdgeCheckboxFilterFacetComponent> ChildrenFilters => DriverExtensions
			.GetElements(this.containerWebElement, ChildrenFiltersLocator, EdgeIpCheckboxTreeFacetComponent.ParentLabelLocator).Select(webElement => new EdgeCheckboxFilterFacetComponent(webElement)).ToList();

		/// <inheritdoc />
		protected override By ComponentLocator => this.containerLocator;

		/// <inheritdoc />
		public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.containerWebElement);
	}
}