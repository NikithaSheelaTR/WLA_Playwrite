namespace Framework.Common.UI.Products.Shared.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The section with the links to get back to the page after session has timed out.
    /// </summary>
    public class ContinueResearchingComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_clientIdSessionTimeoutLinks_container");

        private static readonly By ContinueResearchingButtonLocator = By.XPath(".//button[contains(@class,'co_primaryBtn')]");

        private static readonly By ContinueInOpenPagesButtonLocator = By.XPath(".//button[contains(@class,'co_defaultBtn')]");

        private static readonly By GoToHomePageLinkLocator = By.Id("coid_clientId_timeoutLink_home");

        /// <summary>
        /// ContinueResearchingButton
        /// </summary>
        public IButton ContinueResearchingButton => new Button(this.ComponentLocator, ContinueResearchingButtonLocator);

        /// <summary>
        /// ContinueInOpenPagesButton
        /// </summary>
        public IButton ContinueInOpenPagesButton => new Button(this.ComponentLocator, ContinueInOpenPagesButtonLocator);

        /// <summary>
        /// GoToHomePageButton
        /// </summary>
        public IButton GoToHomePageButton => new Button(this.ComponentLocator, GoToHomePageLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// IsDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);
    }
}