namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Download delivery dialog page
    /// </summary>
    public class DownloadDialog : BaseDeliveryDialog
    {
        private static readonly By DownloadButtonLocator = By.XPath("//input[@id='co_deliveryDownloadButton' or @id='deliveryLinkRow2Download' or @id='coid_deliveryWaitMessage_downloadButton']");
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadDialog"/> class.
        /// </summary>
        public DownloadDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(DeliveryDialogTab.TheBasics, new TheBasicsTabComponent());
        }

        /// <summary>
        /// The Basics Tab Component
        /// </summary>
        public TheBasicsTabComponent TheBasicsTab
            => this.SetActiveTab<TheBasicsTabComponent>(DeliveryDialogTab.TheBasics);

        /// <summary>
        /// Clicks 'Download' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickDownloadButton<T>() where T : BaseModuleRegressionDialog
            => this.ClickButtonAndWaitSpinnerToDisappear<T>(DownloadButtonLocator);

        /// <summary>
        /// This method sets up the download of the items, when Download dialog is already opened
        /// </summary>
        /// <returns> True if ready for download dialog is displayed, false otherwise </returns>
        public bool DownloadAndWaitForConfirmation<T>() where T : ICreatablePageObject
        {
            var readyForDownloadDialog = this.ClickDownloadButton<ReadyForDeliveryDialog>();
            bool isSuccessful = readyForDownloadDialog.IsReadyToDeliveryMessageDisplayed()
                                 || readyForDownloadDialog.IsDownloadButtonDisplayed();
            readyForDownloadDialog.MinimizeDeliveryDialog<T>();
            return isSuccessful;
        }

        /// <summary>
        /// Verifies that download button is displayed.
        /// </summary>
        /// <returns> The true if the download button is displayed. </returns>
        public bool IsDownloadButtonDisplayed() => DriverExtensions.IsDisplayed(DownloadButtonLocator, 5);
    }

   
}