namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.ResultList
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;

    /// <summary>
    /// Canada View Keep List Dialog
    /// </summary>
    public class CanadaViewKeepListDialog : PrecisionViewKeepListDialog
    {
        /// <summary>
        /// Delivery dropdown Component
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();
    }
}