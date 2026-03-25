namespace Framework.Common.UI.Products.WestLawNext.Dialogs.TypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Searches;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Type Ahead Dialog
    /// </summary>
    public class TypeAheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By LinkLocator = By.XPath("//*[contains(@*,'co_typeAheadItem')]//a | //*[@id='co_categoryItems']//a");

        private static readonly By SnapshotStockTickerLocator = By.XPath("//*[@id='co_personLink']/div[1]");

        private static readonly By TypeAheadInfoLocator = By.CssSelector(".co_typeAheadItem ul");

        private static readonly By SuggestionTypeLocator = By.CssSelector(".co_typeAheadItem span b");

        private EnumPropertyMapper<TypeAheadSections, WebElementInfo> typeAheadComponentsMap;

        /// <summary>
        /// TypeAheadComponentsMap
        /// </summary>
        protected EnumPropertyMapper<TypeAheadSections, WebElementInfo> TypeAheadSectionsMap
            => this.typeAheadComponentsMap = this.typeAheadComponentsMap ?? EnumPropertyModelCache.GetMap<TypeAheadSections, WebElementInfo>();

        /// <summary>
        /// Is TypeAhead Section Displayed
        /// </summary>
        /// <param name="section"> The section.  </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsTypeAheadSectionDisplayed(TypeAheadSections section)
            => DriverExtensions.IsDisplayed(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), 3);

        /// <summary>
        /// Is snapshot individual displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSnapshotIndividualDisplayed()
            => DriverExtensions.IsDisplayed(
                    DriverExtensions.WaitForElement(By.XPath(this.TypeAheadSectionsMap[TypeAheadSections.Snapshots].LocatorString)),
                    SnapshotStockTickerLocator);

        /// <summary>
        /// Get Count TypeAhead Sections
        /// </summary>
        /// <param name="section"> The section. </param>
        /// <param name="index"> The element index. </param>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickOnTypeAheadItemByIndex<T>(TypeAheadSections section, int index = 0) where T : ICreatablePageObject
        {
            this.GetTypeAheadResultItems(section)[index].Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click type ahead section item by text value
        /// </summary>
        /// <param name="section"> The section. </param>
        /// <param name="name"> The string value. </param>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickOnTypeAheadItemByName<T>(TypeAheadSections section, string name) where T : ICreatablePageObject
        {
           this.GetTypeAheadResultItems(section).First(e => e.GetText() == name).Click();
           return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Count TypeAhead Sections
        /// </summary>
        /// <param name="section"> The section.  </param>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetCountTypeAheadSections(TypeAheadSections section) => this.GetTypeAheadResultItems(section).Count;

        /// <summary>
        /// Get type-Ahead suggestions 
        /// </summary>
        /// <returns>Type-Ahead suggestions</returns>
        public List<string> GetSuggestionsInTypeAhead()
        {
            DriverExtensions.WaitForElementDisplayed(LinkLocator);
            return DriverExtensions.GetElements(LinkLocator).ToList().Select(e => e.GetText()).ToList();
        }

        /// <summary>
        /// Get type-Ahead suggestions for certain type-Ahead section 
        /// </summary>
        /// <param name="section"> Type- Ahead section</param>
        /// <returns>Type-Ahead suggestions for certain section</returns>
        public List<string> GetSuggestionsInTypeAhead(TypeAheadSections section)
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), LinkLocator);
            return DriverExtensions.GetElements(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), LinkLocator).Select(e => e.GetText()).ToList();
        }

        /// <summary>
        /// Get additional type-Ahead info
        /// </summary>
        /// <returns>Additional type-Ahead info</returns>
        public List<string> GetAdditionalTypeAheadItemInfo()
        {
            DriverExtensions.WaitForElementDisplayed(TypeAheadInfoLocator);
            return DriverExtensions.GetElements(TypeAheadInfoLocator).ToList().Select(e => e.GetText()).ToList();
        }

        /// <summary>
        /// Get additional type-Ahead info for certain type-Ahead section
        /// </summary>
        /// <param name="section"> Type-Ahead section</param>
        /// <returns> Additional type-Ahead info for certain section</returns>
        public List<string> GetAdditionalTypeAheadItemInfo(TypeAheadSections section)
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), TypeAheadInfoLocator);
            return DriverExtensions.GetElements(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), TypeAheadInfoLocator).Select(e => e.GetText()).ToList();
        }

        /// <summary>
        /// Get Suggestions content Type for certain type-Ahead section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public List<string> GetSuggestionsType(TypeAheadSections section) =>               
             DriverExtensions.GetElements(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), SuggestionTypeLocator).Select(e => e.GetText()).ToList();
                
        /// <summary>
        /// Get Count TypeAhead Sections
        /// </summary>
        /// <param name="section"> The section.   </param>
        /// <returns> A list of IWebElements. </returns>
        private IList<IWebElement> GetTypeAheadResultItems(TypeAheadSections section)
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.TypeAheadSectionsMap[section].LocatorString), LinkLocator);
            return
                DriverExtensions.GetElements(
                    DriverExtensions.WaitForElement(By.XPath(this.TypeAheadSectionsMap[section].LocatorString)),
                    LinkLocator);
        }
    }
}