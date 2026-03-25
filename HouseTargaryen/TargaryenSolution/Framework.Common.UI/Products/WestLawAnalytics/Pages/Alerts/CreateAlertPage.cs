namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Create Alert Page
    /// </summary>
    public class CreateAlertPage : BaseAlertPage
    {
        /// <summary>
        /// Click on the Create Alert button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCreateAlertButton<T>() where T : ICreatablePageObject => this.ClickCreateUpdateAlertButton<T>();

        /// <summary>
        /// Create alert
        /// </summary>
        /// <param name="alertObject">
        /// The alert Object.
        /// </param>
        /// <returns>
        /// The <see cref="ManageAlertsPage"/>. 
        /// </returns>
        public ManageAlertsPage CreateAlert(AlertModel alertObject)
        {
            this.FillAlertFields(alertObject);
            return this.ClickCreateAlertButton<ManageAlertsPage>();
        }
    }
}