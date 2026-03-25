namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Enums.RI;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Notes of Decision Page
    /// </summary>
    public class NotesOfDecisionsPage : TabPage
    {
        private static readonly By CategoryTableContainerLocator = By.Id("co_relatedInfo_nod_container");

        private static readonly By ContentBodyHeadersLocator =
            By.XPath("//div[contains(@id, 'co_relatedInfo_Heading_')]/*[self::h3 or self::h4 or self::h5]");

        private static readonly By HeaderTitleWithCountLocator = By.Id("co_docToolbarMenuLeft");

        private static readonly By HierarchicalViewLinkLocator = By.Id("coid_nod_showStandardView");

        private static readonly By LeftTopicListLocator = By.Id("coid_relatedInfo_nod_topLeft");

        private static readonly EnumPropertyMapper<NotesOfDesicionsLinks, WebElementInfo> NotesOfDesicionsLinksMap =
            EnumPropertyModelCache.GetMap<NotesOfDesicionsLinks, WebElementInfo>(); 

        private static readonly By SearchTextFieldLocator = By.Id("co_Search_within_nod_textarea");

        private static readonly By SearchButtonLocator = By.Id("co_Search_within_nod_Button");

        private static readonly By SearchWithinTermLocator = By.ClassName("co_searchWithinTerm");

        private static readonly By SelectedCategoryPathLocator =
            By.XPath("//div[@id='co_relatedInfo_NODSelector_top']//li[@class='co_relatedInfo_nod_activeLink']/a");

        private static readonly By SelectFromIndexMessageLocator = By.Id("coid_relatedInfo_NOD_MainContent");

        private static readonly By SortDropDownLocator = By.XPath("//select[@name='coid_relatedInfo_NOD_SortElement']");

        private static readonly By SubCategoryLinksContainerLocator =
            By.XPath("//div[contains(@id, 'co_relatedInfo_nod_middleRight_')]");

        private static readonly By ViewAllLinkLocator = By.Id("coid_relatedInfo_NODViewAllLink");

        private static readonly By NotesOfDecisionsSortByDropDownLocator = By.XPath("//select[@name='coid_relatedInfo_NOD_SortElement']");

        /// <summary>
        /// Notes Of Decisions SortBy Dropdown.
        /// </summary>
        public IDropdown<NodSortByOptions> SortByDropdown { get; } = new Dropdown<NodSortByOptions>(NotesOfDecisionsSortByDropDownLocator);

        /// <summary>
        /// Determines if the all displayed highlaighted terms text are equal to the inputed term
        /// </summary>
        /// <param name="term"> highlighted term. </param>
        /// <returns> True if all highlighted terms text equals inputed terms text otherwise false </returns>
        public bool AreHighlightedTermsEqualInputedTerm(string term) => this.GetHighlightedSearchWithinTerms().All(elem => elem.ToLower().Equals(term.ToLower()));

        /// <summary>
        /// Determines if the sub categories in the body are displayed in alphabetical order. An exception is made
        /// for any phrase containing the word 'generally' or 'general'.
        /// </summary>
        /// <returns>True if sub categories in the body are displayed in alphabetical order</returns>
        public bool BodyContentSortedAlphabetically()
            => this.IsSortedAlphabetically(DriverExtensions.GetElements(ContentBodyHeadersLocator)
                .Where(s => s.Text.Length > 0 && s.TagName != "h3")
                .Select(s => s.Text).ToList());

        /// <summary>
        /// Gets highlighted terms text
        /// </summary>
        /// <returns> list of highlighted terms text </returns>
        public IEnumerable<string> GetHighlightedSearchWithinTerms() => DriverExtensions.GetElements(SearchWithinTermLocator).Select(elem => elem.Text);

        /// <summary>
        /// Click link
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <param name="link">The link.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickLink<T>(NotesOfDesicionsLinks link) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.GetElement(By.Id(NotesOfDesicionsLinksMap[link].Id)));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on Hierarchical View Link
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <returns>
        /// New instance of T page
        /// </returns>
        public T ClickOnHierarchicalViewLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(HierarchicalViewLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Inputs term in search text fiels and clicks search button
        /// </summary>
        /// <typeparam term="string">
        /// </typeparam>
        /// <returns>
        ///  instance of NotesOfDecisionsPage page
        /// </returns>
        public NotesOfDecisionsPage EnterTermAndClickSearch(string term)
        { 
            DriverExtensions.WaitForElementDisplayed(SearchTextFieldLocator).SetTextField(term);
            DriverExtensions.GetElement(SearchButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Get the category displayed in the body of the page.
        /// </summary>
        /// <returns>Topic displayed</returns>
        public string GetCategoryDisplayedInBodyHeader() => this.GetSubCategoryHeadersInBodyTexts().First();

        /// <summary>
        /// Returns the value of the selected category link.
        /// </summary>
        /// <returns>Category name</returns>
        public string GetSelectedCategory() => DriverExtensions.WaitForElement(SelectedCategoryPathLocator).Text;

        /// <summary>
        /// Checks that header with title and count is displayed.
        /// </summary>
        /// <returns>True if header with title and count is displayed.</returns>
        public bool IsHeaderWithTitleAndCountShown()
            => Regex.Match(
                DriverExtensions.WaitForElement(HeaderTitleWithCountLocator).Text,
                "Notes of Decisions\\s" + Constants.NumberWithParenthesesRegex).Success;

        /// <summary>
        /// Is Category Table Container Displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCategoryTableContainerDisplayed() => DriverExtensions.IsDisplayed(CategoryTableContainerLocator);

        /// <summary>
        /// Is Left Topic List Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLeftTopicListDisplayed() => DriverExtensions.IsDisplayed(LeftTopicListLocator);

        /// <summary>
        /// Is Select From Index Message Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSelectFromIndexMessageDisplayed() => DriverExtensions.IsDisplayed(SelectFromIndexMessageLocator);

        /// <summary>
        /// Is SortDropDown Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSortDropDownDisplayed() => DriverExtensions.IsDisplayed(SortDropDownLocator);

        /// <summary>
        /// Is SubCategory Links Container Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSubCategoryLinksContainerDisplayed()
            => DriverExtensions.IsDisplayed(SubCategoryLinksContainerLocator);

        /// <summary>
        /// Is View All Link Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsViewAllLinkDisplayed() => DriverExtensions.IsDisplayed(ViewAllLinkLocator);

        /// <summary>
        /// Determine if all the subcategories listed in the right side of the top container are displayed
        /// throughout the body below the top container.
        /// </summary>
        /// <returns>True if all subcategories are found throughout the body.</returns>
        public bool AreSubCategoriesAreDisplayedInBody() => this.GetSubCategoryLinksTexts().All(this.GetSubCategoryHeadersInBodyTexts().Contains);

        /// <summary>
        /// Determines if the sub category links are displayed in alphabetical order. An exception is made
        /// for any phrase containing the word 'generally' or 'general'.
        /// </summary>
        /// <returns>True if sub category links are displayed in alphabetical order</returns>
        public bool AreSubCategoriesSortedAlphabetically() => this.IsSortedAlphabetically((List<string>)this.GetSubCategoryLinksTexts());

        /// <summary>
        /// Returns the list of texts of SubCategoryHeaders in body.
        /// </summary>
        /// <returns>List of texts of SubCategoryHeaders in body</returns>
        private IList<string> GetSubCategoryHeadersInBodyTexts()
        {
            DriverExtensions.WaitForElement(ContentBodyHeadersLocator);
            IReadOnlyCollection<IWebElement> subCategoryHeadersInBodyElements =
                DriverExtensions.GetElements(ContentBodyHeadersLocator);
            return subCategoryHeadersInBodyElements.Where(s => s.Text.Length > 0).Select(s => s.Text).ToList();
        }

        /// <summary>
        /// Returns the list of texts of SubCategoryLinks.
        /// </summary>
        /// <returns>List of texts of SubCategoryLinks</returns>
        private IList<string> GetSubCategoryLinksTexts()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(SubCategoryLinksContainerLocator), By.TagName("a"))
               .Select(s => s.Text).ToList();

        /// <summary>
        /// Determines if the sub categories are displayed in alphabetical order. An exception is made
        /// for any phrase containing the word 'generally' or 'general'.
        /// </summary>
        /// <param name="categoryList"> The category List. </param>
        /// <returns> True if sub categories in the body are displayed in alphabetical order </returns>
        private bool IsSortedAlphabetically(List<string> categoryList)
        {
            bool isAlphabetical = false;
            string previousSubCategory = string.Empty;

            foreach (string subCategory in categoryList)
            {
                // Exceptions are made for anything with the word 'Generally' or 'General'. This does not need to be in alphabetical order.
                if (!subCategory.ToLower().Contains("general"))
                {
                    if (previousSubCategory.Length > 0)
                    {
                        isAlphabetical = string.Compare(
                                             previousSubCategory,
                                             subCategory,
                                             StringComparison.OrdinalIgnoreCase) < 0;

                        if (!isAlphabetical)
                        {
                            break;
                        }
                    }

                    previousSubCategory = subCategory;
                }
            }

            return isAlphabetical;
        }
    }
}