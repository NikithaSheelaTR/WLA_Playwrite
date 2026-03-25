namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;

    /// <summary>
    /// Canada Result List Item
    /// </summary>
    public class CanadaResultListItem : EdgeResultListItem
    {
        private static readonly By AddToKeepListButtonLocator = By.XPath(".//input[@aria-checked='false' and @class='Keeplist-checkbox-input']");
        private static readonly By RemoveFromKeepListButtonLocator = By.XPath(".//input[@aria-checked='true' and contains(@class,'Keeplist-checkbox-input')]");
        private static readonly By HideDetailsLocator = By.XPath(".//button[contains(@class, 'KeepList-itemsToggle')]");
        private static readonly By AddToKeepListToolTipLocator = By.XPath("//*[@id=//input[@aria-checked='false' and contains(@class,'Keeplist-checkbox-input')]/@aria-describedby]");
        private static readonly By RemoveFromKeepListToolTipLocator = By.XPath("//*[@id=//input[@aria-checked='true' and contains(@class,'Keeplist-checkbox-input')]/@aria-describedby]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaResultListItem"/> class. 
        /// The constructor
        /// </summary>
        /// <param name="container"> The container. </param>
        public CanadaResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Details Hide/Show Toggle button
        /// </summary>
        public IButton HideDetailsToggleButton => new Button(this.Container, HideDetailsLocator);

        /// <summary>
        /// Athens Add to Keep List button
        /// </summary>
        public IButton AddToKeepListButton => new Button(this.Container, AddToKeepListButtonLocator);

        /// <summary>
        /// Athens Remove from Keep List button
        /// </summary>
        public IButton RemoveFromKeepListButton => new Button(this.Container, RemoveFromKeepListButtonLocator);

        /// <summary>
        /// Athens Add to Keep List Tooltip
        /// </summary>
        public IButton AddToKeepListTooltip => new Button(this.Container, AddToKeepListToolTipLocator);

        /// <summary>
        /// Athens Remove from Keep List Tooltip
        /// </summary>
        public IButton RemoveFromKeepListTooltip => new Button(this.Container, RemoveFromKeepListToolTipLocator);
    }

}