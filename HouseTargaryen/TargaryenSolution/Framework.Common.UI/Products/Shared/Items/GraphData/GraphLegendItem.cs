namespace Framework.Common.UI.Products.Shared.Items.GraphData
{
    using System.Linq;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    ///Graph Legend Item
    /// </summary>
    public class GraphLegendItem : BaseItem
    {
        private static readonly By ItemsTextLocator = By.XPath(".//*[name() = 'tspan' and @id = 'coid_legendItemHide']/following-sibling::*");

        private static readonly By ItemsCountLocator = By.XPath("./*[name() = 'text']/*[name() ='tspan'][1]");

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphLegendItem"/> class. 
        ///  </summary>
        /// <param name="container"> container </param>
        public GraphLegendItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Get count 
        ///  </summary>
        public string NumericValue => DriverExtensions.GetElement(this.Container, ItemsCountLocator).Text;

        /// <summary>
        /// Get text 
        ///  </summary>
        public string Text => string.Join(" ", DriverExtensions.GetElements(this.Container, ItemsTextLocator)
                                                               .Select(elem => elem.Text).ToArray());
    }
}