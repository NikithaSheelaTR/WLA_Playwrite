namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using System.Collections.Generic;

    /// <summary>
    /// Get Started Precision Panel
    /// </summary>
    public class PrecisionGetStartedPanel : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'Athens-getStartedContainer']");
        private static readonly By CustomizeButtonLocator = By.XPath("./button[@class='Athens-button-customize']");
        private static readonly By GetStartedOptionButtonLocator = By.XPath(".//*[@class = 'Athens-getStarted-link']");
        private static readonly By ZeroStateMessageLocator = By.XPath(".//div[@class = 'Athens-getStartedZeroState']");
        private static readonly By ZeroStateMessageLinkLocator = By.XPath("./div/button");
        private static readonly By ToolTipLocator = By.XPath("//div[@id='coid_a11yTooltip_16']/div[contains(@class,'a11yTooltip-pointer')]/following-sibling::div[1]");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Customize Get Started Panel button
        /// </summary>
        public IButton CustomizeGetStartedPanelButton => new Button(ComponentLocator, CustomizeButtonLocator);

        /// <summary>
        /// Get Started Panel zero state message
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(ComponentLocator, ZeroStateMessageLocator);

        /// <summary>
        ///  Get Started Panel zero state message link
        /// </summary>
        public ILink ZeroStateMessageLink => new Link(ComponentLocator, ZeroStateMessageLinkLocator);

        /// <summary>
        /// Get a list Get Started options
        /// </summary>
        /// <returns>A list of Get Started options</returns>
        public IReadOnlyCollection<ILink> GetStartedOptionsLinks => new ElementsCollection<Link>(ComponentLocator, GetStartedOptionButtonLocator);
                
        /// <summary>
        /// Returns the tooltip text for the Get started settings button.
        /// </summary>
        public string ToolTipText()
        {
            IWebElement element = DriverExtensions.GetElement(ToolTipLocator);

            // Hover to trigger tooltip visibility (if required)
            Actions actions = new Actions(BrowserPool.CurrentBrowser.Driver);
            actions.MoveToElement(element).Perform();

            // First, try the standard Selenium Text property
            string tooltipText = element.Text;

            // Fallback to JS (handles cases where .Text is empty but textContent has value)
            if (string.IsNullOrWhiteSpace(tooltipText))
            {
                tooltipText = (string)((IJavaScriptExecutor)BrowserPool.CurrentBrowser.Driver)
                    .ExecuteScript("return (arguments[0].textContent || arguments[0].innerText || '').trim();", element);
            }

            return tooltipText;
        }
    }
}
