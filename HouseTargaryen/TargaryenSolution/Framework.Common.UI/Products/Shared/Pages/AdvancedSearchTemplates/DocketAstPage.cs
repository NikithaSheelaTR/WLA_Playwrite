namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Docket AST Page
    /// </summary>
    public class DocketAstPage : CommonAdvancedSearchPage
    {
        private static readonly By CaseTypeCheckBoxItemLabelLocator =
            By.XPath("//li[contains(@id,'co_search_advancedSearch_CTP')]");

        private static readonly By PartyTypeCheckBoxItemLabelLocator =
            By.XPath("//li[contains(@id,'co_search_advancedSearch_PartyType')]");

        private const string DocketTypeCheckboxesLctMask = "//div[@class='co_search_advancedSearch_verticalList co_3Column']/ul/li/label[text() ='{0}']";

        /// <summary>
        /// CaseTypeCheckBoxesExist
        /// </summary>
        /// <returns>boolean</returns>
        public bool AreAllCaseCheckBoxesEnabled => this.AreCheckBoxesEnabled(CaseTypeCheckBoxItemLabelLocator);

        /// <summary>
        /// PartyTypeCheckBoxesExist
        /// </summary>
        /// <returns>boolean</returns>
        public bool AreAllPartyCheckboxesEnabled => this.AreCheckBoxesEnabled(PartyTypeCheckBoxItemLabelLocator);

        /// <summary>
        /// Select Case/Party Type By Text
        /// </summary>
        /// <param name="text"> text </param>
        /// <param name="state"> The state of the checkbox. </param>
        public void SelectDocketTypeCheckbox(string text, bool state = true) => DriverExtensions.SetCheckbox(By.XPath(string.Format(DocketTypeCheckboxesLctMask, text)), state);

        /// <summary>
        /// Verifies if checkboxes are enabled
        /// </summary>
        /// <returns>boolean</returns>
        private bool AreCheckBoxesEnabled(By locator) =>DriverExtensions.GetElements(locator).All(element => element.Enabled);                                                  
    }
}