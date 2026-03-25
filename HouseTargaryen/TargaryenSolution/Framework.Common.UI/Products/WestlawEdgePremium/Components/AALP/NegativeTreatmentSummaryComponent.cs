namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;

    /// <summary>
    /// Negative Treatment summary component
    /// </summary>
    public class NegativeTreatmentSummaryComponent : BaseModuleRegressionComponent
    {
        private static readonly By NegativeTreatmentSummaryContainerLocator = By.Id("co_docDocumentAnalyzerResults");
        private static readonly By ProgressRingLabelLocator = By.XPath(".//saf-progress-ring[@role='progressbar']");
        private static readonly By SummaryHeadingLocator = By.XPath(".//h2[contains(@class, 'ResultDisclosure-module__documentAnalyzerDisclosureHeading')]");
        private static readonly By SummaryContentParagraphLocator = By.XPath(".//saf-card[contains(@class,'ResultContent-module__negativeTreatmentContentCard')]/p");
        private static readonly By SummaryContentKeyciteFlagLinkLocator = By.XPath(".//span[contains(@class,'SafeResponseFormatter-module__keyciteWrapper')]");
        private static readonly By SummaryContentKeyciteLinkLocator = By.XPath("./following-sibling::a");
        private static readonly By SummaryContentLocator = By.XPath(".//saf-disclosure[contains(@class, 'ResultDisclosure-module__documentAnalyzerDisclosure')]");
        private const string ExpandCollapseScript = "return(arguments[0].shadowRoot.querySelector('summary[class=invoker]'));";

        /// <summary>
        /// Progress ring label
        /// </summary>
        public ILabel ProgressRingLabel => new Label(ProgressRingLabelLocator);

        /// <summary>
        /// Summary Heading Label 
        /// </summary>
        public ILabel SummaryHeadingLabel => new Label(this.ComponentLocator, SummaryHeadingLocator);

        /// <summary>
        /// Summary Content paragrapghs
        /// </summary>
        public IReadOnlyCollection<ILabel> SummaryContentParagraphLabels => new ElementsCollection<Label>(this.ComponentLocator, SummaryContentParagraphLocator);

        /// <summary>
        /// Summary Content keycite flag links
        /// </summary>
        public IReadOnlyCollection<ILink> SummaryContentKeyciteFlagLinks => new ElementsCollection<Link>(this.ComponentLocator, SummaryContentKeyciteFlagLinkLocator);

        /// <summary>
        /// Summary Content document links
        /// </summary>
        public IReadOnlyCollection<ILink> SummaryContentDocumentLinks => new ElementsCollection<Link>(this.ComponentLocator, SummaryContentKeyciteFlagLinkLocator, SummaryContentKeyciteLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => NegativeTreatmentSummaryContainerLocator;

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