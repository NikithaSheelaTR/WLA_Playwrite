namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    
    /// <summary>
    /// Parallel Search Tips component
    /// </summary>
    public class ParallelSearchTipsComponent : BaseModuleRegressionComponent
    {
        private static readonly By SearchTipContainerLocator = By.XPath("//div[contains(@class,'searchTipsContentWrapper')]");
        private static readonly By HeaderLabelLocator= By.XPath(".//h3");
        private static readonly By ParagraphLabelLocator = By.XPath(".//p");
        private static readonly By SearchTipLocator = By.XPath("//*[@id='parallelSearchTipsDialog']");
        private const string CrossScript = "return(arguments[0].shadowRoot.querySelector('saf-button[class=close]'));";
        private const string TitleScript = "return(arguments[0].shadowRoot.querySelector('div[class=dialog-title]'));";
        private static readonly By CloseButtonLocator = By.XPath("//div[contains(@class, '__dialogFooter')]/saf-button");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => SearchTipContainerLocator;

        /// <summary>
        /// Get Tip Title Element
        /// </summary>
        public IWebElement GetTitleElement() {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SearchTipLocator);// Shadow host 
            IWebElement TitleElement = (IWebElement)DriverExtensions.ExecuteScript(TitleScript, searchAreaElement);
            return TitleElement;           
        }

        /// <summary>
        ///  Tip Header label
        /// </summary>
        public ILabel HeaderLabel => new Label(this.ComponentLocator, HeaderLabelLocator);

        /// <summary>
        ///  Tip Paragraph label
        /// </summary>
        public ILabel ParagraphLabel => new Label(this.ComponentLocator, ParagraphLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton TipsCloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// X button
        /// </summary>
        public void SelectXButton()
        {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SearchTipLocator);// Shadow host 
            IWebElement CrossElement = (IWebElement)DriverExtensions.ExecuteScript(CrossScript, searchAreaElement);
            CrossElement.Click();
        }
    }
}
