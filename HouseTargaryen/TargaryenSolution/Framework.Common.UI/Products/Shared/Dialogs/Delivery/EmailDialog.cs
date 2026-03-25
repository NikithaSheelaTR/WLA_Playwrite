namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Email widget to use for delivery actions
    /// </summary>
    public class EmailDialog : BaseDeliveryDialog
    {
        private static readonly By EmailButtonLocator = By.Id("co_deliveryEmailButton");

        private static readonly By EmailTextFieldLocator = By.Id("co_delivery_emailAddress");

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailDialog"/> class. 
        /// </summary>
        public EmailDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(DeliveryDialogTab.Recipients, new RecipientsTabComponent());
        }

        /// <summary>
        /// TheBasicsTabComponent
        /// </summary>
        public RecipientsTabComponent RecipientsTab
            => this.SetActiveTab<RecipientsTabComponent>(DeliveryDialogTab.Recipients);

        /// <summary>
        /// Clicks the email button to deliver
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickEmailButton<T>() where T : BaseModuleRegressionDialog
            => this.ClickButtonAndWaitSpinnerToDisappear<T>(EmailButtonLocator);

        /// <summary>
        /// Enters a email in the the email text field and clicks 'email' button
        /// </summary>
        /// <param name="email"> The email to enter </param>
        /// <returns> page instance of Delivery Dialog </returns>
        public EmailDialog EnterEmailText(string email)
        {
            DriverExtensions.WaitForElementDisplayed(EmailTextFieldLocator);
            DriverExtensions.SetTextField(email, EmailTextFieldLocator);
            return this;
        }

        /// <summary>
        /// Verifies that email button is displayed.
        /// </summary>
        /// <returns> True if email button is displayed </returns>
        public bool IsEmailButtonDisplayed() => DriverExtensions.IsDisplayed(EmailButtonLocator, 5);
    }
}