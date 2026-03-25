namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Delivery dialog.
    /// </summary>
    public class LitigationAnalyticsDownloadDialog : DownloadDialog
    {
        private static readonly By ContainerLocator = By.Id("co_deliveryLightbox");
        private static readonly By DownloadButtonLocator = By.Id("co_deliveryDownloadButton");
        private static readonly By LoadingSpinnerLocator = By.Id("co_deliveryWaitProgress");

        /// <summary>
        /// Litigation Anallytics download dialog
        /// </summary>
        public LitigationAnalyticsDownloadDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(DeliveryDialogTab.TheBasics, new LitigationAnalyticsTheBasicsAlalyticsTabComponent());
        }

        /// <summary>
        /// The Basics Tab Component
        /// </summary>
        public LitigationAnalyticsTheBasicsAlalyticsTabComponent LitigationAnalyticsTheBasicsTab
            => SetActiveTab<LitigationAnalyticsTheBasicsAlalyticsTabComponent>(DeliveryDialogTab.TheBasics);

        /// <summary>
        /// Click on the button and wait when spinner will disappear
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public new T ClickDownloadButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(DownloadButtonLocator);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.Click(DownloadButtonLocator);
            return this.WaitForUpdateComplete<T>(700000, LoadingSpinnerLocator);
        }

        /// <summary>
        /// Verifies that download button is displayed.
        /// </summary>
        public bool IsDownloadDialogDisplayed()
        {
            return DriverExtensions.IsDisplayed(ContainerLocator);
        }
    }
}