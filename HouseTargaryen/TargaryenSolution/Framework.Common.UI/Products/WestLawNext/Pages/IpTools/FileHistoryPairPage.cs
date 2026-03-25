namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// Intellectual property Patents and Applications File History (Pair) related info tab
    /// </summary>
    public class FileHistoryPairPage : TabPage
    {
        /// <summary>
        /// Application Information section
        /// </summary>
        public ApplicationInfoComponent ApplicationInfo => new ApplicationInfoComponent();
    }
}
