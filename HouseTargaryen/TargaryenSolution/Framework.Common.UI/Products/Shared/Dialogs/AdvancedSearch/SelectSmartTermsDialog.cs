namespace Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Smart Terms dialog
    /// </summary>
    public class SelectSmartTermsDialog : BaseModuleRegressionDialog
    {
        private static readonly string AddItemIconButtonLctMask = "//li[@class='co_option']/button[contains(@label, {0})]";

        private static readonly By SaveButtonLocator = By.Id("co_search_alertSmartTermsSave");

        private static readonly By SearchButtonLocator = By.Id("co_searchButtonSmartTerms");

        private static readonly By SearchInputLocator = By.Id("co_smartTermsSearch_input");

        private static readonly By TitleLabelLocator = By.XPath("//*[contains(@id, 'coid_lightboxAriaLabel_')]");

        private EnumPropertyMapper<SmartTermsTabs, WebElementInfo> tabsMap;

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLabelLocator);

        /// <summary>
        /// Add item icon button
        /// </summary>
        public IButton AddItemIconButton(string term) => new Button(SafeXpath.BySafeXpath(AddItemIconButtonLctMask, term));

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

        /// <summary>
        /// Search textbox
        /// </summary>
        public ITextbox SearchTextbox => new Textbox(SearchInputLocator);

        /// <summary>
        /// Add term by text.
        /// </summary>
        /// <param name="tab"> The tab </param>
        /// <param name="term"> The term </param>
        public void AddTermsByText(SmartTermsTabs tab, string term)
        {
            DriverExtensions.WaitForElement(By.Id(this.TabsMap[tab].Id)).Click();
            this.SearchTextbox.SetText(term);
            DriverExtensions.WaitForJavaScript();
            this.SearchButton.Click();
            this.AddItemIconButton(term).Click();
        }

        /// <summary>
        /// Select Smart Terms Tabs Mapper
        /// </summary>
        protected EnumPropertyMapper<SmartTermsTabs, WebElementInfo> TabsMap
            => this.tabsMap = this.tabsMap ?? EnumPropertyModelCache.GetMap<SmartTermsTabs, WebElementInfo>();
    }
}