namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Describe pop up dialog for Graphs Facet charts
    /// </summary>
    public class ChartsPopUpDialog : BaseModuleRegressionDialog
    {
        private static readonly By ChartsPopUpLeftSideTextLocator = By.XPath("//div[@id='coid_tooltipContainer']/div[1]/div[1]");

        private static readonly By ChartsPopUpRightSideTextLocator = By.XPath("//*[@id='coid_tooltipContainer']/div[1]/div[2]");

        /// <summary>
        /// Get Pop up right text
        /// </summary>
        /// <returns> pop up right side text </returns>
        public string GetRightText() => DriverExtensions.GetText(ChartsPopUpRightSideTextLocator);

        /// <summary>
        /// Get Pop up left text
        /// </summary>
        /// <returns> pop up left side text </returns>
        public string GetLeftText() => DriverExtensions.GetText(ChartsPopUpLeftSideTextLocator);
    }
}
