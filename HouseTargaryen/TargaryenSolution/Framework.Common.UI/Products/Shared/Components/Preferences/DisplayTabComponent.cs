namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Display Tab Component
    /// </summary>
    public class DisplayTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_userSettingsTab6Link");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Display";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}