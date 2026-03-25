namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.DocumentMisc
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous;

    /// <summary>
    /// Document Miscelleneous Page
    /// </summary>
    public class DocumentMiscPage : CommonDocumentPage
    {
        /// <summary>
        /// Miscellaneous Component
        /// </summary>
        public CanadaMiscellaneousComponent Miscellaneous { get; } = new CanadaMiscellaneousComponent();

        /// <summary>
        /// CustomDigestBrowse Page
        /// </summary>
        public CustomDigestBrowsePage CustomDigest { get; } = new CustomDigestBrowsePage();

        /// <summary>
        /// Document Miscellaneous Component
        /// </summary>
        public DocumentMiscellaneousComponent DocMiscellaneous { get; } = new DocumentMiscellaneousComponent();

        /// <summary>
        /// Miscellaneous ProgressOfBills
        /// </summary>
        public MiscellaneousProgressOfBillsComponent MiscPOB { get; } = new MiscellaneousProgressOfBillsComponent();
    }
}
