namespace Framework.Common.UI.Products.Shared.Components.Toolbar.CustomToolbars
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar;

    using OpenQA.Selenium;

    /// <summary>
    /// Quick Check Right panel toolbar component
    /// </summary>
    public class RightPanelQuickCheckToolbar : QuickCheckToolbar
    {
        private static readonly By DeliveryDropdownLocator = By.XPath("//div[@class='co_deliveryWidget']/div[contains(@id, 'deliveryWidget')]");
        private static readonly By GoToSelectedTextButtonLocator = By.XPath("//button[contains(.,'Go to selected text')]");
        private static readonly By QuickCheckMaximizeButtonLocator = By.XPath(".//div[@class != 'co_hideState']//button[contains(@class, 'blue')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="RightPanelQuickCheckToolbar"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public RightPanelQuickCheckToolbar(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Gets the delivery dropdown
        /// </summary>
        public override DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(DeliveryDropdownLocator);

        /// <summary>
        /// Go to selected text button.
        /// </summary>
        public IButton GoToSelectedTextButton { get; } = new Button(GoToSelectedTextButtonLocator);

        /// <summary>
        /// Maximize/Collapse Right panel button
        /// </summary>
        public IToggle MaximizePanelButton => new Toggle(this.Container, QuickCheckMaximizeButtonLocator, "aria-pressed", "false");
    }
}