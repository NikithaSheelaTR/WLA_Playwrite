
namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Thumbnail drawing item
    /// </summary>
    public class IpDrawingThumbnailItem : BaseItem
    {
        private static readonly By DrawingLinkLocator = By.XPath(".//a");

        private static readonly By DrawingImageLocator = By.XPath(".//img");

        private static readonly By DrawingsCountLocator = By.XPath(".//span");

        /// <inheritdoc />
        public IpDrawingThumbnailItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Thumbnail
        /// </summary>
        public IButton Thumbnail => new Button(this.Container);

        /// <summary>
        /// Drawing Link
        /// </summary>
        public ILink Link => new Link(this.Container, DrawingLinkLocator);

        /// <summary>
        /// Image
        /// </summary>
        public IImage Image => new Image(this.Container, DrawingImageLocator);

        /// <summary>
        /// Count
        /// </summary>
        public ILabel Count => new Label(this.Container, DrawingsCountLocator);
    }
}
