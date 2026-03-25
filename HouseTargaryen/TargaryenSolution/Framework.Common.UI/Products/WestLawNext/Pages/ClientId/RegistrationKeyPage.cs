namespace Framework.Common.UI.Products.WestLawNext.Pages.ClientId
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext registration key selection screen
    /// </summary>
    public class RegistrationKeyPage : BaseModuleRegressionPage
    {
        private static readonly By ContinueButtonLocator = By.XPath("//button[@id='SignIn'] | //saf-button[@type='submit']");

        private static readonly By RegKeyElementLocator = By.XPath("//li[@class='Form-textSelect Form-radioButtonList']/div");
        
        /// <summary>
        /// Click on the continue button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickContinue<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Selects the registration key by matching the input string to a selection.
        /// Use either the friendly name, or the prism username/password.
        /// </summary>
        /// <param name="regKeyName">The name of the key to select</param>
        public void SelectRegKeyByName(string regKeyName)
            =>
                DriverExtensions.GetElements(RegKeyElementLocator)
                                .Where(elem => elem.Text.Contains(regKeyName))
                                .ToList()
                                .FirstOrDefault()?.Click();
    }
}