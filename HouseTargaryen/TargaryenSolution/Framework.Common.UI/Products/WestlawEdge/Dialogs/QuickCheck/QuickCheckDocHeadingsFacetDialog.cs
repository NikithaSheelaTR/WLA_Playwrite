namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// The document analyzer doc headings facet dialog.
    /// </summary>
    public class QuickCheckDocHeadingsFacetDialog : BaseModuleRegressionDialog
    {
        private static readonly By HeaderItemLocator = By.XPath(".//tr[@class]");

        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_docHeadingFacetModal']");

        private static readonly By FilterButtonLocator = By.XPath(".//button[.='Filter']");

        /// <summary>
        /// Gets the filter button.
        /// </summary>
        public IButton FilterButton => new Button(ContainerLocator, FilterButtonLocator);
        
        /// <summary>
        /// The get headings.
        /// </summary>
        /// <returns>
        /// The list of headings
        /// </returns>
        public ItemsCollection<DocumentHeadingItem> Headings =>
            new ItemsCollection<DocumentHeadingItem>(ContainerLocator, HeaderItemLocator);
    }
}