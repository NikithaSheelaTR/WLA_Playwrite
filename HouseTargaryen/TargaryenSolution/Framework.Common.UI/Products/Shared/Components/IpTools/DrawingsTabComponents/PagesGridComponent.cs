namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items.IpDrawings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Pages Grid
    /// </summary>
    public class PagesGridComponent : BaseModuleRegressionComponent
    {
        private static readonly By DrawingPagesLocator = By.XPath(".//li");

        /// <summary>
        /// The Component Locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//ul[contains(@class,'IPDrawings-images')]");

        /// <summary>
        /// Drawing page item list
        /// </summary>
        public List<DrawingPageItem> DrawingPageItemList =>
            DriverExtensions.GetElements(this.ComponentLocator, DrawingPagesLocator)
                            .Select(webEl => new DrawingPageItem(webEl)).ToList();
    }
}