namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class contains methods and elements related to the Patent textMate (Claims Selection) page
    /// </summary>
    public class PatentTextMateClaimSelectionPage : TabPage
    {
        private static readonly By ClaimsLocator = By.XPath("//ul[@id='coid_selectClaimSection']/li[@class='co_formInline']");

        private static readonly By ClaimsSelectionDescriptionLocator = By.XPath("//h4/em");

        private static readonly By ClaimsSelectionHeaderLocator = By.ClassName("co_inline");

        private static readonly By IncludeUsApplicationsCheckboxLocator = By.Id("coid_app");

        private static readonly By IncludeUsPatentsCheckboxLocator = By.Id("coid_granted");

        private static readonly By LocateBottomButtonLocator = By.Id("coid_relatedInfo_claimsSelection_locate_bottom");

        private static readonly By LocateTopButtonLocator = By.Id("coid_relatedInfo_claimsSelection_locate_top");

        /// <summary>
        /// Locate Bottom Button
        /// </summary>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        private IWebElement LocateBottomButton => DriverExtensions.WaitForElement(LocateBottomButtonLocator);

        /// <summary>
        /// Locate Top Button
        /// </summary>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        private IWebElement LocateTopButton => DriverExtensions.WaitForElement(LocateTopButtonLocator);

        /// <summary>
        /// Is Claim checked
        /// </summary>
        /// <param name="number">Claim number</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsClaimChecked(int number = 0)
            => DriverExtensions.GetElement(this.GetClaims().ElementAt(number), By.TagName("input")).Selected;

        /// <summary>
        /// Is any Claim checked
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAnyClaimChecked() => this.GetClaims().Any(u => u.Selected);

        /// <summary>
        /// Gets the first Claim's text
        /// </summary>
        /// <param name="number">The Claim's number.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetClaimText(int number = 0) => DriverExtensions.GetElement(this.GetClaims().ElementAt(number), By.TagName("label")).Text;

        /// <summary>
        /// Selects the Claim
        /// </summary>
        /// <param name="number">The Claim's number</param>
        public void SelectClaim(int number = 0) => this.GetClaims().ElementAt(number).Click();

        /// <summary>
        /// Gets Description text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDescriptionText() => DriverExtensions.WaitForElement(ClaimsSelectionDescriptionLocator).Text;

        /// <summary>
        /// Gets the Header Title
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetHeaderTitle() => DriverExtensions.WaitForElement(ClaimsSelectionHeaderLocator).Text;

        /// <summary>
        /// Click Locate Button
        /// </summary>
        /// <returns>The <see cref="PatentTextMateResultPage"/>.</returns>
        public PatentTextMateResultPage ClickLocateButton()
        {
            this.LocateTopButton.Click();
            return new PatentTextMateResultPage();
        }

        /// <summary>
        /// Checks whether Bottom Locate Button is Enabled for interacting or not
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsBottomLocateButtonEnabled() => this.LocateBottomButton.Enabled;

        /// <summary>
        /// Is Bottom Locate Button Present
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsBottomLocateButtonDisplayed() => this.LocateBottomButton.Displayed;

        /// <summary>
        /// Is Include Us Applications Checked
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsIncludeUsApplicationsChecked()
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.WaitForElement(IncludeUsApplicationsCheckboxLocator));

        /// <summary>
        /// Is IncludeUs Patents Checked
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsIncludeUsPatentsChecked()
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.WaitForElement(IncludeUsPatentsCheckboxLocator));

        /// <summary>
        /// Checks whether Top Locate Button is Enabled for interacting or not
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTopLocateButtonEnabled() => this.LocateTopButton.Enabled;

        /// <summary>
        /// Is Top Locate Button Present
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTopLocateButtonDisplayed() => this.LocateTopButton.Displayed;

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