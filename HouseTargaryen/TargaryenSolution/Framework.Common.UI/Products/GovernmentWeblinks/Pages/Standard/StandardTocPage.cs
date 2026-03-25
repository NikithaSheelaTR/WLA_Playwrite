namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;

    /// <summary>
    /// Standard toc page for weblinks product
    /// </summary>
    public class StandardTocPage : BaseGovernmentWeblinksPage
    {
        /// <summary>
        /// Header component
        /// </summary>
        public new WeblinksStandardHeaderComponent Header { get; } = new WeblinksStandardHeaderComponent();

        /// <summary>
        /// Toc component
        /// </summary>
        public WeblinksTocComponent TocComponent { get; } = new WeblinksTocComponent();
    }
}
