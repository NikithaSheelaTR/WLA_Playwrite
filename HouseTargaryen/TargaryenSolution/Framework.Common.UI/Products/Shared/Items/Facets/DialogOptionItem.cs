namespace Framework.Common.UI.Products.Shared.Items.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog Option Item
    /// </summary>
    public class DialogOptionItem : BaseItem
    {
        private static readonly By CountLocator = By.XPath(".//a/span[@class='count']");

        private static readonly By CurrentItemLocator = By.XPath(".//a/span[@class='name']/span[@class='co_searchTerm' or @class='co_currentSearchTerm']");

        private static readonly By NameLocator = By.XPath(".//a/span[@class='name']");

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogOptionItem"/> class. 
        /// </summary>
        /// <param name="container"> Element container </param>
        public DialogOptionItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count => DriverExtensions.GetElement(this.Container, CountLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Name
        /// </summary>
        public string Name => DriverExtensions.GetElement(this.Container, NameLocator).Text;

        /// <summary>
        /// Selected Name Text
        /// </summary>
        public string SelectedName
            => DriverExtensions.IsDisplayed(this.Container, CurrentItemLocator)
                    ? DriverExtensions.GetElement(this.Container, CurrentItemLocator).Text
                    : string.Empty;
    }
}