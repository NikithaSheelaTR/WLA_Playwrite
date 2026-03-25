namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// Trademarks Advanced search template page
    /// </summary>
    public class TrademarksAstPage : CommonAdvancedSearchPage
    {
        private static readonly By FilingDateDropdownLocator = By.XPath(@"//div[@id='co_dateWidget_FIDA']");
        private static readonly By IssueDateDropdownLocator = By.XPath(@"//div[@id='co_dateWidget_REDA']");

        /// <summary>
        /// Filing Date DropDown
        /// </summary>
        public DateDropdown FilingDateDropdown => new DateDropdown(FilingDateDropdownLocator);

        /// <summary>
        /// Issue Date DropDown
        /// </summary>
        public DateDropdown IssueDateDropDown => new DateDropdown(IssueDateDropdownLocator);
    }
}
