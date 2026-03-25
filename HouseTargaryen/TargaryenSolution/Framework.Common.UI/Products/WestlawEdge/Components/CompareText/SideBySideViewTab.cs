namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Side by Side View tab
    /// </summary>
    public class SideBySideViewTab : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[./div[@id='panel_SideBySideCompareContent']]");
        private static readonly By InvertComparisonButtonLocator = By.XPath(".//span[text() = 'Invert comparison']");
        
        /// <summary>
        /// Invert comparison button
        /// </summary>
        public IButton InvertComparisonButton => new Button(this.ComponentLocator, InvertComparisonButtonLocator);

        /// <summary>
        /// Toolbar
        /// </summary>
        public SideBySideViewTabToolbarComponent Toolbar => new SideBySideViewTabToolbarComponent(this.ComponentLocator);

        /// <summary>
        /// Left content component
        /// </summary>
        public LeftContentComponent LeftContent { get; } = new LeftContentComponent();

        /// <summary>
        /// Right content component
        /// </summary>
        public RightContentComponent RightContent { get; } = new RightContentComponent();

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Side By Side View";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}