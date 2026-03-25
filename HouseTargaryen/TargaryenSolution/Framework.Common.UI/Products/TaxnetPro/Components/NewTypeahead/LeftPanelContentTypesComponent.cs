namespace Framework.Common.UI.Products.TaxnetPro.Components.NewTypeahead
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.NewTypeahead;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
   
    /// <summary>
    /// LeftPanleContentTypesComponent
    /// </summary>
    public class LeftPanelContentTypesComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// Container Locator
        /// </summary>
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'co-TRDiscover-group co-TRDiscover-category']");
       
        /// <summary>
        /// Selected Content Type Locator
        /// </summary>
        private static readonly By SelectedContentTypeLocator =
                   By.XPath(".//li[contains(@class, 'co_selectedContent')]");

        private EnumPropertyMapper<TNPNewTypeaheadContentType, WebElementInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeftPanelContentTypesComponent"/> class.
        /// </summary>
        public LeftPanelContentTypesComponent()
        {
            this.ActiveTab = this.InitializeActiveTab(this.GetSelectedContentType());
            this.AllPossibleOptions = new Dictionary<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent>
            {
                { TNPNewTypeaheadContentType.Taxnews, new TaxnewsComponent() },
                { TNPNewTypeaheadContentType.AnswerPath, new AnswerPathComponent() },
                { TNPNewTypeaheadContentType.CaseLaw, new CaseLawComponent() },
                { TNPNewTypeaheadContentType.CRAViews, new CRAViewsComponent() },
                { TNPNewTypeaheadContentType.Legislation, new LegislationComponent() },
            };
        }

        /// <summary>
        /// The active tab placeholder
        /// </summary>
        public KeyValuePair<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent> ActiveTab { get; private set; }

        /// <summary>
        /// The active tab placeholder
        /// </summary>
        public Dictionary<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent> AllPossibleOptions { get; }

        /// <summary>
        /// Detail level map initialization
        /// </summary>
        protected EnumPropertyMapper<TNPNewTypeaheadContentType, WebElementInfo> ContentTypeMap
            =>
            this.contentTypeMap =
                this.contentTypeMap ?? EnumPropertyModelCache.GetMap<TNPNewTypeaheadContentType, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/TaxnetPro/NewTypeahead");

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
        public T SelectContentType<T>(TNPNewTypeaheadContentType contentTypeEnum) where T : ContentTypeDetailsBaseComponent
        {
            TNPNewTypeaheadContentType selectedType = this.ActiveTab.Key;
            if (selectedType != contentTypeEnum)
            {
                this.ActiveTab = new KeyValuePair<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent>(
                        contentTypeEnum,
                        this.ClickContentType<T>(contentTypeEnum));
            }

            return this.ActiveTab.Value as T;
        }

        /// <summary>
        /// Gets the currently active/selected delivery dialog tab
        /// </summary>
        /// <returns>the current active delivery dialog tab</returns>
        public TNPNewTypeaheadContentType GetSelectedContentType()
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
        public bool IsContentTypeDisplayed(TNPNewTypeaheadContentType contentTypeEnum)
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
        public ContentTypeDetailsBaseComponent ClickContentTypeOption(TNPNewTypeaheadContentType contentTypeEnum)
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
        private T ClickContentType<T>(TNPNewTypeaheadContentType contentTypeEnum) where T : ContentTypeDetailsBaseComponent
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
        private KeyValuePair<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent> InitializeActiveTab(
            TNPNewTypeaheadContentType currentSelectedTab)
        {
            ContentTypeDetailsBaseComponent contentTypeDetailsBaseComponent;
            switch (currentSelectedTab)
            {
                case TNPNewTypeaheadContentType.Taxnews:
                    contentTypeDetailsBaseComponent = new TaxnewsComponent();
                    break;
                case TNPNewTypeaheadContentType.AnswerPath:
                    contentTypeDetailsBaseComponent = new AnswerPathComponent();
                    break;
                case TNPNewTypeaheadContentType.CaseLaw:
                    contentTypeDetailsBaseComponent = new CaseLawComponent();
                    break;
                case TNPNewTypeaheadContentType.CRAViews:
                    contentTypeDetailsBaseComponent = new CRAViewsComponent();
                    break;
                case TNPNewTypeaheadContentType.Legislation:
                    contentTypeDetailsBaseComponent = new LegislationComponent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new KeyValuePair<TNPNewTypeaheadContentType, ContentTypeDetailsBaseComponent>(
                currentSelectedTab,
                contentTypeDetailsBaseComponent);
        }
    }
}
