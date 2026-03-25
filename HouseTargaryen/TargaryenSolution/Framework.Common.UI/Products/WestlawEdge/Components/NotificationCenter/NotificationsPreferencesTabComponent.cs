namespace Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter
{
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The notifications preferences tab component.
    /// </summary>
    public class NotificationsPreferencesTabComponent : BaseTabComponent
    {
        private const string CheckboxLctMask = "//span[contains(text(),'{0}')]";

        private static readonly By ContainerLocator = By.Id("coid_userSettingsNotificationsContent");

        private EnumPropertyMapper<NotificationsTabCheckboxes, WebElementInfo> checkboxesMap;

        private EnumPropertyMapper<NotificationsTabDisplayRadioButtons, WebElementInfo> displayRadiobuttonsMap;

        private EnumPropertyMapper<NotificationsTabLabels, WebElementInfo> labelsMap;

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => EdgePreferencesDialogTabs.Notifications.ToString();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The display radiobuttons map.
        /// </summary>
        private EnumPropertyMapper<NotificationsTabDisplayRadioButtons, WebElementInfo> DisplayRadiobuttonsMap =>
            this.displayRadiobuttonsMap = this.displayRadiobuttonsMap
                                          ?? EnumPropertyModelCache
                                              .GetMap<NotificationsTabDisplayRadioButtons, WebElementInfo>();

        /// <summary>
        /// The labels map.
        /// </summary>
        private EnumPropertyMapper<NotificationsTabLabels, WebElementInfo> LabelsMap =>
            this.labelsMap = this.labelsMap ?? EnumPropertyModelCache.GetMap<NotificationsTabLabels, WebElementInfo>();

        /// <summary>
        /// The checkboxes map.
        /// </summary>
        private EnumPropertyMapper<NotificationsTabCheckboxes, WebElementInfo> CheckboxesMap =>
            this.checkboxesMap = this.checkboxesMap
                                 ?? EnumPropertyModelCache.GetMap<NotificationsTabCheckboxes, WebElementInfo>();

        /// <summary>
        /// The is shared content checkbox displayed.
        /// </summary>
        /// <param name="checkbox">
        /// The checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxDisplayed(NotificationsTabCheckboxes checkbox) =>
            DriverExtensions.IsDisplayed(By.XPath(this.CheckboxesMap[checkbox].LocatorString));

        /// <summary>
        /// The is shared content checkbox selected.
        /// </summary>
        /// <param name="checkbox">
        /// The checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected(NotificationsTabCheckboxes checkbox) =>
            DriverExtensions.IsCheckboxSelected(By.XPath(this.CheckboxesMap[checkbox].LocatorString));

        /// <summary>
        /// The is checkbox enabled.
        /// </summary>
        /// <param name="checkbox">
        /// The checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxEnabled(NotificationsTabCheckboxes checkbox) =>
            DriverExtensions.IsEnabled(By.XPath(this.CheckboxesMap[checkbox].LocatorString));

        /// <summary>
        /// The is radiobutton enabled.
        /// </summary>
        /// <param name="radiobutton">
        /// The radiobutton.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRadiobuttonEnabled(NotificationsTabDisplayRadioButtons radiobutton) =>
            DriverExtensions.IsEnabled(By.XPath(this.DisplayRadiobuttonsMap[radiobutton].LocatorString));

        /// <summary>
        /// Selects shared content checkbox.
        /// </summary>
        /// <param name="checkbox"> The checkbox. </param>
        /// <param name="selected"> The selected. </param>
        /// <returns> The <see cref="PreferencesDialog"/>. </returns>
        public PreferencesDialog SetCheckbox(NotificationsTabCheckboxes checkbox, bool selected)
        {
            DriverExtensions.SetCheckbox(By.XPath(this.CheckboxesMap[checkbox].LocatorString), selected);
            return new PreferencesDialog();
        }

        /// <summary>
        /// Selects the specified radio button option on the 'Notifications' tab.
        /// </summary>
        /// <param name="radioButton"> the option to look for </param>
        /// <returns>
        /// The <see cref="NotificationsPreferencesTabComponent"/></returns>
        public NotificationsPreferencesTabComponent SelectRadioButton(NotificationsTabDisplayRadioButtons radioButton)
        {
            DriverExtensions.GetElement(By.XPath(this.DisplayRadiobuttonsMap[radioButton].LocatorString)).Click();
            return this;
        }

        /// <summary>
        /// The is checkbox name correct.
        /// </summary>
        /// <param name="checkbox">
        /// The checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxNameCorrect(NotificationsTabCheckboxes checkbox)
            => DriverExtensions.GetText(By.XPath(string.Format(CheckboxLctMask, this.CheckboxesMap[checkbox].Text)))
            .Normalize().Equals(this.CheckboxesMap[checkbox].Text);

        /// <summary>
        /// The is radio button displayed.
        /// </summary>
        /// <param name="radiobutton">
        /// The radiobutton.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRadiobuttonDisplayed(NotificationsTabDisplayRadioButtons radiobutton) =>
            DriverExtensions.IsDisplayed(By.XPath(this.DisplayRadiobuttonsMap[radiobutton].LocatorString));

        /// <summary>
        /// The is radio button selected.
        /// </summary>
        /// <param name="radiobutton">
        /// The radiobutton.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRadioButtonSelected(NotificationsTabDisplayRadioButtons radiobutton) =>
            DriverExtensions.IsRadioButtonSelected(By.XPath(this.DisplayRadiobuttonsMap[radiobutton].LocatorString));

        /// <summary>
        /// The is label displayed.
        /// </summary>
        /// <param name="label">
        /// The label.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLabelDisplayed(NotificationsTabLabels label) =>
            DriverExtensions.IsDisplayed(By.XPath(this.LabelsMap[label].LocatorString));

        /// <summary>
        /// The get label text.
        /// </summary>
        /// <param name="label">
        /// The label.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLabelText(NotificationsTabLabels label) =>
            DriverExtensions.GetText(By.XPath(this.LabelsMap[label].LocatorString)).Normalize();
    }
}