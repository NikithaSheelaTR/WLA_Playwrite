namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The document drawings component.
    /// </summary>
    public class DrawingsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//strong[text() = 'Drawings']/ancestor::tr[@class = 'co_borderTop']");

        private static readonly By DrawingThumbNailItemLocator = By.XPath(".//li");

        private static readonly By ViewAllDrawingsLinkLocator = By.XPath(".//a[text()='View all drawings']");

        private static readonly By NoDrawingsAvailableLocator = By.XPath(".//div[text()='Check the original PDF for drawings.']");

        /// <summary>
        /// The document drawings component.
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Thumbnails items list. 
        /// </summary>
        public ItemsCollection<IpDrawingThumbnailItem> Drawings => new ItemsCollection<IpDrawingThumbnailItem>(ComponentLocator, DrawingThumbNailItemLocator);
                                                                          
        /// <summary>
        /// View all drawings link 
        /// </summary>
        public ILink ViewAllDrawingsLink => new Link(ContainerLocator, ViewAllDrawingsLinkLocator);

        /// <summary>
        /// No drawings available 
        /// </summary>
        public ILabel NoDrawingsAvailableLabel => new Label(ContainerLocator, NoDrawingsAvailableLocator);
    }
}
