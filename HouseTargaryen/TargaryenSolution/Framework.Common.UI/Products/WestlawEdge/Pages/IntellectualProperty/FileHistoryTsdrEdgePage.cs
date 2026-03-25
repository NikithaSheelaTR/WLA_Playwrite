namespace Framework.Common.UI.Products.WestlawEdge.Pages.IntellectualProperty
{
    using Framework.Common.UI.Products.WestlawEdge.Components.ToC;
    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;

    /// <summary>
    /// File History Tsdr document Edge page
    /// </summary>
    public class FileHistoryTsdrEdgePage : FileHistoryTsdrPage
    {
        /// <summary> Table of Contents component </summary>
        public EdgeTocComponent Toc { get; } = new EdgeTocComponent();
    }
}
