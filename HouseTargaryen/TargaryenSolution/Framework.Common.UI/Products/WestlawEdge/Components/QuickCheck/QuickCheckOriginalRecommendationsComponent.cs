namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{ 
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The doc analyzer original recommendations component.
    /// </summary>
    public class QuickCheckOriginalRecommendationsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultItemLocator = By.XPath("./ul/li");
        private static readonly By ShowAllLocator = By.ClassName("DA-MoreLessButton");
        private static readonly By ExpandOrCollapseButtonLocator = By.XPath("./span[contains(@class, 'icon')]");
        private static readonly By ExpandOrCollapsedLocator = By.ClassName("DA-ExpandCollapseButton");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckOriginalRecommendationsComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckOriginalRecommendationsComponent(By container) => this.ComponentLocator = container;

        /// <summary>
        /// The expanded.
        /// </summary>
        public bool Expanded => DriverExtensions.GetAttribute("aria-expanded", ExpandOrCollapsedLocator).Contains("true");

        /// <summary>
        /// Expand or collapse original report button
        /// </summary>
        public IButton ExpandOrCollapseButton => new CustomClickButton(ExpandOrCollapsedLocator, ExpandOrCollapseButtonLocator);

        /// <summary>
        /// show all component
        /// </summary>
        public ICheckBox ToggleShowAll => new OriginalReportExpandCheckbox(DriverExtensions.WaitForElement(ShowAllLocator));

        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<QuickCheckBaseItem> ResultList =>
            new QuickCheckItemsCollection<QuickCheckBaseItem>(this.ComponentLocator, ResultItemLocator, "div");

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
