namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filter Date widget on toolbar on report page
    /// </summary>
    public class FilterDateRangeComponent : BaseModuleRegressionComponent
    {
        private const string RootLctMask = "//li[@id='co_docToolbarDateRange']//select[@ng-model='{0}']";

        private static readonly By ApplyButtonLocator = By.XPath("//input[@value='Apply']");

        private static readonly By EndYearLocator = By.XPath("//select[@value='year'][2]/option[@selected]");

        private static readonly By StartYearLocator = By.XPath("//select[@value='year'][1]/option[@selected]");

        private static readonly By ContainerLocator = By.Id("co_docToolbarDateRange");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// get all years in the region
        /// </summary>
        /// <returns>list of years</returns>
        public List<string> GetDateRange()
        {
            int startYear = DriverExtensions.GetText(StartYearLocator).ConvertCountToInt();
            int endYear = DriverExtensions.GetText(EndYearLocator).ConvertCountToInt();

            return Enumerable.Range(startYear, endYear - startYear + 1).Select(year => year.ToString()).ToList();
        }

        /// <summary>
        /// set the upper bound year and click apply
        /// </summary>
        /// <param name="year">year</param>
        public void SetEndYearAndClickApply(string year) => this.SetYearAndClickApply(year, "endYear");

        /// <summary>
        /// set the lower bound year and click apply
        /// </summary>
        /// <param name="year">year</param>
        public void SetStartYearAndClickApply(string year) => this.SetYearAndClickApply(year, "startYear");

        private void SetYearAndClickApply(string year, string yearLocator)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(RootLctMask, yearLocator))).Click();
            DriverExtensions.WaitForElement(By.XPath(string.Format(RootLctMask + "/option[contains(.,'{1}')]", yearLocator, year)))
                .Click();
            DriverExtensions.WaitForElement(ApplyButtonLocator).Click();
            DriverExtensions.WaitForElementDisplayed(60000, ApplyButtonLocator);
        }
    }
}