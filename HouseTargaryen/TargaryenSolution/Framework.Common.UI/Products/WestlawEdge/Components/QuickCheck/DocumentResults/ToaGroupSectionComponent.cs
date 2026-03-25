namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Products.WestlawEdge.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Group section component
    /// Group Cases, Regulations, Statutes, Trial Court Orders doc types
    /// </summary>
    public class ToaGroupSectionComponent : BaseGroupSectionComponent
    {
        private static readonly By ResultItemLocator = By.XPath(".//div[@class='DA-TOACase']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ToaGroupSectionComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public ToaGroupSectionComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<QuickCheckBaseItem> ResultList =>
            new QuickCheckItemsCollection<QuickCheckBaseItem>(this.Container, ResultItemLocator, "div");
    }
}