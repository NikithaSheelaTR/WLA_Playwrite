namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Update Alert Page
    /// </summary>
    public class UpdateAlertPage : BaseAlertPage
    {
        /// <summary>
        /// Click on the Update Alert button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickUpdateAlertButton<T>() where T : ICreatablePageObject => this.ClickCreateUpdateAlertButton<T>();

        /// <summary>
        /// Get Prepopulated Alert object
        /// </summary>
        /// <returns> The <see cref="AlertModel"/>. </returns>
        public AlertModel GetPrepopulatedAlert()
            =>
                new AlertModel
                    {
                        Email = DriverExtensions.GetText(DeliverToTextboxLocator),
                        AlertName = DriverExtensions.GetText(AlertNameTextboxLocator),
                        CapAmount = DriverExtensions.GetText(CapAmountTextboxLocator),
                        ApplyTo = this.ApplyToDropdown.SelectedOption,
                        CostCondition = this.CostConditionDropdown.SelectedOption,
                        GreaterOrLessThan = this.GreaterOrLessThanDropdown.SelectedOption,
                        TimeFrame = this.TimeFrameDropdown.SelectedOption
                    };

        /// <summary>
        /// Update Alert
        /// </summary>
        /// <param name="alertObject">
        /// The alert Object.
        /// </param>
        /// <returns>
        /// The <see cref="ManageAlertsPage"/>. 
        /// </returns>
        public ManageAlertsPage UpdateAlert(AlertModel alertObject)
        {
            this.FillAlertFields(alertObject);
            return this.ClickUpdateAlertButton<ManageAlertsPage>();
        }
    }
}