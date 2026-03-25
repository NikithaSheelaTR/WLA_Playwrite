namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Key Cite Flag Component on the Document page
    /// </summary>
    public class KeyCiteFlagComponent : BaseModuleRegressionComponent
    {
        private static readonly By ViewAllLinkLocator = By.LinkText("View all");

        private static readonly By ContainerLocator = By.ClassName("co_keyCiteFlagWidgetContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click View All link
        /// </summary>
        /// <returns> The <see cref="ValidityPage"/>. </returns>
        public ValidityPage ClickViewAllLink()
        {
            DriverExtensions.WaitForElement(ViewAllLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<ValidityPage>();
        }

        /// <summary>
        /// Verify View All Link is Displayed
        /// </summary>
        /// <returns> True if View All link is displayed, false otherwise </returns>
        public bool IsViewAllLinkDisplayed() => DriverExtensions.IsDisplayed(ViewAllLinkLocator);
    }
}