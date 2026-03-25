namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar on the Side By Side View tab
    /// </summary>
    public class SideBySideViewTabToolbarComponent : BaseCompareTextReportToolbarComponent
    {        
        private static readonly By ShowSimilaritiesRadioButtonLocator = By.XPath(".//label[text() = 'Show similarities']/input");
        private static readonly By ShowDifferencesRadioButtonLocator = By.XPath(".//label[text() = 'Show differences']/input");

        /// <summary>
        /// Initializes a new instance of the <see cref="SideBySideViewTabToolbarComponent"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="tabComponentLocator">
        /// </param>
        public SideBySideViewTabToolbarComponent(By tabComponentLocator)
        {
            this.ComponentLocator = tabComponentLocator;
        }

        /// <summary>
        /// Show similarities radio button
        /// </summary>
        public IRadiobutton ShowSimilaritiesRadioButton => new Radiobutton(this.ComponentLocator, ShowSimilaritiesRadioButtonLocator);

        /// <summary>
        /// Show differences radio button
        /// </summary>
        public IRadiobutton ShowDifferencesRadioButton => new Radiobutton(this.ComponentLocator, ShowDifferencesRadioButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}