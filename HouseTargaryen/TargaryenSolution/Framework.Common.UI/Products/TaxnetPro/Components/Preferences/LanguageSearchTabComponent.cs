namespace Framework.Common.UI.Products.TaxnetPro.Components.Preferences
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Language Search Tab in Options Dialog
    /// </summary>
    public class LanguageSearchTabComponent : BaseTabComponent
    {
        private static readonly By PlainLanguageSearchCheckboxLocator = By.Id("coid_userSettingsLanguageSearchRunNaturalAsDefault");
        private static readonly By ShowAllContentRadioBtnLocator = By.Id("coid_userSettingsLanguageSearchShowPpvItems");
        private static readonly By SubscribedContentRadioBtnLocator = By.Id("coid_userSettingsLanguageSearchHidePpvItems");
        private static readonly By ShowSearchSuggestionsCheckboxLocator = By.Id("coid_userSettingsLanguageSearchEnableSearchSuggestions");
        private static readonly By ContainerLocator = By.Id("coid_userSettingsLanguageSearchContent");
        private static string ShowDocumentsInRadionBtnLocator = "//input[@id='coid_userSettingsLanguageSearchDocumentLanguage{0}']";
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Language/Search";

        /// <summary>
        /// Checks or unchecks the plain language search check box
        /// </summary>
        /// <param name="selected">Value determines to select or unselect the check box</param>
        public void SetPlainLanguageSearchCheckbox(bool selected = true) =>
            DriverExtensions.SetCheckbox(PlainLanguageSearchCheckboxLocator, selected);

        /// <summary>
        /// Selects All Content under Show Content section 
        /// </summary>
        public void SelectShowAllContent() =>
            DriverExtensions.WaitForElementDisplayed(ShowAllContentRadioBtnLocator).Click();

        /// <summary>
        /// Selects All Content under Show Content section 
        /// </summary>
        public void SelectShowSearchSuggestionsCheckbox() =>
            DriverExtensions.WaitForElementDisplayed(ShowSearchSuggestionsCheckboxLocator).Click();

        /// <summary>
        /// Selects Subscribed under Show Content section 
        /// </summary>
        public void SelectSubscribedContent() =>
            DriverExtensions.WaitForElementDisplayed(SubscribedContentRadioBtnLocator).Click();

        /// <summary>
        /// Selects show document language option
        /// </summary>
        /// <param name="language"></param>
        public void SelectShowDocumentsInRadioButton(string language) =>
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(ShowDocumentsInRadionBtnLocator, language))).Click();
    }
}