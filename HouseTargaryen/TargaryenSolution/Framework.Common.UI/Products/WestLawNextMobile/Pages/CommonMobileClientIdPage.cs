namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common Mobile Client Id Page By
    /// </summary>
    public class CommonMobileClientIdPage : MobileBasePage
    {
        private static readonly By CancelButtonLocator = By.Id("coid_website_cancelButton");

        private static readonly By ClientIdTextboxLocator = By.Id("txtClientId");

        private static readonly By MatterIdTextboxLocator = By.Id("txtClientMatterId");

        private static readonly By ContinueButtonLocator = By.Id("coid_website_changeClientIDButton");

        private static readonly By ErrorMessageLocator = By.ClassName("field-validation-error");

        /// <summary>
        /// Click cancel on client id page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>The page object</returns>
        public T ClickCancel<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Continue To the next page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>The page object</returns>
        public T ClickContinue<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Set the client Id textbox
        /// </summary>
        /// <param name="clientId">The client Id</param>
        public void EnterClientId(string clientId) => DriverExtensions.SetTextField(clientId, ClientIdTextboxLocator);

        /// <summary>
        /// Enters the Client Id and clicks continue
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="clientId"> Client Id </param>
        /// <returns> New instance of the page </returns>
        public T EnterClientIdAndClickContinue<T>(string clientId) where T : ICreatablePageObject
        {
            if (clientId != null)
            {
                this.EnterClientId(clientId);
            }
            
            return this.ClickContinue<T>();
        }

        /// <summary>
        /// Set the matter Id textbox
        /// </summary>
        /// <param name="matterId">The matter Id</param>
        public void EnterMatterId(string matterId) => DriverExtensions.SetTextField(matterId, MatterIdTextboxLocator);

        /// <summary>
        /// Gets the validation message on the page, if there is one
        /// </summary>
        /// <returns> Error message text </returns>
        public string GetValidationMessage() => DriverExtensions.GetText(ErrorMessageLocator);
    }
}