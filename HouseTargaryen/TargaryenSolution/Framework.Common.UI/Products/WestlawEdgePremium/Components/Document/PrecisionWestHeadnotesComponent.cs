namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Precision West Headnotes component
    /// </summary>
    public class PrecisionWestHeadnotesComponent : WestHeadnotesComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_headnotes']");
        private static readonly By BlueBoxLocator = By.XPath(".//div[contains(@class, 'Athens-browseBox co_excludeAnnotations')]");
        private static readonly By HeadnoteMoreLikeThisLinkLocator = By.XPath(".//a[text()='More cases on this issue']");
        private static readonly By HeadnoteSummarizeButtonLocator = By.XPath(".//button[text()='Generate AI Insights']");

        /// <summary>
        /// Headnotes MLT links
        /// </summary>
        public IReadOnlyCollection<ILink> HeadnotesMoreLikeThisLinks => new ElementsCollection<Link>(this.ComponentLocator, HeadnoteMoreLikeThisLinkLocator);

        /// <summary>
        /// List blue boxes components
        /// </summary>
        public List<PrecisionDocumentBlueBoxComponent> BlueBoxes => DriverExtensions.GetElements(this.ComponentLocator, BlueBoxLocator).Select(item => new PrecisionDocumentBlueBoxComponent(item)).ToList();

        /// <summary>
        /// Headnotes Summarize the cases that cite this headnote buttons
        /// </summary>
        public IReadOnlyCollection<ILink> HeadnotesSummarizeButtons => new ElementsCollection<Link>(this.ComponentLocator, HeadnoteSummarizeButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}