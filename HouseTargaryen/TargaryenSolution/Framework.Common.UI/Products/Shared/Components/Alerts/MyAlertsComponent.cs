namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// My Alerts Component on COVID-19 Legal Materials &amp; News and Practice Areas pages
    /// </summary>
    public class MyAlertsComponent : BaseModuleRegressionComponent
    {
        private static readonly By CreateAlertLocator = By.LinkText("Create Alert");

        private static readonly By ContainerLocator = By.ClassName("co_myAlertsWidget");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks Create Alert Button
        /// </summary>
        /// <returns> The <see cref="FinishSettingUpMyAlertsDialog"/>. </returns>
        public FinishSettingUpMyAlertsDialog ClickCreateAlert()
        {
            DriverExtensions.Click(CreateAlertLocator);
            return new FinishSettingUpMyAlertsDialog();
        }
    }
}