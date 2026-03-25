namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// The patent file history page.
    /// </summary>
    public class PatentFileHistoryPage : TabPage
    {
        /// <summary>
        /// GridComponent
        /// </summary>
        public PatentFileHistoryGridComponent GridComponent => new PatentFileHistoryGridComponent();
    }
}