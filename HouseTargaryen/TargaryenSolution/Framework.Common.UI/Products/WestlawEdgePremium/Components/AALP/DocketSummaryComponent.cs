namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;

    /// <summary>
    /// Docket summary component
    /// </summary>
    public class DocketSummaryComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocketSummaryContainerLocator = By.Id("co_docDocumentAnalyzerResults");
        private static readonly By ProgressRingLabelLocator = By.XPath(".//saf-progress-ring[@role='progressbar']");
        private static readonly By DisclaimerLabelLocator = By.XPath(".//p[contains(@class,'ResultDisclosure-module__documentAnalyzerDisclaimer')]");
        private static readonly By SummaryContentLabelLocator = By.XPath(".//div[contains(@class,'ResultContent-module__documentAnalyzerContent')]");
        private static readonly By BottomMsgLabelLocator = By.XPath(".//p[contains(@class,'ResultDisclosure-module__documentAnalyzerBottomMsg')]");
        private static readonly By ErrorLabelLocator = By.XPath(".//saf-alert[contains(@data-testid,'error-alert')]");
        private static readonly By SummaryContentLocator = By.XPath(".//saf-disclosure[contains(@class, 'ResultDisclosure-module__documentAnalyzerDisclosure')]");
        private const string ExpandCollapseScript = "return(arguments[0].shadowRoot.querySelector('summary[class=invoker]'));";

        /// <summary>
        /// Bottom message Label
        /// </summary>
        public ILabel BottomMsgLabel => new Label(this.ComponentLocator, BottomMsgLabelLocator);

        /// <summary>
        /// Disclaimer Label
        /// </summary>
        public ILabel DisclaimerLabel => new Label(this.ComponentLocator, DisclaimerLabelLocator);

        /// <summary>
        /// Error Label
        /// </summary>
        public ILabel ErrorLabel => new Label(this.ComponentLocator, ErrorLabelLocator);

        /// <summary>
        /// Progress ring label
        /// </summary>
        public ILabel ProgressRingLabel => new Label(ProgressRingLabelLocator);

        /// <summary>
        /// Summary Content label
        /// </summary>
        public ILabel SummaryContentLabel => new Label(SummaryContentLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocketSummaryContainerLocator;

        /// <summary>
        /// Click Expand/Collapse button and return the expanded flag status
        /// </summary>
        public bool ClickExpandCollapseButton()
        {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SummaryContentLocator); // Shadow host 
            IWebElement expandCollapseElement = (IWebElement)DriverExtensions.ExecuteScript(ExpandCollapseScript, searchAreaElement);
            expandCollapseElement.Click();
            var isSummaryExpanded = Convert.ToBoolean(searchAreaElement.GetAttribute("expanded"));
            return isSummaryExpanded;
        }
    }
}
