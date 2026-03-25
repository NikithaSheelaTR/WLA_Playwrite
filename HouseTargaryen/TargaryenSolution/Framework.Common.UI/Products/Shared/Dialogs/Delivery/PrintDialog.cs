namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.Utils.Windows;

    using OpenQA.Selenium;

    /// <summary>
    ///  Print Dialog
    /// </summary>
    public class PrintDialog : BaseDeliveryDialog
    {
        private static readonly By DeliveryPrintButtonLocator = By.Id("co_deliveryPrintButton");

        private static readonly By PrintLoadingMessageLocator = By.Id("coid_deliveryPrintLoading");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintDialog" /> class.
        /// </summary>
        public PrintDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(DeliveryDialogTab.TheBasics, new TheBasicsTabComponent());
        }

        /// <summary> 
        /// TheBasicsTab
        /// </summary>
        public TheBasicsTabComponent TheBasicsTab
            => this.SetActiveTab<TheBasicsTabComponent>(DeliveryDialogTab.TheBasics);

        /// <summary>
        /// Clicks Delivery Print Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickPrintButton<T>() where T : BaseModuleRegressionDialog
            => this.ClickButtonAndWaitSpinnerToDisappear<T>(DeliveryPrintButtonLocator);

        /// <summary>
        /// Clicks Delivery Print Button
        /// </summary>
        public void ClickPrintButton() => this.ClickElement(DeliveryPrintButtonLocator);

        /// <summary>
        /// Attempts to close any open browser print dialogs.
        /// </summary>
        /// <param name="browserType">The browser type.</param>
        /// <param name="printDialogActivator">The sequence of actions that cause a print dialog to open.</param>
        /// <returns>True if the print dialog was closed. False otherwise.</returns>
        public bool CloseBrowserPrintDialog(TestClientId browserType, Action printDialogActivator)
        {
            bool result;

            switch (browserType)
            {
                case TestClientId.Chrome:
                case TestClientId.ChromeCanary:
                    result = ProcessManager.CloseLastAddedProcess("chrome", printDialogActivator);
                    break;
                default:
                    throw new NotImplementedException(
                        $"Print dialog handling is not implemented for '{browserType}' browser type.");
            }

            return result;
        }

        /// <summary>
        /// Determines if the loading print request light box, which is usually shown before a browser print dialog opens, is showing.
        /// </summary>
        /// <returns> True if the loading print request light box is displayed. False otherwise. </returns>
        public bool IsLoadingPrintLightBoxDisplayed(int timeOut = 5000)
            => DriverExtensions.WaitForElement(PrintLoadingMessageLocator, timeOut).Displayed;

        /// <summary>
        /// Verifies that print button is displayed.
        /// </summary>
        /// <returns> True if print button is displayed </returns>
        public bool IsPrintButtonDisplayed() => DriverExtensions.IsDisplayed(DeliveryPrintButtonLocator, 5);

        /// <summary>
        /// Click Print.
        /// </summary>
        /// <returns> Clicks the print button </returns>
        public void WaitAndClickPrint()
        {
            DriverExtensions.WaitForCondition(condition => DriverExtensions.IsEnabled(DeliveryPrintButtonLocator));
            DriverExtensions.Click(DeliveryPrintButtonLocator);
        }
    }
}