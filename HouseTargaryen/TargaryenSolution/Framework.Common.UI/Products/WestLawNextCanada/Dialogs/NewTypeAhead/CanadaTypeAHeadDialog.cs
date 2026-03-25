namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NewTypeAhead
{
    using Framework.Common.UI.Products.WestLawNext.Dialogs.TypeAhead;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.NewTypeAhead;

    /// <summary>
    /// Canada Next Type A Head dialog
    /// </summary>
    public class CanadaTypeAHeadDialog : TypeAheadDialog
    {
        /// <summary>
        /// TabPanel
        /// </summary>
        public LarefCasesAndDecisionsComponent LarefCasesAndDecisions { get; set; } = new LarefCasesAndDecisionsComponent();
    }
}