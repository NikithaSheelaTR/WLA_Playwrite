namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Quick check tool bar component
    /// </summary>
    public class CanadaQuickCheckToolbar : QuickCheckToolbar
    {
        private static readonly By SortByDropdownLocator = By.XPath("//div[@id='DA-SortSelectorContainer']");
        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaQuickCheckToolbar"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public CanadaQuickCheckToolbar(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The Quick Check sort dropdown.
        /// </summary>
        public CanadaQuickCheckSortDropdown CanadaQuickCheckSortDropdown =>
            new CanadaQuickCheckSortDropdown(DriverExtensions.WaitForElement(this.Container, SortByDropdownLocator));
    }
}