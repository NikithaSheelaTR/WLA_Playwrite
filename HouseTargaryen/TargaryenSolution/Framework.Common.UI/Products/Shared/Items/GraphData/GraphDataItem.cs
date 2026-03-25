

namespace Framework.Common.UI.Products.Shared.Items.GraphData
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    ///Graph Data Item
    /// </summary>
    public class GraphDataItem : BaseItem
    {
        private static readonly By ItemTextLocator = By.XPath(".//label");

        private static readonly By ItemCheckBoxLocator = By.XPath(".//input");

        private static readonly By ItemCountLocator = By.XPath(".//span");

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphDataItem"/> class. 
        ///  </summary>
        /// <param name="container"> container </param>
        public GraphDataItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        ///Get item count
        ///  </summary>
        public string Count => DriverExtensions.GetElement(this.Container, ItemCountLocator).Text;

        /// <summary>
        ///Check if checkbox is selected
        ///  </summary>
        public bool IsSelected => DriverExtensions.GetElement(this.Container, ItemCheckBoxLocator).Selected;

        /// <summary>
        ///Get item text
        ///  </summary>
        public string Text => DriverExtensions.GetElement(this.Container, ItemTextLocator).Text;

        /// <summary>
        ///Set checkbox
        ///  </summary>
        public void SetCheckbox (bool set = true) => DriverExtensions.SetCheckbox(set, this.Container,ItemCheckBoxLocator);
    }
}
