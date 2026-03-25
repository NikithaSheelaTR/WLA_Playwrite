namespace Framework.Common.UI.Products.WestlawEdge.Pages.IntellectualProperty
{
    using Framework.Common.UI.Products.WestlawEdge.Components.ToC;
    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;

    /// <summary>
    /// Tsdr document Edge page
    /// </summary>
    public class TsdrDocumentEdgePage : TsdrDocumentPage
    {
        /// <summary> Table of Contents component </summary>
        public EdgeTocComponent Toc { get; } = new EdgeTocComponent();
    }
}
