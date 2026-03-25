namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Type Ahead item
    /// </summary>
    public class BlcTypeAheadItem : BaseItem
    {
        private static readonly By PrimarySicLocator
            = By.XPath(".//div[@class='co_filerDesc']/following-sibling::div/*[contains(text(),'Primary SIC:')]");

        private static readonly By TickerNameLocator = By.XPath(".//div[@class='co_filerAttributes']/span[1]");

        private static readonly By CurrentlyKnownAsTitleLocator
            = By.XPath(".//div[@class='co_filerDesc']/following-sibling::div/*[contains(text(),'Currently known as:')]");

        private static readonly By FormerNameLocator
            = By.XPath(".//div[@class='co_filerDesc']/following-sibling::div/*[contains(text(),'Former Name:')]");

        private static readonly By SeriesFormerNameLocator
            = By.XPath(".//div[@class='co_filerDesc']/following-sibling::div/*[contains(text(),'Series Former Name:')]");

        private static readonly By SeriesNameLocator
            = By.XPath(".//div[@class='co_filerDesc']/following-sibling::div/*[contains(text(),'Series:')]");

        private static readonly By TitleLocator = By.XPath(".//div[@class='co_filerDesc']//a");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlcTypeAheadItem"/> class. 
        /// </summary>
        /// <param name="container"> Container </param>
        public BlcTypeAheadItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Title of item
        /// </summary>
        public string Title => DriverExtensions.WaitForElement(this.Container, TitleLocator).GetText();

        /// <summary>
        /// Click item
        /// </summary>
        public void ClickItem() => DriverExtensions.WaitForElement(this.Container, TitleLocator).Click();

        /// <summary>
        /// Is title of item displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsTitleOfItemDisplayed()
            => DriverExtensions.WaitForElement(this.Container, TitleLocator).IsDisplayed();

        /// <summary>
        /// Is item contains cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>True - if contains, false - otherwise</returns>
        public bool IsItemContainsCik(string cik)
            => DriverExtensions.GetAttribute("href", this.Container, TitleLocator).Contains(cik);

        /// <summary>
        /// Get Title By cik number
        /// </summary>
        /// <returns> Title </returns>
        public string GetTitle()
            => DriverExtensions.WaitForElement(this.Container, TitleLocator).GetText();

        /// <summary>
        /// Get Primary sic by cik number
        /// </summary>
        /// <returns>Primary sic</returns>
        public string GetPrimarySic() => this.GetText(PrimarySicLocator);

        /// <summary>
        /// Get Ticker by cik number
        /// </summary>
        /// <returns>Ticker</returns>
        public string GetTicker() => this.GetText(TickerNameLocator);

        /// <summary>
        /// Get Currently Known As Title by cik number
        /// </summary>
        /// <returns>Currently known as title</returns>
        public string GetCurrentlyKnownAsTitle() => this.GetText(CurrentlyKnownAsTitleLocator);

        /// <summary>
        /// Get Former Name by cik number
        /// </summary>
        /// <returns>Former Name</returns>
        public string GetFormerName() => this.GetText(FormerNameLocator);

        /// <summary>
        /// Get Series Former Name
        /// </summary>
        /// <returns>Series Former Name</returns>
        public string GetSeriesFormerName() => this.GetText(SeriesFormerNameLocator);

        /// <summary>
        /// Get Series Name
        /// </summary>
        /// <returns>Series Name</returns>
        public string GetSeriesName() => this.GetText(SeriesNameLocator);

        /// <summary>
        /// Get Text From Parameters
        /// </summary>
        /// <param name="xPath">XPath</param>
        /// <returns>Text From Parameters</returns>
        private string GetText(By xPath)
        {
            IWebElement item = DriverExtensions.SafeGetElement(this.Container, xPath);
            return item != null && item.Displayed ? item.GetText() : string.Empty;
        }
    }
}