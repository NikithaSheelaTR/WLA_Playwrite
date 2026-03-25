namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Cited by component
    /// </summary>
    public class CitedByComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitedByComponent" /> class.
        /// </summary>
        /// <param name="container">Container</param>
        public CitedByComponent(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the component locator.
        /// </summary>
        protected override By ComponentLocator { get; }

        private IWebElement Container { get; }      
    
        /// <summary>
        /// Returns true if component is displayed
        /// </summary>
        /// <returns>true if component is displayed</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.Container);
    }
}
