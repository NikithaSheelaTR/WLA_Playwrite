namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Pages.Document;

    /// <summary>
    /// Intellectual property PAIR document page
    /// </summary>
    public class PairDocumentPage : CommonDocumentPage
    {
        /// <summary>
        /// Application Information section
        /// </summary>
        public ApplicationInfoComponent ApplicationInfo => new ApplicationInfoComponent();
    }
}
