namespace Framework.Common.UI.Products.WestLawNext.Components.Dockets.PrepareMultipleRequestComponents
{
    using System;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Header component
    /// </summary>
    public class TableHeaderComponent : BaseModuleRegressionComponent
    {
        private const int Timeout = 5000;

        private static readonly By TableHeaderSelectAllCheckboxLocator = By.XPath("./th[@class='co_detailsTable_select']/input");

        private static readonly By TableHeaderPdfLocator = By.XPath("./th[@class='co_detailsTable_icon']");

        private static readonly By TableHeaderDescriptionLocator = By.XPath("./th[@class='co_detailsTable_description']");

        private static readonly By TableHeaderEntryNumberLocator = By.XPath("./th[@class='co_detailsTable_entry']");

        private static readonly By TableHeaderDateLocator = By.XPath("./th[@class='co_detailsTable_date']");

        private static readonly By TableHeaderTrashLocator = By.XPath("./th[@class='co_detailsTable_status']");

        private readonly IWebElement tableHeaderContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableHeaderComponent"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="tableHeaderContainer">
        /// The table Header Container.
        /// </param>
        public TableHeaderComponent(IWebElement tableHeaderContainer)
        {
            this.tableHeaderContainer = tableHeaderContainer;
        }

        /// <summary>
        /// Is not implemented
        /// </summary>
        protected override By ComponentLocator
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Set the desired state of checkbox
        /// </summary>
        /// <param name="desiredState">true to check, false to uncheck</param>
        public void SetSelectAllCheckbox(bool desiredState) => DriverExtensions.SetCheckbox(
            desiredState,
            this.tableHeaderContainer,
            TableHeaderSelectAllCheckboxLocator);

        /// <summary>
        /// Get checkbox status
        /// </summary>
        /// <returns>true if all checkboxes are selected</returns>
        public bool IsSelectAllCheckboxSelected() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderSelectAllCheckboxLocator)
                && DriverExtensions.IsCheckboxSelected(this.tableHeaderContainer, TableHeaderSelectAllCheckboxLocator);

        /// <summary>
        /// Get description text.
        /// </summary>
        /// <returns>Description text</returns>
        public string GetDescriptionText() => DriverExtensions.GetText(TableHeaderDescriptionLocator, this.tableHeaderContainer, Timeout);

        /// <summary>
        /// Get entry number.
        /// </summary>
        /// <returns>Entry number text</returns>
        public string GetEntryNumberText() => DriverExtensions.GetText(TableHeaderEntryNumberLocator, this.tableHeaderContainer, Timeout);

        /// <summary>
        /// Get date text.
        /// </summary>
        /// <returns>Date text</returns>
        public string GetDateText() => DriverExtensions.GetText(TableHeaderDateLocator, this.tableHeaderContainer, Timeout);

        /// <summary>
        /// Is checkbox column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsCheckboxColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderSelectAllCheckboxLocator);

        /// <summary>
        /// Is Pdf column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsPdfColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderPdfLocator);

        /// <summary>
        /// Is description column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsDescriptionColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderDescriptionLocator);

        /// <summary>
        /// Is entry number column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsEntryNumberColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderEntryNumberLocator);

        /// <summary>
        /// Is date column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsDateColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderDateLocator);

        /// <summary>
        /// Is trash column displayed.
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsTrashColumnDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer, TableHeaderTrashLocator);

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.tableHeaderContainer);
    }
}