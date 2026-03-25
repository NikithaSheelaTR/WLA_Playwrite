namespace Framework.Common.UI.Products.WestLawNext.Components.Dockets.PrepareMultipleRequestComponents
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Items.Dockets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Entries table component
    /// </summary>
    public class EntriesTableComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//table[@class='co_detailsTable co_docketBatchDownload-content']");

        private static readonly By TableHeaderContainerLocator = By.XPath("./thead/tr");

        private static readonly By ProceedingRowsLocator = By.XPath("./tbody/tr[@class='co_detailsTable_batchDetailsTop']");

        /// <summary>
        /// Initializes a new instance of the <see cref="EntriesTableComponent"/> class. 
        /// The constructor.
        /// </summary>
        public EntriesTableComponent()
        {
            this.InitiateTableComponents();
        }

        /// <summary>
        /// Gets Table header.
        /// </summary>
        public TableHeaderComponent TableHeader
            => new TableHeaderComponent(DriverExtensions.GetElement(this.ComponentLocator, TableHeaderContainerLocator));

        /// <summary>
        /// Proceeding rows.
        /// </summary>
        public List<ProceedingItem> ProceedingList { get; private set; }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private void InitiateTableComponents()
        {
            this.ProceedingList = new List<ProceedingItem>();
            IList<IWebElement> proceedingRowsList = DriverExtensions.GetElements(this.ComponentLocator, ProceedingRowsLocator);
            foreach (IWebElement proceedingRowElement in proceedingRowsList)
            {
                var proceedingRowComponent = new ProceedingItem(proceedingRowElement);
                proceedingRowComponent.OnDelete += this.InitiateTableComponents;
                this.ProceedingList.Add(proceedingRowComponent);
            }
        }
    }
}