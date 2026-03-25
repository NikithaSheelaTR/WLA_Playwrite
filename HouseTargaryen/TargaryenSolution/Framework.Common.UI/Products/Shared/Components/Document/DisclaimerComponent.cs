namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Disclaimer Component
    /// </summary>
    public class DisclaimerComponent : BaseModuleRegressionComponent
    {
        private static readonly By DisclaimerContainerLocator = By.ClassName("co_headnotePublicationBlockContainer");

        private static readonly By DisclaimerTitleLocator = By.ClassName("co_section_header");

        private static readonly By ContainerLocator = By.Id("co_disclaimer_block");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Disclaimer container for State Dockets
        /// </summary>
        public string DisclaimerContainer
            => DriverExtensions.GetElement(DriverExtensions.WaitForElement(this.ComponentLocator), DisclaimerContainerLocator).Text;

        /// <summary>
        /// Disclaimer title for State Dockets
        /// </summary>
        public string DisclaimerTitle
            => DriverExtensions.GetElement(DriverExtensions.WaitForElement(this.ComponentLocator), DisclaimerTitleLocator).Text;
    }
}