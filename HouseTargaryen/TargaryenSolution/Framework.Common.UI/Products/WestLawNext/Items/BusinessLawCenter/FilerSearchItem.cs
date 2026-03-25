namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filer Search Item
    /// </summary>
    public class FilerSearchItem : BaseFilerSearchItem
    {
        private static readonly By FilerSearchTitleLocator = By.XPath(".//a[@class='draggable_document_link ng-binding']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FilerSearchItem" /> class. 
        /// </summary>
        /// <param name="container">Container</param> 
        public FilerSearchItem(IWebElement container) : base(container, "Search")
        {
            this.TitleLocator = FilerSearchTitleLocator;
        }

        /// <summary>
        /// Get Ticker by cik number
        /// </summary>
        /// <param name="option">Option</param>
        /// <returns>Ticker</returns>
        public string GetTickerText(BlcItemOptions option)
        {
            IWebElement item = DriverExtensions.SafeGetElement(this.Container, By.XPath(this.Map[option].LocatorString));
            return item != null && item.Displayed
                       ? item.GetText().Split(' ').FirstOrDefault()
                       : string.Empty;
        }
    }
}