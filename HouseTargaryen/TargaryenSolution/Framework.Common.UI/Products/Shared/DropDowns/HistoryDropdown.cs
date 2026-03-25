namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// HistoryDropdown in Toolbar
    /// </summary>
    public class HistoryDropdown : Dropdown<HistoryOptions>
    {
        private static readonly By HistoryDropdownLocator = By.XPath("//button[@id='co_history_viewOptions_id']");
        private static readonly By HistoryDropdownMenu = By.XPath("//ul[@id='co_history_viewOptions']/li[@role='menuitemradio']");
        private static readonly By HistoryItemsLocator = By.XPath(".//span[@class='a11yDropdown-itemText']");

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryDropdown"/> class.
        /// </summary>
        public HistoryDropdown() : base(HistoryDropdownLocator)
        {
        }

        /// <summary>
        /// Retrieve count from the brackets
        /// </summary>
        /// <param name="historyOption"> The history option </param>
        /// <returns> Count of the history option </returns>
        public int GetOptionCount(HistoryOptions historyOption)
            => DriverExtensions.GetText(By.XPath(this.Map[historyOption].LocatorString)).ConvertCountToInt();

        /// <summary>
        /// Return History Dropdown Options
        /// </summary>

        public List<HistoryOptions> ToList()
        {
            if (!DriverExtensions.IsDisplayed(HistoryDropdownMenu)) DriverExtensions.Click(HistoryDropdownLocator);
            DriverExtensions.WaitForElement(HistoryDropdownMenu);
            DriverExtensions.WaitForElement(HistoryItemsLocator);
            return DriverExtensions.GetElements(HistoryDropdownMenu).Select(li => li.FindElement(HistoryItemsLocator).Text.Trim()).Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => this.GetOptionByText(t)).Distinct().ToList();
        }

        /// <summary>
        /// Gets the currently selected history option from History button text
        /// </summary>
        public HistoryOptions GetSelectedHistoryOption()
        {
            var button = this.GetContainer();
            return this.GetOptionByText(button.Text.Trim());
        }

        /// <summary>
        /// Gets available history options from the a11y dropdown menu items
        /// </summary>
        public new IEnumerable<HistoryOptions> Options => this.ToList();

        /// <summary>
        /// Gets the selected option text from the dropdown button
        /// </summary>
        public new string SelectedOptionText => this.GetContainer().Text;

        /// <summary>
        /// Checks if a history option is enabled in the dropdown
        /// </summary>
        public new bool IsEnabled(HistoryOptions option)
        {
            var optionLocator = By.XPath(this.Map[option].LocatorString);
            return DriverExtensions.IsDisplayed(optionLocator);
        }

        /// <summary>
        /// Select history option from dropdown 
        /// </summary>
        public void SelectHistoryOption(HistoryOptions option)
        {
            if (!DriverExtensions.IsDisplayed(HistoryDropdownMenu))
            {
                DriverExtensions.Click(HistoryDropdownLocator);
            }
            DriverExtensions.WaitForElement(HistoryDropdownMenu);
            var optionLocator = By.XPath(this.Map[option].LocatorString);
            DriverExtensions.Click(DriverExtensions.WaitForElement(optionLocator));
            DriverExtensions.WaitForElementNotDisplayed(HistoryDropdownMenu);
        }


        /// <summary>
        /// Get option by name
        /// </summary>
        /// <param name="text"> Option's text </param>
        /// <returns> Option </returns>
        protected override HistoryOptions GetOptionByText(string text)
            => text.RetainText().GetEnumValueByText<HistoryOptions>();

        /// <summary>
        /// Get text of the option
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> Option's text </returns>
        protected override string GetOptionText(HistoryOptions option) => DriverExtensions.GetText(By.XPath(this.Map[option].LocatorString));
    }
}