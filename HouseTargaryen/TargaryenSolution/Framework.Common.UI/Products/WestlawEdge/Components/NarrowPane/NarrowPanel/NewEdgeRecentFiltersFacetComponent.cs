namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeRecentFiltersFacetComponent for new Narrow panel
    /// </summary>
    public class NewEdgeRecentFiltersFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By RestoreFiltersButtonLocator = By.XPath(".//button[@class = 'co_btnGray co_btnBack' and . = 'Restore previous']");
        private static readonly By ClearButtonLocator = By.XPath(".//button[@class = 'co_btnGray co_btnBack' and (. = 'Effacer' or .='Clear')]");
        private static readonly By ApplyButtonLocator = By.XPath(".//button[@class = 'co_multifacet_apply']");
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'MultipleFilter-controls']");
        
        /// <summary>
        /// RestoreFiltersButton
        /// </summary>
        public IButton RestoreFiltersButton => new Button(this.ComponentLocator, RestoreFiltersButtonLocator);

        /// <summary>
        /// ClearButtonLocator
        /// </summary>
        public IButton ClearButton => new Button(this.ComponentLocator, ClearButtonLocator);

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new Button(this.ComponentLocator, ApplyButtonLocator);

        /// <summary>
        /// ApplyButtons
        /// </summary>
        public IReadOnlyCollection<IButton> ApplyButtons =>
            new ElementsCollection<Button>(this.ComponentLocator, ApplyButtonLocator);

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
