namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Items.CompareText;

    using OpenQA.Selenium;

    /// <summary>
    /// Select to compare tab
    /// </summary>
    public class SelectToCompareTab : BaseCompareTextTab
    {
        private static readonly string ItemByDateMask = ".//li[./div[text() = '{0}']]//li[@class = 'co_redlineLightbox_tabItem']";

        private static readonly By ContainerLocator = By.XPath("//div[@id='panel_SelectToCompare']");
        private static readonly By CompareTextDateLocator = By.XPath(".//div[@class ='compareTextDate']");
        private static readonly By ItemCountLocator = By.XPath(".//span[@id='coid_redlineLightbox_snippetCountMessage']");
        
        /// <summary>
        /// List of compare text dates
        /// </summary>
        public IReadOnlyCollection<ILabel> CompareTextDates => new ElementsCollection<Label>(ContainerLocator, CompareTextDateLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Select to compare";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Group items by date
        /// </summary>
        /// <param name="date"> The date. </param>
        public IItemsCollection<SelectToCompareTabItem> GroupItemsByDate(string date)
            => new ItemsCollection<SelectToCompareTabItem>(this.ComponentLocator, By.XPath(string.Format(ItemByDateMask, date)));

        /// <summary>
        /// Returns number of snippets in the "Select to compare" tab
        /// </summary>
        public ILabel SnippetCount => new Label(this.ComponentLocator, ItemCountLocator);

    }
}