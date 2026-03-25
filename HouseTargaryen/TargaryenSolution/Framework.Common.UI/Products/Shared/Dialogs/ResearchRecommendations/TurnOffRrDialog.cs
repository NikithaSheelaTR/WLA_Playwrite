namespace Framework.Common.UI.Products.Shared.Dialogs.ResearchRecommendations
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Modal appears when click on 'Turn off for this session' link in the bottom of the RA Slider
    /// </summary>
    public class TurnOffRrDialog : BaseModuleRegressionDialog
    {
        private const string HeaderText = "Research Recommendations current session setting";

        private const string BodyText = "By turning off Research Recommendations, it will "
            + "no longer recommend documents during this session. For more Research Recommendations options, go to Westlaw Preferences.";

        private static readonly By CancelButtonLocator = By.Id("co_rx_turnOffWarningPopup_cancelButton");

        private static readonly By CloseButtonLocator = By.Id("co_turnOffRXWarning_popupclose");

        private static readonly By TurnOffLocator = By.Id("co_rx_turnOffWarningPopup_turnOffButton");

        private static readonly By BodyLocator = By.XPath("//div[@id='coid_turnOffRXWarningLightBox']//div[@class='co_overlayBox_content']/div");

        private static readonly By HeaderLocator = By.XPath("//div[@id='coid_turnOffRXWarningLightBox']//h3[contains(@id, 'coid_lightboxAriaLabel_')] | //div[@id='coid_turnOffRXWarningLightBox']//h2[contains(@id, 'coid_lightboxAriaLabel_')]");

        private static readonly By TurnOffDialogLocator = By.Id("coid_turnOffRXWarningLightBox");

        /// <summary>
        /// Initializes a new instance of the TurnOffRaDialog class. 
        /// </summary>
        public TurnOffRrDialog()
        {
            DriverExtensions.WaitForElementDisplayed(TurnOffDialogLocator);
        }

        /// <summary>
        /// Click Cancel button
        /// </summary>
        /// <returns>new instance of RrSliderDialog</returns>
        public RrSliderDialog ClickCancel() => this.ClickElement<RrSliderDialog>(CancelButtonLocator);

        /// <summary>
        /// Click Turn Off button
        /// </summary>
        /// <returns>new instance of RrSliderDialog</returns>
        public RrSliderDialog ClickTurnOff() => this.ClickElement<RrSliderDialog>(TurnOffLocator);

        /// <summary>
        /// Verify Cancel button is displayed
        /// </summary>
        /// <returns>true if Cancel button is displayed, false otherwise</returns>
        public bool IsCancelButtonDisplayed() =>
            DriverExtensions.IsDisplayed(CancelButtonLocator, 5) && DriverExtensions.WaitForElementDisplayed(CancelButtonLocator).Text == "Cancel";

        /// <summary>
        /// Verify Close button is displayed
        /// </summary>
        /// <returns>true if Close button is displayed, false otherwise</returns>
        public bool IsCloseButtonDisplayed() =>
            DriverExtensions.IsDisplayed(CloseButtonLocator, 5);

        /// <summary>
        /// Is Header Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHeaderDisplayed() => DriverExtensions.IsDisplayed(HeaderLocator, 5);

        /// <summary>
        /// Get Header Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHeaderTest() => DriverExtensions.GetText(HeaderLocator);

        /// <summary>
        /// Is Body Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsBodyDisplayed() => DriverExtensions.IsDisplayed(BodyLocator, 5);

        /// <summary>
        /// Get Body Test
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetBodyTest() => DriverExtensions.GetText(BodyLocator);

        /// <summary>
        /// Verify header 'Research Recommendations Settings' and body 
        /// 'By turning off Research Recommendations, it will no longer recommend documents during this session. For more Research Recommendations options, go to Westlaw Preferences.'
        /// are displayed
        /// </summary>
        /// <returns>true if header and body are displayed with expected text, false otherwise</returns>
        public bool IsHeaderAndBodyDisplayed() => 
            DriverExtensions.IsDisplayed(HeaderLocator, 5) 
            && DriverExtensions.IsDisplayed(BodyLocator, 5)
            && DriverExtensions.GetText(HeaderLocator) == HeaderText
            && DriverExtensions.GetText(BodyLocator) == BodyText;
    }
}