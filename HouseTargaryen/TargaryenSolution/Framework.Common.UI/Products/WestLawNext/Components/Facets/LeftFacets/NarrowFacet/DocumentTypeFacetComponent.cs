namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// DocumentTypeFacetComponent
    /// </summary>
    public class DocumentTypeFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./label[contains(text(),'{0}')]]/input";

        private static readonly By DocumentTypesLinkLocator = By.ClassName("co_multiple_xboxes_link_testimonyType");

        private static readonly By ContainerLocator = By.Id("facet_div_testimonyType");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="checkbox"> checkbox to apply </param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkbox, bool setTo = true) where T : ICreatablePageObject
        {
            if (DriverExtensions.IsDisplayed(DocumentTypesLinkLocator))
            {
                DriverExtensions.GetElement(DocumentTypesLinkLocator).CustomClick();
            }

            return this.SetCheckbox<T>(DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), By.XPath(string.Format(CheckboxLctMask, checkbox))), setTo);
        }


        /// <summary>
        /// Click link by partial text
        /// That method is needed for categories which have 'nbsp;' symbols in html markup
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New page object </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            DriverExtensions.WaitForElement(By.PartialLinkText(linkText)).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}