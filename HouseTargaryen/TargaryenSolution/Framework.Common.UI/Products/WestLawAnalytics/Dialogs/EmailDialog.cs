namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Email Billing Investigation Results Dialog
    /// </summary>
    public class EmailDialog : BaseModuleRegressionDialog
    {
        private static readonly By EmailButtonLocator = By.Id("co_deliveryEmailButton");

        private static readonly By EmailToTextboxLocator = By.Id("co_delivery_emailAddress");

        private static readonly By EmailSubjectTextboxLocator = By.Id("co_delivery_subject");

        private static readonly By FormatOptionlocator = By.XPath("//select[@id='co_delivery_format_fulltext']/option");

        /// <summary>
        /// Click on the Email button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickEmailButton<T>() where T : BaseModuleRegressionDialog => this.ClickElement<T>(EmailButtonLocator);

        /// <summary>
        /// Enters an email address into the email field of the delivery dialog.
        /// </summary>
        /// <param name="emailAddress"> The email address </param>
        public void EnterDeliveryEmail(string emailAddress) => DriverExtensions.SetTextField(emailAddress, EmailToTextboxLocator);

        /// <summary>
        /// Get email subject
        /// </summary>
        /// <returns> Email Subject</returns>
        public string GetEmailSubject() => DriverExtensions.GetText(EmailSubjectTextboxLocator);

        /// <summary>
        /// Get Format options
        /// </summary>
        /// <returns> Format options</returns>
        public List<string> GetFormatOptions()
            => DriverExtensions.GetElements(FormatOptionlocator).Select(elem => elem.Text.Trim()).ToList();
    }
}
