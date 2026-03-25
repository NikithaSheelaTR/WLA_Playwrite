namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Decision Date search page for Government Weblinks project
    /// </summary>
    public class DecisionDateSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private const string DateRangeFormLctMask = "//div[.//label[contains(.,'{0}')]]/input";

        private static readonly By DateRangeOptionLocator = By.XPath("./parent::div/label");

        private static readonly By DayDropdownLocator = By.XPath("//select[@id='dd_day']");

        private static readonly By MonthDropdownLocator = By.XPath("//select[@id='dd_month']");

        private static readonly By SearchTermsTextboxLocator = By.XPath("//input[@id='querytext']");

        private static readonly By RadioButtonLocator = By.XPath("//div[@class='co_column oneColumn']//input[contains(@id, 'radio')]");

        private static readonly By YearDropdownLocator = By.XPath("//select[@id='dd_year']");

        private static readonly By DateLocator = By.XPath("//input[@id='date']");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery()
        {
            var query = new List<string>();
            if (DriverExtensions.IsDisplayed(DateLocator, 5))
            {
                query.Add(DriverExtensions.GetElement(DateLocator).GetText());
            }
            else
            {
                query.Add(DriverExtensions.GetSelectedDropdownOptionText(MonthDropdownLocator));
                query.Add(DriverExtensions.GetSelectedDropdownOptionText(DayDropdownLocator));
                query.Add(DriverExtensions.GetSelectedDropdownOptionText(YearDropdownLocator));
            }
            
            query.Add(DriverExtensions.WaitForElement(
                    DriverExtensions.GetElements(RadioButtonLocator).First(e => DriverExtensions.IsRadioButtonSelected(e.GetCssLocator())),  DateRangeOptionLocator ).Text);
            query.Add(this.GetTextFromTextarea(SearchTermsTextboxLocator));

            return query;
        }

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            if (DriverExtensions.IsDisplayed(DateLocator, 5))
            {
                string monthDigit = DateTime.ParseExact(query[0], "MMMM", CultureInfo.CurrentCulture).Month.ToString("d2");
                string date = string.Format("{0}/{1}/{2}", monthDigit, query[1], query[2]);
                DriverExtensions.SetTextField(date, DateLocator);
            }
            else
            {
                DriverExtensions.SelectElementInListByText(MonthDropdownLocator, query[0]);
                DriverExtensions.SelectElementInListByText(DayDropdownLocator, query[1]);
                DriverExtensions.SelectElementInListByText(YearDropdownLocator, query[2]);
            }

            DriverExtensions.WaitForElement(By.XPath(string.Format(DateRangeFormLctMask, query[3]))).Click();
                DriverExtensions.SetTextField(query[4], SearchTermsTextboxLocator);
                return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Gets list of years options
        /// </summary>
        /// <returns> The list of years options </returns>
        public List<string> GetYearDropdownOptions() =>
            DriverExtensions.GetDropdownOptionTexts(YearDropdownLocator).ToList();
    }
}
