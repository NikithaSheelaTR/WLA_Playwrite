namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// NotesOfDecisionPage
    /// </summary>
    public class NotesOfDecisionPage : BaseRelatedInfoPage
    {
        private static readonly By YellowFlagLocator = By.XPath("//img[@alt='KeyCite Yellow Flag - Negative Treatment']");

        /// <summary>
        /// Determines if the red flag is displayed.
        /// </summary>
        /// <returns>True if the red flag is displayed.</returns>
        public bool IsYellowFlagDisplayed() => DriverExtensions.IsDisplayed(YellowFlagLocator, 5);
    }
}