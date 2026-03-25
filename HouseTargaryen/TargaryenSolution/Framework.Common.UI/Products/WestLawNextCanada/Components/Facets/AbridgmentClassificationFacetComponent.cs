namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Abridgment Classification Facet Component
    /// </summary>
    public class AbridgmentClassificationFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By AbridgmentClassificationFacetLocator = By.CssSelector("#facet_div_wlncMetaDocAbridgmentClassification");
        private static readonly By FacetExpandIconLocator = By.XPath("//button[@id='co_facet_selectLink_wlncMetaDocAbridgmentClassification']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => AbridgmentClassificationFacetLocator;

        /// <summary>
        ///  Expand Facet 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Dialog instance</returns>
        public T ExpandFacetToOpenDialog<T>() where T : BaseModuleRegressionDialog
        {
            DriverExtensions.Click(ComponentLocator, FacetExpandIconLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}