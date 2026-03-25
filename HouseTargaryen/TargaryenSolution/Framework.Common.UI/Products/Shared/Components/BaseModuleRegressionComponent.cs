namespace Framework.Common.UI.Products.Shared.Components
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// Base component object for module regression suites
    /// </summary>
    public abstract class BaseModuleRegressionComponent : BaseWebObject
    {
        /// <summary>
        /// Component locator
        /// </summary>
        protected abstract By ComponentLocator { get; }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public virtual bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Verify that component is enabled
        /// </summary>
        /// <returns> True if component is enabled, false otherwise </returns>
        public virtual bool IsEnabled() => DriverExtensions.IsEnabled(this.ComponentLocator);
    }
}