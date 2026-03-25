namespace Framework.Common.UI.Raw.WestlawEdge.Items.CompareText
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Select to compare tab item
    /// </summary>
    public class SelectToCompareTabItem : BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input");
        private static readonly By DeleteButtonLocator = By.XPath(".//button[@title = 'Delete']");
        private static readonly By PrimaryBadgeLocator = By.XPath(".//span[@class = 'Badge badge--mutedIndigo']");
        private static readonly By TitleLocator = By.XPath(".//p[@class = 'co_redlineLightbox_tabItem_itemTitle co_bold']/label");
        private static readonly By ShowMoreButtonLocator = By.XPath(".//button[text() = 'Show more']");
        private static readonly By ShowLessButtonLocator = By.XPath(".//button[text() = 'Show less']");
        private static readonly By TextLocator = By.XPath(".//div[contains(@class , 'itemData')]/div[not(@*)]/span[1]");
        private static readonly By NotAddedLabelLocator = By.XPath(".//span[text() = '[NOT ADDED]']");
        private static readonly By MetadataLocator = By.XPath(".//div[@class = 'Meta']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="SelectToCompareTabItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public SelectToCompareTabItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckboxLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton => new Button(this.Container, DeleteButtonLocator);

        /// <summary>
        /// Show more button
        /// </summary>
        public IButton ShowMoreButton => new Button(this.Container, ShowMoreButtonLocator);

        /// <summary>
        /// Show less button
        /// </summary>
        public IButton ShowLessButton => new Button(this.Container, ShowLessButtonLocator);

        /// <summary>
        /// Primary badge
        /// </summary>
        public ILabel PrimaryBadge => new Label(this.Container, PrimaryBadgeLocator);

        /// <summary>
        /// Title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);

        /// <summary>
        /// Text
        /// </summary>
        public ILabel Text => new Label(this.Container, TextLocator);

        /// <summary>
        /// Not added label
        /// </summary>
        public ILabel NotAddedLabel => new Label(this.Container, NotAddedLabelLocator);

        /// <summary>
        /// Metadata
        /// </summary>
        public IReadOnlyCollection<ILabel> Metadata =>
            new ElementsCollection<Label>(this.Container, MetadataLocator);
    }
}