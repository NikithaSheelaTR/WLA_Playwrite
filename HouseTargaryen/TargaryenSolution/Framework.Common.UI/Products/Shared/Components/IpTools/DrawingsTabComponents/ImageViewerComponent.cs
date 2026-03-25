namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Image Viewer for Drawings zoom
    /// </summary>
    public sealed class ImageViewerComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By BackToDrawingsButtonLocator = By.XPath(".//a[contains(@class,'DocumentBackToButton')]");

        private static readonly By DrawingPageHeadingLocator = By.XPath(".//*[contains(@id,'IPDrawings-imagesHeader')]");

        private static readonly By PreviousImageButtonLocator = By.XPath(".//button[contains(@id,'coid_patentDrawings_previousImage')]");

        private static readonly By NextImageButtonLocator = By.XPath(".//button[contains(@id,'coid_patentDrawings_nextImage')]");

        private static readonly By ZoomToggleCheckboxLocator = By.XPath(".//input[contains(@id,'coid_patentDrawings_toggleZoom')]");

        private static readonly By MagnifiedImageLocator = By.XPath(".//*[contains(@id,'coid_patentDrawings_zoomedImg')]//img");

        private static readonly By OriginalImageLocator = By.XPath(".//div[contains(@class,'PatentDrawings-zoomImage-original')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//div[contains(@class,'ipDrawingsResult')]");

        /// <summary>
        /// Close dialog button
        /// </summary>
        public IButton BackToDrawingsButton => new Button(this.ComponentLocator, BackToDrawingsButtonLocator);

        /// <summary>
        /// Drawing Page Heading Label
        /// </summary>
        public ILabel DrawingPageHeadingLabel => new Label(this.ComponentLocator, DrawingPageHeadingLocator);

        /// <summary>
        /// Previous Image Button
        /// </summary>
        public IButton PreviousImageButton => new Button(this.ComponentLocator, PreviousImageButtonLocator);

        /// <summary>
        /// Next Image Button
        /// </summary>
        public IButton NextImageButton => new Button(this.ComponentLocator, NextImageButtonLocator);

        /// <summary>
        /// Zoom Toggle Checkbox
        /// </summary>
        public ICheckBox ZoomToggleCheckbox => new CheckBox(this.ComponentLocator, ZoomToggleCheckboxLocator);

        /// <summary>
        /// Magnified Image
        /// </summary>
        public IImage MagnifiedImage => new Image(this.ComponentLocator, MagnifiedImageLocator);

        /// <summary>
        /// Original Image
        /// </summary>
        public IImage OriginalImage => new Image(this.ComponentLocator, OriginalImageLocator);
    }
}
