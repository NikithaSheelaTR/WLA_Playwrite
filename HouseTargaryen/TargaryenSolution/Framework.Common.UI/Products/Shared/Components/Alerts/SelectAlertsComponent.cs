namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Select Alerts Section
    /// </summary>
    public class SelectAlertsComponent : BaseAlertComponent
    {
        private const string AlertGroupByNameLctMask = "//li[contains(label,{0})]//i[@role='checkbox']";

        private const string AlertLinkByNumberLctMask = "ul#co_facet_Alert_availableOptions li:nth-child({0})";

        private static readonly By AlertsListLocator = By.CssSelector("ul#co_facet_Alert_availableOptions li");

        private static readonly By AlertGroupsTabLocator = By.XPath("//button[@id='coid_alertGroupsItemSelectorTab']");

        private static readonly By YourAlertSelectionsLocator = By.Id("coid_itemCollector_item_0");

        private static readonly By ContainerLocator = By.XPath("//li[@id='contentSection' and .//*[contains(.,'Select Alerts')]]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Selects a specific alert text by index (1 based)
        /// </summary>
        /// <param name="index"> alert index in the drop down list </param>
        /// <returns> Alert text </returns>
        public string GetAlertTextByIndex(int index) => DriverExtensions.GetText(By.CssSelector(string.Format(AlertLinkByNumberLctMask, index)));

        /// <summary>
        /// Get Your Selections
        /// </summary>
        /// <returns> The alert selections </returns>
        public string GetYourAlertSelectionsText() => DriverExtensions.GetText(YourAlertSelectionsLocator);

        /// <summary>
        /// Selects a specific alert by index (1 based)
        /// </summary>
        /// <param name="index"> alert index in the drop down list </param>
        /// <returns> The <see cref="SelectAlertsComponent"/>. </returns>
        public SelectAlertsComponent SelectAlertByIndex(int index)
        {
            DriverExtensions.GetElement(By.CssSelector(string.Format(AlertLinkByNumberLctMask, index))).Click();
            return this;
        }

        /// <summary>
        /// Select Alert Group by name
        /// </summary>
        /// <param name="groupName"> group Name  </param>
        /// <returns> The <see cref="SelectAlertsComponent"/>. </returns>
        public SelectAlertsComponent SelectAlertGroup(string groupName)
        {
            DriverExtensions.GetElement(AlertGroupsTabLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(AlertGroupByNameLctMask, groupName)).Click();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Selects Alerts
        /// </summary>
        /// <param name="alertName"> Name of the Alert</param>
        /// <param name="alertType">Type of the Alert</param>  
        public void SelectAlertsByName(string alertName, AlertType alertType) => DriverExtensions.GetElements(AlertsListLocator).FirstOrDefault(Alert => Alert.Text.Contains(alertName) && Alert.Text.Contains(alertType.ToString())).Click();
    }
}