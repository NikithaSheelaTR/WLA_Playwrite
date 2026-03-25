namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Typeahead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// LeftPaneContentTypesComponent
    /// </summary>
    public class LeftPaneContentTypesComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'co-TRDiscover-group co-TRDiscover-category']");
        private static readonly By SelectedContentTypeLocator =
            By.XPath(".//li[contains(@class, 'co_selectedContent')]");

        private EnumPropertyMapper<NewTypeAheadContentType, WebElementInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeftPaneContentTypesComponent"/> class.
        /// </summary>
        public LeftPaneContentTypesComponent()
        {
            this.ActiveTab = this.InitializeActiveTab(this.GetSelectedContentType());
            this.AllPossibleOptions = new Dictionary<NewTypeAheadContentType, ContentTypeDetailsBaseComponent>
            {
                { NewTypeAheadContentType.PrecisionSuggestions, new PrecisionSuggestionsComponent() },
                { NewTypeAheadContentType.Cases, new CasesComponent() },
                { NewTypeAheadContentType.Suggestions, new SuggestionsComponent() },
                { NewTypeAheadContentType.Regulations, new RegulationsComponent() },
                { NewTypeAheadContentType.Other, new OtherComponent() },
                { NewTypeAheadContentType.SecondarySources, new SecondarySourcesComponent() },
                { NewTypeAheadContentType.StatutesAndCourtRules, new StatutesAndCourtRulesComponent() },
                { NewTypeAheadContentType.Legislation, new LegislationComponent() },
                { NewTypeAheadContentType.GovernmentAndRegulatoryMaterials, new GovRegComponent() }
            };
        }

        /// <summary>
        /// The active tab placeholder
        /// </summary>
        public KeyValuePair<NewTypeAheadContentType, ContentTypeDetailsBaseComponent> ActiveTab { get; private set; }

        /// <summary>
        /// The active tab placeholder
        /// </summary>
        public Dictionary<NewTypeAheadContentType, ContentTypeDetailsBaseComponent> AllPossibleOptions { get; }

        /// <summary>
        /// Detail level map initialization
        /// </summary>
        protected EnumPropertyMapper<NewTypeAheadContentType, WebElementInfo> ContentTypeMap
            =>
            this.contentTypeMap =
                this.contentTypeMap ?? EnumPropertyModelCache.GetMap<NewTypeAheadContentType, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/NewTypeAhead");

        /// <summary>
        /// The select content type.
        /// </summary>
        /// <param name="contentTypeEnum">
        /// The content type enum.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns> New instance of Page Object </returns>
        public T SelectContentType<T>(NewTypeAheadContentType contentTypeEnum) where T : ContentTypeDetailsBaseComponent
        {
            NewTypeAheadContentType selectedType = this.ActiveTab.Key;
            if (selectedType != contentTypeEnum)
            {
                this.ActiveTab = new KeyValuePair<NewTypeAheadContentType, ContentTypeDetailsBaseComponent>(
                        contentTypeEnum,
                        this.ClickContentType<T>(contentTypeEnum));
            }

            return this.ActiveTab.Value as T;
        }

        /// <summary>
        /// Gets the currently active/selected delivery dialog tab
        /// </summary>
        /// <returns>the current active delivery dialog tab</returns>
        public NewTypeAheadContentType GetSelectedContentType()
        {
            string tabId = DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), SelectedContentTypeLocator)
                                           .GetAttribute("id");

            return this.ContentTypeMap.Where(x => x.Value.LocatorString.Contains(tabId)).Select(x => x.Key).First();
        }

        /// <summary>
        /// Gets the currently active/selected delivery dialog tab
        /// </summary>
        /// <returns>the current active delivery dialog tab</returns>
        public string GetSelectedContentTypeName() 
        
        {
            string tabId = DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), SelectedContentTypeLocator)
                                           .GetAttribute("id");

            return this.ContentTypeMap.Where(x => x.Value.LocatorString.Contains(tabId)).Select(x => x.Value.Text).First();
        }

     /// <summary>
    /// The is content type displayed.
    /// </summary>
    /// <param name="contentTypeEnum">
    /// The content type enum.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IsContentTypeDisplayed(NewTypeAheadContentType contentTypeEnum)
        {
            DriverExtensions.WaitForJavaScript();
            IWebElement facetElement = DriverExtensions.WaitForElement(By.XPath(this.ContentTypeMap[contentTypeEnum].LocatorString));

            return facetElement.Displayed && facetElement.Text.Equals(this.ContentTypeMap[contentTypeEnum].Text);
        }

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="contentTypeEnum">tab option</param>
        /// <returns>ITAB object</returns>
        public ContentTypeDetailsBaseComponent ClickContentTypeOption(NewTypeAheadContentType contentTypeEnum)
        {
            DriverExtensions.WaitForElement(By.XPath(this.contentTypeMap[contentTypeEnum].LocatorString)).Click();
            this.ActiveTab = this.InitializeActiveTab(this.GetSelectedContentType());
            return this.AllPossibleOptions[contentTypeEnum];
        }

        /// <summary>
        /// The click content type.
        /// </summary>
        /// <param name="contentTypeEnum">
        /// The content type enum.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns> New instance of Page Object </returns>
        private T ClickContentType<T>(NewTypeAheadContentType contentTypeEnum) where T : ContentTypeDetailsBaseComponent
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.ContentTypeMap[contentTypeEnum].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The initialize active tab.
        /// </summary>
        /// <param name="currentSelectedTab">
        /// The current selected tab.
        /// </param>
        /// <returns>
        /// The <see cref="T:KeyValuePair"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException
        /// </exception>
        private KeyValuePair<NewTypeAheadContentType, ContentTypeDetailsBaseComponent> InitializeActiveTab(
            NewTypeAheadContentType currentSelectedTab)
        {
            ContentTypeDetailsBaseComponent contentTypeDetailsBaseComponent;
            switch (currentSelectedTab)
            {
                case NewTypeAheadContentType.Suggestions:
                    contentTypeDetailsBaseComponent = new SuggestionsComponent();
                    break;
                case NewTypeAheadContentType.PrecisionSuggestions:
                    contentTypeDetailsBaseComponent = new PrecisionSuggestionsComponent();
                    break;
                case NewTypeAheadContentType.Cases:
                    contentTypeDetailsBaseComponent = new CasesComponent();
                    break;
                case NewTypeAheadContentType.CasesAndDecisions:
                    contentTypeDetailsBaseComponent = new CasesComponent();
                    break;
                case NewTypeAheadContentType.StatutesAndCourtRules:
                    contentTypeDetailsBaseComponent = new StatutesAndCourtRulesComponent();
                    break;
                case NewTypeAheadContentType.SecondarySources:
                    contentTypeDetailsBaseComponent = new SecondarySourcesComponent();
                    break;
                case NewTypeAheadContentType.Regulations:
                    contentTypeDetailsBaseComponent = new RegulationsComponent();
                    break;
                case NewTypeAheadContentType.Other:
                    contentTypeDetailsBaseComponent = new OtherComponent();
                    break;
                case NewTypeAheadContentType.Legislation:
                    contentTypeDetailsBaseComponent = new LegislationComponent();
                    break;
                case NewTypeAheadContentType.GovernmentAndRegulatoryMaterials:
                    contentTypeDetailsBaseComponent = new GovRegComponent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new KeyValuePair<NewTypeAheadContentType, ContentTypeDetailsBaseComponent>(
                currentSelectedTab,
                contentTypeDetailsBaseComponent);
        }
    }
}