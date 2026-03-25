namespace Framework.Common.UI.Raw.WestlawEdge.Items.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// View saved comparison tab item
    /// </summary>
    public class ViewSavedComparisonsTabItem : BaseItem
    {
        private static readonly By DeleteButtonLocator = By.XPath(".//button[@title = 'Delete']");
        private static readonly By NotAddedLabelLocator = By.XPath(".//div[text() = '[NOT ADDED]']");
        private static readonly By SaveButtonLocator = By.XPath(".//button[@title = 'Save']");
        private static readonly By ReportButtonLocator = By.XPath(".//button[@class = 'co_redlineLightbox_tabItem_reportLink co_textLeft']");
        private static readonly By MetadataLocator = By.XPath(".//p[contains(@class, 'Meta')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="ViewSavedComparisonsTabItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public ViewSavedComparisonsTabItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Report button
        /// </summary>
        public IButton Report => new Button(this.Container, ReportButtonLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton => new Button(this.Container, DeleteButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(this.Container, SaveButtonLocator);

        /// <summary>
        /// Not added label
        /// </summary>
        public ILabel NotAddedLabel => new Label(this.Container, NotAddedLabelLocator);

        /// <summary>
        /// Metadata
        /// </summary>
        public ILabel Metadata => new Label(this.Container, MetadataLocator);
    }
}