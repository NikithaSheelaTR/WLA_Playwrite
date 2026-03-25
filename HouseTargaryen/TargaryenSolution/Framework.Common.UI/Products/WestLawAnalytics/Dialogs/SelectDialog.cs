namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Select Dialog, which appears after clicking on the Select button on the Analytics Alert page
    /// </summary>
    public class SelectDialog : BaseModuleRegressionDialog
    {
        private static readonly By SubmitButtonLocator = By.XPath("//input[@id='wa_submitListToApplyAlert']");

        private static readonly By CheckboxLocator = By.XPath("//*[contains(@id,'wa_alertLightboxCheckbox')]");

        private static readonly By ExpandLocationButtonLocator = By.XPath("//a[contains(@class,'wa_settingsAccountStructureSettingsLocationViewHide')]");

        private static readonly By ItemToChooseCheckboxLocator = By.XPath("//input[contains(@id,'wa_alertLightboxSubRowCheckbox')]");
        
        /// <summary>
        /// Select user/location/practice area/client
        /// </summary>
        /// <param name="index"> index to select </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage SelectOption(int index)
        {
            DriverExtensions.GetElements(CheckboxLocator).ElementAt(index).SetCheckbox(true);
            return this.ClickSubmitButton();
        }

        /// <summary>
        /// Select user
        /// </summary>
        /// <param name="index"> index to select user </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage SelectUser(int index)
        {
            DriverExtensions.GetElements(ItemToChooseCheckboxLocator).ElementAt(index).SetCheckbox(true);
            return this.ClickSubmitButton();
        }

        /// <summary>
        /// Select all users (check all checkboxes)
        /// </summary>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage SelectAllUsers()
        {
            this.ClickExpandLocationButton();
            DriverExtensions.GetElements(ItemToChooseCheckboxLocator).ToList().ForEach(checkbox => checkbox.SetCheckbox(true));
            return this.ClickSubmitButton();
        }

        /// <summary>
        /// Verify that alert List Lightbox Users is displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsListUsersCheckboxDisplayed()
           => DriverExtensions.IsDisplayed(CheckboxLocator, 5);
      

        /// <summary>
        /// Verify that expand location button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsExpandLocationButtonDisplayed()
            => DriverExtensions.IsDisplayed(ExpandLocationButtonLocator, 5);

        /// <summary>
        /// Click on the expand location button
        /// </summary>
        public void ClickExpandLocationButton() => DriverExtensions.WaitForElement(ExpandLocationButtonLocator).Click();

        /// <summary>
        /// Verify that items are displayed after clicking on the expand location button
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsItemDisplayed() => DriverExtensions.IsDisplayed(ItemToChooseCheckboxLocator, 5);

        /// <summary>
        /// Click on the Submit Button
        /// </summary>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage ClickSubmitButton()
        {
            DriverExtensions.WaitForElement(SubmitButtonLocator).Click();
            return new CreateAlertPage();
        }
    }
}
