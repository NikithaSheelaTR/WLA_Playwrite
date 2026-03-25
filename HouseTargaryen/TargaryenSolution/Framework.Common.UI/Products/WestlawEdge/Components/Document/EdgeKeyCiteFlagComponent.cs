namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Key Cite Flag Component on the Indigo Document page
    /// </summary>
    public class EdgeKeyCiteFlagComponent : BaseModuleRegressionComponent
    {
        private static readonly By ImpliedOverrulingLocator = By.XPath("//div[@id='co_readingModeCitatorFlag']//img");

        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'co_keyCiteFlagWidgetContainer')]");

        private static readonly By KeyCiteFlagLocator = By.XPath("//div[contains(@class,'co_keyCiteFlagWidgetContainer ')]/div[@id ='co_readingModeCitatorFlag']/span | //a[contains(@id,'coid_relatedInfo_SNT_Statute_kcFlag_')]");

      
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Is Key Cite Flag Widget Displayed
        /// </summary>
        /// <returns>true if element present</returns>
        /// isDisplayed method not working
        public bool IsImpliedOverrulingIconPresent() => DriverExtensions.IsEnabled(ImpliedOverrulingLocator);

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public List<KeyCiteFlag> GetKeyCiteFlags()
        {
            var flagList = new List<KeyCiteFlag>();
            if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
            {
                List<string> flagClasses = DriverExtensions.GetElements(KeyCiteFlagLocator).Select(flag => flag.GetAttribute("class")).ToList();
               
                flagClasses.ForEach(flagClass => {
                    flagList.Add(KeyCiteFlagsMap.Single(
                        map => !string.IsNullOrEmpty(map.Value.ClassName)
                               && flagClass.Contains(map.Value.ClassName)).Key);
                });
            }

            return flagList;
        }

        /// <summary>
        /// Gets NegativeTreatment Text
        /// </summary>
        public string GetNegativeTreatmentText() => DriverExtensions.GetText(this.ComponentLocator);
    }
}
