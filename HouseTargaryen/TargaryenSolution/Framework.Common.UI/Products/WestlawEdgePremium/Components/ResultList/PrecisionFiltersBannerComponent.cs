namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Filters banner Component
    /// </summary>
    public class PrecisionFiltersBannerComponent : BaseModuleRegressionComponent
    {
        private static readonly By BannerLabelLocator = By.XPath(".//div[@class='PrecisionFiltersBanner-label']");
        private static readonly By EditSelectionsButtonLocator = By.XPath(".//button[contains(@class, 'PrecisionFiltersBanner-reset')]");
        private static readonly By AppliedAttributeLabelLocator = By.XPath(".//div[@class='PrecisionFiltersBanner-selection']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionFiltersBannerComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionFiltersBannerComponent(By componentLocator)
        {
            this.ComponentLocator = componentLocator;
        }

        /// <summary>
        /// Applied attributes labels
        /// </summary>
        public IReadOnlyCollection<ILabel> AppliedAttributesLabels => new ElementsCollection<Label>(this.ComponentLocator, AppliedAttributeLabelLocator);

        /// <summary>
        /// Precision Filters banner label
        /// </summary>
        public ILabel BannerLabel => new Label(this.ComponentLocator, BannerLabelLocator);

        /// <summary>
        /// Precision Filters Edit Selections button
        /// </summary>
        public IButton EditSelectionsButton => new Button(this.ComponentLocator, EditSelectionsButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
