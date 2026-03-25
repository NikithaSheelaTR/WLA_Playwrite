namespace Framework.Common.UI.Products.WestLawNext.Components.Header
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// JurisdictionTooltip - appears after hover over Jurisdiction Button
    /// </summary>
    public class JurisdictionToolTipDialog : BaseModuleRegressionDialog
    {
        private static readonly By JurisdictionTooltipLocator = By.CssSelector("#co_searchJurisdictionHoverContainer .co_infoBox_message");

        /// <summary>
        /// Gets Jurisdiction Current Selection Text.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTextFromJurisdictionTooltip() 
            => DriverExtensions.WaitForElementDisplayed(JurisdictionTooltipLocator).GetText();
    }
}