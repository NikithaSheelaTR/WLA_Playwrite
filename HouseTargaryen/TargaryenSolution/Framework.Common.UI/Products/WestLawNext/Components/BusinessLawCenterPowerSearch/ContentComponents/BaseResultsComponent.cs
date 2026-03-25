namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base content section.
    /// </summary>
    public abstract class BaseResultsComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// The next arrow.
        /// </summary>
        protected static readonly By NextArrowLocator =
            By.XPath("//*[@id='rightPane']/div[@class='co_navTools']//a[@class='co_next']");

        private const string SelectPageLctMask =
            "//div[@is-footer='true']/ul/li[@ng-repeat='page in pages']/a[text()={0}]";

        private static readonly By FirstArrowLocator =
            By.XPath("//*[@id='rightPane']/div[@class='co_navTools']//a[@class='co_first']");

        private static readonly By LastArrowLocator =
            By.XPath("//*[@id='rightPane']/div[@class='co_navTools']//a[@class='co_last']");

        private static readonly By NumberOfResultsReturnedLocator =
            By.XPath("//span[@class='companyResultsTableHeader searchResultNumber ng-binding']");

        private static readonly By PageCheckboxLocator = By.XPath("//div[@class='listItems ng-scope']/ul/li//input[@type='checkbox']");

        private static readonly By PrevArrowLocator =
            By.XPath("//*[@id='rightPane']/div[@class='co_navTools']//a[@class='co_prev']");

        private static readonly By SpinnerLocator = By.XPath("//div[@class='co_loading']");

        private static readonly By DeliveryDropDownLocator = By.XPath("//li[contains(@class, 'deliveryDropdown')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResultsComponent"/> class.
        /// </summary>
        protected BaseResultsComponent()
        {
            DriverExtensions.WaitForElementDisplayed(NumberOfResultsReturnedLocator);
        }

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(DeliveryDropDownLocator, "BLC");

        /// <summary>
        /// Returns true if pagination is available and functioning as expected. 
        /// </summary>
        /// <returns>True / False</returns>      
        public bool IsPaginationAvailable()
        {
            bool flag = true;
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
            DriverExtensions.WaitForElementDisplayed(NumberOfResultsReturnedLocator);
            int numberOfSearchResult = this.GetTotalNumberOfSearchResults();

            if (numberOfSearchResult < 20)
            {
                flag = this.ArePaginationArrowsDisplayed(true, numberOfSearchResult, PrevArrowLocator, FirstArrowLocator, NextArrowLocator, LastArrowLocator);
            }

            if ((numberOfSearchResult > 20) && (numberOfSearchResult <= 40))
            {
                flag = this.ArePaginationArrowsDisplayed(true, numberOfSearchResult, NextArrowLocator, LastArrowLocator);

                this.ClickArrow(NextArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, FirstArrowLocator, PrevArrowLocator);

                this.ClickArrow(PrevArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, NextArrowLocator, LastArrowLocator);

                this.ClickArrow(LastArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, FirstArrowLocator, PrevArrowLocator);

                this.ClickArrow(FirstArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, NextArrowLocator, LastArrowLocator);
            }

            if (numberOfSearchResult > 40)
            {
                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, NextArrowLocator, LastArrowLocator);

                this.ClickArrow(NextArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, FirstArrowLocator, PrevArrowLocator, NextArrowLocator, LastArrowLocator);

                this.ClickArrow(LastArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, FirstArrowLocator, PrevArrowLocator);

                this.ClickArrow(PrevArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, FirstArrowLocator, PrevArrowLocator, NextArrowLocator, LastArrowLocator);

                this.ClickArrow(FirstArrowLocator);

                flag = this.ArePaginationArrowsDisplayed(flag, numberOfSearchResult, NextArrowLocator, LastArrowLocator);
            }

            return flag;
        }

        /// <summary>
        /// The select page.
        /// </summary>
        /// <param name="pageNumber">The number of page</param>
        public void SelectPage(int pageNumber)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SelectPageLctMask, pageNumber)).Click();
            DriverExtensions.WaitForPageLoad();
        }

        /// <summary>
        /// The select page by index.
        /// </summary>
        /// <param name="index">The number of item</param>
        /// <param name="state">The state of checkbox</param>
        public void SelectItemByIndex(int index, bool state = true)
        {
            IWebElement checkbox = DriverExtensions.GetElements(PageCheckboxLocator).ElementAt(index);

            if (checkbox.Selected != state)
            {
                checkbox.Click();
            }
        } 

        /// <summary>
        /// Returns the total number of search results from a search
        /// </summary>
        /// <returns>number of search results</returns>
        protected int GetTotalNumberOfSearchResults()
            => DriverExtensions.WaitForElement(NumberOfResultsReturnedLocator).GetAttribute("textContent").RetrieveCountFromBrackets();

        /// <summary>
        /// The are pagination arrows presents.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <param name="numberOfSearchResult">The number Of Search Result.</param>
        /// <param name="arrowLocators">The arrow locators.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="ArgumentNullException">arrowLocators are null</exception>
        private bool ArePaginationArrowsDisplayed(bool flag, int numberOfSearchResult, params By[] arrowLocators)
        {
            if (!flag)
            {
                return false;
            }

            if (arrowLocators == null)
            {
                throw new ArgumentNullException();
            }

            DriverExtensions.WaitForTextInElement(numberOfSearchResult.ToString(), NumberOfResultsReturnedLocator);

            for (int i = 0; flag && i < arrowLocators.Length; i++)
            {
                flag &= DriverExtensions.IsDisplayed(arrowLocators[i], 5);
            }

            return flag;
        }

        /// <summary>
        /// Click on arrow
        /// </summary>
        /// <param name="arrowLocator">arrow locator</param>
        private void ClickArrow(By arrowLocator)
        {
            DriverExtensions.Click(arrowLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementNotDisplayed(60000, SpinnerLocator);
        }
    }
}