namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// KeyCite Treatment tab => The KeyCite Treatment feature facet component.
    /// </summary>
    public class KeyCiteTreatmentFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string CountKeyCiteWarningOptionLctMask = "//*[text()='{0}']//following-sibling::span";
        private const string FlagKeyCiteWarningOptionLctMask = "//div[contains(@class,'SearchFacet-listItem')]//img[contains(@src, '{0}')]//parent::span[@title]";
        private const string DisabledKeyciteWarningOptionLctMask = "//*[text()='No KeyCite treatment']/ancestor::div[@role='listitem']";
        private const string CheckboxKeyCiteWarningOptionLctMask = "//*[text()='{0}']//preceding-sibling::input[@type='checkbox']";

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCiteTreatmentFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The component locator.
        /// </param>
        public KeyCiteTreatmentFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Is Count of options displayed.
        /// </summary>
        /// <param name="warningTitle">
        /// The warning Title.
        /// </param>
        /// <returns>
        /// True if a count value of the Cited Authorities which is displayed for the KeyCite warning option. 
        /// </returns>
        public bool IsCitedAuthoritiesCountDisplayed(string warningTitle) => DriverExtensions
            .GetElement(By.XPath(string.Format(CountKeyCiteWarningOptionLctMask, warningTitle))).IsDisplayed();

        /// <summary>
        /// Gets the KeyCiteFlag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap => EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>();

        /// <summary>
        /// Is Flag displayed.
        /// </summary>
        /// <param name="flag">
        /// The flag.
        /// </param>
        /// <returns>
        /// True if a KeyCite warnings flag which an option contains is displayed.
        /// </returns>
        public bool IsKeyCiteFlagDisplayed(KeyCiteFlag flag) => DriverExtensions
            .GetElement(By.XPath(string.Format(FlagKeyCiteWarningOptionLctMask, this.KeyCiteFlagsMap[flag].SourceFile)))
            .IsDisplayed();

        /// <summary>
        /// Gets the number of results for a specific KeyCite flag text.
        /// </summary>
        /// <param name="keyCiteFlagText"></param>
        /// <returns>Count of the documents</returns>
        public int GetNumberOfResultsForKeyCiteFlag(string keyCiteFlagText)
        {
            By facetCountLocator = By.XPath(string.Format(CountKeyCiteWarningOptionLctMask, keyCiteFlagText));
            return DriverExtensions.WaitForElement(facetCountLocator).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Checks if the KeyCite warning option is disabled.
        /// </summary>
        /// <param name="warningTitle"></param>
        /// <returns>true is disabled</returns>
        public bool IsKeyCiteWarningOptionDisabled(string warningTitle)
        {
            By disabledKeyciteWarningOptionLocator = By.XPath(string.Format(DisabledKeyciteWarningOptionLctMask, warningTitle));
            return DriverExtensions.IsEnabled(disabledKeyciteWarningOptionLocator);
        }

        /// <summary>
        /// Set checkbox.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="keyCiteFlagText">
        /// The warning Title.
        /// </param>
        public void SetCheckbox(bool action, string keyCiteFlagText)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(CheckboxKeyCiteWarningOptionLctMask, keyCiteFlagText))).SetCheckbox(action);
            DriverExtensions.WaitForJavaScript();
        }
    }
}