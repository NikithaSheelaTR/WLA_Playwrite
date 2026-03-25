namespace Framework.Common.UI.Products.WestlawAdvantage.Components
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
    /// Citing Reference Summary Component
    /// </summary>
    public class CitingReferenceSummaryComponent: BaseModuleRegressionComponent
    {
        private static readonly By CitingReferenceSummaryContainerLocator = By.Id("co_docDocumentAnalyzerResults");
        private static readonly By ProgressRingLabelLocator = By.XPath(".//saf-progress-ring[@role='progressbar']");
        private static readonly By SummaryHeadingLocator = By.XPath(".//*[contains(@class, 'ResultDisclosure-module__documentAnalyzerDisclosureHeading')]");
        private static readonly By SummaryContentDocumentLinksLocator = By.XPath(".//a[contains(@class,'SafeResponseFormatter-module__keyCiteLink')]");

        /// <summary>
        /// Progress ring label
        /// </summary>
        public ILabel ProgressRingLabel => new Label(ProgressRingLabelLocator);

        /// <summary>
        /// Heading label
        /// </summary>
        public ILabel SummaryHeadingLabel => new Label(this.ComponentLocator,SummaryHeadingLocator);

        /// <summary>
        /// Heading label
        /// </summary>
        public IReadOnlyCollection<ILink> SummaryContentDocumentLinks => new ElementsCollection<Link>(this.ComponentLocator, SummaryContentDocumentLinksLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => CitingReferenceSummaryContainerLocator;
    }
}
