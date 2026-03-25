namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NewTypeAhead
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Add Content to Content Section - Select Content Typeahead component
    /// </summary>
    public class SelectContentComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.Id("cp_selectContent_autocompleteList");
        private static readonly By ContentSuggestionItemsLocator = By.CssSelector("ul>li.cp_selectContent_autocompleteItem");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get the number of select content typeahead suggestions
        /// </summary>
        /// <returns>Count of select content suggestions</returns>
        public int SelectContentSuggestionItemsCount() => DriverExtensions.GetElements(ContentSuggestionItemsLocator).Count;
    }
}
