namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Selections component
    /// </summary>
    public class PrecisionSelectionsComponent : BaseModuleRegressionComponent
    {
        private const string ClearButtonLctMask = ".//div[@class='PrecisionSearch-selection']//span[text()='{0}']//parent::*/following-sibling::button";
        private const string FacetClearButtonLctMask = ".//div[@class='PrecisionSearch-categoryHeading']//h4[text()='{0}']/following-sibling::button";

        private static readonly By ContainerLocator = By.XPath("//div[@class='PrecisionSearchModal-selections']");
        private static readonly By CasesCountLabelLocator = By.XPath(".//*[@class='PrecisionSearchModal-caseCount']/span/following-sibling::span");
        private static readonly By ClearAllButtonLocator = By.XPath(".//button[contains(@class, 'clearAll')]");
        private static readonly By FacetLabelLocator = By.XPath(".//*[@class='PrecisionSearch-categoryHeading']");
        private static readonly By SelectedItemLocator = By.XPath(".//div[@class='PrecisionSearch-selection']//div | .//div[@class='PrecisionSearch-selection']/span");

        /// <summary>
        /// Selected items labels
        /// </summary>
        public IReadOnlyCollection<ILabel> SelectedItemsLabels => new ElementsCollection<Label>(this.ComponentLocator, SelectedItemLocator);

        /// <summary>
        /// Facet labels
        /// </summary>
        public IReadOnlyCollection<ILabel> FacetLabels => new ElementsCollection<Label>(this.ComponentLocator, FacetLabelLocator);

        /// <summary>
        /// Cases count label
        /// </summary>
        public ILabel CasesCountLabel => new Label(this.ComponentLocator, CasesCountLabelLocator);

        /// <summary>
        /// Clear All button
        /// </summary>
        public IButton ClearAllButton => new Button(this.ComponentLocator, ClearAllButtonLocator);

        /// <summary>
        /// Clear button
        /// </summary>
        public IButton ClearButton(string itemToClear) => new Button(this.ComponentLocator, By.XPath(string.Format(ClearButtonLctMask, itemToClear)));

        /// <summary>
        /// Facet clear button
        /// </summary>
        public IButton FacetClearButton(string itemToClear) => new Button(this.ComponentLocator, By.XPath(string.Format(FacetClearButtonLctMask, itemToClear)));

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
