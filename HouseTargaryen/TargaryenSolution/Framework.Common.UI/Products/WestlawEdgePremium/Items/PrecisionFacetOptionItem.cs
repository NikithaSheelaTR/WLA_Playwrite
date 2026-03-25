namespace Framework.Common.UI.Products.WestlawEdgePremium.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.Browser;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Precision facet item
    /// </summary>
    public class PrecisionFacetOptionItem : BaseItem
    {
        private const string ExpandCollapseButtonLctMask = ".//parent::div//button[contains(@class,'Icon-{0}')]";

        private static readonly By ItemCheckboxLocator = By.XPath(".//span[@class='co_treeItemSelection'] | .//input[@type='checkbox']");
        private static readonly By ItemTitleLinkLocator = By.XPath(".//*[contains(@class,'PrecisionSearch-labelText') or contains(@class,'PrecisionFilters-labelText')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetOptionItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public PrecisionFacetOptionItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Item title link
        /// </summary>
        public ILink ItemTitleLink => new Link(Container, ItemTitleLinkLocator);

        /// <summary>
        /// Item checkbox
        /// </summary>
        public IIndeterminateCheckBox ItemCheckBox => new IndeterminateCheckBox(Container, ItemCheckboxLocator);

        /// <summary>
        /// Expand/Collapse button
        /// </summary>
        public IButton ExpandCollapseButton(string state) => new Button(Container, By.XPath(string.Format(ExpandCollapseButtonLctMask, state)));

        /// <summary>
        /// Expand or Collapse item
        /// </summary>
        public void ChangeFacetItemState(bool state)
        {
            if (state)
            {
                this.ExpandCollapseButton("collapsed").Click();
            }
            else
            {
                this.ExpandCollapseButton("expanded").Click();
            }
        }

        /// <summary>
        ///Click on checkbox
        /// </summary>
        public void ClickItemCheckBox()
        {
            var role = Container.GetAttribute("role");
            if (role == "treeitem")
            {
                // First try: Click the co_treeItemSelection span
                var selectionSpan = Container.FindElements(ItemCheckboxLocator).FirstOrDefault();
                if (selectionSpan != null)
                {
                    var jsExecutor = (IJavaScriptExecutor)BrowserPool.CurrentBrowser.Driver;
                    jsExecutor.ExecuteScript("arguments[0].click();", selectionSpan);
                }
            }
        }
    }
}