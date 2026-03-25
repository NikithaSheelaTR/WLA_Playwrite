namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Contacts;

    using OpenQA.Selenium;

    /// <summary>
    /// Contacts component
    /// </summary>
    public class EdgeContactsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'co_share_contacts']");
        
        /// <summary>
        /// People
        /// </summary>
        public PeopleComponent People { get; } = new PeopleComponent();

        /// <summary>
        /// Group
        /// </summary>
        public GroupComponent Group { get; } = new GroupComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}