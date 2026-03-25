namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.DropDowns;

    using OpenQA.Selenium;

    /// <summary>
    /// Patent Advanced search template page
    /// </summary>
    public class PatentAstPage : CommonAdvancedSearchPage
    {
        private static readonly By FilingDateDropdownLocator = By.XPath(@"//div[@id='co_dateWidget_FILEDATE']");
        private static readonly By PublishedApplicationDateDropdownLocator = By.XPath(@"//div[@id='co_dateWidget_PUBDATE']");
        private static readonly By IssueDateDropdownLocator = By.XPath(@"//div[@id='co_dateWidget_ISSUEDATE']");

        /// <summary>
        /// Filing Date DropDown
        /// </summary>
        public DateDropdown FilingDateDropdown => new DateDropdown(FilingDateDropdownLocator);

        /// <summary>
        /// Published Application Date Dropdown
        /// </summary>
        public DateDropdown PublishedApplicationDateDropdown =>
            new DateDropdown(PublishedApplicationDateDropdownLocator);

        /// <summary>
        /// Issue Date DropDown
        /// </summary>
        public DateDropdown IssueDateDropDown => new DateDropdown(IssueDateDropdownLocator);
    }
}
