namespace Framework.Common.UI.Products.TaxnetPro.Components.ResultList
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Snippet Onboarding display info box on search result page
    /// </summary>
    public class SnippetDisplayInfoBoxComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("snippetNavigationOnboarding_infoBox_Id");

        private static readonly By TitleLocator = By.XPath("//div[@id='snippetNavigationOnboarding_infoBox_Id']//h3");

        private static readonly By InfoBoxTextLocator =
            By.XPath("//div[@id='snippetNavigationOnboarding_infoBox_Id']//p");

        private static readonly By DontShowAgainCheckboxLocator = By.Id("coid_snippetNavigationOnboardingCheckbox");

        private static readonly By SnippetInfoOkayButtonLocator = By.Id("SnippetNavigationOnboarding-primaryButton");

        private static readonly By SnippetInfoBoxCloseBtnLocator =
            By.XPath("//button[@class='co_infoBox_closeButton']");

        /// <summary>
        /// Component locator of snippet info box
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the title of Snippet info box
        /// </summary>
        /// <returns>Title of the info box</returns>
        public string GetTitleOfSnippetInfoBox() => DriverExtensions.WaitForElement(TitleLocator).GetText();

        /// <summary>
        /// Gets the text from info box
        /// </summary>
        /// <returns>Paragraph text from the snippet box</returns>
        public string GetTextFromSnippetInfoBox() => DriverExtensions.WaitForElement(InfoBoxTextLocator).GetText();

        /// <summary>
        /// Checks if checkbox locator displayed
        /// </summary>
        /// <returns>true, if displayed otherwise false</returns>
        public bool IsDontShowAgainCheckBoxDisplayed() => DriverExtensions.IsDisplayed(DontShowAgainCheckboxLocator);

        /// <summary>
        /// Checks if Okay button is displayed
        /// </summary>
        /// <returns>true, if displayed otherwise false</returns>
        public bool IsSnippetInfoOkayButtonDisplayed() => DriverExtensions.IsDisplayed(SnippetInfoOkayButtonLocator);

        /// <summary>
        /// Closes the Snippet info box if displayed
        /// </summary>
        public void CloseSnippetInfoBox()
        {
            if (DriverExtensions.IsDisplayed(SnippetInfoBoxCloseBtnLocator))
                DriverExtensions.Click(SnippetInfoBoxCloseBtnLocator);
        }
    }
}