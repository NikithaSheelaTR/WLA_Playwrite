namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
	using System.Collections.Generic;

	using Framework.Common.UI.Interfaces.Elements;
	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
	using Framework.Common.UI.Products.Shared.Elements.Labels;
	using Framework.Common.UI.Products.Shared.Elements.Links;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// class for parent filter
	/// </summary>
	public class CheckboxFilterForIpFacetComponent : BaseModuleRegressionComponent
	{
		private static readonly By LabelLocator = By.XPath("./label");

		private static readonly By LabelCountLocator = By.XPath("./span[@class='co_facetCount']");

		private static readonly By ExpandCollapselinkLocator =
			By.XPath("./a[@class='co_facet_expand' or @class='co_facet_collapse']");

		private static readonly By CheckBoxLocator = By.XPath("./input[@type='checkbox']");

		private static readonly By ChildrenFiltersLocator = By.XPath("./div[contains(@id,'facet_hierarchy_children')]");

		private readonly By containerLocator;

		private readonly IWebElement containerWebElement;

		/// <summary>
		/// The Constructor
		/// </summary>
		/// <param name="locatorBy"></param>
		public CheckboxFilterForIpFacetComponent(By locatorBy)
		{
			this.containerLocator = locatorBy;
			this.containerWebElement = DriverExtensions.GetElement(locatorBy);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckboxFilterForIpFacetComponent"/> class. 
		/// The Constructor
		/// </summary>
		/// <param name="webElement">
		/// </param>
		public CheckboxFilterForIpFacetComponent(IWebElement webElement)
		{
			this.containerWebElement = webElement;
		}

		/// <summary>
		/// Filter label
		/// </summary>
		public ILabel ParentLabel => new Label(this.containerWebElement, LabelLocator);

		/// <summary>
		/// Filter Checkbox
		/// </summary>
		public ICheckBox ParentCheckBox => new CheckBox(this.containerWebElement, CheckBoxLocator);

		/// <summary>
		/// Expand/collapse link
		/// </summary>
		public ILink ExpandCollapseLink => new Link(this.containerWebElement, ExpandCollapselinkLocator);

		/// <summary>
		/// Filter count label
		/// </summary>
		public ILabel ParentFilterCount => new Label(this.containerWebElement, LabelCountLocator);

		/// <inheritdoc />
		protected override By ComponentLocator => this.containerLocator;

		/// <inheritdoc />
		public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.containerWebElement);

		/// <summary>
		/// Child Filter list
		/// </summary>
		public List<CheckboxFilterForIpFacetComponent> GetChildFilterList()
		{
			var result = new List<CheckboxFilterForIpFacetComponent>();
			foreach (var childElement in DriverExtensions.GetElements(this.containerWebElement, ChildrenFiltersLocator, IpJurisdictionFacetComponent.ParentLabelLocator))
			{
				result.Add(new CheckboxFilterForIpFacetComponent(childElement));
			}

			return result;
		}
	}
}