namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Products.WestlawEdge.Components.CompareText;

    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar on the Compare View tab
    /// </summary>
    public class CompareViewTabToolbarComponent : BaseCompareTextReportToolbarComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareViewTabToolbarComponent"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="tabComponentLocator">
        /// </param>
        public CompareViewTabToolbarComponent(By tabComponentLocator)
        {
            this.ComponentLocator = tabComponentLocator;
        }

        /// <summary>
        /// Navigation component
        /// </summary>
        public CompareTextNavigationComponent NavigationComponent => new CompareTextNavigationComponent(this.ComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}