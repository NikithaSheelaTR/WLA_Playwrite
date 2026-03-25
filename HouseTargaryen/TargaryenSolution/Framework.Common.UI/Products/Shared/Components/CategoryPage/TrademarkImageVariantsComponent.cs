namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Trademark Image Variants component
    /// </summary>
    public class TrademarkImageVariantsComponent : BaseModuleRegressionComponent
    {
        private static readonly By VariantContainer = By.XPath(".//div[contains(@class,'ImageVariantContainer')]");

        private static readonly By ImageVariantLocator = By.XPath("./img[contains(@class,'ImageVariant')]");

        private static readonly By LabelVariantLocator = By.XPath("./div[contains(@class,'ImageVariantType')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; } = By.XPath("//div[contains(@class,'TrademarkImageVariants')]");

        /// <summary>
        /// Image Variants dictionary
        /// </summary>
        public Dictionary<Label, Image> ImageVariantsDictionary =>
            DriverExtensions.GetElements(this.ComponentLocator, VariantContainer)
                            .Select(
                                item => new
                                {
                                    Label = new Label(item, LabelVariantLocator),
                                    Image = new Image(item, ImageVariantLocator)
                                }).ToDictionary(pair => pair.Label, pair => pair.Image);
    }
}