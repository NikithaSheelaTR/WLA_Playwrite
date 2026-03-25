namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Pages.Document;

    /// <summary>
    /// Westlaw Super Browse Page
    /// </summary>
    public class WestlawSuperBrowsePage : BaseSuperBrowsePage
    {
        /// <summary> Toolbar component  </summary>
        public Toolbar Toolbar { get; protected set; } = new Toolbar();
    }
}
