namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision result list
    /// </summary>
    public class PrecisionResultListComponent : EdgeLegacyResultListComponent
    {     
        private IWebElement Container { get; }

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public PrecisionResultListComponent(IWebElement container) : base(container)
        {
            this.Container = container;
        }
    }
}
