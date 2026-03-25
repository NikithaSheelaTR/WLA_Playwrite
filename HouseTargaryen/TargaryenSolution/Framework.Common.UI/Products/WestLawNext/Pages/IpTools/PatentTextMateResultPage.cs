namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class contains methods and elements related to the Patent textMate result page
    /// </summary>
    public class PatentTextMateResultPage : TabPage
    {
        private static readonly By PatentTextMateResultTableLocator = By.Id("co_relatedInfo_table_claimsAnalyzer");

        private static readonly By ReturnToClaimsSelectionLocator = By.CssSelector("#coid_claimsReturnToQuery, #claimsReturnToQueryLinkId > a");

        /// <summary>
        /// Grid Component
        /// </summary>
        public PatentTextMateGridComponent GridComponent { get; } = new PatentTextMateGridComponent();

        /// <summary>
        /// Click ReturnTo Claims Selection Button
        /// </summary>
        /// <returns>The <see cref="PatentTextMateClaimSelectionPage"/>.</returns>
        public PatentTextMateClaimSelectionPage ClickReturnToClaimsSelectionButton()
        {
            DriverExtensions.WaitForElement(ReturnToClaimsSelectionLocator).Click();
            return new PatentTextMateClaimSelectionPage();
        }

        /// <summary>
        /// Checks whether Patent TextMatePage is Displayed or not
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPatentTextMatePageDisplayed()
            => DriverExtensions.WaitForElement(PatentTextMateResultTableLocator).Displayed
                && this.GridComponent.GetAllColumnsHeadersTitles().TrueForAll(u => u != string.Empty);

        /// <summary>
        /// Checks whether Return To Claim Selection Button is presented or not
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsReturnToClaimSelectionButtonDisplayed() => DriverExtensions.IsDisplayed(ReturnToClaimsSelectionLocator, 5);
    }
}