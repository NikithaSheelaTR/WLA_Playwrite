namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Interfaces.Elements;
	using Framework.Common.UI.Products.Shared.Elements.Labels;
	using Framework.Common.UI.Products.Shared.Elements.Textboxes;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// IP Jurisdiction facet for Edge
	/// </summary>
	public class EdgeIpCheckboxTreeFacetComponent : EdgeBaseFacetComponent
	{
		internal static readonly By ParentLabelLocator = By.XPath(".//div[contains(@class,'SearchFacet-listItem ')]");

		private static readonly By JurisdictionFilterComponentsLocator =
			By.XPath(".//div[@class='SearchFacet-list SearchFacet-list--primary']");

		private static readonly By ChildIndicationLocator = By.XPath("./parent::div[contains(@class,'--secondary')]");

		private static readonly By NarrowFilterTextboxLocator = By.XPath(".//div[contains(@class,'SearchFacet-field')]//input[@class='SearchFacet-inputText']");

		private static readonly By NarrowJurisdictionLabelLocator = By.XPath(".//div[contains(@class,'SearchFacet-field')]//label[@class='SearchFacet-label']");

		private readonly By componentLocator;

		/// <inheritdoc />
		public EdgeIpCheckboxTreeFacetComponent(By componentLocator)
		{
			this.componentLocator = componentLocator;
		}

		/// <summary>
		/// Input textbox for narrowing filter tree
		/// </summary>
		public ITextbox NarrowFilterTextbox => new Textbox(this.ComponentLocator, NarrowFilterTextboxLocator);

		/// <summary>
		/// The facet narrow filter label
		/// </summary>
		public ILabel NarrowFilterLabel => new Label(this.ComponentLocator, NarrowJurisdictionLabelLocator);

		/// <summary>
		/// Return children Filter components list
		/// </summary>
		public List<EdgeCheckboxFilterFacetComponent> ParentFilters => DriverExtensions
			.GetElements(this.ComponentLocator, ParentLabelLocator)
			.Where(webElement => !DriverExtensions.GetElements(webElement, ChildIndicationLocator).Any())
			.Select(webElement => new EdgeCheckboxFilterFacetComponent(webElement)).ToList();

		/// <summary>
		/// The container locator.
		/// </summary>
		protected override By ComponentLocator => this.componentLocator;
	}
}