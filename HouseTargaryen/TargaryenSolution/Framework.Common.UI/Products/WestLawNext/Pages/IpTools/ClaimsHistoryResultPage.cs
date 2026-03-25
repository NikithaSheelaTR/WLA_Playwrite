namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The claims history result page.
    /// </summary>
    public class ClaimsHistoryResultPage : TabPage
    {
        private const string TotalNumberOfClaimsApprovedText = "Total number of claims approved:";

        private static readonly By TotalNumberOfClaimsApprovedLocator = By.CssSelector("#coid_relatedinfo_detailsContainer > p > strong");
        
        /// <summary>
        /// Grid Component on the page
        /// </summary>
        public ClaimHistoryGridComponent GridComponent => new ClaimHistoryGridComponent();

        /// <summary>
        /// Gets count of TotalNumberOfClaimsApproved
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetTotalNumberOfClaimsApprovedCount() => this.GetTotalNumberOfClaimsApprovedText().ConvertCountToInt();

        /// <summary>
        /// Checks whether component is presented on page with correct text
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTotalNumberOfClaimsApprovedTextPresent()
            => this.GetTotalNumberOfClaimsApprovedText().StartsWith(TotalNumberOfClaimsApprovedText);

        /// <summary>
        /// Gets TotalNumberOfClaimsApproved component
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetTotalNumberOfClaimsApprovedText() => DriverExtensions.WaitForElement(TotalNumberOfClaimsApprovedLocator).Text;
    }
}