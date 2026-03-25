namespace Framework.Common.UI.Products.WestLawNext.Pages.MedicalLitigator
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ExpandedTermsPage
    /// </summary>
    public class ExpandedTermsPage : CommonBrowsePage
    {
        private const string CheckboxLctMask = "//li[contains(label,'{0}')]//input";

        private const string ExpandCollapseIconLctMask = "//*[@class='co_expandedTermHeading' and label[contains(.,'{0}')]]//a";

        private const string LabelsForCheckBoxesLctMask = "//label[contains(.,'{0}')]";

        private static readonly By ContinueButtonLocator = By.ClassName("expandedtermscontinuebutton");

        private static readonly By LabelsForCheckBoxesLocator = By.XPath("//ul[contains(@class,'co_expandedTermRelationshipList')]//label");

        private static readonly By ResultListHeaderLocator = By.ClassName("co_expandedTermHeading");

        private static readonly By MedicalLitigatorLinkLocator = By.LinkText("MedicalLitigator");

        /// <summary>
        /// Select(check the check box) for the label identified in Term
        /// </summary>
        /// <param name="term">The text contained in the label for check box</param>
        public void CheckExpandedTerm(string term) => DriverExtensions.SetCheckbox(By.XPath(string.Format(LabelsForCheckBoxesLctMask, term)), true);

        /// <summary>
        /// Go back to the med lit home page
        /// </summary>
        /// <returns>A new instance of the <see cref="MedLitCategoryPage"/></returns>
        public MedLitCategoryPage ClickMedicalLitigatorLink()
        {
            DriverExtensions.Click(MedicalLitigatorLinkLocator);
            return new MedLitCategoryPage();
        }

        /// <summary>
        /// Click the Continue button
        /// </summary>
        /// <returns>A new instance of the <see cref="ContentTypeSearchResultsPage"/></returns>
        public ContentTypeSearchResultsPage ClickContinueButton()
        {
            DriverExtensions.Click(ContinueButtonLocator);
            return new ContentTypeSearchResultsPage();
        }

        /// <summary>
        /// Click the icon to expand or collapse
        /// </summary>
        /// <param name="entity">The text contained in the label for check box</param>
        /// <param name="expand"> true if icon should be expanded, false otherwise</param>
        public void ClickExpandCollapseIcon(string entity, bool expand)
        {
            By icon = By.XPath(string.Format(ExpandCollapseIconLctMask, entity));
            if ((DriverExtensions.GetAttribute("class", icon).Contains("expand") && expand)
                || (DriverExtensions.GetAttribute("class", icon).Contains("collapse") && !expand))
            {
                DriverExtensions.Click(icon);
            }
        }

        /// <summary>
        /// Get list of labels for the check boxes on the page
        /// </summary>
        /// <returns>List of labels for the check boxes on the page</returns>
        public List<string> GetExpandedTermsList()
            => DriverExtensions.GetElements(LabelsForCheckBoxesLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Return the count for the number of lists displayed on the interim page
        /// </summary>
        /// <returns>Count of elements in the list</returns>
        public int GetCountOfResultLists() => DriverExtensions.GetElements(ResultListHeaderLocator).Count;

        /// <summary>
        /// Verifies if all of the check boxes for a particular list are checked
        /// </summary>
        /// <param name="findList">The text contained in the label for check box</param>
        /// <returns>True if all check boxes selected</returns>
        public bool VerifyAllCheckBoxesAreChecked(string findList)
        {
            IReadOnlyCollection<IWebElement> checkBoxes =
                DriverExtensions.GetElements(By.XPath(string.Format(CheckboxLctMask, findList)));
            bool hasUnselectedCheckboxes = checkBoxes.Any(x => !x.Selected);
            return !hasUnselectedCheckboxes && checkBoxes.Count > 0;
        }
    }
}