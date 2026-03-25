namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Image tabs
    /// </summary>
    public class ImageVariantsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ImageVariantsComponentlocator = By.XPath("//div[contains(@class,'Tab-container')]");

        private static readonly By ImageTabslocator = By.XPath(".//li[contains(@class,'Tab')]");

        private static readonly By ImageLinkLocator = By.XPath(".//a");

        private static readonly By ImageLocator = By.XPath(".//img");

        private static readonly By ImageTabLabelLocator = By.XPath(".//div");

        /// <inheritdoc />
        protected override By ComponentLocator { get; } = ImageVariantsComponentlocator;

        /// <summary>
        /// Image variant list
        /// </summary>
        public List<ImageVariantItem> ImageVariantItemList =
            DriverExtensions.GetElements(ImageVariantsComponentlocator, ImageTabslocator).Select(item => 
                new ImageVariantItem(item, new Link(item, ImageLinkLocator), new Label(item, ImageTabLabelLocator), new Image(item, ImageLocator))
            ).ToList();
    }
}