namespace Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Track Company Page
    /// </summary>
    public class TrackCompanyPage : BaseModuleRegressionPage
    {
        private const string SelectCompanyLctMask = ".//input[@value = '{0}']";

        private static readonly By CancelButtonLocator = By.Id("coid_trackCompany_cancelAction");

        private static readonly By ContinueButtonLocator = By.Id("coid_trackCompany_createAlerts");
        
        private static readonly By CreatedAlertsLocator = By.XPath("//*[@id='coid_trackCompany_createdAlertsList']//div[contains(text(), 'ticker')]");
       
        private static readonly By ResultsContainerLocator = By.Id("coid_trackCompany_resultsArea");
        
        private static readonly By SearchButtonLocator = By.Id("coid_smartWidget_searchArea_submitSearch");

        private static readonly By SearchInputLocator = By.Id("coid_smartWidget_searchArea_input");

        private static readonly By ProcessIndicatorLocator = By.Id("coid_ProgressIndicator");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackCompanyPage"/> class. 
        /// </summary>
        public TrackCompanyPage()
        {
            DriverExtensions.WaitForElement(SearchInputLocator);
        }

        /// <summary>
        /// Creates the alerts.
        /// </summary>
        public void CreateAlerts()
        {
            DriverExtensions.WaitForElementDisplayed(ContinueButtonLocator).CustomClick();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementNotDisplayed(ProcessIndicatorLocator);
        }

        /// <summary>
        /// The get created alerts.
        /// </summary>
        /// <returns> Created Alerts list </returns>
        public List<string> GetCreatedAlerts()
        {
            DriverExtensions.WaitForElementDisplayed(CreatedAlertsLocator);
            return DriverExtensions.GetElements(CreatedAlertsLocator).Select(alert => alert.Text.Replace("- ", string.Empty))
                            .ToList();
        }

        /// <summary>
        /// Returns to landing page.
        /// </summary>
        /// <returns>landing page</returns>
        public LandingPage ReturnToLandingPage()
        {
            DriverExtensions.WaitForElementDisplayed(CancelButtonLocator).Click();
            return new LandingPage();
        }

        /// <summary>
        /// Searches for company.
        /// </summary>
        /// <param name="company">The company.</param>
        public void SearchForCompany(string company)
        {
            DriverExtensions.WaitForElementDisplayed(SearchInputLocator).ScrollToElement();
            DriverExtensions.GetElement(SearchInputLocator).SetTextField(company);
            DriverExtensions.GetElement(SearchButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Selects the company.
        /// </summary>
        /// <param name="company">The company.</param>
        public void SelectCompany(string company)
        {
            DriverExtensions.ScrollTo(By.XPath(string.Format(SelectCompanyLctMask, company.ToUpper())));
            DriverExtensions.WaitForElement(
                DriverExtensions.GetElement(ResultsContainerLocator),
                By.XPath(string.Format(SelectCompanyLctMask, company.ToUpper()))).SetCheckbox(true);
        }
    }
}