namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document citation components
    /// </summary>
    public class DocumentCitationComponent : BaseModuleRegressionComponent
    {
        // Document Body or Footer
        private static readonly By AllCitationsBlockLocator = By.XPath("//div[@class='co_parallelCites']");

        private static readonly By AllCitationsLabelLocator = By.CssSelector("h2.co_parallelCitesBlockLabel");

        private static readonly By PrimaryCitationLocator = By.XPath("//*[@id='cite'] | //*[@id='cite0']");

        private static readonly By SecondaryCitationLocator = By.XPath("//*[@id='secondaryCite1'] | //*[@id='cite1']");

        private static readonly By SeeAllCitationsLink = By.Id("citeAdditional");

        private static readonly By ContainerLocator = By.Id("co_docHeaderCitation");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get 'All Citations' label
        /// </summary>
        /// <returns> 'All Citations' label </returns>
        public string GetAllCitationsLabelText() => DriverExtensions.WaitForElement(AllCitationsLabelLocator).Text;

        /// <summary>
        /// Get text from the 'All Citations' container
        /// </summary>
        /// <returns> All Citations </returns>
        public string GetAllCitationsText()
            => DriverExtensions.GetElement(AllCitationsBlockLocator).Text.Split(new[] { "\r\n" }, StringSplitOptions.None)[1];

        /// <summary>
        /// Get Primary document citation text
        /// </summary>
        /// <returns> Primary citation as text </returns>
        public string GetPrimaryCitationText() => DriverExtensions.GetElement(this.ComponentLocator, PrimaryCitationLocator).Text;

        /// <summary>
        /// Get Secondary document citation text
        /// </summary>
        /// <returns> Secondary citation as text </returns>
        public string GetSecondaryCitationText() => DriverExtensions.GetElement(this.ComponentLocator, SecondaryCitationLocator).Text;

        /// <summary>
        /// Get See All Citations Link Text
        /// </summary>
        /// <returns> Link text as string </returns>
        public string GetSeeAllCitationsLinkText() => DriverExtensions.GetElement(SeeAllCitationsLink).Text;

        /// <summary>
        /// Verify is See All Citation Link displayed
        /// </summary>
        /// <returns> True if See All Citation link is displayed </returns>
        public bool IsSeeAllCitationsLinkDisplayed() => DriverExtensions.IsDisplayed(SeeAllCitationsLink);

        /// <summary>
        /// Get document header citations.
        /// </summary>
        /// <returns>List of citations</returns>
        public IList<string> GetDocumentHeaderCitaions() =>
            DriverExtensions.GetElements(this.ComponentLocator, By.TagName("li")).Select(c => c.Text).ToList();

        /// <summary>
        /// GetCitationContainerText
        /// </summary>
        /// <returns> Container text </returns>
        public string GetCitationContainerText() => DriverExtensions.WaitForElement(this.ComponentLocator).Text;
    }
}