namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
	using System.Collections.Generic;

	using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Patent Type Facet
	/// </summary>
	public class IpPatentTypeFacetComponent : BaseFacetComponent
	{
		/// <summary>
		/// The component container locator.
		/// </summary>
		private static readonly By ComponentContainerLocator =
			By.XPath("//div[@id='facet_div_patentsPatentType' and (//h4[text()='Patent Type'])]");

		internal static readonly By ParentLabelLocator = By.XPath("./ul/li");

		/// <summary>
		/// Component locator
		/// </summary>
		protected override By ComponentLocator => ComponentContainerLocator;

		/// <summary>
		/// List of Filter Components
		/// </summary>
		/// <returns>
		/// The <see cref="List{T}"/> of ipCheckBoxFilterComponent's </returns>
		public List<CheckboxFilterForIpFacetComponent> GetIpPatentTypesFilterList()
		{
			var result = new List<CheckboxFilterForIpFacetComponent>();
			foreach (var childElement in DriverExtensions.GetElements(this.ComponentLocator, ParentLabelLocator))
			{
				result.Add(new CheckboxFilterForIpFacetComponent(childElement));
			}

			return result;
		}
	}
}