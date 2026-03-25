namespace Framework.Common.UI.Products.WestLawNext.Items.Dockets
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNext.Components.Dockets.PrepareMultipleRequestComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The ProceedingRow component
    /// </summary>
    public class ProceedingItem : BaseItem
    {
        /// <summary>
        /// The default timeout.
        /// </summary>
        private const int Timeout = 30000;

        private const string ConnectedExhibitLctMask =
            "./following-sibling::tr[@data-pdf-index='{0}']";

        private const string ExhibitTitleRowContainerLctMask =
            "./following-sibling::tr[@data-pdf-index='{0}' and @data-is-exhibit='false']";

        private const string ExhibitRowsLctMask = "./following-sibling::tr[@data-pdf-index='{0}' and @data-is-exhibit='true']";

        private static readonly By RowSelectCheckboxLocator = By.XPath("./td[@class='co_detailsTable_select co_relative']/input");

        private static readonly By GreenCaratLocator = By.XPath(".//a[@class='co_docketStatus active']");

        private static readonly By RowPdfIconLocator = By.XPath("./td[@class='co_detailsTable_icon']/span");

        private static readonly By RowDescriptionLocator = By.XPath("./td[@class='co_detailsTable_description']");

        private static readonly By RowEntryNumberLocator = By.XPath("./td[@class='co_detailsTable_entry']");

        private static readonly By RowDateLocator = By.XPath("./td[@class='co_detailsTable_date']");

        private static readonly By RowStatusLocator = By.XPath("./td[@class='co_detailsTable_status']");

        private static readonly By SpanStatusLocator = By.XPath("./td[@class='co_detailsTable_status']/span");

        private static readonly By TrashLinkLocator = By.XPath("./td[@class='co_detailsTable_status']/a[@id='co_trash']");

        private readonly string dataPdfIndex;
        
        /// <summary>
        /// <see cref="ExhibitsTitleComponent"/>
        /// </summary>
        public ExhibitsTitleComponent ExhibitTitleComponent;

        /// <summary>
        /// Gets the Exhibits row component list
        /// <see cref="ExhibitItem"/>
        /// </summary>
        public List<ExhibitItem> ExhibitRowComponentList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProceedingItem"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="rowContainer">IWebElement container</param>
        public ProceedingItem(IWebElement rowContainer) : base(rowContainer)
        {
            this.dataPdfIndex = rowContainer.GetAttribute("data-pdf-index");
            this.InitiateExhibitComponent();
        }

        /// <summary>
        /// The on delete action method container.
        /// </summary>
        internal delegate void OnDeleteAction();

        /// <summary>
        /// Event, raised on delete method.
        /// </summary>
        internal event OnDeleteAction OnDelete;

        /// <summary>
        /// Performs delete the current proceeding
        /// </summary>
        public void DeleteProceeding()
        {
            if (DriverExtensions.IsDisplayed(this.Container, TrashLinkLocator))
            {
                DriverExtensions.GetElement(this.Container, TrashLinkLocator).Click();
                this.OnDelete?.Invoke();
            }
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
        /// Get date text.
        /// </summary>
        /// <returns>Date text</returns>
        public string GetDateText() => DriverExtensions.GetText(RowDateLocator, this.Container, Timeout);

        /// <summary>
        /// Get status class attribute value.
        /// </summary>
        /// <returns>attribute value</returns>
        public string GetStatusText()
        {
            if (DriverExtensions.IsDisplayed(this.Container, TrashLinkLocator))
            {
                return DriverExtensions.GetAttribute("class", this.Container, TrashLinkLocator);
            }

            return DriverExtensions.IsDisplayed(this.Container, SpanStatusLocator)
                ? DriverExtensions.GetAttribute("class", this.Container, SpanStatusLocator) 
                : DriverExtensions.GetText(RowStatusLocator, this.Container, Timeout / 10);
        }

        /// <summary>
        /// Get Pdf icon attribute value.
        /// </summary>
        /// <returns>attribute value</returns>
        public string GetPdfIconText() => DriverExtensions.GetAttribute("class", this.Container, RowPdfIconLocator);

        /// <summary>
        /// Set the desired state of checkbox
        /// </summary>
        /// <param name="desiredState">true to check, false to uncheck</param>
        public void SetProceeding(bool desiredState) => DriverExtensions.SetCheckbox(
            desiredState,
            this.Container,
            RowSelectCheckboxLocator);

        /// <summary>
        /// Get checkbox status
        /// </summary>
        /// <returns>true if selected</returns>
        public bool IsProceedingCheckboxSelected() => DriverExtensions.IsDisplayed(this.Container, RowSelectCheckboxLocator)
                && DriverExtensions.IsCheckboxSelected(this.Container, RowSelectCheckboxLocator);

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

        private void InitiateExhibitComponent()
        {
            if (DriverExtensions.GetElements(
                    this.Container,
                    By.XPath(string.Format(ConnectedExhibitLctMask, this.dataPdfIndex))).Count > 1)
            {
                this.ExhibitTitleComponent = new ExhibitsTitleComponent(
                    DriverExtensions.GetElement(this.Container, By.XPath(string.Format(ExhibitTitleRowContainerLctMask, this.dataPdfIndex))));
                IList<IWebElement> exhibitsRows = DriverExtensions.GetElements(
                    this.Container,
                    By.XPath(string.Format(ExhibitRowsLctMask, this.dataPdfIndex)));
                this.ExhibitRowComponentList = new List<ExhibitItem>();
                foreach (var exhibitRow in exhibitsRows)
                {
                    if (exhibitRow.GetAttribute("style") != "display: none;")
                    {
                        this.ExhibitRowComponentList.Add(new ExhibitItem(exhibitRow));
                    }
                }
            }
        }
    }
}