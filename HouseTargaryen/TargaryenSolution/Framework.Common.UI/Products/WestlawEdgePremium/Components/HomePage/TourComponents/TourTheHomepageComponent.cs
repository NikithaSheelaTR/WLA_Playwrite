namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.TourComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Tour the homepage component
    /// </summary>
    public class TourTheHomepageComponent
    {
        private static readonly By ContainerLocator = By.Id("pendo-guide-container");
        private static readonly By RemindMeLaterButtonLocator = By.XPath(".//button[@aria-label='Remind me later']");

        /// <summary>
        /// Remind me later button
        /// </summary>
        public IButton RemindMeLaterButton { get; } = new Button(ContainerLocator, RemindMeLaterButtonLocator);
    }
}
