namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Company Investigator Search Template Page
    /// </summary>
    public class CompanyInvestigatorAstPage : CommonAdvancedSearchPage
    {
        private static readonly By BusinessNameLocator = By.Id("co_search_advancedSearch_NA");

        private static readonly By CityAddressLocator = By.Id("co_search_advancedSearch_CITY");

        private static readonly By TopSearchButtonLocator = By.Id("co_search_advancedSearchButton_top");
        
        /// <summary>
        /// This method clicks on the top Search button on advance search page
        /// </summary>
        /// <typeparam name="T">page type</typeparam>
        /// <returns>new page object</returns>
        public T ClickTopSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(TopSearchButtonLocator).Click();

            // Click View Document button if Out of Plan dialog is displayed and if it is not Non Billable dialog
            return (this.IsDisplayed(Dialogs.OutOfPlan) && !this.IsDisplayed(Dialogs.NonBillable))
                       ? new OutOfPlanDialog().ClickViewDocumentButton<T>()
                       : DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// This method is used to enter a business name 
        /// </summary>
        /// <param name="name">
        /// The business Name.
        /// </param>
        /// <returns>
        /// The <see cref="CompanyInvestigatorAstPage"/>.
        /// </returns>
        public CompanyInvestigatorAstPage FillBusinessName(string name)
        {
            DriverExtensions.SetTextField(name, BusinessNameLocator);
            return this;
        }

        /// <summary>
        /// This method is used to enter a Street Address 
        /// </summary>
        /// <param name="cityAddress">
        /// The city Address.
        /// </param>
        /// <returns>
        /// The <see cref="CompanyInvestigatorAstPage"/>.
        /// </returns>
        public CompanyInvestigatorAstPage FillCityAddress(string cityAddress)
        {
            DriverExtensions.SetTextField(cityAddress, CityAddressLocator);
            return this;
        }
    }
}