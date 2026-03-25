namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// The ProfileTab 
    /// </summary>
    public class ProfileTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_profileReport_tab");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Profile";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
