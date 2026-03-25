namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Component for uploading files in Quick Check feature.
    /// </summary>
    public class UploadFileComponent : BaseModuleRegressionComponent
    {
        private static readonly By UploadTitleLocator = By.XPath(".//h4");
        private static readonly By UploadIconLocator = By.XPath("//div[@class='DA-UploadIcon DA-IconsLarge']");
        private static readonly By UploadFileButtonLocator = By.Id("coid_ba_documentUploadButton");
        private static readonly By UploadDragFileTextLocator = By.XPath(".//p");

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UploadFileComponent(By container)
        {
            this.ComponentLocator = container;
        }

        /// <summary>
        /// Label for the upload title in the Quick Check feature.
        /// </summary>
        public ILabel UploadTitleLabel => new Label(this.ComponentLocator, UploadTitleLocator);

        /// <summary>
        /// Upload Icon for Quick Check page.
        /// </summary>
        public ILabel UploadIcon => new Label(this.ComponentLocator, UploadIconLocator);

        /// <summary>
        /// Gets the button for uploading files in Quick Check page.
        /// </summary>
        public IButton UploadFileButton => new Button(this.ComponentLocator, UploadFileButtonLocator);

        /// <summary>
        /// Label for the text indicating to drag and drop files for upload in Quick Check feature.
        /// </summary>
        public ILabel UploadDragFileLabel => new Label(this.ComponentLocator, UploadDragFileTextLocator);

        /// <summary>
        /// Gets the upload file component for Quick Check page.
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}