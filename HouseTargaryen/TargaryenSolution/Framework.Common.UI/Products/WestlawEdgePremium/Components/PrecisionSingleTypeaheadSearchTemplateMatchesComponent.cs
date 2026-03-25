namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision single typeahead search all tab filters component
    /// </summary>
    public class PrecisionSingleTypeaheadSearchTemplateMatchesComponent : PrecisionBaseMatchesComponent
    {
        private static readonly By FilterLabelLocator = By.XPath(".//section//h4");
        private static readonly By InfoIconLocator = By.XPath(".//button[@class='co_scopeIcon']");
        private static readonly By AddButtonLocator = By.XPath(".//*[contains(@class, 'PrecisionSearch-addTermButton')]");
        private static readonly By WarningMessageLabelLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By AddTermLabelLocator = By.ClassName("PrecisionSearch-addTerm");

        /// <summary>
        /// The container.
        /// </summary>
        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionAdditionalMatchesComponent "/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionSingleTypeaheadSearchTemplateMatchesComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
            DriverExtensions.WaitForElementDisplayed(componentLocator);
        }

        /// <summary>
        /// Add term label 
        /// </summary>
        public ILabel AddTermLabel => new Label(this.ComponentLocator, AddTermLabelLocator);

        /// <summary>
        /// Warning message label
        /// </summary>
        public IButton WarningMessageLabel => new Button(this.ComponentLocator, WarningMessageLabelLocator);

        /// <summary>
        /// Add material facts button 
        /// </summary>
        public IButton AddMaterialFactsButton => new Button(this.ComponentLocator, AddButtonLocator);

        /// <summary>
        /// Info icon buttons
        /// </summary>
        public IReadOnlyCollection<IButton> InfoIconButtons => new ElementsCollection<Button>(this.ComponentLocator, InfoIconLocator);

        /// <summary>
        /// Additional filters labels
        /// </summary>
        public IReadOnlyCollection<ILabel> FiltersLabels => new ElementsCollection<Label>(this.ComponentLocator, FilterLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;
    }
}
