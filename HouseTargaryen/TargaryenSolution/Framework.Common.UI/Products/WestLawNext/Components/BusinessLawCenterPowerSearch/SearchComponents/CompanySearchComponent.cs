namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The company search section.
    /// </summary>
    public class CompanySearchComponent : BaseSearchComponent<CompanySearchResultsPage>
    {
        private static readonly By FillingDateLocator = By.TagName("option");

        private static readonly By HideInactiveCompaniesCheckboxLocator = By.Id("activeCompanies");

        private static readonly By HideNonEdgarCompaniesCheckboxLocator = By.Id("edgarOnly");

        /// <summary>
        /// The click on hide inactive companies checkbox.
        /// </summary>
        public void ClickOnHideInactiveCompaniesCheckbox() => DriverExtensions.WaitForElement(HideInactiveCompaniesCheckboxLocator).Click();

        /// <summary>
        /// Click on the Search button for Company Search
        /// </summary>
        /// <returns>CompanySearchResultsPage</returns>
        public override CompanySearchResultsPage ClickSearchButton()
        {
            this.BaseClickSearchButton();
            return new CompanySearchResultsPage();
        }

        /// <summary>
        /// The select filing date.
        /// </summary>
        /// <param name="month">The month.</param>
        public void SelectFilingDate(string month)
        {
            IReadOnlyCollection<IWebElement> dropdownDateElements = DriverExtensions.GetElements(FillingDateLocator);
            dropdownDateElements.First(el => el.Text.Equals(month)).Click();
        }

        /// <summary>
        /// Method for setting the  Hide non-EDGAR companies checkbox.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public void SetHideNonEdgarCompaniesCheckbox(bool flag = true)
        {
            bool isChecked = DriverExtensions.WaitForElement(HideNonEdgarCompaniesCheckboxLocator).GetAttribute("checked")
                             == "checked";
            if (isChecked ^ flag)
            {
                DriverExtensions.JavascriptClick(HideNonEdgarCompaniesCheckboxLocator);
            }
        }
    }
}