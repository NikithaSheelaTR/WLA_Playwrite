namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using OpenQA.Selenium;

    /// <summary>
    /// Best heanote box
    /// </summary>
    public class BestHeadnoteBoxComponent : PrecisionBaseBlueBoxComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class='Athens-bestHeadnote']");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="BestHeadnoteBoxComponent"/> class.
        /// </summary>
        /// <param name="containerElement"></param>
        public BestHeadnoteBoxComponent(IWebElement containerElement) : base(containerElement)
        {
           this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;        
    }
}
