namespace Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Infobox with checkbox
    /// </summary>
    public sealed class InfoBoxWithCheckbox : InfoBox, IInfoboxWithCheckbox
    {
        private readonly By checkboxLocator;
        private readonly By titleLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoBoxWithCheckbox"/> class.
        /// </summary>
        /// <param name="containerLocator">Get container locator</param>
        /// <param name="okayButtonLocator">Get Okay button locator</param>
        /// <param name="checkboxLocator">Get checkbox locator</param>
        /// <param name="textLocator">Get text locator</param>
        /// <param name="titleLocator">Get title locator</param>
        public InfoBoxWithCheckbox(
            By containerLocator,
            By okayButtonLocator,
            By checkboxLocator,
            By textLocator,
            By titleLocator = null)
            : base(containerLocator, textLocator, okayButtonLocator)
        {
            this.checkboxLocator = checkboxLocator;
            this.titleLocator = titleLocator;
        }

        /// <summary>
        /// Tooltip title
        /// </summary>
        public ILabel Title => new Label(this.GetContainer(), this.titleLocator);

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.GetContainer(), this.checkboxLocator);
    }
}
