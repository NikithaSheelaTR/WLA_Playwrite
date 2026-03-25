namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// History Tab Component
    /// </summary>
    public class HistoryTabComponent : BaseTabComponent
    {
        private static readonly By AttachmentFormatDropdownLocator =
            By.XPath("//select[@id='coid_userSettingsDeliverHistoryEmailFormatOption']");

        private static readonly By EmailDetailedSessionSummaryCheckboxLocator =
            By.Id("coid_userSettingsDeliverHistoryAtSignOffOption");

        private static readonly By ToTextBoxLocator = By.Id(
            "coid_userSettingsDeliverHistoryAtSignOffEmailAddressOption");

        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel4");

        private EnumPropertyMapper<HistoryTab, WebElementInfo> historyTabMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "History";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the HistoryTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HistoryTab, WebElementInfo> HistoryTabMap
            => this.historyTabMap = this.historyTabMap ?? EnumPropertyModelCache.GetMap<HistoryTab, WebElementInfo>();

        /// <summary>
        /// Get Attachment Format Set Option
        /// </summary>
        /// <returns>set option text</returns>
        public string GetAttachmentFormatSetOption()
        {
            return DriverExtensions.GetSelectedDropdownOptionText(AttachmentFormatDropdownLocator);
        }

        /// <summary>
        /// Get To Field Email
        /// </summary>
        /// <returns>email</returns>
        public string GetToFieldEmailText()
        {
            return DriverExtensions.GetText(ToTextBoxLocator);
        }

        /// <summary>
        /// Returns true if the specified element on the history tab is displayed
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>If the option is visible</returns>
        public bool IsDisplayed(HistoryTab tabOption)
        {
            By locator = By.Id(this.HistoryTabMap[tabOption].Id);
            return DriverExtensions.IsDisplayed(locator);
        }

        /// <summary>
        /// Returns true if the specified element on the history tab is selected 
        /// </summary>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsEmailDetailedSessionSummarySelected()
        {
            return DriverExtensions.IsCheckboxSelected(EmailDetailedSessionSummaryCheckboxLocator);
        }

        /// <summary>
        /// Is field Enabled
        /// </summary>
        /// <param name="tabOption">tab Option</param>
        /// <returns>true if field is disabled</returns>
        public bool IsEnabled(HistoryTab tabOption)
        {
            By locator = By.Id(this.HistoryTabMap[tabOption].Id);
            return DriverExtensions.GetElement(locator).Enabled;
        }

        /// <summary>
        /// Sets the specified checkbox option on the history tab to the specified value.
        /// </summary>
        /// <param name="state"> state of the checkbox </param>
        /// <returns> The <see cref="HistoryTabComponent"/>HistoryTabComponent</returns>
        public HistoryTabComponent SetEmailDetailedSessionSummaryCheckbox(bool state)
        {
            DriverExtensions.GetElement(EmailDetailedSessionSummaryCheckboxLocator).SetCheckbox(state);
            return this;
        }
    }
}