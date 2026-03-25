namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The kindle delivery dialog.
    /// </summary>
    public class KindleDialog : BaseDeliveryDialog
    {
        private static readonly By SendButtonLocator = By.Id("co_deliverySendButton");

        /// <summary>
        /// Initializes a new instance of the <see cref="KindleDialog"/> class.
        /// </summary>
        public KindleDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(
                DeliveryDialogTab.TheBasics,
                new TheBasicsTabComponent());
        }

        /// <summary> 
        /// TheBasicsTab
        /// </summary>
        public TheBasicsTabComponent TheBasicsTab
            => this.SetActiveTab<TheBasicsTabComponent>(DeliveryDialogTab.TheBasics);

        /// <summary>
        /// The is send button displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsSendButtonDisplayed() => DriverExtensions.IsDisplayed(SendButtonLocator, 5);

        /// <summary>
        /// Clicks the Send button to deliver
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSendButton<T>() where T : BaseModuleRegressionDialog
            => this.ClickButtonAndWaitSpinnerToDisappear<T>(SendButtonLocator);
    }
}