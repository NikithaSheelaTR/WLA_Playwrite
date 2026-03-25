namespace Framework.Common.UI.Raw.WestlawEdge.Items.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Alert Grid item
    /// </summary>
    public class AlertViewItem : BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input");
        private static readonly By AlertNameLocator = By.XPath(".//h3//label");
        private static readonly By EditAlertLinkLocator = By.XPath(".//a[@class = 'co_editAlert']");
        private static readonly By HistoryLinkLocator = By.XPath(".//a[@class = 'co_alertHistory']");
        private static readonly By LastUpdateLocator = By.XPath(".//li[contains(text(), 'Last Update')]");
        private static readonly By NextUpdateLocator = By.XPath(".//li[contains(text(), 'Next Update')]");
        private static readonly By ClientIdLocator = By.XPath(".//li[contains(text(), 'Client ID')]");
        private static readonly By DescriptionLocator = By.XPath(".//li[contains(@id, 'alertDescriptionID')]");
        private static readonly By CategoryTextLocator = By.XPath("//ul[starts-with(@id, 'alertCategories_')]");
   
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="AlertViewItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public AlertViewItem(IWebElement container) : base(container)
        { }

        /// <summary>
        /// Edit link
        /// </summary>
        public ILink EditAlertLink => new Link(this.Container, EditAlertLinkLocator);

        /// <summary>
        /// History link
        /// </summary>
        public ILink HistoryLink => new Link(this.Container, HistoryLinkLocator);

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckboxLocator);

        /// <summary>
        /// Alert name
        /// </summary>
        public ILabel AlertName => new Label(this.Container, AlertNameLocator);

        /// <summary>
        /// Last update
        /// </summary>
        public ILabel LastUpdate => new Label(this.Container, LastUpdateLocator);

        /// <summary>
        /// Next Update
        /// </summary>
        public ILabel NextUpdate => new Label(this.Container, NextUpdateLocator);

        /// <summary>
        /// Client Id
        /// </summary>
        public ILabel ClientId => new Label(this.Container, ClientIdLocator);

        /// <summary>
        /// Alert description
        /// </summary>
        public ILabel Description => new Label(this.Container, DescriptionLocator);

        /// <summary>
        /// Category
        /// </summary>
        public ILabel Category => new Label(this.Container, CategoryTextLocator);
    }
}