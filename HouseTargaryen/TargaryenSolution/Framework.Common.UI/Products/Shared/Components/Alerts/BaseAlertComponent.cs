namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base component object for alert components
    /// </summary>
    public abstract class BaseAlertComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContinueButtonLocator = By.XPath("//button[contains(@id, 'co_button_continue')]");

        private static readonly By ValidationButton = By.ClassName("co_buttonLoading");

        private static readonly By WarningMessageLocator = By.CssSelector("#co_alerts .co_infoBox_message");

        /// <summary>
        /// Clicks continue and goes to a new alerts component
        /// </summary>
        /// <param name="waitForValidation"> The wait For Validation. </param>
        /// <typeparam name="T"> The component type </typeparam>
        /// <returns> The specified component </returns>
        public T ClickContinue<T>(bool waitForValidation = true) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.GetElements(ContinueButtonLocator).FirstOrDefault(elem => elem.Displayed));

            if (waitForValidation)
            {
                this.WaitForValidation();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// gets the warning message text
        /// </summary>
        /// <returns> Text from the Warning message </returns>
        public string GetWarningText()
        {
            List<string> warningMessages =
                DriverExtensions.GetElements(WarningMessageLocator).Where(elem => elem.Displayed).Select(elem => elem.Text).ToList();

            string resultString = string.Empty;
            foreach (string warningMessage in warningMessages)
            {
                resultString += warningMessage + " / ";
            }

            return resultString;
        }

        /// <summary>
        /// Checks the contnue button is displayed on the page
        /// </summary>
        /// <returns> True If the contnue button is displayed, false otherwise </returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.IsDisplayed(ContinueButtonLocator);

        /// <summary>
        /// Checks if a warning is displayed on the component
        /// </summary>
        /// <returns>If the warning box is displayed</returns>
        public bool IsWarningDisplayed() => DriverExtensions.IsDisplayed(WarningMessageLocator);

        /// <summary>
        /// Wait for the 'Validating' button to not be visible ('Validating' button replace on the 'Continue' button)
        /// Validation button appears after click on Continue button. Indicate that alert section validation is in progress
        /// </summary>
        public void WaitForValidation() => DriverExtensions.WaitForElementNotPresent(ValidationButton, 70000);
    }
}