namespace Framework.Common.UI.Products.Shared.Components.Facets.RightFacets
{
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Form Families Facet Component - right facet on the document page
    /// </summary>
    public class FormFamiliesFacetComponent : BaseModuleRegressionComponent
    {
        private const string BigFormLinkLctMask = ".//a[@class='co_relatedInfo_topics_document_title' and text()='{0}']";

        private static readonly By ContainerLocator = By.Id("co_relatedinfo_formfamilies_container");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get text from Form Families Facet component
        /// </summary>
        /// <returns> Text from the Form Families Facet component </returns>
        public string GetTextFromFormFamiliesFacetComponent() => DriverExtensions.GetText(this.ComponentLocator);

        /// <summary>
        /// Click on the link in the Form Families component
        /// </summary>
        /// <param name="linkText"> Link text </param>
        /// <returns> The <see cref="BigFormsPage"/>. </returns>
        public BigFormsPage ClickFormFamiliesLink(string linkText)
        {
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(BigFormLinkLctMask, linkText))).Click();
            return new BigFormsPage();
        }
    }
}
