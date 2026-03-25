namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The Drawing section component.
    /// </summary>
    public class DrawingSectionComponent : BaseModuleRegressionComponent
    { 
        private static readonly By ContainerLocator = By.XPath(".//span[@class = 'co_drawings_count']/parent::div[not(contains(@class,'co_hideState'))]");
        
        private static readonly By DrawingsLabelLocator = By.XPath("./span");

        private static readonly By DrawingThumbNailItemLocator = By.XPath(".//li");

        private static readonly By ViewAllDrawingsLinkLocator = By.XPath("./a[text() ='View all drawings']");

        /// <summary>
        /// The Drawing section component.
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private IWebElement Container { get; }

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public DrawingSectionComponent(IWebElement container)
        {
            this.Container = DriverExtensions.SafeGetElement(container, ContainerLocator);
        }

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        public override bool IsDisplayed() => ElementExtensions.IsDisplayed(this.Container);

        /// <summary>
        /// Thumbnails items list. 
        /// </summary>
        public List<IpDrawingThumbnailItem> Drawings() => DriverExtensions.GetElements(Container, DrawingThumbNailItemLocator)
                                                                          .Select(elem => new IpDrawingThumbnailItem(elem)).ToList();

        /// <summary>
        /// Section label
        /// </summary>
        public ILabel SectionLabel => new Label(this.Container, DrawingsLabelLocator);

        /// <summary>
        /// View all drawings link
        /// </summary>
        public ILink ViewAllDrawings => new Link(this.Container, ViewAllDrawingsLinkLocator);
    }
}
