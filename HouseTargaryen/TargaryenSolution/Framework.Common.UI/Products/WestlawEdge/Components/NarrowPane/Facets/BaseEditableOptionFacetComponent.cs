namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class fot NOT LPA Indigo facet
    /// It can be use for Docket Number and Party facets
    /// </summary>s
    public class BaseEditableOptionFacetComponent : BaseSearcheableFacetComponent
    {
        private static readonly By RemoveButtonLocator = By.XPath(".//span[contains(.,'Remove')]");

        private static readonly By AppliedItemLocator = By.XPath(".//span[contains(@class,'SearchFacet-outputTextValue')]");

        private static readonly By ApplyButtonLocator = By.XPath(".//button[contains(@class,'SearchFacet-buttonSubmit')]");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEditableOptionFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public BaseEditableOptionFacetComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        /// <summary>
        /// Enter text into search field and click on the Go or Continue (with turn on Select multiple) button
        /// </summary>
        /// <param name="text">Query for search</param>
        /// <returns>T</returns>
        public T EnterTextAndApplyIt<T>(string text) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            this.EnterTextInFindField(text);
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), ApplyButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets applying query text 
        /// </summary>
        public string GetOutputQueryText() => DriverExtensions.WaitForElement(AppliedItemLocator).Text;

        /// <summary>
        /// Click Remove button
        /// </summary>
        /// <returns>T</returns>
        public T ClickRemoveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(RemoveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}