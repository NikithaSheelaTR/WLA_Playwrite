namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SearchWithinResultsDialog
    /// </summary>
    public class SearchWithinResultsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelLinkLocator = By.Id("co_relatedinfo_cancellink_lightbox");

        private static readonly By CloseButtonLocator = By.Id("co_relatedinfo_searchwithin_popupClose");

        private static readonly By ContinueButtonLocator = By.Id("co_relatedinfo_continueButton_lightbox");

        private static readonly By TextLocator = By.ClassName("co_relatedinfo_searchwithinContent");

        /// <summary>
        /// Click search within dialog continue button 
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page.</returns>
        public T ClickSearchWithinDialogContinueButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        /// Search within dialog text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSearchWithinDialogText() => DriverExtensions.GetText(TextLocator);

        /// <summary>
        /// Is search within dialog cancel link enabled
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCancelLinkDisplayed() => DriverExtensions.IsDisplayed(CancelLinkLocator);

        /// <summary>
        /// Is search within dialog close button enabled
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);

        /// <summary>
        /// Is search within dialog continue button enabled
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.IsDisplayed(ContinueButtonLocator);
    }
}