namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Descriptions component
    /// </summary>
    public class DescriptionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By DescriptionsHeaderLabelLocator = By.XPath(".//h3[@class='IPDrawings-descriptionsHeader']");

        private static readonly By DescriptionsCardListLocator = By.XPath(".//ul[@class='IPDrawings-descriptions']//li/div");

        /// <inheritdoc />
        protected override By ComponentLocator => By.XPath("//div[@class='IPDrawings-descriptions']");

        /// <summary>
        /// Descriptions header label
        /// </summary>
        public ILabel DescriptionsHeaderLabel => new Label(this.ComponentLocator, DescriptionsHeaderLabelLocator);

        /// <summary>
        /// List of description cards
        /// </summary>
        public IReadOnlyCollection<ILabel> DescriptionCardLabelsList =>
            DriverExtensions.GetElements(this.ComponentLocator, DescriptionsCardListLocator)
                            .Select(webEl => new Label(webEl)).ToList();
    }
}