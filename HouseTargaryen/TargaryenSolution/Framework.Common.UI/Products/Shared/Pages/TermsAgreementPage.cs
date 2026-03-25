namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Agreement page for sign in
    /// </summary>
    public class TermsAgreementPage : BaseModuleRegressionPage
    {
        private static readonly By AgreeRadioButtonLocator = By.Id("co_agreementRadioBoxAgree");

        private static readonly By ContinueButtonLocator = By.Id("coid_button_continue");

        private static readonly By DontAgreeRadioButtonLocator = By.Id("co_agreementRadioBoxDontAgree");

        private static readonly By WarningMessageLocator = By.ClassName("co_infoBox_message");
        

        /// <summary>
        /// Select agree radio button
        /// </summary>
        public void SelectAgreeOption() => DriverExtensions.WaitForElement(AgreeRadioButtonLocator).Click();

        /// <summary>
        /// Clicks the continue button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickContinue<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select the don't agree radio button
        /// </summary>
        public void SelectDontAgreeOption() => DriverExtensions.WaitForElement(DontAgreeRadioButtonLocator).Click();

        /// <summary>
        /// Checks if the continue button is enabled
        /// </summary>
        /// <returns> True if the continue button is enabled, false otherwise </returns>
        public bool IsContinueButtonEnabled() => DriverExtensions.WaitForElement(ContinueButtonLocator).Enabled;

        /// <summary>
        /// Verify that 'Don't agree' radio button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDontAgreeRadioButtonDisplayed() => DriverExtensions.IsDisplayed(DontAgreeRadioButtonLocator, 5);

        /// <summary>
        /// Verify that warning message is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsWarningMessageDisplayed() => DriverExtensions.IsDisplayed(WarningMessageLocator);

        /// <summary>
        /// Gets the text of the error box
        /// </summary>
        /// <returns> The error text. Returns an empty string if there was an issue finding the box. </returns>
        public string GetWarningMessage() => DriverExtensions.IsDisplayed(WarningMessageLocator, 5)
                                            ? DriverExtensions.GetText(WarningMessageLocator)
                                            : string.Empty;
    }
}