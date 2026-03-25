namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Alerts
{
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.SearchResultsPageFacets;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The manage alerts page, where you can delete and search for alerts
    /// </summary>
    public class CanadaAlertCenterPage : AlertCenterPage
    {
        private const string AlertCitationTermLctMask = "//li[@class='co_user_alert_item' and .//span[text()='{0}']]//li[contains(text(), 'Citation')]";

        private static readonly By EditDeliveryLocator = By.XPath("//*[@class='co_editDelivery']");

        private static readonly By EmailLinkLocator = By.XPath("//*[@class='co_alert_emailRecipientLink']");

        private static readonly By EditSaveLocator = By.XPath("//*[@id='co_alertssuspend_saveButton']");

        private static readonly By AlertRunLocator = By.XPath("//*[@id='co_alertsRunSliderTab']");

        private static readonly By RunNowLocator = By.XPath("//*[@id='co_runNowContainer']");

        private static readonly By CitationLocator = By.XPath("//*[contains(@id,'alertcitationID')]");

        private static readonly By AlertMenuLocator = By.XPath("//*[@id='co_search_alertMenuLink']");

        private static readonly By UnsubscribeTextBoxLocator = By.XPath(".//*[@id='co_unsubscribeEmail']");

        private static readonly By UnsubscribeButtonLocator = By.XPath(".//*[@id='coid_website_alertUnsubscribe']");

        private static readonly By UnsubscribeLocator = By.XPath(".//*[@id='co_idlightboxOverlayBody']/div/h2");

        private static readonly By PauseButtonLocator = By.Id("co_pause");

        private static readonly By ResumeDateTextboxLocator = By.Id("endDate");

        /// <summary> 
        /// Get Alert Facet Component.
        /// </summary>
        public CanadaAlertFacetComponent CanadaAlertFacet { get; private set; } = new CanadaAlertFacetComponent();

        /// <summary>
        /// Gets the View Results Facet
        /// </summary>
        public ViewFacetComponent ViewResultsFacet { get; } = new ViewFacetComponent();

        /// <summary>
        /// Deletes Alert By Name if displayed
        /// </summary>
        /// <param name="nameOfAlert">name of Alert</param>
        public void DeleteAlertByNameIfDisplayed(string nameOfAlert)
        {
            if (this.IsAlertDisplayed(nameOfAlert))
            {
                this.DeleteAlertByName(nameOfAlert);
            }
        }

        /// <summary>
        /// Get citation text by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Citation text </returns>
        public string GetCitationTextByAlertName(string alertName)
        {
            string text = DriverExtensions.GetText(By.XPath(string.Format(AlertCitationTermLctMask, alertName)));
            return text.Substring(text.IndexOf(":", StringComparison.Ordinal) + 2);
        }

        /// <summary>
        /// Edit Delivery Button 
        /// </summary>
        public IButton EditDeliveryButton => new Button(EditDeliveryLocator);

        /// <summary>
        /// Save Alert Button  
        /// </summary>
        public IButton SaveAlertButton => new Button(EditSaveLocator);

        /// <summary>
        /// Alert Run Button
        /// </summary>
        public IButton AlertRunButton => new Button(AlertRunLocator);

        /// <summary>
        /// Run Now Button
        /// </summary>
        public IButton RunNowButton => new Button(RunNowLocator);

        /// <summary>
        /// Unsubscribe Button
        /// </summary>
        /// <returns> Summary Text </returns>
        public IButton UnsubscribeButton => new Button(UnsubscribeButtonLocator);

        /// <summary>
        /// Alert Menu Link
        /// </summary>
        public ILink AlertMenuLink => new Link(AlertMenuLocator);

        /// <summary>
        /// Email Recipients Link 
        /// </summary>
        public ILink EmailRecipientsLink => new Link(EmailLinkLocator);

        /// <summary>
        /// Citation Label
        /// </summary>
        public ILabel CitationLabel => new Label(CitationLocator);

        /// <summary>
        /// Unsubscribe Label
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel UnsubscribeLabel => new Label(UnsubscribeLocator);

        /// <summary>
        /// Unsubscribe Text box
        /// </summary>
        /// <returns> Summary Text </returns>
        public ITextbox UnsubscribeTextBox => new Textbox(UnsubscribeTextBoxLocator);

        /// <summary>
        /// Pause Button
        /// </summary>
        public IButton PauseButton => new Button(PauseButtonLocator);

        /// <summary>
        /// Resume Date Text box
        /// </summary>
        public ITextbox ResumeDateTextbox => new Textbox(ResumeDateTextboxLocator);
    }
}
