namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Models;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Any create alert page, using components to differentiate the specific create alert pages
    /// </summary>
    public class CreateAlertPage : CommomCreateAlertPage
    {
        private static readonly By AlertCenterLink = By.XPath("//div[@id='breadcrumb']/a");

        private static readonly By BreadcrumbLocator = By.Id("breadcrumb");

        private static readonly By AlertWizardTitleLocator = By.XPath("//h1[@class='co_alertWizardTitle']");

        /// <summary>
        ///  Alert wizard title label
        /// </summary>
        public ILabel AlertWizardTitleLabel => new Label(AlertWizardTitleLocator);

        /// <summary>
        /// Select Content
        /// </summary>
        public SelectContentComponent SelectContent { get; private set; } = new SelectContentComponent();

        /// <summary>
        /// Enter Search Term
        /// </summary>
        public EnterSearchTermsComponent EnterSearchTerm { get; private set; } = new EnterSearchTermsComponent();

        /// <summary>
        /// News Enter Search Term for WestClip alert with News content
        /// </summary>
        public NewsEnterSearchTermComponent NewsEnterSearchTerm { get; private set; } = new NewsEnterSearchTermComponent();

        /// <summary>
        /// Clicks alert center breadcrumb link
        /// </summary>
        /// <returns> The <see cref="AlertCenterPage"/>. </returns>
        public AlertCenterPage ClickAlertCenterLink()
        {
            DriverExtensions.ScrollToTop();
            DriverExtensions.GetElement(AlertCenterLink).Click();
            return new AlertCenterPage();
        }

        /// <summary>
        /// Get breadcrumb text.
        /// </summary>
        /// <returns>Breadcrumb text</returns>
        public string GetBreadcrumbText() => DriverExtensions.GetText(BreadcrumbLocator);

        /// <summary>
        /// Fill out Basics, Select Content, Search Term, Customize Delivery Schedule sections
        /// </summary>
        /// <param name="alert"> Alert </param>
        /// <returns> The <see cref="AlertCenterPage"/>. </returns>
        public AlertCenterPage FillAllSectionsForWestClipAlert(AlertModel alert)
            =>
                this.FillOutBasicsSection<SelectContentComponent>(alert.Name, alert.Description)
                    .ClickAddContentCategory(alert.ContentCategoryToAdd)
                    .ClickContinue<EnterSearchTermsComponent>()
                    .Search.EnterSearchText(alert.SearchText)
                    .ClickContinue<CustomizeDeliveryComponent>()
                    .SetDeliveryCheckbox(true, AlertsDeliveryOption.Email, AlertsDeliveryOption.Html)
                    .ExpandEmailSettingsSection()
                    .ExpandOtherSettingsSection()
                    .DeleteAllRecipientsAndAddNewRecipient<CustomizeDeliveryComponent>(alert.Email)
                    .ClickContinue<ScheduleAlertComponent>()
                    .ClickSaveAlertButton<AlertCenterPage>();
    }
}