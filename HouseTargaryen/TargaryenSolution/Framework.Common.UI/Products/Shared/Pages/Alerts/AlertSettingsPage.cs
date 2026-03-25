namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alert Settings Page
    /// </summary>
    public class AlertSettingsPage : BaseModuleRegressionPage
    {
        private static readonly By AlertLogoInformationTextLocator = By.Id("logoType");

        private static readonly By AlertLogoInformationIconLocator = By.CssSelector("#logoType .co_moreInfo");

        private static readonly By AlertLogoInformationPopUpLocator = By.CssSelector("#logoType .co_infoBox_message");
        
        /// <summary>
        /// Get Alert Logo Information Text
        /// </summary>
        public string GetAlertLogoInformationText() => DriverExtensions.GetText(AlertLogoInformationTextLocator);


        /// <summary>
        /// Get Alert Logo PopUp Information Text
        /// </summary>
        public string GetAlertLogoPopUpInformationText()
        {
            DriverExtensions.Hover(AlertLogoInformationIconLocator);
            return DriverExtensions.WaitForElement(AlertLogoInformationPopUpLocator).GetText();
        }
    }
}