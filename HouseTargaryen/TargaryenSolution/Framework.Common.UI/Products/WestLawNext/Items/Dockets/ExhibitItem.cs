namespace Framework.Common.UI.Products.WestLawNext.Items.Dockets
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Exhibit Row Component
    /// </summary>
    public class ExhibitItem : BaseItem
    {
        private const int Timeout = 3000;

        private static readonly By RowSelectExhibitCheckboxLocator = By.XPath("./td[@class='co_detailsTable_select co_relative']/input");

        private static readonly By RowPdfIconLocator = By.XPath("./td[@class='co_detailsTable_icon']/span");

        private static readonly By RowDescriptionLocator = By.XPath("./td[@class='co_detailsTable_description']");

        private static readonly By RowEntryNumberLocator = By.XPath("./td[@class='co_detailsTable_entry']");

        private static readonly By RowStatusLocator = By.XPath("./td[@class='co_detailsTable_status']");

        private static readonly By IconSpanLocator = By.XPath("./td[@class='co_detailsTable_status']/span");

        private static readonly By GreenCaratLocator = By.XPath(".//a[@class='co_docketStatus active']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExhibitItem"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="exhibitRowContainer"> container </param>
        public ExhibitItem(IWebElement exhibitRowContainer)
            : base(exhibitRowContainer)
        {
        }

        /// <summary>
        /// Get description text.
        /// </summary>
        /// <returns>Description text</returns>
        public string GetDescriptionText() => DriverExtensions.GetText(RowDescriptionLocator, this.Container, Timeout);

        /// <summary>
        /// Get entry number.
        /// </summary>
        /// <returns>Entry number text</returns>
        public string GetEntryNumberText() => DriverExtensions.GetText(RowEntryNumberLocator, this.Container, Timeout);

        /// <summary>
        /// Get status icon class attribute value.
        /// </summary>
        /// <returns>attribute value or error message with details</returns>
        public string GetStatusText() => DriverExtensions.IsElementPresent(this.Container, IconSpanLocator)
                                                ? DriverExtensions.GetAttribute("class", this.Container, IconSpanLocator)
                                                : DriverExtensions.GetText(RowStatusLocator, this.Container, Timeout);

        /// <summary>
        /// Get Pdf icon attribute value.
        /// </summary>
        /// <returns>attribute value</returns>
        public string GetPdfIconText() => DriverExtensions.GetAttribute("class", this.Container, RowPdfIconLocator);

        /// <summary>
        /// Set the desired state of checkbox
        /// </summary>
        /// <param name="desiredState">true to check, false to uncheck</param>
        public void SetCheckBox(bool desiredState)
        {
            if (DriverExtensions.IsDisplayed(this.Container, RowSelectExhibitCheckboxLocator))
            {
                DriverExtensions.SetCheckbox(
                    desiredState,
                    this.Container,
                    RowSelectExhibitCheckboxLocator);
            }
        }

        /// <summary>
        /// Get checkbox status
        /// </summary>
        /// <returns>true if selected</returns>
        public bool IsExhibitCheckboxSelected() => DriverExtensions.IsDisplayed(this.Container, RowSelectExhibitCheckboxLocator)
                                                    && DriverExtensions.IsCheckboxSelected(this.Container, RowSelectExhibitCheckboxLocator);

        /// <summary>
        /// Is Green Carat Displayed
        /// </summary>
        /// <returns>true if selected</returns>
        public bool IsGreenCaratDisplayed() => DriverExtensions.IsDisplayed(this.Container, GreenCaratLocator);

        /// <summary>
        /// Get Green Carat title 
        /// </summary>
        /// <returns>Green Carat title</returns>
        public string GetGreenCaratTitle() =>
            DriverExtensions.GetAttribute("title", DriverExtensions.GetElement(this.Container, GreenCaratLocator));
    }
}