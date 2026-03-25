namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// The quick check landing tile component.
    /// </summary>
    public class QuickCheckLandingTileComponent : BaseModuleRegressionComponent
    {
        private static readonly By DescriptionLocator = By.XPath(".//ul/li");
        private static readonly By FeatureDisabledLabelLocator = By.XPath(".//*[@class='co_infoBox warning']");
        private static readonly By QuickCheckIconLocator = By.XPath(".//span[@class='DA-IconsLarge DA-CheckDraftOpinionIcon']");
        private static readonly By TitleOfTileLabelLocator = By.XPath(".//h4");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckLandingTileComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckLandingTileComponent(By container)
        {
            this.ComponentLocator = container;
        }

        /// <summary>
        /// Feature disabled label
        /// </summary>
        public ILabel FeatureDisabledLabel => new Label(this.ComponentLocator, FeatureDisabledLabelLocator);

        /// <summary>
        /// The descriptions.
        /// </summary>
        public IReadOnlyCollection<ILabel> DescriptionLabels => new ElementsCollection<Label>(this.ComponentLocator, DescriptionLocator);

        /// <summary>
        /// Quick check icon
        /// </summary>
        public ILabel QuickCheckIcon => new Label(this.ComponentLocator, QuickCheckIconLocator);

        /// <summary>
        /// Title of Tile
        /// </summary>
        public ILabel TitleOfTileLabel => new Label(this.ComponentLocator, TitleOfTileLabelLocator);

        /// <summary>
        /// The component locator.
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}