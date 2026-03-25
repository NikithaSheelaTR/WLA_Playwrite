namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Negative Treatment page
    /// </summary>
    public class NegativeTreatmentPage : CommonRiPage
    {
        private static readonly By KeyCiteRedFlagLocator =
            By.XPath("//img[@alt='KeyCite Red Flag - SevereNegative Treatment']");

        /// <summary>
        /// Click First Red Flag
        /// </summary>
        /// <returns>NegativeTreatmentPage</returns>
        public NegativeTreatmentPage ClickFirstRedFlag()
        {
            DriverExtensions.WaitForElement(KeyCiteRedFlagLocator).Click();
            return this;
        }
    }
}