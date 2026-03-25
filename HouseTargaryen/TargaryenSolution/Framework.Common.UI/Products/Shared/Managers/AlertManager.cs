namespace Framework.Common.UI.Products.Shared.Managers
{
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;

    /// <summary>
    /// Alert Manager
    /// </summary>
    public static class AlertManager
    {
        /// <summary>
        /// Makes a basic west clip alert
        /// </summary>
        /// <param name="alertModel"> The alert Model. </param>
        /// <returns> The <see cref="AlertCenterPage"/>. </returns>
        public static AlertCenterPage CreateBasicWestClipAlert(AlertModel alertModel)
        {
            SelectContentComponent component = new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.WestClip).Basics
                                  .SetNameText(alertModel.Name).EnterDescriptionText(alertModel.Description)
                                  .ClickContinue<SelectContentComponent>()
                                  .ClickAddContentCategory(alertModel.ContentCategoryToAdd);
            if (alertModel.Jurisdictions?.Length > 0)
            {
                new JurisdictionOptionsDialog().SelectJurisdictions(true, alertModel.Jurisdictions)
                                               .SaveButton.Click<SelectContentComponent>();
            }

            return component.ClickContinue<EnterSearchTermsComponent>().Search
                                  .EnterSearchText(alertModel.SearchText).ClickContinue<CustomizeDeliveryComponent>()
                                  .SetDeliveryCheckbox(true, AlertsDeliveryOption.Email, AlertsDeliveryOption.Html)
                                  .ExpandEmailSettingsSection().ExpandOtherSettingsSection()
                                  .DeleteAllRecipientsAndAddNewRecipient<CustomizeDeliveryComponent>(alertModel.Email)
                                  .ClickContinue<ScheduleAlertComponent>().ClickSaveAlertButton<AlertCenterPage>();
        }

        /// <summary>
        /// Makes a basic blc alert
        /// </summary>
        /// <param name="alertModel">Blc alert model</param>
        /// <returns>AlertCenterPage with added alert</returns>
        public static AlertCenterPage CreateBasicBlcAlert(AlertModel alertModel)
            => new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.BusinessLawCenter)
                                    .Basics.SetNameText(alertModel.Name)
                                    .EnterDescriptionText(alertModel.Description)
                                    .ClickContinue<SelectContentComponent>()
                                    .SelectYourContentItem(alertModel.ContentCategoryToAdd)
                                    .ClickContinue<EnterSearchTermsComponent>()
                                    .Search.EnterSearchText(alertModel.SearchText)
                                    .ClickContinue<CustomizeDeliveryComponent>()
                                    .DeleteAllRecipientsAndAddNewRecipient<CustomizeDeliveryComponent>(alertModel.Email)
                                    .ClickContinue<ScheduleAlertComponent>()
                                    .ClickSaveAlertButton<AlertCenterPage>();

        /// <summary>
        /// Fill up Basics, Select Content, Enter Search Terms sections for Docket Track alert to Delivery section
        /// </summary>
        /// <param name="alertModel"> Model to fill up </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public static CreateAlertPage CreateDocketTrackAlertAndFillupToDeliverySection(AlertModel alertModel)
        {
            var createPage = new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.DocketTrack)
                                             .Basics.SetNameText(alertModel.Name)
                                             .EnterDescriptionText(alertModel.Description)
                                             .ClickContinue<SelectContentComponent>()
                                             .ClickContentTab(alertModel.ContentTab)
                                             .ClickContentCategory(alertModel.ContentCategoryToSelect)
                                             .ClickAddContentCategory(alertModel.ContentCategoryToAdd)
                                             .EnterDocketNumber(alertModel.DocketNumber)
                                             .ClickContinue<CreateAlertPage>();
            return alertModel.LimitResultsBySearchTerms
                       ? createPage.EnterSearchTerm.SelectLimitResultsRadioButton()
                                   .Search.EnterSearchText(alertModel.SearchText)
                                   .ClickContinue<CreateAlertPage>()
                       : createPage.EnterSearchTerm.SelectDoNotLimitResultsRadioButton()
                                   .ClickContinue<CreateAlertPage>();
        }

        /// <summary>
        /// Fill up Basics, Select Content, Enter Search Terms sections for Docket alert to Delivery section
        /// </summary>
        /// <param name="alertModel"> Model to fill up </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public static CreateAlertPage CreateDocketAlertAndFillupToDeliverySection(AlertModel alertModel)
            =>
                new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.Docket)
                                     .Basics.SetNameText(alertModel.Name)
                                     .EnterDescriptionText(alertModel.Description)
                                     .ClickContinue<SelectContentComponent>()
                                     .ClickContentTab(alertModel.ContentTab)
                                     .ClickAddContentCategory(alertModel.ContentCategoryToAdd)
                                     .ClickContinue<EnterSearchTermsComponent>()
                                     .Search.EnterSearchText(alertModel.SearchText)
                                     .ClickContinue<CreateAlertPage>();

        /// <summary>
        /// Fill up Basics, Select Content, Enter Search Terms sections for WestClip alert to Delivery section
        /// </summary>
        /// <param name="alertModel"> Model to fill up </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public static CreateAlertPage CreateWestClipAlertAndFillupToDeliverySection(AlertModel alertModel)
            =>
                new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.WestClip)
                                     .Basics.SetNameText(alertModel.Name)
                                     .EnterDescriptionText(alertModel.Description)
                                     .ClickContinue<SelectContentComponent>()
                                     .ClickContentTab(alertModel.ContentTab)
                                     .ClickAddContentCategory(alertModel.ContentCategoryToAdd)
                                     .ClickContinue<EnterSearchTermsComponent>()
                                     .Search.EnterSearchText(alertModel.SearchText)
                                     .ClickContinue<CreateAlertPage>();

        /// <summary>
        /// Fill up Basics, Select Content, Enter Search Terms sections for Docket Tr ack alert to Delivery section
        /// </summary>
        /// <param name="alertModel"> Model to fill up </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public static CreateAlertPage CreateDocketTrackAlertAndFillUpToDeliverySection(AlertModel alertModel)
        {
            var createPage = new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.DocketTrack)
                                             .Basics.SetNameText(alertModel.Name)
                                             .EnterDescriptionText(alertModel.Description)
                                             .ClickContinue<SelectContentComponent>()
                                             .ClickContentTab(alertModel.ContentTab)
                                             .ClickContentCategory(alertModel.ContentCategoryToSelect)
                                             .ClickAddContentCategory(alertModel.ContentCategoryToAdd)
                                             .EnterDocketNumber(alertModel.DocketNumber)
                                             .ClickContinue<CreateAlertPage>();
            return alertModel.LimitResultsBySearchTerms
                       ? createPage.EnterSearchTerm.SelectLimitResultsRadioButton()
                                   .Search.EnterSearchText(alertModel.SearchText)
                                   .ClickContinue<CreateAlertPage>()
                       : createPage.EnterSearchTerm.SelectDoNotLimitResultsRadioButton()
                                   .ClickContinue<CreateAlertPage>();
        }

        /// <summary>
        /// Deletes alerts and newsletter alerts
        /// </summary>
        public static void DeleteAlerts()
        {
            var alertCenterPage = new AlertCenterPage();
            alertCenterPage.DeleteAllAlerts();
            AlertCenterPage newsletterSearchResultPage = alertCenterPage.ClickNewsletterLeftPaneLink();
            newsletterSearchResultPage.DeleteAllAlerts();
        }

        /// <summary>
        /// Verify alert Format
        /// </summary>
        public static AlertCenterPage VerifyWestClipAlertFormat(AlertModel alertModel)
        {
            SelectContentComponent component = new AlertCenterPage().ClickCreateAlertButton<CreateAlertPage>(AlertType.WestClip).Basics
                                  .SetNameText(alertModel.Name).EnterDescriptionText(alertModel.Description)
                                  .ClickContinue<SelectContentComponent>()
                                  .ClickAddContentCategory(alertModel.ContentCategoryToAdd);

            return component.ClickContinue<EnterSearchTermsComponent>().Search
                                  .EnterSearchText(alertModel.SearchText).ClickContinue<CustomizeDeliveryComponent>()
                                  .SetDeliveryCheckbox(true, AlertsDeliveryOption.Email)
                                  .ExpandEmailSettingsSection().ExpandOtherSettingsSection()
                                  .DeleteAllRecipientsAndAddNewRecipient<CustomizeDeliveryComponent>(alertModel.Email)
                                  .ExpandEmailSettingsSection()
                                  .GetDeliveryFormat<CustomizeDeliveryComponent>(alertModel.Format)
                                  .ClickContinue<ScheduleAlertComponent>().ClickSaveAlertButton<AlertCenterPage>();
        }
    }
}
