namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using OpenQA.Selenium;

    /// <summary>
    /// Funds Filer Search Item
    /// </summary>
    public class FundsFilerSearchItem : BaseFilerSearchItem
    {
        private static readonly By FilerSearchTitleLocator = By.XPath(".//a[@class='co_pickListTitle ng-binding']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FundsFilerSearchItem" /> class. 
        /// </summary>
        /// <param name="container">Container</param> 
        public FundsFilerSearchItem(IWebElement container)
            : base(container, "Funds")
        {
            this.TitleLocator = FilerSearchTitleLocator;
        }
    }
}