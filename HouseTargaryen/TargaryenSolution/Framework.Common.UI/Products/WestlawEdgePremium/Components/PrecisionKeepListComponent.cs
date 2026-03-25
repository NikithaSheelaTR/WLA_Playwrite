namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// The keep list component.
    /// </summary>
    public class PrecisionKeepListComponent
    {
        private static readonly By ViewKeepListButtonLocator = By.XPath(".//span[text()='Keep List']");
        private static readonly By CountButtonLocator = By.ClassName("KeepList-button-count");
        private static readonly By HoverViewKeepListButtonLocator = By.XPath(".//div[contains(text(),'View Keep List')]");

        /// <summary>
        /// Precision View Keep List button
        /// </summary>
        public IButton ViewKeepListButton => new Button(ViewKeepListButtonLocator);

        /// <summary>
        /// Precision Count Keep List button
        /// </summary>
        public IButton CountKeepListButton => new Button(CountButtonLocator);

        /// <summary>
        /// Precision Hover text for View Keep List
        /// </summary>
        public ILabel HoverViewKeepListLabel => new Label(HoverViewKeepListButtonLocator);
    }
}