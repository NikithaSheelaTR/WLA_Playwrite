namespace Framework.Common.UI.Products.WestLawNext.Pages.CategoryPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter;
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Business Law Center Category Page
    /// </summary>
    public class BusinessLawCenterCategoryPage : CommonBrowsePage
    {
        private const string BlcCategoryPageComponentLctMask =
            "//div[@id='coid_website_browseRightColumn']//*[contains(text(), '{0}')]";

        private static readonly By ActivateNonBillableZoneButtonLocator =
            By.XPath("//input[@value='Activate Non-Billable Zone']");

        private static readonly By BusinesLawCenterHeadingLocator = By.XPath("//h1[normalize-space(text()='Business Law Center')]");

        private static readonly By EnterBlcPowerSearchLinkLocator = By.XPath("//a[text()='BLC PowerSearch']");

        private static readonly By ToggleToBillableLocator = By.XPath("//input[@value='Toggle to Billable Client ID']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLawCenterCategoryPage"/> class. 
        /// Constructor for the Business Law Center Category Page
        /// </summary>
        public BusinessLawCenterCategoryPage()
        {
            DriverExtensions.WaitForElementDisplayed(BusinesLawCenterHeadingLocator);
        }

        /// <summary>
        /// Redline Comparison component
        /// </summary>
        public RedlineComparisonComponent RedlineComparison { get; private set; } = new RedlineComparisonComponent();

        /// <summary>
        /// Click Activate Non-Billable Zone Button
        /// </summary>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>New Page Object</returns>
        public T ClickActivateZoneButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(ActivateNonBillableZoneButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }  

        /// <summary>
        /// The click road runner link.
        /// </summary>
        /// <returns>The <see cref="CompanySearchPage"/>.</returns>
        public CompanySearchPage ClickBlcPowerSearchLink()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            // RA 10/13/2015: temp work-around for qed, under investigation.
            DriverExtensions.WaitForElement(EnterBlcPowerSearchLinkLocator).JavascriptClick();

            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return new CompanySearchPage();
        }

        /// <summary>
        /// Check whether expert component is displayed.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <returns>
        /// Check result
        /// </returns>
        public bool IsComponentDisplayed(string component) 
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(BlcCategoryPageComponentLctMask, component)), 5);

        /// <summary>
        /// Click Toggle To Billable Client Id Button
        /// </summary>
        /// <returns>New instance of ChangeClientIdDialog</returns>
        public ChangeClientIdDialog ToggleToBillableClientId()
        {
            DriverExtensions.WaitForElement(ToggleToBillableLocator).Click();
            return new ChangeClientIdDialog();
        }
    }
}