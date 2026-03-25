namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums;
    using Framework.Common.UI.Products.TaxnetPro.Enums.ContentTypeCategories;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Advanced search page
    /// </summary>
    public class TaxnetProAdvancedSearchPage : CommonAdvancedSearchPage
    {
        private const string ContentTypeExpandLctMask =
            "//ul[@class='co_search_advancedSearchCheckboxGroupList']//button[@aria-label='{0}']";

        private const string ContentTypesLctMask =
            "//button[@aria-label='{0}']/parent::fieldset//input[@type='checkbox']/parent::label";

        private static readonly By AreasOfInterestExpandLocator = By.Id("co_search_advancedSearch_toggleLink_AOI_1");

        private static readonly By AllCommentaryCheckBoxLocator = By.Id("co_search_advancedSearch_COMMENTARY_All");

        private static readonly By AllGovernmentDocCheckBoxLocator = By.Id("co_search_advancedSearch_GOVT_All");

        private static readonly By AllLegislationCheckBoxLocator = By.Id("co_search_advancedSearch_LEGIS_All");

        private EnumPropertyMapper<AreasOfInterest, WebElementInfo> areasOfInterest;

        private EnumPropertyMapper<SortOrderOfResults, WebElementInfo> sortOrderOfResult;

        private EnumPropertyMapper<CommentaryContentCategories, WebElementInfo> commentaryContentTypeValue;

        private EnumPropertyMapper<GovernmentDocContentCategories, WebElementInfo> governmentDocContentTypeValue;

        private EnumPropertyMapper<LegislationContentCategories, WebElementInfo> legislationContentTypeValue;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<AreasOfInterest, WebElementInfo> AreaOfInterest =>
            this.areasOfInterest = this.areasOfInterest
                                   ?? EnumPropertyModelCache.GetMap<AreasOfInterest, WebElementInfo>(
                                       string.Empty,
                                       @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<SortOrderOfResults, WebElementInfo> SortOrderOfResult =>
            this.sortOrderOfResult = this.sortOrderOfResult
                                     ?? EnumPropertyModelCache.GetMap<SortOrderOfResults, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<CommentaryContentCategories, WebElementInfo> CommentaryContentTypeValue =>
            this.commentaryContentTypeValue = this.commentaryContentTypeValue
                                              ?? EnumPropertyModelCache
                                                  .GetMap<CommentaryContentCategories, WebElementInfo>(
                                                      string.Empty,
                                                      @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<GovernmentDocContentCategories, WebElementInfo> GovernmentDocContentTypeValue =>
            this.governmentDocContentTypeValue = this.governmentDocContentTypeValue
                                                 ?? EnumPropertyModelCache
                                                     .GetMap<GovernmentDocContentCategories, WebElementInfo>(
                                                         string.Empty,
                                                         @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>LegislationContentCategories
        protected EnumPropertyMapper<LegislationContentCategories, WebElementInfo> LegislationContentTypeValue =>
            this.legislationContentTypeValue = this.legislationContentTypeValue
                                               ?? EnumPropertyModelCache
                                                   .GetMap<LegislationContentCategories, WebElementInfo>(
                                                       string.Empty,
                                                       @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Get text of area of interest for expand section
        /// </summary>
        /// <returns></returns>
        public string GetAreasOfInterestText() => DriverExtensions.WaitForElement(AreasOfInterestExpandLocator).Text;

        /// <summary>
        /// Click on Provinces Territories expand collapse
        /// </summary>
        public void ClickOnProvincesTerritories()
        {
            DriverExtensions.Click(AreasOfInterestExpandLocator);
        }

        /// <summary>
        /// Checks if Area of interest is checked
        /// </summary>
        /// <param name="areaOfInterest">Area of Interest to check</param>
        /// <returns>true if checked otherwise false</returns>
        public bool IsAreaOfInterestChecked(AreasOfInterest areaOfInterest) =>
            DriverExtensions.IsCheckboxSelected(By.XPath(AreaOfInterest[areaOfInterest].LocatorString));

        /// <summary>
        /// Set Area of Interest checkbox 
        /// </summary>
        /// <param name="toSelect">State to select or unselect the checkbox</param>
        /// <param name="areaOfInterest">Area of Interest to select or unselect</param>
        public void SetAreaOfInterestCheckBox(AreasOfInterest areaOfInterest, bool toSelect = true) =>
            DriverExtensions.SetCheckbox(toSelect, By.XPath(AreaOfInterest[areaOfInterest].LocatorString));

        /// <summary>
        /// Select radio button in sort order
        /// </summary>
        /// <param name="sortOrder">Sort order value to select</param>
        public void SelectSortOrder(SortOrderOfResults sortOrder) =>
            DriverExtensions.WaitForElement(By.Id(SortOrderOfResult[sortOrder].Id)).Click();

        /// <summary>
        /// Get the content type values in Commentary search template
        /// </summary>
        /// <param name="content">Content type to get the sub values</param>
        /// <returns>List of Commentary content types</returns>
        public List<CommentaryContentCategories> GetCommentaryContentTypeValues(CommentaryContentCategories content) =>
            GetContentTypes<CommentaryContentCategories>(
                CommentaryContentTypeValue[content].Text.Replace("&", "&amp;"));

        /// <summary>
        /// Get the content type values in Government document search template
        /// </summary>
        /// <param name="content">Content type to get the sub values</param>
        /// <returns>List of Government document content types</returns>
        public List<GovernmentDocContentCategories> GetGovernmentDocContentTypeValues(GovernmentDocContentCategories content) =>
            GetContentTypes<GovernmentDocContentCategories>(GovernmentDocContentTypeValue[content].Text);


        /// <summary>
        /// Get the content type values in Government document search template
        /// </summary>
        /// <param name="content">Content type to get the sub values</param>
        /// <returns>List of Government document content types</returns>
        public List<LegislationContentCategories> GetLegislationContentTypeValues(LegislationContentCategories content) =>
            GetContentTypes<LegislationContentCategories>(LegislationContentTypeValue[content].Text);


        /// <summary>
        /// Expands all the content types in search template
        /// </summary>
        public void ExpandContentType(CommentaryContentCategories content) =>
            DriverExtensions
                .GetElements(
                    By.XPath(
                        string.Format(
                            ContentTypeExpandLctMask,
                            CommentaryContentTypeValue[content].Text.Replace("&", "&amp;")))).ToList()
                .ForEach(elem => elem.Click());


        /// <summary>
        /// Checks if All Commentary is displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool VerifyAllCommentaryIsDisplayed() => DriverExtensions.IsDisplayed(AllCommentaryCheckBoxLocator);

        /// <summary>
        /// Checks if All Government Documents is displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool VerifyAllGovernmentDocIsDisplayed() =>
            DriverExtensions.IsDisplayed(AllGovernmentDocCheckBoxLocator);

        /// <summary>
        /// Checks if All Legislation checkbox is displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool VerifyAllLegislationIsDisplayed() => DriverExtensions.IsDisplayed(AllLegislationCheckBoxLocator);

        /// <summary>
        /// Gets the content values from all search templates
        /// </summary>
        /// <typeparam name="TEnum">Enumeration type</typeparam>
        /// <param name="contentType">The mapper value</param>
        /// <returns>List of values in enum format</returns>
        private List<TEnum> GetContentTypes<TEnum>(string contentType) where TEnum : struct
        {
            List<string> contentTypeValues = DriverExtensions
                                             .GetElements(By.XPath(string.Format(ContentTypesLctMask, contentType)))
                                             .Select(type => type.Text).ToList();
            List<TEnum> contentTypes = contentTypeValues.Select(
                text => text.GetEnumValueByText<TEnum>(string.Empty, @"Resources/EnumPropertyMaps/TaxnetPro")).ToList();
            return contentTypes;
        }
    }
}