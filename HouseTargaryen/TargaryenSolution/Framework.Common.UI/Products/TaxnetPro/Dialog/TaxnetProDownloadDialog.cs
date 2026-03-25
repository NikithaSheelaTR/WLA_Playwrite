namespace Framework.Common.UI.Products.TaxnetPro.Dialog
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.TaxnetPro.Components.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;

    /// <summary>
    /// Taxnet pro3 download dialog
    /// </summary>
    public class TaxnetProDownloadDialog : DownloadDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxnetProDownloadDialog"/> class.
        /// </summary>
        public TaxnetProDownloadDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(DeliveryDialogTab.TheBasics, new TaxnetProDeliveryBasicTabComponent());
        }
    }
}
