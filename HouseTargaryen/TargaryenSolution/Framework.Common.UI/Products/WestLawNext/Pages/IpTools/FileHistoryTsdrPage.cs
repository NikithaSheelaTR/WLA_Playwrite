namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using OpenQA.Selenium;

    /// <summary>
    /// Intellectual property Trademark File History (TSDR) related info tab
    /// TSDR Toolbar (Custom toolbar)
    /// Application Information section
    /// File History section with File History Table
    /// </summary>
    public class FileHistoryTsdrPage : TabPage
    {
        private static readonly By BatchExportButtonLocator = By.XPath("//button[@id='batchExportButton']");

        private static readonly By BatchDeliveryButtonLocator = By.XPath("//button[@id='deliveryLink1']");

        private static readonly By BatchDeliveryDropdownContainerLocator = By.XPath("//*[@id='co_TsdrDeliveryWidget_PDF']");

        private static readonly By DeliveryRestrictionLabelLocator = By.XPath("//div[@id ='co_TsdrDeliveryWidget_PDF']//div[@class = 'co_infoBox_message']");

        /// <summary>
        /// Application Information section
        /// </summary>
        public ApplicationInfoComponent ApplicationInfo => new ApplicationInfoComponent();

        /// <summary>
        /// Pdf documents file History table component
        /// </summary>
        public FileHistoryTableComponent FileHistoryTable => new FileHistoryTableComponent();

        /// <summary>
        /// Gets the delivery dropdown.
        /// </summary>
        public DeliveryDropdown BatchDeliveryDropdown => new DeliveryDropdown(BatchDeliveryDropdownContainerLocator);

        /// <summary>
        /// The Batch export button
        /// </summary>
        public IButton BatchExportButton => new Button(BatchExportButtonLocator);

        /// <summary>
        /// The Batch delivery button
        /// </summary>
        public IButton BatchDeliveryButton => new Button(BatchDeliveryButtonLocator);

        /// <summary>
        /// The delivery restriction Label
        /// </summary>
        public ILabel DeliveryRestrictionLabel => new Label(DeliveryRestrictionLabelLocator);
    }
}
