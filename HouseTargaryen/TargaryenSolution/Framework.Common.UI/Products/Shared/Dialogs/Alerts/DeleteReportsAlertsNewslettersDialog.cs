namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// 'Delete Reports' or 'Delete Newsletters' or 'Delete Alerts' dialog 
    /// </summary>
    public class DeleteReportsAlertsNewslettersDialog : BaseModuleRegressionDialog
    {
        private static readonly By YesButtonLocator = By.Id("co_delete_item_yes");

        /// <summary>
        /// Click on the 'Yes' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickYesButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(YesButtonLocator).Click();

            // When we delete alerts, 2 dialog may appear: 
            // first - to confirm deleting alerts, second - to confirm deleting alerts groups (if they was created)
            // Here we confirm that group will be deleted if Yes button appeared again (groups were created) 
            if (DriverExtensions.IsDisplayed(YesButtonLocator, 5))
            {
                DriverExtensions.WaitForElement(YesButtonLocator).Click();
            }

            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
