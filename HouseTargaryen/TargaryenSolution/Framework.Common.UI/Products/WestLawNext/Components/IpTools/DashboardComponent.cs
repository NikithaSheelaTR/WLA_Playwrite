namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dashboard Component
    /// </summary>
    public class DashboardComponent : BaseModuleRegressionComponent
    {
        private static readonly By DescriptionLocator = By.CssSelector("div.co_relatedInfo_dashboard_inner p");

        private static readonly By LinkLocator = By.CssSelector("a.co_relatedInfo_primaryLink");

        private static readonly By TitleLocator = By.CssSelector("div.co_relatedInfo_dashboard_inner h3");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardComponent"/> class. 
        /// </summary>
        /// <param name="containerLocator">Container locator</param>
        public DashboardComponent(By containerLocator)
        {
            this.componentLocator = containerLocator;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        /// <summary>
        /// Gets description
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDescription() => DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), DescriptionLocator).Text;

        /// <summary>
        /// Gets a title
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTitle() => DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), TitleLocator).Text;

        /// <summary>
        /// Checks if dashboard element is active
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDashboardElementActive()
            => this.GetTitle().RetrieveCountFromBrackets() != 0 && DriverExtensions.IsDisplayed(this.ComponentLocator, LinkLocator);

        /// <summary>
        /// Checks if Dashboard Element with specific title exist
        /// </summary>
        /// <param name="title">Dashboard Element title</param>
        /// <param name="description">Dashboard Element description.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDashboardElementPresent(string title, string description)
            => DriverExtensions.IsDisplayed(this.ComponentLocator) && this.GetTitle().Contains(title) && this.GetDescription().Equals(description);

        /// <summary>
        /// ClickSection
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <returns>New instance of a page T</returns>
        public T ClickSection<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), LinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}