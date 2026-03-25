namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Form Type Facet for Indigo
    /// </summary>
    public class FormTypeFacetComponent : EdgeBaseFacetComponent
    {
        private const string FacetCountLctMask =
            " //label[.//*[text()='{0}']]/following-sibling::span[@class='SearchFacet-outputText']/span[@class='SearchFacet-outputTextValue']";

        private const string FacetCheckboxLctMask =
            "//label[.//span[text()='{0}']]/preceding-sibling::input[contains(@class, 'SearchFacet-inputCheckbox')]";

        private static readonly By ContainerLocator = By.Id("facet_div_formType");

        private static readonly By CheckedOptionLocator = By.XPath(".//input[@checked]/following-sibling::label[@class='SearchFacet-labelText']");

        private EnumPropertyMapper<FormType, BaseTextModel> formTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the FormType enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<FormType, BaseTextModel> FormTypeMap
            => this.formTypeMap = this.formTypeMap ?? EnumPropertyModelCache.GetMap<FormType, BaseTextModel>();

        /// <summary>
        /// Apply a form type facet
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="formType"> Form type to apply </param>
        /// <param name="state">state</param>
        /// <returns> a new search result page object </returns>
        public T ApplyFacet<T>(FormType formType, bool state) where T : ICreatablePageObject
        {
            string formTypeString = this.FormTypeMap[formType].Text;
            DriverExtensions.SetCheckbox(By.XPath(string.Format(FacetCheckboxLctMask, formTypeString)), state);

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets name of selected options
        /// </summary>
        /// <returns>selected options names</returns>
        public List<string> GetSelectedOptions() =>
            DriverExtensions.GetElements(this.ComponentLocator, CheckedOptionLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Get the count for a specified form type facet option
        /// </summary>
        /// <param name="formType"> The form type option to get the count for</param>
        /// <returns> The count for the form type facet</returns>
        public int GetFormTypeCount(FormType formType)
        {
            DriverExtensions.GetElement(this.ComponentLocator).CustomClick();
            string formTypeString = this.FormTypeMap[formType].Text;
            IWebElement elementCount =
                DriverExtensions.GetElement(By.XPath(string.Format(FacetCountLctMask, formTypeString)));
            return this.GetCount(elementCount);
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>int</returns>
        private int GetCount(IWebElement element) => element.Text.ConvertCountToInt();
    }
}