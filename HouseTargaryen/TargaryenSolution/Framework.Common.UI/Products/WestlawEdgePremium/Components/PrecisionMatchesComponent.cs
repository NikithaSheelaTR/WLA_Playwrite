namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision matches component
    /// </summary>
    public class PrecisionMatchesComponent : PrecisionBaseMatchesComponent
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionMatchesComponent "/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionMatchesComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;
    }
}
