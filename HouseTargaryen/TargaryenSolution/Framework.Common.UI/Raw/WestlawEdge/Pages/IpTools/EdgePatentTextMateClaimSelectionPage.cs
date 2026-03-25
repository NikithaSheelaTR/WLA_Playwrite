namespace Framework.Common.UI.Raw.WestlawEdge.Pages.IpTools
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Patent Text Mate Claim Selection Page
    /// </summary>
    public class EdgePatentTextMateClaimSelectionPage : EdgeTabPage
    {
        private static readonly By LocateTopButtonLocator = By.Id("coid_relatedInfo_claimsSelection_locate_top");
        private static readonly By ClaimsLocator = By.XPath("//ul[@id='coid_selectClaimSection']/li[@class='co_formInline']");

        /// <summary>
        /// Click Locate Button
        /// </summary>
        /// <returns>The <see cref="PatentTextMateResultPage"/>.</returns>
        public EdgePatentTextMateResultPage ClickLocateButton()
        {
            DriverExtensions.WaitForElement(LocateTopButtonLocator).Click();
            return new EdgePatentTextMateResultPage();
        }

        /// <summary>
        /// Selects the Claim
        /// </summary>
        /// <param name="number">The Claim's number</param>
        public void SelectClaim(int number = 0) => this.GetClaims().ElementAt(number).Click();

        /// <summary>
        /// Gets the first Claim's text
        /// </summary>
        /// <param name="number">The Claim's number.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetClaimText(int number = 0) => DriverExtensions.GetElement(this.GetClaims().ElementAt(number), By.TagName("label")).Text;

        /// <summary>
        /// Is Claim checked
        /// </summary>
        /// <param name="number">Claim number</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsClaimChecked(int number = 0)
            => DriverExtensions.GetElement(this.GetClaims().ElementAt(number), By.TagName("input")).Selected;

        /// <summary>
        /// Gets all claims
        /// </summary>
        /// <returns>List of all Claims</returns>
        private IReadOnlyCollection<IWebElement> GetClaims()
        {
            DriverExtensions.WaitForElement(ClaimsLocator);
            return DriverExtensions.GetElements(ClaimsLocator);
        }
    }
}