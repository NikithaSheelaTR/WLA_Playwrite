namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
	using Framework.Common.UI.Interfaces;
	using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DocumentsInFoldersFacetComponent
    /// </summary>
    public class DocumentsInFoldersFacetComponent : BaseFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_foldered");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

	    /// <summary>
	    /// Gets the AnnotatedDocumentsFacetOptions enumeration to WebElementInfo map.
	    /// </summary>
	    protected EnumPropertyMapper<FolderedDocumentFacetOptions, WebElementInfo> FolderedFacetsMap { get; } = EnumPropertyModelCache.GetMap<FolderedDocumentFacetOptions, WebElementInfo>();

		/// <summary>
		/// IsCheckboxSelected
		/// </summary>
		/// <param name="option">The option.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		public bool IsCheckboxSelected(FolderedDocumentFacetOptions option)
			=> DriverExtensions.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.FolderedFacetsMap[option].LocatorString)));

	    /// <summary>
	    /// Apply the specified checkbox
	    /// </summary>
	    /// <typeparam name="T">T</typeparam>
	    /// <param name="option">The option.</param>
	    /// <param name="desiredState">desiredState</param>
	    /// <returns>The new instance of T page.</returns>
	    public T SetCheckbox<T>(FolderedDocumentFacetOptions option, bool desiredState = true)
		    where T : ICreatablePageObject
	    {
		    DriverExtensions.SetCheckbox(desiredState, this.ComponentLocator, By.XPath(this.FolderedFacetsMap[option].LocatorString));
		    return DriverExtensions.CreatePageInstance<T>();
	    }
    }
}