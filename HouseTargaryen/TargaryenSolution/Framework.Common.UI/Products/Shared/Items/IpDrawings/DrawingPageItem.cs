namespace Framework.Common.UI.Products.Shared.Items.IpDrawings
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Drawing page item
    /// </summary>
    public class DrawingPageItem : BaseItem
    {
        private static readonly By PageCheckBoxLocator =
            By.XPath(".//input[contains(@class,'IPDrawings-pageCheckbox')]");

        private static readonly By PageAccessibilityLabelLocator =
            By.XPath(".//label[contains(@class,'accessibilityLabel')]");

        private static readonly By PageImageLocator = By.XPath(".//a");

        private static readonly By PageNumberLabelLocator = By.XPath(".//div[not(@class)]");

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="itemWebElement"></param>
        public DrawingPageItem(IWebElement itemWebElement)
            : base(itemWebElement)
        {
        }

        /// <summary>
        /// Page checkbox
        /// </summary>
        public ICheckBox PageCheckBox => new CheckBox(this.Container, PageCheckBoxLocator);

        /// <summary>
        /// Page Accessibility Label
        /// </summary>
        public ILabel PageAccessibilityLabel => new Label(this.Container, PageAccessibilityLabelLocator);

        /// <summary>
        /// Page Image
        /// </summary>
        public IImage PageImage => new Image(this.Container, PageImageLocator);

        /// <summary>
        /// Page Label
        /// </summary>
        public ILabel PageNumberLabel => new Label(this.Container, PageNumberLabelLocator);
    }
}