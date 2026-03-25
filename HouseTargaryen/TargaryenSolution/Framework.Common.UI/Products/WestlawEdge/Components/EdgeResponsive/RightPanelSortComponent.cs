namespace Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Right panel Sort component on kebab menu's Sort submenu
    /// </summary>
    public class RightPanelSortComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'Panel-right-subMenu')]");
        // Table of authorities Sort options
        private static readonly By AlphabeticallyByTitleLocator = By.XPath(".//span[contains(text(),'Alphabetically by Title')]");
        private static readonly By DepthHighestFirstLocator = By.XPath(".//span[contains(text(),'Depth: Highest First')]");
        private static readonly By DepthLowestFirstLocator = By.XPath(".//span[contains(text(),'Depth: Lowest First')]");
        private static readonly By QuoteQuotedFirstLocator = By.XPath(".//span[contains(text(),'Quote: Quoted First')]");
        private static readonly By QuoteQuotedLastLocator = By.XPath(".//span[contains(text(),'Quote: Quoted Last')]");
        // Sort options shared by: Citing References;History: Legislative History Materials;Medical References 
        private static readonly By DateNewestFirstLocator = By.XPath(".//span[contains(text(),'Date: Newest First')]");
        private static readonly By DateOldestFirstLocator = By.XPath(".//span[contains(text(),'Date: Oldest First')]");
        // Medical References Sort options
        private static readonly By RelevanceLocator = By.XPath(".//span[contains(text(),'Relevance')]");
        private static readonly By TitleAToZLocator = By.XPath(".//span[contains(text(),'Title: A-Z')]");
        private static readonly By TitleZToALocator = By.XPath(".//span[contains(text(),'Title: Z-A')]");
        // Common Back and Close buttons
        private static readonly By BackLocator = By.XPath(".//span[text()='Back']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[text()='Close']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Alphabetically By Title button
        /// </summary>
        public IButton AlphabeticallyByTitleButton => new Button(this.ComponentLocator, AlphabeticallyByTitleLocator);

        /// <summary>
        /// Depth Highest First button
        /// </summary>
        public IButton DepthHighestFirstButton => new Button(this.ComponentLocator, DepthHighestFirstLocator);

        /// <summary>
        /// Depth Lowest First button
        /// </summary>
        public IButton DepthLowestFirstButton => new Button(this.ComponentLocator, DepthLowestFirstLocator);

        /// <summary>
        /// Quote Quoted First button
        /// </summary>
        public IButton QuoteQuotedFirstButton => new Button(this.ComponentLocator, QuoteQuotedFirstLocator);

        /// <summary>
        /// Quote Quoted Last button
        /// </summary>
        public IButton QuoteQuotedLastButton => new Button(this.ComponentLocator, QuoteQuotedLastLocator);

        /// <summary>
        /// Date: Newest First button
        /// </summary>
        public IButton DateNewestFirstButton => new Button(this.ComponentLocator, DateNewestFirstLocator);

        /// <summary>
        ///  Date: Oldest First button
        /// </summary>
        public IButton DateOldestFirstButton => new Button(this.ComponentLocator, DateOldestFirstLocator);

        /// <summary>
        /// Relevance button
        /// </summary>
        public IButton RelevanceButton => new Button(this.ComponentLocator, RelevanceLocator);

        /// <summary>
        /// Title: A-Z button
        /// </summary>
        public IButton TitleAToZButton => new Button(this.ComponentLocator, TitleAToZLocator);

        /// <summary>
        /// Title: Z-A button
        /// </summary>
        public IButton TitleZToAButton => new Button(this.ComponentLocator, TitleZToALocator);

        /// <summary>
        /// Back button taking to previous right panel
        /// </summary>
        public IButton BackButton => new Button(this.ComponentLocator, BackLocator);

        /// <summary>
        /// Panel close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);
    }
}

