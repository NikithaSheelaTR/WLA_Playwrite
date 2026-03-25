namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// InjuryInputComponent
    /// </summary>
    public class InjuryInputComponent : CaseEvReportBuilderBaseComponent
    {
        private const string InjuryLocatorLctMask = ".//label/span[text()='{0}']";

        private static readonly By ContainerLocator = By.XPath("//div[@section='templateData.injury']");

        private static readonly By SearchButtonLocator = By.XPath(".//input[@value='Search']");

        private static readonly By SearchTextBoxLocator = By.XPath(".//input[@id='injurySearchInput']");

        private static readonly By SpinnerLocator = By.XPath(".//*[contains(@class,'co_search_ajaxLoading')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// click the search button in the injury component
        /// </summary>
        public void ClickInjurySearchButton()
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), SearchButtonLocator).Click();
            DriverExtensions.WaitForElementNotDisplayed(this.ComponentLocator, SpinnerLocator);
        }

        /// <summary>
        /// input desired search into text box and click search
        /// </summary>
        /// <param name="query">
        /// query string
        /// </param>
        public void EnterInjurySearchQueryAndClick(string query)
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), SearchTextBoxLocator).SetTextField(query);
            this.ClickInjurySearchButton();
        }

        /// <summary>
        /// Add Injury to Input Criteria
        /// </summary>
        /// <param name="injury">
        /// The injury.
        /// </param>
        public void SelectInjury(string injury)
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(InjuryLocatorLctMask, injury))).Click();
    }
}