namespace Framework.Common.UI.Products.WestLawAnalytics.Pages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Firm Health page
    /// </summary>
    public class FirmHealthPage : BasePage
    {
        private static readonly By AgreeCheckboxLocator = By.Id("agreeToTerms");
        private static readonly By ContinueButtonLocator = By.Id("continueButton");
        
        /// <summary>
        /// Agree To Terms And Continue
        /// </summary>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage AgreeToTermsAndContinue()
        {
            if (DriverExtensions.IsDisplayed(AgreeCheckboxLocator, 5))
            {
                DriverExtensions.SetCheckbox(AgreeCheckboxLocator, true);
                DriverExtensions.Click(ContinueButtonLocator);
            }

            return new AnalyticsPage();
        }
    }
}
