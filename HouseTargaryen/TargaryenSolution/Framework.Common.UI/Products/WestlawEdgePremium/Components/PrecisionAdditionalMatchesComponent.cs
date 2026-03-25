namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision additional filters facet component
    /// </summary>
    public class PrecisionAdditionalMatchesComponent : PrecisionBaseMatchesComponent
    {
        private static readonly By AdditionalFilterLabelLocator = By.XPath(".//*[@class='PrecisionSearch-moreMatchesSubHead']//h4 | .//h3/following-sibling::div/h3");

        /// <summary>
        /// The container.
        /// </summary>
        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionAdditionalMatchesComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionAdditionalMatchesComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
            DriverExtensions.WaitForElementDisplayed(componentLocator);
        }

        /// <summary>
        /// Additional filters labels
        /// </summary>
        public IReadOnlyCollection<ILabel> AdditionalFiltersLabels => new ElementsCollection<Label>(this.ComponentLocator, AdditionalFilterLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;
    }
}
