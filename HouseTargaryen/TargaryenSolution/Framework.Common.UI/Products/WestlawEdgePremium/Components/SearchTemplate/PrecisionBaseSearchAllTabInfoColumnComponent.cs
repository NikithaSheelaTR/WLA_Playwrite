namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Search all tab info column component
    /// </summary>
    public class PrecisionBaseSearchAllTabInfoColumnComponent
    {
        private static readonly By DescriptionLabelLocator = By.XPath(".//p");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBaseSearchAllTabInfoColumnComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionBaseSearchAllTabInfoColumnComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Description labels
        /// </summary>
        public IReadOnlyCollection<ILabel> DescriptionLabels => new ElementsCollection<Label>(this.ComponentLocator, DescriptionLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected virtual By ComponentLocator => this.componentLocator;
    }
}
