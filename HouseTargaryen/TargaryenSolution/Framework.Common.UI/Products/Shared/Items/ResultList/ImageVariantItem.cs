namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// ImageVariant Item
    /// </summary>
    public class ImageVariantItem : BaseItem
    {
        /// <summary>
        /// Tab Item
        /// </summary>
        public ILabel Tabitem;
        
        /// <summary>
        /// Tab link
        /// </summary>
        public ILink TabLink;

        /// <summary>
        /// Tab label
        /// </summary>
        public ILabel TabLabel;

        /// <summary>
        /// Tab image
        /// </summary>
        public IImage TabImage;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="basElement"></param>
        /// <param name="tabLink"></param>
        /// <param name="tabLabel"></param>
        /// <param name="tabImage"></param>
        public ImageVariantItem(IWebElement basElement, ILink tabLink, ILabel tabLabel, IImage tabImage)
            : base(basElement)
        {
            this.Tabitem = new Label(basElement);
            this.TabLink = tabLink;
            this.TabLabel = tabLabel;
            this.TabImage = tabImage;
        }
    }
}