namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Pages.Browse;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro3 Table Of Contents page
    /// </summary>
    public class TaxnetProTableOfContentsPage : CommonBrowsePage
    {
        private static readonly By TableOfContentsResultCountLocator = By.ClassName("co_hitCountTOCLabel");

        /// <summary>
        /// Table of Contents result count label
        /// </summary>
        public ILabel TableOfContentsResultCountLabel => new Label(TableOfContentsResultCountLocator);
    }
}
