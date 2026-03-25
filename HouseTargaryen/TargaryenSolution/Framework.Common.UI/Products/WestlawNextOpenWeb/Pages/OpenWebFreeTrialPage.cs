namespace Framework.Common.UI.Products.WestlawNextOpenWeb.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// open web free trial page
    /// </summary>
    public class OpenWebFreeTrialPage : BaseModuleRegressionPage
    {
        private static readonly By FreeTrialPageSectionLocator = By.XPath("//h1[contains(text(),'tasks with a')]");

        /// <summary>
        /// Get text from Section on the Free Trial page
        /// </summary>
        /// <returns> Text from section </returns>
        public string GetTextFromSection()
            =>
                DriverExtensions.IsDisplayed(FreeTrialPageSectionLocator, 5)
                    ? DriverExtensions.GetText(FreeTrialPageSectionLocator)
                    : string.Empty;
    }
}
