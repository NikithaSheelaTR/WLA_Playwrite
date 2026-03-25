namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd base category item.
    /// </summary>
    public class TrdBaseCategoryItem : BaseItem
    {
        private static readonly By SearchTermLocator = By.XPath(".//span[@class='co_searchTerm']");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdBaseCategoryItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdBaseCategoryItem(IWebElement container): base(container)
        {
        }

        /// <summary>
        /// The search term.
        /// </summary>
        public List<string> SearchTerm
            =>
            DriverExtensions.GetElements(this.Container, SearchTermLocator)
                            .Where(x => x.Text.Length > 0)
                            .Select(term => term.Text)
                            .ToList();
         

        /// <summary>
        /// The link text.
        /// </summary>
        public virtual string Text => DriverExtensions.GetElement(this.Container)?.Text ?? string.Empty;

    }
}