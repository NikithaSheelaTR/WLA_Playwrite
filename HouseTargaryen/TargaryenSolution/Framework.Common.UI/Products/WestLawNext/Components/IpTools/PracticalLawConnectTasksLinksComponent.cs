namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Enums.Widgets;

    using OpenQA.Selenium;

    /// <summary>
    /// Practical Law Connect Tasks Links Component
    /// </summary>
    public sealed class PracticalLawConnectTasksLinksComponent : LinksComponent
    {
        private const string ComponentLctMask =
            "//div[contains(@class,'co_tabShow')]//div[contains(@class,'co_genericBox')][contains(./h3/span/text(), '{0}')]";

        /// <summary>
        /// Initializes a new instance of the <see cref="PracticalLawConnectTasksLinksComponent"/> class. 
        /// </summary>
        public PracticalLawConnectTasksLinksComponent()
            : base(LinksNames.PracticalLawConnectTasks)
        {
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(ComponentLctMask, this.ComponentName));
    }
}
